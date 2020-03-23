//
//  ExtensionMethods.cs
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
using System.Text;
using MBS.Framework.UserInterface.Dialogs;

namespace UniversalEditor.UserInterface
{
	public static class ExtensionMethods
	{
		public static void AddFileNameFilterFromAssociations(this FileDialog dialog, string title, Association association)
		{
			AddFileNameFilterFromAssociations(dialog, title, new Association[] { association });
		}
		public static void AddFileNameFilterFromAssociations(this FileDialog dialog, string title, Association[] associations)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Association assoc in associations)
			{
				for (int i = 0; i < assoc.Filters.Count; i++)
				{
					for (int j = 0; j < assoc.Filters[i].FileNameFilters.Count; j++)
					{
						sb.Append(assoc.Filters[i].FileNameFilters[j]);
						if (j < assoc.Filters[i].FileNameFilters.Count - 1)
							sb.Append("; ");
					}

					if (i < assoc.Filters.Count - 1)
						sb.Append("; ");
				}
			}
			dialog.FileNameFilters.Add(title, sb.ToString());
		}
	}
}
