using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Handles the output window in Universal Editor. If the user is running a plugin which makes
	/// use of these features in non-GUI mode, it will write to the console instead.
	/// </summary>
	public class HostApplicationOutputWindow
	{
		public event TextWrittenEventHandler TextWritten;
		protected virtual void OnTextWritten(TextWrittenEventArgs e)
		{
			if (TextWritten != null)
			{
				TextWritten(this, e);
			}
			else
			{
				Console.Write(e.Text);
			}
		}

		public event EventHandler TextCleared;
		protected virtual void OnTextCleared(EventArgs e)
		{
			if (TextCleared != null)
			{
				TextCleared(this, e);
			}
			else
			{
				Console.Clear();
			}
		}

		public void Clear()
		{
			OnTextCleared(EventArgs.Empty);
		}
		public void Write(string text)
		{
			OnTextWritten(new TextWrittenEventArgs(text));
		}
		public void WriteLine(string text)
		{
			Write(text + System.Environment.NewLine);
		}
	}

	public delegate void TextWrittenEventHandler(object sender, TextWrittenEventArgs e);
	public class TextWrittenEventArgs
	{
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } }

		public TextWrittenEventArgs(string text)
		{
			mvarText = text;
		}
	}
}
