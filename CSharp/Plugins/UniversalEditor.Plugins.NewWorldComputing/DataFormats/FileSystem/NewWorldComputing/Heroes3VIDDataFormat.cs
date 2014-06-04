using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing
{
	public class Heroes3VIDDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Heroes of Might and Magic VID archive", new string[] { "*.vid" });
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			uint fileCount = br.ReadUInt32();
			uint offset = 0;
			for (uint i = 0; i < fileCount; i++)
			{
				File file = new File();
				file.Name = br.ReadNullTerminatedString(40);

				uint length = br.ReadUInt32();

				offsets.Add(file, offset);
				lengths.Add(file, length);

				file.DataRequest += new DataRequestEventHandler(file_DataRequest);
				file.Size = length;
				fsom.Files.Add(file);

				offset += length;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();
			bw.WriteUInt32((uint)files.Length);
			foreach (File file in files)
			{
				bw.WriteNullTerminatedString(file.Name, 40);
				bw.WriteUInt32((uint)file.Size);
			}
			foreach (File file in files)
			{
				bw.WriteBytes(file.GetDataAsByteArray());
			}
		}

		#region Data Request
		private Dictionary<File, uint> offsets = new Dictionary<File, uint>();
		private Dictionary<File, uint> lengths = new Dictionary<File, uint>();
		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			string FileName = String.Empty;
			if (Accessor is FileAccessor)
			{
				FileName = (Accessor as FileAccessor).FileName;
			}
			
			IO.Reader br = new IO.Reader(new FileAccessor(FileName));
			File send = (sender as File);
			br.Accessor.Position = offsets[send];
			e.Data = br.ReadBytes(lengths[send]);
			br.Close();
		}
		#endregion
	}
}
