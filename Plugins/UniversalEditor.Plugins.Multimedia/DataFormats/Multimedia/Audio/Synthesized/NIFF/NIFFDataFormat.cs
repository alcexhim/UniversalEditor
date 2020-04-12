//
//  NIFFDataFormat.cs - provides a DataFormat for manipulating synthesized audio in NIFF format
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

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.NIFF
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio in NIFF format.
	/// </summary>
	public class NIFFDataFormat : RIFFDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = new DataFormatReference(this.GetType());
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel riff = (objectModels.Pop() as ChunkedObjectModel);
			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);

			throw new InvalidDataFormatException();
		}
		protected override void BeforeSaveInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);
			ChunkedObjectModel riff = new ChunkedObjectModel();

			objectModels.Push(riff);
		}
	}
}
