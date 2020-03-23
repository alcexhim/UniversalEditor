using System;
namespace MBS.Framework.UserInterface.Dialogs
{
	public enum TaskDialogIcon
	{
		None = 0,

		Warning = (int)(UInt16.MaxValue),
		Error = (int)(UInt16.MaxValue - 1),
		Information = (int)(UInt16.MaxValue - 2),
        Question = 0,

		Security = (int)(UInt16.MaxValue - 3),
		SecurityTrusted = (int)(UInt16.MaxValue - 4),
		SecurityWarning = (int)(UInt16.MaxValue - 5),
		SecurityError = (int)(UInt16.MaxValue - 6),
		SecurityOK = (int)(UInt16.MaxValue - 7),
		SecurityUntrusted = (int)(UInt16.MaxValue - 8)
	}

    [Flags()]
	public enum TaskDialogButtons
	{
		Custom = -1,
		None = 0x00,

		OK = 0x01,     // returns (1)(IDOK)
		Yes = 0x02,     // returns (6)(IDYES)
		No = 0x04,      // returns (7)(IDNO)
		Cancel = 0x08,  // returns (2)(IDCANCEL)
		Retry = 0x10,  // returns (4)(IDRETRY)
		Close = 0x20  // returns (8)(IDCLOSE)
	}
	public enum TaskDialogButtonStyle
	{
		Buttons,
		Commands,
		CommandsNoIcon
	}
	public class TaskDialog : CommonDialog
	{
		public TaskDialogButtonStyle ButtonStyle { get; set; } = TaskDialogButtonStyle.Buttons;

		public string Prompt { get; set; } = null;
		public string Content { get; set; } = null;

		public string VerificationText { get; set; } = null;
		public bool VerificationChecked { get; set; } = false;

		public TaskDialogButtons ButtonsPreset { get; set; } = TaskDialogButtons.None;
		public TaskDialogIcon Icon { get; set; } = TaskDialogIcon.None;

        public static DialogResult ShowDialog(string instruction, string content, string title, Controls.Button[] buttons, TaskDialogIcon icon)
        {
            TaskDialog td = new TaskDialog();
            td.Prompt = instruction;
            td.Content = content;
            td.Text = title;
            td.ButtonsPreset = TaskDialogButtons.Custom;
            td.ButtonStyle = TaskDialogButtonStyle.Commands;
            for (int i = 0; i < buttons.Length; i++)
            {
                td.Buttons.Add(buttons[i]);
            }
            td.Icon = icon;
            return td.ShowDialog();
        }
        public static DialogResult ShowDialog(string instruction, string content, string title, TaskDialogButtons buttons, TaskDialogIcon icon)
        {
            TaskDialog td = new TaskDialog();
            td.Prompt = instruction;
            td.Content = content;
            td.Text = title;
            td.ButtonsPreset = buttons;
            td.Icon = icon;
            return td.ShowDialog();
        }
    }
}
