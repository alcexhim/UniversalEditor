using System;
using MBS.Framework.UserInterface.Controls.Native;
using MBS.Framework.UserInterface.Input.Keyboard;

namespace MBS.Framework.UserInterface.Controls
{
	namespace Native
	{
		public interface ITextBoxImplementation
		{
			void InsertText(string content);

			int GetSelectionStart();
			void SetSelectionStart(int pos);
			int GetSelectionLength();
			void SetSelectionLength(int len);

			string GetSelectedText();
			void SetSelectedText(string text);
		}
	}
	public class TextBox : SystemControl
	{
		private int mvarMaxLength = -1;
		public int MaxLength { get { return mvarMaxLength; } set { mvarMaxLength = value; } }

		private int mvarWidthChars = -1;
		public int WidthChars { get { return mvarWidthChars; } set { mvarWidthChars = value; } }

		private bool mvarMultiline = false;
		/// <summary>
		/// Determines whether this <see cref="TextBox"/> supports multi-line editing.
		/// </summary>
		/// <value><c>true</c> if multiline; otherwise, <c>false</c>.</value>
		public bool Multiline {  get { return mvarMultiline;  } set { mvarMultiline = value; } }

		private HorizontalAlignment mvarHorizontalAlignment = HorizontalAlignment.Default;
		public HorizontalAlignment HorizontalAlignment {  get { return mvarHorizontalAlignment;  } set { mvarHorizontalAlignment = value; } }

		private VerticalAlignment mvarVerticalAlignment = VerticalAlignment.Default;
		public VerticalAlignment VerticalAlignment {  get { return mvarVerticalAlignment;  } set { mvarVerticalAlignment = value; } }

		private bool mvarUseSystemPasswordChar = false;

		public void Insert(string content)
		{
			(ControlImplementation as Native.ITextBoxImplementation).InsertText(content);
		}

		public bool UseSystemPasswordChar { get { return mvarUseSystemPasswordChar; } set { mvarUseSystemPasswordChar = value; } }

		private bool mvarEditable = true;
		/// <summary>
		/// Determines if text in this <see cref="TextBox" /> may be edited.
		/// </summary>
		/// <value><c>true</c> if text may be edited; otherwise, <c>false</c>.</value>
		public bool Editable { get { return mvarEditable; } set { mvarEditable = value; } }

		/// <summary>
		/// Determines whether this <see cref="TextBox"/> has been changed by the user.
		/// </summary>
		/// <value><c>true</c> if is changed by user; otherwise, <c>false</c>.</value>
		public bool IsChangedByUser { get; private set; }

		public void ResetChangedByUser()
		{
			IsChangedByUser = false;
		}

		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Cancel) return;
			
			this.IsChangedByUser = true;
		}

		public event EventHandler Changed;
		protected internal virtual void OnChanged(EventArgs e)
		{
			if (Changed != null)
				Changed (this, e);
		}

		private string mvarSelectedText = null;
		public string SelectedText
		{
			get
			{
				ITextBoxImplementation impl = (ControlImplementation as ITextBoxImplementation);
				if (impl != null)
					mvarSelectedText = impl.GetSelectedText();

				return mvarSelectedText;
			}
			set
			{
				(ControlImplementation as ITextBoxImplementation)?.SetSelectedText(value);
			}
		}

		private int mvarSelectionStart = 0;
		public int SelectionStart
		{
			get
			{
				ITextBoxImplementation impl = (ControlImplementation as ITextBoxImplementation);
				if (impl != null)
					mvarSelectionStart = impl.GetSelectionStart();

				return mvarSelectionStart;
			}
			set
			{
				(ControlImplementation as ITextBoxImplementation)?.SetSelectionStart(value);
			}
		}
		private int mvarSelectionLength = 0;
		public int SelectionLength
		{
			get
			{
				ITextBoxImplementation impl = (ControlImplementation as ITextBoxImplementation);
				if (impl != null)
					mvarSelectionLength = impl.GetSelectionLength();

				return mvarSelectionLength;
			}
			set
			{
				(ControlImplementation as ITextBoxImplementation)?.SetSelectionLength(value);
			}
		}
	}
}

