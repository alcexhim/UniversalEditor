//
//  SyntaxTextBoxImplementation.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(SyntaxTextBox))]
	public class SyntaxTextBoxImplementation : GTKNativeImplementation
	{
		public SyntaxTextBoxImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override void SetControlTextInternal(Control control, string text)
		{
			// base.SetControlTextInternal(control, text);
			IntPtr hBuffer = (Handle as GTKNativeControl).GetNamedHandle("TextBuffer");
			if (hBuffer != IntPtr.Zero)
			{
				if (!String.IsNullOrEmpty(text))
					Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_set_text(hBuffer, text, text.Length);
			}
			else
			{
				Console.Error.WriteLine("uwt: SyntaxTextBox: named handle 'TextBuffer' is NULL");
			}
		}

		private NativeControl CreateSyntaxTextBox(Control control)
		{
			SyntaxTextBox ctl = (control as SyntaxTextBox);
			IntPtr handle = IntPtr.Zero;

			IntPtr hLanguageManager = Internal.GTK.Methods.GtkSourceLanguageManager.gtk_source_language_manager_get_default();

			IntPtr hLanguage = Internal.GTK.Methods.GtkSourceLanguageManager.gtk_source_language_manager_get_language(hLanguageManager, "vala");

			IntPtr hBuffer = Internal.GTK.Methods.GtkSourceBuffer.gtk_source_buffer_new(IntPtr.Zero);
			Internal.GTK.Methods.GtkSourceBuffer.gtk_source_buffer_set_language(hBuffer, hLanguage);
			if (!String.IsNullOrEmpty(ctl.Text))
			{
				Internal.GTK.Methods.GtkTextBuffer.gtk_text_buffer_set_text(hBuffer, ctl.Text, ctl.Text.Length);
			}
			handle = Internal.GTK.Methods.GtkSourceView.gtk_source_view_new_with_buffer(hBuffer);

			// setup monospace
			IntPtr hError = IntPtr.Zero;
			IntPtr /*GtkCssProvider*/ provider = Internal.GTK.Methods.GtkCss.gtk_css_provider_new();
			string data = "textview { font-family: Monospace; }";
			Internal.GTK.Methods.GtkCss.gtk_css_provider_load_from_data(provider, data, data.Length, ref hError);

			IntPtr hStyleContext = Internal.GTK.Methods.GtkWidget.gtk_widget_get_style_context(handle);
			Internal.GTK.Methods.GtkStyleContext.gtk_style_context_add_provider(hStyleContext, provider, Internal.GTK.Constants.GtkStyleProviderPriority.Application);
			Internal.GObject.Methods.g_object_unref(provider);

			Console.WriteLine("provider {0}, hStyleContext {1}", provider, hStyleContext);

			return new GTKNativeControl(handle, new System.Collections.Generic.KeyValuePair<string, IntPtr>[] { new System.Collections.Generic.KeyValuePair<string, IntPtr>("TextBuffer", hBuffer) });
		}

		private NativeControl CreatePlainTextBox(Control control)
		{
			TextBoxImplementation impl = new TextBoxImplementation(Engine, control);
			(control as TextBox).Multiline = true;

			NativeControl nc = impl.CreateControl(control);
			return nc;
		}

		protected override NativeControl CreateControlInternal (Control control)
		{
			NativeControl ncSyntaxTextBox = null;
			try
			{
				ncSyntaxTextBox = CreateSyntaxTextBox(control);
			}
			catch (DllNotFoundException)
			{
				ncSyntaxTextBox = CreatePlainTextBox(control);
			}

			popup = new PopupWindow();
			Engine.CreateControl(popup);

			return ncSyntaxTextBox;
		}

		private PopupWindow popup = null;

		protected override void OnKeyDown (MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			base.OnKeyDown (e);

			if (!popup.Visible) {
				popup.Show ();
			}
		}
	}
}

