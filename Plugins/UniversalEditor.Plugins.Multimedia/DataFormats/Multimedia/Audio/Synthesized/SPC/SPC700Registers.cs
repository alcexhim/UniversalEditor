//
//  SPC700Registers.cs - the registers available for an SPC700 audio file
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
	/// The registers available for an SPC700 audio file.
	/// </summary>
	public class SPC700Registers
	{
		/// <summary>
		/// Gets or sets the value of the PC register.
		/// </summary>
		/// <value>The value of the PC register.</value>
		public ushort fPC { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the A register.
		/// </summary>
		/// <value>The value of the A register.</value>
		public byte fA { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the X register.
		/// </summary>
		/// <value>The value of the X register.</value>
		public byte fX { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the Y register.
		/// </summary>
		/// <value>The value of the Y register.</value>
		public byte fY { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the PSW register.
		/// </summary>
		/// <value>The value of the PSW register.</value>
		public byte fPSW { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the SP register.
		/// </summary>
		/// <value>The value of the SP register.</value>
		public byte fSP { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the Reserved1 register.
		/// </summary>
		/// <value>The value of the Reserved1 register.</value>
		public ushort fReserved1 { get; set; } = 0;
	}
}
