using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Picture
{
	public partial class PictureEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(PictureObjectModel));
			}
			return _er;
		}

		public PictureEditor()
		{
			InitializeComponent();

			Graphics g = Graphics.FromImage(_mTextureImage);
			g.FillRectangle(Brushes.Gray, new Rectangle(0, 0, 8, 8));
			g.FillRectangle(Brushes.LightGray, new Rectangle(0, 8, 8, 8));
			g.FillRectangle(Brushes.Gray, new Rectangle(8, 8, 8, 8));
			g.FillRectangle(Brushes.LightGray, new Rectangle(8, 0, 8, 8));

			_mTextureBrush = new TextureBrush(_mTextureImage);
		}

		private Bitmap _mTextureImage = new Bitmap(16, 16);
		private TextureBrush _mTextureBrush = null;
		private Bitmap _mObjectToBitmap = null;

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			PictureObjectModel picObj = (base.ObjectModel as PictureObjectModel);
			if (picObj == null)
			{
				_mObjectToBitmap = null;
			}
			else
			{
				_mObjectToBitmap = picObj.ToBitmap();
			}
			Invalidate();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			
			PictureObjectModel picObj = (base.ObjectModel as PictureObjectModel);
			if (picObj != null)
			{
				e.Graphics.FillRectangle(_mTextureBrush, new Rectangle(0, 0, picObj.Width, picObj.Height));
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (_mObjectToBitmap != null)
			{
				e.Graphics.DrawImage(_mObjectToBitmap, 0, 0);
			}
		}

	}
}
