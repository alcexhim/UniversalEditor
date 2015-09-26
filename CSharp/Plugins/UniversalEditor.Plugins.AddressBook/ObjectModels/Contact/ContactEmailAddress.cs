using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactEmailAddress : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactEmailAddressCollection
			: System.Collections.ObjectModel.Collection<ContactEmailAddress>
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

		private string mvarType = String.Empty;
		public string Type { get { return mvarType; } set { mvarType = value; } }

		private string mvarAddress = String.Empty;
		public string Address { get { return mvarAddress; } set { mvarAddress = value; } }

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		public object Clone()
		{
			ContactEmailAddress clone = new ContactEmailAddress();
			clone.Address = (mvarAddress.Clone() as string);
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as string);
			}
			clone.ModificationDate = mvarModificationDate;
			clone.Type = (mvarType.Clone() as string);
			return clone;
		}
	}
}
