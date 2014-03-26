using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioStylePlugin
	{
		private Guid mvarID = Guid.Empty;
		private string mvarName = string.Empty;
		private Version mvarVersion = new Version(1, 0);
		public Guid ID
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
		public Version Version
		{
			get
			{
				return this.mvarVersion;
			}
			set
			{
				this.mvarVersion = value;
			}
		}
	}
}
