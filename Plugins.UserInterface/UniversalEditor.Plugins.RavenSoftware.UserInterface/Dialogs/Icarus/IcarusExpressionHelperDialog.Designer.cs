using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Plugins.RavenSoftware.UserInterface.Controls.Icarus;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Dialogs.Icarus
{
	partial class IcarusExpressionHelperDialog
	{
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Buttons.Add(new Button(StockType.OK, cmdOK_Click));
			this.Buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));

			this.Layout = new BoxLayout(Orientation.Horizontal);

			this.Name = "IcarusExpressionHelperDialog";
			this.Text = "Expression Helper";
		}

		protected override void OnCreating(EventArgs e)
		{
			base.OnCreating(e);
		}
	}
}