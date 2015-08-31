using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors
{
	public partial class PlainTextEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PlainTextObjectModel));
			}
			return _er;
		}

		public PlainTextEditor()
		{
			InitializeComponent();
			txt.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 10);
		}
		
		private int mvarSelectionStart = 0;

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			InhibitUndo = true;

			PlainTextObjectModel text = (ObjectModel as PlainTextObjectModel);
			txt.Text = text.Text;

			if (mvarSelectionStart >= txt.Text.Length) mvarSelectionStart = txt.Text.Length;
			txt.SelectionStart = mvarSelectionStart;

			InhibitUndo = false;
		}
		protected override void OnObjectModelSaving(CancelEventArgs e)
		{
			base.OnObjectModelSaving(e);

			PlainTextObjectModel text = (ObjectModel as PlainTextObjectModel);
			text.Text = txt.Text;
		}

		private void txt_KeyDown(object sender, KeyEventArgs e)
		{
			char c = (char)e.KeyValue;
			if ((c >= '!' && c <= '~') || e.KeyCode == Keys.Back || e.KeyCode == Keys.Return)
			{
				mvarSelectionStart = txt.SelectionStart;
				if (e.KeyCode == Keys.Return || txt.Text == String.Empty)
				{
					EndEdit();
					BeginEdit("Text", txt.Text);
				}

				if (mvarSelectionStart >= txt.Text.Length) mvarSelectionStart = txt.Text.Length;
				txt.SelectionStart = mvarSelectionStart;
			}
		}

		private void txt_TextChanged(object sender, EventArgs e)
		{
			PlainTextObjectModel text = (ObjectModel as PlainTextObjectModel);
			text.Text = txt.Text;

			// notify the object model that it's being edited
			OnDocumentEdited(EventArgs.Empty);
		}
	}
}
