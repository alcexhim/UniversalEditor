//
//  UDLDataFormat.cs - provides a DataFormat for manipulating Universal Data Link shortcuts
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.PropertyList;

using UniversalEditor.ObjectModels.DataLink;

namespace UniversalEditor.DataFormats.DataLink.UniversalDataLink
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Universal Data Link shortcuts.
	/// </summary>
	public class UDLDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(DataLinkObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.Bootstrap);
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

			Group oledb = plom.Items.OfType<Group>("oledb");
			if (oledb == null) throw new InvalidDataFormatException("File does not contain a [oledb] group");
			// if (oledb.CommentAfter != "Everything after this line is an OLE DB initstring") throw new InvalidDataFormatException("File does not contain the magic comment string \"Everything after this line is an OLE DB initstring\"");

			Property provider = oledb.Items.OfType<Property>("Provider");
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

			oledb.Items.AddProperty("Provider", connstr.ToString());

			plom.Items.Add(oledb);

			objectModels.Push(plom);
		}
	}
}
