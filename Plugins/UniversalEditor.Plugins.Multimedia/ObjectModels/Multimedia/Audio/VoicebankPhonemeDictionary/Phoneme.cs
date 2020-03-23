using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary
{
	public class Phoneme : ICloneable
	{
		public class PhonemeCollection
			: System.Collections.ObjectModel.Collection<Phoneme>
		{
		}

		private string mvarValue = String.Empty;
		public string Value
		{
			get { return mvarValue; }
			set { mvarValue = value; }
		}

		private System.Collections.Specialized.StringCollection mvarMappings = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Mappings
		{
			get { return mvarMappings; }
		}

		public object Clone()
		{
			Phoneme clone = new Phoneme();
			clone.Value = (mvarValue.Clone() as string);
			foreach (string mapping in mvarMappings)
			{
				clone.Mappings.Add(mapping.Clone() as string);
			}
			return clone;
		}
	}
}
