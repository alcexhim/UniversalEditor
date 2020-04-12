//
//  ProjectTaskActionConvert.cs - represents a ProjectTaskAction that converts an ObjectModel from an input DataFormat to an output DataFormat.
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Project;

namespace UniversalEditor.ProjectTaskActions
{
	/// <summary>
	/// Represents a <see cref="ProjectTaskAction" /> that converts an <see cref="ObjectModel" /> from an input <see cref="DataFormat" /> to an
	/// output <see cref="DataFormat" />.
	/// </summary>
	public class ProjectTaskActionConvert : ProjectTaskAction
	{
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" />
		/// compatible with the <see cref="ObjectModel" /> of the specified
		/// <see cref="ProjectFile" />.
		/// </summary>
		public DataFormatReference InputDataFormatReference { get; set; } = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" />
		/// compatible with the <see cref="ObjectModel" /> of the specified
		/// <see cref="ProjectFile" />.
		/// </summary>
		public DataFormatReference OutputDataFormatReference { get; set; } = null;
		/// <summary>
		/// The <see cref="ProjectFile" /> to be converted upon project build.
		/// </summary>
		public ProjectFile ProjectFile { get; set; } = null;

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
