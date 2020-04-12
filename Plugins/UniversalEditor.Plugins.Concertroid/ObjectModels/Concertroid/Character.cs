//
//  Character.cs - represents a character in a Concertroid Performance
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
	/// Represents a character in a Concertroid <see cref="Concert.Performance" />.
	/// </summary>
	public class Character : ICloneable
	{
		public class CharacterCollection
			: System.Collections.ObjectModel.Collection<Character>
		{
		}

		/// <summary>
		/// Gets or sets the given name of the <see cref="Character" />.
		/// </summary>
		/// <value>The given name of the <see cref="Character" />.</value>
		public string GivenName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the family name of the <see cref="Character" />.
		/// </summary>
		/// <value>The family name of the <see cref="Character" />.</value>
		public string FamilyName { get; set; } = String.Empty;

		/// <summary>
		/// Gets or sets the full name of the <see cref="Character" /> in "Given Family" format.
		/// </summary>
		/// <value>The full name of the <see cref="Character" /> in "Given Family" format.</value>
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
		/// Gets or sets the full path to the file containing the base 3D character model (without costume).
		/// </summary>
		/// <value>The full path to the file containing the base 3D character model (without costume).</value>
		public string BaseModelFileName { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="Language" /> instances representing the languages associated with this <see cref="Character" />.
		/// </summary>
		/// <value>The languages associated with this <see cref="Character" />.</value>
		public Language.LanguageCollection Languages { get; } = new Language.LanguageCollection();
		/// <summary>
		/// Gets a collection of <see cref="Costume" /> instances representing the costumes associated with this <see cref="Character" />.
		/// </summary>
		/// <value>The costumes associated with this <see cref="Character" />.</value>
		public Costume.CostumeCollection Costumes { get; } = new Costume.CostumeCollection();

		public object Clone()
		{
			Character clone = new Character();
			clone.GivenName = (GivenName.Clone() as string);
			clone.FamilyName = (FamilyName.Clone() as string);
			clone.BaseModelFileName = (BaseModelFileName.Clone() as string);

			foreach (Costume costume in Costumes)
			{
				clone.Costumes.Add(costume.Clone() as Costume);
			}
			return clone;
		}
	}
}
