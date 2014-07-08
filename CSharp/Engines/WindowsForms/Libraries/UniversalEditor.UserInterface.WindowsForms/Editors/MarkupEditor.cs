using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.Editors
{
    public partial class MarkupEditor : Editor
    {
        public MarkupEditor()
        {
            InitializeComponent();

            base.SupportedObjectModels.Add(typeof(MarkupObjectModel));

            string dirname = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
            {
                "Editors",
                "Markup",
                "Images"
            });

            // don't even try to load if the data directory does not exist (this should be a global
            // Editor functionality, not specific to MarkupEditor...)
            if (System.IO.Directory.Exists(dirname))
            {
                string[] FileNames = System.IO.Directory.GetFiles(dirname);

                foreach (string FileName in FileNames)
                {
                    string FileTitle = System.IO.Path.GetFileNameWithoutExtension(FileName);
                    Image image = Image.FromFile(FileName);
                    imlSmallIcons.Images.Add(FileTitle, image);
                }
            }

            ActionMenuItem mnuMarkup = new ActionMenuItem("mnuMarkup", "&Markup");
            mnuMarkup.Position = 4;

            ActionMenuItem mnuXMLApplyStylesheet = new ActionMenuItem("mnuXMLApplyStylesheet", "Apply &Stylesheet");
            mnuXMLApplyStylesheet.Click += mnuXMLApplyStylesheet_Click;

            mnuMarkup.Items.Add(mnuXMLApplyStylesheet);

            this.MenuBar.Items.Add(mnuMarkup);
        }

        private void mnuXMLApplyStylesheet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Applying stylesheet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnObjectModelChanged(EventArgs e)
        {
            base.OnObjectModelChanged(e);

            MarkupObjectModel mom = (ObjectModel as MarkupObjectModel);
            tv.Nodes.Clear();
            if (mom == null) return;

            foreach (MarkupElement el in mom.Elements)
            {
                RecursiveLoadElement(el);
            }
        }

        private void RecursiveLoadElement(MarkupElement el, TreeNode parent = null)
        {
            TreeNode tn = new TreeNode();
            if (el is MarkupPreprocessorElement)
            {
                tn.ImageKey = "Preprocessor";
            }
            else if (el is MarkupTagElement)
            {
                tn.ImageKey = "Tag";
            }
            else if (el is MarkupCommentElement)
            {
                tn.ImageKey = "Comment";
            }
            tn.Text = el.FullName;
            tn.SelectedImageKey = tn.ImageKey;
            tn.Tag = el;

            if (el is MarkupContainerElement)
            {
                foreach (MarkupElement el1 in (el as MarkupContainerElement).Elements)
                {
                    RecursiveLoadElement(el1, tn);
                }
            }

            if (parent != null)
            {
                parent.Nodes.Add(tn);
            }
            else
            {
                tv.Nodes.Add(tn);
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pnlTag.Enabled = false;
            pnlTag.Visible = false;

            if (e.Node == null) return;

            MarkupElement el = (e.Node.Tag as MarkupElement);
            if (el == null) return;

            if (el is MarkupTagElement)
            {
                MarkupTagElement tag = (el as MarkupTagElement);
                pnlTag.Enabled = true;
                pnlTag.Visible = true;

                txtTagName.Text = tag.Name;
                txtNamespace.Text = tag.Namespace;

                if (String.IsNullOrEmpty(tag.Value))
                {
                    optValueEmpty.Checked = true;
                    txtValue.Text = String.Empty;
                }
                else
                {
                    optValueSpecific.Checked = true;
                    txtValue.Text = tag.Value;
                }

                lvAttributes.Items.Clear();
                foreach (MarkupAttribute att in tag.Attributes)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageKey = "Attribute";
                    lvi.Text = att.Name;
                    lvi.SubItems.Add(att.Value);

                    lvAttributes.Items.Add(lvi);
                }
            }
        }

        private void optValue_CheckedChanged(object sender, EventArgs e)
        {
            txtValue.ReadOnly = !optValueSpecific.Checked;
        }

        private void txtValue_Validated(object sender, EventArgs e)
        {
            BeginEdit();
            EndEdit();
        }
    }
}
