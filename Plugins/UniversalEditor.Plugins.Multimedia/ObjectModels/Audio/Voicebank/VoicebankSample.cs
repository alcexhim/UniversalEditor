using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int Frequency { get { return mvarFrequency; } set { mvarFrequency = value; } }
        private short mvarChannelCount = 0;
        public short ChannelCount { get { return mvarChannelCount; } set { mvarChannelCount = value; } }
        private int mvarDummy = 0;
        public int Dummy { get { return mvarDummy; } set { mvarDummy = value; } }

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
