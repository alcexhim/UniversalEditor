using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Controls.Michelangelo
{
	public partial class CanvasControl : UserControl
	{
		public CanvasControl()
		{
			InitializeComponent();

			BuildTransparentBackgroundBrush();
		}

		private TextureBrush mvarTransparentBackgroundBrush = null;
		private void BuildTransparentBackgroundBrush()
		{
			Bitmap bitmap = new Bitmap(16, 16);
			Graphics g = Graphics.FromImage(bitmap);

			SolidBrush light = new SolidBrush(Color.FromArgb(153, 153, 153));
			SolidBrush dark = new SolidBrush(Color.FromArgb(102, 102, 102));

			g.FillRectangle(light, new Rectangle(0, 0, 8, 8));
			g.FillRectangle(dark, new Rectangle(8, 0, 8, 8));
			g.FillRectangle(dark, new Rectangle(0, 8, 8, 8));
			g.FillRectangle(light, new Rectangle(8, 8, 8, 8));

			mvarTransparentBackgroundBrush = new TextureBrush(bitmap);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(mvarTransparentBackgroundBrush, new Rectangle(0, 0, Width - 1, Height - 1));
		}
	}
}
