using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioCommandNote : SynthesizedAudioCommand
	{
		private bool mvarProtected = false;
		private int mvarPosition = 0;
		private string mvarPhoneme = null;
		private string mvarLyric = null;
		private double mvarFrequency = 0;
		private int mvarPreUtterance = 0;
		private int mvarVoiceOverlap = 0;
		private int mvarIntensity = 0;
		private int mvarModulation = 0;
		private int mvarPBType = 0;
		private double[] mvarPitches = new double[0];
		private string[] mvarEnvelope = new string[0];
		private double[] mvarVBR = new double[0];
		private int mvarAccent = 50;
		private int mvarPitchBendDepth = 8;
		private int mvarPitchBendLength = 0;
		private int mvarDecay = 50;
		private bool mvarPortamentoFalling = false;
		private int mvarOpening = 127;
		private bool mvarPortamentoRising = false;
		private int mvarVibratoLength = 0;
		private SynthesizedAudioVibratoType mvarVibratoType = SynthesizedAudioVibratoType.None;
		public bool Protected
		{
			get
			{
				return this.mvarProtected;
			}
			set
			{
				this.mvarProtected = value;
			}
		}
		public int Position
		{
			get
			{
				return this.mvarPosition;
			}
			set
			{
				this.mvarPosition = value;
			}
		}
        private double mvarLength = 0;
        public double Length
		{
			get { return mvarLength; }
			set { mvarLength = value; }
		}
		public string Phoneme
		{
			get { return mvarPhoneme; }
			set { mvarPhoneme = value; }
		}
		public string Lyric
		{
			get { return mvarLyric; }
			set { mvarLyric = value; }
		}
		public double Frequency
		{
			get
			{
				return this.mvarFrequency;
			}
			set
			{
				this.mvarFrequency = value;
			}
		}
		public int PreUtterance
		{
			get
			{
				return this.mvarPreUtterance;
			}
			set
			{
				this.mvarPreUtterance = value;
			}
		}
		public int VoiceOverlap
		{
			get
			{
				return this.mvarVoiceOverlap;
			}
			set
			{
				this.mvarVoiceOverlap = value;
			}
		}
		public int Intensity
		{
			get
			{
				return this.mvarIntensity;
			}
			set
			{
				this.mvarIntensity = value;
			}
		}
		public int Modulation
		{
			get
			{
				return this.mvarModulation;
			}
			set
			{
				this.mvarModulation = value;
			}
		}
		public int PBType
		{
			get
			{
				return this.mvarPBType;
			}
			set
			{
				this.mvarPBType = value;
			}
		}
		public double[] Pitches
		{
			get
			{
				return this.mvarPitches;
			}
			set
			{
				this.mvarPitches = value;
			}
		}
		public string[] Envelope
		{
			get
			{
				return this.mvarEnvelope;
			}
			set
			{
				this.mvarEnvelope = value;
			}
		}
		public double[] VBR
		{
			get
			{
				return this.mvarVBR;
			}
			set
			{
				this.mvarVBR = value;
			}
		}
		public int Accent
		{
			get
			{
				return this.mvarAccent;
			}
			set
			{
				this.mvarAccent = value;
			}
		}
		public int PitchBendDepth
		{
			get
			{
				return this.mvarPitchBendDepth;
			}
			set
			{
				this.mvarPitchBendDepth = value;
			}
		}
		public int PitchBendLength
		{
			get
			{
				return this.mvarPitchBendLength;
			}
			set
			{
				this.mvarPitchBendLength = value;
			}
		}
		public int Decay
		{
			get
			{
				return this.mvarDecay;
			}
			set
			{
				this.mvarDecay = value;
			}
		}
		public bool PortamentoFalling
		{
			get
			{
				return this.mvarPortamentoFalling;
			}
			set
			{
				this.mvarPortamentoFalling = value;
			}
		}
		public int Opening
		{
			get
			{
				return this.mvarOpening;
			}
			set
			{
				this.mvarOpening = value;
			}
		}
		public bool PortamentoRising
		{
			get
			{
				return this.mvarPortamentoRising;
			}
			set
			{
				this.mvarPortamentoRising = value;
			}
		}
		public int VibratoLength
		{
			get
			{
				return this.mvarVibratoLength;
			}
			set
			{
				this.mvarVibratoLength = value;
			}
		}
		public SynthesizedAudioVibratoType VibratoType
		{
			get
			{
				return this.mvarVibratoType;
			}
			set
			{
				this.mvarVibratoType = value;
			}
		}
		public override object Clone()
		{
			return new SynthesizedAudioCommandNote
			{
				Envelope = this.mvarEnvelope.Clone() as string[], 
				Intensity = this.mvarIntensity, 
				Length = this.mvarLength, 
				Lyric = this.mvarLyric, 
				Modulation = this.mvarModulation, 
				PBType = this.mvarPBType, 
				Phoneme = this.mvarPhoneme, 
				Pitches = this.mvarPitches.Clone() as double[], 
				PortamentoFalling = this.mvarPortamentoFalling, 
				PortamentoRising = this.mvarPortamentoRising, 
				PreUtterance = this.mvarPreUtterance, 
				Protected = this.mvarProtected, 
				Frequency = this.mvarFrequency, 
				VBR = this.mvarVBR.Clone() as double[], 
				VoiceOverlap = this.mvarVoiceOverlap
			};
		}
	}
}
