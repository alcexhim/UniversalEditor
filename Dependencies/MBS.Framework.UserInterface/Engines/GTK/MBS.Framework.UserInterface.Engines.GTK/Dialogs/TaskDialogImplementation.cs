using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface.Engines.GTK.Dialogs
{
	[ControlImplementation(typeof(TaskDialog))]
	public class TaskDialogImplementation : GTKDialogImplementation
	{
		public TaskDialogImplementation(Engine engine, Control control)
			: base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			return true;
		}

		protected override bool UseCustomButtonImplementation => true;

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			TaskDialog td = (dialog as TaskDialog);

			CustomDialog dlg = new CustomDialog();
			dlg.Layout = new BoxLayout(Orientation.Vertical);

			Container ctIconAndPrompt = new Container();
			ctIconAndPrompt.Layout = new BoxLayout(Orientation.Horizontal);

			PictureFrame picIcon = new PictureFrame();
			picIcon.VerticalAlignment = VerticalAlignment.Top;
			picIcon.Image = UserInterface.Drawing.Image.FromStock(StockType.DialogQuestion, 48);
			ctIconAndPrompt.Controls.Add(picIcon, new BoxLayout.Constraints(false, false, 16));

			Container ctPromptAndContent = new Container();
			ctPromptAndContent.Layout = new BoxLayout(Orientation.Vertical);

			Label lblPrompt = new Label(td.Prompt);
			// lblPrompt.Font.Size = 18;
			lblPrompt.Attributes.Add("scale", 1.2);
			lblPrompt.HorizontalAlignment = HorizontalAlignment.Left;
			ctPromptAndContent.Controls.Add(lblPrompt, new BoxLayout.Constraints(true, true, 0));

			dlg.Controls.Add(ctIconAndPrompt, new BoxLayout.Constraints(false, false, 16));

			Label lblContent = new Label(td.Content);
			lblContent.HorizontalAlignment = HorizontalAlignment.Left;
			ctPromptAndContent.Controls.Add(lblContent, new BoxLayout.Constraints(false, false, 24));

			ctIconAndPrompt.Controls.Add(ctPromptAndContent, new BoxLayout.Constraints(true, true, 0));

			List<Button> mybuttons = new List<Button>();
			if (td.ButtonsPreset == TaskDialogButtons.Custom)
			{
				for (int i = 0; i < td.Buttons.Count; i++)
				{
					mybuttons.Add(td.Buttons[i]);
				}
			}
			else
			{
				if ((td.ButtonsPreset & TaskDialogButtons.Cancel) == TaskDialogButtons.Cancel)
				{
					mybuttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));
				}
				if ((td.ButtonsPreset & TaskDialogButtons.Close) == TaskDialogButtons.Close)
				{
					mybuttons.Add(new Button(StockType.Close, DialogResult.None));
				}
				if ((td.ButtonsPreset & TaskDialogButtons.No) == TaskDialogButtons.No)
				{
					mybuttons.Add(new Button(StockType.No, DialogResult.No));
				}
				if ((td.ButtonsPreset & TaskDialogButtons.OK) == TaskDialogButtons.OK)
				{
					mybuttons.Add(new Button(StockType.OK, DialogResult.OK));
				}
				if ((td.ButtonsPreset & TaskDialogButtons.Retry) == TaskDialogButtons.Retry)
				{
					mybuttons.Add(new Button("_Retry", DialogResult.Retry));
				}
				if ((td.ButtonsPreset & TaskDialogButtons.Yes) == TaskDialogButtons.Yes)
				{
					mybuttons.Add(new Button(StockType.Yes, DialogResult.Yes));
				}
			}

			TaskDialogButtonStyle tdButtonStyle = td.ButtonStyle;
			if (mybuttons.Count == 0)
			{
				tdButtonStyle = TaskDialogButtonStyle.Buttons;
				mybuttons.Add(new Button(StockType.OK, DialogResult.OK));
			}

			switch (tdButtonStyle)
			{
				case TaskDialogButtonStyle.Buttons:
				{
					for (int i = 0; i < mybuttons.Count; i++)
					{
						dlg.Buttons.Add(mybuttons[i]);
					}
					break;
				}
				case TaskDialogButtonStyle.Commands:
				case TaskDialogButtonStyle.CommandsNoIcon:
				{
					Container ctButtonContainer = new Container();
					ctButtonContainer.Layout = new BoxLayout(Orientation.Vertical);

					for (int i = 0; i < mybuttons.Count; i++)
					{
						if (td.ButtonStyle == TaskDialogButtonStyle.Commands)
						{
							mybuttons[i].Image = UserInterface.Drawing.Image.FromStock(StockType.ArrowRight, 24);
							mybuttons[i].AlwaysShowImage = true;
						}

						mybuttons[i].BorderStyle = ButtonBorderStyle.None;

						System.Text.StringBuilder sb = new System.Text.StringBuilder();
						sb.AppendLine();
						string[] lines = mybuttons[i].Text.Split(new char[] { '\n' }, 2);
						if (lines.Length > 0)
						{
							sb.Append("<big>");
							sb.Append(lines[0]);
							sb.Append("</big>");
							sb.AppendLine();

							if (lines.Length > 1)
							{
								sb.Append(lines[1]);
							}
						}
						sb.AppendLine();
						mybuttons[i].HorizontalAlignment = HorizontalAlignment.Left;
						mybuttons[i].Text = sb.ToString();
						mybuttons[i].UseMarkup = true;
						// mybuttons[i].Padding = new Framework.Drawing.Padding(16);

						ctButtonContainer.Controls.Add(mybuttons[i], new BoxLayout.Constraints(true, true, 0));
					}

					dlg.Controls.Add(ctButtonContainer, new BoxLayout.Constraints(false, false, 24));
					break;
				}
			}

			Application.Engine.CreateControl(dlg);

			return (Application.Engine.GetHandleForControl(dlg) as GTKNativeControl);
		}
	}
}
