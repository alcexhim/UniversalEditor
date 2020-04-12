//
//  ProjectTaskActionPackage.cs - represents a ProjectTaskAction that packages multiple files into a single FileSystemObjectModel
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

using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.ProjectTaskActions
{
	/// <summary>
	/// Represents a <see cref="ProjectTaskAction" /> that packages multiple files into a single
	/// <see cref="ObjectModels.FileSystem.FileSystemObjectModel"/>.
	/// </summary>
	public class ProjectTaskActionPackage : ProjectTaskAction
	{
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" /> compatible with the
		/// <see cref="ObjectModels.FileSystem.FileSystemObjectModel" /> in which to package the project files.
		/// </summary>
		public DataFormatReference DataFormatReference { get; } = null;

		private static ProjectTaskActionReference _ptar = null;
		protected override ProjectTaskActionReference MakeReferenceInternal()
		{
			if (_ptar == null)
			{
				_ptar = base.MakeReferenceInternal();
				_ptar.ProjectTaskActionTypeID = new Guid("{527B7B07-FB0E-46F2-9EA8-0E93E3B21A14}");
				_ptar.ProjectTaskActionTypeName = "UniversalEditor.ProjectTaskActionPackage";
			}
			return _ptar;
		}
		public ExpandedString OutputFileName { get; set; } = null;

		public override string Title
		{
			get { return "Package: "; }
		}
		protected override void ExecuteInternal(ExpandedStringVariableStore variables)
		{
			DataFormat df = DataFormatReference.Create();

			string outputFileName = String.Empty;
			if (OutputFileName != null) outputFileName = OutputFileName.ToString(variables);
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
			if (tagOutputFileName != null) OutputFileName = ExpandedString.FromMarkup(tagOutputFileName);
		}
	}
}
