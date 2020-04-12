//
//  ContactName.cs - represents a name in a ContactObjectModel
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
	/// Represents a name in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactName : ICloneable, IContactComplexType
	{
		public class ContactNameCollection
			: System.Collections.ObjectModel.Collection<ContactName>
		{

		}

		#region IContactComplexType members
		public bool IsEmpty { get; set; } = false;
		public Guid ElementID { get; set; } = Guid.Empty;
		public DateTime? ModificationDate { get; set; } = null;
		#endregion

		public string Nickname { get; set; } = String.Empty;
		public string Title { get; set; } = String.Empty;
		public string FormattedName { get; set; } = String.Empty;
		public string FamilyName { get; set; } = String.Empty;
		public string MiddleName { get; set; } = String.Empty;
		public string GivenName { get; set; } = String.Empty;

		public object Clone()
		{
			ContactName clone = new ContactName();
			clone.IsEmpty = IsEmpty;
			clone.ModificationDate = ModificationDate;
			clone.ElementID = ElementID;
			clone.FamilyName = (FamilyName.Clone() as string);
			clone.FormattedName = (FormattedName.Clone() as string);
			clone.GivenName = (GivenName.Clone() as string);
			clone.MiddleName = (MiddleName.Clone() as string);
			clone.Nickname = (Nickname.Clone() as string);
			clone.Title = (Title.Clone() as string);
			return clone;
		}
	}
}
