//
//  GrammarItem.cs - the abstract base class from which all binary grammar items derive
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.BinaryGrammar
{
	/// <summary>
	/// The abstract base class from which all binary grammar items derive.
	/// </summary>
	public class GrammarItem
	{
		public class GrammarItemCollection
			: System.Collections.ObjectModel.Collection<GrammarItem>
		{
		}

		public string ID { get; set; } = null;
		public string Name { get; set; } = null;
		public string Length { get; set; } = "0";

		public FixedValue.FixedValueCollection FixedValues { get; } = new FixedValue.FixedValueCollection();
	}
}
