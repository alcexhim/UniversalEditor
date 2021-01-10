//
//  ImportTableEntry.cs - represents an entry in the import table of an Unreal Engine package file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 20112020 Mike Becker's Software
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
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Represents an entry in the import table of an Unreal Engine package file.
	/// </summary>
	public class ImportTableEntry : ICloneable
	{
		public class ImportTableEntryCollection
			: System.Collections.ObjectModel.Collection<ImportTableEntry>
		{
		}

		public object Clone()
		{
			ImportTableEntry clone = new ImportTableEntry();
			return clone;
		}

		/// <summary>
		/// Package file in which the class of the object is defined
		/// </summary>
		public NameTableEntry PackageName { get; set; }
		/// <summary>
		/// Class of the object, i.e. "Texture", "Palette", "Package", etc.
		/// </summary>
		public NameTableEntry ClassName { get; set; }
		/// <summary>
		/// Reference where the object resides
		/// </summary>
		public ObjectReference Package { get; set; }
		/// <summary>
		/// The name of the object
		/// </summary>
		public NameTableEntry ObjectName { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (PackageName != null)
			{
				sb.Append(PackageName.ToString(false));
				sb.Append(".");
			}
			if (ObjectName == null)
			{
				sb.Append("(invalid name)");
			}
			else
			{
				sb.Append(ObjectName.ToString(false));
			}
			/*
            if (mvarClassName != null)
            {
                sb.Append(" (");
                sb.Append(mvarClassName.ToString(false));
                sb.Append(")");
            }
            */
			return sb.ToString();
		}
	}
}
