//
//  FNTDataFormat.cs
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
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Picture.ZSoft;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.Multimedia.Font.Bitmap;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.Plugins.Webfoot.DataFormats.Font.Bitmap
{
	public class FNTDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(BitmapFontObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public FNTDataFormat()
		{
			PropertyValuePrefix = String.Empty;
			PropertyValueSuffix = String.Empty;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			string basePath = null;
			if (Accessor is FileAccessor)
			{
				basePath = System.IO.Path.GetDirectoryName(((FileAccessor)Accessor).FileName);
			}

			PropertyListObjectModel plom = objectModels.Pop() as PropertyListObjectModel;
			BitmapFontObjectModel font = objectModels.Pop() as BitmapFontObjectModel;

			Group grpFontProperties = plom.Items["Font Properties"] as Group;
			if (grpFontProperties != null)
			{
				font.Title = (grpFontProperties.Items["Name"] as Property).Value?.ToString();

				double width =
					Double.Parse((grpFontProperties.Items["Width"] as Property).Value?.ToString()),
				height =
					Double.Parse((grpFontProperties.Items["Height"] as Property).Value?.ToString());

				font.Size = new MBS.Framework.Drawing.Dimension2D(width, height);

				int filesCount = Int32.Parse((grpFontProperties.Items["NbFiles"] as Property).Value?.ToString());
				string paletteFileName = (grpFontProperties.Items["Palette"] as Property).Value?.ToString();

				for (int i = 0; i < filesCount; i++)
				{
					BitmapFontPixmap pixmap = new BitmapFontPixmap();

					Group grpFile = plom.Items[String.Format("File{0}", (i + 1).ToString().PadLeft(3, '0'))] as Group;
					if (grpFile != null)
					{
						string fileName = (grpFile.Items["Name"] as Property).Value?.ToString();
						pixmap.RelativePath = fileName;

						if (basePath != null)
						{
							string fullyQualifiedFileName = System.IO.Path.Combine(basePath, fileName);
							if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
							{
								// try and fix Win32 assumptions on Linux
								if (!System.IO.File.Exists(fullyQualifiedFileName))
								{
									fileName = fileName.ToUpper();
									fullyQualifiedFileName = System.IO.Path.Combine(basePath, fileName);
								}
								if (!System.IO.File.Exists(fullyQualifiedFileName))
								{
									fileName = fileName.Replace("\\", "/");
									fullyQualifiedFileName = System.IO.Path.Combine(basePath, fileName);
								}
							}

							if (System.IO.File.Exists(fullyQualifiedFileName))
							{
								PictureObjectModel pic = new PictureObjectModel();
								FileAccessor fa = new FileAccessor(fullyQualifiedFileName);
								Association[] assocs = Association.FromCriteria(new AssociationCriteria() { Accessor = fa, ObjectModel = pic.MakeReference(), FileName = fa.FileName });

								Document.Load(pic, new PCXDataFormat(), fa);
								pixmap.Picture = pic;
							}
						}

						int type = Int32.Parse((grpFile.Items["Type"] as Property).Value?.ToString());

						int nbLines = Int32.Parse((grpFile.Items["NbLines"] as Property).Value?.ToString());

						for (int j = 0; j < nbLines; j++)
						{
							string lineXXX = (grpFile.Items[String.Format("Line{0}", (j + 1).ToString().PadLeft(3, '0'))] as Property).Value?.ToString();
							for (int k = 0; k < lineXXX.Length; k++)
							{
								BitmapFontGlyph glyph = new BitmapFontGlyph();
								glyph.X = k * font.Size.Width;
								glyph.Y = j * font.Size.Height;
								glyph.Pixmap = pixmap;
								glyph.Character = lineXXX[k];
								pixmap.Glyphs.Add(glyph);
							}
						}
						font.Pixmaps.Add(pixmap);
					}
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			BitmapFontObjectModel font = objectModels.Pop() as BitmapFontObjectModel;
			PropertyListObjectModel plom = new PropertyListObjectModel();

			Group grpFontProperties = new Group("Font Properties");
			grpFontProperties.Items.AddProperty("Name", font.Title);
			grpFontProperties.Items.AddProperty("Width", font.Size.Width);
			grpFontProperties.Items.AddProperty("Height", font.Size.Height);
			grpFontProperties.Items.AddProperty("NbFiles", font.Pixmaps.Count);
			grpFontProperties.Items.AddProperty("Palette", font.Pixmaps[0].RelativePath);
			plom.Items.Add(grpFontProperties);

			Group grpCharacterProperties = new Group("Character Properties");
			grpCharacterProperties.Items.AddProperty("MapSrc", "abcdefghijklmnopqrstuvwxyz\u0083\u0084\u0085\u0086\u00a0\u0082\u0088\u0089\u008a\u008b\u008c\u008d¡\u0093\u0094\u0095¢\u0081\u0096\u0097£\u0098\u0087¤\u008e\u008f\u0080\u0090¥\u0099¨­");
			grpCharacterProperties.Items.AddProperty("MapDst", "ABCDEFGHIJKLMNOPQRSTUVWXYZaaaaaeeeeiiiioooouuuuycnAACENO?!");
			plom.Items.Add(grpCharacterProperties);

			for (int i = 0; i < font.Pixmaps.Count; i++)
			{
				BitmapFontPixmap pixmap = font.Pixmaps[i];
				Group grpFile = new Group(String.Format("File{0}", (i + 1).ToString().PadLeft(3, '0')));
				grpFile.Items.AddProperty("Name", font.Pixmaps[i].RelativePath);
				grpFile.Items.AddProperty("Type", 1);

				if (font.Pixmaps[i].Picture == null)
				{
				}

				double picwidth = font.Pixmaps[i].Picture.Size.Width;
				int nbLines = (int)Math.Round((font.Pixmaps[i].Glyphs.Count * font.Size.Width) / picwidth);
				grpFile.Items.AddProperty("NbLines", font.Pixmaps[i].Glyphs.Count);

				double x = 0;
				int k = 0;
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				for (int j = 0; j < font.Pixmaps[i].Glyphs.Count; j++)
				{
					sb.Append(font.Pixmaps[i].Glyphs[j].Character);
					x += font.Size.Width;
					if (x >= picwidth)
					{
						grpFile.Items.AddProperty(String.Format("Line{0}", (k + 1).ToString().PadLeft(3, '0')), sb.ToString());
						x = 0;
						sb.Clear();
						k++;
					}
				}
				plom.Items.Add(grpFile);
			}

			objectModels.Push(plom);
		}
	}
}
