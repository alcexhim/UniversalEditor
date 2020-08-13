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
		private static DataFormatReference _dfr = null;
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

		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			if (package == null) throw new ObjectModelNotSupportedException();

			FileSystemObjectModel fsom = (package.FileSystem.Clone() as FileSystemObjectModel);
			{
				MemoryAccessor ma = new MemoryAccessor();
				ma.Writer.WriteFixedLengthString("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<manifest:manifest xmlns:manifest=\"urn:oasis:names:tc:opendocument:xmlns:manifest:1.0\" manifest:version=\"1.2\" xmlns:loext=\"urn:org:documentfoundation:names:experimental:office:xmlns:loext:1.0\">\n <manifest:file-entry manifest:full-path=\"/\" manifest:version=\"1.2\" manifest:media-type=\"application/vnd.oasis.opendocument.presentation\"/>\n <manifest:file-entry manifest:full-path=\"Configurations2/\" manifest:media-type=\"application/vnd.sun.xml.ui.configuration\"/>\n <manifest:file-entry manifest:full-path=\"styles.xml\" manifest:media-type=\"text/xml\"/>\n <manifest:file-entry manifest:full-path=\"Pictures/100002010000042200000098867BFA9F3EB7C7AD.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"Pictures/1000020100000422000000B2B43F3309F5F5EA0B.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"Pictures/1000020100000422000000A2277504F30A810755.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"content.xml\" manifest:media-type=\"text/xml\"/>\n <manifest:file-entry manifest:full-path=\"settings.xml\" manifest:media-type=\"text/xml\"/>\n <manifest:file-entry manifest:full-path=\"Thumbnails/thumbnail.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"meta.xml\" manifest:media-type=\"text/xml\"/>\n</manifest:manifest>");
				fsom.AddFile("META-INF/manifest.xml", ma.ToArray());
			}

			objectModels.Push(fsom);

			base.BeforeSaveInternal(objectModels);
		}
	}
}
