//
//  PictureEditor.Designer.cs - UWT designer initialization for PictureEditor
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

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;
namespace UniversalEditor.Editors.Multimedia.Picture
{
	partial class PictureEditor
	{
		private Controls.DrawingArea.DrawingAreaControl da = new Controls.DrawingArea.DrawingAreaControl();

		/// <summary>
		/// UWT designer initialization for <see cref="PictureEditor"/>.
		/// </summary>
		/// <remarks>
		/// UWT designer initialization in code is deprecated; continue improving the cross-platform Glade XML parser for <see cref="ContainerLayoutAttribute" />!
		/// </remarks>
		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);
			this.Controls.Add(da, new BoxLayout.Constraints(true, true));
		}
	}
}
