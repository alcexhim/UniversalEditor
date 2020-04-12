//
//  MP3DataFormat.cs - provides a DataFormat for manipulating waveform audio in MPEG-II Layer 3 (MP3) format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MP3
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating waveform audio in MPEG-II Layer 3 (MP3) format.
	/// </summary>
	public class MP3DataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = objectModel as WaveformAudioObjectModel;
			Reader br = base.Accessor.Reader;
			string ID3 = br.ReadFixedLengthString(3);
			if (ID3 == "ID3")
			{
				int lz = br.ReadInt32();
			}
			else
			{
				br.Accessor.Seek(-3L, SeekOrigin.Current);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = objectModel as WaveformAudioObjectModel;
			Writer bw = base.Accessor.Writer;
			if (wave.Information.CustomProperties.Count > 0)
			{
				bw.WriteFixedLengthString("ID3");
			}
			bw.Flush();
		}
	}
}
