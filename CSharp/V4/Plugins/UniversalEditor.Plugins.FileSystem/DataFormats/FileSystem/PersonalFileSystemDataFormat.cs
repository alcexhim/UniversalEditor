using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.IO;
using UniversalEditor.Compression;

namespace UniversalEditor.DataFormats.FileSystem
{
	public class PersonalFileSystemDataFormat : DataFormat
	{
		private CompressionMethod mvarCompressionMethod = CompressionMethod.None;
		private string mvarTitle = "";
		private Version mvarVersion = new Version(1, 0);

		public CompressionMethod CompressionMethod
		{
			get
			{
				return this.mvarCompressionMethod;
			}
			set
			{
				this.mvarCompressionMethod = value;
			}
		}
		public string Title
		{
			get
			{
				return this.mvarTitle;
			}
			set
			{
				this.mvarTitle = value;
			}
		}
		public Version Version
		{
			get
			{
				return this.mvarVersion;
			}
			set
			{
				this.mvarVersion = value;
			}
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = objectModel as FileSystemObjectModel;
			BinaryReader br = base.Stream.BinaryReader;
			string PFSX = br.ReadFixedLengthString(4);
		}
		private File ReadPFSv1File(BinaryReader br)
		{
			File f = new File();
			f.Name = br.ReadString();
			int len = br.ReadInt32();
			byte[] data = br.ReadBytes(len);
			f.SetDataAsByteArray(data);
			return f;
		}
		private Folder ReadPFSv1Folder(BinaryReader br)
		{
			Folder dir = new Folder();
			dir.Name = br.ReadString();
			int folderCount = br.ReadInt32();
			for (int i = 0; i < folderCount; i++)
			{
				Folder dir2 = this.ReadPFSv1Folder(br);
				dir.Folders.Add(dir2);
			}
			int fileCount = br.ReadInt32();
			for (int i = 0; i < fileCount; i++)
			{
				File f = this.ReadPFSv1File(br);
				dir.Files.Add(f);
			}
			return dir;
		}
		private void LoadFile(BinaryReader br, File.FileCollection collection)
		{
			string szFileName = br.ReadNullTerminatedString();
			int compressionMethod = br.ReadInt32();
			try
			{
				this.mvarCompressionMethod = (CompressionMethod)compressionMethod;
			}
			catch
			{
				this.mvarCompressionMethod = CompressionMethod.Unknown;
			}
			byte[] fileData;
			if (this.mvarCompressionMethod != CompressionMethod.None && this.mvarCompressionMethod != CompressionMethod.Unknown)
			{
				uint uncompressedLen = br.ReadUInt32();
				uint compressedLen = br.ReadUInt32();
				byte[] compressedData = br.ReadBytes(compressedLen);
				fileData = CompressionStream.Decompress(this.mvarCompressionMethod, compressedData, uncompressedLen);
			}
			else
			{
				uint fileLen = br.ReadUInt32();
				fileData = br.ReadBytes(fileLen);
			}
			collection.Add(szFileName, fileData);
		}
		private void LoadFolder(BinaryReader br, Folder.FolderCollection collection)
		{
			Folder dir = new Folder();
			string dirName = br.ReadNullTerminatedString();
			dir.Name = dirName;
			uint nFileCount = br.ReadUInt32();
			for (uint i = 0u; i < nFileCount; i += 1u)
			{
				this.LoadFile(br, dir.Files);
			}
			uint nFolderCount = br.ReadUInt32();
			for (uint i = 0u; i < nFolderCount; i += 1u)
			{
				this.LoadFolder(br, dir.Folders);
			}
			collection.Add(dir);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = objectModel as FileSystemObjectModel;
			BinaryWriter bw = base.Stream.BinaryWriter;
			bw.WriteFixedLengthString("PFSX");
			bw.Write(this.mvarVersion.Major);
			bw.Write(this.mvarVersion.Minor);
			bw.Write(this.mvarVersion.Build);
			bw.Write(this.mvarVersion.Revision);
			bw.WriteNullTerminatedString(this.mvarTitle);
			bw.Write(fsom.Files.Count);
			for (int i = 0; i < fsom.Files.Count; i++)
			{
				this.WriteFile(bw, fsom.Files[i]);
			}
			bw.Write(fsom.Folders.Count);
			for (int i = 0; i < fsom.Folders.Count; i++)
			{
				this.WriteFolder(bw, fsom.Folders[i]);
			}
			bw.Flush();
		}
		private void WriteFile(BinaryWriter bw, File file)
		{
			bw.WriteNullTerminatedString(file.Name);
			bw.Write((int)this.mvarCompressionMethod);
			byte[] fileData = file.GetDataAsByteArray();
			if (this.mvarCompressionMethod != CompressionMethod.None && this.mvarCompressionMethod != CompressionMethod.Unknown)
			{
				bw.Write(fileData.Length);
				byte[] compressedData = CompressionStream.Compress(this.mvarCompressionMethod, fileData);
				bw.Write(compressedData.Length);
				bw.Write(compressedData);
			}
			else
			{
				if (fileData != null)
				{
					bw.Write(fileData.Length);
					bw.Write(fileData);
				}
				else
				{
					bw.Write(0);
				}
			}
		}
		private void WriteFolder(BinaryWriter bw, Folder dir)
		{
			bw.WriteNullTerminatedString(dir.Name);
			bw.Write(dir.Files.Count);
			for (int i = 0; i < dir.Files.Count; i++)
			{
				this.WriteFile(bw, dir.Files[i]);
			}
			bw.Write(dir.Folders.Count);
			for (int i = 0; i < dir.Folders.Count; i++)
			{
				this.WriteFolder(bw, dir.Folders[i]);
			}
		}
	}
}
