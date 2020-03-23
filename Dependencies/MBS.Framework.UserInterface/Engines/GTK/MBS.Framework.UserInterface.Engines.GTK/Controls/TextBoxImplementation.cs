using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(TextBox))]
	public class TextBoxImplementation : GTKNativeImplementation, ITextBoxImplementation
	{
		public TextBoxImplementation(Engine engine, Control control) : base(engine, control)
		{
			TextBox_Changed_Handler = new Internal.GObject.Delegates.GCallback(TextBox_Changed);
			TextBuffer_Changed_Handler = new Internal.GObject.Delegates.GCallbackV1I(TextBuffer_Changed);
		}

		public void InsertText(string content)
		{
			TextBox ctl = (Control as TextBox);
			if (ctl.Multiline)
			{
				IntPtr hScrolledWindow = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
				IntPtr hList = Internal.GTK.Methods.GtkContainer.gtk_container_get_children(hScrolledWindow);
				IntPtr hTextBox = Internal.GLib.Methods.g_list_nth_data(hList, 0);

				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);
				Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_insert_at_cursor(hBuffer, content, content.Length);
			}
		}

		protected override string GetControlTextInternal(Control control)
		{
			IntPtr handle = (Engine.GetHandleForControl(control) as GTKNativeControl).Handle;

			GType typeEntry = Internal.GTK.Methods.GtkEntry.gtk_entry_get_type();
			bool isTextBox = Internal.GObject.Methods.G_TYPE_CHECK_INSTANCE_TYPE(handle, typeEntry);

			TextBox ctl = (control as TextBox);
			/*
			if (textboxChanged.ContainsKey (handle)) {
				if (!textboxChanged [handle])
					return null;
			}
			*/
			// textboxChanged [handle] = false;

			string value = String.Empty;

			if (ctl.Multiline)
			{
				// handle points to the ScrolledWindow
				IntPtr hTextBox = Internal.GTK.Methods.GtkContainer.gtk_container_get_focus_child(handle);
				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);

				Internal.GTK.Structures.GtkTextIter hStartIter = new Internal.GTK.Structures.GtkTextIter();
				Internal.GTK.Structures.GtkTextIter hEndIter = new Internal.GTK.Structures.GtkTextIter();

				Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_start_iter(hBuffer, ref hStartIter);
				Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_end_iter(hBuffer, ref hEndIter);

				value = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_text(hBuffer, ref hStartIter, ref hEndIter, true);
			}
			else
			{
				ushort textLength = Internal.GTK.Methods.GtkEntry.gtk_entry_get_text_length(handle);
				if (textLength > 0)
				{
					try
					{
						value = Internal.GTK.Methods.GtkEntry.gtk_entry_get_text(handle);
					}
					catch (Exception ex)
					{
						Console.Error.WriteLine(ex.Message);
					}
				}
			}
			return value;
		}
		protected override void SetControlTextInternal(Control control, string text)
		{
			if (text == null)
				text = String.Empty;

			TextBox ctl = (control as TextBox);
			if (ctl.Multiline)
			{
				IntPtr hScrolledWindow = (Engine.GetHandleForControl(control) as GTKNativeControl).Handle;
				IntPtr hList = Internal.GTK.Methods.GtkContainer.gtk_container_get_children(hScrolledWindow);
				IntPtr hTextBox = Internal.GLib.Methods.g_list_nth_data(hList, 0);

				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);

				text = text.Replace('\0', ' ');
				Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_set_text(hBuffer, text, text.Length);
			}
			else
			{
				// this isn't working.. why not?
				IntPtr hTextBox = (Engine.GetHandleForControl(control) as GTKNativeControl).Handle;
				Internal.GTK.Methods.GtkEntry.gtk_entry_set_text(hTextBox, text);
			}
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Contract.Assert(control is TextBox);

			TextBox ctl = (control as TextBox);
			IntPtr handle = IntPtr.Zero;
			if (ctl.Multiline)
			{
				handle = Internal.GTK.Methods.GtkTextView.gtk_text_view_new();
			}
			else
			{
				handle = Internal.GTK.Methods.GtkEntry.gtk_entry_new();
				Internal.GObject.Methods.g_signal_connect(handle, "changed", TextBox_Changed_Handler);
			}

			string ctlText = ctl.Text;
			if (ctlText != null)
			{
				if (ctl.Multiline)
				{
					IntPtr hTextTagTable = Internal.GTK.Methods.GtkTextTagTable.gtk_text_tag_table_new();
					IntPtr hBuffer = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_new(hTextTagTable);
					Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_set_text(hBuffer, ctlText, ctlText.Length);
					Internal.GTK.Methods.GtkTextView.gtk_text_view_set_buffer(handle, hBuffer);
					Internal.GTK.Methods.GtkTextView.gtk_text_view_set_wrap_mode(handle, Internal.GTK.Constants.GtkWrapMode.Word);
					_TextBoxForBuffer[hBuffer] = ctl;
					Internal.GObject.Methods.g_signal_connect(hBuffer, "changed", TextBuffer_Changed_Handler);
				}
				else
				{
					Internal.GTK.Methods.GtkEntry.gtk_entry_set_text(handle, ctlText);
				}
			}

			if (ctl.MaxLength > -1)
			{
				Internal.GTK.Methods.GtkEntry.gtk_entry_set_max_length(handle, ctl.MaxLength);
			}
			if (ctl.WidthChars > -1)
			{
				Internal.GTK.Methods.GtkEntry.gtk_entry_set_width_chars(handle, ctl.WidthChars);
			}
			Internal.GTK.Methods.GtkEntry.gtk_entry_set_activates_default(handle, true);
			Internal.GTK.Methods.GtkEntry.gtk_entry_set_visibility(handle, !ctl.UseSystemPasswordChar);

			Internal.GTK.Methods.GtkEditable.gtk_editable_set_editable(handle, ctl.Editable);

			if (ctl.Multiline)
			{
				IntPtr hHAdjustment = Internal.GTK.Methods.GtkAdjustment.gtk_adjustment_new(0, 0, 100, 1, 10, 10);
				IntPtr hVAdjustment = Internal.GTK.Methods.GtkAdjustment.gtk_adjustment_new(0, 0, 100, 1, 10, 10);

				IntPtr hScrolledWindow = Internal.GTK.Methods.GtkScrolledWindow.gtk_scrolled_window_new(hHAdjustment, hVAdjustment);
				Internal.GTK.Methods.GtkContainer.gtk_container_add(hScrolledWindow, handle);
				return new GTKNativeControl(hScrolledWindow);
			}
			else
			{
				return new GTKNativeControl(handle);
			}
		}

		private Dictionary<IntPtr, bool> textboxChanged = new Dictionary<IntPtr, bool>();
		private Internal.GObject.Delegates.GCallback TextBox_Changed_Handler;
		private Internal.GObject.Delegates.GCallbackV1I TextBuffer_Changed_Handler;
		private void TextBox_Changed(IntPtr handle, IntPtr data)
		{
			TextBox ctl = Control as TextBox;
			if (ctl == null)
				return;

			textboxChanged[handle] = true;
			InvokeMethod(ctl, "OnChanged", EventArgs.Empty);
		}

		private Dictionary<IntPtr, TextBox> _TextBoxForBuffer = new Dictionary<IntPtr, TextBox>();
		private void TextBuffer_Changed(IntPtr handle)
		{
			if (_TextBoxForBuffer.ContainsKey(handle))
			{
				TextBox ctl = _TextBoxForBuffer[handle];
				if (ctl == null)
					return;

				textboxChanged[handle] = true;
				InvokeMethod(ctl, "OnChanged", EventArgs.Empty);
			}
		}

		public int GetSelectionStart()
		{
			TextBox ctl = (Control as TextBox);
			if (ctl.Multiline)
			{
				IntPtr hScrolledWindow = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
				IntPtr hList = Internal.GTK.Methods.GtkContainer.gtk_container_get_children(hScrolledWindow);
				IntPtr hTextBox = Internal.GLib.Methods.g_list_nth_data(hList, 0);

				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);
				Internal.GTK.Structures.GtkTextIter iterStart = new Internal.GTK.Structures.GtkTextIter();
				Internal.GTK.Structures.GtkTextIter iterEnd = new Internal.GTK.Structures.GtkTextIter();
				bool success = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_selection_bounds(hBuffer, ref iterStart, ref iterEnd);
				if (success)
				{
					return Internal.GTK.Methods.GtkTextIter.gtk_text_iter_get_offset(ref iterStart);
				}
				return 0;
			}
			return -1;
		}
		public void SetSelectionStart(int pos)
		{
			throw new NotImplementedException();
		}
		public int GetSelectionLength()
		{
			TextBox ctl = (Control as TextBox);
			if (ctl.Multiline)
			{
				IntPtr hScrolledWindow = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
				IntPtr hList = Internal.GTK.Methods.GtkContainer.gtk_container_get_children(hScrolledWindow);
				IntPtr hTextBox = Internal.GLib.Methods.g_list_nth_data(hList, 0);

				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);
				Internal.GTK.Structures.GtkTextIter iterStart = new Internal.GTK.Structures.GtkTextIter();
				Internal.GTK.Structures.GtkTextIter iterEnd = new Internal.GTK.Structures.GtkTextIter();
				bool success = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_selection_bounds(hBuffer, ref iterStart, ref iterEnd);
				if (success)
				{
					int start = Internal.GTK.Methods.GtkTextIter.gtk_text_iter_get_offset(ref iterStart);
					int end = Internal.GTK.Methods.GtkTextIter.gtk_text_iter_get_offset(ref iterEnd);
					return end - start;
				}
			}
			return 0;
		}
		public void SetSelectionLength(int len)
		{
			throw new NotImplementedException();
		}

		public string GetSelectedText()
		{
			TextBox ctl = (Control as TextBox);
			if (ctl.Multiline)
			{
				IntPtr hScrolledWindow = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
				IntPtr hList = Internal.GTK.Methods.GtkContainer.gtk_container_get_children(hScrolledWindow);
				IntPtr hTextBox = Internal.GLib.Methods.g_list_nth_data(hList, 0);

				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);
				Internal.GTK.Structures.GtkTextIter iterStart = new Internal.GTK.Structures.GtkTextIter();
				Internal.GTK.Structures.GtkTextIter iterEnd = new Internal.GTK.Structures.GtkTextIter();
				bool success = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_selection_bounds(hBuffer, ref iterStart, ref iterEnd);
				if (success)
				{
					string value = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_text(hBuffer, ref iterStart, ref iterEnd, true);
					return value;
				}
			}
			return null;
		}

		public void SetSelectedText(string text)
		{
			TextBox ctl = (Control as TextBox);
			if (ctl.Multiline)
			{
				IntPtr hScrolledWindow = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
				IntPtr hList = Internal.GTK.Methods.GtkContainer.gtk_container_get_children(hScrolledWindow);
				IntPtr hTextBox = Internal.GLib.Methods.g_list_nth_data(hList, 0);

				IntPtr hBuffer = Internal.GTK.Methods.GtkTextView.gtk_text_view_get_buffer(hTextBox);
				Internal.GTK.Structures.GtkTextIter iterStart = new Internal.GTK.Structures.GtkTextIter();
				Internal.GTK.Structures.GtkTextIter iterEnd = new Internal.GTK.Structures.GtkTextIter();
				bool success = Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_get_selection_bounds(hBuffer, ref iterStart, ref iterEnd);

				Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_delete(hBuffer, ref iterStart, ref iterEnd);
				if (!String.IsNullOrEmpty(text))
				{
					Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_insert(hBuffer, ref iterStart, text, text.Length);
				}
			}
		}
	}
}
