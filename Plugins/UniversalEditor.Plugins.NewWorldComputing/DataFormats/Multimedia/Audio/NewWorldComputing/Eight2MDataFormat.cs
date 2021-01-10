//
//  Eight2MDataFormat.cs - provides a DataFormat for manipulating audio in Heroes of Might and Magic II 82M format
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.NewWorldComputing
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating audio in Heroes of Might and Magic II 82M format.
	/// </summary>
	/// <remarks>
	///	The .82M files can be played with any sound editor capable of reading raw wave data at a
	///	rate of 22050Hz, 8-Bit Unsigned, Mono. Thanks to Terry Butler,
	/// http://www.terrybutler.co.uk/misc/heroes-of-might-and-magic-ii/ for his research on this
	/// file format.
	/// </remarks>
	public class Eight2MDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null) _dfr = base.MakeReferenceInternal();
			_dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			_dfr.Sources.Add("http://www.terrybutler.co.uk/misc/heroes-of-might-and-magic-ii/");
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			wave.Header.BitsPerSample = 8;
			wave.Header.ChannelCount = 1;
			wave.Header.SampleRate = 22050;
			wave.RawData = reader.ReadToEnd();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			if (wave.Header.BitsPerSample != 8 || wave.Header.ChannelCount != 1 || wave.Header.SampleRate != 22050)
			{
				// TODO: should we throw an exception or attempt to convert the audio (how?)
			}
			writer.WriteBytes(wave.RawData);
		}
	}
}
