//
//  BinaryGrammarObjectModel.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalEditor.ObjectModels.BinaryGrammar.GrammarItems;

namespace UniversalEditor.ObjectModels.BinaryGrammar
{
	public class BinaryGrammarObjectModel : ObjectModel
	{
		public string Name { get; set; } = String.Empty;
		public string Author { get; set; } = String.Empty;
		public string FileExtension { get; set; } = String.Empty;
		public string UniversalTypeIdentifier { get; set; } = String.Empty;

		public string Description { get; set; } = String.Empty;

		public GrammarItemStructure InitialStructure { get; set; } = null;

		public GrammarItemStructure.GrammarItemStructureCollection Structures { get; } = new GrammarItemStructure.GrammarItemStructureCollection();

		public bool IsComplete { get; set; } = false;

		public override void Clear()
		{
			Name = String.Empty;
			Author = String.Empty;
			FileExtension = String.Empty;
			UniversalTypeIdentifier = String.Empty;
			Description = String.Empty;
			InitialStructure = null;
			Structures.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}
	}
}
