using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	public class PhonemeGroup
	{
		public class PhonemeGroupCollection
			: System.Collections.ObjectModel.Collection<PhonemeGroup>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Phoneme.PhonemeCollection mvarPhonemes = new Phoneme.PhonemeCollection();
		public Phoneme.PhonemeCollection Phonemes { get { return mvarPhonemes; } }
	}
}
