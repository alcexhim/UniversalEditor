//
//  MyClass.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using MBS.Framework;
using MBS.Framework.UserInterface;
using UniversalEditor.Accessors;
using UniversalEditor.Plugins.AutoSave.Dialogs;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.AutoSave
{
	public class AutoSavePlugin : UserInterfacePlugin
	{
		private Timer tmr = new Timer();

		private AutoSaveDataFormat asdf = new AutoSaveDataFormat();

		private string GetAutosavePath()
		{
			return String.Format("/tmp/autosave/{0}", Application.Instance.ShortName);
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			Console.WriteLine("autosave: looking for dirty documents...");

			IHostApplication ha = (Application.Instance as IHostApplication);

			string path = System.IO.Path.Combine(new string[] { GetAutosavePath(), DateTime.Now.ToString("yyyyMMdd") });
			Console.WriteLine("autosave: saving dirty documents in /tmp/autosave/universal-editor/...");

			for (int i = 0; i < ha.CurrentWindow.Editors.Count; i++)
			{
				Editor ed = ha.CurrentWindow.Editors[i];
				if (ed.Changed || !ed.Document.IsSaved)
				{
					string filename = System.IO.Path.Combine(new string[] { path, String.Format("{0}{1}.tmp", DateTime.Now.ToString("HHmmss"), i.ToString().PadLeft(2, '0')) });

					string dir = System.IO.Path.GetDirectoryName(filename);
					if (!System.IO.Directory.Exists(dir))
						System.IO.Directory.CreateDirectory(dir);

					if (!fas.ContainsKey(ed))
					{
						fas[ed] = new FileAccessor(filename, true, true);
					}

					AutoSaveObjectModel autosave = new AutoSaveObjectModel();
					autosave.ObjectModel = ed.ObjectModel;
					if (ed.Document.IsSaved)
					{
						autosave.OriginalFileName = ed.Document.Accessor.GetFileName();
					}
					else
					{
						autosave.OriginalFileName = null;
					}
					Document.Save(autosave, asdf, fas[ed]);

					i++;
				}
			}

			Console.WriteLine("autosave: going back to sleep");
		}

		private System.Collections.Generic.Dictionary<Editor, FileAccessor> fas = new System.Collections.Generic.Dictionary<Editor, FileAccessor>();


		protected override void InitializeInternal()
		{
			base.InitializeInternal();

			// check to see if we have any dirty documents
			Console.WriteLine("autosave: checking for existing dirty documents...");

			string path = GetAutosavePath();
			if (!System.IO.Directory.Exists(path))
			{
				return;
			}

			string[] autosaves = System.IO.Directory.GetFiles(path, "*.tmp", System.IO.SearchOption.AllDirectories);

			if (autosaves.Length > 0)
			{
				AutoSaveDialog dlg = new AutoSaveDialog();
				dlg.FileNames.AddRange(autosaves);
				if (dlg.ShowDialog() == DialogResult.OK)
				{

				}
			}

			tmr.Duration = 10000; // 5 /*minutes*/ * 60 /*seconds in a minute*/ * 1000 /*milliseconds in a second*/;
			tmr.Tick += tmr_Tick;
			tmr.Enabled = true;
		}
	}
}
