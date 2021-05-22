//
//  GUTDataFormat.cs - provides a DataFormat for manipulating archives in Sinister Games GUT format
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
using System.Text;
using MBS.Framework.Settings;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.SinisterGames.GUT
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Sinister Games GUT format.
	/// </summary>
	public class GUTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(GameTitle), "Game _title: ", "Shadow Company: Left for Dead"));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(GameCopyright), "Game _copyright: ", "Copyright 1998 by Sinister Games Inc."));
			}
			return _dfr;
		}

		public string GameTitle { get; set; } = "Shadow Company: Left for Dead";
		public string GameCopyright { get; set; } = "Copyright 1998 by Sinister Games Inc.";
		public DateTime CreationTimestamp { get; set; } = DateTime.Now;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadLine();
			if (signature != "*************************************************************") throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("** ")) throw new InvalidDataFormatException();

			GameTitle = signature.Substring(3);

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("** ")) throw new InvalidDataFormatException();
			GameCopyright = signature.Substring(3);

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (signature != "*************************************************************") throw new InvalidDataFormatException();

			ulong unknown1 = reader.ReadUInt64();
			uint unknown2 = reader.ReadUInt32();

			string archiveFileName = reader.ReadFixedLengthString(32).TrimNull();

			uint offsetOffset = (uint)reader.Accessor.Position;

			// for each file
			uint fileNameLength = reader.ReadUInt32();
			uint fileSize = reader.ReadUInt32();
			uint offset = reader.ReadUInt32() + offsetOffset; // [+ headerSize]
			uint unknown3 = reader.ReadUInt32();
			uint unknown4 = reader.ReadUInt32();
			string fileName = reader.ReadFixedLengthString(fileNameLength);

			// TODO: Figure out how to decrypt fileName, as well as how to calculate
			// how many files are in the archive
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			string archiveFileName = String.Empty;
			if (base.Accessor is FileAccessor)
			{
				archiveFileName = System.IO.Path.GetFileName((base.Accessor as FileAccessor).FileName);
			}
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("*************************************************************");
			sb.AppendLine("** " + GameTitle);
			sb.AppendLine("**");
			sb.AppendLine("** [" + archiveFileName + "] : GUT resource file");
			sb.AppendLine("** " + GameCopyright);
			sb.AppendLine("**");
			sb.AppendLine("** [gut_tool.exe] Build date: 17:58:56, Aug 24 1999");
			sb.AppendLine("** [" + archiveFileName + "] Created: " + CreationTimestamp.Hour.ToString().PadLeft(2, '0') + ":" + CreationTimestamp.Minute.ToString().PadLeft(2, '0') + ":" + CreationTimestamp.Second.ToString().PadLeft(2, '0') + ", " + CreationTimestamp.Month.ToString() + "/" + CreationTimestamp.Day.ToString().PadLeft(2, '0') + "/" + CreationTimestamp.Year.ToString().PadLeft(4, '0'));
			sb.AppendLine("*************************************************************");

			writer.WriteFixedLengthString(sb.ToString());

			writer.WriteUInt64(0);
			writer.WriteUInt32(0);
			writer.WriteFixedLengthString(archiveFileName, 32);

			uint offset = 0;

			File[] files = fsom.GetAllFiles();
			foreach (File file in files)
			{
				writer.WriteUInt32((uint)file.Name.Length);
				writer.WriteUInt32((uint)file.Size);
				writer.WriteUInt32(offset);
				writer.WriteUInt32(0);
				writer.WriteUInt32(0);

				string fileName = file.Name;
				// TODO: figure out encryption for filename
				writer.WriteFixedLengthString(fileName, (uint)fileName.Length);
			}
			foreach (File file in files)
			{
				writer.WriteBytes(file.GetData());
			}
		}
	}
}
