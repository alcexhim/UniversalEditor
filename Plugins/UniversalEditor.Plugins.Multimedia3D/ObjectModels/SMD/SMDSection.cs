//
//  SMDSection.cs - represents a section in a StudioMDL file
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

namespace UniversalEditor.ObjectModels.SMD
{
	/// <summary>
	/// Represents a section in a StudioMDL file.
	/// </summary>
	public class SMDSection : ICloneable
	{
		public class SMDSectionCollection
			: System.Collections.ObjectModel.Collection<SMDSection>
		{
		}

		public string Name { get; set; } = String.Empty;
		public System.Collections.Specialized.StringCollection Lines { get; } = new System.Collections.Specialized.StringCollection();

		public override string ToString()
		{
			return Name + " [" + Lines.Count.ToString() + " lines]";
		}
		public object Clone()
		{
			SMDSection clone = new SMDSection();
			clone.Name = (Name.Clone() as string);
			foreach (string line in Lines)
			{
				clone.Lines.Add(line.Clone() as string);
			}
			return clone;
		}
	}
}
