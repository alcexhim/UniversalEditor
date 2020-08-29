//
//  DataSetObjectModel.cs
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
namespace UniversalEditor.Plugins.Scientific.ObjectModels.DataSetCollection
{
	public class DataSetCollectionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Data set collection";
			}
			return _omr;
		}

		public DataSet.DataSetCollection DataSets { get; } = new DataSet.DataSetCollection();

		public override void Clear()
		{
			DataSets.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			for (int i = 0; i < DataSets.Count; i++)
			{
				DataSets.Add(DataSets[i].Clone() as DataSet);
			}
		}
	}
}
