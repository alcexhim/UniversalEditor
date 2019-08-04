using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
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

		private ContactNotes mvarNotes = new ContactNotes();
		public ContactNotes Notes { get { return mvarNotes; } }

		private ContactGender mvarGender = ContactGender.Unspecified;
		public ContactGender Gender { get { return mvarGender; } set { mvarGender = value; } }

		private DateTime mvarCreationDate = DateTime.Now;
		public DateTime CreationDate { get { return mvarCreationDate; } set { mvarCreationDate = value; } }

		private ContactIdentifier.ContactIdentifierCollection mvarIdentifiers = new ContactIdentifier.ContactIdentifierCollection();
		public ContactIdentifier.ContactIdentifierCollection Identifiers { get { return mvarIdentifiers; } }

		private ContactEmailAddress.ContactEmailAddressCollection mvarEmailAddresses = new ContactEmailAddress.ContactEmailAddressCollection();
		public ContactEmailAddress.ContactEmailAddressCollection EmailAddresses { get { return mvarEmailAddresses; } }

		private ContactName.ContactNameCollection mvarNames = new ContactName.ContactNameCollection();
		public ContactName.ContactNameCollection Names { get { return mvarNames; } }

		private ContactPhysicalAddress.ContactPhysicalAddressCollection mvarPhysicalAddresses = new ContactPhysicalAddress.ContactPhysicalAddressCollection();
		public ContactPhysicalAddress.ContactPhysicalAddressCollection PhysicalAddresses { get { return mvarPhysicalAddresses; } }

		private ContactPhoneNumber.ContactPhoneNumberCollection mvarPhoneNumbers = new ContactPhoneNumber.ContactPhoneNumberCollection();
		public ContactPhoneNumber.ContactPhoneNumberCollection PhoneNumbers { get { return mvarPhoneNumbers; } }

		private ContactRelationship.ContactRelationshipCollection mvarRelationships = new ContactRelationship.ContactRelationshipCollection();
		public ContactRelationship.ContactRelationshipCollection Relationships { get { return mvarRelationships; } }

		private ContactPhoto.ContactPhotoCollection mvarPhotos = new ContactPhoto.ContactPhotoCollection();
		public ContactPhoto.ContactPhotoCollection Photos { get { return mvarPhotos; } }

		private ContactDate.ContactDateCollection mvarDates = new ContactDate.ContactDateCollection();
		public ContactDate.ContactDateCollection Dates { get { return mvarDates; } }

		private ContactPosition.ContactPositionCollection mvarPositions = new ContactPosition.ContactPositionCollection();
		public ContactPosition.ContactPositionCollection Positions { get { return mvarPositions; } }

		private ContactUrl.ContactUrlCollection mvarUrls = new ContactUrl.ContactUrlCollection();
		public ContactUrl.ContactUrlCollection Urls { get { return mvarUrls; } }

		public override void Clear()
		{
			mvarNotes = new ContactNotes();
			mvarGender = ContactGender.Unspecified;
			mvarCreationDate = DateTime.Now;
			mvarIdentifiers.Clear();
			mvarEmailAddresses.Clear();
			mvarNames.Clear();
			mvarPhysicalAddresses.Clear();
			mvarPhoneNumbers.Clear();
			mvarRelationships.Clear();
			mvarPhotos.Clear();
			mvarDates.Clear();
			mvarPositions.Clear();
			mvarUrls.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ContactObjectModel clone = (where as ContactObjectModel);
			clone.Notes.Content = (mvarNotes.Content.Clone() as string);
			clone.Notes.ModificationDate = mvarNotes.ModificationDate;
			clone.Gender = mvarGender;
			clone.CreationDate = mvarCreationDate;
			foreach (ContactIdentifier item in mvarIdentifiers)
			{
				clone.Identifiers.Add(item.Clone() as ContactIdentifier);
			}
			foreach (ContactEmailAddress item in mvarEmailAddresses)
			{
				clone.EmailAddresses.Add(item.Clone() as ContactEmailAddress);
			}
			foreach (ContactName item in mvarNames)
			{
				clone.Names.Add(item.Clone() as ContactName);
			}
			foreach (ContactPhysicalAddress item in mvarPhysicalAddresses)
			{
				clone.PhysicalAddresses.Add(item.Clone() as ContactPhysicalAddress);
			}
			foreach (ContactPhoneNumber item in mvarPhoneNumbers)
			{
				clone.PhoneNumbers.Add(item.Clone() as ContactPhoneNumber);
			}
			foreach (ContactRelationship item in mvarRelationships)
			{
				clone.Relationships.Add(item.Clone() as ContactRelationship);
			}
			foreach (ContactPhoto item in mvarPhotos)
			{
				clone.Photos.Add(item.Clone() as ContactPhoto);
			}
			foreach (ContactDate item in mvarDates)
			{
				clone.Dates.Add(item.Clone() as ContactDate);
			}
			foreach (ContactPosition item in mvarPositions)
			{
				clone.Positions.Add(item.Clone() as ContactPosition);
			}
			foreach (ContactUrl item in mvarUrls)
			{
				clone.Urls.Add(item.Clone() as ContactUrl);
			}
		}
	}
}
