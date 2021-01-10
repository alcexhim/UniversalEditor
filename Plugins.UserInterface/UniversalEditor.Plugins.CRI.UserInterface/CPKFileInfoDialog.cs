//
//  FileSystemEditorExtensions.cs - provide extensions to the FileSystemEditor for CRI CPK archives
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Text;
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.CRI.UserInterface
{
	public class CPKFileInfoDialog : CustomDialog
	{
		public Accessor Accessor { get; set; } = null;
		public CPKDataFormat DataFormat { get; set; }

		public TextBox txt;
		public Button cmdSave;

		public CPKFileInfoDialog()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			Text = "CPK file information";
			Size = new MBS.Framework.Drawing.Dimension2D(600, 400);

			cmdSave = new Button(StockType.Save);
			cmdSave.Click += cmdSave_Click;

			Container ctbuttons = new Container();
			ctbuttons.Layout = new BoxLayout(Orientation.Horizontal);

			ctbuttons.Controls.Add(cmdSave, new BoxLayout.Constraints(false, false, 8, BoxLayout.PackType.End));
			DefaultButton = cmdSave;

			txt = new TextBox();
			txt.Multiline = true;
			Controls.Add(txt, new BoxLayout.Constraints(true, true));

			Controls.Add(ctbuttons, new BoxLayout.Constraints(false, false));
		}

		void cmdSave_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Save;
			dlg.Text = "Save CPK File Information";
			dlg.SelectedFileName = "CpkFileInfo.txt";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				System.IO.File.WriteAllText(dlg.SelectedFileName, txt.Text);
			}
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			StringBuilder sb = new StringBuilder();
			System.Collections.Generic.Dictionary<string, string> dict = new System.Collections.Generic.Dictionary<string, string>();
			dict.Add("CPK Filename", Accessor?.GetFileTitle());

			ushort versionMajor = (ushort)DataFormat.HeaderTable.Records[0].Fields["Version"].Value;
			ushort versionMinor = (ushort)DataFormat.HeaderTable.Records[0].Fields["Revision"].Value;

			dict.Add("File format version", String.Format("Ver.{0}, Rev.{1}", versionMajor, versionMinor));
			dict.Add("Data alignment", DataFormat.SectorAlignment.ToString());
			dict.Add("Content files", DataFormat.HeaderTable.Records[0].Fields["Files"].Value?.ToString());  // DataFormat.HeaderTable.xxx
			dict.Add("Compressed files", "0");  // DataFormat.HeaderTable.xxx
			dict.Add("Content file size", DataFormat.HeaderTable.Records[0].Fields["ContentSize"].Value?.ToString());  // DataFormat.HeaderTable.xxx
			dict.Add("Compressed file size", "0");  // DataFormat.HeaderTable.xxx

			CPKFileMode cpkmode = (CPKFileMode)(uint)DataFormat.HeaderTable.Records[0].Fields["CpkMode"].Value;

			bool enableFilenameInfo = (cpkmode == CPKFileMode.FilenameOnly || cpkmode == CPKFileMode.IDFilename || cpkmode == CPKFileMode.FilenameGroup || cpkmode == CPKFileMode.FilenameIDGroup);
			bool enableIDInfo = (cpkmode == CPKFileMode.IDOnly || cpkmode == CPKFileMode.IDGroup || cpkmode == CPKFileMode.IDFilename || cpkmode == CPKFileMode.FilenameIDGroup);
			bool enableGroupInfo = (cpkmode == CPKFileMode.IDGroup || cpkmode == CPKFileMode.FilenameGroup || cpkmode == CPKFileMode.FilenameIDGroup);

			dict.Add("Enable filename info.", enableFilenameInfo.ToString() + " [Sorted]");  // DataFormat.HeaderTable.xxx
			dict.Add("Enable ID info.", enableIDInfo.ToString());  // DataFormat.HeaderTable.xxx
			dict.Add("Enable Group info.", enableGroupInfo.ToString());  // DataFormat.HeaderTable.xxx
			dict.Add("Enable GInfo Table", "False");  // DataFormat.HeaderTable.xxx
			dict.Add("Enable CRC32 info.", "False");  // DataFormat.HeaderTable.xxx
			dict.Add("Enable CheckSum64 info.", "False");  // DataFormat.HeaderTable.xxx
			dict.Add("Compression Mode", "Layla Standard Compression");  // DataFormat.HeaderTable.xxx
			dict.Add("Work size to bind CPK", "0 bytes");  // DataFormat.HeaderTable.xxx
			dict.Add("Tool version", DataFormat.VersionString);  // DataFormat.HeaderTable.xxx

			foreach (System.Collections.Generic.KeyValuePair<string, string> kvp in dict)
			{
				sb.Append(kvp.Key);
				sb.Append("：");
				sb.Append(kvp.Value);
				sb.AppendLine();
			}
			txt.Text = sb.ToString();
		}

	}
}