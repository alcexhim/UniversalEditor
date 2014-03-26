using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
namespace UniversalEditor.ObjectModels.Multimedia.Picture
{
	public abstract class PictureItem
	{
		public class PictureItemCollection : Collection<PictureItem>
		{
		}
		private int mvarLeft = 0;
		private int mvarTop = 0;
		private int mvarWidth = 0;
		private int mvarHeight = 0;
		public int Left
		{
			get
			{
				return this.mvarLeft;
			}
			set
			{
				this.mvarLeft = value;
			}
		}
		public int Top
		{
			get
			{
				return this.mvarTop;
			}
			set
			{
				this.mvarTop = value;
			}
		}
		public int Width
		{
			get
			{
				return this.mvarWidth;
			}
			set
			{
				this.mvarWidth = value;
			}
		}
		public int Height
		{
			get
			{
				return this.mvarHeight;
			}
			set
			{
				this.mvarHeight = value;
			}
		}
		public int Right
		{
			get
			{
				return this.mvarLeft + this.mvarWidth;
			}
		}
		public int Bottom
		{
			get
			{
				return this.mvarTop + this.mvarHeight;
			}
		}
		protected internal abstract void OnRender(PaintEventArgs e);
	}
}
