//
//  JSONSettings.cs - represents settings for the JSONDataFormat parser
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

namespace UniversalEditor.DataFormats.JSON
{
	/// <summary>
	/// Represents settings for the <see cref="JSONDataFormat" /> parser.
	/// </summary>
	public class JSONSettings
	{
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate the object name from the object value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to separate the object name from the object value.</value>
		public string ObjectNameValueSeparator { get; set; } = ":";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix a field name.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix a field name.</value>
		public string FieldNamePrefix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix a field name.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix a field name.</value>
		public string FieldNameSuffix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix an array.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix an array.</value>
		public string ArrayPrefix { get; set; } = "[";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix an array.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix an array.</value>
		public string ArraySuffix { get; set; } = "]";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix an array value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix an array value.</value>
		public string ArrayValuePrefix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix an array value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix an array value.</value>
		public string ArrayValueSuffix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should append a space after an object name.
		/// </summary>
		/// <value><c>true</c> if a space should be appended after an object name; otherwise, <c>false</c>.</value>
		public bool AppendSpaceAfterObjectName { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should append a newline after an object name.
		/// </summary>
		/// <value><c>true</c> if a newline should be appended after an object name; otherwise, <c>false</c>.</value>
		public bool AppendLineAfterObjectName { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should append a newline after the start of an array.
		/// </summary>
		/// <value><c>true</c> if a newline should be appended after the start of an array; otherwise, <c>false</c>.</value>
		public bool AppendLineAfterStartArray { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should append a newline after a value in an array.
		/// </summary>
		/// <value><c>true</c> if a newline should be appended after a value in an array; otherwise, <c>false</c>.</value>
		public bool AppendLineAfterArrayValue { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should append a newline after a field.
		/// </summary>
		/// <value><c>true</c> if a newline should be appended after a field; otherwise, <c>false</c>.</value>
		public bool AppendLineAfterField { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should append a newline after a field name.
		/// </summary>
		/// <value><c>true</c> if a newline should be appended after a field name; otherwise, <c>false</c>.</value>
		public bool AppendLineAfterFieldName { get; set; } = false;
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate fields in an object.
		/// </summary>
		/// <value>The <see cref="string" /> with which to separate fields in an object.</value>
		public string FieldSeparator { get; set; } = ",";
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should indent child fields.
		/// </summary>
		/// <value><c>true</c> if child fields should be indented; otherwise, <c>false</c>.</value>
		public bool IndentChildFields { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="JSONDataFormat" /> should indent array values.
		/// </summary>
		/// <value><c>true</c> if array values should be indented; otherwise, <c>false</c>.</value>
		public bool IndentArrayValues { get; set; } = true;
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix an object name.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix an object name.</value>
		public string ObjectNamePrefix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix an object name.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix an object name.</value>
		public string ObjectNameSuffix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix an object definition.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix an object definition.</value>
		public string ObjectPrefix { get; set; } = "{";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix an object definition.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix an object definition.</value>
		public string ObjectSuffix { get; set; } = "}";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate the field name from the field value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to separate the field name from the field value.</value>
		public string FieldNameValueSeparator { get; set; } = ":";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix a string literal.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix a string literal.</value>
		public string StringLiteralPrefix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix a string literal.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix a string literal.</value>
		public string StringLiteralSuffix { get; set; } = "\"";
	}
}
