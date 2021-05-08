//
//  StringTable.cs - specify some localizable strings (idk where this is used today)
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

namespace UniversalEditor.Localization
{
	public static class StringTable
	{
		private static string mvarApplicationName = "Universal Editor";
		public static string ApplicationName { get { return mvarApplicationName; } }

		private static string mvarErrorDataFormatNotOpen = "The data format is not open.";
		public static string ErrorDataFormatNotOpen { get { return mvarErrorDataFormatNotOpen; } }

		private static string mvarErrorDataCorrupted = "The file is corrupted.";
		public static string ErrorDataCorrupted { get { return mvarErrorDataCorrupted; } }

		private static string mvarErrorFileNotFound = "The file could not be found.";
		public static string ErrorFileNotFound { get { return mvarErrorFileNotFound; } }
		private static string mvarErrorNotAnObjectModel = "The specified type is not an object model.";
		public static string ErrorNotAnObjectModel { get { return mvarErrorNotAnObjectModel; } }

		private static string mvarErrorObjectModelNull = "The object model must not be null.";
		public static string ErrorObjectModelNull { get { return mvarErrorObjectModelNull; } set { mvarErrorObjectModelNull = value; } }
	}
}
