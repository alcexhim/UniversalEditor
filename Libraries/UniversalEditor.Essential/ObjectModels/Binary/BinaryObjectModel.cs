//
//  BinaryObjectModel.cs - provides an ObjectModel for manipulating raw binary data
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

using System;
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Binary
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating raw binary data.
	/// </summary>
	public class BinaryObjectModel : ObjectModel
	{
		public byte[] Data { get; set; } = new byte[0];

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Binary" };
				_omr.Description = "Edits raw binary data";
			}
			return _omr;
		}
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

		protected override CriteriaResult[] FindInternal(CriteriaQuery query)
		{
			List<CriteriaResult> list = new List<CriteriaResult>();
			for (int i = 0; i < Data.Length; i++)
			{
				if ((i < Data.Length - 1) && (query.Check(_CriteriaObjects[0].Properties[0], BitConverter.ToUInt16(Data, i))))
				{
					list.Add(new CriteriaResult(new BinarySelection(new byte[] { Data[i], Data[i + 1] }, i, 2)));
				}
			}
			return list.ToArray();
		}

		private CriteriaObject[] _CriteriaObjects = null;
		protected override CriteriaObject[] GetCriteriaObjectsInternal()
		{
			if (_CriteriaObjects == null)
			{
				_CriteriaObjects = new CriteriaObject[]
				{
					new CriteriaObject("Data", new CriteriaProperty[]
					{
						new CriteriaProperty("Unsigned short", typeof(ushort)),
						new CriteriaProperty("Unsigned integer", typeof(uint)),
						new CriteriaProperty("Unsigned long", typeof(ulong)),
						new CriteriaProperty("String", typeof(string)),
						new CriteriaProperty("HexString", typeof(string))
					}),
				};
			}
			return _CriteriaObjects;
		}
	}
}
