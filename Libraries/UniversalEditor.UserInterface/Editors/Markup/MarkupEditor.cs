//
//  MarkupEditor.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Text;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Markup
{
	[ContainerLayout("~/Editors/Markup/MarkupEditor.glade")]
	public class MarkupEditor : Editor
	{
		private ListViewControl tv;
		private DefaultTreeModel tm;
		private TextBox txtValue;
		private SplitContainer scAttributesValue;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(MarkupObjectModel));
			}
			return _er;
		}

		public MarkupEditor()
		{
			DocumentExplorer.BeforeContextMenu += DocumentExplorer_BeforeContextMenu;
		}

		private void DocumentExplorer_BeforeContextMenu(object sender, EditorDocumentExplorerBeforeContextMenuEventArgs e)
		{
			MarkupElement elParent = null;
			if (e.Node != null)
			{
				elParent = e.Node.GetExtraData<MarkupElement>("el");
			}
			e.ContextMenuCommandID = "MarkupEditor_DocumentExplorer_ContextMenu";
		}


		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(e);

			Context.AttachCommandEventHandler("MarkupEditor_DocumentExplorer_ContextMenu_Add_Tag", MarkupEditor_DocumentExplorer_ContextMenu_Add_Tag);
			Context.AttachCommandEventHandler("MarkupEditor_DocumentExplorer_ContextMenu_Add_Preprocessor", MarkupEditor_DocumentExplorer_ContextMenu_Add_Preprocessor);
			Context.AttachCommandEventHandler("MarkupEditor_DocumentExplorer_ContextMenu_Add_Comment", MarkupEditor_DocumentExplorer_ContextMenu_Add_Comment);
		}

		private void MarkupEditor_DocumentExplorer_ContextMenu_Add_Tag(object sender, EventArgs e)
		{
			MarkupObjectModel mom = (ObjectModel as MarkupObjectModel);
			if (mom == null) return;

			MarkupTagElement tag = new MarkupTagElement();
			tag.FullName = "untitled";

			if (DocumentExplorer.SelectedNode != null)
			{
				MarkupElement el = DocumentExplorer.SelectedNode.GetExtraData<MarkupElement>("el");
				RecursiveLoadDocumentExplorer(tag, DocumentExplorer.SelectedNode);
			}
			else
			{
				RecursiveLoadDocumentExplorer(tag, null);
			}
		}
		private void MarkupEditor_DocumentExplorer_ContextMenu_Add_Preprocessor(object sender, EventArgs e)
		{
			MarkupObjectModel mom = (ObjectModel as MarkupObjectModel);
			if (mom == null) return;

			if (DocumentExplorer.SelectedNode != null)
			{
				MarkupElement el = DocumentExplorer.SelectedNode.GetExtraData<MarkupElement>("el");
				if (el is MarkupTagElement)
				{
					MarkupPreprocessorElement tag = new MarkupPreprocessorElement();
					(el as MarkupTagElement).Elements.Add(tag);
					RecursiveLoadDocumentExplorer(tag, DocumentExplorer.SelectedNode);
				}
			}
		}
		private void MarkupEditor_DocumentExplorer_ContextMenu_Add_Comment(object sender, EventArgs e)
		{
			MarkupObjectModel mom = (ObjectModel as MarkupObjectModel);
			if (mom == null) return;

			if (DocumentExplorer.SelectedNode != null)
			{
				MarkupElement el = DocumentExplorer.SelectedNode.GetExtraData<MarkupElement>("el");
				if (el is MarkupTagElement)
				{
					MarkupCommentElement tag = new MarkupCommentElement();
					(el as MarkupTagElement).Elements.Add(tag);
					RecursiveLoadDocumentExplorer(tag, DocumentExplorer.SelectedNode);
				}
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
			if (!IsCreated) return;

			tm.Rows.Clear();

			MarkupObjectModel mom = (ObjectModel as MarkupObjectModel);
			if (mom == null) return;

			for (int i = 0; i < mom.Elements.Count; i++)
			{
				RecursiveLoadDocumentExplorer(mom.Elements[i], null);
			}
		}

		private const int MAX_PREVIEW_LENGTH = 24;

		private void RecursiveLoadDocumentExplorer(MarkupElement el, EditorDocumentExplorerNode parent)
		{
			string title = el.Name;
			if (el is MarkupPreprocessorElement)
			{
				title = String.Format("<?{0} {1}?>", el.Name, el.Value);
			}
			else if (el is MarkupCommentElement)
			{
				title = String.Format("<!-- {0}{1} -->", el.Value.Substring(0, Math.Min(el.Value.Length, MAX_PREVIEW_LENGTH)), (el.Value.Length > MAX_PREVIEW_LENGTH ? "..." : String.Empty));
			}
			else if (el is MarkupTagElement)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				StringBuilder sb = new StringBuilder();
				sb.Append('<');
				sb.Append(el.Name);
				if (tag.Attributes.Count > 0)
				{
					sb.Append(' ');
					for (int i = 0; i < tag.Attributes.Count; i++)
					{
						sb.Append(tag.Attributes[i].Name);
						sb.Append('=');
						sb.Append('"');
						sb.Append(tag.Attributes[i].Value);
						sb.Append('"');
						if (i < tag.Attributes.Count - 1)
						{
							sb.Append(' ');
						}
					}
				}

				if (String.IsNullOrEmpty(el.Value))
				{
					sb.Append(" />");
				}
				else
				{
					sb.Append(">...</");
					sb.Append(el.Name);
					sb.Append('>');
				}
				title = sb.ToString();
			}

			EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(title);
			node.SetExtraData<MarkupElement>("el", el);

			if (el is MarkupContainerElement)
			{
				MarkupContainerElement ct = (el as MarkupContainerElement);
				for (int i = 0; i < ct.Elements.Count; i++)
				{
					RecursiveLoadDocumentExplorer(ct.Elements[i], node);
				}
			}

			if (parent == null)
			{
				DocumentExplorer.Nodes.Add(node);
			}
			else
			{
				parent.Nodes.Add(node);
			}
		}

		protected internal override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);

			tm.Rows.Clear();
			if (e.Node != null)
			{
				MarkupElement el = e.Node.GetExtraData<MarkupElement>("el");
				if (el is MarkupTagElement)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					for (int i = 0; i < tag.Attributes.Count; i++)
					{
						TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
						{
							new TreeModelRowColumn(tm.Columns[0], tag.Attributes[i].Name),
							new TreeModelRowColumn(tm.Columns[1], tag.Attributes[i].Value)
						});
						row.SetExtraData<MarkupAttribute>("att", tag.Attributes[i]);
						tm.Rows.Add(row);
					}
					scAttributesValue.Panel1.Expanded = true;
				}
				else
				{
					scAttributesValue.Panel1.Expanded = false;
				}
				txtValue.Text = el.Value;
			}
		}

		private void RecursiveLoadElement(MarkupElement el, TreeModelRow parent)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], el.Name),
				new TreeModelRowColumn(tm.Columns[1], el.Value)
			});

			if (el is MarkupContainerElement)
			{
				MarkupContainerElement ct = (el as MarkupContainerElement);
				for (int i = 0; i < ct.Elements.Count; i++)
				{
					RecursiveLoadElement(ct.Elements[i], row);
				}
			}

			if (parent == null)
			{
				tm.Rows.Add(row);
			}
			else
			{
				parent.Rows.Add(row);
			}
		}

		private CustomSettingsProvider _spMarkupProvider = null;
		protected override SettingsProvider[] GetDocumentPropertiesSettingsProviders()
		{
			if (_spMarkupProvider == null)
			{
				_spMarkupProvider = new CustomSettingsProvider();
				_spMarkupProvider.SettingsLoaded += _spMarkupProvider_SettingsLoaded;
				_spMarkupProvider.SettingsSaved += _spMarkupProvider_SettingsSaved;
				_spMarkupProvider.SettingsGroups.Add(new SettingsGroup("General", new Setting[]
				{
					new ChoiceSetting("Version", null, new ChoiceSetting.ChoiceSettingValue[]
					{
						new ChoiceSetting.ChoiceSettingValue("1.0", 1.0)
					}),
					new ChoiceSetting("Encoding", null, new ChoiceSetting.ChoiceSettingValue[]
					{
						new ChoiceSetting.ChoiceSettingValue("UTF-8", "utf-8")
					}),
					new BooleanSetting("Standalone", false)
				}));
			}
			return new SettingsProvider[] { _spMarkupProvider };
		}

		void _spMarkupProvider_SettingsLoaded(object sender, EventArgs e)
		{
		}


		void _spMarkupProvider_SettingsSaved(object sender, EventArgs e)
		{
		}

	}
}
