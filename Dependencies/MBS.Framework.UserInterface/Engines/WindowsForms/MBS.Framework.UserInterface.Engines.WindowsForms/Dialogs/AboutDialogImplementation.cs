using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs
{
	[ControlImplementation(typeof(AboutDialog))]
	public class AboutDialogImplementation : WindowsFormsDialogImplementation
	{
		public AboutDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			throw new NotImplementedException();
		}

		protected override WindowsFormsNativeDialog CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			throw new NotImplementedException();
		}
	}
}
