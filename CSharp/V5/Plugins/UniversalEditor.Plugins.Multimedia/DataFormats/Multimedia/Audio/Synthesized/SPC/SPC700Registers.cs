// Universal Editor DataFormat for loading SPC700 synthesized audio files
// Copyright (C) 2014  Mike Becker's Software
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
	/// Description of SPC700Registers.
	/// </summary>
	public class SPC700Registers
	{
		private ushort _fPC = 0;
		public ushort fPC { get { return _fPC; } set { _fPC = value; } }
		
		private byte _fA = 0;
		public byte fA { get { return _fA; } set { _fA = value; } }
		
		private byte _fX = 0;
		public byte fX { get { return _fX; } set { _fX = value; } }
		
		private byte _fY = 0;
		public byte fY { get { return _fY; } set { _fY = value; } }
		
		private byte _fPSW = 0;
		public byte fPSW { get { return _fPSW; } set { _fPSW = value; } }
		
		private byte _fSP = 0;
		public byte fSP { get { return _fSP; } set { _fSP = value; } }
		
		private ushort _fReserved1 = 0;
		public ushort fReserved1 { get { return _fReserved1; } set { _fReserved1 = value; } }
	}
}
