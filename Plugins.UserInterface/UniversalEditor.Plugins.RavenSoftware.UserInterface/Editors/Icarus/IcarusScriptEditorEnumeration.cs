//
//  IcarusScriptEditorEnumeration.cs - describes an enumeration in an IcarusScriptEditor configuration
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

using UniversalEditor.ObjectModels.Icarus;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Icarus
{
	/// <summary>
	/// Describes an enumeration in an <see cref="IcarusScriptEditor" /> configuration.
	/// </summary>
	public class IcarusScriptEditorEnumeration
	{
		public class IcarusScriptEditorEnumerationCollection
			: System.Collections.ObjectModel.Collection<IcarusScriptEditorEnumeration>
		{
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public IcarusExpression Value { get; set; }
	}
}
