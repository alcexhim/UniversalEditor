using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	public class Phoneme : ICloneable
	{
		public class PhonemeCollection
			: System.Collections.ObjectModel.Collection<Phoneme>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public object Clone()
		{
			Phoneme clone = new Phoneme();
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return Title;
		}
	}
}
