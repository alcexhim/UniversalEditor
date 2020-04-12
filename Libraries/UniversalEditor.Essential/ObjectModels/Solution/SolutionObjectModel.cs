//
//  SolutionObjectModel.cs - provides an ObjectModel for manipulating a collection of ProjectObjectModels in a Solution
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

using UniversalEditor.ObjectModels.Project;

namespace UniversalEditor.ObjectModels.Solution
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating a collection of <see cref="ProjectObjectModel" />s in a Solution.
	/// </summary>
	public class SolutionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Solution";
			}
			return _omr;
		}

		public override void Clear()
		{
			Configuration.Clear();
			Projects.Clear();
			Title = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			SolutionObjectModel solution = (where as SolutionObjectModel);
			solution.Title = (Title.Clone() as string);
			foreach (ProjectObjectModel project in Projects)
			{
				solution.Projects.Add(project);
			}
			Configuration.CopyTo(solution.Configuration);
		}

		/// <summary>
		/// Gets a collection of <see cref="ProjectObjectModel" /> instances representing the projects contained within this solution.
		/// </summary>
		/// <value>The projects contained within this solution.</value>
		public ProjectObjectModel.ProjectObjectModelCollection Projects { get; } = new ProjectObjectModel.ProjectObjectModelCollection();
		/// <summary>
		/// Gets or sets the title of this <see cref="SolutionObjectModel" />.
		/// </summary>
		/// <value>The title of this <see cref="SolutionObjectModel" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets a <see cref="PropertyList.PropertyListObjectModel" /> containing configuration information for this <see cref="SolutionObjectModel" />.
		/// </summary>
		/// <value>The <see cref="PropertyList.PropertyListObjectModel" /> containing configuration information for this <see cref="SolutionObjectModel" />.</value>
		public PropertyList.PropertyListObjectModel Configuration { get; } = new PropertyList.PropertyListObjectModel();
	}
}
