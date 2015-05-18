using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactRelationship : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactRelationshipCollection
			: System.Collections.ObjectModel.Collection<ContactRelationship>
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

		private string mvarFormattedName = String.Empty;
		public string FormattedName { get { return mvarFormattedName; } set { mvarFormattedName = value; } }

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		public object Clone()
		{
			ContactRelationship clone = new ContactRelationship();
			clone.ElementID = mvarElementID;
			clone.FormattedName = (mvarFormattedName.Clone() as string);
			clone.IsEmpty = mvarIsEmpty;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.ModificationDate = mvarModificationDate;
			return clone;
		}
	}
}
