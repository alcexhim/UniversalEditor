//
//  Gender.cs - represents a gender in a story
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

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	/// <summary>
	/// Represents a gender in a story.
	/// </summary>
	public class Gender
	{
		public class GenderCollection
			: System.Collections.ObjectModel.Collection<Gender>
		{

		}

		/// <summary>
		/// Gets a <see cref="Gender" /> indicating typically masculine attributes, such as the pronouns "he/him/his".
		/// </summary>
		/// <value>The masculine gender.</value>
		public static Gender Masculine { get; } = new Gender(new Guid("{0CA5B741-4DFE-4A10-A51D-A29A4503DF62}"), "Masculine", "he", "they", "his", "their", "him", "them");
		/// <summary>
		/// Gets a <see cref="Gender" /> indicating typically feminine attributes, such as the pronouns "she/her/hers".
		/// </summary>
		/// <value>The feminine gender.</value>
		public static Gender Feminine { get; } = new Gender(new Guid("{231A45E1-5DCC-4C6C-892C-AE6F69CAAE36}"), "Feminine", "she", "they", "hers", "their", "her", "them");
		/// <summary>
		/// Gets a <see cref="Gender" /> indicating gender-neutral attributes, such as the pronouns "it/it/its".
		/// </summary>
		/// <value>The masculine gender.</value>
		public static Gender Neutral { get; } = new Gender(new Guid("{6E90BE20-B142-4902-B560-5B3A77546B17}"), "Neutral", "it", "them", "its", "theirs", "it", "them");

		public Gender()
		{

		}
		public Gender(Guid id, string name)
		{
			ID = id;
			Name = name;
		}
		public Gender(Guid id, string name, string singularSubject, string pluralSubject, string singularPossessive, string pluralPossessive, string singularObject, string pluralObject)
		{
			ID = id;
			Name = name;
			SingularSubject = singularSubject;
			SingularPossessive = singularPossessive;
			SingularObject = singularObject;
			PluralSubject = pluralSubject;
			PluralPossessive = pluralPossessive;
			PluralObject = pluralObject;
		}

		public Guid ID { get; set; } = Guid.Empty;
		public string Name { get; set; } = String.Empty;

		/// <summary>
		/// He, she, it.
		/// </summary>
		public string SingularSubject { get; set; } = String.Empty;
		/// <summary>
		/// they
		/// </summary>
		public string PluralSubject { get; set; } = String.Empty;
		/// <summary>
		/// His, hers, its.
		/// </summary>
		public string SingularPossessive { get; set; } = String.Empty;
		/// <summary>
		/// Theirs
		/// </summary>
		public string PluralPossessive { get; set; } = String.Empty;
		/// <summary>
		/// Him, her, it.
		/// </summary>
		public string SingularObject { get; set; } = String.Empty;
		/// <summary>
		/// Them
		/// </summary>
		public string PluralObject { get; set; } = String.Empty;
	}
}
