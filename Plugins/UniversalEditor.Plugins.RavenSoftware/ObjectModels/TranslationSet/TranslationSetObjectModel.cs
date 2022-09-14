//
//  TranslationSetObjectModel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.ObjectModels.TranslationSet
{
	public class TranslationSetObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;

		public TranslationSetEntry.TranslationSetEntryCollection Entries { get; } = new TranslationSetEntry.TranslationSetEntryCollection();

		public int ID { get; set; } = 0;
		public string Reference { get; set; } = null;
		public string Description { get; set; } = null;

		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Game development", "Raven Software", "Translation set / strip (string package)" };
				_omr.EmptyTemplatePrefix = "TranslationSet";
			}
			return _omr;
		}

		public override void Clear()
		{
			Entries.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			TranslationSetObjectModel clone = (where as TranslationSetObjectModel);
			if (clone == null)
				throw new ObjectModelNotSupportedException();

			foreach (TranslationSetEntry entry in Entries)
			{
				clone.Entries.Add(entry.Clone() as TranslationSetEntry);
			}
		}
	}
}
