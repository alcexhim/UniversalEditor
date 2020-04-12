//
//  REX2DataFormat.cs - provides a DataFormat for manipulating waveform audio in REX2 format
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

using System.Collections.Generic;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.REX2
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating waveform audio in REX2 format.
	/// </summary>
	public class REX2DataFormat : RIFFDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private string[] mvarRIFFTags = new string[] { "CAT " };
		public override string[] RIFFTagsLittleEndian { get { return mvarRIFFTags; } }

		protected override bool IsObjectModelSupported(ObjectModel objectModel)
		{
			if (objectModel is ChunkedObjectModel)
			{
				ChunkedObjectModel riff = (objectModel as ChunkedObjectModel);
				// TODO: Figure this out
			}
			return base.IsObjectModelSupported(objectModel);
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			ChunkedObjectModel rom = (objectModels.Pop() as ChunkedObjectModel);
			WaveformAudioObjectModel wave = (objectModels.Pop() as WaveformAudioObjectModel);
		}
	}
}
