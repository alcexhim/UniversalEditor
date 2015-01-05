using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.RichTextMarkup;

namespace UniversalEditor.DataFormats.RichTextMarkup.RTML
{
	/// <summary>
	/// Data format for expressing Rich Text Format files as MarkupObjectModel.
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
			mvarSettings.GroupBeginChar = '{';
			mvarSettings.GroupEndChar = '}';
			mvarSettings.TagBeginChar = '\\';
		}

		private RTMLSettings mvarSettings = new RTMLSettings();
		public RTMLSettings Settings { get { return mvarSettings; } }

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
					string content = reader.ReadStringUntilAny(new char[] { mvarSettings.TagBeginChar, mvarSettings.GroupBeginChar, mvarSettings.GroupEndChar });
					rtml.Items.Add(new RichTextMarkupItemLiteral(content));
				}
				else if (c == mvarSettings.GroupBeginChar)
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
				else if (c == mvarSettings.GroupEndChar)
				{
					if (currentGroup == null) throw new InvalidDataFormatException("Attempted to close RTML group when none was opened");

					currentGroup = currentGroup.Parent;
				}
				else if (c == mvarSettings.TagBeginChar)
				{
					string name = reader.ReadStringUntilAny(new char[] { mvarSettings.TagBeginChar, ' ', mvarSettings.GroupBeginChar, mvarSettings.GroupEndChar });
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
