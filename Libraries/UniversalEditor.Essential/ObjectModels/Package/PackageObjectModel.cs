//
//  PackageObjectModel.cs - provides an ObjectModel to manipulate Open Packaging Convention documents
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

using System.Collections.Generic;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Package.ContentTypes;
using UniversalEditor.ObjectModels.Package.Relationships;

namespace UniversalEditor.ObjectModels.Package
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> to manipulate Open Packaging Convention documents. This <see cref="ObjectModel" /> may be inherited to provide
	/// additional functionality for document types based on the Open Packaging Convention standards.
	/// </summary>
	public class PackageObjectModel : ObjectModel
	{
		public DefaultDefinition.DefaultDefinitionCollection DefaultContentTypes { get; } = new DefaultDefinition.DefaultDefinitionCollection ();
		public OverrideDefinition.OverrideDefinitionCollection OverrideContentTypes { get; } = new OverrideDefinition.OverrideDefinitionCollection ();

		private Relationship.RelationshipCollection mvarRelationships = new Relationship.RelationshipCollection();
		public Relationship.RelationshipCollection Relationships { get { return mvarRelationships; } }

		public override void Clear()
		{
			DefaultContentTypes.Clear();
			OverrideContentTypes.Clear ();
			mvarRelationships.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			PackageObjectModel clone = (where as PackageObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (DefaultDefinition item in DefaultContentTypes)
			{
				clone.DefaultContentTypes.Add(item.Clone() as DefaultDefinition);
			}
			foreach (OverrideDefinition item in OverrideContentTypes)
			{
				clone.OverrideContentTypes.Add(item.Clone() as OverrideDefinition);
			}
			foreach (Relationship item in mvarRelationships)
			{
				clone.Relationships.Add(item.Clone() as Relationship);
			}
		}

		private FileSystemObjectModel mvarFileSystem = new FileSystemObjectModel();
		public FileSystemObjectModel FileSystem { get { return mvarFileSystem; } }

		public File[] GetFilesBySchema(string schema, string relationshipSource = null)
		{
			List<File> files = new List<File>();
			Relationship[] rels = mvarRelationships.GetBySchema(schema, relationshipSource);
			foreach (Relationship rel in rels)
			{
				if (rel.Target.StartsWith("/"))
				{
					string target = rel.Target.Substring(1);

					File file = mvarFileSystem.FindFile(target);
					if (file != null) files.Add(file);
				}
			}
			return files.ToArray();
		}
	}
}
