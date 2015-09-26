using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	public class VoicebankSample : ICloneable
	{
		public class VoicebankSampleCollection : Collection<VoicebankSample>
		{
			private Dictionary<string, VoicebankSample> phonemesByName = new Dictionary<string, VoicebankSample>();
			public VoicebankSample this[string Name] { get { return phonemesByName[Name]; } }

			public VoicebankSample Add(string Name, byte[] data)
			{
				VoicebankSample vp = new VoicebankSample();
				vp.Name = Name;
				vp.Data = data;
				this.Add(vp);
				return vp;
			}
			public bool Contains(string Name)
			{
				return this.phonemesByName.ContainsKey(Name);
			}
            protected override void InsertItem(int index, VoicebankSample item)
            {
                base.InsertItem(index, item);
                if (!this.phonemesByName.ContainsKey(item.Name))
                {
                    this.phonemesByName.Add(item.Name, item);
                }
            }
		}

		private string mvarName = string.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

        private int mvarFrequency = 0;
		/// <summary>
		/// The frequency at which this sample was recorded. Used to pitch-shift the sample.
		/// </summary>
        public int Frequency { get { return mvarFrequency; } set { mvarFrequency = value; } }

        private short mvarChannelCount = 0;
        public short ChannelCount { get { return mvarChannelCount; } set { mvarChannelCount = value; } }
        private int mvarDummy = 0;
        public int Dummy { get { return mvarDummy; } set { mvarDummy = value; } }


		private WaveformAudioObjectModel mvarWaveform = null;
		/// <summary>
		/// The waveform audio data for this sample.
		/// </summary>
		public WaveformAudioObjectModel Waveform { get { return mvarWaveform; } set { mvarWaveform = value; } }

		private int mvarMaximumFrequency = -1; // 440?
		/// <summary>
		/// The maximum frequency in which to use this particular sample.
		/// </summary>
		/// <remarks>To use the sample for exactly one note, ensure that both MaximumFrequency and MinimumFrequency are set to the same value, matching the desired note's frequency.</remarks>
		public int MaximumFrequency { get { return mvarMaximumFrequency; } set { mvarMaximumFrequency = value; } }
		private int mvarMinimumFrequency = -1; // 440?
		/// <summary>
		/// The maximum frequency in which to use this particular sample.
		/// </summary>
		/// <remarks>To use the sample for exactly one note, ensure that both MaximumFrequency and MinimumFrequency are set to the same value, matching the desired note's frequency.</remarks>
		public int MinimumFrequency { get { return mvarMinimumFrequency; } set { mvarMinimumFrequency = value; } }

		private string mvarPhoneme = null;
		/// <summary>
		/// The phoneme that is represented by this sample. May be null for non-vocal samples.
		/// </summary>
		public string Phoneme { get { return mvarPhoneme; } set { mvarPhoneme = value; } }


        private byte[] mvarData = new byte[0];
        public byte[] Data { get { return mvarData; } set { mvarData = value; } }

        private string mvarFileName = string.Empty;
        public string FileName
		{
			get { return mvarFileName; }
			set { mvarFileName = value; }
		}
		public object Clone()
		{
			return new VoicebankSample
			{
				Name = this.mvarName, 
				FileName = this.mvarFileName, 
				Data = this.mvarData
			};
		}
	}
}
