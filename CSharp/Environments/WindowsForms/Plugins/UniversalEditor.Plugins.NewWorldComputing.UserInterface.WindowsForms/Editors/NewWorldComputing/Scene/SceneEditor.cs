using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.NWCSceneLayout;

using AwesomeControls.Designer;
using UniversalEditor.Editors.NewWorldComputing.Scene.DesignerObjectClasses;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN;

using UniversalEditor.ObjectModels.NWCSceneLayout.SceneObjects;

using UniversalEditor.Editors.NewWorldComputing.Scene.DesignerAreas;

namespace UniversalEditor.Editors.NewWorldComputing.Scene
{
    public partial class SceneEditor : Editor
    {
        public SceneEditor()
        {
            InitializeComponent();
            base.SupportedObjectModels.Add(typeof(NWCSceneLayoutObjectModel));
            base.DoubleBuffered = true;

            txtLeft.Minimum = UInt16.MinValue;
            txtLeft.Maximum = UInt16.MaxValue;

            txtTop.Minimum = UInt16.MinValue;
            txtTop.Maximum = UInt16.MaxValue;

            txtWidth.Minimum = UInt16.MinValue;
            txtWidth.Maximum = UInt16.MaxValue;

            txtHeight.Minimum = UInt16.MinValue;
            txtHeight.Maximum = UInt16.MaxValue;
        }

        protected override void OnObjectModelChanged(EventArgs e)
        {
            base.OnObjectModelChanged(e);

            NWCSceneLayoutObjectModel scene = (ObjectModel as NWCSceneLayoutObjectModel);
            if (scene == null) return;

            RefreshEditor();
        }

        private Bitmap _scnBmp = null;
        private ICNDataFormat icn = new ICNDataFormat();

        private void RefreshEditor()
        {
            dsnr.Areas.Clear();

            NWCSceneLayoutObjectModel scene = (ObjectModel as NWCSceneLayoutObjectModel);
            if (scene == null) return;

            BasicDesignerArea area = new BasicDesignerArea();
            if (chkVisibilityScreen.Checked)
            {
                area.Scene = scene;
            }
            area.Left = 0;
            area.Top = 0;
            area.Width = scene.Width;
            area.Height = scene.Height;

            NwcGenericControlClass ocButton = new NwcGenericControlClass();
            // dsnr.Classes.Add(ocButton);

            string ParentDirectory = String.Empty;
            if (ObjectModel.Accessor is FileAccessor) ParentDirectory = System.IO.Path.GetDirectoryName((ObjectModel.Accessor as FileAccessor).FileName);

            foreach (SceneObject obj in scene.Objects)
            {
                if ((obj is SceneObjectButton) && !chkVisibilityButton.Checked) continue;
                if ((obj is SceneObjectImage) && !chkVisibilityImage.Checked) continue;
                if ((obj is SceneObjectLabel) && !chkVisibilityLabel.Checked) continue;

                DesignerObject objct = new DesignerObject("Object" + (scene.Objects.IndexOf(obj) + 1).ToString(), ocButton);
                objct.Bounds = new Rectangle(obj.Left, obj.Top, obj.Width, obj.Height);
                objct.Properties.Add("SceneObject", obj);

                objct.Properties.Add("ParentDirectory", ParentDirectory);

                area.Objects.Add(objct);
            }
            dsnr.Areas.Add(area);
            dsnr.Refresh();
        }

        private void chkVisibility_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEditor();
        }

        private SceneObject mvarCurrentObject = null;
        private void dsnr_DesignerObjectSelected(object sender, DesignerObjectSelectedEventArgs e)
        {
            if (e.Item == null)
            {
                pnlObjectSpecificProperties.Visible = false;
                pnlObjectSpecificProperties.Enabled = false;
                pnlCommonProperties.Visible = false;
                pnlCommonProperties.Enabled = false;
                return;
            }

            pnlObjectSpecificProperties.Enabled = true;
            pnlObjectSpecificProperties.Visible = true;
            pnlCommonProperties.Enabled = true;
            pnlCommonProperties.Visible = true;

            txtLeft.Value = e.Item.Left;
            txtTop.Value = e.Item.Top;
            txtWidth.Value = e.Item.Width;
            txtHeight.Value = e.Item.Height;

            SceneObject obj = (e.Item.Properties["SceneObject"] as SceneObject);
            if (obj is SceneObjectButton)
            {
                fraObjectProperties.Text = "Button Properties";
                SwitchObjectPropertiesPanel(pnlObjectPropertiesButton);
            }
            else if (obj is SceneObjectImage)
            {
                fraObjectProperties.Text = "Image Properties";
                SwitchObjectPropertiesPanel(pnlObjectPropertiesImage);
            }
            else if (obj is SceneObjectLabel)
            {
                SceneObjectLabel lbl = (obj as SceneObjectLabel);

                fraObjectProperties.Text = "Label Properties";
                SwitchObjectPropertiesPanel(pnlObjectPropertiesLabel);

                txtLabelText.Text = lbl.Text;
            }
            mvarCurrentObject = obj;
        }

        private void SwitchObjectPropertiesPanel(Panel pnl)
        {
            foreach (Control ctl in pnlObjectSpecificProperties.Controls)
            {
                if (ctl == pnl)
                {
                    ctl.Enabled = true;
                    ctl.Visible = true;
                }
                else
                {
                    ctl.Visible = false;
                    ctl.Enabled = false;
                }
            }
        }

        private void txtLabelText_Validated(object sender, EventArgs e)
        {
            if (mvarCurrentObject is SceneObjectLabel)
            {
                SceneObjectLabel lbl = (mvarCurrentObject as SceneObjectLabel);
                lbl.Text = txtLabelText.Text;
                dsnr.Refresh();
            }
        }

        private void txtBounds_Validated(object sender, EventArgs e)
        {
            foreach (DesignerObject obj in dsnr.SelectedObjects)
            {
                obj.Left = (int)txtLeft.Value;
                obj.Top = (int)txtTop.Value;
                obj.Width = (int)txtWidth.Value;
                obj.Height = (int)txtHeight.Value;
            }
        }

        /*
        void dsnr_Paint(object sender, PaintEventArgs e)
        {
            NWCSceneLayoutObjectModel scene = (ObjectModel as NWCSceneLayoutObjectModel);
            if (scene == null) return;

            string ParentDirectory = String.Empty;
            if (ObjectModel.Accessor is FileAccessor) ParentDirectory = System.IO.Path.GetDirectoryName((ObjectModel.Accessor as FileAccessor).FileName);

            if (_scnBmp == null)
            {
                string imageFileName = scene.BackgroundImageFileName;
                if (!String.IsNullOrEmpty(ParentDirectory)) imageFileName = ParentDirectory + "\\" + imageFileName;
                if (!System.IO.File.Exists(imageFileName))
                {
                    return;
                }

                PictureCollectionObjectModel picc = new PictureCollectionObjectModel();
                FileAccessor.Load(imageFileName, picc, icn, true);
                _scnBmp = picc.Pictures[(int)scene.BackgroundImageIndex].ToBitmap();
            }
            e.Graphics.DrawImage(_scnBmp, new Rectangle(0, 0, scene.Width, scene.Height));

        }
        */

    }
}
