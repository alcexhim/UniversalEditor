//
//  GTKClipboard.cs
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
namespace MBS.Framework.UserInterface.Engines.GTK
{
	public class GTKClipboard : Clipboard
	{
		public IntPtr Handle { get; private set; } = IntPtr.Zero;
		public GTKClipboard(IntPtr handle)
		{
			Handle = handle;
			text_received_func_handler = new Action<IntPtr, string, IntPtr>(text_received_func);
		}

		private Action<IntPtr, string, IntPtr> text_received_func_handler;
		private void text_received_func(IntPtr /*GtkClipboard*/ clipboard, string text, IntPtr data)
		{

		}

		protected override void ClearInternal()
		{
			Internal.GTK.Methods.GtkClipboard.gtk_clipboard_clear(Handle);
		}

		protected override bool ContainsTextInternal()
		{
			return Internal.GTK.Methods.GtkClipboard.gtk_clipboard_wait_is_text_available(Handle);
		}
		protected override string GetTextInternal()
		{
			string text = Internal.GTK.Methods.GtkClipboard.gtk_clipboard_wait_for_text(Handle);
			return text;
		}
		protected override void SetTextInternal(string value)
		{
			Internal.GTK.Methods.GtkClipboard.gtk_clipboard_set_text(Handle, value, value.Length);
		}

		protected override bool ContainsFileListInternal()
		{
			if (ContainsText)
			{
				string text = GetText();
				return text.StartsWith("x-special/nautilus-clipboard\n");
			}
			return false;
		}

		protected override CrossThreadData GetContentInternal()
		{
			IntPtr targets = IntPtr.Zero;
			int n_targets = 0;
			bool retval = Internal.GTK.Methods.GtkClipboard.gtk_clipboard_wait_for_targets(Handle, ref targets, ref n_targets);

			if (!retval)
				return null;

			CrossThreadData data = new CrossThreadData();
			return data;
		}
		protected override object GetDataInternal(string format)
		{
			throw new NotImplementedException();
		}
	}
}
