//
//  OutputWindowPanel.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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

namespace UniversalEditor.UserInterface.Panels
{
	public class OutputWindowPanel : Panel
	{
		public static readonly Guid ID = new Guid("{34e9b282-4803-4797-b1ed-18261cf29b96}");

		private TextBox txt;

		public OutputWindowPanel()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			txt = new TextBox();
			txt.Multiline = true;
			txt.Editable = false;
			this.Controls.Add(txt, new BoxLayout.Constraints(true, true));
		}
		}
	}
