using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs
{
    [ControlImplementation(typeof(ColorDialog))]
    public class ColorDialogImplementation : WindowsFormsDialogImplementation
    {
        public ColorDialogImplementation(Engine engine, Control control) : base(engine, control)
        {
        }

        protected override bool AcceptInternal()
        {
            System.Drawing.Color c = ((Handle as WindowsFormsNativeDialog).Handle as System.Windows.Forms.ColorDialog).Color;
            (Control as ColorDialog).SelectedColor = Framework.Drawing.Color.FromRGBAByte(c.R, c.G, c.B, c.A);
            return true;
        }

        protected override WindowsFormsNativeDialog CreateDialogInternal(Dialog dialog, List<Button> buttons)
        {
            ColorDialog dlg = (dialog as ColorDialog);

            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            cd.Color = System.Drawing.Color.FromArgb(dlg.SelectedColor.GetAlphaByte(), dlg.SelectedColor.GetRedByte(), dlg.SelectedColor.GetGreenByte(), dlg.SelectedColor.GetBlueByte());

            return new WindowsFormsNativeDialog(cd);
        }
    }
}
