//
//  SceneObject.cs - represents an object in a New World Computing (Heroes of Might and Magic II) dialog
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

namespace UniversalEditor.ObjectModels.NWCSceneLayout
{
	/// <summary>
	/// Represents an object in a New World Computing (Heroes of Might and Magic II) dialog.
	/// </summary>
	public abstract class SceneObject : IComparable<SceneObject>
	{
		public class SceneObjectCollection
			: System.Collections.ObjectModel.Collection<SceneObject>
		{
		}

		public ushort DisplayIndex { get; set; } = 0;

		public ushort Left { get; set; } = 0;
		public ushort Top { get; set; } = 0;
		public ushort Width { get; set; } = 0;
		public ushort Height { get; set; } = 0;

		public int CompareTo(SceneObject other)
		{
			return DisplayIndex.CompareTo(other.DisplayIndex);
		}
	}
}
