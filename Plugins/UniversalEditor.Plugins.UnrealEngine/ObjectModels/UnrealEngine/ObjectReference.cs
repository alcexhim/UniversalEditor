//
//  ObjectReference.cs - represents a reference to an object in an Unreal Engine package file
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

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Represents a reference to an object in an Unreal Engine package file.
	/// </summary>
	public class ObjectReference
	{
		/// <summary>
		/// Gets the parent <see cref="UnrealPackageObjectModel" /> to which this <see cref="ObjectReference" /> belongs.
		/// </summary>
		/// <value>The parent.</value>
		public UnrealPackageObjectModel Parent { get; } = null;
		/// <summary>
		/// Gets or sets the index into the export or import table. If the index is less than zero, this is a pointer to an entry in the import table. If the
		/// index is greater than zero, this is a pointer to an entry in the export table. If the index is zero, the pointer is <see langword="null"/>.
		/// </summary>
		/// <value>The index into the export or import table.</value>
		public int IndexValue { get; set; } = 0;

		public static readonly ObjectReference Empty = new ObjectReference(0, null);

		public ObjectReference(int indexValue = 0, UnrealPackageObjectModel parent = null)
		{
			Parent = parent;
			IndexValue = indexValue;
		}

		public object Value
		{
			get
			{
				if (Parent != null)
				{
					if (IndexValue < 0)
					{
						// pointer to an entry of the ImportTable
						int index = -IndexValue - 1;
						if (index >= 0 && index < Parent.ImportTableEntries.Count)
						{
							return Parent.ImportTableEntries[index];
						}
					}
					else if (IndexValue > 0)
					{
						// pointer to an entry in the ExportTable
						int index = IndexValue - 1;
						if (index >= 0 && index < Parent.ExportTableEntries.Count)
						{
							return Parent.ExportTableEntries[index];
						}
					}
				}
				return null;
			}
		}

		public NameTableEntry Name
		{
			get
			{
				if (Value is ExportTableEntry)
				{
					return (Value as ExportTableEntry).Name;
				}
				else if (Value is ImportTableEntry)
				{
					return (Value as ImportTableEntry).ClassName;
				}
				return null;
			}
		}

		public override string ToString()
		{
			if (IndexValue == 0) return "(null)";
			if (Parent != null)
			{
				if (IndexValue < 0)
				{
					// pointer to an entry of the ImportTable
					int index = -IndexValue - 1;
					if (index >= 0 && index < Parent.ImportTableEntries.Count)
					{
						return Parent.ImportTableEntries[index].ToString();
					}
					return "(unknown: " + IndexValue.ToString() + ")";
				}
				else if (IndexValue > 0)
				{
					// pointer to an entry in the ExportTable
					int index = IndexValue - 1;
					if (index >= 0 && index < Parent.ExportTableEntries.Count)
					{
						return Parent.ExportTableEntries[index].ToString();
					}
					return "(unknown: " + IndexValue.ToString() + ")";
				}
			}
			return "(unknown: " + IndexValue.ToString() + ")";
		}
	}
}
