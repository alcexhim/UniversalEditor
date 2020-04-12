//
//  ContactGender.cs - represents a gender in a ContactObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Contact
{
	/// <summary>
	/// Represents a gender in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactGender
	{
		private static ContactGender mvarMale = new ContactGender("Male", "Male");
		public static ContactGender Male { get { return Male; } }

		private static ContactGender mvarFemale = new ContactGender("Female", "Female");
		public static ContactGender Female { get { return Female; } }

		private static ContactGender mvarUnspecified = new ContactGender("Unspecified", "Unspecified");
		public static ContactGender Unspecified { get { return mvarUnspecified; } }

		private string mvarValue = String.Empty;
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public ContactGender(string value, string title)
		{
			mvarValue = value;
			mvarTitle = title;
		}

		public override string ToString()
		{
			return mvarTitle;
		}
	}
}
