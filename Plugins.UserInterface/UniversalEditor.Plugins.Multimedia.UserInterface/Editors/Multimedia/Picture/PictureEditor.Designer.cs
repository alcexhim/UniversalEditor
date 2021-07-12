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

using System;
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Multimedia.Picture;

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

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Context.AttachCommandEventHandler("ImageTransformRotateClockwise", ImageTransformRotateClockwise_Click);
			Context.AttachCommandEventHandler("ImageTransformRotateCounterclockwise", ImageTransformRotateCounterclockwise_Click);
			Context.AttachCommandEventHandler("ImageTransformRotate180", ImageTransformRotate180_Click);
			Context.AttachCommandEventHandler("ImageTransformRotateArbitrary", ImageTransformRotateArbitrary_Click);
		}

		private void ImageTransformRotateClockwise_Click(object sender, EventArgs e)
		{
			BeginEdit();
			(this.ObjectModel as PictureObjectModel).Rotate(90);
			this.da.Picture = (this.ObjectModel as PictureObjectModel);
			EndEdit();
		}
		private void ImageTransformRotateCounterclockwise_Click(object sender, EventArgs e)
		{
			BeginEdit();
			(this.ObjectModel as PictureObjectModel).Rotate(-90);
			this.da.Picture = (this.ObjectModel as PictureObjectModel);
			EndEdit();
		}
		private void ImageTransformRotate180_Click(object sender, EventArgs e)
		{
			BeginEdit();
			(this.ObjectModel as PictureObjectModel).Rotate(180);
			this.da.Picture = (this.ObjectModel as PictureObjectModel);
			EndEdit();
		}
		private void ImageTransformRotateArbitrary_Click(object sender, EventArgs e)
		{

		}
	}
}
