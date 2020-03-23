using System;
using System.Diagnostics;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface
{
	public abstract class Dialog : Window
	{
		private Control mvarParent = null;
		public new Control Parent { get { return mvarParent; } set { mvarParent = value; } }

		private Button.ButtonCollection mvarButtons = null;
		public Button.ButtonCollection Buttons { get { return mvarButtons; } }

		public Dialog()
		{
			mvarButtons = new Button.ButtonCollection(this);
		}

		public bool AutoAlignButtons { get; set; } = true;

		private Button mvarDefaultButton = null;
		public Button DefaultButton { get { return mvarDefaultButton; } set { mvarDefaultButton = value; } }

		public DialogResult DialogResult { get; set; } = DialogResult.None;

		// [DebuggerNonUserCode()]
		public DialogResult ShowDialog(Window parent = null)
		{
			DialogResult result = Application.Engine.ShowDialog(this, parent);
			if (result == DialogResult.None)
				return DialogResult;
			return result;
		}
	}
}

