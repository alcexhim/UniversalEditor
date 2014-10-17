using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankIndex
{
	public class VoicebankIndexObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Path = new string[] { "Multimedia", "Audio", "Voicebank index" };
				_omr.Title = "Voicebank index";
			}
			return _omr;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Phoneme.PhonemeCollection mvarPhonemes = new Phoneme.PhonemeCollection();
		public Phoneme.PhonemeCollection Phonemes { get { return mvarPhonemes; } }

		private PhonemeGroup.PhonemeGroupCollection mvarGroups = new PhonemeGroup.PhonemeGroupCollection();
		public PhonemeGroup.PhonemeGroupCollection Groups { get { return mvarGroups; } }

		public override void Clear()
		{
			mvarTitle = String.Empty;
			mvarPhonemes.Clear();
			mvarGroups.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			VoicebankIndexObjectModel clone = (where as VoicebankIndexObjectModel);
			clone.Title = (mvarTitle.Clone() as string);
			foreach (Phoneme p in mvarPhonemes)
			{
				clone.Phonemes.Add(p.Clone() as Phoneme);
			}
			foreach (PhonemeGroup pg in mvarGroups)
			{
				clone.Groups.Add(pg.Clone() as PhonemeGroup);
			}
		}
	}
}
