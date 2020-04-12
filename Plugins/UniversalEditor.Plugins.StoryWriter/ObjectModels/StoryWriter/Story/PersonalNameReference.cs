//
//  PersonalNameReference.cs - represents a formattable reference to an existing PersonalName
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
	/// Represents a formattable reference to an existing <see cref="PersonalName" />.
	/// </summary>
	public class PersonalNameReference : ContentItem
	{
		/// <summary>
		/// Gets the <see cref="PersonalName" /> to which this <see cref="PersonalNameReference" /> refers.
		/// </summary>
		/// <value>The <see cref="PersonalName" /> to which this <see cref="PersonalNameReference" /> refers.</value>
		public PersonalName Value { get; } = null;
		/// <summary>
		/// Gets or sets the format string used in displaying this <see cref="PersonalNameReference" />.
		/// </summary>
		/// <value>The format string used in displaying this <see cref="PersonalNameReference" />.</value>
		public string Format { get; set; } = "{GivenName}";

		/// <summary>
		/// Renders the content of this <see cref="ContentItem" /> to a <see cref="String" />.
		/// </summary>
		/// <returns>The content of this <see cref="ContentItem" /> as a <see cref="String" />.</returns>
		protected override string RenderContent()
		{
			if (Value == null) throw new NullReferenceException("Value cannot be null");
			return FormatString(Format, Value);
		}

		/// <summary>
		/// Formats the specified <see cref="string" /> with the given <see cref="PersonalName" />.
		/// </summary>
		/// <returns>The <see cref="string" /> containing parameters replaced with the respective values of the given <see cref="PersonalName" />.</returns>
		/// <param name="format">The <see cref="string" /> to format.</param>
		/// <param name="value">The <see cref="PersonalName" /> which supplies the attributes for formatting.</param>
		private static string FormatString(string format, PersonalName value)
		{
			string retval = format;
			retval = retval.Replace("{GivenName}", value.GivenName);
			retval = retval.Replace("{FamilyName}", value.FamilyName);
			for (int i = 0; i < value.MiddleNames.Count; i++)
			{
				retval = retval.Replace("{MiddleName:" + i.ToString() + "}", value.MiddleNames[i]);
			}
			retval = retval.Replace("{Nickname}", value.Nickname);
			return retval;
		}
	}
}
