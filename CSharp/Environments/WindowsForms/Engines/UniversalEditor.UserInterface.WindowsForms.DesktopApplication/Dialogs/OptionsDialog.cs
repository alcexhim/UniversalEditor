using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
    public partial class OptionsDialog : AwesomeControls.Dialog
    {
        public OptionsDialog()
        {
            InitializeComponent();

            IconMethods.PopulateSystemIcons(ref imlSmallIcons);

            IOptionPanelImplementation[] panels = UniversalEditor.UserInterface.Common.Reflection.GetAvailableOptionPanels();
            foreach (IOptionPanelImplementation panel in panels)
            {
                if (panel is OptionPanel && panel.GetType().IsSubclassOf(typeof(OptionPanel)))
                {
                    OptionPanel oppanel = (panel as OptionPanel);
                    TreeNode tnParent = null;

                    #region get the parent
                    for (int i = 0; i < oppanel.OptionGroups.Length - 1; i++)
                    {
                        TreeNodeCollection tnc = null;
                        if (tnParent == null)
                        {
                            tnc = tv.Nodes;
                        }
                        else
                        {
                            tnc = tnParent.Nodes;
                        }
                        if (tnc.ContainsKey(oppanel.OptionGroups[i]))
                        {
                            tnParent = tnc[oppanel.OptionGroups[i]];
                        }
                        else
                        {
                            tnParent = tnc.Add(oppanel.OptionGroups[i], oppanel.OptionGroups[i], "generic-folder-closed");
                        }
                    }
                    #endregion

                    if (tnParent == null)
                    {
                        tnParent = tv.Nodes.Add(oppanel.OptionGroups[oppanel.OptionGroups.Length - 1], oppanel.OptionGroups[oppanel.OptionGroups.Length - 1], oppanel.OptionGroups[oppanel.OptionGroups.Length - 1]);
                    }
                    else
                    {
                        tnParent = tnParent.Nodes.Add(oppanel.OptionGroups[oppanel.OptionGroups.Length - 1], oppanel.OptionGroups[oppanel.OptionGroups.Length - 1], oppanel.OptionGroups[oppanel.OptionGroups.Length - 1]);
                    }


                    tnParent.Tag = oppanel;
                    tnParent.SelectedImageKey = tnParent.ImageKey;
                    if (!imlSmallIcons.Images.ContainsKey(tnParent.Text) && oppanel.IconImage != null)
                    {
                        imlSmallIcons.Images.Add(tnParent.Text, oppanel.IconImage);
                    }

                    oppanel.Dock = DockStyle.Fill;
                    pnlContainer.Controls.Add(oppanel);
                }
            }

            Font = SystemFonts.MenuFont;
            AutoSize = true;
        }

        private void SwitchPanel(OptionPanel panel)
        {
            foreach (Control ctl in pnlContainer.Controls)
            {
                if (ctl == panel)
                {
                    ctl.Enabled = true;
                    ctl.Visible = true;
                    ctl.BringToFront();
                }
                else
                {
                    ctl.Visible = false;
                    ctl.Enabled = false;
                }
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            OptionPanel panel = (e.Node.Tag as OptionPanel);
            SwitchPanel(panel);
        }

        private void tv_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            if (e.Node.ImageKey == "generic-folder-open")
            {
                e.Node.ImageKey = "generic-folder-closed";
                e.Node.SelectedImageKey = "generic-folder-closed";
            }
        }

        private void tv_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            if (e.Node.ImageKey == "generic-folder-closed")
            {
                e.Node.ImageKey = "generic-folder-open";
                e.Node.SelectedImageKey = "generic-folder-open";
            }
        }
    }
}
