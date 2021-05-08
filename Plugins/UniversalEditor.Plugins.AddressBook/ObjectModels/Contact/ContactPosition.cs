//
//  ContactPosition.cs - represents a position in a ContactObjectModel
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
	/// Represents a position in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactPosition : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPositionCollection
			: System.Collections.ObjectModel.Collection<ContactPosition>
		{

		}

		#region IContactComplexType members
		public bool IsEmpty { get; set; } = false;
		public Guid ElementID { get; set; } = Guid.Empty;
		public DateTime? ModificationDate { get; set; } = null;
		#endregion

		public ContactLabel.ContactLabelCollection Labels { get; } = new ContactLabel.ContactLabelCollection();
		public ContactGenericField<string> Office { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Department { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> JobTitle { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Company { get; set; } = ContactGenericField<string>.Empty;

		public object Clone()
		{
			ContactPosition clone = new ContactPosition();
			clone.ElementID = ElementID;
			clone.IsEmpty = IsEmpty;
			foreach (ContactLabel item in Labels)
			{
				clone.Labels.Add(item.Clone() as string);
			}
			clone.ModificationDate = ModificationDate;
			clone.Office = Office;
			clone.Department = Department;
			clone.JobTitle = JobTitle;
			clone.Company = Company;
			return clone;
		}
	}
}
