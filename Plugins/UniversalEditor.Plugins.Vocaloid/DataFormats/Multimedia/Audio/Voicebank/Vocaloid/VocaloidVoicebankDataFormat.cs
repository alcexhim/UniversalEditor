//
//  VocaloidVoicebankDataFormat.cs - provides a DataFormat for manipulating synthesized audio voicebank data in Vocaloid DDB format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.Accessors;

namespace UniversalEditor.Plugins.Vocaloid.DataFormats.Multimedia.Audio.Voicebank.Vocaloid
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio voicebank data in Vocaloid DDB format.
	/// </summary>
	public class VocaloidVoicebankDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(VoicebankObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;

			VoicebankObjectModel vom = (objectModel as VoicebankObjectModel);
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			byte[] signature = br.ReadBytes(4u);
			int vocaloidVersion = 1;
			if (signature[0] == 70 && signature[1] == 45 && signature[2] == 0 && signature[3] == 0)
			{
				vocaloidVersion = 1;
			}
			else
			{
				if (signature[0] == 70 && signature[1] == 82 && signature[2] == 77 && signature[3] == 50)
				{
					vocaloidVersion = 2;
				}
			}
			if (vom != null)
			{
				vom.CreatorVersion = new Version(vocaloidVersion, 0);
			}

			br.Seek(-4, SeekOrigin.Current); // so we can properly read the chunked format (FRM2)

			int i = 0;
			while (!br.EndOfStream)
			{
				string chunkName = null;
				uint chunkSize = 0;
				byte[] chunkData = ReadChunk(br, out chunkName, out chunkSize);

				Reader r = new Reader(new MemoryAccessor(chunkData));

				switch (chunkName)
				{
					case "FRM2":
					{
						uint unknown1 = r.ReadUInt32();
						uint unknown2 = r.ReadUInt32();
						uint unknown3 = r.ReadUInt32();
						uint unknown4 = r.ReadUInt32();

						uint dataOffset = r.ReadUInt32();       // 32
						uint dataLengthInChunks = r.ReadUInt32();      // 350
						uint actualDataLength = dataLengthInChunks * 4;

						// byte[] chunk1 = r.ReadBytes(actualDataLength);
						// byte[] chunk2 = r.ReadBytes(actualDataLength);
						// byte[] chunk3 = r.ReadBytes(actualDataLength);
						break;
					}
					case "SND ":
					{
						int freq = r.ReadInt32();
						short channels = r.ReadInt16();
						int dummy = r.ReadInt32();

						// TODO: test this
						byte[] data = r.ReadBytes(chunkSize - 18);

						if (fsom != null)
						{
							fsom.Files.Add(i.ToString() + ".raw", data);
						}
						else if (vom != null)
						{
							VoicebankSample vs = new VoicebankSample();
							vs.ChannelCount = channels;
							vs.Dummy = dummy;
							vs.Frequency = freq;
							vs.Data = data;
							vom.Samples.Add(vs);
						}
						Console.WriteLine("found sound file " + i.ToString() + ":");
						Console.WriteLine("    size: " + chunkSize.ToString());
						Console.WriteLine("    freq: " + freq.ToString());
						Console.WriteLine("    channels: " + channels.ToString());
						Console.WriteLine("    unknown_int32: " + dummy.ToString());
						i++;
						break;
					}
					default:
					{
						Console.WriteLine("vocaloid_ddb: skipping unknown chunk type '{0}'", chunkName);
						break;
					}
				}
			}
		}

		private byte[] ReadChunk(Reader br, out string chunkName, out uint chunkSize)
		{
			chunkName = br.ReadFixedLengthString(4);
			chunkSize = br.ReadUInt32();

			uint dataSize = chunkSize - 8;

			byte[] chunkData = br.ReadBytes(dataSize);
			return chunkData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
