/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/12/2013
 * Time: 11:57 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Nero
{
	/// <summary>
	/// Description of NeroDiskImageDataFormat.
	/// </summary>
	public partial class NeroDiskImageDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Nero Burning ROM disk image", new byte?[][] { new byte?[] { 0xE, (byte)'N', (byte)'e', (byte)'r', (byte)'o', (byte)'I', (byte)'S', (byte)'O', (byte)'0', (byte)'.', (byte)'0', (byte)'2', (byte)'.', (byte)'0', (byte)'3' } }, new string[] { "*.nrb", "*.nri", "*.nrg" });
				_dfr.ExportOptions.Add(new CustomOptionText("ImageName", "Image &name:"));
				_dfr.ExportOptions.Add(new CustomOptionText("ImageName2", "Image name (&Joliet):"));
			}
			return _dfr;
		}
		
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
			IO.Reader br = base.Accessor.Reader;
			
			string header = br.ReadLengthPrefixedString();
			if (header != "NeroISO0.02.03") throw new InvalidDataFormatException("File does not begin with 0xE, \"NeroISO0.02.03\"");
			
			int unknown1 = br.ReadInt32();
			int unknown2 = br.ReadInt32();
			short unknown3 = br.ReadInt16();
            mvarCreator = br.ReadLengthPrefixedString();
			
			byte[] unknown4 = br.ReadBytes(27);
			
			int unknowna1 = br.ReadInt32();
			int unknowna2 = br.ReadInt32();
			int unknowna3 = br.ReadInt32();
			int unknowna4 = br.ReadInt32();
			int unknowna5 = br.ReadInt32();
			int unknowna6 = br.ReadInt32();

            mvarImageName = br.ReadLengthPrefixedString();
            mvarImageName2 = br.ReadLengthPrefixedString();
			
			int unknown5 = br.ReadInt32();
			int unknown6 = br.ReadInt32();
			int unknown7 = br.ReadInt32();
			
			byte[] unknown8 = br.ReadBytes(51);
			System.Collections.Generic.List<Internal.DirectoryEntry> entries = new System.Collections.Generic.List<Internal.DirectoryEntry>();
			for (int i = 0; i < unknown5; i++)
			{
				Internal.DirectoryEntry entry = ReadDirectoryEntry(br);
				entries.Add(entry);
			}
		}
		
		private static Internal.DirectoryEntry ReadDirectoryEntry(IO.Reader br)
		{
			Internal.DirectoryEntry entry = new Internal.DirectoryEntry();
			entry.entries = new System.Collections.Generic.List<UniversalEditor.DataFormats.FileSystem.Nero.Internal.DirectoryEntry>();
			
			int nameLength = br.ReadInt32();
			entry.name = br.ReadFixedLengthString(nameLength);
			
			byte unknownb1 = br.ReadByte();
			short subDirectoryCount = br.ReadInt16();
			short unknownb3 = br.ReadInt16();
			
			for (short i = 0; i < subDirectoryCount; i++)
			{
				Internal.DirectoryEntry entry1 = ReadDirectoryEntry(br);
				entry.entries.Add(entry1);
			}
			return entry;
		}
		
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		
		private string mvarImageName = String.Empty;
		public string ImageName { get { return mvarImageName; } set { mvarImageName = value; } }
		
		private string mvarImageName2 = String.Empty;
		public string ImageName2 { get { return mvarImageName2; } set { mvarImageName2 = value; } }
		
		private string mvarCreator = String.Empty;
		public string Creator { get { return mvarCreator; } set { mvarCreator = value; } }
	}
}
