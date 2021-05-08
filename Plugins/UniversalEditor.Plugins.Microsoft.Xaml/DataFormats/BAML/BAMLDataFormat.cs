//
//  BAMLDataFormat.cs
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
using UniversalEditor;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Microsoft.Xaml.ObjectModels.XAML;

namespace UniversalEditor.Plugins.Microsoft.Xaml.DataFormats.BAML
{
	public class BAMLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(XAMLObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public string FeatureID { get; set; } = "MSBAML";

		public Version ReaderVersion { get; set; } = new Version(0, 96);
		public Version UpdaterVersion { get; set; } = new Version(0, 96);
		public Version WriterVersion { get; set; } = new Version(0, 96);

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = Accessor.Reader;

			uint signatureLength = reader.ReadUInt32();
			byte[] signatureData = reader.ReadBytes(signatureLength);
			string signature = System.Text.Encoding.Unicode.GetString(signatureData);
			FeatureID = signature;

			ReaderVersion = Read2xUInt16Version(reader);
			UpdaterVersion = Read2xUInt16Version(reader);
			WriterVersion = Read2xUInt16Version(reader);

			while (!reader.EndOfStream)
			{
				BAMLRecordType recordType = (BAMLRecordType)reader.ReadByte();
				switch (recordType)
				{
					case BAMLRecordType.DocumentStart:
					{
						byte zero1 = reader.ReadByte();
						int neg1 = reader.ReadInt32();
						byte zero2 = reader.ReadByte();
						break;
					}
					case BAMLRecordType.AssemblyInfo:
					{
						ushort unknown4a = reader.ReadUInt16();
						byte unknown5a = reader.ReadByte();
						string assemblyName = reader.ReadLengthPrefixedString();
						Console.WriteLine("Assembly name: {0}", assemblyName);
						break;
					}
					case BAMLRecordType.TypeInfo:
					{
						byte u = reader.ReadByte();
						uint unknown4 = reader.ReadUInt32();

						string typename = reader.ReadLengthPrefixedString();
						Console.WriteLine("typename is {0}", typename);
						break;
					}
					case BAMLRecordType.ElementStart:
					{
						byte u = reader.ReadByte();
						ushort u1 = reader.ReadUInt16();
						break;
					}
				}
			}
		}

		private Version Read2xUInt16Version(Reader reader)
		{
			ushort majorVer = reader.ReadUInt16();
			ushort minorVer = reader.ReadUInt16();
			return new Version(majorVer, minorVer);
		}
		private void Write2xUInt16Version(Writer writer, Version value)
		{
			writer.WriteUInt16((ushort)value.Major);
			writer.WriteUInt16((ushort)value.Minor);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer writer = Accessor.Writer;

			string signature = FeatureID;
			byte[] signatureData = System.Text.Encoding.Unicode.GetBytes(signature);
			writer.WriteUInt32((uint)signatureData.Length);

			Write2xUInt16Version(writer, ReaderVersion);
			Write2xUInt16Version(writer, UpdaterVersion);
			Write2xUInt16Version(writer, WriterVersion);
		}
	}
}
