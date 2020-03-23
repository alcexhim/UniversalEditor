using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs
{
	[ControlImplementation(typeof(PrintDialog))]
	public class PrintDialogImplementation : WindowsFormsDialogImplementation
	{
		public PrintDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			return true;
		}

		protected override WindowsFormsNativeDialog CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			System.Windows.Forms.PrintDialog pd = new System.Windows.Forms.PrintDialog();
			PrintDialog dlg = (dialog as PrintDialog);
			pd.UseEXDialog = dlg.AutoUpgradeEnabled;
			return new WindowsFormsNativeDialog(pd);
		}
	}
}
