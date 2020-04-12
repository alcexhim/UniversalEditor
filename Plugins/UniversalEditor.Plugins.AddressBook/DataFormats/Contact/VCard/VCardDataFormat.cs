//
//  VCardDataFormat.cs - provides a DataFormat for manipulating contact information in VCard (VCF) format
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

using UniversalEditor.DataFormats.CoreObject;
using UniversalEditor.ObjectModels.Contact;
using UniversalEditor.ObjectModels.CoreObject;

namespace UniversalEditor.DataFormats.Contact.VCard
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating contact information in VCard (VCF) format.
	/// </summary>
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
				_dfr.ContentTypes.Add("text/x-vcard");                      // deprecated
				_dfr.ContentTypes.Add("text/directory;profile=vCard");      // deprecated
				_dfr.ContentTypes.Add("text/directory");        // deprecated
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
					case "PHOTO":
					{
						ContactPhoto photo = new ContactPhoto();

						CoreObjectAttribute attMediaType = prop.Attributes["MEDIATYPE"];
						if (attMediaType != null && attMediaType.Values.Count > 0)
						{
							photo.ContentType = attMediaType.Values[0];
						}

						if (prop.Values.Count > 0)
						{
							photo.ImageUrl = prop.Values[0];
						}

						contact.Photos.Add(photo);
						break;
					}
					case "TEL":
					{
						ContactPhoneNumber phone = new ContactPhoneNumber();
						if (mvarFormatVersion.Major >= 4)
						{
							phone.Value = prop.Values[0].Substring(4);
						}
						else
						{
							phone.Value = prop.Values[0];
						}

						CoreObjectAttribute attType = prop.Attributes["TYPE"];
						if (attType != null)
						{
							foreach (string value in attType.Values)
							{
								switch (value.ToLower())
								{
									case "work":
									{
										break;
									}
									case "home":
									{
										break;
									}
									case "voice":
									{
										break;
									}
								}
								phone.Labels.Add(value);
							}
						}

						contact.PhoneNumbers.Add(phone);
						break;
					}
					case "ADR":
					{
						ContactPhysicalAddress addr = new ContactPhysicalAddress();

						CoreObjectAttribute attType = prop.Attributes["TYPE"];
						if (attType != null)
						{
							foreach (string value in attType.Values)
							{
								addr.Labels.Add(value);
							}
						}

						if (prop.Values.Count > 2)
						{
							addr.StreetAddress = new ContactGenericField<string>(prop.Values[2]);
							if (prop.Values.Count > 3)
							{
								addr.Locality = new ContactGenericField<string>(prop.Values[3]);
								if (prop.Values.Count > 4)
								{
									addr.Region = new ContactGenericField<string>(prop.Values[4]);
									if (prop.Values.Count > 5)
									{
										addr.PostalCode = new ContactGenericField<string>(prop.Values[5]);
										if (prop.Values.Count > 6)
										{
											addr.Country = new ContactGenericField<string>(prop.Values[6]);
										}
									}
								}
							}
						}

						contact.PhysicalAddresses.Add(addr);
						break;
					}
					case "EMAIL":
					{
						ContactEmailAddress email = new ContactEmailAddress();
						if (prop.Values.Count > 0)
						{
							email.Address = prop.Values[0];
						}

						CoreObjectAttribute attType = prop.Attributes["TYPE"];
						if (attType != null)
						{
							foreach (string value in attType.Values)
							{
								switch (value)
								{
									case "PREF":
									{
										email.Labels.Add("Preferred");
										break;
									}
									default:
									{
										email.Labels.Add(value);
										break;
									}
								}
							}
						}
						contact.EmailAddresses.Add(email);
						break;
					}
					case "REV":
					{
						if (prop.Values.Count > 0)
						{
							string datetime = prop.Values[0];
							try
							{
								contact.CreationDate = DateTime.Parse(datetime);
							}
							catch
							{
								if (!datetime.Contains("-"))
								{
									// hack
									string strYear = datetime.Substring(0, 4);
									string strMonth = datetime.Substring(4, 2);
									string strDay = datetime.Substring(6, 2);
									string strHour = datetime.Substring(9, 2);
									string strMinute = datetime.Substring(11, 2);
									string strSecond = datetime.Substring(13, 2);
									string strTimeZone = datetime.Substring(15);

									int year = Int32.Parse(strYear);
									int month = Int32.Parse(strMonth);
									int day = Int32.Parse(strDay);
									int hour = Int32.Parse(strHour);
									int minute = Int32.Parse(strMinute);
									int second = Int32.Parse(strSecond);

									contact.CreationDate = new DateTime(year, month, day, hour, minute, second);
								}
							}
						}
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
