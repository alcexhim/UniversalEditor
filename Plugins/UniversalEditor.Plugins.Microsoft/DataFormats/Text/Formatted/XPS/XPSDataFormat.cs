//
//  XPSDataFormat.cs - provides a DataFormat to manipulate Microsoft XML Paper Specification (XPS) documents
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
using UniversalEditor.DataFormats.Package.OpenPackagingConvention;
using UniversalEditor.DataFormats.Text.Formatted.XPS.FixedDocument;
using UniversalEditor.DataFormats.Text.Formatted.XPS.FixedDocumentSequence;
using UniversalEditor.DataFormats.Text.Formatted.XPS.FixedPage;
using UniversalEditor.DataFormats.Text.Formatted.XPS.PrintTicket;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Package;
using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocument;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocumentSequence;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft XML Paper Specification (XPS) documents.
	/// </summary>
	public class XPSDataFormat : OPCDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private static System.Collections.Generic.Dictionary<XPSSchemaKey, string> Schemas = new System.Collections.Generic.Dictionary<XPSSchemaKey, string>();
		static XPSDataFormat()
		{
		}

		public XPSSchemaVersion SchemaVersion { get; set; } = XPSSchemaVersion.OpenXPS;

		protected override void BeforeLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new PackageObjectModel());
			base.BeforeLoadInternal(objectModels);
		}
		protected override void AfterLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			FormattedTextObjectModel text = (objectModels.Pop() as FormattedTextObjectModel);

			// we need to get the FixedRepresentation for the XPS document
			File[] files = package.GetFilesBySchema(XPSSchemas.GetSchema(SchemaVersion, XPSSchemaType.FixedRepresentation));

			// get the related print tickets
			File[] printTickets = package.GetFilesBySchema(XPSSchemas.GetSchema(SchemaVersion, XPSSchemaType.PrintTicket), "FixedDocumentSequence.fdseq");
			PrintTicketObjectModel printTicket = printTickets[0].GetObjectModel<PrintTicketObjectModel>(new PrintTicketXMLDataFormat());

			FixedDocumentSequenceObjectModel fdom = files[0].GetObjectModel<FixedDocumentSequenceObjectModel>(new FDSEQDataFormat());

			File fileFDOC = package.FileSystem.FindFile(fdom.DocumentReferences[0].Source.Substring(1));

			FixedDocumentObjectModel fdoc = fileFDOC.GetObjectModel<FixedDocumentObjectModel>(new FDOCDataFormat(SchemaVersion));


		}

		protected override void BeforeSaveInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			FormattedTextObjectModel text = (objectModels.Pop() as FormattedTextObjectModel);

			PackageObjectModel package = new PackageObjectModel();
			package.DefaultContentTypes.Add("fdseq", "application/vnd.ms-package.xps-fixeddocumentsequence+xml");
			package.DefaultContentTypes.Add("fdoc", "application/vnd.ms-package.xps-fixeddocument+xml");
			package.DefaultContentTypes.Add("fpage", "application/vnd.ms-package.xps-fixedpage+xml");
			package.DefaultContentTypes.Add("odttf", "application/vnd.ms-package.obfuscated-opentype");
			package.DefaultContentTypes.Add("xml", "application/vnd.ms-printing.printticket+xml");
			package.DefaultContentTypes.Add("JPG", "image/jpeg");

			FixedDocumentObjectModel fdoc = new FixedDocumentObjectModel();

			Folder fldDocuments = package.FileSystem.Folders.Add("Documents");
			Folder fldDocument1 = fldDocuments.Folders.Add("1");

			Folder fldDocument1Metadata = fldDocument1.Folders.Add("Metadata");

			for (int i = 0; i < 1; i++)
			{
				File fldDocument1MetadataPage1PT = new File();
				fldDocument1MetadataPage1PT.Name = "Page" + (i + 1).ToString() + "_PT.xml";

				PrintTicketObjectModel pt = new PrintTicketObjectModel();
				fldDocument1MetadataPage1PT.SetObjectModel<PrintTicketObjectModel>(new PrintTicketXMLDataFormat(), pt);

				File fldDocument1MetadataPage1Thumbnail = new File();
				fldDocument1MetadataPage1Thumbnail.Name = "Page" + (i + 1).ToString() + "_Thumbnail.JPG";
			}

			FPAGEDataFormat fpageDF = new FPAGEDataFormat();
			fpageDF.Generator = new XPSGenerator("Universal Editor", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
			fpageDF.SchemaVersion = SchemaVersion;

			objectModels.Push(package);
		}
	}
}
