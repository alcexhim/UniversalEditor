//
//  SpreadsheetEditor.cs
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

using UniversalEditor.UserInterface;

using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.Spreadsheet;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Plugins.Office.UserInterface.Editors.Spreadsheet.Controls;

namespace UniversalEditor.Plugins.Office.UserInterface.Editors.Spreadsheet
{
	public class SpreadsheetEditor : Editor
	{
		private SheetControl sheetctrl = null;
		public SpreadsheetEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			sheetctrl = new SheetControl();
			Controls.Add(sheetctrl, new BoxLayout.Constraints(true, true));
		}

		public Sheet SelectedSheet
		{
			get
			{
				return sheetctrl.Sheet;
			}
			set
			{
				sheetctrl.Sheet = value;
			}
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(SpreadsheetObjectModel));
			}
			return _er;
		}

		public int DefaultRowHeight { get; set; } = 16;
		public int DefaultColumnWidth { get; set; } = 81;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(e);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			SpreadsheetObjectModel spreadsheet = (ObjectModel as SpreadsheetObjectModel);
			if (spreadsheet == null) return;

			if (spreadsheet.Sheets.Count == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					spreadsheet.Sheets.Add(new Sheet(String.Format("Sheet{0}", i.ToString())));
				}
				SelectedSheet = spreadsheet.Sheets[0];
			}
		}
	}
}
