//
//  XMLDataFormatSettings.cs - represents settings for the XMLDataFormat parser
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

using MBS.Framework.Collections.Generic;

namespace UniversalEditor.DataFormats.Markup.XML
{
	/// <summary>
	/// Represents settings for the <see cref="XMLDataFormat" /> parser.
	/// </summary>
	public class XMLDataFormatSettings
	{
		/// <summary>
		/// Gets or sets the character used to indicate the beginning of a tag.
		/// </summary>
		/// <value>The character used to indicate the beginning of a tag.</value>
		public char TagBeginChar { get; set; } = '<';
		/// <summary>
		/// Gets or sets the character used to indicate the beginning of a special declaration tag name.
		/// </summary>
		/// <value>The character used to indicate the beginning of a special declaration tag name.</value>
		public char TagSpecialDeclarationStartChar { get; set; } = '!';
		/// <summary>
		/// Gets or sets the character used to indicate the beginning and ending of a comment special declaration tag.
		/// </summary>
		/// <value>The character used to indicate the beginning and ending of a comment special declaration tag.</value>
		public string TagSpecialDeclarationCommentStart { get; set; } = "--";
		/// <summary>
		/// Gets or sets the character used to indicate the end of a tag.
		/// </summary>
		/// <value>The character used to indicate the end of a tag.</value>
		public char TagEndChar { get; set; } = '>';
		/// <summary>
		/// Gets or sets the character placed before the tag name used to indicate the closing of a tag.
		/// </summary>
		/// <value>The character placed before the tag name used to indicate the closing of a tag.</value>
		public char TagCloseChar { get; set; } = '/';
		/// <summary>
		/// Gets or sets the character used to indicate a preprocessor tag.
		/// </summary>
		/// <value>The character used to indicate a preprocessor tag.</value>
		public char PreprocessorChar { get; set; } = '?';
		/// <summary>
		/// Gets or sets the character used to separate a tag namespace from a tag name.
		/// </summary>
		/// <value>The character used to separate a tag namespace from a tag name.</value>
		public char TagNamespaceSeparatorChar { get; set; } = ':';
		/// <summary>
		/// Gets or sets the character used to separate an attribute value from an attribute name.
		/// </summary>
		/// <value>The character used to separate an attribute value from an attribute name.</value>
		public char AttributeNameValueSeparatorChar { get; set; } = '=';
		/// <summary>
		/// Gets or sets the character used to indicate the beginning of an entity reference.
		/// </summary>
		/// <value>The character used to indicate the beginning of an entity reference.</value>
		public char EntityBeginChar { get; set; } = '&';
		/// <summary>
		/// Gets or sets the character used to indicate the end of an entity reference.
		/// </summary>
		/// <value>The character used to indicate the end of an entity reference.</value>
		public char EntityEndChar { get; set; } = ';';

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.DataFormats.Markup.XML.XMLDataFormatSettings"/> is standalone.
		/// </summary>
		/// <value><c>true</c> if is standalone; otherwise, <c>false</c>.</value>
		public bool IsStandalone { get; set; } = false;
		/// <summary>
		/// Gets a collection of <see cref="string" />s that represent names of tags that do not require an ending tag in order to be closed.
		/// </summary>
		/// <value>The names of tags that do not require an ending tag in order to be closed.</value>
		public System.Collections.Specialized.StringCollection AutoCloseTagNames { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// Gets a collection of <see cref="string" />s that represent translations from entity names to text values.
		/// </summary>
		/// <value>The translations from entity names to text values.</value>
		public BidirectionalDictionary<string, string> Entities { get; } = new BidirectionalDictionary<string, string>();

		/// <summary>
		/// Gets or sets the character used to indicate the beginning of a CDATA section.
		/// </summary>
		/// <value>The character used to indicate the beginning of a CDATA section.</value>
		public char CDataBeginChar { get; set; } = '[';
		/// <summary>
		/// Gets or sets the character used to indicate the end of a CDATA section.
		/// </summary>
		/// <value>The character used to indicate the end of a CDATA section.</value>
		public char CDataEndChar { get; set; } = ']';

		/// <summary>
		/// Determines whether to insert tabs and spaces to "pretty-print" the output XML.
		/// </summary>
		public bool PrettyPrint { get; set; } = true;
	}
}
