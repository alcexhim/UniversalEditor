using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioCommandTimeSignature : SynthesizedAudioCommand
	{
		private byte mvarNumerator = 0;
		private byte mvarDenominator = 0;
		private byte mvarTicksPerMetronomeClick = 0;
		private byte mvarNumberOf32ndNotesPerQuarterNote = 0;
		public byte Numerator
		{
			get
			{
				return this.mvarNumerator;
			}
			set
			{
				this.mvarNumerator = value;
			}
		}
		public byte Denominator
		{
			get
			{
				return this.mvarDenominator;
			}
			set
			{
				this.mvarDenominator = value;
			}
		}
		public byte TicksPerMetronomeClick
		{
			get
			{
				return this.mvarTicksPerMetronomeClick;
			}
			set
			{
				this.mvarTicksPerMetronomeClick = value;
			}
		}
		public byte NumberOf32ndNotesPerQuarterNote
		{
			get
			{
				return this.mvarNumberOf32ndNotesPerQuarterNote;
			}
			set
			{
				this.mvarNumberOf32ndNotesPerQuarterNote = value;
			}
		}
		public SynthesizedAudioCommandTimeSignature()
		{
		}
		public SynthesizedAudioCommandTimeSignature(byte numerator, byte denominator, byte ticksPerMetronomeClick, byte numberOf32ndNotesPerQuarterNote)
		{
			this.mvarNumerator = numerator;
			this.mvarDenominator = denominator;
			this.mvarTicksPerMetronomeClick = ticksPerMetronomeClick;
			this.mvarNumberOf32ndNotesPerQuarterNote = numberOf32ndNotesPerQuarterNote;
		}
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"TS = ", 
				this.mvarNumerator.ToString(), 
				"/", 
				this.mvarDenominator.ToString(), 
				"; ♪ = ", 
				this.mvarNumberOf32ndNotesPerQuarterNote.ToString()
			});
		}
		public override object Clone()
		{
			return new SynthesizedAudioCommandTimeSignature
			{
				Denominator = this.mvarDenominator, 
				NumberOf32ndNotesPerQuarterNote = this.mvarNumberOf32ndNotesPerQuarterNote, 
				Numerator = this.mvarNumerator, 
				TicksPerMetronomeClick = this.mvarTicksPerMetronomeClick
			};
		}
	}
}
