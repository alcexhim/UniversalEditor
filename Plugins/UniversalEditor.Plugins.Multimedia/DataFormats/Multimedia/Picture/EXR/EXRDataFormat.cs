//
//  EXRBaseDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.Drawing;
using MBS.Framework.Settings;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.EXR
{
	public class EXRDataFormat : DataFormat
	{
		public EXRDataFormat()
		{
		}

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting("LineOrder", "_Line order", EXRLineOrder.IncreasingY, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("IncreasingY", "Increasing", EXRLineOrder.IncreasingY),
					new ChoiceSetting.ChoiceSettingValue("DecreasingY", "Decreasing", EXRLineOrder.DecreasingY),
					new ChoiceSetting.ChoiceSettingValue("RandomY", "Random", EXRLineOrder.RandomY)
				}));
			}
			return _dfr;
		}

		public readonly byte[] EXR_SIGNATURE = new byte[] { 0x76, 0x2F, 0x31, 0x01 };

		public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();
		public EXRLineOrder LineOrder { get; set; } = EXRLineOrder.IncreasingY;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

			byte[] signature = Accessor.Reader.ReadBytes(4);
			if (!signature.Match(EXR_SIGNATURE))
				throw new InvalidDataFormatException("file does not begin with exr signature");

			byte version = Accessor.Reader.ReadByte();
			EXRFlags flags = (EXRFlags) Accessor.Reader.ReadUInt24();

			if ((flags & EXRFlags.Multipart) == EXRFlags.Multipart)
			{

			}

			// The header component of the single-part file holds a single header (for single-part files).
			// Each header is a sequence of attributes ended by a null byte.
			// The file has the same structure as a 1.7 file. That is, the multi-part bit(bit 12) must be 0, and the single null
			// byte that signals the end of the headers must be omitted. This structure also applies to single-part deep data
			// files.
			ReadProperties(Accessor);

			RequireProperty(new string[] { "dataWindow", "displayWindow", "lineOrder", "compression", "channels", "chromaticities" });

			LineOrder = (EXRLineOrder)Properties["lineOrder"];

			Vector2D screenWindowCenter = new Vector2D(0, 0);
			PreferProperty("screenWindowCenter", ref screenWindowCenter);

			int width = (int)((Rectangle)Properties["dataWindow"]).Width + 1;
			int height = (int)((Rectangle)Properties["dataWindow"]).Height + 1;
			pic.Width = width;
			pic.Height = height;

			int chunkCount = 0;
			if (Properties.ContainsKey("chunkCount"))
			{
				chunkCount = (int)Properties["chunkCount"];
			}
			else if (((flags & EXRFlags.Multipart) == EXRFlags.Multipart) || (flags & EXRFlags.NonImage) == EXRFlags.NonImage)
			{
				throw new InvalidDataFormatException("multipart or non-image EXR file does not contain a chunkCount attribute");
			}
			else
			{
				chunkCount = height / 16;
			}

			ulong[] chunkOffsets = new ulong[chunkCount];
			for (int i = 0; i < chunkCount; i++)
			{
				chunkOffsets[i] = Accessor.Reader.ReadUInt64();
			}

			int scanlinesPerBlock = 0;

			EXRCompression compressionMethod = (EXRCompression)Properties["compression"];
			switch (compressionMethod)
			{
				case EXRCompression.None:
				case EXRCompression.RunLengthEncoding:
				case EXRCompression.ZipPerScanline:
				{
					scanlinesPerBlock = 1;
					break;
				}
				case EXRCompression.ZipPer16Scanline:
				case EXRCompression.PXR24Deflate:
				{
					scanlinesPerBlock = 16;
					break;
				}
				case EXRCompression.PIZWavelet:
				case EXRCompression.B44:
				case EXRCompression.B44A:
				{
					scanlinesPerBlock = 32;
					break;
				}
			}

			EXRChannel[] channels = (EXRChannel[])Properties["channels"];
			EXRChromaticities chromaticities = (EXRChromaticities)Properties["chromaticities"];

			for (int i = 0; i < chunkCount; i++)
			{
				int scanlineOffsetY  = Accessor.Reader.ReadInt32();
				uint compressedLength = Accessor.Reader.ReadUInt32();

				byte[] compressedData = Accessor.Reader.ReadBytes(compressedLength);
				byte[] decompressedData = (new Compression.Modules.Zlib.ZlibCompressionModule()).Decompress(compressedData);

				MemoryAccessor ma = new MemoryAccessor(decompressedData);

				switch (LineOrder)
				{
					case EXRLineOrder.IncreasingY:
					{
						for (int y = 0; y < scanlinesPerBlock; y++)
						{
							ReadChannels(ma.Reader, pic, channels, scanlineOffsetY + y);
						}
						break;
					}
					case EXRLineOrder.DecreasingY:
					{
						for (int y = scanlinesPerBlock - 1; y >= 0; y--)
						{
							ReadChannels(ma.Reader, pic, channels, scanlineOffsetY + y);
						}
						break;
					}
				}

				System.IO.File.WriteAllBytes(String.Format("/tmp/zlib{0}.dat", scanlineOffsetY), decompressedData);
			}
		}

		/// <summary>
		/// Converts the attribute data in <paramref name="data" /> to a <see cref="Object" /> instance of the appropriate
		/// type as indicated by <paramref name="dataType" />.
		///
		/// This method can be overridden in derived classes to implement support for data types other than those defined in
		/// the official OpenEXR specifications. To ensure backward compatibility with official OpenEXR implementations, it
		/// is recommended that you return the value returned from base.ConvertAttributeData(...) if it is not null (i.e., if
		/// support for that data type is already implemented in the base class).
		/// </summary>
		/// <returns>The attribute data.</returns>
		/// <param name="data">Data.</param>
		/// <param name="dataType">Data type.</param>
		protected virtual object ConvertAttributeData(byte[] data, string dataType)
		{
			object propertyData = null;
			MemoryAccessor acc = new MemoryAccessor(data);
			switch (dataType)
			{
				case "chlist":
				{
					List<EXRChannel> chlist = new List<EXRChannel>();
					while (!acc.Reader.EndOfStream)
					{
						string name = acc.Reader.ReadNullTerminatedString();
						if (String.IsNullOrEmpty(name))
							break;

						EXRChannel channel = new EXRChannel();
						channel.Name = name;
						channel.PixelType = (EXRPixelType)acc.Reader.ReadInt32();
						channel.IsLinear = (acc.Reader.ReadUInt32() == 1);
						channel.XSampling = acc.Reader.ReadInt32();
						channel.YSampling = acc.Reader.ReadInt32();
						chlist.Add(channel);
					}
					propertyData = chlist.ToArray();
					break;
				}
				case "chromaticities":
				{
					EXRChromaticities chromaticities = new EXRChromaticities();
					chromaticities.RedX = acc.Reader.ReadSingle();
					chromaticities.RedY = acc.Reader.ReadSingle();
					chromaticities.GreenX = acc.Reader.ReadSingle();
					chromaticities.GreenY = acc.Reader.ReadSingle();
					chromaticities.BlueX = acc.Reader.ReadSingle();
					chromaticities.BlueY = acc.Reader.ReadSingle();
					chromaticities.WhiteX = acc.Reader.ReadSingle();
					chromaticities.WhiteY = acc.Reader.ReadSingle();
					propertyData = chromaticities;
					break;
				}
				case "compression":
				{
					if (data.Length == 1)
					{
						propertyData = (EXRCompression)acc.Reader.ReadByte();
					}
					else
					{
						throw new InvalidDataFormatException("property data size mismatch");
					}
					break;
				}
				case "lineOrder":
				{
					if (data.Length == 1)
					{
						propertyData = (EXRLineOrder)acc.Reader.ReadByte();
					}
					else
					{
						throw new InvalidDataFormatException("property data size mismatch");
					}
					break;
				}
				case "box2i":
				{
					if (data.Length == 16)
					{
						uint x = acc.Reader.ReadUInt32();
						uint y = acc.Reader.ReadUInt32();
						uint w = acc.Reader.ReadUInt32();
						uint h = acc.Reader.ReadUInt32();
						propertyData = new MBS.Framework.Drawing.Rectangle(x, y, w, h);
					}
					else
					{
						throw new InvalidDataFormatException("property data size mismatch");
					}
					break;
				}
				case "v2f":
				{
					if (data.Length == 8)
					{
						float x = acc.Reader.ReadSingle();
						float y = acc.Reader.ReadSingle();
						propertyData = new MBS.Framework.Drawing.Vector2D(x, y);
					}
					else
					{
						throw new InvalidDataFormatException("property data size mismatch");
					}
					break;
				}
				case "float":
				{
					if (data.Length == 4)
					{
						propertyData = acc.Reader.ReadSingle();
					}
					else
					{
						throw new InvalidDataFormatException("property data size mismatch");
					}
					break;
				}
			}
			return propertyData;
		}

		private void ReadChannels(Reader reader, PictureObjectModel pic, EXRChannel[] channels, int y)
		{
			for (int c = 0; c < channels.Length; c++)
			{
				for (int x = 0; x < pic.Width; x++)
				{
					Color color = pic.GetPixel(x, y);
					color.A = 1.0;
					switch (channels[c].Name)
					{
						case "R":
						{
							color.R = ReadPixelComponent(reader, channels[c].PixelType);
							break;
						}
						case "G":
						{
							color.G = ReadPixelComponent(reader, channels[c].PixelType);
							break;
						}
						case "B":
						{
							color.B = ReadPixelComponent(reader, channels[c].PixelType);
							break;
						}
						default:
						{
							Console.Error.WriteLine("channel '{0}' not handled", channels[c].Name);
							break;
						}
					}
					Console.WriteLine("setpixel ({0}, {1}) : {2}", x, y, color.ToString());
					pic.SetPixel(color, x, y);
				}
			}
		}

		/// <summary>
		/// Indicates that a property with the given
		/// <paramref name="propertyName" /> is preferred to be defined; however,
		/// if the property is not defined, use the value specified by
		/// <paramref name="propertyValue" />.
		/// </summary>
		/// <param name="propertyName">The name of the property to check.</param>
		/// <param name="propertyValue">The value to assign property to.</param>
		/// <typeparam name="T">The expected type of the property.</typeparam>
		private void PreferProperty<T>(string propertyName, ref T propertyValue)
		{
			if (Properties.ContainsKey(propertyName))
			{
				object value = Properties[propertyName];
				if (value is T)
				{
					propertyValue = (T)Properties[propertyName];
				}
				else
				{
					Console.Error.WriteLine(String.Format("exr: required attribute '{0}' not of type '{1}'; assuming {2}", propertyName, typeof(T).Name, propertyValue));
				}
			}
			else
			{
				Console.Error.WriteLine(String.Format("exr: required attribute '{0}' not found; assuming {1}", propertyName, propertyValue));
			}
		}

		private void RequireProperty(string propertyName)
		{
			RequireProperty(new string[] { propertyName });
		}
		private void RequireProperty(string[] propertyNames)
		{
			for (int i = 0; i < propertyNames.Length; i++)
			{
				if (!Properties.ContainsKey(propertyNames[i]))
				{
					throw new InvalidDataFormatException(String.Format("exr file does not contain required '{0}' attribute", propertyNames[i]));
				}
			}
		}

		private double ReadPixelComponent(Reader reader, EXRPixelType pixelType)
		{
			switch (pixelType)
			{
				case EXRPixelType.Float:
				{
					return (double)reader.ReadSingle();
				}
				case EXRPixelType.Half:
				{
					ushort value = reader.ReadUInt16();
					return (double)value;
				}
				case EXRPixelType.UInt:
				{
					return (double)reader.ReadUInt32();
				}
			}
			throw new NotImplementedException();
		}

		private void ReadProperties(Accessor acc)
		{
			while (!acc.Reader.EndOfStream)
			{
				string propertyName = acc.Reader.ReadNullTerminatedString();
				if (String.IsNullOrEmpty(propertyName))
					break;

				string propertyDataType = acc.Reader.ReadNullTerminatedString();
				uint propertyDataSize = acc.Reader.ReadUInt32();

				object propertyData = null;
				byte[] propertyDataBytes = acc.Reader.ReadBytes(propertyDataSize);
				propertyData = ConvertAttributeData(propertyDataBytes, propertyDataType);
				if (propertyData == null)
				{
					propertyData = propertyDataBytes;
				}
				Properties[propertyName] = propertyData;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
