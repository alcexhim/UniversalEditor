using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.SinisterGames.GUT
{
	public class GUTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(GameTitle), "Game &title: ", "Shadow Company: Left for Dead"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(GameCopyright), "Game &copyright: ", "Copyright 1998 by Sinister Games Inc."));
			}
			return _dfr;
		}

		private string mvarGameTitle = "Shadow Company: Left for Dead";
		public string GameTitle { get { return mvarGameTitle; } set { mvarGameTitle = value; } }

		private string mvarGameCopyright = "Copyright 1998 by Sinister Games Inc.";
		public string GameCopyright { get { return mvarGameCopyright; } set { mvarGameCopyright = value; } }

		private DateTime mvarCreationTimestamp = DateTime.Now;
		public DateTime CreationTimestamp { get { return mvarCreationTimestamp; } set { mvarCreationTimestamp = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadLine();
			if (signature != "*************************************************************") throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("** ")) throw new InvalidDataFormatException();

			mvarGameTitle = signature.Substring(3);

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("**")) throw new InvalidDataFormatException();

			signature = reader.ReadLine();
			if (!signature.StartsWith("** ")) throw new InvalidDataFormatException();
			mvarGameCopyright = signature.Substring(3);

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
			sb.AppendLine("** " + mvarGameTitle);
			sb.AppendLine("**");
			sb.AppendLine("** [" + archiveFileName + "] : GUT resource file");
			sb.AppendLine("** " + mvarGameCopyright);
			sb.AppendLine("**");
			sb.AppendLine("** [gut_tool.exe] Build date: 17:58:56, Aug 24 1999");
			sb.AppendLine("** [" + archiveFileName + "] Created: " + mvarCreationTimestamp.Hour.ToString().PadLeft(2, '0') + ":" + mvarCreationTimestamp.Minute.ToString().PadLeft(2, '0') + ":" + mvarCreationTimestamp.Second.ToString().PadLeft(2, '0') + ", " + mvarCreationTimestamp.Month.ToString() + "/" + mvarCreationTimestamp.Day.ToString().PadLeft(2, '0') + "/" + mvarCreationTimestamp.Year.ToString().PadLeft(4, '0'));
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
