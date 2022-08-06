//
//  DOCDataFormat.cs - provides a DataFormat to manipulate Microsoft Office Word DOC files
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

using System;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.CompoundDocument;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.DataFormats.Office;
using UniversalEditor.ObjectModels.CompoundDocument;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Office;
using UniversalEditor.ObjectModels.Text.Formatted;

namespace UniversalEditor.DataFormats.Text.Formatted.DOC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft Office Word DOC files.
	/// </summary>
	public class DOCDataFormat : MicrosoftOfficeDocumentDataFormat
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

		protected override string MainDocumentStreamName => "WordDocument";
		protected override ushort MainDocumentIdentifier => 0xA5EC;

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new MicrosoftOfficeDocumentObjectModel());
			base.BeforeLoadInternal(objectModels);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MicrosoftOfficeDocumentObjectModel fsom = (objectModels.Pop() as MicrosoftOfficeDocumentObjectModel);
			FormattedTextObjectModel ftom = (objectModels.Pop() as FormattedTextObjectModel);

			if (fsom.AssociationTypeId != "Word.Document.8")
			{
				throw new InvalidDataFormatException(String.Format("association type '{0}' invalid for Microsoft Word 97-2003 data format", fsom.AssociationTypeId));
			}

		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			FormattedTextObjectModel ftom = (objectModels.Pop() as FormattedTextObjectModel);
			CompoundDocumentObjectModel fsom = new CompoundDocumentObjectModel();


			objectModels.Push(fsom);
			base.BeforeSaveInternal(objectModels);
		}
	}
}
