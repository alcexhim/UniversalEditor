//
//  DirectWavePatchDataFormat.cs - provides a DataFormat for manipulating a synthesized audio voicebank file in DirectWave patch format
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
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Voicebank.DirectWave
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating a synthesized audio voicebank file in DirectWave patch format.
	/// </summary>
	public class DirectWavePatchDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(VoicebankObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VoicebankObjectModel vom = (objectModel as VoicebankObjectModel);
			Reader br = base.Accessor.Reader;
			string DwPr = br.ReadFixedLengthString(4);
			int n0 = br.ReadInt32();
			int n = br.ReadInt32();
			vom.Title = br.ReadNullTerminatedString(32);
			vom.InstallationPath = br.ReadNullTerminatedString(260);
			int n2 = br.ReadInt32();
			int n3 = br.ReadInt32();
			int n4 = br.ReadInt32();
			int sampleCount = br.ReadInt32();
			byte[] data = br.ReadBytes(144u);
			for (int i = 0; i < sampleCount; i++)
			{
				VoicebankSample sample = new VoicebankSample();
				sample.Name = br.ReadNullTerminatedString(32);
				sample.FileName = br.ReadNullTerminatedString(260);
				sample.Data = br.ReadBytes(1406u);
				vom.Samples.Add(sample);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			VoicebankObjectModel vom = objectModel as VoicebankObjectModel;
			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("DwPr");
			int n0 = 0;
			bw.WriteInt32(n0);
			int n = 0;
			bw.WriteInt32(n);
			bw.WriteNullTerminatedString(vom.Title, 32);
			bw.WriteNullTerminatedString(vom.InstallationPath, 264);
			bw.Flush();
		}
	}
}
