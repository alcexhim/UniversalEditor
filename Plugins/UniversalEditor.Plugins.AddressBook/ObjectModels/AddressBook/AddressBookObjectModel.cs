//
//  AddressBookObjectModel.cs - provides an ObjectModel for manipulating address book / contact group files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UniversalEditor.ObjectModels.Contact;

namespace UniversalEditor.ObjectModels.AddressBook
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating address book / contact group files.
	/// </summary>
	public class AddressBookObjectModel : ObjectModel
	{
		public override void Clear()
		{
			Contacts.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			AddressBookObjectModel clone = (where as AddressBookObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (ContactObjectModel c in Contacts)
			{
				clone.Contacts.Add(c.Clone() as ContactObjectModel);
			}
		}

		public ContactObjectModel.ContactObjectModelCollection Contacts { get; } = new ContactObjectModel.ContactObjectModelCollection();

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Address book" };
				_omr.Description = "Stores names and contact information for people";
			}
			return _omr;
		}
	}
}
