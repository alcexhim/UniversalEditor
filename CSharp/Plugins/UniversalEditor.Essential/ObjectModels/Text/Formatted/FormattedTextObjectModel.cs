using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public class FormattedTextObjectModel : ObjectModel, IFormattedTextItemParent
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Formatted Text Document";
				_omr.Path = new string[] { "General", "Text", "Formatted" };
			}
			return _omr;
		}

		private FormattedTextFont mvarDefaultFont = null;
		public FormattedTextFont DefaultFont { get { return mvarDefaultFont; } set { mvarDefaultFont = value; } }

		private FormattedTextFont.FormattedTextFontCollection mvarFonts = new FormattedTextFont.FormattedTextFontCollection();
		public FormattedTextFont.FormattedTextFontCollection Fonts { get { return mvarFonts; } }

		private FormattedTextItem.FormattedTextItemCollection mvarItems = new FormattedTextItem.FormattedTextItemCollection();
		public FormattedTextItem.FormattedTextItemCollection Items { get { return mvarItems; } }

		public override void Clear()
		{
			mvarFonts.Clear();
			mvarItems.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FormattedTextObjectModel clone = (where as FormattedTextObjectModel);
			foreach (FormattedTextFont font in mvarFonts)
			{
				clone.Fonts.Add(font.Clone() as FormattedTextFont);
			}
			foreach (FormattedTextItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
		}
	}
}
