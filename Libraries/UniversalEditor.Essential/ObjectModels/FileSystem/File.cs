//
//  File.cs - represents a File in a FileSystemObjectModel
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.ObjectModels.FileSystem
{
	/// <summary>
	/// Represents a <see cref="File" /> in a <see cref="FileSystemObjectModel" />.
	/// </summary>
	public class File : IFileSystemObject
	{
		public class FileCollection
			: System.Collections.ObjectModel.Collection<File>
		{
			public File Add(string FileName)
			{
				File file = new File();
				file.Name = FileName;
				base.Add(file);
				return file;
			}
			public File Add(string FileName, byte[] FileData)
			{
				File file = new File();
				file.Name = FileName;
				file.SetData(FileData);
				base.Add(file);
				return file;
			}
			public File Add(string FileName, string SourceFileName)
			{
				File file = new File();
				file.Name = FileName;
				file.SetData(System.IO.File.ReadAllBytes(SourceFileName));
				base.Add(file);
				return file;
			}

#if ENABLE_FILESYSTEM_INHERENT_COMPRESSION
			public CompressedFile Add(string FileName, byte[] FileData, Compression.CompressionMethod CompressionMethod)
			{
				CompressedFile file = new CompressedFile();
				file.Name = FileName;
				file.SetDataAsByteArray(FileData);
				file.CompressionMethod = CompressionMethod;
				base.Add(file);
				return file;
			}
			public CompressedFile Add(string FileName, string SourceFileName, Compression.CompressionMethod CompressionMethod)
			{
				CompressedFile file = new CompressedFile();
				file.Name = FileName;
				file.SetDataAsByteArray(System.IO.File.ReadAllBytes(SourceFileName));
				file.CompressionMethod = CompressionMethod;
				base.Add(file);
				return file;
			}
#endif

			public File this[string Name, MBS.Framework.IO.CaseSensitiveHandling caseSensitiveHandling = MBS.Framework.IO.CaseSensitiveHandling.System]
			{
				get
				{
					bool caseSensitive = (caseSensitiveHandling == MBS.Framework.IO.CaseSensitiveHandling.CaseSensitive || (caseSensitiveHandling == MBS.Framework.IO.CaseSensitiveHandling.System && System.Environment.OSVersion.Platform == PlatformID.Unix));
					foreach (File file in this)
					{
						if ((caseSensitive && file.Name == Name) || (!caseSensitive && file.Name.ToUpper() == Name.ToUpper()))
						{
							return file;
						}
					}
					return null;
				}
			}

			public bool Contains(string Name)
			{
				return (this[Name] != null);
			}

			private IFileSystemContainer mvarParent = null;
			public FileCollection(IFileSystemContainer parent = null)
			{
				mvarParent = parent;
			}

			protected override void InsertItem(int index, File item)
			{
				base.InsertItem(index, item);
				item.Parent = mvarParent;
				item.FileSystem = mvarParent.FileSystem;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, File item)
			{
				item.Parent = mvarParent;
				base.SetItem(index, item);
			}
		}

		public FileAttributes Attributes { get; set; } = FileAttributes.None;
		/// <summary>
		/// The name of this file.
		/// </summary>
		public string Name { get; set; } = String.Empty;

		public void SetData(byte[] data)
		{
			Source = new MemoryFileSource(data);
		}

		public byte[] GetData()
		{
			if (Source != null) return Source.GetData();
			if (DataRequest != null)
			{
				DataRequestEventArgs e = new DataRequestEventArgs();
				DataRequest(this, e);
				return e.Data;
			}

			Console.WriteLine("DataRequest: " + Name + ": No source associated with this file");
			return new byte[0];
		}

		public byte[] GetData(long offset, long length)
		{
			if (Source != null) return Source.GetDataInternal(offset, length);

			Console.WriteLine("DataRequest: " + Name + ": No source associated with this file");
			return new byte[length];
		}

		private System.IO.Stream mvarStream = null;
		public System.IO.Stream GetDataAsStream()
		{
			if (mvarStream == null) throw new InvalidOperationException("Data is not represented as a stream");
			return mvarStream;
		}
		public void SetData(System.IO.Stream stream)
		{
			Source = new AccessorFileSource(new StreamAccessor(stream));
		}

		public void SetData(string value)
		{
			SetData(value, Encoding.Default);
		}
		public void SetData(string value, Encoding encoding)
		{
			SetData(encoding.GetBytes(value));
		}

		public object Clone()
		{
			File clone = new File();
			clone.Name = Name;
			clone.Source = Source;
			if (DataRequest != null)
			{
				clone.DataRequest += DataRequest;
			}
			clone.Source = Source;
			foreach (KeyValuePair<string, object> kvp in Properties)
			{
				clone.Properties.Add(kvp.Key, kvp.Value);
			}
			foreach (KeyValuePair<string, object> kvp in AdditionalDetails)
			{
				clone.AdditionalDetails.Add(kvp.Key, kvp.Value);
			}
			clone.Parent = Parent;
			return clone;
		}

		/// <summary>
		/// Gets an <see cref="ObjectModel" /> for this file using the specified <see cref="DataFormat" />.
		/// </summary>
		/// <typeparam name="T">The type of <see cref="ObjectModel" /> to return.</typeparam>
		/// <param name="df">The <see cref="DataFormat" /> specifying how to read the file data.</param>
		/// <returns>An <see cref="ObjectModel" /> with the file data, or null if the specified ObjectModel cannot be retrieved with the specified DataFormat from this file.</returns>
		public T GetObjectModel<T>(DataFormat df) where T : ObjectModel, new()
		{
			byte[] data = GetData();
			MemoryAccessor ma = new MemoryAccessor(data);

			T om = new T();
			Document.Load(om, df, ma);

			return om;
		}
		/// <summary>
		/// Sets the data for this file using the specified <see cref="DataFormat" /> and <see cref="ObjectModel" />.
		/// </summary>
		/// <typeparam name="T">The type of <see cref="ObjectModel" /> to save.</typeparam>
		/// <param name="dataFormat">The <see cref="DataFormat" /> specifying how to save the file data.</param>
		/// <param name="objectModel">The <see cref="ObjectModel" /> containing the file data to save.</param>
		public void SetObjectModel<T>(DataFormat dataFormat, T objectModel) where T : ObjectModel
		{
			MemoryAccessor ma = new MemoryAccessor();

			Document.Save(objectModel, dataFormat, ma);

			byte[] data = ma.ToArray();
			SetData(data);
		}

		public override string ToString()
		{
			string strSize = "*";
			try
			{
				if (mvarSize != null)
				{
					strSize = mvarSize.ToString();
				}
				else
				{
					byte[] data = this.GetData();
					if (data != null)
					{
						strSize = data.Length.ToString();
					}
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("ue: FileSystem: File: {0}", ex.Message);
				if (mvarSize != null)
				{
					strSize = Size.ToString() + "?";
				}
				else
				{
					strSize = "?";
				}
			}
			return Name + " [" + strSize + "]";
		}

		public void Save()
		{
			Save(Name);
		}
		public void Save(string FileName)
		{
			string FileDirectory = System.IO.Path.GetDirectoryName(FileName);
			if (!System.IO.Directory.Exists(FileDirectory))
			{
				System.IO.Directory.CreateDirectory(FileDirectory);
			}

			FileSource source = Source;
			long blockSize = (System.Environment.WorkingSet / 8);
			long blockCount = (blockSize / source.GetLength());
			long offset = 0;

			FileAccessor fa = new FileAccessor(FileName, true, true);
			fa.Open();
			for (long i = 0; i < blockCount; i++)
			{
				byte[] blockData = GetData(offset, blockSize);
				offset += blockSize;

				fa.Writer.WriteBytes(blockData);
			}
			fa.Writer.Flush();
			fa.Close();
		}

		private long? mvarSize = null;
		public long Size
		{
			get
			{
				if (mvarSize != null)
				{
					return mvarSize.Value;
				}
				else
				{
					if (Source != null)
					{
						return Source.GetLength();
					}
					return 0;
				}
			}
			set
			{
				mvarSize = value;
			}
		}

		public void ResetSize()
		{
			mvarSize = null;
		}

		public string Title { get; set; } = null;
		public string Description { get; set; } = null;

		public event DataRequestEventHandler DataRequest;

		public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();
		public DateTime ModificationTimestamp { get; set; } = DateTime.Now;

		/// <summary>
		/// Determines where this <see cref="File" /> gets its data from.
		/// </summary>
		public FileSource Source { get; set; } = null;

		[NonSerializedProperty]
		public string Path
		{
			get
			{
				List<string> list = new List<string>();
				IFileSystemContainer parent = Parent;
				list.Add(Name);
				while (parent != null)
				{
					list.Add(parent.Name);
					parent = parent.Parent;
				}
				list.Reverse();

				return String.Join("/", list.ToArray());
			}
		}

		[NonSerializedProperty]
		public IFileSystemContainer Parent { get; internal set; } = null;

		[NonSerializedProperty]
		public FileSystemObjectModel FileSystem { get; private set; } = null;

		// The amount of working set to allocate to each block.
		public const int BLOCK_FRACTION = 4;

		/// <summary>
		/// Writes the entire contents of this <see cref="File" />, in blocks, to a <see cref="Writer" />.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="transformations">The <see cref="FileSourceTransformation" />s that are applied to the output data.</param>
		/// <returns>The number of bytes written to the <see cref="Writer" />.</returns>
		public long WriteTo(Writer bw, FileSourceTransformation[] transformations = null)
		{
			long count = 0;
			long blockSize = (System.Environment.SystemPageSize / BLOCK_FRACTION);

			long offset = 0;
			double dbl = ((double)Source.GetLength() / (double)blockSize);
			long blockCount = (long)Math.Ceiling(dbl);

			if (transformations != null)
			{

			}

			for (long i = 0; i < blockCount; i++)
			{
				byte[] data = Source.GetDataInternal(offset, blockSize);
				offset += blockSize;

				bw.WriteBytes(data);
				count += data.Length;
			}
			bw.Flush();
			return count;
		}

		public T GetProperty<T>(string name, T defaultValue = default(T))
		{
			if (Properties.ContainsKey(name))
			{
				if (Properties[name] is T)
					return (T)Properties[name];
			}
			return defaultValue;
		}

		private Dictionary<string, object> AdditionalDetails = new Dictionary<string, object>();
		public void SetAdditionalDetail(string name, object value)
		{
			AdditionalDetails[name] = value;
		}
		public object GetAdditionalDetail(string name, object defaultValue = null)
		{
			if (AdditionalDetails.ContainsKey(name))
			{
				object value = AdditionalDetails[name];
				if (value != null) return value;
			}
			return defaultValue;
		}
	}
}
