//
//  EditorContext.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Represents a <see cref="MBS.Framework.UserInterface.Context" /> associated with an <see cref="Editor" />.
	/// </summary>
	public class EditorContext : UIContext
	{
		public EditorReference Reference { get; private set; } = null;
		public PanelReference.PanelReferenceCollection Panels { get; } = new PanelReference.PanelReferenceCollection();

		public override string ToString()
		{
			return String.Format("[Editor] {0}", Name);
		}

		public EditorContext(Guid id, string name, EditorReference reference)
			: base(id, name)
		{
			Reference = reference;

			for (int i = 0; i < reference.Commands.Count; i++)
			{
				Commands.Add(reference.Commands[i]);
			}
			for (int i = 0; i < reference.CommandBars.Count; i++)
			{
				CommandBars.Add(reference.CommandBars[i]);
			}
			for (int i = 0; i < reference.MenuBar.Items.Count; i++)
			{
				MenuItems.Add(reference.MenuBar.Items[i]);
			}
			for (int i = 0; i < reference.Panels.Count; i++)
			{
				Panels.Add(reference.Panels[i]);
			}
		}
	}
}
