using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.DataFormats.FileSystem.ZIP;

using UniversalEditor.ObjectModels.Package;
using UniversalEditor.ObjectModels.Package.Relationships;

using UniversalEditor.DataFormats.Package.Relationships;
using UniversalEditor.ObjectModels.Package.ContentTypes;
using UniversalEditor.DataFormats.Package.ContentTypes;

namespace UniversalEditor.DataFormats.Package.OpenPackagingConvention
{
	public class OPCDataFormat : ZIPDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new FileSystemObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			
			File[] files = fsom.GetAllFiles();
			foreach (File file in files)
			{
				if (file.Name.EndsWith(".rels") && file.Parent.Name == "_rels")
				{
					string relatedFileName = null;
					string fn = System.IO.Path.GetFileName(file.Name);
					if (fn != ".rels")
					{
						relatedFileName = fn.Substring(0, fn.Length - ".rels".Length);
					}


					byte[] data = file.GetData();

					OPCRelationshipsDataFormat df = new OPCRelationshipsDataFormat();
					RelationshipsObjectModel rels = new RelationshipsObjectModel();
					Document.Load(rels, df, new MemoryAccessor(data));

					if (relatedFileName != null)
					{

					}
					else
					{
						foreach (Relationship rel in rels.Relationships)
						{
							package.Relationships.Add(rel);
						}
					}
				}
				else if (file.Name == "[Content_Types].xml" && file.Parent == null)
				{
					byte[] data = file.GetData();

					OPCContentTypesDataFormat df = new OPCContentTypesDataFormat();
					ContentTypesObjectModel contentTypes = new ContentTypesObjectModel();
					Document.Load(contentTypes, df, new MemoryAccessor(data));

					foreach (ContentType type in contentTypes.ContentTypes)
					{
						package.ContentTypes.Add(type);
					}
				}
				else
				{
					package.FileSystem.AddFile(file.Name, file.GetData());
				}
			}

			objectModels.Push(package);
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			FileSystemObjectModel fsom = new FileSystemObjectModel();
			
			#region _rels
			{
				Folder fldr = new Folder();
				fldr.Name = "_rels";

				File _rels = new File();
				_rels.Name = ".rels";

				fldr.Files.Add(_rels);

				fsom.Folders.Add(fldr);
			}
			#endregion
			#region Documents
			{
				Folder fldr = new Folder();
				fldr.Name = "Documents";
				fsom.Folders.Add(fldr);
			}
			#endregion
			#region Metadata
			{
				Folder fldr = new Folder();
				fldr.Name = "Metadata";
				fsom.Folders.Add(fldr);
			}
			#endregion
			#region [Content_Types].xml
			{
				File file = new File();
				file.Name = "[Content_Types].xml";
				fsom.Files.Add(file);
			}
			#endregion
			#region FixedDocumentSequence.fdseq
			{
				File file = new File();
				file.Name = "FixedDocumentSequence.fdseq";
				fsom.Files.Add(file);
			}
			#endregion

			objectModels.Push(fsom);
		}
	}
}
