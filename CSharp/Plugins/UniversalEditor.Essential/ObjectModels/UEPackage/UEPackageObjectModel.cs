using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UEPackage
{
	public class UEPackageObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Universal Editor Package";
				_omr.Path = new string[] { "Extensibility" };
			}
			return _omr;
		}

		private ObjectModelReference.ObjectModelReferenceCollection mvarObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		/// <summary>
		/// The <see cref="ObjectModel" />s provided by this package.
		/// </summary>
		public ObjectModelReference.ObjectModelReferenceCollection ObjectModels { get { return mvarObjectModels; } }

		private DataFormatReference.DataFormatReferenceCollection mvarDataFormats = new DataFormatReference.DataFormatReferenceCollection();
		/// <summary>
		/// The <see cref="DataFormat" />s provided by this package.
		/// </summary>
		public DataFormatReference.DataFormatReferenceCollection DataFormats { get { return mvarDataFormats; } }

		private ProjectType.ProjectTypeCollection mvarProjectTypes = new ProjectType.ProjectTypeCollection();
		/// <summary>
		/// The <see cref="ProjectType" />s provided by this package.
		/// </summary>
		public ProjectType.ProjectTypeCollection ProjectTypes { get { return mvarProjectTypes; } }

		private DocumentTemplate.DocumentTemplateCollection mvarDocumentTemplates = new DocumentTemplate.DocumentTemplateCollection();
		public DocumentTemplate.DocumentTemplateCollection DocumentTemplates { get { return mvarDocumentTemplates; } }

		private ProjectTemplate.ProjectTemplateCollection mvarProjectTemplates = new ProjectTemplate.ProjectTemplateCollection();
		public ProjectTemplate.ProjectTemplateCollection ProjectTemplates { get { return mvarProjectTemplates; } }

		public override void Clear()
		{
			mvarDataFormats.Clear();
			mvarObjectModels.Clear();
			mvarProjectTypes.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
