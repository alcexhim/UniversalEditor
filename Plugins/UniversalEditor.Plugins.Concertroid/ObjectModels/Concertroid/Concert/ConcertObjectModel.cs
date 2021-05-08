//
//  ConcertObjectModel.cs - provides an ObjectModel for manipulating Concertroid concert information
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

namespace UniversalEditor.ObjectModels.Concertroid.Concert
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Concertroid concert information.
	/// </summary>
	public class ConcertObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Concertroid", "Concert" };
			}
			return _omr;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarBandName = String.Empty;
		public string BandName { get { return mvarBandName; } set { mvarBandName = value; } }

		private Musician.MusicianCollection mvarBandMusicians = new Musician.MusicianCollection();
		public Musician.MusicianCollection BandMusicians { get { return mvarBandMusicians; } set { mvarBandMusicians = value; } }

		private Musician.MusicianCollection mvarGuestMusicians = new Musician.MusicianCollection();
		public Musician.MusicianCollection GuestMusicians { get { return mvarGuestMusicians; } set { mvarGuestMusicians = value; } }

		private Performance.PerformanceCollection mvarPerformances = new Performance.PerformanceCollection();
		public Performance.PerformanceCollection Performances { get { return mvarPerformances; } }

		public override void Clear()
		{
			mvarTitle = String.Empty;
			mvarBandName = String.Empty;
			mvarBandMusicians.Clear();
			mvarGuestMusicians.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ConcertObjectModel clone = (where as ConcertObjectModel);
			clone.Title = (mvarTitle.Clone() as string);
			clone.BandName = (mvarBandName.Clone() as string);
			foreach (Musician musician in mvarBandMusicians)
			{
				clone.BandMusicians.Add(musician.Clone() as Musician);
			}
			foreach (Musician musician in mvarGuestMusicians)
			{
				clone.GuestMusicians.Add(musician.Clone() as Musician);
			}
		}
	}
}
