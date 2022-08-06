//
//  SUODataFormat.cs - provides a DataFormat for manipulating Microsoft Visual Studio solution user options files
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
using MBS.Framework;
using MBS.Framework.Settings;
using UniversalEditor.DataFormats.CompoundDocument;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.ObjectModels.CompoundDocument;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.Microsoft.VisualStudio
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Visual Studio solution user options files.
	/// </summary>
	public class SUODataFormat : CompoundDocumentDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType(), base.MakeReferenceInternal());
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new CompoundDocumentObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			CompoundDocumentObjectModel fsom = (objectModels.Pop() as CompoundDocumentObjectModel);
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			CompoundDocumentObjectModel fsom = new CompoundDocumentObjectModel();



			objectModels.Push(fsom);
		}
	}
}
