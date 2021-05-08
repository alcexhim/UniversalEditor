//
//  ContactEmailAddress.cs - represents an e-mail address in a ContactObjectModel
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

using System;

namespace UniversalEditor.ObjectModels.Contact
{
	/// <summary>
	/// Represents an e-mail address in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactEmailAddress : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactEmailAddressCollection
			: System.Collections.ObjectModel.Collection<ContactEmailAddress>
		{

		}

		#region IContactComplexType members
		public bool IsEmpty { get; set; } = false;
		public Guid ElementID { get; set; } = Guid.Empty;
		public DateTime? ModificationDate { get; set; } = null;
		#endregion

		public string Type { get; set; } = String.Empty;
		public string Address { get; set; } = String.Empty;
		public ContactLabel.ContactLabelCollection Labels { get; } = new ContactLabel.ContactLabelCollection();

		public object Clone()
		{
			ContactEmailAddress clone = new ContactEmailAddress();
			clone.Address = (Address.Clone() as string);
			clone.ElementID = ElementID;
			clone.IsEmpty = IsEmpty;
			foreach (ContactLabel item in Labels)
			{
				clone.Labels.Add(item.Clone() as string);
			}
			clone.ModificationDate = ModificationDate;
			clone.Type = (Type.Clone() as string);
			return clone;
		}
	}
}
