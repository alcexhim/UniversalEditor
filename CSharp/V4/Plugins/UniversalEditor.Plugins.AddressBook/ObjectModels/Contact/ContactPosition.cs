using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactPosition : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPositionCollection
			: System.Collections.ObjectModel.Collection<ContactPosition>
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

		private ContactGenericField<string> mvarOffice = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Office { get { return mvarOffice; } set { mvarOffice = value; } }

		private ContactGenericField<string> mvarDepartment = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Department { get { return mvarDepartment; } set { mvarDepartment = value; } }

		private ContactGenericField<string> mvarJobTitle = ContactGenericField<string>.Empty;
		public ContactGenericField<string> JobTitle { get { return mvarJobTitle; } set { mvarJobTitle = value; } }

		private ContactGenericField<string> mvarCompany = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Company { get { return mvarCompany; } set { mvarCompany = value; } }

		public object Clone()
		{
			ContactPosition clone = new ContactPosition();
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as string);
			}
			clone.ModificationDate = mvarModificationDate;
			clone.Office = mvarOffice;
			clone.Department = mvarDepartment;
			clone.JobTitle = mvarJobTitle;
			clone.Company = mvarCompany;
			return clone;
		}
	}
}
