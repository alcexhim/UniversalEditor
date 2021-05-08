//
//  IContactComplexType.cs - an interface for ContactObjectModel types that contain an element ID, modification date, and flag indicating whether the value is empty
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
	/// An interface for <see cref="ContactObjectModel" /> types that contain an element ID, modification date, and flag indicating whether the value is empty.
	/// </summary>
	public interface IContactComplexType
	{
		bool IsEmpty { get; set; }
		Guid ElementID { get; set; }
		DateTime? ModificationDate { get; set; }
	}
}
