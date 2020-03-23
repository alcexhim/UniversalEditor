using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Theming
{
    public class FontTable
	{
		private System.Drawing.Font mvarDefault = System.Drawing.SystemFonts.MenuFont;
		public System.Drawing.Font Default { get { return mvarDefault; } set { mvarDefault = value; } }

        private System.Drawing.Font mvarCommandBar = System.Drawing.SystemFonts.MenuFont;
        public System.Drawing.Font CommandBar { get { return mvarCommandBar; } set { mvarCommandBar = value; } }

        private System.Drawing.Font mvarDialogFont = System.Drawing.SystemFonts.MenuFont;
		public System.Drawing.Font DialogFont { get { return mvarDialogFont; } set { mvarDialogFont = value; } }

		private System.Drawing.Font mvarDocumentTabTextSelected = null;
		public System.Drawing.Font DocumentTabTextSelected { get { return mvarDocumentTabTextSelected; } set { mvarDocumentTabTextSelected = value; } }
	}
}
