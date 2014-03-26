using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary
{
	public class PhonemeDictionaryObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Phoneme dictionary";
			}
			return _omr;
		}
		public override void Clear()
		{
			mvarPhonemeLists.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			PhonemeDictionaryObjectModel clone = (where as PhonemeDictionaryObjectModel);
			if (clone == null) return;

			foreach (PhonemeList list in mvarPhonemeLists)
			{
				clone.PhonemeLists.Add(list.Clone() as PhonemeList);
			}
		}

		private PhonemeList.PhonemeListCollection mvarPhonemeLists = new PhonemeList.PhonemeListCollection();
		public PhonemeList.PhonemeListCollection PhonemeLists { get { return mvarPhonemeLists; } }
	}
}
