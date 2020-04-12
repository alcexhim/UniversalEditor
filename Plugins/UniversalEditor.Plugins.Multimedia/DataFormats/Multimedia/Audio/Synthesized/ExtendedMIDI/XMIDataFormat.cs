//
//  XMIDataFormat.cs - provides a DataFormat for manipulating synthesized audio files in Extended MIDI (XMI) format
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
using UniversalEditor.DataFormats.Chunked.RIFF;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.ExtendedMIDI
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio files in Extended MIDI (XMI) format.
	/// </summary>
	public class XMIDataFormat : RIFFDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(this.GetType());
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.Bootstrap);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);
			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);
			ChunkedObjectModel chunked = new ChunkedObjectModel();



			objectModels.Push(chunked);
		}
	}
}
