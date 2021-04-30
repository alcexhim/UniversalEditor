//
//  CalculateNeighboringColorsDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette.Dialogs
{
	[ContainerLayout(typeof(CalculateNeighboringColorsDialog), "UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Palette.Dialogs.CalculateNeighboringColorsDialog.glade")]
	public class CalculateNeighboringColorsDialog : CustomDialog
	{
		private Button cmdOK;
		private Controls.ColorPalette.ColorPaletteControl cc;
		private ComboBox cboMode;

		public Color SelectedColor { get; set; } = Color.Empty;
		public Color[] Colors { get; private set; } = new Color[0];

		[EventHandler(nameof(cboMode), nameof(ComboBox.Changed))]
		private void cboMode_Changed(object sender, EventArgs e)
		{
			cc.Entries.Clear();

			Color[] colors = new Color[5];
			for (int i = 0; i < colors.Length; i++)
			{
				colors[i] = (Color)SelectedColor.Clone();
			}

			int selectedIndex = (cboMode.Model as DefaultTreeModel).Rows.IndexOf(cboMode.SelectedItem);
			switch (selectedIndex)
			{
				case 0: // analogous
				{
					double hue = colors[2].GetHueScaled(360.0);

					// hsv in GIMP is 0.0-360.0, 0.0-100.0, and 0.0-100.0, so that's what we'll use
					colors[0].Saturation -= 0.05;
					colors[0].SetHueScaled(hue + 32, 360.0);
					colors[1].Saturation -= 0.05;
					colors[1].SetHueScaled(hue + 16, 360.0);

					colors[3].Saturation -= 0.05;
					colors[3].SetHueScaled(hue - 16, 360.0);
					colors[4].Saturation -= 0.05;
					colors[4].SetHueScaled(hue - 32, 360.0);
					break;
				}
				case 8: // shades
				{
					colors[0].Saturation = 0.65;
					colors[1].Saturation = 0.40;
					colors[2].Saturation = 0.90;
					colors[3].Saturation = 0.95;
					colors[4].Saturation = 0.80;
					break;
				}
			}

			cc.Entries.Clear();
			for (int i = 0; i < colors.Length; i++)
			{
				cc.Entries.Add(colors[i]);
			}
			cc.Refresh();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			DefaultButton = cmdOK;
		}

		[EventHandler(nameof(cmdOK), nameof(Control.Click))]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			List<Color> list = new List<Color>();
			for (int i = 0; i < cc.Entries.Count; i++)
			{
				list.Add(cc.Entries[i].Color);
			}
			Colors = list.ToArray();
		}

	}
}
