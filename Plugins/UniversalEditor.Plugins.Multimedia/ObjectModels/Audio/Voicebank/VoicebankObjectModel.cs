using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	public class VoicebankObjectModel : ObjectModel
	{
		public class VoicebankObjectModelCollection : Collection<VoicebankObjectModel>
		{
		}

		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Synthesized audio voice bank";
            omr.Path = new string[] { "Multimedia", "Audio", "Voicebank" };
            return omr;
		}

        public override void Clear()
        {
            mvarBankSelect = 0;
            mvarCreatorVersion = new Version(1, 0);
            mvarDocumentProperties = new DocumentProperties();
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

		private VoicebankSynthesisParameters mvarSynthesisParameters = new VoicebankSynthesisParameters();
		public VoicebankSynthesisParameters SynthesisParameters { get { return mvarSynthesisParameters; } }

        private DocumentProperties mvarDocumentProperties = new DocumentProperties();
        public DocumentProperties DocumentProperties { get { return mvarDocumentProperties; } }

		private VoicebankSample.VoicebankSampleCollection mvarSamples = new VoicebankSample.VoicebankSampleCollection();
		public VoicebankSample.VoicebankSampleCollection Samples { get { return mvarSamples; } }

		private Version mvarCreatorVersion = new Version(1, 0);
		public Version CreatorVersion { get { return mvarCreatorVersion; } set { mvarCreatorVersion = value; } }

		private string mvarInstallationPath = string.Empty;
		public string InstallationPath { get { return mvarInstallationPath; } set { mvarInstallationPath = value; } }

		public override void CopyTo(ObjectModel destination)
		{
			VoicebankObjectModel clone = (destination as VoicebankObjectModel);
            if (clone == null) return;

			foreach (KeyValuePair<string, string> kvp in mvarDocumentProperties.CustomProperties)
			{
				if (clone.DocumentProperties.CustomProperties.ContainsKey(kvp.Key))
				{
					clone.DocumentProperties.CustomProperties[kvp.Key] = kvp.Value;
				}
				else
				{
					clone.DocumentProperties.CustomProperties.Add(kvp.Key, kvp.Value);
				}
			}
			foreach (VoicebankSample phoneme in this.mvarSamples)
			{
				if (!clone.Samples.Contains(phoneme.Name)) clone.Samples.Add(phoneme.Clone() as VoicebankSample);
			}
			clone.CreatorVersion = (this.mvarCreatorVersion.Clone() as Version);
			clone.InstallationPath = this.mvarInstallationPath;
		}
	}
}
