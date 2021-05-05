//
//  DataSet.cs
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
	public class DataSet : ICloneable
	{

		public class DataSetCollection
			: System.Collections.ObjectModel.Collection<DataSet>
		{

		}

		public string Name { get; set; } = String.Empty;
		public int Order { get; set; } = 0;
		public Type DataType { get; set; } = null;
		public int Dimensions { get; set; }
		public int[] Sizes { get; set; } = null;

		public object Clone()
		{
			DataSet clone = new DataSet();
			clone.Name = Name.Clone() as string;
			clone.Order = Order;
			clone.DataType = DataType;
			clone.Dimensions = Dimensions;
			return clone;
		}

		private float?[][] fs = null;

		private void Init()
		{
			if (fs == null)
			{
				fs = new float?[Dimensions][];
				for (int i = 0; i < Dimensions; i++)
				{
					fs[i] = new float?[Sizes[i]];
				}
			}
		}
		public float? GetValue(int nDimension, int nIndex, float? defaultValue = null)
		{
			Init();
			return fs[nDimension][nIndex].GetValueOrDefault(defaultValue.GetValueOrDefault());
		}
		public void SetValue(int nDimension, int nIndex, float? value)
		{
			Init();
			fs[nDimension][nIndex] = value;
		}
	}
}
