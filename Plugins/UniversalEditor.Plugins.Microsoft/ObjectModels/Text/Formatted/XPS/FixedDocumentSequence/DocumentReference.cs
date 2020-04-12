//
//  DocumentReference.cs - represents a FixedDocumentSequence document reference in an XML Paper Specification (XPS) document
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

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocumentSequence
{
	/// <summary>
	/// Represents a FixedDocumentSequence document reference in an XML Paper Specification (XPS) document.
	/// </summary>
	public class DocumentReference : ICloneable
	{
		public class DocumentReferenceCollection
			: System.Collections.ObjectModel.Collection<DocumentReference>
		{

		}

		private string mvarSource = String.Empty;
		public string Source { get { return mvarSource; } set { mvarSource = value; } }

		public object Clone()
		{
			DocumentReference clone = new DocumentReference();
			clone.Source = (mvarSource.Clone() as string);
			return clone;
		}
	}
}
