//
//  ContactDataFormat.cs - provides a DataFormat for manipulating contact information in Microsoft XML contact format
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

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Contact;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Contact.Microsoft
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating contact information in Microsoft XML contact format.
	/// </summary>
	public class ContactDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(ContactObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			ContactObjectModel contact = (objectModels.Pop() as ContactObjectModel);

			MarkupTagElement tagContact = (mom.Elements["c:contact"] as MarkupTagElement);
			if (tagContact == null) throw new InvalidDataFormatException("File does not contain a top-level 'c:contact' element");

			MarkupTagElement tagNotes = (tagContact.Elements["c:Notes"] as MarkupTagElement);
			if (tagNotes != null)
			{
				LoadContactComplexType(tagNotes, contact.Notes);
				contact.Notes.Content = tagNotes.Value;
			}

			MarkupTagElement tagGender = (tagContact.Elements["c:Gender"] as MarkupTagElement);
			if (tagGender != null)
			{
				// idk why this breaks
				switch (tagGender.Value)
				{
					case "Unspecified":
					{
						// contact.Gender = ContactGender.Unspecified;
						break;
					}
					case "Male":
					{
						// contact.Gender = ContactGender.Male;
						break;
					}
					case "Female":
					{
						// contact.Gender = ContactGender.Female;
						break;
					}
				}
			}

			MarkupTagElement tagCreationDate = (tagContact.Elements["c:CreationDate"] as MarkupTagElement);
			if (tagCreationDate != null)
			{
				contact.CreationDate = DateTime.Parse(tagCreationDate.Value);
			}

			MarkupTagElement tagExtended = (tagContact.Elements["c:Extended"] as MarkupTagElement);
			if (tagExtended != null)
			{
				foreach (MarkupElement item in tagExtended.Elements)
				{
					MarkupTagElement tag = (item as MarkupTagElement);
					if (tag == null) continue;

					switch (tag.FullName)
					{
						case "MSWABMAPI:PropTag0x3A58101F":
						{
							MarkupAttribute attType = tag.Attributes["c:type"];
							MarkupAttribute attContentType = tag.Attributes["c:ContentType"];
							MarkupAttribute attVersion = tag.Attributes["c:Version"];

							if (attContentType != null)
							{
								switch (attContentType.Value.ToLower())
								{
									case "binary/x-ms-wab-mapi":
									{
										string content = tag.Value;
										byte[] data = Convert.FromBase64String(content);

										// LoadContactComplexType(tag, item);
										break;
									}
								}
							}
							break;
						}
					}
				}
			}

			#region ContactIdentifiers
			MarkupTagElement tagContactIDCollection = (tagContact.Elements["c:ContactIDCollection"] as MarkupTagElement);
			if (tagContactIDCollection != null)
			{
				foreach (MarkupElement el in tagContactIDCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:ContactID") continue;

					ContactIdentifier id = new ContactIdentifier();
					LoadContactComplexType(tag, id);

					MarkupTagElement tagValue = (tag.Elements["c:Value"] as MarkupTagElement);
					id.Value = new Guid(tagValue.Value);

					contact.Identifiers.Add(id);
				}
			}
			#endregion
			#region EmailAddresses
			MarkupTagElement tagEmailAddressCollection = (tagContact.Elements["c:EmailAddressCollection"] as MarkupTagElement);
			if (tagEmailAddressCollection != null)
			{
				foreach (MarkupElement el in tagEmailAddressCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:EmailAddress") continue;

					ContactEmailAddress item = new ContactEmailAddress();

					MarkupTagElement tagType = (tag.Elements["c:Type"] as MarkupTagElement);
					if (tagType != null) item.Type = tagType.Value;

					MarkupTagElement tagAddress = (tag.Elements["c:Address"] as MarkupTagElement);
					if (tagAddress != null) item.Address = tagAddress.Value;

					LoadContactComplexType(tag, item);
					LoadLabelCollection(tag.Elements["c:LabelCollection"] as MarkupTagElement, item);

					contact.EmailAddresses.Add(item);
				}
			}
			#endregion
			#region Names
			MarkupTagElement tagNameCollection = (tagContact.Elements["c:NameCollection"] as MarkupTagElement);
			if (tagNameCollection != null)
			{
				foreach (MarkupElement elName in tagNameCollection.Elements)
				{
					MarkupTagElement tagName = (elName as MarkupTagElement);
					if (tagName == null) continue;
					if (tagName.FullName != "c:Name") continue;

					ContactName item = new ContactName();

					LoadContactComplexType(tagName, item);

					MarkupTagElement tagNickName = (tagName.Elements["c:NickName"] as MarkupTagElement);
					if (tagNickName != null) item.Nickname = tagNickName.Value;

					MarkupTagElement tagTitle = (tagName.Elements["c:Title"] as MarkupTagElement);
					if (tagTitle != null) item.Nickname = tagTitle.Value;

					MarkupTagElement tagFormattedName = (tagName.Elements["c:FormattedName"] as MarkupTagElement);
					if (tagFormattedName != null) item.FormattedName = tagFormattedName.Value;

					MarkupTagElement tagFamilyName = (tagName.Elements["c:FamilyName"] as MarkupTagElement);
					if (tagFamilyName != null) item.FamilyName = tagFamilyName.Value;

					MarkupTagElement tagMiddleName = (tagName.Elements["c:MiddleName"] as MarkupTagElement);
					if (tagMiddleName != null) item.MiddleName = tagMiddleName.Value;

					MarkupTagElement tagGivenName = (tagName.Elements["c:GivenName"] as MarkupTagElement);
					if (tagGivenName != null) item.GivenName = tagGivenName.Value;

					contact.Names.Add(item);
				}
			}
			#endregion
			#region Physical Addresses
			MarkupTagElement tagPhysicalAddressCollection = (tagContact.Elements["c:PhysicalAddressCollection"] as MarkupTagElement);
			if (tagPhysicalAddressCollection != null)
			{
				foreach (MarkupElement el in tagPhysicalAddressCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:PhysicalAddress") continue;

					ContactPhysicalAddress item = new ContactPhysicalAddress();

					#region Attributes
					{
						MarkupAttribute attElementID = tag.Attributes["c:ElementID"];
						if (attElementID != null) item.ElementID = new Guid(attElementID.Value);

						MarkupAttribute attVersion = tag.Attributes["c:Version"];
						if (attVersion != null)
						{

						}

						MarkupAttribute attModificationDate = tag.Attributes["c:ModificationDate"];
						if (attModificationDate != null) item.ModificationDate = DateTime.Parse(attModificationDate.Value);
					}
					#endregion

					#region Country
					{
						MarkupTagElement tag1 = (tag.Elements["c:Country"] as MarkupTagElement);
						if (tag1 != null)
						{
							MarkupAttribute attVersion = tag1.Attributes["c:Version"];
							if (attVersion != null)
							{

							}

							string value = tag1.Value;
							DateTime modificationDate = DateTime.Now;

							MarkupAttribute attModificationDate = tag1.Attributes["c:ModificationDate"];
							if (attModificationDate != null) modificationDate = DateTime.Parse(attModificationDate.Value);

							item.Country = new ContactGenericField<string>(value, modificationDate);
						}
					}
					#endregion
					#region PostalCode
					{
						MarkupTagElement tag1 = (tag.Elements["c:PostalCode"] as MarkupTagElement);
						if (tag1 != null)
						{
							MarkupAttribute attVersion = tag1.Attributes["c:Version"];
							if (attVersion != null)
							{

							}

							string value = tag1.Value;
							DateTime modificationDate = DateTime.Now;

							MarkupAttribute attModificationDate = tag1.Attributes["c:ModificationDate"];
							if (attModificationDate != null) modificationDate = DateTime.Parse(attModificationDate.Value);

							item.PostalCode = new ContactGenericField<string>(value, modificationDate);
						}
					}
					#endregion
					#region Region
					{
						MarkupTagElement tag1 = (tag.Elements["c:Region"] as MarkupTagElement);
						if (tag1 != null)
						{
							MarkupAttribute attVersion = tag1.Attributes["c:Version"];
							if (attVersion != null)
							{

							}

							string value = tag1.Value;
							DateTime modificationDate = DateTime.Now;

							MarkupAttribute attModificationDate = tag1.Attributes["c:ModificationDate"];
							if (attModificationDate != null) modificationDate = DateTime.Parse(attModificationDate.Value);

							item.Region = new ContactGenericField<string>(value, modificationDate);
						}
					}
					#endregion
					#region Locality
					{
						MarkupTagElement tag1 = (tag.Elements["c:Locality"] as MarkupTagElement);
						if (tag1 != null)
						{
							MarkupAttribute attVersion = tag1.Attributes["c:Version"];
							if (attVersion != null)
							{

							}

							string value = tag1.Value;
							DateTime modificationDate = DateTime.Now;

							MarkupAttribute attModificationDate = tag1.Attributes["c:ModificationDate"];
							if (attModificationDate != null) modificationDate = DateTime.Parse(attModificationDate.Value);

							item.Locality = new ContactGenericField<string>(value, modificationDate);
						}
					}
					#endregion
					#region Street
					{
						MarkupTagElement tag1 = (tag.Elements["c:Street"] as MarkupTagElement);
						if (tag1 != null)
						{
							MarkupAttribute attVersion = tag1.Attributes["c:Version"];
							if (attVersion != null)
							{

							}

							string value = tag1.Value;
							DateTime modificationDate = DateTime.Now;

							MarkupAttribute attModificationDate = tag1.Attributes["c:ModificationDate"];
							if (attModificationDate != null) modificationDate = DateTime.Parse(attModificationDate.Value);

							item.StreetAddress = new ContactGenericField<string>(value, modificationDate);
						}
					}
					#endregion
					#region Labels
					{
						LoadLabelCollection(tag.Elements["c:LabelCollection"] as MarkupTagElement, item);
					}
					#endregion

					contact.PhysicalAddresses.Add(item);
				}
			}
			#endregion
			#region Phone Numbers
			MarkupTagElement tagPhoneNumberCollection = (tagContact.Elements["c:PhoneNumberCollection"] as MarkupTagElement);
			if (tagPhoneNumberCollection != null)
			{
				foreach (MarkupElement el in tagPhoneNumberCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:PhoneNumber") continue;

					ContactPhoneNumber item = new ContactPhoneNumber();

					LoadContactComplexType(tag, item);

					MarkupTagElement tagNumber = (tag.Elements["c:Number"] as MarkupTagElement);
					if (tagNumber != null)
					{
						item.Value = tagNumber.Value;
					}

					LoadLabelCollection(tag, item);

					contact.PhoneNumbers.Add(item);
				}
			}
			#endregion
			#region Relationships
			MarkupTagElement tagPersonCollection = (tagContact.Elements["c:PersonCollection"] as MarkupTagElement);
			if (tagPersonCollection != null)
			{
				foreach (MarkupElement el in tagPersonCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:Person") continue;

					ContactRelationship item = new ContactRelationship();

					LoadContactComplexType(tag, item);

					MarkupTagElement tagFormattedName = (tag.Elements["c:FormattedName"] as MarkupTagElement);
					if (tagFormattedName != null)
					{
						item.FormattedName = tagFormattedName.Value;
					}

					LoadLabelCollection(tag, item);

					contact.Relationships.Add(item);
				}
			}
			#endregion
			#region Photos
			MarkupTagElement tagPhotoCollection = (tagContact.Elements["c:PhotoCollection"] as MarkupTagElement);
			if (tagPhotoCollection != null)
			{
				foreach (MarkupElement el in tagPhotoCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:Photo") continue;

					ContactPhoto item = new ContactPhoto();

					LoadContactComplexType(tag, item);


					LoadLabelCollection(tag, item);

					contact.Photos.Add(item);
				}
			}
			#endregion
			#region Dates
			MarkupTagElement tagDateCollection = (tagContact.Elements["c:DateCollection"] as MarkupTagElement);
			if (tagDateCollection != null)
			{
				foreach (MarkupElement el in tagDateCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:Date") continue;

					ContactDate item = new ContactDate();

					LoadContactComplexType(tag, item);

					MarkupTagElement tagValue = (tag.Elements["c:Value"] as MarkupTagElement);
					if (tagValue != null) item.Value = DateTime.Parse(tagValue.Value);

					LoadLabelCollection(tag, item);

					contact.Dates.Add(item);
				}
			}
			#endregion
			#region Positions
			MarkupTagElement tagPositionCollection = (tagContact.Elements["c:PositionCollection"] as MarkupTagElement);
			if (tagPositionCollection != null)
			{
				foreach (MarkupElement el in tagPositionCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:Date") continue;

					ContactPosition item = new ContactPosition();

					LoadContactComplexType(tag, item);

					MarkupTagElement tagOffice = (tag.Elements["c:Office"] as MarkupTagElement);
					if (tagOffice != null) item.Office = new ContactGenericField<string>(tagOffice.Value);

					MarkupTagElement tagDepartment = (tag.Elements["c:Department"] as MarkupTagElement);
					if (tagDepartment != null) item.Department = new ContactGenericField<string>(tagDepartment.Value);

					MarkupTagElement tagJobTitle = (tag.Elements["c:JobTitle"] as MarkupTagElement);
					if (tagJobTitle != null) item.JobTitle = new ContactGenericField<string>(tagJobTitle.Value);

					MarkupTagElement tagCompany = (tag.Elements["c:Company"] as MarkupTagElement);
					if (tagCompany != null) item.JobTitle = new ContactGenericField<string>(tagCompany.Value);

					LoadLabelCollection(tag, item);

					contact.Positions.Add(item);
				}
			}
			#endregion
			#region URLs
			MarkupTagElement tagUrlCollection = (tagContact.Elements["c:UrlCollection"] as MarkupTagElement);
			if (tagUrlCollection != null)
			{
				foreach (MarkupElement el in tagUrlCollection.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "c:Url") continue;

					ContactUrl item = new ContactUrl();

					LoadContactComplexType(tag, item);

					MarkupTagElement tagValue = (tag.Elements["c:Value"] as MarkupTagElement);
					if (tagValue != null) item.Value = tagValue.Value;

					LoadLabelCollection(tag, item);

					contact.Urls.Add(item);
				}
			}
			#endregion
		}

		private void LoadContactComplexType(MarkupTagElement tag, IContactComplexType item)
		{
			MarkupAttribute attXsiNil = tag.Attributes["xsi:nil"];
			if (attXsiNil != null && attXsiNil.Value == "true") item.IsEmpty = true;

			MarkupAttribute attModificationDate = tag.Attributes["c:ModificationDate"];
			if (attModificationDate != null)
			{
				item.ModificationDate = DateTime.Parse(attModificationDate.Value);
			}
		}

		private void LoadLabelCollection(MarkupTagElement tag, IContactLabelContainer item)
		{
			if (tag != null)
			{
				foreach (MarkupElement el in tag.Elements)
				{
					MarkupTagElement tag1 = (el as MarkupTagElement);
					if (tag1 == null) continue;
					if (tag1.FullName != "c:Label") continue;

					ContactLabel label = new ContactLabel();

					MarkupAttribute attVersion = tag1.Attributes["c:Version"];
					if (attVersion != null)
					{

					}

					label.Value = tag1.Value;

					MarkupAttribute attModificationDate = tag1.Attributes["c:ModificationDate"];
					if (attModificationDate != null) label.ModificationDate = DateTime.Parse(attModificationDate.Value);

					item.Labels.Add(label);
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
		}
	}
}
