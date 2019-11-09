//
//  PaletteEditor.Designer.cs
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette
{
	partial class PaletteEditor
	{
		private CustomControl cc = null;
		private Container cInfo = null;
		private TextBox txtColorName = null;

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			cc = new CustomControl();
			cc.MouseDown += cc_MouseDown;
			cc.MouseDoubleClick += cc_MouseDoubleClick;
			cc.Paint += cc_Paint;
			this.Controls.Add(cc, new BoxLayout.Constraints(true, true));

			cInfo = new Container();
			cInfo.Layout = new BoxLayout(Orientation.Vertical);
			this.Controls.Add(cInfo, new BoxLayout.Constraints(false, false));

			txtColorName = new TextBox();
			txtColorName.KeyDown += txtColorName_KeyDown;
			cInfo.Controls.Add(txtColorName, new BoxLayout.Constraints(false, true));
		}

	}
}
