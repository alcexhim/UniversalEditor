using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactPhysicalAddress : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPhysicalAddressCollection
			: System.Collections.ObjectModel.Collection<ContactPhysicalAddress>
		{

		}

		#region IContactComplexType members
		private bool mvarIsEmpty = false;
		public bool IsEmpty { get { return mvarIsEmpty; } set { mvarIsEmpty = value; } }

		private Guid mvarElementID = Guid.Empty;
		public Guid ElementID { get { return mvarElementID; } set { mvarElementID = value; } }

		private DateTime? mvarModificationDate = null;
		public DateTime? ModificationDate { get { return mvarModificationDate; } set { mvarModificationDate = value; } }
		#endregion

		private ContactGenericField<string> mvarCountry = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Country { get { return mvarCountry; } set { mvarCountry = value; } }

		private ContactGenericField<string> mvarPostalCode = ContactGenericField<string>.Empty;
		public ContactGenericField<string> PostalCode { get { return mvarPostalCode; } set { mvarPostalCode = value; } }

		private ContactGenericField<string> mvarRegion = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Region { get { return mvarRegion; } set { mvarRegion = value; } }

		private ContactGenericField<string> mvarLocality = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Locality { get { return mvarLocality; } set { mvarLocality = value; } }

		private ContactGenericField<string> mvarStreetAddress = ContactGenericField<string>.Empty;
		public ContactGenericField<string> StreetAddress { get { return mvarStreetAddress; } set { mvarStreetAddress = value; } }

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		public object Clone()
		{
			ContactPhysicalAddress clone = new ContactPhysicalAddress();
			clone.Country = mvarCountry;
			clone.ElementID = mvarElementID;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.IsEmpty = mvarIsEmpty;
			clone.Locality = mvarLocality;
			clone.ModificationDate = mvarModificationDate;
			clone.PostalCode = mvarPostalCode;
			clone.Region = mvarRegion;
			clone.StreetAddress = mvarStreetAddress;
			return clone;
		}
	}
}
