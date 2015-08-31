using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactPhoto : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPhotoCollection
			: System.Collections.ObjectModel.Collection<ContactPhoto>
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

		private string mvarContentType = String.Empty;
		public string ContentType { get { return mvarContentType; } set { mvarContentType = value; } }

		private string mvarImageUrl = String.Empty;
		public string ImageUrl { get { return mvarImageUrl; } set { mvarImageUrl = value; } }

		public object Clone()
		{
			ContactPhoto clone = new ContactPhoto();
			clone.ElementID = mvarElementID;
			clone.IsEmpty = mvarIsEmpty;
			foreach (ContactLabel item in mvarLabels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.ModificationDate = mvarModificationDate;

			clone.ContentType = (mvarContentType.Clone() as string);
			clone.ImageUrl = (mvarImageUrl.Clone() as string);
			return clone;
		}
	}
}
