using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(TextBox))]
	public class TextBoxImplementation : WindowsFormsNativeImplementation, ITextBoxImplementation
	{
		public TextBoxImplementation(Engine engine, TextBox control) : base(engine, control)
		{
		}

		public void InsertText(string content)
		{
			TextBox ctl = (Control as TextBox);
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).Text = content;
		}

		protected override string GetControlTextInternal(Control control)
		{
			return ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).Text;
		}
		protected override void SetControlTextInternal(Control control, string text)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).Text = text;
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Contract.Assert(control is TextBox);

			TextBox ctl = (control as TextBox);

			System.Windows.Forms.TextBox handle = new System.Windows.Forms.TextBox();

			// we don't use any auto complete source by default, but as Raymond Chen suggests, it automagically grants us proper ctrl+backspace behavior
			handle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			handle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;

			handle.Multiline = ctl.Multiline;
			handle.TextChanged += handle_TextChanged;
			handle.Text = ctl.Text;
			if (ctl.MaxLength > -1)
			{
				handle.MaxLength = ctl.MaxLength;
			}
			handle.UseSystemPasswordChar = ctl.UseSystemPasswordChar;
			handle.ReadOnly = !ctl.Editable;
			return new WindowsFormsNativeControl(handle);
		}

		private void handle_TextChanged(object sender, EventArgs e)
		{
			TextBox ctl = (Control as TextBox);
			if (ctl == null) return;
			InvokeMethod(ctl, "OnChanged", EventArgs.Empty);
		}

		public int GetSelectionStart()
		{
			return ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).SelectionStart;
		}
		public void SetSelectionStart(int pos)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).SelectionStart = pos;
		}
		public int GetSelectionLength()
		{
			return ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).SelectionLength;
		}
		public void SetSelectionLength(int len)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).SelectionLength = len;
		}

		public string GetSelectedText()
		{
			return ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).SelectedText;
		}

		public void SetSelectedText(string text)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TextBox).SelectedText = text;
		}
	}
}
