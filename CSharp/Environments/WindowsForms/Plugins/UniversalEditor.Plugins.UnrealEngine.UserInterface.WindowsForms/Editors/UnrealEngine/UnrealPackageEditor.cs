using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.UnrealEngine;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.UnrealEngine
{
    public partial class UnrealPackageEditor : Editor
    {
        public UnrealPackageEditor()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;

            base.SupportedObjectModels.Add(typeof(UnrealPackageObjectModel));
        }

        protected override void OnObjectModelChanged(EventArgs e)
        {
            base.OnObjectModelChanged(e);

            tv.Nodes.Clear();

            UnrealPackageObjectModel upk = (ObjectModel as UnrealPackageObjectModel);
            if (upk == null) return;

            TreeNode tnToplevel = new TreeNode();

            if (upk.Accessor is FileAccessor) tnToplevel.Text = System.IO.Path.GetFileName((upk.Accessor as FileAccessor).FileName);
            if (String.IsNullOrEmpty(tnToplevel.Text)) tnToplevel.Text = "<ROOT>";

            #region Name Table
            {
                TreeNode tn = new TreeNode();
                tn.Name = "nodeNameTable";
                tn.Text = "Name Table";
                tnToplevel.Nodes.Add(tn);

                foreach (NameTableEntry entry in upk.NameTableEntries)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = entry;
                    lvi.Text = entry.Name;
                    lvi.SubItems.Add(entry.Flags.ToString());
                    lvNameTable.Items.Add(lvi);
                }
                lvNameTable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            #endregion
            #region Export Table
            {
                TreeNode tn = new TreeNode();
                tn.Name = "nodeExportTable";
                tn.Text = "Export Table";
                tnToplevel.Nodes.Add(tn);

                foreach (ExportTableEntry entry in upk.ExportTableEntries)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = entry;
                    if (entry.Name == null)
                    {
                        lvi.Text = String.Empty;
                    }
                    else
                    {
                        lvi.Text = entry.Name.ToString(false);
                    }
                    if (entry.ObjectParent == null)
                    {
                        lvi.SubItems.Add(String.Empty);
                    }
                    else
                    {
                        lvi.SubItems.Add(entry.ObjectParent.ToString());
                    }
                    if (entry.ObjectClass == null)
                    {
                        lvi.SubItems.Add(String.Empty);
                    }
                    else
                    {
                        lvi.SubItems.Add(entry.ObjectClass.ToString());
                    }
                    lvi.SubItems.Add(entry.Flags.ToString());
                    lvi.SubItems.Add(entry.Offset.ToString());
                    lvi.SubItems.Add(entry.Size.ToString());
                    lvExportTable.Items.Add(lvi);
                }
                lvExportTable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            #endregion
            #region Import Table
            {
                TreeNode tn = new TreeNode();
                tn.Name = "nodeImportTable";
                tn.Text = "Import Table";
                tnToplevel.Nodes.Add(tn);

                foreach (ImportTableEntry entry in upk.ImportTableEntries)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = entry;
                    if (entry.PackageName != null)
                    {
                        lvi.Text = entry.PackageName.ToString(false);
                    }
                    else
                    {
                        lvi.Text = "(invalid)";
                    }
                    if (entry.ObjectName != null)
                    {
                        lvi.SubItems.Add(entry.ObjectName.ToString(false));
                    }
                    else
                    {
                        lvi.SubItems.Add("(invalid)");
                    }
                    if (entry.ClassName != null)
                    {
                        lvi.SubItems.Add(entry.ClassName.ToString(false));
                    }
                    else
                    {
                        lvi.SubItems.Add("(invalid)");
                    }
                    lvImportTable.Items.Add(lvi);
                }
                lvImportTable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            #endregion
            #region Heritage Table
            {
                TreeNode tn = new TreeNode();
                tn.Name = "nodeHeritageTable";
                tn.Text = "Heritage Table";
                tnToplevel.Nodes.Add(tn);

                foreach (Guid guid in upk.PackageGUIDs)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = guid;
                    lvi.Text = guid.ToString("B");
                    lvHeritageTable.Items.Add(lvi);
                }
                lvHeritageTable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            #endregion

            tv.Nodes.Add(tnToplevel);
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            if (e.Node.Name.StartsWith("node"))
            {
                string name = e.Node.Name.Substring(4);
                SwitchPanel("pnl" + name);
            }
        }

        private void SwitchPanel(Panel panel)
        {
            foreach (Control ctl in splitContainer1.Panel2.Controls)
            {
                if (ctl == panel)
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
        private void SwitchPanel(string panelName)
        {
            foreach (Control ctl in splitContainer1.Panel2.Controls)
            {
                if (ctl is Panel && ctl.Name == panelName)
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

        private void tv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) tv.SelectedNode = tv.HitTest(e.Location).Node;
        }

        private void lvExportTable_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem lvi = (e.Item as ListViewItem);
            if (lvi == null) return;

            ExportTableEntry ete = (lvi.Tag as ExportTableEntry);
            if (ete == null) return;

            string FileName = TemporaryFileManager.CreateTemporaryFile(ete.Name.Name, ete.GetData());

            DataObject dobj = new DataObject("FileDrop", new string[] { FileName });
            lvExportTable.DoDragDrop(dobj, DragDropEffects.Copy);
        }
    }
}
