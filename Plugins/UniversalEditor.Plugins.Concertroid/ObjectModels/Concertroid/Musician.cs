//
//  Musician.cs - represents a musician in a band for a Concertroid Performance
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

namespace UniversalEditor.ObjectModels.Concertroid
{
	/// <summary>
	/// Represents a musician in a band for a Concertroid <see cref="Concert.Performance" />.
	/// </summary>
	public class Musician
	{
		public class MusicianCollection
			: System.Collections.ObjectModel.Collection<Musician>
		{
		}

		/// <summary>
		/// Gets or sets the given name of this <see cref="Musician" />.
		/// </summary>
		/// <value>The given name of this <see cref="Musician" />.</value>
		public string GivenName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the family name of this <see cref="Musician" />.
		/// </summary>
		/// <value>The family name of this <see cref="Musician" />.</value>
		public string FamilyName { get; set; } = String.Empty;

		/// <summary>
		/// Gets or sets the full name of this <see cref="Musician" /> in "Given Family" format.
		/// </summary>
		/// <value>The full name of this <see cref="Musician" /> in "Given Family" format.</value>
		public string FullName
		{
			get { return GivenName + " " + FamilyName; }
			set
			{
				if (value.Contains(" "))
				{
					string[] dup = value.Split(new char[] { ' ' }, 2);
					if (dup.Length > 1)
					{
						GivenName = dup[0];
						FamilyName = dup[1];
					}
					else
					{
						GivenName = dup[0];
						FamilyName = String.Empty;
					}
				}
			}
		}
		/// <summary>
		/// Gets or sets the instrument played by this <see cref="Musician" />.
		/// </summary>
		/// <value>The instrument played by this <see cref="Musician" />.</value>
		public string Instrument { get; set; } = String.Empty;

		public object Clone()
		{
			Musician clone = new Musician();
			clone.GivenName = (GivenName.Clone() as string);
			clone.FamilyName = (FamilyName.Clone() as string);
			clone.Instrument = (Instrument.Clone() as string);
			return clone;
		}
	}
}
