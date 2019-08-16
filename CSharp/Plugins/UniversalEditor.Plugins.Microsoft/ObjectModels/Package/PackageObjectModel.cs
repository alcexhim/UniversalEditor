using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Package.ContentTypes;
using UniversalEditor.ObjectModels.Package.Relationships;

namespace UniversalEditor.ObjectModels.Package
{
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
