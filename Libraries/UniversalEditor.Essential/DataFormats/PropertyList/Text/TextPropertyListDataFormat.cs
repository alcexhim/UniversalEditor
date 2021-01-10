//
//  TextPropertyListDataFormat.cs - provides a DataFormat for manipulating property lists in Universal Editor's Text Property List format
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

namespace UniversalEditor.DataFormats.PropertyList.Text
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating property lists in Universal Editor's Text Property List format.
	/// </summary>
	public class TextPropertyListDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Represents settings for the <see cref="TextPropertyListDataFormat" /> parser.
		/// </summary>
		/// <value>The settings for the <see cref="TextPropertyListDataFormat" /> parser.</value>
		public TextPropertyListSettings Settings { get; } = new TextPropertyListSettings();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = IO.Encoding.UTF8;
			IO.Reader tr = new IO.Reader(base.Accessor);
			string signature = tr.ReadLine();
			if (!signature.StartsWith("#TPL-1.0")) throw new InvalidDataFormatException();

			Group parentGroup = null;

			plom.Title = signature.Substring(8);

			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				line = line.Trim();
				line = TrimComments(line);
				if (String.IsNullOrEmpty(line)) continue;

				if (line.StartsWith("Begin "))
				{
					string blockName = line.Substring(6);
					Group group = new Group();
					group.Name = blockName;
					if (parentGroup != null)
					{
						parentGroup.Items.Add(group);
					}
					else
					{
						plom.Items.Add(group);
					}
					parentGroup = group;
				}
				else if (line.StartsWith("End "))
				{
					string blockName = line.Substring(4);
					if (parentGroup != null && parentGroup.Name == blockName)
					{
						parentGroup = parentGroup.Parent;
					}
					else if (parentGroup != null)
					{
						throw new InvalidDataFormatException("End block name \"" + blockName + "\" does not match start block name \"" + parentGroup.Name + "\"");
					}
					else
					{
						throw new InvalidDataFormatException("End block name \"" + blockName + "\" encountered when block was not started");
					}
				}
				else
				{
					string[] splits = line.Split(Settings.PropertyNameValueSeparators, Settings.IgnoreBegin, Settings.IgnoreEnd, StringSplitOptions.RemoveEmptyEntries, 2, true);
					string name = splits[0].Trim();
					string value = null;
					if (splits.Length > 1) value = splits[1];

					Property property = new Property(name, value);
					if (parentGroup != null)
					{
						parentGroup.Items.Add(property);
					}
					else
					{
						plom.Items.Add(property);
					}
				}
			}
		}

		private string TrimComments(string line)
		{
			string line2 = (line.Clone() as string);
			foreach (string comment in Settings.CommentSignals)
			{
				int indexof = line2.IndexOf(comment);
				if (indexof > -1)
				{
					line2 = line2.Substring(0, indexof);
				}
			}
			return line2;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = UniversalEditor.IO.Encoding.UTF8;
			IO.Writer tw = new IO.Writer(base.Accessor);
			tw.WriteLine("#TPL-1.0 " + plom.Title);

			foreach (PropertyListItem item in plom.Items)
			{
				if (item is Group)
				{
					WriteGroup(tw, item as Group);
				}
				else if (item is Property)
				{
					WriteProperty(tw, item as Property);
				}
			}

			tw.Flush();
		}

		private void WriteGroup(IO.Writer tw, Group group, int indent = 0)
		{
			string indentStr = new string('\t', indent);
			tw.WriteLine(indentStr + "Begin " + group.Name);
			foreach (PropertyListItem item in group.Items)
			{
				if (item is Group)
				{
					WriteGroup(tw, item as Group, indent + 1);
				}
				else if (item is Property)
				{
					WriteProperty(tw, item as Property, indent + 1);
				}
			}
			tw.WriteLine(indentStr + "End " + group.Name);
		}

		private void WriteProperty(IO.Writer tw, Property property, int indent = 0)
		{
			string indentStr = new string('\t', indent);
			tw.WriteLine(indentStr + property.Name + "\t" + property.Value);
		}
	}
}
