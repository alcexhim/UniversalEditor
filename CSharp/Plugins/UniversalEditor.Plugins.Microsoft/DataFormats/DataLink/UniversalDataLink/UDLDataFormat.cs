using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.PropertyList;

using UniversalEditor.ObjectModels.DataLink;

namespace UniversalEditor.DataFormats.DataLink.UniversalDataLink
{
	public class UDLDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(DataLinkObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.Bootstrap);
				_dfr.Filters.Add("Universal Data Link", new byte?[][] { new byte?[] { (byte)'[', (byte)'o', (byte)'l', (byte)'e', (byte)'d', (byte)'b', (byte)']' } }, new string[] { "*.udl" });
				_dfr.Sources.Add("http://msdn.microsoft.com/en-us/library/ms722656%28v=vs.71%29.aspx");
			}
			return _dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			DataLinkObjectModel data = (objectModels.Pop() as DataLinkObjectModel);

			Group oledb = plom.Groups["oledb"];
			if (oledb == null) throw new InvalidDataFormatException("File does not contain a [oledb] group");
			// if (oledb.CommentAfter != "Everything after this line is an OLE DB initstring") throw new InvalidDataFormatException("File does not contain the magic comment string \"Everything after this line is an OLE DB initstring\"");

			Property provider = oledb.Properties["Provider"];
			if (provider == null) throw new InvalidDataFormatException("File does not contain a \"Provider\" property in the \"oledb\" group");

			string connstr = provider.Value.ToString();
			string[] providerNameAndVersionParts = connstr.Split(new char[] { ';' }, 1);
			string providerNameAndVersion = providerNameAndVersionParts[0];
			string connstr1 = providerNameAndVersionParts[1];

			string providerName = providerNameAndVersion;
			string providerVersion = null;
			if (providerNameAndVersion.Contains("."))
			{
				string[] pnav = providerNameAndVersion.Split(new char[] { '.' }, 2);
				providerName = pnav[0];
				providerVersion = pnav[1];
			}
			data.ProviderName = providerName;
			data.ProviderVersion = providerVersion;

			Property.PropertyCollection props = ParseConnectionString(connstr1);

			foreach (Property p in props)
			{
				if (p.Value == null) continue;
				switch (p.Name)
				{
					case "Data Source":
					{
						data.DataSourceName = p.Value.ToString();
						break;
					}
					case "Initial Catalog":
					{
						data.InitialCatalog = p.Value.ToString();
						break;
					}
					case "Integrated Security":
					{
						data.UseIntegratedSecurity = (p.Value.ToString() == "True");
						break;
					}
					case "Persist Security Info":
					{
						data.PersistSecurityInformation = (p.Value.ToString() == "True");
						break;
					}
					default:
					{
						data.Properties.Add(p.Clone() as Property);
						break;
					}
				}
			}
		}

		private Property.PropertyCollection ParseConnectionString(string connstr)
		{
			Property.PropertyCollection props = new Property.PropertyCollection();
			string next = String.Empty;
			Property prop = new Property();
			for (int i = 0; i < connstr.Length; i++)
			{
				char c = connstr[i];
				if (c == '=')
				{
					if (i < connstr.Length - 1 && connstr[i + 1] == '=')
					{
						next += "=";
						i++;
						continue;
					}
					else
					{
						prop.Name = next;
						next = String.Empty;
					}
				}
				else if (c == ';')
				{
					prop.Value = next;
					next = String.Empty;
					props.Add(prop);
					prop = new Property();
				}
				else
				{
					connstr += c;
				}
			}
			return props;
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			DataLinkObjectModel data = (objectModels.Pop() as DataLinkObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			Group oledb = new Group("oledb");
			oledb.CommentAfter = "Everything after this line is an OLE DB initstring";

			StringBuilder connstr = new StringBuilder();
			connstr.Append(data.ProviderName);
			if (data.ProviderVersion != null)
			{
				connstr.Append(".");
				connstr.Append(data.ProviderVersion);
			}
			connstr.Append(";");
			connstr.Append("Persist Security Info=");
			if (data.PersistSecurityInformation)
			{
				connstr.Append("True");
			}
			else
			{
				connstr.Append("False");
			}
			connstr.Append(";Data Source=");
			connstr.Append(data.DataSourceName);
			connstr.Append(";Initial Catalog=");
			connstr.Append(data.InitialCatalog);
			if (data.UseIntegratedSecurity)
			{
				connstr.Append(";Integrated Security=SSPI");
			}

			foreach (Property prop in data.Properties)
			{
				connstr.Append(";");
				connstr.Append(prop.Name.Replace("=", "=="));
				connstr.Append("=");
				connstr.Append(prop.Value.ToString());
			}

			oledb.Properties.Add("Provider", connstr.ToString());

			plom.Groups.Add(oledb);

			objectModels.Push(plom);
		}
	}
}
