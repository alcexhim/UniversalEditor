//
//  PhonemeGroup.cs - represents a group of phonemes in a synthesizer voicebank file
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

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	/// <summary>
	/// Represents a group of phonemes in a synthesizer voicebank file.
	/// </summary>
	public class PhonemeGroup : ICloneable
	{
		public class PhonemeGroupCollection
			: System.Collections.ObjectModel.Collection<PhonemeGroup>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Phoneme.PhonemeCollection mvarPhonemes = new Phoneme.PhonemeCollection();
		public Phoneme.PhonemeCollection Phonemes { get { return mvarPhonemes; } }

		public object Clone()
		{
			PhonemeGroup clone = new PhonemeGroup();
			clone.Title = (mvarTitle.Clone() as string);
			foreach (Phoneme p in mvarPhonemes)
			{
				clone.Phonemes.Add(p);
			}
			return clone;
		}
	}
}
