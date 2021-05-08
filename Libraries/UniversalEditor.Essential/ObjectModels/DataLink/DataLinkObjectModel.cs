//
//  DataLinkObjectModel.cs - provides an ObjectModel to manipulate Microsoft Universal Data Link shortcuts
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

namespace UniversalEditor.ObjectModels.DataLink
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> to manipulate Microsoft Universal Data Link shortcuts.
	/// </summary>
	public class DataLinkObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Data Link" };
			}
			return _omr;
		}

		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// The name of the OLEDB provider to use in this data link. If no Provider is specified,
		/// the OLE DB Provider for ODBC (MSDASQL) is the default value. This provides backward
		/// compatibility with ODBC connection strings.
		/// </summary>
		public string ProviderName { get; set; } = String.Empty;
		/// <summary>
		/// The version number of the OLEDB provider to use. If unspecified, will use the version-
		/// independent OLEDB provider.
		/// </summary>
		public string ProviderVersion { get; set; } = null;
		public bool PersistSecurityInformation { get; set; } = false;
		/// <summary>
		/// The name of the data source or host name of the server from which to retrieve data.
		/// </summary>
		public string DataSourceName { get; set; } = String.Empty;
		/// <summary>
		/// The initial catalog or name of the database from which to retrieve data.
		/// </summary>
		public string InitialCatalog { get; set; } = String.Empty;
		/// <summary>
		/// Determines whether integrated security (e.g. Windows authentication) is to be used.
		/// </summary>
		public bool UseIntegratedSecurity { get; set; } = false;
		/// <summary>
		/// Additional properties to pass into the connection string.
		/// </summary>
		public PropertyList.Property.PropertyCollection Properties { get; } = new PropertyList.Property.PropertyCollection();
	}
}
