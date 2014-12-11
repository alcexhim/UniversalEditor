﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class DataFormatReference : ReferencedBy<DataFormat>
	{
		public class DataFormatReferenceCollection
			: System.Collections.ObjectModel.Collection<DataFormatReference>
		{

		}

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

		private Type mvarType = null;
		public Type Type { get { return mvarType; } }

		private string mvarTypeName = null;
		public string TypeName { get { return mvarTypeName; } set { mvarTypeName = value; } }

		private string DataFormatFilterCollectionToString(DataFormatFilter.DataFormatFilterCollection collection)
		{
			StringBuilder sb = new StringBuilder();
			foreach (DataFormatFilter filter in collection)
			{
				sb.Append(StringArrayToString(filter.FileNameFilters));
				if (collection.IndexOf(filter) < collection.Count - 1)
				{
					sb.Append("; ");
				}
			}
			return sb.ToString();
		}
		private string StringArrayToString(System.Collections.Specialized.StringCollection collection)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in collection)
			{
				sb.Append(s);
				if (collection.IndexOf(s) < collection.Count - 1)
				{
					sb.Append(", ");
				}
			}
			return sb.ToString();
		}

		public string[] GetDetails()
		{
			string title = mvarTitle;
			if (String.IsNullOrEmpty(mvarTitle) && mvarFilters.Count > 0)
			{
				title = mvarFilters[0].Title;
			}
			return new string[] { title, DataFormatFilterCollectionToString(mvarFilters) };
		}
		public bool ShouldFilterObject(string filter)
		{
			string title = mvarTitle;
			if (String.IsNullOrEmpty(mvarTitle) && mvarFilters.Count > 0)
			{
				title = mvarFilters[0].Title;
			}
			if (title == null) title = String.Empty;
			if (title.ToLower().Contains(filter.ToLower())) return true;

			foreach (DataFormatFilter filter1 in mvarFilters)
			{
				foreach (string s in filter1.FileNameFilters)
				{
					if (s.ToLower().Contains(filter.ToLower()))
					{
						return true;
					}
				}
			}
			return false;
		}

		public DataFormatReference(Guid id)
		{
			mvarID = id;
		}
		public DataFormatReference(string dataFormatTypeName)
		{
			mvarTypeName = dataFormatTypeName;
		}
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

			mvarType = dataFormatType;
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
			DataFormat df = (mvarType.Assembly.CreateInstance(mvarType.FullName) as DataFormat);
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
			else if (mvarType != null)
			{
				return mvarType.FullName;
			}
			return GetType().FullName;
		}

		private int mvarPriority = 0;
		public int Priority { get { return mvarPriority; } set { mvarPriority = value; } }

		private static Dictionary<Guid, DataFormatReference> _referencesByGUID = new Dictionary<Guid, DataFormatReference>();
		private static Dictionary<string, DataFormatReference> _referencesByTypeName = new Dictionary<string, DataFormatReference>();

		public static bool Register(DataFormatReference dfr)
		{
			bool retval = false;
			if (dfr.Type != null)
			{
				dfr.TypeName = dfr.Type.FullName;
			}
			if (dfr.ID != Guid.Empty)
			{
				_referencesByGUID[dfr.ID] = dfr;
				retval = true;
			}
			if (dfr.TypeName != null)
			{
				_referencesByTypeName[dfr.TypeName] = dfr;
				retval = true;
			}
			return retval;
		}
		public static bool Unregister(DataFormatReference dfr)
		{
			bool retval = false;
			if (dfr.ID != Guid.Empty && _referencesByGUID.ContainsKey(dfr.ID))
			{
				_referencesByGUID.Remove(dfr.ID);
				retval = true;
			}
			if (dfr.TypeName != null && _referencesByTypeName.ContainsKey(dfr.TypeName))
			{
				_referencesByTypeName.Remove(dfr.TypeName);
				retval = true;
			}
			return retval;
		}

		public static DataFormatReference FromTypeName(string typeName)
		{
			if (_referencesByTypeName.ContainsKey(typeName)) return _referencesByTypeName[typeName];
			return null;
		}
		public static DataFormatReference FromGUID(Guid guid)
		{
			if (_referencesByGUID.ContainsKey(guid)) return _referencesByGUID[guid];
			return null;
		}
	}
}
