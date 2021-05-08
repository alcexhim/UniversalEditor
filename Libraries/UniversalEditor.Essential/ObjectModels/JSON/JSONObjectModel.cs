//
//  JSONObjectModel.cs - provides an ObjectModel for manipulating data in JavaScript Object Notation (JSON) format
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

namespace UniversalEditor.ObjectModels.JSON
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating data in JavaScript Object Notation (JSON) format.
	/// </summary>
	public class JSONObjectModel : ObjectModel
	{
		/// <summary>
		/// Gets a collection of <see cref="JSONObject" /> instances representing the objects in this <see cref="JSONObjectModel" /> document.
		/// </summary>
		/// <value>The objects.</value>
		public JSONObject.JSONObjectCollection Objects { get; } = new JSONObject.JSONObjectCollection();

		public override void Clear()
		{
			Objects.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			JSONObjectModel clone = (where as JSONObjectModel);
			if (clone == null)
				throw new ObjectModelNotSupportedException();

			foreach (JSONObject obj in Objects)
			{
				clone.Objects.Add(obj.Clone() as JSONObject);
			}
		}
	}
}
