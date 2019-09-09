//
//  BinaryObjectModel.cs
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
namespace UniversalEditor.ObjectModels.Binary
{
	public class BinaryObjectModel : ObjectModel
	{
		public byte[] Data { get; set; } = new byte[0];

		public override void Clear()
		{
			Data = new byte[0];
		}

		public override void CopyTo(ObjectModel where)
		{
			BinaryObjectModel clone = (where as BinaryObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			clone.Data = (Data.Clone() as byte[]);
		}
	}
}
