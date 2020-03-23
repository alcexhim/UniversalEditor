using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactUrl : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactUrlCollection
			: System.Collections.ObjectModel.Collection<ContactUrl>
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

		private string mvarValue = null;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		public object Clone()
		{
			ContactUrl clone = new ContactUrl();
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.ModificationDate = mvarModificationDate;
			clone.Value = (mvarValue.Clone() as string);
			return clone;
		}
	}
}
