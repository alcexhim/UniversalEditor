//
//  BitmapFontEditor.cs
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
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.ObjectModels.Multimedia.Font.Bitmap;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Font.Bitmap
{
	[ContainerLayout("~/Editors/Multimedia/Font/Bitmap/BitmapFontEditor.glade")]
	public class BitmapFontEditor : Editor
	{
		private ListViewControl lv;
		private TextBox txtTitle;
		private NumericTextBox txtWidth;
		private NumericTextBox txtHeight;
		private CustomControl canvas;
		private TextBox txtRenderTest;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(BitmapFontObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			if (IsCreated)
			{
				lv.Model.Rows.Clear();

				base.OnObjectModelChanged(e);

				BitmapFontObjectModel font = (ObjectModel as BitmapFontObjectModel);
				//txtName.Text = font.Name;
				txtTitle.Text = font.Title;
				txtWidth.Value = font.Size.Width;
				txtHeight.Value = font.Size.Height;

				foreach (BitmapFontPixmap pixmap in font.Pixmaps)
				{
					foreach (BitmapFontGlyph glyph in pixmap.Glyphs)
					{
						MBS.Framework.UserInterface.Drawing.Image image = null;
						if (glyph.Picture != null)
						{
							image = glyph.Picture.ToImage();
						}
						else if (pixmap.Picture != null)
						{
							if (!((int)pixmap.Picture.Width == (int)font.Size.Width && (int)pixmap.Picture.Height == (int)font.Size.Height))
							{
								// pixmap size does not equal font size - pixmap contains multiple glyphs
								PictureObjectModel pic = new PictureObjectModel((int)font.Size.Width, (int)font.Size.Height);
								pixmap.Picture.CopyTo(pic, glyph.X, glyph.Y, font.Size.Width, font.Size.Height);

								image = pic.ToImage();
							}
							else
							{
								// pixmap size equal to font size - pixmap is one per glyph
								image = pixmap.Picture.ToImage();
							}
						}

						if (image != null)
						{
							_glyphs[glyph.Character] = image;
						}

						TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
						{
							new TreeModelRowColumn(lv.Model.Columns[0], glyph.Character.ToString()),
							new TreeModelRowColumn(lv.Model.Columns[1], image)
						});
						lv.Model.Rows.Add(row);
					}
				}
			}
		}
		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(e);
		}

		[EventHandler(nameof(txtRenderTest), nameof(TextBox.Changed))]
		private void txtRenderTest_Changed(object sender, EventArgs e)
		{
			canvas.Refresh();
		}

		private System.Collections.Generic.Dictionary<char, MBS.Framework.UserInterface.Drawing.Image> _glyphs = new System.Collections.Generic.Dictionary<char, MBS.Framework.UserInterface.Drawing.Image>();

		[EventHandler(nameof(canvas), nameof(Paint))]
		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(MBS.Framework.Drawing.Colors.Black);

			int x = 16, y = 16;
			for (int i = 0; i < txtRenderTest.Text.Length; i++)
			{
				BitmapFontObjectModel font = (ObjectModel as BitmapFontObjectModel);
				if (_glyphs.ContainsKey(txtRenderTest.Text[i]))
				{
					MBS.Framework.UserInterface.Drawing.Image image = _glyphs[txtRenderTest.Text[i]];
					e.Graphics.DrawImage(image, x, y);
				}
				x += (int)font.Size.Width;
			}
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}
	}
}
