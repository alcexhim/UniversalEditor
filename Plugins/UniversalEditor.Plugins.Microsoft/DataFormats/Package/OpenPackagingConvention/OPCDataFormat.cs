//
//  OPCDataFormat.cs - provides a DataFormat for manipulating Microsoft Open Packaging Convention documents
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
using UniversalEditor.DataFormats.FileSystem.ZIP;

using UniversalEditor.ObjectModels.Package;
using UniversalEditor.ObjectModels.Package.Relationships;

using UniversalEditor.DataFormats.Package.Relationships;
using UniversalEditor.ObjectModels.Package.ContentTypes;
using UniversalEditor.DataFormats.Package.ContentTypes;

namespace UniversalEditor.DataFormats.Package.OpenPackagingConvention
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Open Packaging Convention documents. This <see cref="DataFormat" /> may be inherited
	/// to implement <see cref="DataFormat" />s which handle file types based on the Microsoft Open Packaging Convention, such as DOCX, XLSX, and PPTX.
	/// </summary>
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

					RelationshipsObjectModel rels = file.GetObjectModel<RelationshipsObjectModel>(new OPCRelationshipsDataFormat());
					if (relatedFileName != null)
					{
						foreach (Relationship rel in rels.Relationships)
						{
							rel.Source = relatedFileName;
							package.Relationships.Add(rel);
						}
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
					ContentTypesObjectModel contentTypes = file.GetObjectModel<ContentTypesObjectModel>(new OPCContentTypesDataFormat());
					foreach (DefaultDefinition type in contentTypes.DefaultDefinitions)
					{
						package.DefaultContentTypes.Add(type);
					}
					foreach (OverrideDefinition type in contentTypes.OverrideDefinitions)
					{
						package.OverrideContentTypes.Add(type);
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
			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			FileSystemObjectModel fsom = (package.FileSystem.Clone() as FileSystemObjectModel);

			#region _rels
			{
				Folder fldr = new Folder();
				fldr.Name = "_rels";

				File _rels = new File();
				_rels.Name = ".rels";

				RelationshipsObjectModel rels = new RelationshipsObjectModel();
				foreach (Relationship rel in package.Relationships)
				{
					rels.Relationships.Add(rel);
				}
				_rels.SetObjectModel<RelationshipsObjectModel>(new OPCRelationshipsDataFormat(), rels);

				fldr.Files.Add(_rels);

				fsom.Folders.Add(fldr);
			}
			#endregion

			#region [Content_Types].xml
			{
				File file = new File();
				file.Name = "[Content_Types].xml";

				ContentTypesObjectModel contentTypes = new ContentTypesObjectModel();
				foreach (DefaultDefinition type in package.DefaultContentTypes)
				{
					contentTypes.DefaultDefinitions.Add(type);
				}
				foreach (OverrideDefinition type in package.OverrideContentTypes)
				{
					contentTypes.OverrideDefinitions.Add(type);
				}
				file.SetObjectModel<ContentTypesObjectModel>(new OPCContentTypesDataFormat(), contentTypes);

				fsom.Files.Add(file);
			}
			#endregion

			#region XPS-specific
			/*
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
			#region FixedDocumentSequence.fdseq
			{
				File file = new File ();
				file.Name = "FixedDocumentSequence.fdseq";
				fsom.Files.Add (file);
			}
			#endregion
			*/
			#endregion

			objectModels.Push(fsom);

			base.BeforeSaveInternal(objectModels);
		}
	}
}
