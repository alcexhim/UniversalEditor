using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript;

namespace UniversalEditor.Editors.Setup.Microsoft.ACME.BootstrapScript
{
	public partial class BootstrapScriptEditor : Editor
	{
		public BootstrapScriptEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Bootstrap Script Editor";
				_er.SupportedObjectModels.Add(typeof(BootstrapScriptObjectModel));
			}
			return _er;
		}

		private void chkRequire31_CheckedChanged(object sender, EventArgs e)
		{
			txtRequire31.ReadOnly = !chkRequire31.Checked;

			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.Require31Enabled = chkRequire31.Checked;
			EndEdit();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) script = new BootstrapScriptObjectModel();

			txtWindowTitle.Text = script.WindowTitle;
			txtWindowMessage.Text = script.WindowMessage;
			txtTemporaryDirectorySize.Value = script.TemporaryDirectorySize;
			txtTemporaryDirectoryName.Text = script.TemporaryDirectoryName;
			txtCommandLine.Text = script.CommandLine;
			txtWindowClassName.Text = script.WindowClassName;
			chkRequire31.Enabled = script.Require31Enabled;
			txtRequire31.Text = script.Require31Message;
		}

		private void txtWindowTitle_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtWindowMessage_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtWindowTitle_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.WindowTitle = txtWindowTitle.Text;
			EndEdit();
		}

		private void txtWindowMessage_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.WindowMessage = txtWindowMessage.Text;
			EndEdit();
		}

		private void txtWindowClassName_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.WindowClassName = txtWindowClassName.Text;
			EndEdit();
		}

		private void txtTemporaryDirectoryName_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.TemporaryDirectoryName = txtTemporaryDirectoryName.Text;
			EndEdit();
		}

		private void txtTemporaryDirectorySize_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.TemporaryDirectorySize = (int)txtTemporaryDirectorySize.Value;
			EndEdit();
		}

		private void txtCommandLine_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.CommandLine = txtCommandLine.Text;
			EndEdit();
		}

		private void txtRequire31_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.Require31Message = txtRequire31.Text;
			EndEdit();
		}
	}
}
