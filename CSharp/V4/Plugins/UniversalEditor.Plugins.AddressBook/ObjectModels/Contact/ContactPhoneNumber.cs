using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactPhoneNumber : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPhoneNumberCollection
			: System.Collections.ObjectModel.Collection<ContactPhoneNumber>
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

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ContactPhoneNumber clone = new ContactPhoneNumber();
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			clone.ModificationDate = mvarModificationDate;
			clone.Value = (mvarValue.Clone() as string);
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			return clone;
		}

		public override string ToString()
		{
			return mvarValue;
		}
	}
}
