//  
//  XPSDataFormat.cs
//  
//  Author:
//       beckermj <${AuthorEmail}>
// 
//  Copyright (c) 2014 beckermj
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
using UniversalEditor.DataFormats.FileSystem.ZIP;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.ObjectModels.Text.Formatted;

namespace UniversalEditor
{
	public class XPSDataFormat : ZIPDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Microsoft XPS document", new string[] { "*.xps", "*.oxps" });
			}
			return _dfr;
		}
		
		protected override void BeforeLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new FileSystemObjectModel());
		}
		protected override void AfterLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			FormattedTextObjectModel text = (objectModels.Pop() as FormattedTextObjectModel);
		}
		
		protected override void BeforeSaveInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			FormattedTextObjectModel text = (objectModels.Pop() as FormattedTextObjectModel);
			
			FileSystemObjectModel fsom = new FileSystemObjectModel();
			
			#region _rels
			{
				Folder fldr = new Folder();
				fldr.Name = "_rels";
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
