using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Multimedia.Palette
{
	public partial class PaletteEditor : Editor
	{
		public PaletteEditor()
		{
			InitializeComponent();
			base.SupportedObjectModels.Add(typeof(PaletteObjectModel));
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			pal.Entries.Clear();

			PaletteObjectModel palette = (ObjectModel as PaletteObjectModel);
			if (palette == null) return;

			foreach (PaletteEntry color in palette.Entries)
			{
				pal.Entries.Add(color);
			}
			pal.Refresh();
		}

		private void pal_SelectionChanged(object sender, EventArgs e)
		{
			txtColorName.Text = String.Empty;
			if (pal.SelectedColor == null) return;

			txtColorName.Text = pal.SelectedColor.Name;

			Color color = pal.SelectedColor.Color;
			cmdColor.BackColor = color.ToGdiColor();
			txtHue.Value = (decimal)(cmdColor.BackColor.GetHue() % 239);
			txtSaturation.Value = (decimal)(cmdColor.BackColor.GetSaturation() % 240);
			txtSaturation.Value = (decimal)(cmdColor.BackColor.GetBrightness() % 240);

			txtRed.Value = (decimal)(color.Red * 255);
			txtGreen.Value = (decimal)(color.Green * 255);
			txtBlue.Value = (decimal)(color.Blue * 255);
		}

		private void cmdColor_Click(object sender, EventArgs e)
		{
			if (pal.SelectedColor == null) return;

			Color color = pal.SelectedColor.Color;
			ColorDialog dlg = new ColorDialog();
			dlg.Color = color.ToGdiColor();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				cmdColor.BackColor = dlg.Color;
				pal.Entries[pal.SelectedIndex].Color = dlg.Color.ToUniversalColor();
				pal.Refresh();
			}
		}

		private void txtHSV_Validated(object sender, EventArgs e)
		{

		}

		private void txtRGB_Validated(object sender, EventArgs e)
		{
			sldRed.Value = (int)txtRed.Value;
			sldGreen.Value = (int)txtGreen.Value;
			sldBlue.Value = (int)txtBlue.Value;

			UpdateSelectedColor();
		}

		private void UpdateSelectedColor()
		{
			Color color = Color.FromArgb((byte)txtRed.Value, (byte)txtGreen.Value, (byte)txtBlue.Value);
			pal.Entries[pal.SelectedIndex].Color = color;
			pal.Refresh();

			cmdColor.BackColor = color.ToGdiColor();
		}

		private void sldRGB_Scroll(object sender, EventArgs e)
		{
			txtRed.Value = sldRed.Value;
			txtGreen.Value = sldGreen.Value;
			txtBlue.Value = sldBlue.Value;

			UpdateSelectedColor();
		}
	}
}
