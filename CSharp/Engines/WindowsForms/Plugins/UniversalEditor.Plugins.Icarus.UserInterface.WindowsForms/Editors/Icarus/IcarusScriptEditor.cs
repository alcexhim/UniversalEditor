using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.Icarus.Commands;

namespace UniversalEditor.Editors.Icarus
{
    public partial class IcarusScriptEditor : Editor
    {
        public IcarusScriptEditor()
        {
            InitializeComponent();
            mnuContextRun.Font = new Font(mnuContextRun.Font, FontStyle.Bold);
            
            /*
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string iconPath = path + System.IO.Path.DirectorySeparatorChar.ToString() + "../Editors/Icarus/Images";
            string[] iconFileNames = System.IO.Directory.GetFiles(iconPath, "*.png");
            foreach (string iconFileName in iconFileNames)
            {
                Image image = Image.FromFile(iconFileName);
                string fileTitle = System.IO.Path.GetFileNameWithoutExtension(iconFileName);
                imlSmallIcons.Images.Add(fileTitle, image);
            }
            */

            base.SupportedObjectModels.Add(typeof(IcarusScriptObjectModel));

            tv.ImageList = SmallImageList;

            ActionMenuItem mnuDebug = MenuBar.Items.Add("mnuDebug", "&Debug", 4);
            mnuDebug.Items.Add("mnuDebugStart", "&Start Debugging", mnuDebugStart_Click);
            mnuDebug.Items.Add("mnuDebugBreak", "Brea&k Execution", mnuDebugBreak_Click);
            mnuDebug.Items.Add("mnuDebugStop", "Stop D&ebugging", mnuDebugStop_Click);
            mnuDebug.Items.AddSeparator();
            mnuDebug.Items.Add("mnuDebugStepInto", "Step &Into", mnuDebugStepInto_Click);
            mnuDebug.Items.Add("mnuDebugStepOver", "Step &Over", mnuDebugStepOver_Click);

            Toolbar tbDebug = Toolbars.Add("tbDebug", "ICARUS Debug");
            tbDebug.Items.Add("mnuDebugStart", "&Start Debugging", mnuDebugStart_Click);
            tbDebug.Items.AddSeparator();
            tbDebug.Items.Add("mnuDebugStepInto", "Step &Into", mnuDebugStepInto_Click);
            tbDebug.Items.Add("mnuDebugStepOver", "Step &Over", mnuDebugStepOver_Click);

            mnuDebug.Items["mnuDebugBreak"].Visible = false;
            mnuDebug.Items["mnuDebugStop"].Visible = false;
        }

        private System.Threading.Thread tDebugger = null;
        private void mnuDebugStart_Click(object sender, EventArgs e)
        {
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStart"].Visible = false;
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugBreak"].Visible = true;
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStop"].Visible = true;

            IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);
            tv.SelectedNode = null;
            tasksByName.Clear();

            if (tDebugger != null)
            {
                if (tDebugger.IsAlive) tDebugger.Abort();
                tDebugger = null;
            }

            tDebugger = new System.Threading.Thread(tDebugger_Start);
            tDebugger.Start();
        }
        private void mnuDebugBreak_Click(object sender, EventArgs e)
        {

        }
        private void mnuDebugStop_Click(object sender, EventArgs e)
        {
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStart"].Visible = true;
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugBreak"].Visible = false;
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStop"].Visible = false;

            if (tDebugger != null)
            {
                if (tDebugger.IsAlive) tDebugger.Abort();
                tDebugger = null;
            }

            if (_prevTreeNode != null)
            {
                _prevTreeNode.BackColor = Color.Empty;
                _prevTreeNode = null;
            }
        }

        private void LogOutputWindow(string text)
        {
            HostApplication.OutputWindow.WriteLine(text);
        }
        private void ClearOutputWindow()
        {
            HostApplication.OutputWindow.Clear();
        }

        private Dictionary<IcarusCommand, TreeNode> treeNodesForCommands = new Dictionary<IcarusCommand, TreeNode>();

        private void tDebugger_Start()
        {
            Action _ClearOutputWindow = new Action(ClearOutputWindow);
            Action<string> _LogOutputWindow = new Action<string>(LogOutputWindow);

            Invoke(_ClearOutputWindow);
            Invoke(_LogOutputWindow, "=== ICARUS Engine Debugger v1.0 - copyright (c) 2013 Mike Becker's Software ===");

            DateTime dtStart = DateTime.Now;

            IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);
            foreach (IcarusCommand command in script.Commands)
            {
                try
                {
                    DebugCommand(command);
                }
                catch (InvalidOperationException ex)
                {
                    Invoke(_LogOutputWindow, "unknown command (" + (script.Commands.IndexOf(command) + 1).ToString() + " of " + script.Commands.Count.ToString() + "): " + command.GetType().Name);
                }
            }

