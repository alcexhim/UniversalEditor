//
//  SPC700Memory.cs - the initial memory allocations for an SPC700 synthesized audio file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2014-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	/// <summary>
	/// The initial memory allocations for an SPC700 synthesized audio file.
	/// </summary>
	public class SPC700Memory
	{
		/// <summary>
		/// Gets or sets the 65536 bytes of RAM.
		/// </summary>
		/// <value>The 65536 bytes of RAM.</value>
		public byte[] fRAM { get; set; } = new byte[65536];
		/// <summary>
		/// Gets or sets the 128 bytes of DSP.
		/// </summary>
		/// <value>The 128 bytes of DSP.</value>
		public byte[] fDSP { get; set; } = new byte[128];
		/// <summary>
		/// Gets or sets the 64 bytes of unused.
		/// </summary>
		/// <value>The 64 bytes of unused.</value>
		public byte[] fUnused { get; set; } = new byte[64];
		/// <summary>
		/// Gets or sets the 64 bytes of extra.
		/// </summary>
		/// <value>The 64 bytes of extra.</value>
		public byte[] fExtra { get; set; } = new byte[64];
	}
}
