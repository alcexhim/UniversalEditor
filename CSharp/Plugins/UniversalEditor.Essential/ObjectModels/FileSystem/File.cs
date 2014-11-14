using System;
using System.Collections.Generic;
using System.Text;

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
				file.SetDataAsByteArray(FileData);
				base.Add(file);
				return file;
			}
			public File Add(string FileName, string SourceFileName)
			{
				File file = new File();
				file.Name = FileName;
				file.SetDataAsByteArray(System.IO.File.ReadAllBytes(SourceFileName));
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

		public event DataRequestEventHandler DataRequest;

		private string mvarName = String.Empty;
		/// <summary>
		/// The name of this file.
		/// </summary>
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private byte[] mvarData = null;
		public byte[] GetDataAsByteArray()
		{
			if (mvarData == null && DataRequest == null)
			{
				throw new InvalidOperationException("Data is not represented as a byte array and data request is not handled");
			}
			else if (mvarData == null && DataRequest != null)
			{
				DataRequestEventArgs e = new DataRequestEventArgs();
				DataRequest(this, e);
				mvarData = e.Data;
			}
			return mvarData;
		}
		public void SetDataAsByteArray(byte[] data)
		{
			mvarStream = null;
			mvarData = data;
		}

		private System.IO.Stream mvarStream = null;
		public System.IO.Stream GetDataAsStream()
		{
			if (mvarStream == null) throw new InvalidOperationException("Data is not represented as a stream");
			return mvarStream;
		}
		public void SetDataAsStream(System.IO.Stream stream)
		{
			mvarStream = stream;
			mvarData = null;
		}

		public string GetDataAsString()
		{
			return GetDataAsString(Encoding.Default);
		}
		public string GetDataAsString(Encoding encoding)
		{
			if (mvarData == null) throw new InvalidOperationException("Data is not represented as a byte array");
			return encoding.GetString(mvarData);
		}
		public void SetDataAsString(string value)
		{
			SetDataAsString(value, Encoding.Default);
		}
		public void SetDataAsString(string value, Encoding encoding)
		{
			SetDataAsByteArray(encoding.GetBytes(value));
		}

		public object Clone()
		{
			File clone = new File();
			clone.Name = mvarName;
			clone.DataRequest = DataRequest;
			if (mvarData == null && mvarStream != null)
			{
				clone.SetDataAsStream(mvarStream);
			}
			else if (mvarData != null && mvarStream == null)
			{
				clone.SetDataAsByteArray(GetDataAsByteArray());
			}
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
				byte[] data = this.GetDataAsByteArray();
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
			System.IO.File.WriteAllBytes(FileName, GetDataAsByteArray());
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
					if (mvarData != null)
					{
						return mvarData.Length;
					}
					else if (mvarStream != null)
					{
						return mvarStream.Length;
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

		private Dictionary<string, object> mvarProperties = new Dictionary<string,object>();
		public Dictionary<string, object> Properties { get { return mvarProperties; } }

		private DateTime mvarModificationTimestamp = DateTime.Now;
		public DateTime ModificationTimestamp { get { return mvarModificationTimestamp; } set { mvarModificationTimestamp = value; } }
	}
}
