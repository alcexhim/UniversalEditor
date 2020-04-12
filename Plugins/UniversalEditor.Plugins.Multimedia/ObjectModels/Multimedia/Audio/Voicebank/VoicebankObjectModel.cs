//
//  VoicebankObjectModel.cs - provides an ObjectModel for manipulating synthesizer voicebank databases
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

using System;
using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating synthesizer voicebank databases.
	/// </summary>
	public class VoicebankObjectModel : ObjectModel
	{
		public class VoicebankObjectModelCollection : Collection<VoicebankObjectModel>
		{
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Synthesized audio voice bank";
				_omr.Path = new string[] { "Multimedia", "Audio", "Voicebank" };
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarBankSelect = 0;
			mvarCreatorVersion = new Version(1, 0);
			mvarID = String.Empty;
			mvarInstallationPath = String.Empty;
			mvarProgramChange = 0;
			mvarSamples.Clear();
			mvarSynthesisParameters.Clear();
		}

		private int mvarBankSelect = 0;
		public int BankSelect { get { return mvarBankSelect; } set { mvarBankSelect = value; } }

		private int mvarProgramChange = 0;
		public int ProgramChange { get { return mvarProgramChange; } set { mvarProgramChange = value; } }

		private string mvarID = string.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		private PhonemeGroup.PhonemeGroupCollection mvarPhonemeGroups = new PhonemeGroup.PhonemeGroupCollection();
		public PhonemeGroup.PhonemeGroupCollection PhonemeGroups { get { return mvarPhonemeGroups; } }

		private VoicebankSynthesisParameters mvarSynthesisParameters = new VoicebankSynthesisParameters();
		public VoicebankSynthesisParameters SynthesisParameters { get { return mvarSynthesisParameters; } }

		private VoicebankSample.VoicebankSampleCollection mvarSamples = new VoicebankSample.VoicebankSampleCollection();
		public VoicebankSample.VoicebankSampleCollection Samples { get { return mvarSamples; } }

		private Version mvarCreatorVersion = new Version(1, 0);
		public Version CreatorVersion { get { return mvarCreatorVersion; } set { mvarCreatorVersion = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarInstallationPath = string.Empty;
		public string InstallationPath { get { return mvarInstallationPath; } set { mvarInstallationPath = value; } }

		public override void CopyTo(ObjectModel destination)
		{
			VoicebankObjectModel clone = (destination as VoicebankObjectModel);
			if (clone == null) return;

			foreach (VoicebankSample phoneme in this.mvarSamples)
			{
				if (!clone.Samples.Contains(phoneme.Name)) clone.Samples.Add(phoneme.Clone() as VoicebankSample);
			}
			clone.CreatorVersion = (this.mvarCreatorVersion.Clone() as Version);
			clone.InstallationPath = this.mvarInstallationPath;
		}

		public VoicebankSample GetSampleFromPhoneme(string phoneme, double frequency)
		{
			foreach (VoicebankSample sample in mvarSamples)
			{
				if ((sample.Phoneme == phoneme) && (sample.MaximumFrequency == -1 || frequency < sample.MaximumFrequency) && (sample.MinimumFrequency == -1 || frequency > sample.MinimumFrequency))
				{
					return sample;
				}
			}
			return null;
		}
	}
}
