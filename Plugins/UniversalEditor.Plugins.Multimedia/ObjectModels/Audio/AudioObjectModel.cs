using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio
{
	public abstract class AudioObjectModel : ObjectModel
	{
		private AudioObjectModelInformation mvarInformation = new AudioObjectModelInformation();
		public AudioObjectModelInformation Information { get { return mvarInformation; } }
	}
}
