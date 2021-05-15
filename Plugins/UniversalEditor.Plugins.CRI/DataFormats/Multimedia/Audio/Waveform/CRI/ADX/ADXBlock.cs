//
//  ADXBlock.cs - provides metadata information for a block in an ADX-encoded audio file
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.CRI.ADX
{
	/// <summary>
	/// Provides metadata information for a block in an ADX-encoded audio file.
	/// </summary>
	public class ADXBlock
	{
		/// <summary>
		/// The scale is a 16bit unsigned integer (big-endian like the header) which is essentially the amplification of all the samples in that block.
		/// </summary>
		public ushort Scale { get; set; } = 0;
	}
}
