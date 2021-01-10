//
//  PlaceName.cs
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
namespace UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree
{
	public class PlaceName : ICloneable
	{
		public class PlaceNameCollection
			: System.Collections.ObjectModel.Collection<PlaceName>
		{

		}

		public string Value { get; set; } = null;
		public DateReference DateSpan { get; set; } = null;
		public string Language { get; set; } = null;

		public PlaceName()
		{
		}

		public object Clone()
		{
			PlaceName clone = new PlaceName();
			clone.Value = (Value?.Clone() as string);
			clone.DateSpan = DateSpan;
			clone.Language = (Language?.Clone() as string);
			return clone;
		}
	}
}
