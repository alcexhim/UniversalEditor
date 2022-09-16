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

using MBS.Framework;

namespace UniversalEditor.Localization
{
	public static class StringTable
	{
		public static string ApplicationName { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Title", null) ?? "Universal Editor"; } }

		public static string ErrorDataFormatNotOpen { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Errors.DataFormatNotOpen", null) ?? "The data format is not open."; } }
		public static string ErrorDataFormatInvalid { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Errors.DataFormatInvalid", null) ?? "The data format is invalid."; } }
		public static string ErrorDataCorrupted { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Errors.DataCorrupted", null) ?? "The file is corrupted."; } }
		public static string ErrorFileNotFound { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Errors.FileNotFound", null) ?? "The file could not be found."; } }
		public static string ErrorNotAnObjectModel { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Errors.NotAnObjectModel", null) ?? "The specified type is not an object model."; } }
		public static string ErrorObjectModelNull { get { return Application.Instance?.DefaultLanguage?.GetStringTableEntry("Application.Errors.ObjectModelNull", null) ?? "The object model must not be null."; } }
	}
}
