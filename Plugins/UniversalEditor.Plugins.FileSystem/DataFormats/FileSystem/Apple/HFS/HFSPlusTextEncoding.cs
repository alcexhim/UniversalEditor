//
//  HFSPlusTextEncoding.cs - indicates the text encoding for an HFS+ filesystem
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

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	/// <summary>
	/// Indicates the text encoding for an HFS+ filesystem.
	/// </summary>
	public enum HFSPlusTextEncoding
	{
		MacRoman = 0,
		MacJapanese = 1,
		MacChineseTraditional = 2,
		MacKorean = 3,
		MacArabic = 4,
		MacHebrew = 5,
		MacGreek = 6,
		MacCyrillic = 7,
		MacDevanagari = 9,
		MacGurmukhi = 10,
		MacGujarati = 11,
		MacOriya = 12,
		MacBengali = 13,
		MacTamil = 14,
		MacTelugu = 15,
		MacKannada = 16,
		MacMalayalam = 17,
		MacSinhalese = 18,
		MacBurmese = 19,
		MacKhmer = 20,
		MacThai = 21,
		MacLaotian = 22,
		MacGeorgian = 23,
		MacArmenian = 24,
		MacChineseSimplified = 25,
		MacTibetan = 26,
		MacMongolian = 27,
		MacEthiopic = 28,
		MacCentralEurRoman = 29,
		MacVietnamese = 30,
		MacExtArabic = 31,
		MacSymbol = 33,
		MacDingbats = 34,
		MacTurkish = 35,
		MacCroatian = 36,
		MacIcelandic = 37,
		MacRomanian = 38,
		MacFarsi49 = 49,
		MacFarsi140 = 140,
		MacUkrainian48 = 48,
		MacUkrainian152 = 152
	}
}
