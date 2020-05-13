//
//  IPropertyListContainer.cs - interface supporting objects that can contain Groups and/or Property instances
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.PropertyList
{
	/// <summary>
	/// Interface supporting objects that can contain <see cref="Group" />s and/or <see cref="Property" /> instances.
	/// </summary>
	public interface IPropertyListContainer
	{
		PropertyListItem.PropertyListItemCollection Items { get; }
	}
}
