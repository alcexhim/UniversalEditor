using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.Editors.Multimedia.Picture.Collection
{
    public partial class PictureCollectionEditor : Editor
    {
        public PictureCollectionEditor()
        {
            InitializeComponent();
            base.SupportedObjectModels.Add(typeof(PictureCollectionObjectModel));
        }

        private void optView_CheckedChanged(object sender, EventArgs e)
        {
            if (optViewCenter.Checked)
            {
                pic.SizeMode = PictureBoxSizeMode.CenterImage;
                pic.Visible = true;
                pnlTileView.Visible = false;
            }
            else if (optViewStretch.Checked)
            {
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Visible = true;
                pnlTileView.Visible = false;
            }
            else if (optViewZoom.Checked)
            {
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Visible = true;
                pnlTileView.Visible = false;
            }
            else if (optViewTile.Checked)
            {
                pic.Visible = false;
                tex = new TextureBrush(pic.Image);
                pnlTileView.Visible = true;
            }
        }

        private TextureBrush tex = null;
        private void pnlTileView_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(tex, 0, 0, pnlTileView.Width, pnlTileView.Height);
        }

        protected override void OnObjectModelChanged(EventArgs e)
        {
            base.OnObjectModelChanged(e);
            
            PictureCollectionObjectModel pcom = (ObjectModel as PictureCollectionObjectModel);
            if (pcom == null) return;

            txtStartFrame.Minimum = 0;
            txtStartFrame.Maximum = pcom.Pictures.Count;
            txtEndFrame.Minimum = 0;
            txtEndFrame.Maximum = pcom.Pictures.Count;

            txtStartFrame.Value = txtStartFrame.Minimum;
            txtEndFrame.Value = txtEndFrame.Maximum;

            lv.Items.Clear();
            foreach (PictureObjectModel pic in pcom.Pictures)
            {
                AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
                lvi.Data = pic;
                lvi.Image = pic.ToBitmap();
                lv.Items.Add(lvi);
            }
        }

        private void lv_ItemActivate(object sender, EventArgs e)
        {
            PictureObjectModel picc = (lv.SelectedItems[0].Data as PictureObjectModel);
            RenderPicture(picc);
        }

        private int _frameDelay = 50;
        private int _frameStart = 0;
        private int _frameEnd = 0;

        private System.Threading.Thread tPlayThread = null;
        private void tPlayThread_ThreadStart()
        {
            PictureCollectionObjectModel coll = (ObjectModel as PictureCollectionObjectModel);
            for (int i = _frameStart; i <= _frameEnd; i++)
            {
                if (i >= 0 && i < coll.Pictures.Count)
                {
                    PictureObjectModel picc = coll.Pictures[i];
                    try
                    {
                        Invoke(new Action<PictureObjectModel>(RenderPicture), picc);
                    }
                    catch (InvalidOperationException ex)
                    {
                    }
                    System.Threading.Thread.Sleep(_frameDelay);
                }
                
                if (i == _frameEnd)
                {
                    i = _frameStart -  1;
                }

                if (!IsHandleCreated) break;
            }
        }

        private PictureObjectModel mvarCurrentPicture = null;

        private void RenderPicture(PictureObjectModel picc)
        {
            mvarCurrentPicture = picc;
            pic.Image = picc.ToBitmap();
            tex = new TextureBrush(pic.Image);
            pnlTileView.Refresh();
        }

        private void cmdAnimatePlay_Click(object sender, EventArgs e)
        {
            if (tPlayThread != null)
            {
                tPlayThread.Abort();
                tPlayThread = null;
                cmdAnimatePlay.Text = "&Play";
                return;
            }
            else
            {
                _frameDelay = (int)txtFrameDelay.Value;
                _frameStart = (int)txtStartFrame.Value;
                _frameEnd = (int)txtEndFrame.Value;

                cmdAnimatePlay.Text = "Sto&p";
                tPlayThread = new System.Threading.Thread(tPlayThread_ThreadStart);
                tPlayThread.Start();
            }
        }

        private void cmdExportCurrent_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export Current Frame";

            ObjectModelReference omr = mvarCurrentPicture.MakeReference();
            sfd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(omr);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(sfd.FileName);
                if (dfrs.Length > 0)
                {
                    DataFormat df = dfrs[0].Create();

                    UniversalEditor.Accessors.File.FileAccessor.Save(sfd.FileName, mvarCurrentPicture, df, true);
                }
            }
        }

        private void cmdExportAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
