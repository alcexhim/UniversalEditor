using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactName : ICloneable, IContactComplexType
	{
		public class ContactNameCollection
			: System.Collections.ObjectModel.Collection<ContactName>
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

		private string mvarNickname = String.Empty;
		public string Nickname { get { return mvarNickname; } set { mvarNickname = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarFormattedName = String.Empty;
		public string FormattedName { get { return mvarFormattedName; } set { mvarFormattedName = value; } }

		private string mvarFamilyName = String.Empty;
		public string FamilyName { get { return mvarFamilyName; } set { mvarFamilyName = value; } }

		private string mvarMiddleName = String.Empty;
		public string MiddleName { get { return mvarMiddleName; } set { mvarMiddleName = value; } }

		private string mvarGivenName = String.Empty;
		public string GivenName { get { return mvarGivenName; } set { mvarGivenName = value; } }

		public object Clone()
		{
			ContactName clone = new ContactName();
			clone.IsEmpty = mvarIsEmpty;
			clone.ModificationDate = mvarModificationDate;
			clone.ElementID = mvarElementID;
			clone.FamilyName = (mvarFamilyName.Clone() as string);
			clone.FormattedName = (mvarFormattedName.Clone() as string);
			clone.GivenName = (mvarGivenName.Clone() as string);
			clone.MiddleName = (mvarMiddleName.Clone() as string);
			clone.Nickname = (mvarNickname.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
