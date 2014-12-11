using System;
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
			foreach (Association assoc in _associations)
			{
				if ((objectModel != null && assoc.ObjectModels.Contains(objectModel)) || (dataFormat != null && assoc.DataFormats.Contains(dataFormat)))
				{
					associations.Add(assoc);
				}
			}
			return associations.ToArray();
		}
		public static Association[] FromAccessor(Accessor accessor = null, string fileNameFilter = null)
		{
			Association[] _associations = Association.GetAllAssociations();
			List<Association> associations = new List<Association>();
			Association[] assocs = _associations;
			foreach (Association assoc in assocs)
			{
				foreach (DataFormatFilter filter in assoc.Filters)
				{
					if (accessor != null)
					{
						for (int i = 0; i < filter.MagicBytes.Count; i++)
						{
							byte?[] bytes = filter.MagicBytes[i];
							if ((accessor.Position + bytes.Length) <= accessor.Length)
							{
								bool ret = true;
								byte[] cmp = new byte[bytes.Length];
								long offset = accessor.Position;
								if (i < filter.MagicByteOffsets.Length)
								{
									if (filter.MagicByteOffsets[i] < 0)
									{
										accessor.Seek(filter.MagicByteOffsets[i], SeekOrigin.End);
									}
									else
									{
										accessor.Seek(filter.MagicByteOffsets[i], SeekOrigin.Begin);
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
									associations.Add(assoc);
									break;
								}
							}
						}
					}
					if (fileNameFilter != null)
					{
						if (filter.FileNameFilters.Contains(fileNameFilter))
						{
							associations.Add(assoc);
							break;
						}
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
	}
}
