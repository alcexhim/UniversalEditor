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
using UniversalEditor.DataFormats.Package.OpenPackagingConvention;
using UniversalEditor.DataFormats.Text.Formatted.XPS.FixedDocumentSequence;
using UniversalEditor.DataFormats.Text.Formatted.XPS.PrintTicket;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Package;
using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocumentSequence;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
	public class XPSDataFormat : OPCDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		
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
			File[] files = package.GetFilesBySchema("http://schemas.microsoft.com/xps/2005/06/fixedrepresentation");
			
			// get the related print tickets
			File[] printTickets = package.GetFilesBySchema("http://schemas.microsoft.com/xps/2005/06/printticket", "FixedDocumentSequence.fdseq");
			PrintTicketObjectModel printTicket = printTickets[0].GetObjectModel<PrintTicketObjectModel>(new PrintTicketXMLDataFormat());

			FixedDocumentSequenceObjectModel fdom = files[0].GetObjectModel<FixedDocumentSequenceObjectModel>(new FDSEQDataFormat());
			
			File fdoc = package.FileSystem.FindFile(fdom.DocumentReferences[0].Source.Substring(1));

			// FixedDocument fdoc = file.GetObjectModel<FixedDocumentObjectModel>(new FDOCDataFormat());
		}
		
		protected override void BeforeSaveInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			FormattedTextObjectModel text = (objectModels.Pop() as FormattedTextObjectModel);

			PackageObjectModel package = new PackageObjectModel();
			
			objectModels.Push(package);
		}
	}
}
