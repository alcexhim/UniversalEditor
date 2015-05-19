using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.CoreObject;
using UniversalEditor.ObjectModels.Contact;
using UniversalEditor.ObjectModels.CoreObject;

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
			objectModels.Push(new CoreObjectObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			CoreObjectObjectModel core = (objectModels.Pop() as CoreObjectObjectModel);
			ContactObjectModel contact = (objectModels.Pop() as ContactObjectModel);

			CoreObjectGroup vcard = core.Groups["VCARD"];
			if (vcard == null) throw new InvalidDataFormatException("File does not contain a top-level VCARD group");

			bool hasVersion = false;

			foreach (CoreObjectProperty prop in vcard.Properties)
			{
				switch (prop.Name)
				{
					case "VERSION":
					{
						if (!hasVersion)
						{
							if (prop.Values.Count > 0)
							{
								mvarFormatVersion = new Version(prop.Values[0].ToString());
								hasVersion = true;
							}
						}
						break;
					}
					case "N":
					{
						ContactName name = new ContactName();
						if (prop.Values.Count > 0)
						{
							name.GivenName = prop.Values[0];
							if (prop.Values.Count > 1)
							{
								name.FamilyName = prop.Values[0];
								name.GivenName = prop.Values[1];
								if (prop.Values.Count > 2)
								{
									// third assuming is middle name
									name.MiddleName = prop.Values[2];
									if (prop.Values.Count > 3)
									{
										// per wikipedia fourth is title
										name.Title = prop.Values[3];
									}
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
							if (prop.Values.Count > 0)
							{
								contact.Names[contact.Names.Count - 1].FormattedName = prop.Values[0].ToString();
							}
						}
						else
						{
							ContactName name = new ContactName();
							if (prop.Values.Count > 0)
							{
								name.FormattedName = prop.Values[0].ToString();
							}
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
			CoreObjectObjectModel core = new CoreObjectObjectModel();

			objectModels.Push(core);
		}
	}
}
