//
//  JSONObjectModel.cs
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
namespace UniversalEditor.ObjectModels.JSON
{
	public class JSONObjectModel : ObjectModel
	{
		private JSONObject.JSONObjectCollection mvarObjects = new JSONObject.JSONObjectCollection();
		public JSONObject.JSONObjectCollection Objects { get { return mvarObjects; } }

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
