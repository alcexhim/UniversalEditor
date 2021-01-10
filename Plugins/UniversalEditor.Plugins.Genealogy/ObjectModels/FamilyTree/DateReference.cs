//
//  DateReference.cs
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
	public class DateReference
	{
		private DateTime _val = DateTime.Now;
		private string _valstr = null;

		private DateReference()
		{
		}

		public DateReference(Date date)
		{
			StartDate = date;
			EndDate = null;
			Type = DateType.Exact;
		}
		public DateReference(Date startDate, Date endDate, DateType type)
		{
			StartDate = startDate;
			EndDate = endDate;
			Type = type;
		}

		public DateType Type { get; set; } = DateType.Exact;

		public Date StartDate { get; set; } = null;
		public Date EndDate { get; set; } = null;

		public static DateReference Parse(string value)
		{
			// hack: this is for english locales only
			DateReference date = new DateReference();
			if (value.Contains("from ") && value.Contains(" to "))
			{
				date.Type = DateType.Range;
			}
			else if (value.StartsWith("about "))
			{
				date.Type = DateType.Approximate;
				date.StartDate = Date.Parse(value.Substring("about ".Length));
			}
			else if (value.Contains("between ") && value.Contains(" and "))
			{
				date.Type = DateType.Range2;
			}
			else
			{
				date.Type = DateType.Exact;
				date.StartDate = Date.Parse(value);
				date.EndDate = null;
			}
			return date;
		}
	}
}
