//
//  VPRProjectZIPDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.FileSystem.ZIP;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.Plugins.Vocaloid.DataFormats.Multimedia.Audio.Synthesized.Vocaloid.VPR
{
	public class VPRProjectZIPDataFormat : ZIPDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new FileSystemObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			ObjectModel whatever = objectModels.Pop();

			if (whatever is SynthesizedAudioObjectModel)
			{
				// have the VPRSequenceJSONDataFormat load the sequence.json into the synthesized audio object model
				SynthesizedAudioObjectModel vsq = (whatever as SynthesizedAudioObjectModel);
				VPRSequenceJSONDataFormat vpr = new VPRSequenceJSONDataFormat();

				File sequence_json = fsom.FindFile("Project\\sequence.json");
				if (sequence_json == null) throw new InvalidDataFormatException("archive does not contain a file called 'Project\\sequence.json'");

				MemoryAccessor ma = new MemoryAccessor(sequence_json.GetData());
				Document.Load(vsq, vpr, ma);
			}
			else
			{
				// huh?
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ObjectModel whatever = objectModels.Pop();
			FileSystemObjectModel fsom = new FileSystemObjectModel();

			if (whatever is SynthesizedAudioObjectModel)
			{
				// have the VPRSequenceJSONDataFormat save the sequence.json and just add it as the only file in the ZIP
				SynthesizedAudioObjectModel vsq = (whatever as SynthesizedAudioObjectModel);
				VPRSequenceJSONDataFormat vpr = new VPRSequenceJSONDataFormat();
				MemoryAccessor ma = new MemoryAccessor();
				Document.Save(vsq, vpr, ma);

				fsom.Files.Add("Project\\sequence.json", ma.ToArray());
			}
			else
			{
				// huh?
			}
			objectModels.Push(fsom);
		}
	}
}
