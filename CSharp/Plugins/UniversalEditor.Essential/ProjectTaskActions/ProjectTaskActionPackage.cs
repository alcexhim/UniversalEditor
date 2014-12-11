using System;
using System.Collections.Generic;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.ProjectTaskActions
{
	public class ProjectTaskActionPackage : ProjectTaskAction
	{
		private DataFormatReference mvarDataFormatReference = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" /> compatible with the
		/// <see cref="UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel" /> in which to package the
		/// project files.
		/// </summary>
		public DataFormatReference DataFormatReference { get { return mvarDataFormatReference; } }

		private static ProjectTaskActionReference _ptar = null;
		public override ProjectTaskActionReference MakeReference()
		{
			if (_ptar == null)
			{
				_ptar = base.MakeReference();
				_ptar.ProjectTaskActionTypeID = new Guid("{527B7B07-FB0E-46F2-9EA8-0E93E3B21A14}");
				_ptar.ProjectTaskActionTypeName = "UniversalEditor.ProjectTaskActionPackage";
			}
			return _ptar;
		}

		private ExpandedString mvarOutputFileName = null;
		public ExpandedString OutputFileName { get { return mvarOutputFileName; } set { mvarOutputFileName = value; } }

		public override string Title
		{
			get { return "Package: "; }
		}
		protected override void ExecuteInternal(ExpandedStringVariableStore variables)
		{
			DataFormat df = mvarDataFormatReference.Create();

			string outputFileName = String.Empty;
			if (mvarOutputFileName != null) outputFileName = mvarOutputFileName.ToString(variables);
		}
		protected override void LoadFromMarkupInternal(MarkupTagElement tag)
		{
			MarkupTagElement tagDataFormatReference = (tag.Elements["DataFormatReference"] as MarkupTagElement);
			if (tagDataFormatReference != null)
			{
				MarkupAttribute attTypeName = tagDataFormatReference.Attributes["TypeName"];
				MarkupAttribute attTypeID = tagDataFormatReference.Attributes["TypeID"];
				if (attTypeName == null && attTypeID == null) throw new ArgumentNullException("Must specify at least one of 'TypeName' or 'TypeID'");


			}

			MarkupTagElement tagOutputFileName = (tag.Elements["OutputFileName"] as MarkupTagElement);
			if (tagOutputFileName != null) mvarOutputFileName = ExpandedString.FromMarkup(tagOutputFileName);
		}
	}
}
