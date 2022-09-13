//
//  LevelRenderer.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.Plugins.ChaosWorks.ObjectModels.ChaosWorksScene;
using UniversalEditor.Plugins.Multimedia.UserInterface;

namespace UniversalEditor.Plugins.ChaosWorks.UserInterface.Editors.ChaosWorksScene
{
	public class LevelRenderer : MBS.Framework.UserInterface.CustomControl
	{
		private ChaosWorksSceneLevel _Level = null;
		public ChaosWorksSceneLevel Level { get { return _Level; } set { _Level = value; InitPicx(); Refresh(); } }

		public ChaosWorksSceneEditor ParentEditor { get; private set; } = null;
		public LevelRenderer(ChaosWorksSceneEditor parentEditor)
		{
			ParentEditor = parentEditor;
		}

		private System.Collections.Generic.Dictionary<int, Image> _picx = new System.Collections.Generic.Dictionary<int, Image>();
		private void InitPicx()
		{
			_picx.Clear();

			double minScrollX = 0, minScrollY = 0, maxScrollX = 0, maxScrollY = 0;

			if (Level != null)
			{
				for (int i = 0; i < Level.Planes.Count; i++)
				{
					for (int j = 0; j < Level.Planes[i].Objects.Count; j++)
					{
						if (Level.Planes[i].Objects[j].Position.X < minScrollX)
						{
							minScrollX = Level.Planes[i].Objects[j].Position.X;
						}
						if (Level.Planes[i].Objects[j].Position.Y < minScrollY)
						{
							minScrollY = Level.Planes[i].Objects[j].Position.Y;
						}
						if (Level.Planes[i].Objects[j].Position.X > maxScrollX)
						{
							maxScrollX = Level.Planes[i].Objects[j].Position.X;
						}
						if (Level.Planes[i].Objects[j].Position.Y > maxScrollY)
						{
							maxScrollY = Level.Planes[i].Objects[j].Position.Y;
						}

						PictureCollectionObjectModel piccoll = ParentEditor.LoadFFSprite(Level.Planes[i].Objects[j].SpriteName);
						PictureObjectModel pic = piccoll.Pictures[Level.Planes[i].Objects[j].Phase];
						_picx[j] = pic.ToImage();
					}
				}

				ScrollBounds = new MBS.Framework.Drawing.Dimension2D(maxScrollX, maxScrollY);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (Level != null)
			{
				for (int i = 0; i < Level.Planes.Count; i++)
				{
					for (int j = 0; j < Level.Planes[i].Objects.Count; j++)
					{
						e.Graphics.DrawImage(_picx[j], Level.Planes[i].Position.X * (Level.Planes[i].Objects[j].Position.X - Level.CenterPosition.X - HorizontalAdjustment.Value), Level.Planes[i].Position.Y * (Level.Planes[i].Objects[j].Position.Y - Level.CenterPosition.Y - VerticalAdjustment.Value));
					}
				}
			}
		}

		protected override void OnScrolled(ScrolledEventArgs e)
		{
			base.OnScrolled(e);
			Invalidate();
		}
	}
}
