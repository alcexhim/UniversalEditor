//
//  MoostaCommon.cs - provides common functionality for Moosta OMP
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

namespace UniversalEditor.Plugins.Moosta
{
	/// <summary>
	/// Indicates the type of file being manipulated by Moosta OMP.
	/// </summary>
	[Flags()]
	public enum MoostaFileType
	{
		None = 0,
		Mcha = 1,
		Dmcha = 2,
		Mstg = 4,
		Dmstg = 8,
		PMD = 16,
		PMX = 32,
		MoPkg = 64,
		DMoPkg = 128,
		CsProj = 256,
		CsScene = 512,
		CCamW = 1024,
		OMM = 2048
	}
	/// <summary>
	/// Provides common functionality for Moosta OMP.
	/// </summary>
	public static class Common
	{
		public static MoostaFileType CheckMoostaFileType(byte[] array, out int size)
		{
			size = 0;
			if (array[0] == 77 && array[1] == 99 && array[2] == 104 && array[3] == 97)
			{
				size = 4;
				return MoostaFileType.Mcha;
			}
			if (array[9] == 100 && array[10] == 77 && array[11] == 99 && array[12] == 104 && array[13] == 97)
			{
				size = 14;
				return MoostaFileType.Dmcha;
			}
			if (array[0] == 77 && array[1] == 115 && array[2] == 116 && array[3] == 103)
			{
				size = 4;
				return MoostaFileType.Mstg;
			}
			if (array[9] == 100 && array[10] == 77 && array[11] == 115 && array[12] == 116 && array[13] == 103)
			{
				size = 14;
				return MoostaFileType.Dmstg;
			}
			/*
			if (array[0] == 80 && array[1] == 109 && array[2] == 100)
			{
				size = 3;
				return MoostaFileType.PMD;
			}
			if (array[0] == 80 && array[1] == 77 && array[2] == 88)
			{
				size = 3;
				return MoostaFileType.PMX;
			}
			*/
			if (array[0] == 77 && array[1] == 111 && array[2] == 80 && array[3] == 107 && array[4] == 103)
			{
				size = 5;
				return MoostaFileType.MoPkg;
			}
			if (array[9] == 100 && array[10] == 77 && array[11] == 112 && array[12] == 107 && array[13] == 103)
			{
				size = 14;
				return MoostaFileType.DMoPkg;
			}
			if (array[0] == 99 && array[1] == 115 && array[2] == 80 && array[3] == 114 && array[4] == 111 && array[5] == 106)
			{
				size = 6;
				return MoostaFileType.CsProj;
			}
			if (array[0] == 99 && array[1] == 115 && array[2] == 83 && array[3] == 99 && array[4] == 101 && array[5] == 110 && array[6] == 101)
			{
				size = 7;
				return MoostaFileType.CsScene;
			}
			if (array[0] == 99 && array[1] == 99 && array[2] == 97 && array[3] == 109 && array[4] == 119)
			{
				size = 5;
				return MoostaFileType.CCamW;
			}
			if (array[0] == 111 && array[1] == 109 && array[2] == 109)
			{
				size = 3;
				return MoostaFileType.OMM;
			}
			return MoostaFileType.None;
		}
		public static bool CheckMoostaFileType(byte[] signature, MoostaFileType type, out int size)
		{
			return (type == CheckMoostaFileType(signature, out size));
		}
	}
}
