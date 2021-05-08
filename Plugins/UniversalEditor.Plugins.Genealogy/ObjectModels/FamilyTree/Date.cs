//
//  Date.cs
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
	public class Date
	{
		public int? Year { get; set; } = null;
		public int? Month { get; set; } = null;
		public int? Day { get; set; } = null;

		public static Date Parse(string value)
		{
			Date date = new Date();
			if (value.Length == 10 && value.Contains("-"))
			{
				// yyyy-mm-dd format
				string[] parts = value.Split(new char[] { '-' });
				if ((parts.Length == 3 && (parts[0].Length == 4 && parts[1].Length > 0 && parts[1].Length <= 2 && parts[2].Length > 0 && parts[2].Length <= 2)))
				{
					if (int.TryParse(parts[1], out int month) && int.TryParse(parts[2], out int day))
					{
						if (month != 0)
							date.Month = month;
						if (day != 0)
							date.Day = day;
					}
					if (parts[0] == "????")
					{
						date.Year = null;
					}
					else if (int.TryParse(parts[0], out int year))
					{
						date.Year = year;
					}
				}
				else
				{
					throw new FormatException("must be in yyyy-mm-dd format");
				}
			}
			else if (value.Length == 4)
			{
				if (int.TryParse(value, out int year))
				{
					date.Year = year;
				}
				else
				{
					throw new FormatException("single four-digit component date must be an integer");
				}
			}
			return date;
		}
	}
}
