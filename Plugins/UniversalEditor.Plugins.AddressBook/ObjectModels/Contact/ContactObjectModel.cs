//
//  ContactObjectModel.cs - provides an ObjectModel for manipulating contact information
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

namespace UniversalEditor.ObjectModels.Contact
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating contact information.
	/// </summary>
	public class ContactObjectModel : ObjectModel
	{
		public class ContactObjectModelCollection
			: System.Collections.ObjectModel.Collection<ContactObjectModel>
		{
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Contact";
				_omr.Path = new string[] { "General", "Contact" };
			}
			return _omr;
		}

		public ContactNotes Notes { get; private set; } = new ContactNotes();
		public ContactGender Gender { get; set; } = ContactGender.Unspecified;
		public DateTime CreationDate { get; set; } = DateTime.Now;
		public ContactIdentifier.ContactIdentifierCollection Identifiers { get; } = new ContactIdentifier.ContactIdentifierCollection();
		public ContactEmailAddress.ContactEmailAddressCollection EmailAddresses { get; } = new ContactEmailAddress.ContactEmailAddressCollection();
		public ContactName.ContactNameCollection Names { get; } = new ContactName.ContactNameCollection();
		public ContactPhysicalAddress.ContactPhysicalAddressCollection PhysicalAddresses { get; } = new ContactPhysicalAddress.ContactPhysicalAddressCollection();
		public ContactPhoneNumber.ContactPhoneNumberCollection PhoneNumbers { get; } = new ContactPhoneNumber.ContactPhoneNumberCollection();
		public ContactRelationship.ContactRelationshipCollection Relationships { get; } = new ContactRelationship.ContactRelationshipCollection();
		public ContactPhoto.ContactPhotoCollection Photos { get; } = new ContactPhoto.ContactPhotoCollection();
		public ContactDate.ContactDateCollection Dates { get; } = new ContactDate.ContactDateCollection();
		public ContactPosition.ContactPositionCollection Positions { get; } = new ContactPosition.ContactPositionCollection();
		public ContactUrl.ContactUrlCollection Urls { get; } = new ContactUrl.ContactUrlCollection();

		public override void Clear()
		{
			Notes = new ContactNotes();
			Gender = ContactGender.Unspecified;
			CreationDate = DateTime.Now;
			Identifiers.Clear();
			EmailAddresses.Clear();
			Names.Clear();
			PhysicalAddresses.Clear();
			PhoneNumbers.Clear();
			Relationships.Clear();
			Photos.Clear();
			Dates.Clear();
			Positions.Clear();
			Urls.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ContactObjectModel clone = (where as ContactObjectModel);
			clone.Notes.Content = (Notes.Content.Clone() as string);
			clone.Notes.ModificationDate = Notes.ModificationDate;
			clone.Gender = Gender;
			clone.CreationDate = CreationDate;
			foreach (ContactIdentifier item in Identifiers)
			{
				clone.Identifiers.Add(item.Clone() as ContactIdentifier);
			}
			foreach (ContactEmailAddress item in EmailAddresses)
			{
				clone.EmailAddresses.Add(item.Clone() as ContactEmailAddress);
			}
			foreach (ContactName item in Names)
			{
				clone.Names.Add(item.Clone() as ContactName);
			}
			foreach (ContactPhysicalAddress item in PhysicalAddresses)
			{
				clone.PhysicalAddresses.Add(item.Clone() as ContactPhysicalAddress);
			}
			foreach (ContactPhoneNumber item in PhoneNumbers)
			{
				clone.PhoneNumbers.Add(item.Clone() as ContactPhoneNumber);
			}
			foreach (ContactRelationship item in Relationships)
			{
				clone.Relationships.Add(item.Clone() as ContactRelationship);
			}
			foreach (ContactPhoto item in Photos)
			{
				clone.Photos.Add(item.Clone() as ContactPhoto);
			}
			foreach (ContactDate item in Dates)
			{
				clone.Dates.Add(item.Clone() as ContactDate);
			}
			foreach (ContactPosition item in Positions)
			{
				clone.Positions.Add(item.Clone() as ContactPosition);
			}
			foreach (ContactUrl item in Urls)
			{
				clone.Urls.Add(item.Clone() as ContactUrl);
			}
		}
	}
}
