//
//  DataFormatFilter.cs - stores magic byte / file name filters for a DF
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
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
			public DataFormatFilter Add(string Title, string[] FileNameFilters, byte?[][] MagicBytes, DataFormatHintComparison comparison = DataFormatHintComparison.FilterThenMagic)
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
				dff.HintComparison = comparison;
				base.Add(dff);
				return dff;
			}
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private DataFormatHintComparison mvarHintComparison = DataFormatHintComparison.Unspecified;
		public DataFormatHintComparison HintComparison { get { return mvarHintComparison; } set { mvarHintComparison = value; } }

		private System.Collections.Specialized.StringCollection mvarFileNameFilters = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection FileNameFilters { get { return mvarFileNameFilters; } }

		private List<byte?[]> mvarMagicBytes = new List<byte?[]>();
		public List<byte?[]> MagicBytes { get { return mvarMagicBytes; } }

		/// <summary>
		/// Determines if this <see cref="DataFormatFilter" /> matches the content of the specified
		/// <see cref="Accessor" />. Depending on the value of <see cref="HintComparison" />, this will look at the
		/// data in the <see cref="Accessor" /> as well as the file name provided by the
		/// <see cref="Accessor.GetFileName" /> method.
		/// </summary>
		/// <param name="accessor">The <see cref="Accessor" /> to examine for a match.</param>
		/// <returns>True if the filter applies to the given <see cref="Accessor" />; false otherwise.</returns>
		public bool Matches(Accessor accessor)
		{
			switch (mvarHintComparison)
			{
				case DataFormatHintComparison.Always: return true;
				case DataFormatHintComparison.Never: return false;
			}

			bool basedOnFilter = false;
			bool basedOnMagic = false;

			// first determine if our file name matches any of the filters
			string fileName = accessor.GetFileName();
			fileName = System.IO.Path.GetFileName(fileName);

			bool caseSensitiveOS = true;
			// extremely hacky
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					caseSensitiveOS = false;
					break;
				}
			}

			if (!caseSensitiveOS) fileName = fileName.ToLower();
			for (int i = 0; i < mvarFileNameFilters.Count; i++)
			{
				string filter = mvarFileNameFilters[i];
				if (!caseSensitiveOS) filter = filter.ToLower();

				if (fileName.Match(mvarFileNameFilters[i]))
				{
					basedOnFilter = true;
					break;
				}
			}

			bool requiresOpen = false;
			if (!accessor.IsOpen) requiresOpen = true;

			if (requiresOpen)
			{
				try
				{
					accessor.Open();
				}
				catch (Exception ex)
				{
					basedOnMagic = false;
					requiresOpen = false;
				}
			}

			if (accessor.IsOpen)
			{
				// then determine if the magic bytes match
				for (int i = 0; i < mvarMagicBytes.Count; i++)
				{
					byte?[] bytes = mvarMagicBytes[i];
					if ((accessor.Position + bytes.Length) <= accessor.Length)
					{
						bool ret = true;
						byte[] cmp = new byte[bytes.Length];

						long offset = accessor.Position;
						if (i < mvarMagicByteOffsets.Length)
						{
							if (mvarMagicByteOffsets[i] < 0)
							{
								accessor.Seek(mvarMagicByteOffsets[i], SeekOrigin.End);
							}
							else
							{
								accessor.Seek(mvarMagicByteOffsets[i], SeekOrigin.Begin);
							}
						}
						accessor.Reader.Read(cmp, 0, cmp.Length);
						accessor.Position = offset;

						for (int j = 0; j < bytes.Length; j++)
						{
							if (bytes[j] == null) continue;
							if (bytes[j] != cmp[j])
							{
								ret = false;
								break;
							}
						}
						if (ret)
						{
							basedOnMagic = true;
							break;
						}
					}
				}
			}

			if (requiresOpen)
			{
				accessor.Close();
				requiresOpen = false;
			}

			switch (mvarHintComparison)
			{
				case DataFormatHintComparison.Always: return true;
				case DataFormatHintComparison.Never: return false;
				case DataFormatHintComparison.FilterOnly:
				{
					return basedOnFilter;
				}
				case DataFormatHintComparison.FilterThenMagic:
				{
					if (!basedOnFilter) return basedOnMagic;
					return basedOnFilter;
				}
				case DataFormatHintComparison.MagicOnly:
				{
					return basedOnMagic;
				}
				case DataFormatHintComparison.Unspecified:
				case DataFormatHintComparison.MagicThenFilter:
				{
					if (!basedOnMagic) return basedOnFilter;
					return basedOnMagic;
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
