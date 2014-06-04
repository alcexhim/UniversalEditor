using AwesomeControls.Designer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors.File;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.ObjectModels.NWCSceneLayout;
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN;

namespace UniversalEditor.Editors.NewWorldComputing.Scene.DesignerAreas
{
    public class BasicDesignerArea : DesignerArea
    {
        private Bitmap _bmpBackground = null;
        private TextureBrush brshChecked = null;

        protected override void OnBeforePaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (mvarScene != null)
            {
                if (_bmpBackground == null)
                {
                    string ParentDirectory = String.Empty;
                    if (mvarScene.Accessor is FileAccessor) ParentDirectory = System.IO.Path.GetDirectoryName((mvarScene.Accessor as FileAccessor).FileName);
                    string ICNFileName = Scene.BackgroundImageFileName;
                    if (!String.IsNullOrEmpty(ParentDirectory)) ICNFileName = ParentDirectory + System.IO.Path.DirectorySeparatorChar.ToString() + ICNFileName;

                    if (!System.IO.File.Exists(ICNFileName))
                    {
                        return;
                    }

                    ICNDataFormat icn = new ICNDataFormat();
                    PictureCollectionObjectModel picc = new PictureCollectionObjectModel();
                    FileAccessor.Load(ICNFileName, picc, icn, true);
                    _bmpBackground = picc.Pictures[(int)mvarScene.BackgroundImageIndex].ToBitmap();
                }
                if (_bmpBackground != null)
                {
                    e.Graphics.DrawImage(_bmpBackground, ClientRectangle);
                }
            }
            else
            {
                if (brshChecked == null)
                {
                    Bitmap bmp = new Bitmap(8, 8);
                    Graphics gfx = Graphics.FromImage(bmp);
                    gfx.Clear(Colors.DarkGray.ToGdiColor());
                    gfx.FillRectangle(new SolidBrush(Colors.Silver.ToGdiColor()), new Rectangle(0, 0, 4, 4));
                    gfx.FillRectangle(new SolidBrush(Colors.Silver.ToGdiColor()), new Rectangle(4, 4, 4, 4));
                    brshChecked = new TextureBrush(bmp);
                }
                e.Graphics.FillRectangle(brshChecked, ClientRectangle);
                e.Graphics.DrawRectangle(System.Drawing.Pens.Black, ClientRectangle);
            }
        }

        private NWCSceneLayoutObjectModel mvarScene = null;
        public NWCSceneLayoutObjectModel Scene { get { return mvarScene; } set { mvarScene = value; } }
    }
}
