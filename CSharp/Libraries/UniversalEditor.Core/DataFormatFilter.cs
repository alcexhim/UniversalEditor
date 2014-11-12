using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor
{
	public class DataFormatFilter
	{
		public class DataFormatFilterCollection
			: System.Collections.ObjectModel.Collection<DataFormatFilter>
		{
			public DataFormatFilter Add(string Title, string[] FileNameFilters)
			{
				DataFormatFilter dff = new DataFormatFilter();
				dff.Title = Title;
				foreach (string FileNameFilter in FileNameFilters)
				{
					dff.FileNameFilters.Add(FileNameFilter);
				}
				dff.HintComparison = DataFormatHintComparison.FilterOnly;
				base.Add(dff);
				return dff;
			}
			public DataFormatFilter Add(string Title, string[] FileNameFilters, byte?[][] MagicBytes)
			{
				DataFormatFilter dff = new DataFormatFilter();
				dff.Title = Title;
				foreach (string FileNameFilter in FileNameFilters)
				{
					dff.FileNameFilters.Add(FileNameFilter);
				}
				foreach (byte?[] magicBytes in MagicBytes)
				{
					dff.MagicBytes.Add(magicBytes);
				}
				dff.HintComparison = DataFormatHintComparison.FilterThenMagic;
				base.Add(dff);
				return dff;
			}
			public DataFormatFilter Add(string Title, byte?[][] MagicBytes, string[] FileNameFilters)
			{
				DataFormatFilter dff = new DataFormatFilter();
				dff.Title = Title;
				foreach (string FileNameFilter in FileNameFilters)
				{
					dff.FileNameFilters.Add(FileNameFilter);
				}
				foreach (byte?[] magicBytes in MagicBytes)
				{
					dff.MagicBytes.Add(magicBytes);
				}
				dff.HintComparison = DataFormatHintComparison.MagicThenFilter;
				base.Add(dff);
				return dff;
			}
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private DataFormatHintComparison mvarHintComparison = DataFormatHintComparison.None;
		public DataFormatHintComparison HintComparison { get { return mvarHintComparison; } set { mvarHintComparison = value; } }

		private System.Collections.Specialized.StringCollection mvarFileNameFilters = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection FileNameFilters { get { return mvarFileNameFilters; } }

		private List<byte?[]> mvarMagicBytes = new List<byte?[]>();
		public List<byte?[]> MagicBytes { get { return mvarMagicBytes; } }

		public bool MatchesFile(byte[] FileData)
		{
			return MatchesFile(null, FileData);
		}
		public bool MatchesFile(string FileName)
		{
			UniversalEditor.Accessors.FileAccessor fa = null;
			if (System.IO.File.Exists(FileName))
			{
				fa = new UniversalEditor.Accessors.FileAccessor(FileName);
			}
			bool matches = MatchesFile(FileName, fa);
			if (fa != null) fa.Close();
			return matches;
		}
		public bool MatchesFile(string FileName, byte[] FileData)
		{
			UniversalEditor.Accessors.MemoryAccessor ms = new UniversalEditor.Accessors.MemoryAccessor(FileData);
			return MatchesFile(FileName, ms);
		}
		public bool MatchesFile(string FileName, Accessor FileData)
		{
			if (FileName == null) return false;
			if (System.IO.File.Exists(FileName)) FileName = System.IO.Path.GetFileName(FileName);

			if (!FileData.IsOpen) throw new InvalidOperationException("Accessor must be open");

			switch (mvarHintComparison)
			{
				case DataFormatHintComparison.Always:
				{
					return true;
				}
				case DataFormatHintComparison.None:
				{
					return false;
				}
				case DataFormatHintComparison.FilterOnly:
				{
					foreach (string filter in mvarFileNameFilters)
					{
						if (FileName.ToLower().Match(filter.ToLower())) return true;
					}
					return false;
				}
				case DataFormatHintComparison.FilterThenMagic:
				{
					foreach (string filter in mvarFileNameFilters)
					{
						if (FileName.ToLower().Match(filter.ToLower())) return true;
					}
					if (FileData != null)
					{
						for (int i = 0; i < mvarMagicBytes.Count; i++)
						{
							byte?[] bytes = mvarMagicBytes[i];
							if ((FileData.Position + bytes.Length) <= FileData.Length)
							{
								bool ret = true;
								byte[] cmp = new byte[bytes.Length];
								long offset = FileData.Position;
								if (i < mvarMagicByteOffsets.Length)
								{
									if (mvarMagicByteOffsets[i] < 0)
									{
										FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.End);
									}
									else
									{
										FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.Begin);
									}
								}
								FileData.Reader.Read(cmp, 0, cmp.Length);
								FileData.Position = offset;

								for (int j = 0; j < bytes.Length; j++)
								{
									if (bytes[j] == null) continue;
									if (bytes[j] != cmp[j])
									{
										ret = false;
										break;
									}
								}
								if (ret) return true;
							}
						}
					}
					return false;
				}
				case DataFormatHintComparison.MagicOnly:
				{
					if (FileData != null)
					{
						for (int i = 0; i < mvarMagicBytes.Count; i++)
						{
							byte?[] bytes = mvarMagicBytes[i];
							if ((FileData.Position + bytes.Length) <= FileData.Length)
							{
								byte[] cmp = new byte[bytes.Length];
								long offset = FileData.Position;
								if (i < mvarMagicByteOffsets.Length)
								{
									if (mvarMagicByteOffsets[i] < 0)
									{
										FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.End);
									}
									else
									{
										FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.Begin);
									}
								}
								FileData.Reader.Read(cmp, 0, cmp.Length);
								FileData.Position = offset;

								bool ret = true;
								for (int j = 0; j < bytes.Length; j++)
								{
									if (bytes[j] == null) continue;
									if (bytes[j] != cmp[j])
									{
										ret = false;
										break;
									}
								}
								if (ret) return true;
							}
						}
					}
					return false;
				}
				case DataFormatHintComparison.MagicThenFilter:
				{
					if (FileData != null)
					{
						for (int i = 0; i < mvarMagicBytes.Count; i++)
						{
							byte?[] bytes = mvarMagicBytes[i];
							if ((FileData.Position + bytes.Length) <= FileData.Length)
							{
								byte[] cmp = new byte[bytes.Length];
								long offset = FileData.Position;
								if (i < mvarMagicByteOffsets.Length)
								{
									if (mvarMagicByteOffsets[i] < 0)
									{
										FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.End);
									}
									else
									{
										FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.Begin);
									}
								}
								FileData.Reader.Read(cmp, 0, cmp.Length);
								FileData.Position = offset;

								bool ret = true;
								for (int j = 0; j < bytes.Length; j++)
								{
									if (bytes[j] == null) continue;
									if (bytes[j] != cmp[j])
									{
										ret = false;
										break;
									}
								}
								if (ret) return true;
							}
						}
					}
					foreach (string filter in mvarFileNameFilters)
					{
						if (FileName.ToLower().Match(filter.ToLower())) return true;
					}
					return false;
				}
			}
			return false;
		}
		public bool MatchesFile(Accessor FileData)
		{
			switch (mvarHintComparison)
			{
				case DataFormatHintComparison.Always:
				{
					return true;
				}
				case DataFormatHintComparison.MagicOnly:
				case DataFormatHintComparison.FilterThenMagic:
				case DataFormatHintComparison.MagicThenFilter:
				{
					for (int i = 0; i < mvarMagicBytes.Count; i++)
					{
						byte?[] bytes = mvarMagicBytes[i];
						if ((FileData.Position + bytes.Length) <= FileData.Length)
						{
							bool ret = true;
							byte[] cmp = new byte[bytes.Length];

							long offset = FileData.Position;
							if (i < mvarMagicByteOffsets.Length)
							{
								if (mvarMagicByteOffsets[i] < 0)
								{
									FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.End);
								}
								else
								{
									FileData.Seek(mvarMagicByteOffsets[i], SeekOrigin.Begin);
								}
							}
							FileData.Reader.Read(cmp, 0, cmp.Length);
							FileData.Position = offset;

							for (int j = 0; j < bytes.Length; j++)
							{
								if (bytes[j] == null) continue;
								if (bytes[j] != cmp[j])
								{
									ret = false;
									break;
								}
							}
							if (ret) return true;
						}
					}
					return false;
				}
				case DataFormatHintComparison.None:
				{
					return false;
				}
			}
			return false;
		}

		private int[] mvarMagicByteOffsets = new int[0];
		public int[] MagicByteOffsets { get { return mvarMagicByteOffsets; } set { mvarMagicByteOffsets = value; } }
	}
	public class DataFormatCapabilityCollection
	{
		private System.Collections.Generic.Dictionary<Type, DataFormatCapabilities> mvarCapabilities = new Dictionary<Type, DataFormatCapabilities>();
		private System.Collections.Generic.Dictionary<Guid, DataFormatCapabilities> mvarCapabilities2 = new Dictionary<Guid, DataFormatCapabilities>();

		public void Add(Type objectModelType, DataFormatCapabilities capabilities)
		{
			if (objectModelType == null) return;
			if (!objectModelType.IsSubclassOf(typeof(ObjectModel))) throw new InvalidOperationException("objectModelType is not an object model");
			if (mvarCapabilities.ContainsKey(objectModelType))
			{
				mvarCapabilities[objectModelType] = capabilities;
			}
			else
			{
				mvarCapabilities.Add(objectModelType, capabilities);
			}
		}

		public void Add(Guid objectModelID, DataFormatCapabilities capabilities)
		{
			if (mvarCapabilities2.ContainsKey(objectModelID))
			{
				mvarCapabilities2[objectModelID] = capabilities;
			}
			else
			{
				mvarCapabilities2.Add(objectModelID, capabilities);
			}
		}

		public void Remove(Type objectModelType)
		{
			if (!objectModelType.IsSubclassOf(typeof(ObjectModel))) throw new InvalidOperationException("objectModelType is not an object model");
			if (mvarCapabilities.ContainsKey(objectModelType))
			{
				mvarCapabilities.Remove(objectModelType);
			}
		}
		public void Remove(Guid objectModelID)
		{
			if (mvarCapabilities2.ContainsKey(objectModelID))
			{
				mvarCapabilities2.Remove(objectModelID);
			}
		}

		public void Clear()
		{
			mvarCapabilities.Clear();
			mvarCapabilities2.Clear();
		}

		public DataFormatCapabilities this[Type objectModelType]
		{
			get
			{
				if (!objectModelType.IsSubclassOf(typeof(ObjectModel))) return DataFormatCapabilities.None;
				if (!mvarCapabilities.ContainsKey(objectModelType)) return DataFormatCapabilities.None;
				return mvarCapabilities[objectModelType];
			}
		}
		public DataFormatCapabilities this[Guid objectModelID]
		{
			get
			{
				if (!mvarCapabilities2.ContainsKey(objectModelID)) return DataFormatCapabilities.None;
				return mvarCapabilities2[objectModelID];
			}
		}

		public bool Contains(Type objectModelType)
		{
			return mvarCapabilities.ContainsKey(objectModelType);
		}
		public bool Contains(Guid objectModelID)
		{
			return mvarCapabilities2.ContainsKey(objectModelID);
		}
	}
}
