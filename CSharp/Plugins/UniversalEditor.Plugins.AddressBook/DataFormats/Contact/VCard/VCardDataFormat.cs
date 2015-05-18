using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.PropertyList.CoreObject;
using UniversalEditor.ObjectModels.Contact;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.Contact.VCard
{
	public class VCardDataFormat : CoreObjectDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(ContactObjectModel), DataFormatCapabilities.All);
				_dfr.ContentTypes.Add("text/vcard");
				_dfr.ContentTypes.Add("text/x-vcard");						// deprecated
				_dfr.ContentTypes.Add("text/directory;profile=vCard");		// deprecated
				_dfr.ContentTypes.Add("text/directory");		// deprecated
				_dfr.Sources.Add("https://en.wikipedia.org/wiki/VCard");
			}
			return _dfr;
		}

		private Version mvarFormatVersion = new Version(4, 0);
		public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			ContactObjectModel contact = (objectModels.Pop() as ContactObjectModel);

			Group vcard = plom.Groups["VCARD"];
			if (vcard == null) throw new InvalidDataFormatException("File does not contain a top-level VCARD group");

			bool hasVersion = false;

			foreach (Property prop in vcard.Properties)
			{
				switch (prop.Name)
				{
					case "VERSION":
					{
						if (!hasVersion)
						{
							mvarFormatVersion = new Version(prop.Value.ToString());
							hasVersion = true;
						}
						break;
					}
					case "N":
					{
						ContactName name = new ContactName();
						string[] splits = prop.Value.ToString().Split(new char[] { ';' });
						if (splits.Length > 1)
						{
							name.FamilyName = splits[0];
							name.GivenName = splits[1];
							if (splits.Length > 2)
							{
								// third assuming is middle name
								name.MiddleName = splits[2];
								if (splits.Length > 3)
								{
									// per wikipedia fourth is title
									name.Title = splits[3];
								}
							}
						}
						contact.Names.Add(name);
						break;
					}
					case "FN":
					{
						// formatted name
						if (contact.Names.Count > 0)
						{
							contact.Names[contact.Names.Count - 1].FormattedName = prop.Value.ToString();
						}
						else
						{
							ContactName name = new ContactName();
							name.FormattedName = prop.Value.ToString();
							contact.Names.Add(name);
						}
						break;
					}
					case "ORG":
					{
						// organizations
						break;
					}
					case "TITLE":
					{
						// organizations
						break;
					}
				}
			}


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ContactObjectModel contact = (objectModels.Pop() as ContactObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			objectModels.Push(plom);
		}
	}
}
