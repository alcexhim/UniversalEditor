//
//  ExtensibleConfigurationSettings.cs - represents settings for the ExtensibleConfigurationDataFormat parser
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

namespace UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration
{
	/// <summary>
	/// Represents settings for the <see cref="ExtensibleConfigurationDataFormat" /> parser.
	/// </summary>
	public class ExtensibleConfigurationSettings
	{
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix a property name.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix a property name.</value>
		public string PropertyNamePrefix { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix a property name.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix a property name.</value>
		public string PropertyNameSuffix { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate the property name from the property value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to separate the property name from the property value.</value>
		public string PropertyNameValueSeparator { get; set; } = "=";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix a property value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix a property value.</value>
		public string PropertyValuePrefix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix a property value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix a property value.</value>
		public string PropertyValueSuffix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate one property declaration from another.
		/// </summary>
		/// <value>The <see cref="string" /> with which to separate one property declaration from another.</value>
		public string PropertySeparator { get; set; } = ";";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to indicate the start of a multi-line comment.
		/// </summary>
		/// <value>The <see cref="string" /> with which to indicate the start of a multi-line comment.</value>
		public string MultiLineCommentStart { get; set; } = "/*";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to indicate the end of a multi-line comment.
		/// </summary>
		/// <value>The <see cref="string" /> with which to indicate the end of a multi-line comment.</value>
		public string MultiLineCommentEnd { get; set; } = "*/";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to indicate the start of a single-line comment.
		/// </summary>
		/// <value>The <see cref="string" /> with which to indicate the start of a single-line comment.</value>
		public string SingleLineCommentStart { get; set; } = "//";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to indicate the start of a property group.
		/// </summary>
		/// <value>The <see cref="string" /> with which to indicate the start of a property group.</value>
		public string GroupStart { get; set; } = "{";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to indicate the end of a property group.
		/// </summary>
		/// <value>The <see cref="string" /> with which to indicate the end of a property group.</value>
		public string GroupEnd { get; set; } = "}";
		/// <summary>
		/// Determines whether the group start character (default '{') should be placed on the same line as the group name.
		/// </summary>
		public bool InlineGroupStart { get; set; } = true;
		/// <summary>
		/// Determines whether top-level properties (i.e., those outside of a group definition) are allowed.
		/// </summary>
		public bool AllowTopLevelProperties { get; set; } = true;
	}
}
