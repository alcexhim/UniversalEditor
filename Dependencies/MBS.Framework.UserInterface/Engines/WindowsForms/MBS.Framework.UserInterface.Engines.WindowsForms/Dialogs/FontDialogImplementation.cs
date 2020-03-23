using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs
{
	[ControlImplementation(typeof(FontDialog))]
	public class FontDialogImplementation : WindowsFormsDialogImplementation
	{
		public FontDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			return true;
		}

		protected override WindowsFormsNativeDialog CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			System.Windows.Forms.FontDialog fd = new System.Windows.Forms.FontDialog();
			return new WindowsFormsNativeDialog(fd);
		}
	}
}
