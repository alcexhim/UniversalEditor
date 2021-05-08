//
//  SystemRecordType.cs - internal enum specifying the type of SYSTEMRECORD present in a WinHelp file
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

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Internal enum specifying the type of SYSTEMRECORD present in a WinHelp file.
	/// </summary>
	internal enum SystemRecordType : ushort
	{
		/// <summary>
		/// Help file title
		/// </summary>
		Title = 1,
		/// <summary>
		/// Copyright notice shown in AboutBox
		/// </summary>
		Copyright = 2,
		/// <summary>
		/// Topic offset of starting topic
		/// </summary>
		Contents = 3,
		/// <summary>
		/// All macros executed on opening
		/// </summary>
		Config = 4,
		/// <summary>
		/// See WIN31WH on icon file format
		/// </summary>
		Icon = 5,
		/// <summary>
		/// Windows defined in the HPJ-file; Viewer 2.0 Windows defined in MVP-file
		/// </summary>
		Window = 6,
		/// <summary>
		/// The Citation printed
		/// </summary>
		Citation = 8,
		/// <summary>
		/// Language ID, Windows 95 (HCW 4.00)
		/// </summary>
		LanguageID = 9,
		/// <summary>
		/// CNT file name, Windows 95 (HCW 4.00)
		/// </summary>
		TableOfContents = 10,
		/// <summary>
		/// Charset, Windows 95 (HCW 4.00)
		/// </summary>
		CharacterSet = 11,
		/// <summary>
		/// Default dialog font, Windows 95 (HCW 4.00); Multimedia Help Files dtypes
		/// </summary>
		DefaultDialogFontOrFTIndex = 12,
		/// <summary>
		/// Defined GROUPs, Multimedia Help File
		/// </summary>
		Groups = 13,
		/// <summary>
		/// Separators, Windows 95 (HCW 4.00); Multimedia Help Files
		/// </summary>
		IndexSeparatorsOrKeyIndex = 14,
		/// <summary>
		/// Defined language, Multimedia Help Files
		/// </summary>
		Language = 18,
		/// <summary>
		/// Defined DLLMAPS, Multimedia Help Files
		/// </summary>
		DLLMaps = 19
	}
}
