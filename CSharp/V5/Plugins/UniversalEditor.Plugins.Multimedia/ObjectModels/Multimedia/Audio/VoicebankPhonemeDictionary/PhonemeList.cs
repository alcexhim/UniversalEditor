using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary
{
	public class PhonemeList : ICloneable
	{
		public class PhonemeListCollection
			: System.Collections.ObjectModel.Collection<PhonemeList>
		{
		}

		private int mvarLanguageID = 1033;
		public int LanguageID { get { return mvarLanguageID; } set { mvarLanguageID = value; } }

		private Phoneme.PhonemeCollection mvarPhonemes = new Phoneme.PhonemeCollection();
		public Phoneme.PhonemeCollection Phonemes { get { return mvarPhonemes; } }

		public object Clone()
		{
			PhonemeList clone = new PhonemeList();
			clone.LanguageID = mvarLanguageID;
			foreach (Phoneme phoneme in mvarPhonemes)
			{
				clone.Phonemes.Add(phoneme.Clone() as Phoneme);
			}
			return clone;
		}

		public Phoneme GetPhonemeFromMapping(string mapping)
		{
			foreach (Phoneme p in mvarPhonemes)
			{
				if (p.Mappings.Contains(mapping.ToLower())) return p;
			}
			return null;
		}
	}
}
