using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
