using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactDate : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactDateCollection
			: System.Collections.ObjectModel.Collection<ContactDate>
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

		private DateTime? mvarValue = null;
		public DateTime? Value { get { return mvarValue; } set { mvarValue = value; } }

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		public object Clone()
		{
			ContactDate clone = new ContactDate();
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.ModificationDate = mvarModificationDate;
			clone.Value = mvarValue;
			return clone;
		}
	}
}
