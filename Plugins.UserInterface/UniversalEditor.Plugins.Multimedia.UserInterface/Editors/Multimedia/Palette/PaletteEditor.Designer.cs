//
//  PaletteEditor.Designer.cs - UWT designer initialization for PaletteEditor
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
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

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette
{
	partial class PaletteEditor
	{
		private CustomControl cc = null;
		private Container cInfo = null;
		private TextBox txtColorName = null;

		/// <summary>
		/// UWT designer initialization for <see cref="PaletteEditor" />.
		/// </summary>
		/// <remarks>
		/// UWT designer initialization in code is deprecated; continue improving the cross-platform Glade XML parser for <see cref="ContainerLayoutAttribute" />!
		/// </remarks>
		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			cc = new CustomControl();
			cc.MouseDown += cc_MouseDown;
			cc.MouseDoubleClick += cc_MouseDoubleClick;
			cc.KeyDown += cc_KeyDown;
			cc.Paint += cc_Paint;
			this.Controls.Add(cc, new BoxLayout.Constraints(true, true));

			cInfo = new Container();
			cInfo.Layout = new BoxLayout(Orientation.Vertical);
			this.Controls.Add(cInfo, new BoxLayout.Constraints(false, false));

			Context.AttachCommandEventHandler("PaletteEditor_ContextMenu_Add", PaletteEditor_ContextMenu_Add_Click);
			Context.AttachCommandEventHandler("PaletteEditor_ContextMenu_Change", PaletteEditor_ContextMenu_Change_Click);
			Context.AttachCommandEventHandler("PaletteEditor_ContextMenu_Delete", PaletteEditor_ContextMenu_Delete_Click);

			txtColorName = new TextBox();
			txtColorName.KeyDown += txtColorName_KeyDown;
			cInfo.Controls.Add(txtColorName, new BoxLayout.Constraints(false, true));
		}

	}
}
