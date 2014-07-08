using AwesomeControls.Designer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors.File;
using UniversalEditor.ObjectModels.NWCSceneLayout;
using UniversalEditor.ObjectModels.NWCSceneLayout.SceneObjects;

using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN;

namespace UniversalEditor.Editors.NewWorldComputing.Scene.DesignerObjectClasses
{
    public class NwcButtonClass : DesignerObjectClass
    {
        private static ICNDataFormat icn = new ICNDataFormat();

        protected override void RenderClientArea(DesignerObjectPaintEventArgs e)
        {
            SceneObject obj = (SceneObject)e.Item.Properties["SceneObject"];
            if (obj is SceneObjectButton)
            {
                SceneObjectButton btn = (obj as SceneObjectButton);

                System.Drawing.Bitmap _bmp = null;
                if (e.Item.Properties.ContainsKey("Bitmap"))
                {
                    _bmp = (e.Item.Properties["Bitmap"] as System.Drawing.Bitmap);
                }
                if (_bmp == null)
                {
                    string imageFileName = btn.BackgroundImageFileName;

                    string ParentDirectory = String.Empty;
                    if (e.Item.Properties.ContainsKey("ParentDirectory")) ParentDirectory = e.Item.Properties["ParentDirectory"].ToString();
                    if (!String.IsNullOrEmpty(ParentDirectory)) imageFileName = ParentDirectory + "\\" + imageFileName;

                    if (!System.IO.File.Exists(imageFileName))
                    {
                        e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.Item.Bounds);
                        return;
                    }

                    PictureCollectionObjectModel picc = new PictureCollectionObjectModel();
                    FileAccessor.Load(imageFileName, picc, icn, true);
                    _bmp = picc.Pictures[(int)btn.BackgroundImageIndex].ToBitmap();
                    e.Item.Properties["Bitmap"] = _bmp;
                }
                e.Graphics.DrawImage(_bmp, e.Item.Bounds);
            }
            else if (obj is SceneObjectImage)
            {
                SceneObjectImage btn = (obj as SceneObjectImage);

                System.Drawing.Bitmap _bmp = null;
                if (e.Item.Properties.ContainsKey("Bitmap"))
                {
                    _bmp = (e.Item.Properties["Bitmap"] as System.Drawing.Bitmap);
                }
                if (_bmp == null)
                {
                    string imageFileName = btn.BackgroundImageFileName;

                    string ParentDirectory = String.Empty;
                    if (e.Item.Properties.ContainsKey("ParentDirectory")) ParentDirectory = e.Item.Properties["ParentDirectory"].ToString();
                    if (!String.IsNullOrEmpty(ParentDirectory)) imageFileName = ParentDirectory + "\\" + imageFileName;

                    if (!System.IO.File.Exists(imageFileName))
                    {
                        e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.Item.Bounds);
                        return;
                    }

                    PictureCollectionObjectModel picc = new PictureCollectionObjectModel();
                    FileAccessor.Load(imageFileName, picc, icn, true);
                    _bmp = picc.Pictures[(int)btn.BackgroundImageIndex].ToBitmap();
                    e.Item.Properties["Bitmap"] = _bmp;
                }
                e.Graphics.DrawImage(_bmp, e.Item.Bounds);
            }
            else if (obj is SceneObjectLabel)
            {
                SceneObjectLabel lbl = (obj as SceneObjectLabel);

                PictureCollectionObjectModel piccFont = null;
                if (e.Item.Properties.ContainsKey("Font"))
                {
                    piccFont = (e.Item.Properties["Font"] as PictureCollectionObjectModel);
                }
                if (piccFont == null)
                {
                    string imageFileName = lbl.FontFileName;
                    imageFileName = "FONT.ICN";

                    string ParentDirectory = String.Empty;
                    if (e.Item.Properties.ContainsKey("ParentDirectory")) ParentDirectory = e.Item.Properties["ParentDirectory"].ToString();
                    if (!String.IsNullOrEmpty(ParentDirectory)) imageFileName = ParentDirectory + "\\" + imageFileName;

                    if (!System.IO.File.Exists(imageFileName))
                    {
                        e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.Item.Bounds);
                        return;
                    }

                    piccFont = new PictureCollectionObjectModel();
                    FileAccessor.Load(imageFileName, piccFont, icn, true);
                    e.Item.Properties["Font"] = piccFont;
                }
                
                System.Drawing.Bitmap bitmap = RenderFontICN(lbl.Text, piccFont, e.Item.Bounds);
                if (bitmap != null) e.Graphics.DrawImage(bitmap, e.Item.Bounds);

                // e.Graphics.DrawRectangle(System.Drawing.Pens.Black, e.Item.Bounds);
                // e.Graphics.DrawString(lbl.Text, System.Drawing.SystemFonts.MenuFont, System.Drawing.Brushes.Black, e.Item.Bounds);
            }

            // e.Graphics.FillRectangle(System.Drawing.Brushes.Red, e.Item.Bounds);
        }

        private Dictionary<char, int> icnsdata = null;
        private System.Drawing.Bitmap RenderFontICN(string p, PictureCollectionObjectModel piccFont, System.Drawing.Rectangle rectangle)
        {
            if (icnsdata == null)
            {
                char[] icns = new char[] { '\'', '!', '"', '*', '$', '%', '&', '\'', '(', ')', '*', '+', '`', '-', '\'', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '\'', '=', '\'', '?', '\'', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '.', ']', '.', '_', ',', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                icnsdata = new Dictionary<char, int>();
                for (int i = 0; i < icns.Length; i++)
                {
                    if (icnsdata.ContainsKey(icns[i])) continue;
                    icnsdata.Add(icns[i], i);
                }
            }

            if (rectangle.Width <= 0 || rectangle.Height <= 0) return null;

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rectangle.Width, rectangle.Height);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            int maxpich = piccFont.MaximumPictureHeight;

            int x = 0, y = 0;
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] == ' ')
                {
                    x += 8;
                    continue;
                }
                int index = icnsdata[p[i]];

                System.Drawing.Bitmap bmp = piccFont.Pictures[index].ToBitmap();
                int diff = maxpich - bmp.Height;

                graphics.DrawImage(bmp, x, y + diff);
                x += bmp.Width;
            }
            return bitmap;
        }

        protected override void RenderNonClientArea(DesignerObjectPaintEventArgs e)
        {
        }
    }
}
