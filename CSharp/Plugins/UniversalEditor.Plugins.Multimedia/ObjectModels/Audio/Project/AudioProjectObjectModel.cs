using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Project
{
	public class AudioProjectObjectModel : ObjectModel
	{
		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Audio project";
            omr.Path = new string[] { "Multimedia", "Audio", "Audio Project" };
			return omr;
		}
		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel destination)
		{
			AudioProjectObjectModel clone = destination as AudioProjectObjectModel;
		}
	}
}
