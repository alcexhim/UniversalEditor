//
//  Association.cs - explicitly associates accessor with data format and/or object model
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
	/// <summary>
	/// Associates <see cref="ObjectModel" />s, <see cref="DataFormat" />s, and other related Universal Editor objects.
	/// </summary>
	public class Association
	{
		public class AssociationCollection
			: System.Collections.ObjectModel.Collection<Association>
		{

		}

		private static List<Association> _associations = new List<Association>();


		public static bool Register(Association assoc)
		{
			if (_associations.Contains(assoc)) return false;
			_associations.Add(assoc);
			return true;
		}
		public static bool Unregister(Association assoc)
		{
			if (!_associations.Contains(assoc)) return false;
			_associations.Remove(assoc);
			return true;
		}

		public static Association[] GetAllAssociations()
		{
			return _associations.ToArray();
		}
		public static Association[] FromObjectModelOrDataFormat(ObjectModelReference objectModel = null, DataFormatReference dataFormat = null)
		{
			Association[] _associations = Association.GetAllAssociations();
			List<Association> associations = new List<Association>();
			for (int i = 0; i < _associations.Length; i++)
			{
				if ((objectModel != null && _associations[i].ObjectModels.Contains(objectModel)) || (dataFormat != null && _associations[i].DataFormats.Contains(dataFormat)))
				{
					associations.Add(_associations[i]);
				}
			}
			return associations.ToArray();
		}
		public static Association[] FromAccessor(Accessor accessor = null, string fileNameFilter = null)
		{
			Association[] assocs = Association.GetAllAssociations();
			List<Association> associations = new List<Association>();

			// stopwatch diagnostics determined a nested for loop is 0.0547024 ms faster than foreach
			for (int i = 0; i < assocs.Length; i++)
			{
				for (int j = 0;  j < assocs[i].Filters.Count;  j++)
				{
					if (assocs[i].Filters[j].Matches(accessor))
					{
						associations.Add(assocs[i]);
					}
				}
			}
			return associations.ToArray();
		}

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of this <see cref="Association" />; for example, "JPEG images".
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private DataFormatFilter.DataFormatFilterCollection mvarFilters = new DataFormatFilter.DataFormatFilterCollection();
		/// <summary>
		/// The filters that are used to determine which documents can be handled by this <see cref="Association" />.
		/// </summary>
		public DataFormatFilter.DataFormatFilterCollection Filters { get { return mvarFilters; } }

		private string mvarExternalCommandLine = String.Empty;
		/// <summary>
		/// The command line of an external application to launch when a file handled by this association is opened.
		/// </summary>
		public string ExternalCommandLine { get { return mvarExternalCommandLine; } set { mvarExternalCommandLine = value; } }

		private ObjectModelReference.ObjectModelReferenceCollection mvarObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		/// <summary>
		/// The <see cref="ObjectModelReference" />s which refer to <see cref="ObjectModel" />s that are included in this <see cref="Association" />.
		/// </summary>
		public ObjectModelReference.ObjectModelReferenceCollection ObjectModels { get { return mvarObjectModels; } }

		private DataFormatReference.DataFormatReferenceCollection mvarDataFormats = new DataFormatReference.DataFormatReferenceCollection();
		/// <summary>
		/// The <see cref="DataFormatReference" />s which refer to <see cref="DataFormat" />s that are included in this <see cref="Association" />.
		/// </summary>
		public DataFormatReference.DataFormatReferenceCollection DataFormats { get { return mvarDataFormats; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append('[');
			for (int i = 0; i < mvarFilters.Count; i++)
			{
				sb.Append(mvarFilters[i].Title);
				if (i < mvarFilters.Count - 1) sb.Append(", ");
			}
			sb.Append(']');
			sb.Append(' ');
			sb.Append('{');
			for (int i = 0; i < mvarObjectModels.Count; i++)
			{
				sb.Append(mvarObjectModels[i].TypeName);
				if (i < mvarObjectModels.Count - 1) sb.Append(", ");
			}
			sb.Append('}');
			sb.Append(' ');
			sb.Append('(');
			for (int i = 0; i < mvarDataFormats.Count; i++)
			{
				sb.Append(mvarDataFormats[i].TypeName);
				if (i < mvarDataFormats.Count - 1) sb.Append(", ");
			}
			sb.Append(')');
			return sb.ToString();
		}

		public static Association[] FromCriteria(AssociationCriteria ac)
		{
			List<Association> associations = new List<Association>();
			Association[] _associations = GetAllAssociations();
			foreach (Association assoc in _associations)
			{
				if (ac.ObjectModel != null)
				{
					if (assoc.ObjectModels.Contains(ac.ObjectModel))
					{
						associations.Add(assoc);
						continue;
					}
				}
				if (ac.DataFormat != null)
				{
					if (assoc.DataFormats.Contains(ac.DataFormat))
					{
						associations.Add(assoc);
						continue;
					}
				}
				if (ac.Accessor != null)
				{
					bool found = false;
					foreach (DataFormatFilter filter in assoc.Filters)
					{
						if (filter.Matches(ac.Accessor))
						{
							associations.Add(assoc);
							found = true;
							break;
						}
					}
					if (found) continue;
				}
			}
			return associations.ToArray();
		}
	}
}
