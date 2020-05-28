//
//  BINDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Designer;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.ObjectModels.NWCSceneLayout;

using UniversalEditor.Plugins.Multimedia.UserInterface;

namespace UniversalEditor.DataFormats.NWCSceneLayout.NewWorldComputing.BIN
{
	public class BINDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(DesignerObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public string ICNDataDirectory { get; set; } = null;

		public static Library NWCSceneLayoutLibrary { get; private set; } = null;
		static BINDataFormat()
		{
			NWCSceneLayoutLibrary = new Library();
			NWCSceneLayoutLibrary.Components.Add(new Component(SceneObjectGuids.Button, "Button", new Property[]
			{
				new Property(ScenePropertyGuids.Button.BackgroundImageFileName, "Background image file name"),
				new Property(ScenePropertyGuids.Button.BackgroundImageIndex, "Background image index")
			}, Button_Render));
			NWCSceneLayoutLibrary.Components.Add(new Component(SceneObjectGuids.Image, "Image", new Property[]
			{
				new Property(ScenePropertyGuids.Image.BackgroundImageFileName, "Background image file name"),
				new Property(ScenePropertyGuids.Image.BackgroundImageIndex, "Background image index")
			}, Image_Render));
			NWCSceneLayoutLibrary.Components.Add(new Component(SceneObjectGuids.Label, "Label", new Property[]
			{
				new Property(ScenePropertyGuids.Label.Text, "Text"),
				new Property(ScenePropertyGuids.Label.FontFileName, "Font file name")
			}, Label_Render));
			NWCSceneLayoutLibrary.Components.Add(new Component(SceneObjectGuids.DropDownList, "Drop-down list", new Property[]
			{
				new Property(ScenePropertyGuids.DropDownList.BackgroundImageFileName, "Background image file name"),
				new Property(ScenePropertyGuids.DropDownList.BackgroundImageIndex, "Background image index")
			}, DropDownList_Render));
			NWCSceneLayoutLibrary.Components.Add(new Component(SceneObjectGuids.ListBox, "List box", new Property[]
			{
				new Property(ScenePropertyGuids.DropDownList.BackgroundImageFileName, "Background image file name"),
				new Property(ScenePropertyGuids.DropDownList.BackgroundImageIndex, "Background image index")
			}, ListBox_Render));
		}

		static void Button_Render(ComponentInstance instance, PaintEventArgs e, Rectangle bounds)
		{
			Property propBackgroundImage = NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage];
			if (propBackgroundImage != null)
			{
				PropertyValue propVal = instance.PropertyValues[propBackgroundImage];
				if (propVal != null)
				{
					PictureObjectModel pic = propVal.Value as PictureObjectModel;
					if (pic != null)
					{
						e.Graphics.DrawImage(pic.ToImage(), bounds.X, bounds.Y);
					}
				}
			}

		}
		static void ListBox_Render(ComponentInstance instance, PaintEventArgs e, Rectangle bounds)
		{
			Property propBackgroundImage = NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage];
			if (propBackgroundImage != null)
			{
				PropertyValue propVal = instance.PropertyValues[propBackgroundImage];
				if (propVal != null)
				{
					PictureObjectModel pic = propVal.Value as PictureObjectModel;
					if (pic != null)
					{
						e.Graphics.DrawImage(pic.ToImage(), bounds.X, bounds.Y);
					}
				}
			}

		}
		static void DropDownList_Render(ComponentInstance instance, PaintEventArgs e, Rectangle bounds)
		{
			Property propBackgroundImage = NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage];
			if (propBackgroundImage != null)
			{
				PropertyValue propVal = instance.PropertyValues[propBackgroundImage];
				if (propVal != null)
				{
					PictureObjectModel pic = propVal.Value as PictureObjectModel;
					if (pic != null)
					{
						e.Graphics.DrawImage(pic.ToImage(), bounds.X, bounds.Y);
					}
				}
			}

		}
		static void Image_Render(ComponentInstance instance, PaintEventArgs e, Rectangle bounds)
		{
			Property propBackgroundImage = NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage];
			if (propBackgroundImage != null)
			{
				PropertyValue propVal = instance.PropertyValues[propBackgroundImage];
				if (propVal != null)
				{
					PictureObjectModel pic = propVal.Value as PictureObjectModel;
					if (pic != null)
					{
						e.Graphics.DrawImage(pic.ToImage(), bounds.X, bounds.Y);
					}
				}
			}

		}
		static void Label_Render(ComponentInstance instance, PaintEventArgs e, Rectangle bounds)
		{
			Property propBackgroundImage = NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage];
			if (propBackgroundImage != null)
			{
				PropertyValue propVal = instance.PropertyValues[propBackgroundImage];
				if (propVal != null)
				{
					PictureObjectModel pic = propVal.Value as PictureObjectModel;
					if (pic != null)
					{
						e.Graphics.DrawImage(pic.ToImage(), bounds.X, bounds.Y);
					}
				}
			}
			e.Graphics.DrawText(instance.PropertyValues[instance.Component.Properties[ScenePropertyGuids.Label.Text]].Value?.ToString(), null, bounds, new MBS.Framework.UserInterface.Drawing.SolidBrush(SystemColors.WindowForeground));
		}

		static ICNDataFormat icndf = new ICNDataFormat();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			DesignerObjectModel designer = (objectModel as DesignerObjectModel);
			if (designer == null)
				throw new ObjectModelNotSupportedException();

			// HACK: because there simply is no better way to differentiate between window layout BINs and animation BINs (except maybe filename ending in FRM.BIN)
			if (Accessor.Length == 821 && Accessor.GetFileName().EndsWith("FRM.BIN"))
			{
				throw new InvalidDataFormatException(); // to kick it to the *other* BIN format
			}

			if (!designer.Libraries.Contains(NWCSceneLayoutLibrary))
				designer.Libraries.Add(NWCSceneLayoutLibrary);

			if (designer.Designs.Count == 0)
				designer.Designs.Add(new Design());

			Reader reader = Accessor.Reader;
			ushort canvasWidth = reader.ReadUInt16();
			ushort canvasHeight = reader.ReadUInt16();
			designer.Designs[0].Size = new MBS.Framework.Drawing.Dimension2D(canvasWidth, canvasHeight);

			ushort componentCount = reader.ReadUInt16();
			bool breakout = false;
			while (!reader.EndOfStream && !breakout)
			{
				BINComponentType componentType = (BINComponentType)reader.ReadUInt16();

				ushort x = reader.ReadUInt16();
				ushort y = reader.ReadUInt16();
				ushort width = reader.ReadUInt16();
				ushort height = reader.ReadUInt16();

				switch (componentType)
				{
					case BINComponentType.TextBox:
					case BINComponentType.TextBox2:
					{
						ushort textLength = reader.ReadUInt16();
						string text = reader.ReadFixedLengthString(textLength).TrimNull();

						string fntname = reader.ReadFixedLengthString(13).TrimNull();

						ushort u01 = reader.ReadUInt16();
						ushort u02 = reader.ReadUInt16();

						string icnFileName = reader.ReadFixedLengthString(13).TrimNull();

						ushort lx = reader.ReadUInt16();
						ushort ly = reader.ReadUInt16();
						ushort lwidth = reader.ReadUInt16();
						ushort lheight = reader.ReadUInt16();

						ushort u1 = reader.ReadUInt16();
						ushort u2 = reader.ReadUInt16();
						ushort u3 = reader.ReadUInt16();
						ushort u4 = reader.ReadUInt16();
						ushort u5 = reader.ReadUInt16();

						if (componentType == BINComponentType.TextBox2)
						{
							string w = reader.ReadFixedLengthString(u5).TrimNull();
							string fntFileName = reader.ReadFixedLengthString(13).TrimNull();

							ushort u1a = reader.ReadUInt16();
							ushort u2a = reader.ReadUInt16();
							ushort u3a = reader.ReadUInt16();
							ushort u4a = reader.ReadUInt16();
						}
						break;
					}
					case BINComponentType.ListBox:
					{
						string fntFileName = reader.ReadFixedLengthString(13).TrimNull();
						string icnFileName = reader.ReadFixedLengthString(13).TrimNull();

						ushort lx = reader.ReadUInt16();
						ushort ly = reader.ReadUInt16();
						ushort lwidth = reader.ReadUInt16();
						ushort lheight = reader.ReadUInt16();

						ushort u1 = reader.ReadUInt16();
						PictureObjectModel pic = LoadICN(icnFileName, 0);

						designer.Designs[0].ComponentInstances.Add(new ComponentInstance(NWCSceneLayoutLibrary.Components[SceneObjectGuids.ListBox], new Rectangle(x, y, width, height), new PropertyValue[]
						{
							new PropertyValue(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image].Properties[ScenePropertyGuids.Image.BackgroundImageFileName], icnFileName),
							new PropertyValue(NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage], pic)
						}));
						break;
					}
					case BINComponentType.DropDownList:
					{
						string fntFileName = reader.ReadFixedLengthString(13).TrimNull();
						string icnFileName = reader.ReadFixedLengthString(13).TrimNull();

						ushort lx = reader.ReadUInt16();
						ushort ly = reader.ReadUInt16();
						ushort lwidth = reader.ReadUInt16();
						ushort lheight = reader.ReadUInt16();

						ushort u1 = reader.ReadUInt16();
						ushort u2 = reader.ReadUInt16();
						ushort u3 = reader.ReadUInt16();
						ushort u4 = reader.ReadUInt16();
						ushort u5 = reader.ReadUInt16();
						ushort u6 = reader.ReadUInt16();

						PictureObjectModel pic = LoadICN(icnFileName, 0);

						designer.Designs[0].ComponentInstances.Add(new ComponentInstance(NWCSceneLayoutLibrary.Components[SceneObjectGuids.DropDownList], new Rectangle(x, y, width, height), new PropertyValue[]
						{
							new PropertyValue(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image].Properties[ScenePropertyGuids.Image.BackgroundImageFileName], icnFileName),
							new PropertyValue(NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage], pic)
						}));
						break;
					}
					case BINComponentType.Border:
					{
						ushort i1 = reader.ReadUInt16();                            // 65535

						BINComponentFlags flags = (BINComponentFlags)reader.ReadUInt16();                            // 2049
						if ((flags & BINComponentFlags.HasICN) == BINComponentFlags.HasICN)
						{
							string icnFileName = reader.ReadFixedLengthString(13).TrimNull();      // qwikhero.icn

							PictureObjectModel pic = LoadICN(icnFileName, 0);

							designer.Designs[0].ComponentInstances.Add(new ComponentInstance(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image], new Rectangle(x, y, width, height), new PropertyValue[]
							{
								new PropertyValue(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image].Properties[ScenePropertyGuids.Image.BackgroundImageFileName], icnFileName),
								new PropertyValue(NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage], pic)
							}));
						}
						else
						{
							ushort unk = reader.ReadUInt16();
						}
						break;
					}
					case BINComponentType.Label:
					{
						ushort textLength = reader.ReadUInt16();
						string text = reader.ReadFixedLengthString(textLength).TrimNull();
						string fontFileName = reader.ReadFixedLengthString(13).TrimNull();

						ushort b01 = reader.ReadUInt16();
						ushort b02 = reader.ReadUInt16();
						ushort b03 = reader.ReadUInt16();
						ushort b04 = reader.ReadUInt16();

						designer.Designs[0].ComponentInstances.Add(new ComponentInstance(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Label], new Rectangle(x, y, width, height), new PropertyValue[]
						{
							new PropertyValue(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Label].Properties[ScenePropertyGuids.Label.Text], text)
						}));
						break;
					}
					case BINComponentType.Image:
					{
						// this is the background and shadow images
						string icnFileName = reader.ReadFixedLengthString(13).TrimNull();
						ushort icnIndex = reader.ReadUInt16();	// 0		7
						ushort b02 = reader.ReadUInt16();	// 0		0
						ushort b03 = reader.ReadUInt16();   // 100		65535
						ushort b04 = reader.ReadUInt16();   // 16		16
						ushort b05 = reader.ReadUInt16();   // 0		0

						PictureObjectModel pic = LoadICN(icnFileName, icnIndex);

						designer.Designs[0].ComponentInstances.Add(new ComponentInstance(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image], new Rectangle(x, y, width, height), new PropertyValue[]
						{
							new PropertyValue(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image].Properties[ScenePropertyGuids.Image.BackgroundImageFileName], icnFileName),
							new PropertyValue(NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage], pic)
						}));
						break;
					}
					case BINComponentType.Button:
					{
						string icnFileName = reader.ReadFixedLengthString(13).TrimNull();
						ushort icnIndexStateNormal = reader.ReadUInt16();
						ushort icnIndexStatePressed = reader.ReadUInt16();
						ushort b03 = reader.ReadUInt16();
						ushort b04 = reader.ReadUInt16();
						ushort b05 = reader.ReadUInt16();
						ushort b06 = reader.ReadUInt16();

						PictureObjectModel pic = LoadICN(icnFileName, icnIndexStateNormal);

						designer.Designs[0].ComponentInstances.Add(new ComponentInstance(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Button], new Rectangle(x, y, width, height), new PropertyValue[]
						{
							new PropertyValue(NWCSceneLayoutLibrary.Components[SceneObjectGuids.Image].Properties[ScenePropertyGuids.Image.BackgroundImageFileName], icnFileName),
							new PropertyValue(NWCSceneLayoutLibrary.Components[DesignerObjectGuids.Common].Properties[DesignerPropertyGuids.Common.BackgroundImage], pic)
						}));
						break;
					}
					case BINComponentType.None:
					{
						breakout = true;
						break;
					}
					default:
					{
						break;
					}
				}
			}
		}

		private string icnDataDir = null;

		private PictureObjectModel LoadICN(string icnFileName, int icnIndex)
		{
			if (icnDataDir == null)
			{
				icnDataDir = ICNDataDirectory;
				if (icnDataDir == null)
				{
					string fn = Accessor.GetFileName();
					if (fn != null)
					{
						icnDataDir = System.IO.Path.GetDirectoryName(fn);
					}
				}
			}

			PictureObjectModel pic = null;
			string icnFullyQualifiedPath = MBS.Framework.IO.File.Find(System.IO.Path.Combine(new string[] { icnDataDir, icnFileName }), MBS.Framework.IO.CaseSensitiveHandling.CaseInsensitive);
			if (System.IO.File.Exists(icnFullyQualifiedPath))
			{
				PictureCollectionObjectModel pcom = new PictureCollectionObjectModel();
				try
				{
					Document.Load(pcom, icndf, new FileAccessor(icnFullyQualifiedPath));
					pic = pcom.Pictures[icnIndex];
				}
				catch
				{

				}
			}
			return pic;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
