using System;
using System.Reflection;
using MBS.Framework;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.Accessors;
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.AutoSave.Dialogs
{
	[ContainerLayout(typeof(AutoSaveDialog), "UniversalEditor.Plugins.AutoSave.Dialogs.AutoSaveDialog.glade")]
	public class AutoSaveDialog : CustomDialog
	{
		private Label lblPrompt;
		private ListViewControl lv;

		public System.Collections.Specialized.StringCollection FileNames { get; } = new System.Collections.Specialized.StringCollection();

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			lblPrompt.Text = lblPrompt.Text.Replace("${Application.Title}", Application.Instance.Title);

			Document[] ds = new Document[FileNames.Count];
			for (int i = 0; i < ds.Length; i++)
			{
				ds[i] = new Document(new AutoSaveObjectModel(), new AutoSaveDataFormat(), new FileAccessor(FileNames[i], false, false, true));
				ds[i].Load();

				string fn = (ds[i].ObjectModel as AutoSaveObjectModel).OriginalFileName;
				fn = String.IsNullOrEmpty(fn) ? "(untitled)" : fn;

				string dts = String.Format("{0} {1}", (ds[i].ObjectModel as AutoSaveObjectModel).LastUpdateDateTime.ToLongDateString(), (ds[i].ObjectModel as AutoSaveObjectModel).LastUpdateDateTime.ToLongTimeString());
				lv.Model.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(lv.Model.Columns[0], fn),
					new TreeModelRowColumn(lv.Model.Columns[1], dts)
				}));
			}
		}

	}
}
