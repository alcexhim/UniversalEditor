using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace UniversalEditor.ObjectModels.Multimedia.Picture
{
	public class PicturePathItem : PictureItem
	{
		private Color mvarColor = Color.Empty;
		private GraphicsPath mvarPath = null;
		private int mvarSize = 1;
		public Color Color
		{
			get
			{
				return this.mvarColor;
			}
			set
			{
				this.mvarColor = value;
			}
		}
		public GraphicsPath Path
		{
			get
			{
				return this.mvarPath;
			}
			set
			{
				this.mvarPath = value;
			}
		}
		public int Size
		{
			get
			{
				return this.mvarSize;
			}
			set
			{
				this.mvarSize = value;
			}
		}
		protected internal override void OnRender(PaintEventArgs e)
		{
			e.Graphics.DrawPath(new Pen(this.mvarColor, (float)this.mvarSize), this.mvarPath);
		}
	}
}
