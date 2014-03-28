using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.IO;

namespace UniversalEditor
{
	public abstract class DataFormat
	{
		internal DataFormatReference mvarReference = null;
		/// <summary>
		/// The DataFormatReference used to create this DataFormat.
		/// </summary>
		public DataFormatReference Reference
		{
			get { return mvarReference; }
		}

		public virtual DataFormatReference MakeReference()
		{
			DataFormatReference dfr = new DataFormatReference(GetType());
			return dfr;
		}
		
		private Accessor mvarAccessor = null;
		protected internal Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		protected virtual bool IsObjectModelSupported(ObjectModel objectModel)
		{
			DataFormatReference dfr = MakeReference();
			ObjectModelReference omr = objectModel.MakeReference();
			return dfr.Capabilities.Contains(omr.ObjectModelType) || dfr.Capabilities.Contains(omr.ObjectModelID);
		}

		public void Load(ref ObjectModel objectModel)
		{
			if (objectModel == null) throw new ArgumentNullException("objectModel", "objectModel cannot be null");

			Stack<ObjectModel> stack = new Stack<ObjectModel>();
			stack.Push(objectModel);
			BeforeLoadInternal(stack);

			ObjectModel omb = stack.Pop();
			LoadInternal(ref omb);
			stack.Push(omb);
			/*
			if (!IsObjectModelSupported(omb))
			{
				throw new NotSupportedException("Object model not supported");
			}
			*/
			AfterLoadInternal(stack);
		}
		public void Save(ObjectModel objectModel)
		{
			if (objectModel == null) throw new ArgumentNullException("objectModel", "objectModel cannot be null");

			Stack<ObjectModel> stack = new Stack<ObjectModel>();
			stack.Push(objectModel);
			BeforeSaveInternal(stack);

			ObjectModel omb = stack.Pop();
			SaveInternal(omb);
			stack.Push(omb);

			AfterSaveInternal(stack);
		}

		protected abstract void LoadInternal(ref ObjectModel objectModel);
		protected abstract void SaveInternal(ObjectModel objectModel);

		protected virtual void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
		}
		protected virtual void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
		}
		protected virtual void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
		}
		protected virtual void AfterSaveInternal(Stack<ObjectModel> objectModels)
		{
		}
	}
	public enum DataFormatCapabilities
	{
		None = 0,
		Load = 1,
		Save = 2,
		Bootstrap = 4,
		All = Load | Save | Bootstrap
	}
	public class DataFormatReference
	{
		private string mvarTitle = null;
		public string Title
		{
			get
			{
				if (mvarTitle == null)
				{
					if (mvarFilters.Count > 0) return mvarFilters[0].Title;
				}
				return mvarTitle;
			}
			set { mvarTitle = value; }
		}

		private Type mvarDataFormatType = null;
		public Type DataFormatType { get { return mvarDataFormatType; } }

		public DataFormatReference(Type dataFormatType)
		{
			if (!dataFormatType.IsSubclassOf(typeof(DataFormat)))
			{
				throw new InvalidCastException("Cannot create a data format reference to a non-DataFormat type");
			}
			else if (dataFormatType.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create a data format reference to an abstract type");
			}

			mvarDataFormatType = dataFormatType;
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private DataFormatFilter.DataFormatFilterCollection mvarFilters = new DataFormatFilter.DataFormatFilterCollection();
		public DataFormatFilter.DataFormatFilterCollection Filters { get { return mvarFilters; } }

		private DataFormatCapabilityCollection mvarCapabilities = new DataFormatCapabilityCollection();
		public DataFormatCapabilityCollection Capabilities { get { return mvarCapabilities; } }

		private System.Collections.Specialized.StringCollection mvarContentTypes = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection ContentTypes { get { return mvarContentTypes; } }

		private System.Collections.Specialized.StringCollection mvarSources = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Sources { get { return mvarSources; } }

		private CustomOption.CustomOptionCollection mvarImportOptions = new CustomOption.CustomOptionCollection();
		public CustomOption.CustomOptionCollection ImportOptions { get { return mvarImportOptions; } }

		private CustomOption.CustomOptionCollection mvarExportOptions = new CustomOption.CustomOptionCollection();
		public CustomOption.CustomOptionCollection ExportOptions { get { return mvarExportOptions; } }

		public virtual DataFormat Create()
		{
			DataFormat df = (mvarDataFormatType.Assembly.CreateInstance(mvarDataFormatType.FullName) as DataFormat);
			df.mvarReference = this;
			return df;
		}

		public void Clear()
		{
			mvarCapabilities.Clear();
			mvarContentTypes.Clear();
			mvarFilters.Clear();
			mvarSources.Clear();
			mvarTitle = null;
		}

		public override string ToString()
		{
			if (!String.IsNullOrEmpty(mvarTitle))
			{
				return mvarTitle;
			}
			else if (mvarFilters.Count > 0 && !String.IsNullOrEmpty(mvarFilters[0].Title))
			{
				return mvarFilters[0].Title;
			}
			else if (mvarDataFormatType != null)
			{
				return mvarDataFormatType.FullName;
			}
			return GetType().FullName;
		}

		private int mvarPriority = 0;
		public int Priority { get { return mvarPriority; } set { mvarPriority = value; } }
	}
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
			System.IO.FileStream fs = null;
			if (System.IO.File.Exists(FileName))
			{
				fs = System.IO.File.Open(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
			}
			bool matches = MatchesFile(FileName, fs);
			if (fs != null) fs.Close();
			return matches;
		}
		public bool MatchesFile(string FileName, byte[] FileData)
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream(FileData);
			return MatchesFile(FileName, ms);
		}
		public bool MatchesFile(string FileName, System.IO.Stream FileData)
		{
			if (FileName != null)
			{
				FileName = System.IO.Path.GetFileName(FileName);
			}

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
										FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.End);
									}
									else
									{
										FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.Begin);
									}
								}
								FileData.Read(cmp, 0, cmp.Length);
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
										FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.End);
									}
									else
									{
										FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.Begin);
									}
								}
								FileData.Read(cmp, 0, cmp.Length);
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
										FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.End);
									}
									else
									{
										FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.Begin);
									}
								}
								FileData.Read(cmp, 0, cmp.Length);
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
		public bool MatchesFile(System.IO.Stream FileData)
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
									FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.End);
								}
								else
								{
									FileData.Seek(mvarMagicByteOffsets[i], System.IO.SeekOrigin.Begin);
								}
							}
							FileData.Read(cmp, 0, cmp.Length);
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
	public enum DataFormatHintComparison
	{
		None = 0,
		FilterOnly = 1,
		MagicOnly = 2,
		FilterThenMagic = 3,
		MagicThenFilter = 4,
		Always = 5
	}
}
