//
//  CompoundDocumentObjectModel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.ObjectModels.CompoundDocument
{
	public class CompoundDocumentObjectModel : FileSystemObjectModel
	{
		private static ObjectModelReference _omr = null;

		public string UserType { get; set; }
		public CompoundDocumentClipboardFormat ClipboardFormat { get; set; }
		public string AssociationTypeId { get; set; }

		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = new ObjectModelReference(GetType());
				_omr.Path = new string[] { "Microsoft", "OLE", "OLE2 Compound Document" };
			}
			return _omr;
		}
	}
}
