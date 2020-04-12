//
//  ContactLabel.cs - represents a label in a ContactObjectModel
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
	/// Represents a label in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactLabel : ICloneable, IContactComplexType
	{
		public class ContactLabelCollection
			: System.Collections.ObjectModel.Collection<ContactLabel>
		{
			public ContactLabel Add(string value, DateTime? modificationDate = null)
			{
				ContactLabel item = new ContactLabel();
				item.Value = value;
				item.ModificationDate = modificationDate;
				Add(item);
				return item;
			}
		}

		#region IContactComplexType members
		public bool IsEmpty { get; set; } = false;
		public Guid ElementID { get; set; } = Guid.Empty;
		public DateTime? ModificationDate { get; set; } = null;
		#endregion

		public string Value { get; set; } = String.Empty;

		public object Clone()
		{
			ContactLabel clone = new ContactLabel();
			clone.ModificationDate = ModificationDate;
			clone.Value = (Value.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
