//
//  JSONArrayField.cs - represents an array field in a JSON document
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

namespace UniversalEditor.ObjectModels.JSON.Fields
{
	/// <summary>
	/// Represents an array field in a JSON document.
	/// </summary>
	public class JSONArrayField : JSONField
	{
		/// <summary>
		/// Gets a collection of <see cref="string" /> instances representing the values in the array.
		/// </summary>
		/// <value>The values in the array.</value>
		public System.Collections.Specialized.StringCollection Values { get; } = new System.Collections.Specialized.StringCollection();

		public override object Clone()
		{
			JSONArrayField clone = new JSONArrayField();
			foreach (string value in Values)
			{
				clone.Values.Add(value.Clone() as string);
			}
			return clone;
		}
	}
}
