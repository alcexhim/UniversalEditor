//
//  UEPackageObjectModel.cs - provides an ObjectModel to manipulate Universal Editor package, extension, and configuration files
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

namespace UniversalEditor.ObjectModels.UEPackage
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> to manipulate Universal Editor package, extension, and configuration files.
	/// </summary>
	public class UEPackageObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Extensibility", "Universal Editor package" };
			}
			return _omr;
		}

		private Association.AssociationCollection mvarAssociations = new Association.AssociationCollection();
		/// <summary>
		/// The <see cref="Association" />s provided by this package.
		/// </summary>
		public Association.AssociationCollection Associations { get { return mvarAssociations; } }

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
			mvarAssociations.Clear();
			mvarDataFormats.Clear();
			mvarObjectModels.Clear();
			mvarProjectTypes.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
