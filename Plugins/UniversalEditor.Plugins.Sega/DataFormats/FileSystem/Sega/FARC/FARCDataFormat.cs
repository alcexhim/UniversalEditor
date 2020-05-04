//
//  FARCDataFormat.cs - provides a DataFormat for manipulating archives in Sega FARC format
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
using System.Collections.Generic;
using System.Security.Cryptography;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.Sega.DataFormats.FileSystem.Sega.FARC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Sega FARC format.
	/// </summary>
	public class FARCDataFormat : DataFormat
	{
		// As of 2014-06-20:
		// Format       Status
		// ------------ -------------
		// FArc         Load, Save
		// FARC         Load
		// FArC         Load


		// this was mentioned on 
		// http://forum.xentax.com/viewtopic.php?f=10&t=9639
		// could this mean anything? -v-
		// PS : for each files same header (0x10 bytes - maybe key for decrypt??) - 6D4A249C8529DE62C8E3893931C9E0BC 
		// The files from Project Diva F are the same way, except they have a different header -- 69173ED8F50714439F6240AA7466C37A

		// EDAT v4
		// key
		// 6D4BF3D7245DB294B6C3F9E32AA57E79
		// kgen key
		// D1DF87B5C1471B360ACE21315A339C06

		// (it's not keys for FARC)

		// I guess it's like AES / XOR because all Sony FS use this.
		// EBOOT - AES
		// PSARC - AES
		// PGD - AES + XOR

		private DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);

				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(Encrypted), "_Encrypt the data with the specified key"));
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(Compressed), "_Compress the data with the gzip algorithm"));
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FARCDataFormat"/> is encrypted.
		/// </summary>
		/// <value><c>true</c> if encrypted; otherwise, <c>false</c>.</value>
		public bool Encrypted { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FARCDataFormat"/> is compressed.
		/// </summary>
		/// <value><c>true</c> if encrypted; otherwise, <c>false</c>.</value>
		public bool Compressed { get; set; } = false;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			Reader reader = base.Accessor.Reader;
			reader.Endianness = IO.Endianness.BigEndian;

			string FArC = reader.ReadFixedLengthString(4);
			int directorySize = reader.ReadInt32();
			uint flags = reader.ReadUInt32();

			// so there is no way to determine whether the file header is encrypted or not by looking at unencrypted flags...

			if (FArC == "FArC" || FArC == "FArc" || FArC == "FARC")
			{
				Compressed = (FArC == "FArC" || FArC == "FARC");
				Encrypted = (FArC == "FARC");

				if (Encrypted)
				{
					uint flag0 = reader.ReadUInt32();
					uint flag1 = reader.ReadUInt32();
					uint flag2 = reader.ReadUInt32();
					uint flag3 = reader.ReadUInt32();
					uint flag4 = reader.ReadUInt32();
				}
				
				while (reader.Accessor.Position < directorySize)
				{
					string FileName = reader.ReadNullTerminatedString();
					int offset = reader.ReadInt32();
					int compressedSize = reader.ReadInt32();
					int decompressedSize = compressedSize;
					if (Compressed)
					{
						decompressedSize = reader.ReadInt32();
					}

					if (Encrypted)
					{
						int flag = reader.ReadInt32();
					}

					File file = fsom.AddFile(FileName);
					if (decompressedSize == 0)
					{
						decompressedSize = compressedSize;
					}
					file.Size = decompressedSize;

					file.Properties.Add("reader", reader);
					file.Properties.Add("offset", offset);
					if (Compressed)
					{
						file.Properties.Add("compressedLength", compressedSize);
						file.Properties.Add("decompressedLength", compressedSize);
					}
					else
					{
						file.Properties.Add("length", compressedSize);
					}
					file.DataRequest += file_DataRequest;
				}
			}
			else
			{
				throw new InvalidDataFormatException("Unrecognized FARC signature: \"" + FArC + "\"");
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];

			int offset = (int)file.Properties["offset"];
			reader.Seek(offset, SeekOrigin.Begin);

			if (Compressed && Encrypted)
			{
				int compressedLength = (int)file.Properties["compressedLength"];
				int decompressedLength = (int)file.Properties["decompressedLength"];

				byte[] compressedData = reader.ReadBytes(compressedLength);
				e.Data = compressedData;

				// data encrypted? we have to decrypt it
				byte[][] keys =
				{
					// Virtua Fighter 5
					new byte[] { 0x6D, 0x4A, 0x24, 0x9C, 0x85, 0x29, 0xDE, 0x62, 0xC8, 0xE3, 0x89, 0x39, 0x31, 0xC9, 0xE0, 0xBC },
					// Project DIVA VF5
					new byte[] { 0x70, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x5F, 0x64, 0x69, 0x76, 0x61, 0x2E, 0x62, 0x69, 0x6E },

					// Other keys, don't know what they're for
					new byte[] { 0x69, 0x17, 0x3E, 0xD8, 0xF5, 0x07, 0x14, 0x43, 0x9F, 0x62, 0x40, 0xAA, 0x74, 0x66, 0xC3, 0x7A },
					new byte[] { 0x6D, 0x4B, 0xF3, 0xD7, 0x24, 0x5D, 0xB2, 0x94, 0xB6, 0xC3, 0xF9, 0xE3, 0x2A, 0xA5, 0x7E, 0x79 },
					new byte[] { 0xD1, 0xDF, 0x87, 0xB5, 0xC1, 0x47, 0x1B, 0x36, 0x0A, 0xCE, 0x21, 0x31, 0x5A, 0x33, 0x9C, 0x06 }
				};

				byte[] encryptedData = null;
				bool foundMatch = false;
				for (int k = 0; k < keys.Length; k++)
				{
					encryptedData = compressedData;
					try
					{
						encryptedData = Decrypt(compressedData, keys[k]);
						foundMatch = true;
						break;
					}
					catch (CryptographicException ex1)
					{
						continue;
					}
				}
				if (!foundMatch)
				{
					UserInterface.HostApplication.Messages.Add(UserInterface.HostApplicationMessageSeverity.Warning, "No valid encryption keys were available to process this file", file.Name);
					return;
				}

				// FIXME:   Project DIVA F key seems to work (without throwing an invalid padding error) but is not in a known compression
				//          method...
				e.Data = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(encryptedData);
			}
			else if (Compressed && !Encrypted)
			{
				int compressedLength = (int)file.Properties["compressedLength"];
				int decompressedLength = (int)file.Properties["decompressedLength"];

				byte[] compressedData = reader.ReadBytes(compressedLength);
				e.Data = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(compressedData);
			}
			else if (!Compressed && !Encrypted)
			{
				int length = (int)file.Properties["length"];

				e.Data = reader.ReadBytes(length);
			}
		}

		private byte[] Encrypt(byte[] data, byte[] key)
		{
			byte[] input = data;
			AesManaged aes = new AesManaged();
			aes.Key = key;

			ICryptoTransform xform = aes.CreateEncryptor();
			int blockCount = input.Length / xform.InputBlockSize;
			for (int i = 0; i < blockCount; i++)
			{
				if (i == blockCount - 1)
				{
					byte[] output2 = xform.TransformFinalBlock(input, i * xform.InputBlockSize, xform.InputBlockSize);
				}
				else
				{
					int l = xform.TransformBlock(input, i * xform.InputBlockSize, xform.InputBlockSize, input, i * xform.InputBlockSize);
				}
			}
			return input;
		}
		private byte[] Decrypt(byte[] data, byte[] key)
		{
			byte[] input = data;
			System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
			aes.Key = key;

			System.Security.Cryptography.ICryptoTransform xform = aes.CreateDecryptor();
			int blockCount = input.Length / xform.InputBlockSize;
			for (int i = 0; i < blockCount; i++)
			{
				if (i == blockCount - 1)
				{
					byte[] output2 = xform.TransformFinalBlock(input, i * xform.InputBlockSize, xform.InputBlockSize);
				}
				else
				{
					int l = xform.TransformBlock(input, i * xform.InputBlockSize, xform.InputBlockSize, input, i * xform.InputBlockSize);
				}
			}
			return input;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			Writer writer = base.Accessor.Writer;
			writer.Endianness = Endianness.BigEndian;

			writer.WriteFixedLengthString("FA");
			writer.WriteFixedLengthString(Encrypted ? "R" : "r");
			writer.WriteFixedLengthString(Compressed ? "C" : "c");

			if (Compressed && Encrypted)
			{
				Console.WriteLine("ue: Sega.FARC: Compressed AND encrypted FARC not supported yet");
			}
			else if (Compressed && !Encrypted)
			{
				int directorySize = 4;
				int dummy = 32;

				int offset = 8;

				foreach (File file in files)
				{
					directorySize += file.Name.Length + 1;
					directorySize += 12;
				}
				writer.WriteInt32(directorySize);
				writer.WriteInt32(dummy);

				offset += directorySize;

				Dictionary<File, byte[]> compressedDatas = new Dictionary<File, byte[]>();
				foreach (File file in files)
				{
					writer.WriteNullTerminatedString(file.Name);
					writer.WriteInt32(offset);

					byte[] decompressedData = file.GetData();
					byte[] compressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Compress(decompressedData);
					writer.WriteInt32(compressedData.Length);
					writer.WriteInt32(decompressedData.Length);

					compressedDatas.Add(file, compressedData);
				}
				foreach (File file in files)
				{
					writer.WriteBytes(compressedDatas[file]);
				}
			}
			else if (!Compressed && !Encrypted)
			{
				int directorySize = 4;
				int dummy = 32;
				foreach (File file in files)
				{
					directorySize += 8 + (file.Name.Length + 1);
				}
				writer.WriteInt32(directorySize);
				writer.WriteInt32(dummy);

				int offset = directorySize + 8;

				foreach (File file in files)
				{
					writer.WriteNullTerminatedString(file.Name);
					writer.WriteInt32(offset);
					writer.WriteInt32((int)file.Size);
					offset += (int)file.Size;
				}

				// writer.Align(96);

				foreach (File file in files)
				{
					writer.WriteBytes(file.GetData());
				}
			}
			else
			{
				throw new NotSupportedException("Unsupported parameters");
			}
			writer.Flush();
		}
	}
}
