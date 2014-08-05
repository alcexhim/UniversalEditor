using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	public class Phoneme
	{
		public class PhonemeCollection
			: System.Collections.ObjectModel.Collection<Phoneme>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
