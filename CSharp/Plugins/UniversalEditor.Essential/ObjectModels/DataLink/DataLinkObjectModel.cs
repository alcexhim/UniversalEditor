using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.DataLink
{
	public class DataLinkObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Data Link";
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

		private string mvarProviderName = String.Empty;
		/// <summary>
		/// The name of the OLEDB provider to use in this data link. If no Provider is specified,
		/// the OLE DB Provider for ODBC (MSDASQL) is the default value. This provides backward
		/// compatibility with ODBC connection strings.
		/// </summary>
		public string ProviderName { get { return mvarProviderName; } set { mvarProviderName = value; } }

		private string mvarProviderVersion = null;
		/// <summary>
		/// The version number of the OLEDB provider to use. If unspecified, will use the version-
		/// independent OLEDB provider.
		/// </summary>
		public string ProviderVersion { get { return mvarProviderVersion; } set { mvarProviderVersion = value; } }

		private bool mvarPersistSecurityInformation = false;
		public bool PersistSecurityInformation { get { return mvarPersistSecurityInformation; } set { mvarPersistSecurityInformation = value; } }

		private string mvarDataSourceName = String.Empty;
		/// <summary>
		/// The name of the data source or host name of the server from which to retrieve data.
		/// </summary>
		public string DataSourceName { get { return mvarDataSourceName; } set { mvarDataSourceName = value; } }

		private string mvarInitialCatalog = String.Empty;
		/// <summary>
		/// The initial catalog or name of the database from which to retrieve data.
		/// </summary>
		public string InitialCatalog { get { return mvarInitialCatalog; } set { mvarInitialCatalog = value; } }

		private bool mvarUseIntegratedSecurity = false;
		/// <summary>
		/// Determines whether integrated security (e.g. Windows authentication) is to be used.
		/// </summary>
		public bool UseIntegratedSecurity { get { return mvarUseIntegratedSecurity; } set { mvarUseIntegratedSecurity = value; } }

		private PropertyList.Property.PropertyCollection mvarProperties = new PropertyList.Property.PropertyCollection();
		/// <summary>
		/// Additional properties to pass into the connection string.
		/// </summary>
		public PropertyList.Property.PropertyCollection Properties { get { return mvarProperties; } }

	}
}
