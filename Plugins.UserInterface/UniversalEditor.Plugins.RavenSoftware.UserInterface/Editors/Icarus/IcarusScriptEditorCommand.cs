//
//  IcarusScriptEditorCommand.cs - describes the appearance of a predefined command for the IcarusScriptEditor
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

using System.Collections.Generic;
using UniversalEditor.ObjectModels.Icarus;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Icarus
{
	/// <summary>
	/// Describes the appearance of a predefined command for the <see cref="IcarusScriptEditor" />.
	/// </summary>
	public class IcarusScriptEditorCommand
	{
		public class IcarusScriptEditorCommandCollection
			: System.Collections.ObjectModel.Collection<IcarusScriptEditorCommand>
		{
			private Dictionary<string, IcarusScriptEditorCommand> _commandsByName = new Dictionary<string, IcarusScriptEditorCommand>();
			protected override void ClearItems()
			{
				base.ClearItems();
				_commandsByName.Clear();
			}
			protected override void InsertItem(int index, IcarusScriptEditorCommand item)
			{
				base.InsertItem(index, item);
				_commandsByName[item.Name] = item;
			}
			protected override void RemoveItem(int index)
			{
				_commandsByName.Remove(this[index].Name);
				base.RemoveItem(index);
			}

			public IcarusScriptEditorCommand this[string name]
			{
				get
				{
					if (_commandsByName.ContainsKey(name))
						return _commandsByName[name];
					return null;
				}
			}
		}

		public string Name { get; set; } = null;
		public string Description { get; set; } = null;
		public string IconName { get; set; } = null;

		public int TypeCode { get; set; } = 0;

		public IcarusParameter.IcarusParameterCollection Parameters { get; } = new IcarusParameter.IcarusParameterCollection();
		public IcarusScriptEditorCommandCollection Commands { get; } = new IcarusScriptEditorCommandCollection();
	}
}
