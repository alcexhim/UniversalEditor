using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactIdentifier : ICloneable, IContactComplexType
	{
		public class ContactIdentifierCollection
			: System.Collections.ObjectModel.Collection<ContactIdentifier>
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

		private Guid mvarValue = Guid.Empty;
		public Guid Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ContactIdentifier clone = new ContactIdentifier();
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			clone.ModificationDate = mvarModificationDate;
			clone.Value = mvarValue;
			return clone;
		}
	}
}
