//
//  PhonemeList.cs - represents a collection of phonemes for a particular language
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary
{
	/// <summary>
	/// Represents a collection of phonemes for a particular language.
	/// </summary>
	public class PhonemeList : ICloneable
	{
		public class PhonemeListCollection
			: System.Collections.ObjectModel.Collection<PhonemeList>
		{
		}

		public int LanguageID { get; set; } = 1033;
		public Phoneme.PhonemeCollection Phonemes { get; } = new Phoneme.PhonemeCollection();

		public object Clone()
		{
			PhonemeList clone = new PhonemeList();
			clone.LanguageID = LanguageID;
			foreach (Phoneme phoneme in Phonemes)
			{
				clone.Phonemes.Add(phoneme.Clone() as Phoneme);
			}
			return clone;
		}

		public Phoneme GetPhonemeFromMapping(string mapping)
		{
			foreach (Phoneme p in Phonemes)
			{
				if (p.Mappings.Contains(mapping.ToLower()))
				{
					return p;
				}
			}
			return null;
		}
	}
}
