using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.AddressBook
{
	public class AddressBookObjectModel : ObjectModel
	{
		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}
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
