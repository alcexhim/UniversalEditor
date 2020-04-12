//
//  ADXVersion.cs - indicates the version of the ADX codec in use
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
	/// Indicates the version of the ADX codec in use.
	/// </summary>
	public enum ADXVersion : byte
	{
		ADXVersion3DifferentDecoder = 0x02,
		ADXVersion3 = 0x03,
		ADXVersion4 = 0x04,
		ADXVersion4WithoutLooping = 0x05
	}
}
