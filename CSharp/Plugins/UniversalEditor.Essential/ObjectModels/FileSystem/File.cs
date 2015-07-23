using System;
using System.Collections.Generic;

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.ObjectModels.FileSystem
{
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

			public File this[string Name]
			{
				get
				{
					foreach (File file in this)
					{
						if (file.Name == Name)
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
		}

		private FileAttributes mvarAttributes = FileAttributes.None;
		public FileAttributes Attributes { get { return mvarAttributes; } set { mvarAttributes = value; } }

		private string mvarName = String.Empty;
		/// <summary>
		/// The name of this file.
		/// </summary>
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public void SetData(byte[] data)
		{
			mvarSource = new MemoryFileSource(data);
		}

		public byte[] GetData()
		{
			if (mvarSource != null) return mvarSource.GetData();
			if (DataRequest != null)
			{
				DataRequestEventArgs e = new DataRequestEventArgs();
				DataRequest(this, e);
				return e.Data;
			}

			Console.WriteLine("DataRequest: " + mvarName + ": No source associated with this file");
			return null;
		}
		public byte[] GetData(long offset, long length)
		{
			if (mvarSource != null) return mvarSource.GetData(offset, length);

			Console.WriteLine("DataRequest: " + mvarName + ": No source associated with this file");
			return null;
		}

		private System.IO.Stream mvarStream = null;
		public System.IO.Stream GetDataAsStream()
		{
			if (mvarStream == null) throw new InvalidOperationException("Data is not represented as a stream");
			return mvarStream;
		}
		public void SetData(System.IO.Stream stream)
		{
			mvarSource = new AccessorFileSource(new StreamAccessor(stream));
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
			clone.Name = mvarName;
			clone.Source = mvarSource;
			foreach (KeyValuePair<string, object> kvp in mvarProperties)
			{
				clone.Properties.Add(kvp.Key, kvp.Value);
			}
			return clone;
		}

		public override string ToString()
		{
			string strSize = "*";
			try
			{
				byte[] data = this.GetData();
				if (data != null)
				{
					strSize = data.Length.ToString();
				}
			}
			catch
			{
				strSize = "?";
			}
			return mvarName + " [" + strSize + "]";
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

			FileSource source = mvarSource;
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
					if (mvarSource != null)
					{
						return mvarSource.GetLength();
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

		#region Metadata
		private string mvarTitle = null;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarDescription = null;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }
		#endregion

		public event DataRequestEventHandler DataRequest;

		private Dictionary<string, object> mvarProperties = new Dictionary<string,object>();
		public Dictionary<string, object> Properties { get { return mvarProperties; } }

		private DateTime mvarModificationTimestamp = DateTime.Now;
		public DateTime ModificationTimestamp { get { return mvarModificationTimestamp; } set { mvarModificationTimestamp = value; } }

		private FileSource mvarSource = null;
		/// <summary>
		/// Determines where this <see cref="File" /> gets its data from.
		/// </summary>
		public FileSource Source { get { return mvarSource; } set { mvarSource = value; } }

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
			long blockSize = (System.Environment.WorkingSet / BLOCK_FRACTION);
			
			long offset = 0;
			long blockCount = mvarSource.GetLength() / blockSize;

			if (transformations != null)
			{
			
			}

			for (long i = 0; i < blockCount; i++)
			{
				byte[] data = mvarSource.GetData(offset, blockSize);
				offset += blockSize;

				bw.WriteBytes(data);
				count += data.Length;
			}
			bw.Flush();
			return count;
		}
	}
}
