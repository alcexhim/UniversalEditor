//
//  OpenDocumentDataFormat.cs
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
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.FileSystem.ZIP;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Package;

namespace UniversalEditor.DataFormats.Package.OpenDocument
{
	public class OpenDocumentDataFormat : ZIPDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Clear();
				_dfr.Capabilities.Add(typeof(PackageObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

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
			package.FileSystem.CopyFrom(fsom);

			objectModels.Push(package);
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			if (package == null) throw new ObjectModelNotSupportedException();

			FileSystemObjectModel fsom = (package.FileSystem.Clone() as FileSystemObjectModel);
			{
				MemoryAccessor ma = new MemoryAccessor();
				Internal.ManifestXML.ManifestXMLDataFormat xml = new Internal.ManifestXML.ManifestXMLDataFormat();
				Internal.ManifestXML.ManifestXMLObjectModel manifest = new Internal.ManifestXML.ManifestXMLObjectModel();
				manifest.FileEntries.Add(new Internal.ManifestXML.ManifestFileEntry("/", "application/vnd.oasis.opendocument.text", "1.3"));
				Document.Save(manifest, xml, ma);

				fsom.AddFile("META-INF/manifest.xml", ma.ToArray());
			}

			objectModels.Push(fsom);

			base.BeforeSaveInternal(objectModels);
		}
	}
}
