//
//  CustomDataFormatStructure.cs - declaration of a structure used in a CDF
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
	public class CustomDataFormatStructure
	{
		public class CustomDataFormatStructureCollection
			: System.Collections.ObjectModel.Collection<CustomDataFormatStructure>
		{
			public CustomDataFormatStructure this[Guid id]
			{
				get
				{
					foreach (CustomDataFormatStructure item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		public string Title { get; set; }

		private CustomDataFormatItem.CustomDataFormatItemCollection mvarItems = new CustomDataFormatItem.CustomDataFormatItemCollection();
		public CustomDataFormatItem.CustomDataFormatItemCollection Items
		{
			get { return mvarItems; }
		}

		public CustomDataFormatStructureInstance CreateInstance()
		{
			CustomDataFormatStructureInstance inst = new CustomDataFormatStructureInstance(this);
			return inst;
		}
	}
}
