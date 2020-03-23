using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.AddressBook;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.AddressBook.Microsoft.ContactGroup
{
	public class ContactGroupDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal ()
		{
			if (_dfr == null) {
				_dfr = base.MakeReferenceInternal ();
				_dfr.Capabilities.Add (typeof (AddressBookObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal (objectModels);
			objectModels.Push (new MarkupObjectModel ());
		}
		protected override void AfterLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal (objectModels);
			MarkupObjectModel mom = (objectModels.Pop () as MarkupObjectModel);
			AddressBookObjectModel addr = (objectModels.Pop () as AddressBookObjectModel);

			

			objectModels.Push (addr);
		}
	}
}
