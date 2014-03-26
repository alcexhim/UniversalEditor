using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortablePixelmap
{
	public class PPMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Portable AnyMap (NetPBM) image", new byte?[][] { new byte?[] { (byte)'P', (byte)'1' }, new byte?[] { (byte)'P', (byte)'2' }, new byte?[] { (byte)'P', (byte)'3' }, new byte?[] { (byte)'P', (byte)'4' }, new byte?[] { (byte)'P', (byte)'5' }, new byte?[] { (byte)'P', (byte)'6' }, new byte?[] { (byte)'P', (byte)'7' } }, new string[] { "*.ppm", "*.pgm", "*.pbm", "*.pam" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) return;

			IO.BinaryReader br = base.Stream.BinaryReader;
			string signature = br.ReadFixedLengthString(2);

			switch (signature)
			{
				case "P1":
				case "P2":
				case "P3":
				{
					IO.TextReader tr = base.Stream.TextReader;
					while (!tr.EndOfStream)
					{
						string line = tr.ReadLine();
						if (line.Contains("#")) line = line.Substring(0, line.IndexOf("#"));
						if (String.IsNullOrEmpty(line.Trim())) continue;


					}
					break;
				}
				case "P4":
				case "P5":
				case "P6":
				{
					IO.TextReader tr = base.Stream.TextReader;

					bool dimensionsRead = false;
					bool divisorRead = false;
					int divisor = 1; // maximum value of pixel

					while (!tr.EndOfStream)
					{
						string line = tr.ReadLine();
						if (line.Contains("#")) line = line.Substring(0, line.IndexOf("#"));
						if (String.IsNullOrEmpty(line.Trim())) continue;
						if (!dimensionsRead)
						{
							string[] dimensions = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
							if (dimensions.Length < 2) throw new InvalidDataFormatException("Could not read dimensions");

							pic.Width = Int32.Parse(dimensions[0]);
							pic.Height = Int32.Parse(dimensions[1]);
							dimensionsRead = true;
						}
						else if (!divisorRead)
						{
							if (signature == "P4") break;
							divisor = Int32.Parse(line);
							divisorRead = true;
							break;
						}
					}

					for (int x = 0; x < pic.Width; x++)
					{
						for (int y = 0; y < pic.Height; y++)
						{
							switch (signature)
							{
								case "P4":
								{
									// pixelmap
									byte r = (byte)tr.Read();
									byte g = (byte)tr.Read();
									byte b = (byte)tr.Read();

									r = (byte)(255 - r);
									g = (byte)(255 - g);
									b = (byte)(255 - b);

									Color color = Color.FromRGBA(r, g, b);
									pic.SetPixel(color, x, y);
									break;
								}
								case "P5":
								{
									// graymap
									byte level = (byte)tr.Read();
									level = (byte)((double)level / (256 - divisor));

									Color color = Color.FromRGBA(level, level, level);
									pic.SetPixel(color, x, y);
									break;
								}
								case "P6":
								{
									// colormap
									byte r = (byte)tr.Read();
									byte g = (byte)tr.Read();
									byte b = (byte)tr.Read();

									Color color = Color.FromRGBA(r, g, b);
									pic.SetPixel(color, x, y);
									break;
								}
							}
						}
					}
					break;
				}
				case "P7":
				{
					throw new NotImplementedException();
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
