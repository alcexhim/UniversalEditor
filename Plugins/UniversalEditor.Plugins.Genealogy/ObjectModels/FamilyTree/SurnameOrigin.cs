//
//  SurnameOrigin.cs
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
	public class SurnameOrigin : ICloneable
	{
		public static SurnameOrigin Patrilineal { get; } = new SurnameOrigin(new Guid("{e54fefde-6dee-42fa-bb17-acc78c4bf7e1}"), "Patrilineal");
		public static SurnameOrigin Matrilineal { get; } = new SurnameOrigin(new Guid("{25bce564-5785-4621-9990-5dc4e7ee9d1e}"), "Matrilineal");
		public static SurnameOrigin Unknown { get; } = new SurnameOrigin(Guid.Empty, "Unknown");

		private Guid id = Guid.Empty;
		public string Title { get; set; } = null;
		public SurnameOrigin(Guid id, string title)
		{
			this.id = id;
			Title = title;
		}

		public object Clone()
		{
			SurnameOrigin clone = new SurnameOrigin(id, Title);
			return clone;
		}

		public static SurnameOrigin Parse(string value)
		{
			if (value == null) return null;
			switch (value.ToLower())
			{
				case "matrilineal": return SurnameOrigin.Matrilineal;
				case "patrilineal": return SurnameOrigin.Patrilineal;
			}

			return new SurnameOrigin(Guid.Empty, value);
		}
	}
}
