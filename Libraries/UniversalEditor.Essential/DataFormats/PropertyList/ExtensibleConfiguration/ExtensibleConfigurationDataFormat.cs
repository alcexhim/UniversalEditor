//
//  ExtensibleConfigurationDataFormat.cs - provides a DataFormat for manipulating data in Mike Becker's Software Extensible Configuration (XNI) format
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

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating data in Mike Becker's Software Extensible Configuration (XNI) format.
	/// </summary>
	/// <remarks>
	/// I first wrote the code for parsing a nested group configuration file in Visual Basic 6 back in middle school. This arguably was the first DataFormat
	/// that the original version of Universal Editor supported (way back before it was called Universal Editor). It's designed to allow multiple syntactically-
	/// nested groups in a plain text file similar to an INI file but with C-like brace syntax for indicating start and end groups.
	/// </remarks>
	public class ExtensibleConfigurationDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		public int IndentLength { get; set; } = 4;

		/// <summary>
		/// Represents settings for the <see cref="ExtensibleConfigurationDataFormat" /> parser.
		/// </summary>
		/// <value>The settings for the <see cref="ExtensibleConfigurationDataFormat" /> parser.</value>
		protected ExtensibleConfigurationSettings Settings { get; } = new ExtensibleConfigurationSettings();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = objectModel as PropertyListObjectModel;
			Reader tr = base.Accessor.Reader;
			string nextString = string.Empty;
			string nextPropertyName = string.Empty;
			bool insideQuotedString = false;
			bool escaping = false;
			Group nextGroup = null;

			// true if a real char (non-whitespace) was found; false otherwise
			bool foundRealChar = false;

			while (!tr.EndOfStream)
			{
				if (nextString.StartsWith(Settings.SingleLineCommentStart))
				{
					string comment = tr.ReadLine();
				}
				if (nextString.StartsWith(Settings.MultiLineCommentStart))
				{
					string comment = tr.ReadUntil(Settings.MultiLineCommentEnd);
					string cmntend = tr.ReadFixedLengthString(Settings.MultiLineCommentEnd.Length);
					nextString = String.Empty;
					continue;
				}

				char nextChar = tr.ReadChar();
				if (!foundRealChar)
				{
					if (!Char.IsWhiteSpace(nextChar))
					{
						foundRealChar = true;
					}
					else
					{
						continue;
					}
				}

				if (insideQuotedString)
				{
					if (nextChar == '"')
					{
						if (!escaping)
						{
							insideQuotedString = false;
							continue;
						}
					}
					else
					{
						if (nextChar == '\\')
						{
							if (!escaping)
							{
								escaping = true;
								continue;
							}
						}
					}
					nextString += nextChar;
					escaping = false;
				}
				else
				{
					char c = nextChar;
					string cw = c.ToString();
					if (c == '"')
					{
						insideQuotedString = !insideQuotedString;
						continue;
					}
					else if (cw == Settings.PropertyNameValueSeparator && (Settings.AllowTopLevelProperties || nextGroup != null))
					{
						nextPropertyName = nextString;
						nextString = string.Empty;
						foundRealChar = false;
					}
					else if (cw == Settings.PropertySeparator && (Settings.AllowTopLevelProperties || nextGroup != null))
					{
						if (nextPropertyName != null)
						{
							nextPropertyName = nextPropertyName.Trim();
							nextString = nextString.Trim();

							if (nextGroup != null)
							{
								nextGroup.Items.AddProperty(nextPropertyName, nextString);
							}
							else
							{
								plom.Items.AddProperty(nextPropertyName, nextString);
							}
						}
						nextPropertyName = string.Empty;
						nextString = string.Empty;
						foundRealChar = false;
					}
					else if (cw == Settings.GroupStart)
					{
						Group group = new Group();
						group.Name = nextString.Trim();
						nextString = string.Empty;
						if (nextGroup != null)
						{
							nextGroup.Items.Add(group);
						}
						else
						{
							plom.Items.Add(group);
						}
						nextGroup = group;
					}
					else if (cw == Settings.GroupEnd)
					{
						if (nextGroup != null)
						{
							nextGroup = nextGroup.Parent;
						}
					}
					else
					{
						nextString += nextChar;
						continue;
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = objectModel as PropertyListObjectModel;
			Writer tw = base.Accessor.Writer;
			foreach (PropertyListItem p in plom.Items)
			{
				if (p is Property)
				{
					tw.Write(Settings.PropertyNamePrefix);
					tw.Write(p.Name);
					tw.Write(Settings.PropertyNameSuffix);
					tw.Write(Settings.PropertyNameValueSeparator);
					tw.Write(Settings.PropertyValuePrefix);
					tw.WriteFixedLengthString((p as Property).Value.ToString());
					tw.Write(Settings.PropertyValueSuffix);
				}
				else if (p is Group)
				{
					this.WriteGroup(tw, (p as Group), 0);
				}
			}
			tw.Flush();
			tw.Close();
		}
		private void WriteGroup(Writer tw, Group group, int indent)
		{
			string indents = new string(' ', indent * this.IndentLength);
			tw.Write(indents + group.Name);
			if (Settings.InlineGroupStart)
			{
				tw.Write(' ');
				tw.Write(Settings.GroupStart);
				tw.WriteLine();
			}
			else
			{
				tw.WriteLine();
				tw.WriteLine(indents + Settings.GroupStart);
			}
			foreach (PropertyListItem p in group.Items)
			{
				if (p is Property)
				{
					tw.WriteLine(string.Concat(new object[]
					{
					indents,
					new string(' ', this.IndentLength),
					p.Name,
					Settings.PropertyNameValueSeparator,
					Settings.PropertyValuePrefix,
					(p as Property).Value,
					Settings.PropertyValueSuffix,
					Settings.PropertySeparator
					}));
				}
				else if (p is Group)
				{
					this.WriteGroup(tw, p as Group, indent + 1);
				}
			}
			tw.WriteLine(indents + Settings.GroupEnd);
		}
	}
}
