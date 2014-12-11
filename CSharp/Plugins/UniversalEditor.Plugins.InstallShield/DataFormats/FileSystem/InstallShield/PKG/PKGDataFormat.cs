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
				_dfr.Filters.Add("InstallShield installation package", new byte?[][] { new byte?[] { 0x4A, 0xA3 } }, new string[] { "*.pkg" });
			}
			return _dfr;
		}
		
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
			Reader br = base.Accessor.Reader;
			ushort signature = br.ReadUInt16();
			if (signature != 0xA34A) throw new InvalidDataFormatException("File does not begin with 0xA34A");
			
			ushort unknown1a = br.ReadUInt16();
			ushort unknown1b = br.ReadUInt16();
			uint unknown2 = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			uint unknown4 = br.ReadUInt32();
			ushort unknown5 = br.ReadUInt16();
			ushort unknown6 = br.ReadUInt16();
			ushort unknown7 = br.ReadUInt16();
			ushort fileCount = br.ReadUInt16();
			for (ushort i = 0; i < fileCount; i++)
			{
				string FileName = br.ReadLengthPrefixedString();
				byte unknown8 = br.ReadByte();
				uint unknown9 = br.ReadUInt32();
				ushort unknown10 = br.ReadUInt16();
				
				File file = new File();
				file.Name = FileName;
				file.Size = unknown10;
				file.DataRequest += new DataRequestEventHandler(file_DataRequest);
				fsom.Files.Add(file);
				
				if (file.Name == "OP.Z")
				{
					
				}
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
		}
		
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
