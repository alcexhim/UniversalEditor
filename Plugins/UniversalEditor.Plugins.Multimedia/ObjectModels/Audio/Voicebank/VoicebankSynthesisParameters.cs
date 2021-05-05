using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	public class VoicebankSynthesisParameters
	{
		private byte mvarBreathiness = 0;
		public byte Breathiness { get { return mvarBreathiness; } set { mvarBreathiness = value; } }

        private byte mvarBrightness = 0;
        public byte Brightness { get { return mvarBrightness; } set { mvarBrightness = value; } }

        private byte mvarClearness = 0;
        public byte Clearness { get { return mvarClearness; } set { mvarClearness = value; } }

        private byte mvarGenderFactor = 0;
        public byte GenderFactor { get { return mvarGenderFactor; } set { mvarGenderFactor = value; } }

        private byte mvarOpenness = 0;
        public byte Openness { get { return mvarOpenness; } set { mvarOpenness = value; } }

        public void Clear()
        {
            mvarBreathiness = 0;
            mvarBrightness = 0;
            mvarClearness = 0;
            mvarGenderFactor = 0;
            mvarOpenness = 0;
        }
    }
}
