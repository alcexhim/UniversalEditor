using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Contact;

namespace UniversalEditor.ObjectModels.AddressBook
{
	public class AddressBookObjectModel : ObjectModel
	{
		public override void Clear()
		{
			Contacts.Clear ();
		}

		public override void CopyTo(ObjectModel where)
		{
			AddressBookObjectModel clone = (where as AddressBookObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException ();

			foreach (ContactObjectModel c in Contacts) {
				clone.Contacts.Add (c.Clone () as ContactObjectModel);
			}
		}

		public ContactObjectModel.ContactObjectModelCollection Contacts { get; } = new ContactObjectModel.ContactObjectModelCollection ();

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Address book";
				_omr.Description = "Stores names and contact information for people";
			}
			return _omr;
		}
	}
}