            Action<TreeNode> _ReleaseTreeNode = new Action<TreeNode>(ReleaseTreeNode);
            if (_prevTreeNode != null)
            {
                Invoke(_ReleaseTreeNode, _prevTreeNode);
                _prevTreeNode = null;
            }

            DateTime dtEnd = DateTime.Now;

            TimeSpan tsDiff = dtEnd - dtStart;
            Invoke(_LogOutputWindow, "execution complete, " + tsDiff.ToString() + " elapsed since execution started");

            Action<bool> _UpdateMenuItems = new Action<bool>(UpdateMenuItems);
            Invoke(_UpdateMenuItems, true);
        }

        private void UpdateMenuItems(bool enable)
        {
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStart"].Visible = enable;
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugBreak"].Visible = !enable;
            (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStop"].Visible = !enable;
        }

        private TreeNode _prevTreeNode = null;
        private void ActivateTreeNode(TreeNode tn)
        {
            tn.EnsureVisible();
            tn.BackColor = Color.Yellow;
        }
        private void ReleaseTreeNode(TreeNode tn)
        {
            tn.BackColor = Color.Empty;
        }

        private Dictionary<string, IcarusCommandTask> tasksByName = new Dictionary<string, IcarusCommandTask>();

        private void DebugCommand(IcarusCommand command)
        {
            Action<TreeNode> _ActivateTreeNode = new Action<TreeNode>(ActivateTreeNode);
            Action<TreeNode> _ReleaseTreeNode = new Action<TreeNode>(ReleaseTreeNode);
            if (_prevTreeNode != null)
            {
                Invoke(_ReleaseTreeNode, _prevTreeNode);
                _prevTreeNode = null;
            }

            TreeNode tn = treeNodesForCommands[command];
            Invoke(_ActivateTreeNode, tn);
            _prevTreeNode = tn;

            Action<string> _LogOutputWindow = new Action<string>(LogOutputWindow);
            if (command is IcarusCommandAffect)
            {
                IcarusCommandAffect cmd = (command as IcarusCommandAffect);
                Invoke(_LogOutputWindow, "on " + cmd.Target.Value.ToString() + "\r\n{");
                foreach (IcarusCommand command1 in cmd.Commands)
                {
                    DebugCommand(command1);
                }
                Invoke(_LogOutputWindow, "}");
            }
            else if (command is IcarusCommandSet)
            {
                IcarusCommandSet cmd = (command as IcarusCommandSet);
                Invoke(_LogOutputWindow, "set " + cmd.ObjectName + " = " + (cmd.Value == null ? "(null)" : cmd.Value.ToString()));
            }
            else if (command is IcarusCommandWait)
            {
                IcarusCommandWait cmd = (command as IcarusCommandWait);
                int timeout = (int)cmd.Duration;
                System.Threading.Thread.Sleep(timeout);
            }
            else if (command is IcarusCommandPrint)
            {
                IcarusCommandPrint cmd = (command as IcarusCommandPrint);
                string text = cmd.Text;
                Invoke(_LogOutputWindow, text);
            }
            else if (command is IcarusCommandTask)
            {
                IcarusCommandTask cmd = (command as IcarusCommandTask);
                if (tasksByName.ContainsKey(cmd.TaskName))
                {
                    Invoke(_LogOutputWindow, "WARNING: redefining task \"" + cmd.TaskName + "\"");
                }
                tasksByName[cmd.TaskName] = cmd;
            }
            else if (command is IcarusCommandControlFlowDo)
            {
                IcarusCommandControlFlowDo cmd = (command as IcarusCommandControlFlowDo);
                if (!tasksByName.ContainsKey(cmd.Target))
                {
                    Invoke(_LogOutputWindow, "ERROR: task \"" + cmd.Target + "\" not found!");
                    return;
                }

                IcarusCommandTask task = tasksByName[cmd.Target];
                foreach (IcarusCommand command1 in task.Commands)
                {
                    DebugCommand(command1);
                }
            }
            else if (command is IcarusCommandLoop)
            {
                IcarusCommandLoop cmd = (command as IcarusCommandLoop);
                float timeout = (float)cmd.Count;
                if (timeout == -1)
                {
                    while (true)
                    {
                        foreach (IcarusCommand command1 in cmd.Commands)
                        {
                            DebugCommand(command1);
                        }
                    }
                }
                else
                {
                    for (float i = 0; i < timeout; i++)
                    {
                        foreach (IcarusCommand command1 in cmd.Commands)
                        {
                            DebugCommand(command1);
                        }
                    }
                }
            }
            else if (command is IcarusCommandControlFlowDo)
            {
            }
            else
            {
                throw new InvalidOperationException();
            }

            System.Threading.Thread.Sleep(50);
        }

        private void mnuDebugStepInto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Step Into Icarus Script", "Information", MessageBoxButtons.OK);
        }
        private void mnuDebugStepOver_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Step Over Icarus Script", "Information", MessageBoxButtons.OK);
        }

        protected override void OnDocumentClosing(CancelEventArgs e)
        {
            base.OnDocumentClosing(e);

            if (tDebugger != null && tDebugger.IsAlive)
            {
                if (MessageBox.Show("Do you want to stop debugging?", "ICARUS Debugger", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                tDebugger.Abort();
            }
        }
        
        protected override void OnObjectModelChanged(EventArgs e)
        {
            base.OnObjectModelChanged(e);

            tv.Nodes.Clear();
            treeNodesForCommands.Clear();

            IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);
            if (script == null) return;

            foreach (IcarusCommand command in script.Commands)
            {
                RecursiveAddCommand(command);
            }
        }

        private void RecursiveAddCommand(IcarusCommand command, TreeNode parent = null)
        {
            TreeNode tn = new TreeNode();
            StringBuilder sb = new StringBuilder();
            if (command == null) return;

            if (command is IcarusPredefinedCommand)
            {
                sb.Append((command as IcarusPredefinedCommand).Name);
            }
            else if (command is IcarusCustomCommand)
            {
                sb.Append((command as IcarusCustomCommand).CommandType.ToString());
            }
            tn.ImageKey = command.GetType().Name;
            tn.SelectedImageKey = command.GetType().Name;

            if (command is IcarusCommandSet)
            {
                IcarusCommandSet cmd = (command as IcarusCommandSet);
                sb.Append("                ( ");
                if (cmd.ObjectName != null)
                {
                    sb.Append(cmd.ObjectName);
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(", ");
                if (cmd.Value != null)
                {
                    sb.Append(cmd.Value.ToString());
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(" );");
            }
            else if (command is IcarusCommandAffect)
            {
                IcarusCommandAffect cmd = (command as IcarusCommandAffect);
                sb.Append("                ( ");
                if (cmd.Target != null)
                {
                    sb.Append(cmd.Target.ToString());
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(" );");
            }
            else if (command is IcarusCommandUse)
            {
                IcarusCommandUse cmd = (command as IcarusCommandUse);
                sb.Append("                ( ");
                if (cmd.Target != null)
                {
                    sb.Append(cmd.Target.ToString());
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(" );");
            }
            else if (command is IcarusCommandRun)
            {
                IcarusCommandRun cmd = (command as IcarusCommandRun);
                sb.Append("                ( ");
                if (cmd.Target != null)
                {
                    sb.Append(cmd.Target.ToString());
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(" );");
            }
            else if (command is IcarusCommandKill)
            {
                IcarusCommandKill cmd = (command as IcarusCommandKill);
                sb.Append("                ( ");
                if (cmd.Target != null)
                {
                    sb.Append(cmd.Target.ToString());
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(" );");
            }

            if (command is IIcarusContainerCommand)
            {
                IIcarusContainerCommand container = (command as IIcarusContainerCommand);
                foreach (IcarusCommand ic1 in container.Commands)
                {
                    RecursiveAddCommand(ic1, tn);
                }

                sb.Append("                (" + container.Commands.Count.ToString() + " commands)");
            }

            tn.Text = sb.ToString();
            treeNodesForCommands.Add(command, tn);

            if (parent == null)
            {
                tv.Nodes.Add(tn);
            }
            else
            {
                parent.Nodes.Add(tn);
            }
        }

        private void tv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode tn = tv.HitTest(e.Location).Node;
            if (tn != null)
            {
                Dialogs.Icarus.IcarusExpressionHelperDialog dlg = new Dialogs.Icarus.IcarusExpressionHelperDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}
