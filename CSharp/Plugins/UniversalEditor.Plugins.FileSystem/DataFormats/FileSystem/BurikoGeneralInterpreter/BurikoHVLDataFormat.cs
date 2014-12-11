using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.BurikoGeneralInterpreter
{
	public class BurikoHVLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Buriko General Interpreter HVL", new byte?[][] { new byte?[] { (byte)'B', (byte)'H', (byte)'V', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)0, (byte)0, (byte)0, (byte)0 } }, new string[] { "*.hvl" });
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Reader br = base.Accessor.Reader;
			string BHV_____0000 = br.ReadFixedLengthString(12);
			if (BHV_____0000 != "BHV_____\0\0\0\0")
			{
				throw new InvalidDataFormatException("File does not begin with BHV_____");
			}

			int FileCount = br.ReadInt32();
			for (int i = 0; i < FileCount; i++)
			{
				string FileName = br.ReadFixedLengthString(56);
				if (FileName.Contains("\0")) FileName = FileName.Substring(0, FileName.IndexOf('\0'));

				int u1 = br.ReadInt32();
				int u2 = br.ReadInt32();

				MemoryAccessor ma = new MemoryAccessor();
				IO.Writer bw = new IO.Writer(ma);
				bw.Close();

				fsom.Files.Add(FileName, ma.ToArray());
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
