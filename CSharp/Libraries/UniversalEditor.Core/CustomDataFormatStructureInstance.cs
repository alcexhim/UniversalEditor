//
//  CustomDataFormatStructureInstance.cs - an instance of a structure used in a CDF
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class CustomDataFormatStructureInstance
	{
		private CustomDataFormatStructure mvarStructure = null;
		public CustomDataFormatStructure Structure { get { return mvarStructure; } }

		private CustomDataFormatItem.CustomDataFormatItemReadOnlyCollection mvarItems = null;
		public CustomDataFormatItem.CustomDataFormatItemReadOnlyCollection Items { get { return mvarItems; } }

		internal CustomDataFormatStructureInstance(CustomDataFormatStructure structure)
		{
			mvarStructure = structure;

			List<CustomDataFormatItem> items = new List<CustomDataFormatItem>();
			foreach (CustomDataFormatItem item in structure.Items)
			{
				items.Add(item);
			}
			mvarItems = new CustomDataFormatItem.CustomDataFormatItemReadOnlyCollection(items);
		}
	}
}
