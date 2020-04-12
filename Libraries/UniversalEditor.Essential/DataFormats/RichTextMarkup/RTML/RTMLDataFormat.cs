//
//  RTMLDataFormat.cs - provides a DataFormat for manipulating markup in Rich Text Format
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

using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.RichTextMarkup;

namespace UniversalEditor.DataFormats.RichTextMarkup.RTML
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating markup in Rich Text Format.
	/// </summary>
	public class RTMLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(RichTextMarkupObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public RTMLDataFormat()
		{
			Settings.GroupBeginChar = '{';
			Settings.GroupEndChar = '}';
			Settings.TagBeginChar = '\\';
		}

		/// <summary>
		/// Represents settings for the <see cref="RTMLDataFormat" /> parser.
		/// </summary>
		/// <value>The settings for the <see cref="RTMLDataFormat" /> parser.</value>
		public RTMLSettings Settings { get; } = new RTMLSettings();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = base.Accessor.Reader;

			RichTextMarkupObjectModel rtml = (objectModel as RichTextMarkupObjectModel);
			if (rtml == null) throw new ObjectModelNotSupportedException();

			RichTextMarkupItemGroup currentGroup = null;
			StringBuilder sbnext = new StringBuilder();

			while (!reader.EndOfStream)
			{
				char c = reader.ReadChar();
				if (c == ' ')
				{
					string content = reader.ReadStringUntilAny(new char[] { Settings.TagBeginChar, Settings.GroupBeginChar, Settings.GroupEndChar });
					rtml.Items.Add(new RichTextMarkupItemLiteral(content));
				}
				else if (c == Settings.GroupBeginChar)
				{
					if (currentGroup == null)
					{
						RichTextMarkupItemGroup group = new RichTextMarkupItemGroup();
						rtml.Items.Add(group);
						currentGroup = group;
					}
					else
					{
						RichTextMarkupItemGroup group = new RichTextMarkupItemGroup();
						currentGroup.Items.Add(group);
						currentGroup = group;
					}
				}
				else if (c == Settings.GroupEndChar)
				{
					if (currentGroup == null) throw new InvalidDataFormatException("Attempted to close RTML group when none was opened");

					currentGroup = currentGroup.Parent;
				}
				else if (c == Settings.TagBeginChar)
				{
					string name = reader.ReadStringUntilAny(new char[] { Settings.TagBeginChar, ' ', Settings.GroupBeginChar, Settings.GroupEndChar });
					if (currentGroup == null)
					{
						rtml.Items.Add(new RichTextMarkupItemTag(name));
					}
					else
					{
						currentGroup.Items.Add(new RichTextMarkupItemTag(name));
					}
				}
				else
				{
					sbnext.Append(c);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer writer = base.Accessor.Writer;

			RichTextMarkupObjectModel rtml = (objectModel as RichTextMarkupObjectModel);
			if (rtml == null) throw new ObjectModelNotSupportedException();

			foreach (RichTextMarkupItem item in rtml.Items)
			{
				RenderItem(writer, item);
			}
		}

		private RichTextMarkupItem lastItemRendered = null;

		private void RenderItem(Writer writer, RichTextMarkupItem item)
		{
			if (item is RichTextMarkupItemGroup)
			{
				RichTextMarkupItemGroup itm = (item as RichTextMarkupItemGroup);
				writer.Write("{");
				foreach (RichTextMarkupItem item1 in itm.Items)
				{
					RenderItem(writer, item1);
				}
				writer.Write("}");
			}
			else if (item is RichTextMarkupItemTag)
			{
				RichTextMarkupItemTag itm = (item as RichTextMarkupItemTag);
				writer.Write("\\");
				writer.Write(itm.Name);
			}
			else if (item is RichTextMarkupItemLiteral)
			{
				RichTextMarkupItemLiteral itm = (item as RichTextMarkupItemLiteral);
				if (!(lastItemRendered is RichTextMarkupItemLiteral)) writer.Write(" ");
				writer.Write(itm.Content);
			}
			lastItemRendered = item;
		}
	}
}
