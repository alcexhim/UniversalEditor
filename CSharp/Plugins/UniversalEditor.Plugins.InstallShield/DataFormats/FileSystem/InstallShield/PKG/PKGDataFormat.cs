/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/12/2013
 * Time: 1:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.InstallShield.PKG
{
	/// <summary>
	/// Description of PKGDataFormat.
	/// </summary>
	public class PKGDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
			Reader br = Accessor.Reader;
			ushort signature = br.ReadUInt16();
			if (signature != 0xA34A) throw new InvalidDataFormatException("File does not begin with 0xA34A");
			
			ushort unknown1a = br.ReadUInt16();
			ushort unknown1b = br.ReadUInt16();
			uint unknown2 = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			/*
			uint unknown4 = br.ReadUInt32();
			ushort unknown5 = br.ReadUInt16();
			ushort unknown6 = br.ReadUInt16();
			ushort unknown7 = br.ReadUInt16();
			*/
			ushort fileCount = br.ReadUInt16();
			for (ushort i = 0; i < fileCount; i++)
			{
				ushort FileNameLength = br.ReadUInt16();
				string FileName = br.ReadFixedLengthString(FileNameLength);
				/*
				byte unknown8 = br.ReadByte();
				uint unknown9 = br.ReadUInt32();
				ushort unknown10 = br.ReadUInt16();
				*/
				ushort unknown8 = br.ReadUInt16();
				ushort maybeLength = br.ReadUInt16();
				ushort unknown9 = br.ReadUInt16();

				File file = fsom.AddFile(FileName);
				file.Size = maybeLength;
				file.DataRequest += new DataRequestEventHandler(file_DataRequest);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
		}
		
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			Writer bw = Accessor.Writer;
			bw.WriteUInt16(0xA34A);
			bw.WriteUInt32(10);
			bw.WriteUInt32(2);
			bw.WriteUInt32(65536);

			File[] files = fsom.GetAllFiles();
			bw.WriteUInt16((ushort)files.Length);
			for (ushort i = 0; i < (ushort)files.Length; i++)
			{
				bw.WriteUInt16((ushort)files[i].Name.Length);
				bw.WriteFixedLengthString(files[i].Name, (ushort)files[i].Name.Length);

				bw.WriteUInt16(2);
				bw.WriteUInt16((ushort)files[i].Size);
				bw.WriteUInt16(8);
			}
		}
	}
}
