using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Text.Formatted.RichText;
using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors
{
	public partial class FormattedTextEditor : Editor
	{
		public FormattedTextEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Formatted text";
				_er.SupportedObjectModels.Add(typeof(FormattedTextObjectModel));
			}
			return _er;
		}

		private static RTFDataFormat rtf = new RTFDataFormat();

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			FormattedTextObjectModel text = (ObjectModel as FormattedTextObjectModel);

			txt.Text = String.Empty;

			if (text == null) return;

			FormattedTextFont fnt = new FormattedTextFont();
			fnt.Family = FormattedTextFontFamily.Roman;
			fnt.Name = "Times New Roman";
			text.Fonts.Add(fnt);

			text.DefaultFont = text.Fonts[0];

			text.Items.Add(new UniversalEditor.ObjectModels.Text.Formatted.Items.FormattedTextItemLiteral("\r\n"));

			StringAccessor sa = new StringAccessor();
			Document.Save(text, rtf, sa);

			string rtftext = sa.ToString();
			txt.Rtf = rtftext;
		}

		private void txt_SelectionChanged(object sender, EventArgs e)
		{

		}

		private void txt_TextChanged(object sender, EventArgs e)
		{
			FormattedTextObjectModel text = (ObjectModel as FormattedTextObjectModel);
			if (text == null) return;

			this.BeginEdit();

			StringAccessor sa = new StringAccessor(txt.Rtf);
			Document.Load(text, rtf, sa);

			this.EndEdit();
		}
	}
}
