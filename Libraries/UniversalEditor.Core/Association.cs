//
//  Association.cs - explicitly associates accessor with data format and/or object model
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
using System.Text;

namespace UniversalEditor
{
	/// <summary>
	/// Associates <see cref="ObjectModel" />s, <see cref="DataFormat" />s, and other related Universal Editor objects.
	/// </summary>
	public class Association : IComparable<Association>
	{
		/// <summary>
		/// Defines a collection of <see cref="Association" />s.
		/// </summary>
		public class AssociationCollection
			: System.Collections.ObjectModel.Collection<Association>
		{

		}

		private static List<Association> _associations = new List<Association>();

		/// <summary>
		/// Registers the specified <see cref="Association" /> if it is not already registered.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="Association" /> is not already registered and has been added; <c>false</c> otherwise.</returns>
		/// <param name="assoc">The <see cref="Association" /> to register.</param>
		public static bool Register(Association assoc)
		{
			if (_associations.Contains(assoc)) return false;
			_associations.Add(assoc);
			return true;
		}
		/// <summary>
		/// Unregisters the specified <see cref="Association" /> if it has been registered.
		/// </summary>
		/// <returns><c>true</c> if the given <see cref="Association" /> has been unregistered; <c>false</c> if the given <see cref="Association" /> has not been registered.</returns>
		/// <param name="assoc">The <see cref="Association" /> to unregister.</param>
		public static bool Unregister(Association assoc)
		{
			if (!_associations.Contains(assoc)) return false;
			_associations.Remove(assoc);
			return true;
		}
		/// <summary>
		/// Returns an array of all known <see cref="Association" />s.
		/// </summary>
		/// <returns>The all associations.</returns>
		public static Association[] GetAllAssociations()
		{
			return _associations.ToArray();
		}
		/// <summary>
		/// Gets an array of <see cref="Association" />s that match the given <see cref="ObjectModelReference" /> or <see cref="DataFormatReference" />.
		/// </summary>
		/// <returns>An array of <see cref="Association" />s that match the given <see cref="ObjectModelReference" /> or <see cref="DataFormatReference" />.</returns>
		/// <param name="objectModel">The <see cref="ObjectModelReference" /> to compare.</param>
		/// <param name="dataFormat">The <see cref="DataformatReference" /> to compare.</param>
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
		/// <summary>
		/// Gets an array of <see cref="Association" />s that match the given <see cref="Accessor" /> or file name filters.
		/// </summary>
		/// <returns>An array of <see cref="Association" />s that match the given <see cref="Accessor" /> or file name filters.</returns>
		/// <param name="accessor">The <see cref="Accessor" /> to compare.</param>
		/// <param name="fileNameFilter">Not implemented.</param>
		public static Association[] FromAccessor(Accessor accessor = null, string fileNameFilter = null)
		{
			Association[] assocs = Association.GetAllAssociations();
			List<Association> associations = new List<Association>();

			// FIXME: the fileNameFilter parameter is not referenced in this method body
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

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:UniversalEditor.Association"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:UniversalEditor.Association"/>.</returns>
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

		/// <summary>
		/// Returns an array of <see cref="Association" />s that satisfy the given <see cref="AssociationCriteria" />.
		/// </summary>
		/// <returns>An array of <see cref="Association" />s that match the specified criteria.</returns>
		/// <param name="ac">The <see cref="AssociationCriteria" /> that define the criteria to match when searching for <see cref="Association"/>s.</param>
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
						if (ac.DataFormat != null)
						{
							if (assoc.DataFormats.Contains(ac.DataFormat))
							{
								associations.Add(assoc);
								continue;
							}
						}
						else
						{
							associations.Add(assoc);
						}
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

		public int CompareTo(Association other)
		{
			int nFileFormatFilters = 0, nMagicBytes = 0;
			foreach (DataFormatFilter filter in Filters)
			{
				nFileFormatFilters += filter.FileNameFilters.Count;
				nMagicBytes += filter.MagicBytes.Count;
			}

			int nFileFormatFiltersOther = 0, nMagicBytesOther = 0;
			foreach (DataFormatFilter filter in other.Filters)
			{
				nFileFormatFiltersOther += filter.FileNameFilters.Count;
				nMagicBytesOther += filter.MagicBytes.Count;
			}

			return (nFileFormatFilters + nMagicBytes).CompareTo(nFileFormatFiltersOther + nMagicBytesOther);
		}
	}
}
