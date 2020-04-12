//
//  FLACMetadataBlock.cs - provides metadata information about the FLAC file
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

using System.Collections.ObjectModel;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.FLAC.Internal
{
	/// <summary>
	/// Provides metadata information about the FLAC file.
	/// </summary>
	public class FLACMetadataBlock
	{
		public class FLACMetadataBlockCollection : Collection<FLACMetadataBlock>
		{
		}
	}
}
