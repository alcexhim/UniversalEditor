//
//  IcarusScriptEditor.cs - provides a UWT-based Editor for an IcarusScriptObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MBS.Framework;
using MBS.Framework.Settings;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.DataFormats.Icarus;
using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.Plugins.RavenSoftware.UserInterface.Dialogs.Icarus;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Icarus
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for an <see cref="IcarusScriptObjectModel" />.
	/// </summary>
	[ContainerLayout("~/Editors/RavenSoftware/Icarus/IcarusScriptEditor.glade")]
	public class IcarusScriptEditor : Editor
	{
		private ListViewControl tv;

		public static IcarusScriptEditorConfiguration IcarusConfiguration { get; } = new IcarusScriptEditorConfiguration();

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(IcarusScriptObjectModel));
				_er.ConfigurationLoaded += _er_ConfigurationLoaded;
			}
			return _er;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			tv.ContextMenuCommandID = "Icarus_ContextMenu";
			OnObjectModelChanged(EventArgs.Empty);
		}

		protected override void OnToolboxItemActivated(ToolboxItemEventArgs e)
		{
			base.OnToolboxItemActivated(e);
			IcarusScriptEditorCommand cmd = e.Item.GetExtraData<IcarusScriptEditorCommand>("command");

			BeginEdit();

			IcarusCommand omcmd = ScriptEditorCommandToOMCommand(cmd);
			RecursiveAddCommand(omcmd);
			(ObjectModel as IcarusScriptObjectModel).Commands.Add(omcmd);

			EndEdit();
		}

		private IcarusCommand ScriptEditorCommandToOMCommand(IcarusScriptEditorCommand cmd)
		{
			IcarusCommand command = new IcarusCommand(cmd.Name, cmd.TypeCode);
			command.Description = cmd.Description;
			for (int i = 0; i < cmd.Parameters.Count; i++)
			{
				command.Parameters.Add(cmd.Parameters[i].Clone() as IcarusParameter);
			}
			for (int i = 0; i < cmd.Commands.Count; i++)
			{
				command.Commands.Add(ScriptEditorCommandToOMCommand(cmd.Commands[i]));
			}
			return command;
		}

		private void _er_ConfigurationLoaded(object sender, EventArgs e)
		{
			if (_er.Configuration != null)
			{
				MarkupTagElement tagConfiguration = (_er.Configuration.Elements["Configuration"] as MarkupTagElement);
				if (tagConfiguration != null)
				{
					MarkupTagElement tagEnumerations = (tagConfiguration.Elements["Enumerations"] as MarkupTagElement);
					if (tagEnumerations != null)
					{
						for (int i = 0; i < tagEnumerations.Elements.Count; i++)
						{
							MarkupTagElement tagEnumeration = (tagEnumerations.Elements[i] as MarkupTagElement);
							if (tagEnumeration == null) continue;
							if (tagEnumeration.FullName != "Enumeration") continue;

							MarkupAttribute attName = tagEnumeration.Attributes["Name"];
							if (attName == null) continue;

							IcarusScriptEditorEnumeration _enum = new IcarusScriptEditorEnumeration();
							_enum.Name = attName.Value;

							MarkupAttribute attDescription = tagEnumeration.Attributes["Description"];
							if (attDescription != null)
							{
								_enum.Description = attDescription.Value;
							}

							MarkupAttribute attValue = tagEnumeration.Attributes["Value"];
							if (attValue != null)
							{
								_enum.Value = new IcarusConstantExpression(attValue.Value);
							}
							else
							{
								_enum.Value = new IcarusConstantExpression(attName.Value);
							}

							IcarusConfiguration.Enumerations.Add(_enum);
						}
					}

					MarkupTagElement tagCommands = (tagConfiguration.Elements["IcarusCommands"] as MarkupTagElement);
					if (tagCommands != null)
					{
						for (int i = 0; i < tagCommands.Elements.Count; i++)
						{
							IcarusScriptEditorCommand cmd = LoadCommandXML(tagCommands.Elements[i] as MarkupTagElement);
							if (cmd != null)
							{
								IcarusConfiguration.Commands.Add(cmd);

								ToolboxItem tbi = new ToolboxCommandItem(cmd.Name, cmd.Name);
								tbi.SetExtraData<IcarusScriptEditorCommand>("command", cmd);
								_er.Toolbox.Items.Add(tbi);
							}
						}
					}
				}
			}
		}

		private IcarusScriptEditorCommand LoadCommandXML(MarkupTagElement tagCommand)
		{
			if (tagCommand == null) return null;
			if (tagCommand.FullName != "IcarusCommand") return null;

			MarkupAttribute attName = tagCommand.Attributes["Name"];
			if (attName == null) return null;

			IcarusScriptEditorCommand cmd = new IcarusScriptEditorCommand();
			cmd.Name = attName.Value;

			MarkupAttribute attTypeCode = tagCommand.Attributes["TypeCode"];
			if (attTypeCode != null)
			{
				cmd.TypeCode = Int32.Parse(attTypeCode.Value);
			}

			MarkupAttribute attIcon = tagCommand.Attributes["Icon"];
			if (attIcon != null)
			{
				cmd.IconName = attIcon.Value;
			}
			MarkupAttribute attDescription = tagCommand.Attributes["Description"];
			if (attDescription != null)
			{
				cmd.Description = attDescription.Value;
			}

			MarkupTagElement tagParameters = tagCommand.Elements["Parameters"] as MarkupTagElement;
			if (tagParameters != null)
			{
				for (int j = 0; j < tagParameters.Elements.Count; j++)
				{
					MarkupTagElement tagParameter = tagParameters.Elements[j] as MarkupTagElement;
					if (tagParameter == null) continue;
					if (tagParameter.FullName != "Parameter") continue;

					string parameterName = GetPositionalParameterNameForCommand(cmd.Name, j);
					MarkupAttribute attParameterName = tagParameter.Attributes["Name"];
					if (attParameterName != null) parameterName = attParameterName.Value;

					IcarusGenericParameter parm = new IcarusGenericParameter(parameterName);

					MarkupAttribute attParameterValue = tagParameter.Attributes["Value"];
					MarkupAttribute attParameterEnumeration = tagParameter.Attributes["Enumeration"];

					MarkupAttribute attAutoCompleteCommandType = tagParameter.Attributes["AutoCompleteCommandType"];
					MarkupAttribute attAutoCompleteParameterIndex = tagParameter.Attributes["AutoCompleteParameterIndex"];

					if (attParameterValue != null)
					{
						parm.Value = new IcarusConstantExpression(attParameterValue.Value);
					}
					if (attParameterEnumeration != null)
					{
						parm.EnumerationName = attParameterEnumeration.Value;
					}

					if (attAutoCompleteCommandType != null)
					{
						parm.AutoCompleteCommandType = (IcarusCommandType)Enum.Parse(typeof(IcarusCommandType), attAutoCompleteCommandType.Value);
						if (attAutoCompleteParameterIndex != null)
						{
							parm.AutoCompleteParameterIndex = Int32.Parse(attAutoCompleteParameterIndex.Value);
						}
					}
					cmd.Parameters.Add(parm);
				}
			}

			MarkupTagElement tagChildCommands = tagCommand.Elements["Commands"] as MarkupTagElement;
			if (tagChildCommands != null)
			{
				for (int j = 0; j < tagChildCommands.Elements.Count; j++)
				{
					IcarusScriptEditorCommand cmdChild = LoadCommandXML(tagChildCommands.Elements[j] as MarkupTagElement);
					if (cmdChild != null)
					{
						cmd.Commands.Add(cmdChild);
					}
				}
			}
			return cmd;
		}

		private string GetPositionalParameterNameForCommand(string name, int pos)
		{
			// FIXME: implement this
			IcarusScriptEditorCommand cmd = IcarusConfiguration.Commands[name];
			if (cmd != null)
			{
				if (pos < cmd.Parameters.Count)
				{
					return cmd.Parameters[pos].Name;
				}
			}

			return String.Format("parm{0}", (pos + 1).ToString());
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}
		public override void UpdateSelections()
		{
			// FIXME: BehavEd writes three or more lines to the system clipboard with the following values:
			// //(BHVD)
			// (command text)
			// ...
			//


		}

		public IcarusScriptEditor()
		{
			// mnuContextRun.Font = new Font(mnuContextRun.Font, FontStyle.Bold);
			// mnuContextRun.IsDefault = true;

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

			// tv.ImageList = SmallImageList;

			// TODO: figure out why menutiems have to use Application.AttachCommand... and context menus have to use Context
			// FIXME: correct me if I'm wrong - maybe because we weren't calling the universal Application.ExecuteCommand() from the menu item handler, but we were in context?
			// ----> this should be fixed!
			Context.AttachCommandEventHandler("Icarus_Debug_StartDebugging", mnuDebugStart_Click);
			Context.AttachCommandEventHandler("Icarus_Debug_BreakExecution", mnuDebugBreak_Click);
			Context.AttachCommandEventHandler("Icarus_Debug_StopDebugging", mnuDebugStop_Click);
			Context.AttachCommandEventHandler("Icarus_Debug_StepInto", mnuDebugStepInto_Click);
			Context.AttachCommandEventHandler("Icarus_Debug_StepOver", mnuDebugStepOver_Click);

			Context.AttachCommandEventHandler("Icarus_ContextMenu_Comment", Icarus_ContextMenu_Comment);
			Context.AttachCommandEventHandler("Icarus_ContextMenu_TEST_EXPRESSION_EDITOR", TestExpressionEditor);
			Context.AttachCommandEventHandler("Icarus_ContextMenu_Insert_From_File", Icarus_ContextMenu_Insert_From_File);
			Context.AttachCommandEventHandler("Icarus_ContextMenu_Rename", Icarus_ContextMenu_Rename);

			// Commands["Icarus_Debug_BreakExecution"].Visible = false;
			// Commands["Icarus_Debug_BreakExecution"].Visible = false;
			// Commands["Icarus_Debug_StopDebugging"].Visible = false;
		}

		/// <summary>
		/// Handles the <see cref="ListViewControl.SelectionChanged" /> event
		/// on <see cref="tv" />, the IcarusCommand tree view.
		/// </summary>
		/// <param name="sender">The <see cref="ListViewControl" /> which generated the event.</param>
		/// <param name="e">The <see cref="EventArgs" /> for the event.</param>
		[EventHandler(nameof(tv), nameof(ListViewControl.SelectionChanged))]
		private void tv_SelectionChanged(object sender, EventArgs e)
		{
			bool hasSelectedItems = tv.SelectedRows.Count > 0;

			Application.Instance.Commands["EditCut"].Enabled = hasSelectedItems;
			Application.Instance.Commands["EditCopy"].Enabled = hasSelectedItems;
			Application.Instance.Commands["EditDelete"].Enabled = hasSelectedItems;

			Application.Instance.Commands["FileProperties"].Enabled = hasSelectedItems;

			Context.Commands["Icarus_ContextMenu_Comment"].Enabled = hasSelectedItems;

			if (tv.SelectedRows.Count == 1)
			{
				Context.Commands["Icarus_ContextMenu_Rename"].Enabled = tv.SelectedRows[0].GetExtraData<IcarusCommand>("cmd").IsMacro;
			}
			else
			{
				Context.Commands["Icarus_ContextMenu_Rename"].Enabled = false;
			}
		}

		[EventHandler(nameof(tv), nameof(Control.BeforeContextMenu))]
		private void tv_BeforeContextMenu(object sender, EventArgs e)
		{
			Context.Commands["Icarus_ContextMenu_Comment"].Title = "Comment Selected Items";

			if (tv.SelectedRows.Count == 1)
			{
				IcarusCommand cmd = tv.SelectedRows[0].GetExtraData<IcarusCommand>("cmd");
				if (cmd != null)
				{
					if (cmd.IsCommented)
					{
						Context.Commands["Icarus_ContextMenu_Comment"].Title = "Uncomment Selected Items";
					}
					else
					{
						Context.Commands["Icarus_ContextMenu_Comment"].Title = "Comment Selected Items";
					}
				}
			}

			tv.ReloadContextMenu();
		}

		private void Icarus_ContextMenu_Comment(object sender, EventArgs e)
		{
			// we loop twice here because of the way BehavEd handles the "comment" feature -
			// if at least one item is commented, it uncomments ALL selected lines; otherwise, it comments all selected lines.

			// TODO: determine if we should follow original BehavEd warning

			bool uncomment = false;
			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				TreeModelRow row = tv.SelectedRows[i];
				IcarusCommand cmd = row.GetExtraData<IcarusCommand>("cmd");
				if (cmd != null)
				{
					if (cmd.IsCommented)
						uncomment = true;
				}
			}
			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				TreeModelRow row = tv.SelectedRows[i];
				IcarusCommand cmd = row.GetExtraData<IcarusCommand>("cmd");
				if (cmd != null)
				{
					BeginEdit();
					cmd.IsCommented = !uncomment;
					if (cmd.Commands.Count > 0)
					{
						if (!(e is MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs && (((e as MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs).ModifierKeys & MBS.Framework.UserInterface.Input.Keyboard.KeyboardModifierKey.Control) == MBS.Framework.UserInterface.Input.Keyboard.KeyboardModifierKey.Control)))
						{
							for (int j = 0; j < cmd.Commands.Count; j++)
							{
								cmd.Commands[j].IsCommented = !uncomment;
								row.Rows[j].RowColumns[0].Value = GetCommandText(cmd.Commands[j]);
							}
						}
					}
					EndEdit();

					row.RowColumns[0].Value = GetCommandText(cmd);
				}
			}
		}

		[EventHandler(nameof(tv), "KeyDown")]
		private void tv_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Back)
			{
				Icarus_ContextMenu_Comment(sender, e);
			}
		}

		private void Icarus_ContextMenu_Rename(object sender, EventArgs e)
		{
			IcarusCommand cmd = tv.SelectedRows[0].GetExtraData<IcarusCommand>("cmd");

			SettingsDialog dlg = new SettingsDialog();
			dlg.EnableProfiles = false;
			dlg.Text = "Rename Macro";
			dlg.SettingsProviders.Clear();
			dlg.SettingsProviders.Add(new CustomSettingsProvider(new SettingsGroup[]
			{
				new SettingsGroup("General", new Setting[]
				{
					new TextSetting("NewName", "New name for the macro", cmd.Name)
				})
			}));

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				cmd.Name = dlg.SettingsProviders[0].SettingsGroups[0].Settings[0].GetValue<string>();
				tv.SelectedRows[0].RowColumns[0].Value = GetCommandText(cmd);
			}
		}

		private void Icarus_ContextMenu_Insert_From_File(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;
			dlg.Text = "Insert Script from File";
			dlg.FileNameFilters.Add("ICARUS scripts", "*.ibi; *.icarus");
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string ext = System.IO.Path.GetExtension(dlg.SelectedFileName).ToLower();
				if (ext.Equals(".icarus") || ext.Equals(".txt"))
				{
					using (System.IO.FileStream fs = System.IO.File.OpenRead(dlg.SelectedFileName))
					{
						using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
						{
							string signature = sr.ReadLine();
							sr.Close();

							if (signature != "//Generated by BehavEd")
							{
								if (MessageDialog.ShowDialog(String.Format("The following file has NOT been written by BehavEd !!!:\r\n\r\n(\"{0}\" )\r\n\r\n... so do you *really* want to read it in?\r\n\r\n(This can mess up big-style if you load a programmer-script with nested statements etc)", dlg.SelectedFileName), "BehavEd", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.No)
								{
									return;
								}
							}
						}
					}
				}

				IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);

				IcarusScriptObjectModel icarus = new IcarusScriptObjectModel();
				IcarusTextDataFormat ictxt = new IcarusTextDataFormat();
				Document.Load(icarus, ictxt, new Accessors.FileAccessor(dlg.SelectedFileName));

				int start = script.Commands.Count;
				script.Commands.Insert(start++, new IcarusCommentCommand("============================="));
				script.Commands.Insert(start++, new IcarusCommentCommand(String.Format("   Appended File ({0}) follows...  ", dlg.SelectedFileName)));
				script.Commands.Insert(start++, new IcarusCommentCommand("============================="));
				for (int i = 0; i < icarus.Commands.Count; i++)
				{
					script.Commands.Insert(start, icarus.Commands[i]);
					start++;
				}

				OnObjectModelChanged(EventArgs.Empty);
			}
		}

		private System.Threading.Thread tDebugger = null;
		private void mnuDebugStart_Click(object sender, EventArgs e)
		{
			// Commands["Icarus_Debug_StartDebugging"].Visible = false;
			// Commands["Icarus_Debug_BreakExecution"].Visible = true;
			// Commands["Icarus_Debug_StopDebugging"].Visible = true;

			IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);
			tv.SelectedRows.Clear();
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
			// (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStart"].Visible = true;
			// (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugBreak"].Visible = false;
			// (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStop"].Visible = false;

			if (tDebugger != null)
			{
				if (tDebugger.IsAlive) tDebugger.Abort();
				tDebugger = null;
			}

			if (_prevTreeNode != null)
			{
				// _prevTreeNode.BackColor = Color.Empty;
				_prevTreeNode = null;
			}
		}

		private void LogOutputWindow(string text)
		{
			(Application.Instance as IHostApplication).OutputWindow.WriteLine(text);
		}
		private void ClearOutputWindow()
		{
			(Application.Instance as IHostApplication).OutputWindow.Clear();
		}

		private Dictionary<IcarusCommand, TreeModelRow> treeNodesForCommands = new Dictionary<IcarusCommand, TreeModelRow>();

		private void tDebugger_Start()
		{
			ClearOutputWindow();
			LogOutputWindow("=== ICARUS Engine Debugger v1.0 - copyright (c) 2013 Mike Becker's Software ===");

			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();

			IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);
			foreach (IcarusCommand command in script.Commands)
			{
				try
				{
					DebugCommand(command);
				}
				catch (InvalidOperationException ex)
				{
					LogOutputWindow("unknown command (" + (script.Commands.IndexOf(command) + 1).ToString() + " of " + script.Commands.Count.ToString() + "): " + command.GetType().Name);
				}
			}

			if (_prevTreeNode != null)
			{
				ReleaseTreeNode(_prevTreeNode);
				_prevTreeNode = null;
			}

			stopwatch.Stop();
			LogOutputWindow("execution complete, " + stopwatch.Elapsed.ToString() + " elapsed since execution started");

			UpdateMenuItems(true);
		}

		private void UpdateMenuItems(bool enable)
		{
			// (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStart"].Visible = enable;
			// (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugBreak"].Visible = !enable;
			// (MenuBar.Items["mnuDebug"] as ActionMenuItem).Items["mnuDebugStop"].Visible = !enable;
		}

		private TreeModelRow _prevTreeNode = null;
		private void ActivateTreeNode(TreeModelRow tn)
		{
			// tn.EnsureVisible();
			// tn.BackColor = Color.Yellow;
		}
		private void ReleaseTreeNode(TreeModelRow tn)
		{
			// tn.BackColor = Color.Empty;
		}

		private Dictionary<string, IcarusCommand> tasksByName = new Dictionary<string, IcarusCommand>();

		private void DebugCommand(IcarusCommand command)
		{
			/*
			if (_prevTreeNode != null)
			{
				ReleaseTreeNode(_prevTreeNode);
				_prevTreeNode = null;
			}

			TreeModelRow tn = treeNodesForCommands[command];
			ActivateTreeNode(tn);
			_prevTreeNode = tn;

			Action<string> _LogOutputWindow = new Action<string>(LogOutputWindow);
			switch ((IcarusCommandType)command.CommandType)
			{
				case IcarusCommandType.Affect:
				{
					LogOutputWindow("on " + command.Parameters["target"].Value.GetValue<string>() + "\r\n{");
					foreach (IcarusCommand command1 in (command as IcarusContainerCommand).Commands)
					{
						DebugCommand(command1);
					}
					LogOutputWindow("}");
					break;
				}
				case IcarusCommandType.Set:
				{
					LogOutputWindow(String.Format("set {0} = {1}", command.Parameters["objectName"]?.Value, (command.Parameters["value"]?.Value == null ? "(null)" : command.Parameters["value"].Value.ToString()));
					break;
				}
				case IcarusCommandType.Wait:
				{
					break;
				}
			}
			else if (command is IcarusCommandWait)
			{
				IcarusCommandWait cmd = (command as IcarusCommandWait);
				int timeout = (int)cmd.Duration.GetValue<int>();
				System.Threading.Thread.Sleep(timeout);
			}
			else if (command is IcarusCommandPrint)
			{
				IcarusCommandPrint cmd = (command as IcarusCommandPrint);
				string text = cmd.Text.GetValue<string>();
				LogOutputWindow(text);
			}
			else if (command is IcarusCommandTask)
			{
				IcarusCommandTask cmd = (command as IcarusCommandTask);
				if (tasksByName.ContainsKey(cmd.TaskName.GetValue<string>()))
				{
					LogOutputWindow("WARNING: redefining task \"" + cmd.TaskName + "\"");
				}
				tasksByName[cmd.TaskName.GetValue<string>()] = cmd;
			}
			else if (command is IcarusCommandControlFlowDo)
			{
				IcarusCommandControlFlowDo cmd = (command as IcarusCommandControlFlowDo);
				string targetName = cmd.Target.GetValue<string>();
				if (targetName != null)
				{
					if (!tasksByName.ContainsKey(targetName))
					{
						LogOutputWindow("ERROR: task \"" + cmd.Target + "\" not found!");
						return;
					}

					IcarusCommandTask task = tasksByName[targetName];
					foreach (IcarusCommand command1 in task.Commands)
					{
						DebugCommand(command1);
					}
				}
				else
				{
					LogOutputWindow("ERROR: cmd called null target");
				}
			}
			else if (command is IcarusCommandLoop)
			{
				IcarusCommandLoop cmd = (command as IcarusCommandLoop);
				float timeout = (float)cmd.Count.GetValue<float>();
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
			*/
			System.Threading.Thread.Sleep(50);
		}

		private void mnuDebugStepInto_Click(object sender, EventArgs e)
		{
			MessageDialog.ShowDialog("Step Into Icarus Script", "Information", MessageDialogButtons.OK);
		}
		private void mnuDebugStepOver_Click(object sender, EventArgs e)
		{
			MessageDialog.ShowDialog("Step Over Icarus Script", "Information", MessageDialogButtons.OK);
		}

		protected override void OnDocumentClosing(CancelEventArgs e)
		{
			base.OnDocumentClosing(e);

			if (tDebugger != null && tDebugger.IsAlive)
			{
				if (MessageDialog.ShowDialog("Do you want to stop debugging?", "ICARUS Debugger", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.No)
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

			if (!IsCreated) return;

			tv.Model.Rows.Clear();
			treeNodesForCommands.Clear();

			IcarusScriptObjectModel script = (ObjectModel as IcarusScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			foreach (IcarusCommand command in script.Commands)
			{
				RecursiveAddCommand(command);
			}
			EndEdit();
		}

		private void RecursiveAddCommand(IcarusCommand command, TreeModelRow parent = null)
		{
			TreeModelRow tn = new TreeModelRow();
			if (command == null) return;

			tn.RowColumns.Add(new TreeModelRowColumn(tv.Model.Columns[0], GetCommandText(command)));

			foreach (IcarusCommand ic1 in command.Commands)
			{
				RecursiveAddCommand(ic1, tn);
			}
			tn.SetExtraData<IcarusCommand>("cmd", command);
			treeNodesForCommands.Add(command, tn);

			if (parent == null)
			{
				tv.Model.Rows.Add(tn);
			}
			else
			{
				parent.Rows.Add(tn);
			}
		}

		protected override void OnSettingsChanged(EventArgs e)
		{
			base.OnSettingsChanged(e);

			foreach (TreeModelRow row in tv.Model.Rows)
			{
				row.RowColumns[0].Value = GetCommandText(row.GetExtraData<IcarusCommand>("cmd"));
			}
		}

		private string GetCommandText(IcarusCommand command)
		{
			StringBuilder sb = new StringBuilder();
			if (command == null) return sb.ToString();

			if (command.IsCommented)
			{
				sb.Append(new string('/', 13));
				sb.Append("  ");
			}

			sb.Append(command.Name);

			if (((UIApplication)Application.Instance).GetSetting<bool>(KnownSettingsGuids.DisplayIcarusOpcodeInTreeView))
			{
				sb.Append(" (0x");
				sb.Append(command.CommandType.ToString("x").ToUpper().PadLeft(2, '0'));
				sb.Append(')');
			}

			// tn.ImageKey = command.GetType().Name;
			// tn.SelectedImageKey = command.GetType().Name;

			if (!command.IsMacro)
			{
				sb.Append("                ( ");
				for (int i = 0; i < command.Parameters.Count; i++)
				{
					if (command.Parameters[i].Value != null)
					{
						sb.Append(command.Parameters[i].Value);
					}
					else
					{
						sb.Append("null");
					}

					if (i < command.Parameters.Count - 1)
						sb.Append(", ");
				}
				sb.Append(" )");
			}

			if (command.Commands.Count > 0)
			{
				sb.Append("                (" + command.Commands.Count.ToString() + " commands)");
			}
			return sb.ToString();
		}

		[EventHandler(nameof(tv), "RowActivated")]
		private void tv_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			if (e.Row != null)
			{
				IcarusExpressionHelperDialog dlg = new IcarusExpressionHelperDialog();

				IcarusCommand cmd = e.Row.GetExtraData<IcarusCommand>("cmd");
				if (cmd.Commands.Count > 0 && cmd.Parameters.Count == 0)
					return; // nothing to edit, so don't interrupt expanding the container row for a useless dialog

				dlg.Script = (ObjectModel as IcarusScriptObjectModel);
				dlg.Command = cmd;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					tv.SelectedRows[0].RowColumns[0].Value = GetCommandText(tv.SelectedRows[0].GetExtraData<IcarusCommand>("cmd"));
				}
			}
		}

		private void TestExpressionEditor(object sender, EventArgs e)
		{
			IcarusExpressionHelperDialog dlg = new IcarusExpressionHelperDialog();
			dlg.Command = new IcarusCommand("set", (int)IcarusCommandType.Set);
			dlg.Script = (ObjectModel as IcarusScriptObjectModel);

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				tv.SelectedRows[0].RowColumns[0].Value = GetCommandText(tv.SelectedRows[0].GetExtraData<IcarusCommand>("cmd"));
			}
		}

		protected override bool ShowDocumentPropertiesDialogInternal()
		{
			if (tv.Focused && tv.SelectedRows.Count > 0)
			{
				tv_RowActivated(this, new ListViewRowActivatedEventArgs(tv.SelectedRows[0]));
				return true;
			}
			return base.ShowDocumentPropertiesDialogInternal();
		}
	}
}
