//
//  ADFBlock.cs
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
namespace UniversalEditor.Plugins.Amiga.DataFormats.FileSystem.ADF.Internal
{
	public class ADFBlock
	{
		internal uint blockPrimaryType;
		internal uint headerKey;
		internal uint highSeq;
		internal uint hashTableSize;
		internal uint firstData;
		internal uint blockChecksum;
		internal uint[] hashTableEntries;
		internal uint bm_flag;
		internal uint[] bm_pages;
		internal uint bm_ext;
		internal uint r_days;
		internal uint r_mins;
		internal uint r_ticks;
		internal string filename;
		internal byte unused1;
		internal uint unused2;
		internal uint unused3;
		internal uint v_days;
		internal uint v_mins;
		internal uint v_ticks;
		internal uint c_days;
		internal uint c_mins;
		internal uint c_ticks;
		internal uint next_hash;
		internal uint parent_dir;
		internal uint extension;
		internal ADFDiskImageBlockSecondaryType sec_type;
	}
}
