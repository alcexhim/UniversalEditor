// one line to give the program's name and an idea of what it does.
// Copyright (C) yyyy  name of author
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	/// <summary>
	/// Description of SPC700Memory.
	/// </summary>
	public class SPC700Memory
	{
		private byte[] _fRAM = new byte[65536];
		public byte[] fRAM { get { return _fRAM; } set { _fRAM = value; } }
		
		private byte[] _fDSP = new byte[128];
		public byte[] fDSP { get { return _fDSP; } set { _fDSP = value; } }
		
		private byte[] _fUnused = new byte[64];
		public byte[] fUnused { get { return _fUnused; } set { _fUnused = value; } }
		
		private byte[] _fExtra = new byte[64];
		public byte[] fExtra { get { return _fExtra; } set { _fExtra = value; } }
	}
}
