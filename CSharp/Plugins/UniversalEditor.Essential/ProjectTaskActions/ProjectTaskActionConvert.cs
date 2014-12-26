using System;
using System.Collections.Generic;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Project;

namespace UniversalEditor.ProjectTaskActions
{
	/// <summary>
	/// Converts an <see cref="ObjectModel" /> from an input  <see cref="DataFormat" /> to an
	/// output <see cref="DataFormat" />.
	/// </summary>
	public class ProjectTaskActionConvert : ProjectTaskAction
	{
		private DataFormatReference mvarInputDataFormatReference = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" />
		/// compatible with the <see cref="ObjectModel" /> of the specified
		/// <see cref="ProjectFile" />.
		/// </summary>
		public DataFormatReference InputDataFormatReference { get { return mvarInputDataFormatReference; } set { mvarInputDataFormatReference = value; } }

		private DataFormatReference mvarOutputDataFormatReference = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" />
		/// compatible with the <see cref="ObjectModel" /> of the specified
		/// <see cref="ProjectFile" />.
		/// </summary>
		public DataFormatReference OutputDataFormatReference { get { return mvarOutputDataFormatReference; } set { mvarOutputDataFormatReference = value; } }

		private ProjectFile mvarProjectFile = null;
		/// <summary>
		/// The <see cref="ProjectFile" /> to be converted upon project build.
		/// </summary>
		public ProjectFile ProjectFile { get { return mvarProjectFile; } set { mvarProjectFile = value; } }

		private static ProjectTaskActionReference _ptar = null;
		protected override ProjectTaskActionReference MakeReferenceInternal()
		{
			if (_ptar == null)
			{
				_ptar = base.MakeReferenceInternal();
				_ptar.ProjectTaskActionTypeID = new Guid("{0EB3C0A9-804A-433F-BDD8-888D0CA8C760}");
				_ptar.ProjectTaskActionTypeName = "UniversalEditor.ProjectTaskActionConvert";
			}
			return _ptar;
		}

		public override string Title
		{
			get { return "Convert: "; }
		}
		protected override void ExecuteInternal(ExpandedStringVariableStore variables)
		{
		}
		protected override void LoadFromMarkupInternal(MarkupTagElement tag)
		{
			MarkupTagElement tagInputDataFormatReference = (tag.Elements["InputDataFormatReference"] as MarkupTagElement);
			if (tagInputDataFormatReference != null)
			{
				MarkupAttribute attTypeName = tagInputDataFormatReference.Attributes["TypeName"];
				MarkupAttribute attTypeID = tagInputDataFormatReference.Attributes["TypeID"];
				if (attTypeName == null && attTypeID == null) throw new ArgumentNullException("Must specify at least one of 'TypeName' or 'TypeID'");


			}
			MarkupTagElement tagOutputDataFormatReference = (tag.Elements["OutputDataFormatReference"] as MarkupTagElement);
			if (tagOutputDataFormatReference != null)
			{
				MarkupAttribute attTypeName = tagOutputDataFormatReference.Attributes["TypeName"];
				MarkupAttribute attTypeID = tagOutputDataFormatReference.Attributes["TypeID"];
				if (attTypeName == null && attTypeID == null) throw new ArgumentNullException("Must specify at least one of 'TypeName' or 'TypeID'");


			}
		}
	}
}
