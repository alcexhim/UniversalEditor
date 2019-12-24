using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioTrack : ICloneable
	{
		public class SynthesizedAudioTrackCollection : Collection<SynthesizedAudioTrack>
		{
			private Dictionary<string, SynthesizedAudioTrack> tracksByID = new Dictionary<string, SynthesizedAudioTrack>();
			public SynthesizedAudioTrack this[string ID]
			{
				get
				{
					SynthesizedAudioTrack result;
					if (this.tracksByID.ContainsKey(ID))
					{
						result = this.tracksByID[ID];
					}
					else
					{
						result = null;
					}
					return result;
				}
			}
			public new void Add(SynthesizedAudioTrack item)
			{
				if (!string.IsNullOrEmpty(item.Name))
				{
					this.tracksByID[item.Name] = item;
				}
				base.Add(item);
			}
		}
		private string mvarID = string.Empty;
		private string mvarName = string.Empty;
		private string mvarComment = string.Empty;
		private SynthesizedAudioCommand.SynthesizedAudioCommandCollection mvarCommands = new SynthesizedAudioCommand.SynthesizedAudioCommandCollection();
		private VoicebankObjectModel mvarSynthesizer = null;
		private bool mvarIsMuted = false;
		private bool mvarIsSolo = false;
		private byte mvarPanpot = 64;
		private byte mvarVolume = 0;
		public string ID
		{
			get
			{
				return this.mvarID;
			}
			set
			{
				this.mvarID = value;
			}
		}
		public string Name
		{
			get
			{
				return this.mvarName;
			}
			set
			{
				this.mvarName = value;
			}
		}
		public string Comment
		{
			get
			{
				return this.mvarComment;
			}
			set
			{
				this.mvarComment = value;
			}
		}
		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection Commands
		{
			get
			{
				return this.mvarCommands;
			}
		}
		public VoicebankObjectModel Synthesizer
		{
			get
			{
				return this.mvarSynthesizer;
			}
			set
			{
				this.mvarSynthesizer = value;
			}
		}
		public bool IsMuted
		{
			get
			{
				return this.mvarIsMuted;
			}
			set
			{
				this.mvarIsMuted = value;
			}
		}
		public bool IsSolo
		{
			get
			{
				return this.mvarIsSolo;
			}
			set
			{
				this.mvarIsSolo = value;
			}
		}
		public byte Panpot
		{
			get
			{
				return this.mvarPanpot;
			}
			set
			{
				this.mvarPanpot = value;
			}
		}
		public byte Volume
		{
			get
			{
				return this.mvarVolume;
			}
			set
			{
				this.mvarVolume = value;
			}
		}
		public object Clone()
		{
			SynthesizedAudioTrack clone = new SynthesizedAudioTrack();
			foreach (SynthesizedAudioCommand command in this.mvarCommands)
			{
				clone.Commands.Add(command.Clone() as SynthesizedAudioCommand);
			}
			clone.ID = (this.mvarID.Clone() as string);
			clone.Name = (this.mvarName.Clone() as string);
			return clone;
		}

		public double Tempo { get; set; } = 120;
		public Color Color { get; set; } = Color.Empty;
	}
}
