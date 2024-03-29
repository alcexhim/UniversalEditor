//
//  ProjectObjectModel.cs - provides an ObjectModel for manipulating projects (collections of files and settings)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Project
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating projects (collections of files and settings).
	/// </summary>
	public class ProjectObjectModel : ObjectModel
	{
		public class ProjectObjectModelCollection
			: System.Collections.ObjectModel.Collection<ProjectObjectModel>
		{
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Project" };
				_omr.Description = "Stores a set of related files and folders with an accompanying configuration.";
			}
			return _omr;
		}

		public override void Clear()
		{
			Configuration.Clear();
			FileSystem.Clear();
			ID = Guid.Empty;
			ProjectTypes.Clear();
			References.Clear();
			RelativeFileName = String.Empty;
			Title = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			ProjectObjectModel clone = (where as ProjectObjectModel);
			Configuration.CopyTo(clone.Configuration);
			FileSystem.CopyTo(clone.FileSystem);
			clone.ID = ID;
			for (int i = 0; i < ProjectTypes.Count; i++)
			{
				clone.ProjectTypes.Add(ProjectTypes[i]);
			}
			foreach (Reference _ref in References)
			{
				clone.References.Add(_ref);
			}
			clone.RelativeFileName = (RelativeFileName.Clone() as string);
			clone.Title = (Title.Clone() as string);
		}

		public string BasePath { get; set; } = null;

		/// <summary>
		/// Gets or sets the globally-unique identifier (GUID) for this <see cref="ProjectObjectModel" />.
		/// </summary>
		/// <value>The globally-unique identifier (GUID) for this <see cref="ProjectObjectModel" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the title of this <see cref="ProjectObjectModel" />.
		/// </summary>
		/// <value>The title of this <see cref="ProjectObjectModel" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets a <see cref="PropertyListObjectModel" /> representing the configuration settings for this <see cref="ProjectObjectModel" />.
		/// </summary>
		/// <value>The configuration settings for this <see cref="ProjectObjectModel" />.</value>
		public PropertyListObjectModel Configuration { get; } = new PropertyListObjectModel();
		/// <summary>
		/// Gets a collection of <see cref="Reference" /> instances representing the references to other projects and libraries used by this <see cref="ProjectObjectModel" />.
		/// </summary>
		/// <value>The references to other projects and libraries used by this <see cref="ProjectObjectModel" />.</value>
		public Reference.ReferenceCollection References { get; } = new Reference.ReferenceCollection();
		/// <summary>
		/// Gets a <see cref="ProjectFileSystem" /> containing the <see cref="ProjectFile" />s and <see cref="ProjectFolder" />s referenced by this <see cref="ProjectObjectModel" />.
		/// </summary>
		/// <value>The file system containing the <see cref="ProjectFile" />s and <see cref="ProjectFolder" />s referenced by this <see cref="ProjectObjectModel" />.</value>
		public ProjectFileSystem FileSystem { get; } = new ProjectFileSystem();
		/// <summary>
		/// Gets a collection of <see cref="ProjectType" />s containing common settings and build actions shared between multiple projects of the
		/// same <see cref="ProjectType" />.
		/// </summary>
		/// <value>A collection of <see cref="ProjectType" />s containing common settings and build actions shared between multiple projects of the
		/// same <see cref="ProjectType" />.</value>
		public ProjectType.ProjectTypeCollection ProjectTypes { get; } = new ProjectType.ProjectTypeCollection();
		/// <summary>
		/// Gets or sets the relative path to the <see cref="ProjectObjectModel" />.
		/// </summary>
		/// <value>The relative path to the <see cref="ProjectObjectModel" />.</value>
		public string RelativeFileName { get; set; } = String.Empty;

		private System.Collections.Generic.Dictionary<Guid, object> _projectSettings = new System.Collections.Generic.Dictionary<Guid, object>();
		public object GetProjectSetting(Guid id, object defaultValue = null)
		{
			if (_projectSettings.ContainsKey(id))
			{
				return _projectSettings[id];
			}
			return defaultValue;
		}
		public void SetProjectSetting(Guid id, object value)
		{
			_projectSettings[id] = value;
		}
	}
}
