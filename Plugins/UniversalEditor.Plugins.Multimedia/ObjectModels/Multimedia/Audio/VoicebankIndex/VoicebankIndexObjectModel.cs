//
//  VoicebankIndexObjectModel.cs - provides an ObjectModel for manipulating voicebank index files
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

using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankIndex
{
	/// <summary>
	/// Provides an ObjectModel for manipulating voicebank index files
	/// </summary>
	public class VoicebankIndexObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "Audio", "Voicebank index" };
			}
			return _omr;
		}
		public string Title { get; set; } = String.Empty;
		public Phoneme.PhonemeCollection Phonemes { get; } = new Phoneme.PhonemeCollection();
		public PhonemeGroup.PhonemeGroupCollection Groups { get; } = new PhonemeGroup.PhonemeGroupCollection();

		public override void Clear()
		{
			Title = String.Empty;
			Phonemes.Clear();
			Groups.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			VoicebankIndexObjectModel clone = (where as VoicebankIndexObjectModel);
			clone.Title = (Title.Clone() as string);
			foreach (Phoneme p in Phonemes)
			{
				clone.Phonemes.Add(p.Clone() as Phoneme);
			}
			foreach (PhonemeGroup pg in Groups)
			{
				clone.Groups.Add(pg.Clone() as PhonemeGroup);
			}
		}
	}
}
