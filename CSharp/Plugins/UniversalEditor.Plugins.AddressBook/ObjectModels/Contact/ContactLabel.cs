using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactLabel : ICloneable, IContactComplexType
	{
		public class ContactLabelCollection
			: System.Collections.ObjectModel.Collection<ContactLabel>
		{
			public ContactLabel Add(string value, DateTime? modificationDate = null)
			{
				ContactLabel item = new ContactLabel();
				item.Value = value;
				item.ModificationDate = modificationDate;
				Add(item);
				return item;
			}
		}

		#region IContactComplexType members
		private bool mvarIsEmpty = false;
		public bool IsEmpty { get { return mvarIsEmpty; } set { mvarIsEmpty = value; } }

		private Guid mvarElementID = Guid.Empty;
		public Guid ElementID { get { return mvarElementID; } set { mvarElementID = value; } }

		private DateTime? mvarModificationDate = null;
		public DateTime? ModificationDate { get { return mvarModificationDate; } set { mvarModificationDate = value; } }
		#endregion

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ContactLabel clone = new ContactLabel();
			clone.ModificationDate = mvarModificationDate;
			clone.Value = (mvarValue.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return mvarValue;
		}
	}
}
