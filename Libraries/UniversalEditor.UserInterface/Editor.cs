//
//  Editor.cs - the base class for document editor implementations built on the Universal Widget Toolkit
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

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.UserInterface.Panels;
using System.Linq;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// The base class for document editor implementations built on the Universal Widget Toolkit.
	/// </summary>
	public abstract class Editor : MBS.Framework.UserInterface.Container, IDocumentPropertiesProviderControl
	{
		public class ReadOnlyEditorCollection
			: System.Collections.ObjectModel.ReadOnlyCollection<Editor>
		{
			public ReadOnlyEditorCollection(IList<Editor> list) : base(list)
			{
			}
		}

		public EditorContext Context { get; private set; } = null;

		private EditorDocumentExplorer _EditorDocumentExplorer = null;
		public EditorDocumentExplorer DocumentExplorer
		{
			get
			{
				if (_EditorDocumentExplorer == null)
					_EditorDocumentExplorer = new EditorDocumentExplorer(this);
				return _EditorDocumentExplorer;
			}
		}

		protected virtual void OnSelectionsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
		}

		private void Selections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			Application.Instance.Commands["EditCut"].Enabled = (Selections.Count > 0);
			Application.Instance.Commands["EditCopy"].Enabled = (Selections.Count > 0);
			Application.Instance.Commands["EditDelete"].Enabled = (Selections.Count > 0);

			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
				{
					if (e.NewItems.Count == 1)
					{
						Selection sel = (Selection) e.NewItems[0];
						foreach (PropertyPanelObject obj in PropertiesPanel.Objects)
						{
							if (obj.GetExtraData<object>("content") == sel.Content)
							{
								PropertiesPanel.SelectedObject = obj;
								break;
							}
						}
					}
					break;
				}
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
				{
					PropertiesPanel.SelectedObject = null;
					break;
				}
			}

			OnSelectionsChanged(e);
		}

		public EditorPropertiesPanel PropertiesPanel { get; } = null;
		public Selection.SelectionCollection Selections { get; } = new Selection.SelectionCollection();

		public abstract void UpdateSelections();
		public Selection[] GetSelections()
		{
			UpdateSelections();
			return Selections.ToArray();
		}

		public event EditorDocumentExplorerSelectionChangedEventHandler DocumentExplorerSelectionChanged;
		protected internal virtual void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			DocumentExplorerSelectionChanged?.Invoke(this, e);
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			UserInterfacePlugin[] plugins = UserInterfacePlugin.Get();
			Type typ = typeof(EditorPlugin);
			for (int i = 0; i < plugins.Length; i++)
			{
				if (plugins[i] is EditorPlugin)
				{
					EditorPlugin ep = (plugins[i] as EditorPlugin);
					if (ep.SupportedEditors.Count > 0 && !ep.SupportedEditors.Contains(this.GetType()))
						continue;

					typ.GetProperty("Editor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).SetValue(ep, this, null);
					typ.GetProperty("Document", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).SetValue(ep, (Parent as Pages.EditorPage)?.Document, null);
					typ.GetMethod("OnEditorCreated", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(ep, new object[] { EventArgs.Empty });
				}
			}
		}

		private EditorView _CurrentView = null;
		public EditorView CurrentView
		{
			get { return _CurrentView; }
			set
			{
				EditorViewChangingEventArgs e = new EditorViewChangingEventArgs(_CurrentView, value);
				OnViewChanging(e);
				if (e.Cancel) return;
				_CurrentView = e.NewView;
				OnViewChanged(new EditorViewChangedEventArgs(e.OldView, e.NewView));

				(Parent as Pages.EditorPage).UpdateViewButton(_CurrentView);
			}
		}

		public event EditorViewChangingEventHandler ViewChanging;
		protected virtual void OnViewChanging(EditorViewChangingEventArgs e)
		{
			ViewChanging?.Invoke(this, e);
		}
		public event EditorViewChangedEventHandler ViewChanged;
		protected virtual void OnViewChanged(EditorViewChangedEventArgs e)
		{
			ViewChanged?.Invoke(this, e);
		}

		protected abstract Selection CreateSelectionInternal(object content);
		public Selection CreateSelection(object content)
		{
			// we could "if content is string, then return new TextSelection"
			// really, there should be difference between a "text fragment" (e.g. "s")
			// and a text selection (i.e., what is currently highlighted in a text box editor)
			return CreateSelectionInternal(content);
		}

		/// <summary>
		/// Places the specified <see cref="Selection" /> at the current insertion point.
		/// </summary>
		/// <param name="selection">The <see cref="Selection" /> to place.</param>
		protected virtual void PlaceSelection(Selection selection)
		{
		}

		/// <summary>
		/// Copies the content of all selections to the system clipboard, and then clears the content.
		/// </summary>
		public void Cut()
		{
			StringBuilder sb = new StringBuilder();
			Selection[] sels = GetSelections();
			foreach (Selection sel in sels)
			{
				if (sel.Content != null)
				{
					sb.Append(sel.Content.ToString());
				}
				sel.Content = null;
			}
			Clipboard.Default.SetText(sb.ToString());
		}
		/// <summary>
		/// Copies the content of all selections to the system clipboard.
		/// </summary>
		public void Copy()
		{
			StringBuilder sb = new StringBuilder();
			Selection[] sels = GetSelections();

			// FIXME: NONE OF THIS SHOULD BE IN THE MAIN EDITOR.CS CODE FILE
			// > we need to figure out the proper way to format selections for clipboard use
			// > also, the x-special/nautilus-clipboard is obviously nautilus-only
			// > > WHAT ABOUT THE POOR WINDOWS PEOPLE
			if (sels.Length > 0 && sels[0].Content is UniversalEditor.ObjectModels.FileSystem.IFileSystemObject[])
			{
				sb.AppendLine("x-special/nautilus-clipboard");
				sb.AppendLine("copy");
			}
			foreach (Selection sel in sels)
			{
				if (sel.Content != null)
				{
					UniversalEditor.ObjectModels.FileSystem.IFileSystemObject fso = (sel.Content as UniversalEditor.ObjectModels.FileSystem.IFileSystemObject[])[0];
					if (fso != null)
					{
						if (fso is UniversalEditor.ObjectModels.FileSystem.Folder)
						{
							UniversalEditor.ObjectModels.FileSystem.Folder fldr = (fso as UniversalEditor.ObjectModels.FileSystem.Folder);
							string tempfilename = TemporaryFileManager.CreateTemporaryFolder(fldr.Name, fldr.GetContents());
							Uri uri = new Uri("file://" + tempfilename);
							sb.AppendLine(uri.ToString());
						}
						else if (fso is UniversalEditor.ObjectModels.FileSystem.File)
						{
							UniversalEditor.ObjectModels.FileSystem.File f = (fso as UniversalEditor.ObjectModels.FileSystem.File);
							string tempfilename = TemporaryFileManager.CreateTemporaryFile(f.Name, f.GetData());
							Uri uri = new Uri("file://" + tempfilename);
							sb.AppendLine(uri.ToString());
						}
					}
					else
					{
						sb.Append(sel.Content);
					}
				}
			}
			Clipboard.Default.SetText(sb.ToString());
		}
		/// <summary>
		/// Pastes the content from the system clipboard into a new selection, overwriting any selected content.
		/// </summary>
		public void Paste()
		{
			Selections.Clear();

			string clipboardText = Clipboard.Default.GetText();

			Selection selection = CreateSelection(clipboardText);
			if (selection == null) return;

			Selections.Add(selection);
			PlaceSelection(selection);
		}
		public void Delete()
		{
			Selection[] sels = GetSelections();
			if (sels.Length > 0)
			{
				BeginEdit();
				foreach (Selection sel in sels)
				{
					sel.Delete();
				}
				EndEdit();

				OnObjectModelChanged(EventArgs.Empty);
			}
		}

		#region IEditorImplementation Members
		public string Title { get; set; }

		public Document Document { get; set; } = null;

		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel
		{
			get { return mvarObjectModel; }
			set
			{
				ObjectModelChangingEventArgs omce = new ObjectModelChangingEventArgs(mvarObjectModel, value);

				BeginUpdate();
				OnObjectModelChanging(omce);
				EndUpdate();

				if (omce.Cancel) return;

				mvarObjectModel = omce.NewObjectModel;

				BeginUpdate();
				OnObjectModelChanged(EventArgs.Empty);
				EndUpdate();
			}
		}
		#endregion

		public virtual EditorReference MakeReference()
		{
			return new EditorReference(GetType());
		}

		private bool mvarInhibitUndo = false;
		protected bool InhibitUndo { get { return mvarInhibitUndo; } set { mvarInhibitUndo = value; } }

		// private AwesomeControls.PropertyGrid.PropertyGroup.PropertyGroupCollection mvarPropertyGroups = new AwesomeControls.PropertyGrid.PropertyGroup.PropertyGroupCollection(null);
		// public AwesomeControls.PropertyGrid.PropertyGroup.PropertyGroupCollection PropertyGroups { get { return mvarPropertyGroups; } }

		/// <summary>
		/// The event raised when a toolbox item is selected. Use this to change the current Editor's internal mode without
		/// actually affecting the content of the ObjectModel.
		/// </summary>
		public event ToolboxItemEventHandler ToolboxItemSelected;
		protected virtual void OnToolboxItemSelected(ToolboxItemEventArgs e)
		{
			if (ToolboxItemSelected != null) ToolboxItemSelected(this, e);
		}
		/// <summary>
		/// The event raised when a toolbox item is activated in the Editor. Use this to adjust the content of the ObjectModel
		/// based on which toolbox item was activated.
		/// </summary>
		public event ToolboxItemEventHandler ToolboxItemActivated;
		protected virtual void OnToolboxItemActivated(ToolboxItemEventArgs e)
		{
			if (ToolboxItemActivated != null) ToolboxItemActivated(this, e);
		}
		/// <summary>
		/// The event raised when a toolbox item is dropped onto the Editor. Use this to adjust the content of the ObjectModel
		/// based on which toolbox item was dropped.
		/// </summary>
		public event ToolboxItemEventHandler ToolboxItemDropped;
		protected virtual void OnToolboxItemDropped(ToolboxItemEventArgs e)
		{
			if (ToolboxItemDropped != null) ToolboxItemDropped(this, e);
		}

		/// <summary>
		/// Causes the editor to select the specified toolbox item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>True if the editor accepted the new selection; false otherwise. Update the toolbox user interface accordingly.</returns>
		public bool SelectToolboxItem(ToolboxItem item)
		{
			ToolboxItemEventArgs e = new ToolboxItemEventArgs(item);
			OnToolboxItemSelected(e);
			if (e.Cancel) return false;
			return true;
		}

		/// <summary>
		/// Causes the editor to activate the specified toolbox item.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>True if the editor accepted the new selection; false otherwise. Update the toolbox user interface accordingly.</returns>
		public bool ActivateToolboxItem(ToolboxItem item)
		{
			ToolboxItemEventArgs e = new ToolboxItemEventArgs(item);
			OnToolboxItemActivated(e);
			if (e.Cancel) return false;
			return true;
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			if (e.Data.ContainsData(typeof(ToolboxItem)))
			{
				e.Effect = DragDropEffect.Copy;
			}
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (e.Data.ContainsData(typeof(ToolboxItem)))
			{
				ToolboxItem item = (e.Data.GetData(typeof(ToolboxItem)) as ToolboxItem);
				ToolboxItemEventArgs e1 = new ToolboxItemEventArgs(item);
				OnToolboxItemDropped(e1);
			}
		}

		public event CancelEventHandler DocumentClosing;
		protected virtual void OnDocumentClosing(CancelEventArgs e)
		{
			if (DocumentClosing != null) DocumentClosing(this, e);
		}
		public event EventHandler DocumentClosed;
		protected virtual void OnDocumentClosed(EventArgs e)
		{
			if (DocumentClosed != null) DocumentClosed(this, e);
		}

		private UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel mvarConfiguration = new UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel();
		public UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel Configuration { get { return mvarConfiguration; } }

		public Editor()
		{
			Selections.CollectionChanged += Selections_CollectionChanged;

			PropertiesPanel = new EditorPropertiesPanel(this);

			EditorReference er = MakeReference();
			Context = new EditorContext(er.ID, er.Title, er);

			// mvarLargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			// mvarLargeImageList.ImageSize = new System.Drawing.Size(32, 32);
			// mvarLargeImageList.PopulateSystemIcons();

			string largeImageListPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
			{
				DataPath,
				"Images",
				"ImageList",
				"32x32"
			});
			if (System.IO.Directory.Exists(largeImageListPath))
			{
				string[] fileNames = System.IO.Directory.GetFiles(largeImageListPath);
				foreach (string fileName in fileNames)
				{
					try
					{
						// Image image = Image.FromFile(fileName);
						// mvarLargeImageList.Images.Add(System.IO.Path.GetFileNameWithoutExtension(fileName), image);
					}
					catch (System.OutOfMemoryException)
					{
					}
				}
			}

			// mvarSmallImageList.ColorDepth = ColorDepth.Depth32Bit;
			// mvarSmallImageList.ImageSize = new System.Drawing.Size(16, 16);
			// mvarSmallImageList.PopulateSystemIcons();

			string smallImageListPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
			{
				DataPath,
				"Images",
				"ImageList",
				"16x16"
			});
			if (System.IO.Directory.Exists(smallImageListPath))
			{
				string[] fileNames = System.IO.Directory.GetFiles(smallImageListPath);
				foreach (string fileName in fileNames)
				{
					try
					{
						// Image image = Image.FromFile(fileName);
						// mvarSmallImageList.Images.Add(System.IO.Path.GetFileNameWithoutExtension(fileName), image);
					}
					catch (System.OutOfMemoryException)
					{
					}
				}
			}

			string configurationPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
			{
				DataPath,
				"Configuration"
			});
		}

		#region Implementation

		public event ObjectModelChangingEventHandler ObjectModelChanging;
		protected virtual void OnObjectModelChanging(ObjectModelChangingEventArgs e)
		{
			if (ObjectModelChanging != null) ObjectModelChanging(this, e);
		}
		public event EventHandler ObjectModelChanged;
		protected virtual void OnObjectModelChanged(EventArgs e)
		{
			if (ObjectModelChanged != null) ObjectModelChanged(this, e);
		}

		public event CancelEventHandler ObjectModelSaving;
		protected virtual void OnObjectModelSaving(CancelEventArgs e)
		{
			if (ObjectModelSaving != null) ObjectModelSaving(this, e);
		}

		private struct EDITINFO
		{
			public object item;
			public string propertyName;
			public object oldValue;
			public bool closed;

			public Control EditingControl;

			public EDITINFO(object item, string propertyName, object oldValue, Control editingControl = null)
			{
				this.item = item;
				this.propertyName = propertyName;
				this.oldValue = oldValue;
				this.closed = false;
				this.EditingControl = editingControl;
			}
		}

		private Stack<EDITINFO> undo = new Stack<EDITINFO>();
		/// <summary>
		/// Gets the number of items currently in this <see cref="Editor" />'s undo stack.
		/// </summary>
		/// <value>The number of items currently in this <see cref="Editor" />'s undo stack.</value>
		public int UndoItemCount { get { return undo.Count; } }
		private Stack<EDITINFO> redo = new Stack<EDITINFO>();
		/// <summary>
		/// Gets the number of items currently in this <see cref="Editor" />'s redo stack.
		/// </summary>
		/// <value>The number of items currently in this <see cref="Editor" />'s redo stack.</value>
		public int RedoItemCount { get { return redo.Count; } }

		public event EventHandler DocumentEdited;
		protected virtual void OnDocumentEdited(EventArgs e)
		{
			if (DocumentEdited != null) DocumentEdited(this, e);
		}

		private int mvarEditing = 0;
		private int mvarUpdating = 0;

		protected void BeginUpdate()
		{
			mvarUpdating++;
		}
		protected void EndUpdate()
		{
			if (mvarUpdating == 0) return;
			mvarUpdating--;
		}

		public bool InEdit { get { return mvarEditing > 0; } }
		public void BeginEdit(string PropertyName = null, object Value = null, object ParentObject = null, Control editingControl = null)
		{
			if (mvarUpdating > 0) return;

			if (mvarEditing > 0)
			{
				mvarEditing++;
				return;
			}
			mvarEditing++;

			if (ParentObject == null) ParentObject = ObjectModel;

			// check to see if this property has been edited before
			if (undo.Count > 0)
			{
				EDITINFO oldedit = undo.Pop();
				if ((PropertyName != null && oldedit.propertyName != PropertyName) || oldedit.closed) undo.Push(oldedit);
			}

			// push the new edit
			if (PropertyName != null)
			{
				if (Value == null)
				{
					System.Reflection.PropertyInfo pi = ParentObject.GetType().GetProperty(PropertyName);
					if (pi != null)
					{
						Value = ParentObject.GetType().GetProperty(PropertyName).GetValue(ParentObject, null);
					}
				}
			}

			EDITINFO edit = new EDITINFO(ParentObject, PropertyName, Value, editingControl);
			if (PropertyName == null)
			{
				edit.oldValue = mvarObjectModel.Clone() as ObjectModel;
			}
			undo.Push(edit);

			// clear out all the redos
			redo.Clear();
		}
		public void ContinueEdit()
		{
			if (undo.Count > 0)
			{
				EDITINFO edit = undo.Pop();
				edit.oldValue = mvarObjectModel.Clone() as ObjectModel;
				undo.Push(edit);
			}
		}
		public void EndEdit()
		{
			if (mvarUpdating > 0) return;

			if (mvarEditing == 0) return; // throw new InvalidOperationException();
			if (mvarEditing > 1)
			{
				mvarEditing--;
				return;
			}

			if (undo.Count == 0) return;
			EDITINFO oldedit = undo.Pop();
			oldedit.closed = true;
			undo.Push(oldedit);

			// notify the object model that it's being edited
			OnDocumentEdited(EventArgs.Empty);

			mvarEditing--;

			Application.Instance.Commands["EditUndo"].Enabled = undo.Count > 0;
			Application.Instance.Commands["EditRedo"].Enabled = redo.Count > 0;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Editor"/> is currently processing an undo/redo operation.
		/// </summary>
		/// <value><c>true</c> if processing undo redo; otherwise, <c>false</c>.</value>
		public bool ProcessingUndoRedo { get; private set; } = false;

		/// <summary>
		/// Restores the previous object model in the stack.
		/// </summary>
		public void Undo()
		{
			if (undo.Count == 0) return;

			ProcessingUndoRedo = true;

			EDITINFO edi = undo.Pop();
			EDITINFO newedi = edi;

			if (edi.propertyName != null)
			{
				// get the property that owns this edit
				System.Reflection.PropertyInfo pi = edi.item.GetType().GetProperty(edi.propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

				// get the current value of the property, for a "redo"
				object newValue = pi.GetValue(edi.item, null);
				newedi = new EDITINFO(edi.item, edi.propertyName, newValue, edi.EditingControl);

				// set the current value to the "un-done" value
				pi.SetValue(edi.item, edi.oldValue, null);
			}
			else
			{
				newedi = new EDITINFO(null, null, mvarObjectModel, edi.EditingControl);
				mvarObjectModel = (edi.oldValue as ObjectModel);
			}

			// cause a refresh of the editor
			OnObjectModelChanged(EventArgs.Empty);

			if (edi.EditingControl != null) {
				edi.EditingControl.Focus ();
			}

			// push the previous value into the redo log
			redo.Push(newedi);

			ProcessingUndoRedo = false;

			Application.Instance.Commands["EditUndo"].Enabled = undo.Count > 0;
			Application.Instance.Commands["EditRedo"].Enabled = redo.Count > 0;
		}

		/// <summary>
		/// Restores the previously-undone object model from the stack.
		/// </summary>
		public void Redo()
		{
			// this is EXACTLY like undo, only in reverse ;)
			if (redo.Count == 0) return;

			ProcessingUndoRedo = true;

			EDITINFO edi = redo.Pop();
			EDITINFO newedi = edi;

			if (edi.propertyName != null)
			{
				// get the property that owns this edit
				System.Reflection.PropertyInfo pi = edi.item.GetType().GetProperty(edi.propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

				// get the current value of the property, for a "redo"
				object newValue = pi.GetValue(edi.item, null);
				newedi = new EDITINFO(edi.item, edi.propertyName, newValue, edi.EditingControl);

				// set the current value to the "un-done" value
				pi.SetValue(edi.item, edi.oldValue, null);
			}
			else
			{
				newedi = new EDITINFO(null, null, mvarObjectModel, edi.EditingControl);
				mvarObjectModel = (edi.oldValue as ObjectModel);
			}

			// cause a refresh of the editor
			OnObjectModelChanged(EventArgs.Empty);

			if (edi.EditingControl != null)
				edi.EditingControl.Focus ();

			// push the previous value into the undo log
			undo.Push(newedi);

			ProcessingUndoRedo = false;

			Application.Instance.Commands["EditUndo"].Enabled = undo.Count > 0;
			Application.Instance.Commands["EditRedo"].Enabled = redo.Count > 0;
		}
		#endregion

		private CommandBar.CommandBarCollection mvarToolbars = new CommandBar.CommandBarCollection();
		public CommandBar.CommandBarCollection Toolbars { get { return mvarToolbars; } }

		public bool NotifySaving()
		{
			CancelEventArgs ce = new CancelEventArgs();
			OnObjectModelSaving(ce);
			if (ce.Cancel) return false;
			return true;
		}

		public void NotifyClosing(CancelEventArgs ce)
		{
			OnDocumentClosing(ce);
		}
		public void NotifyClosed(EventArgs e)
		{
			OnDocumentClosed(e);
		}

		public string DataPath { get { return String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { "Editors", this.GetType().FullName }); } }
		public Command.CommandCollection Commands { get; } = new Command.CommandCollection();

		/*
		protected override bool ProcessKeyPreview(ref Message m)
		{
			Keys keys = (Keys)m.WParam;
			OnKeyDown(new KeyEventArgs(keys));

			return base.ProcessKeyPreview(ref m);
		}
		*/

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			// FIXME: this should be sorted by contextID
			foreach (CommandBinding binding in (Application.Instance as UIApplication).CommandBindings)
			{
				if (binding.Match(e))
				{
					if (binding.ContextID == null || Application.Instance.Contexts.Contains(binding.ContextID.Value))
					{
						Application.Instance.ExecuteCommand(binding.CommandID);
						e.Cancel = true;
						break;
					}
				}
			}
		}

		public object /*Image*/ GetThemeImage(string path)
		{
			string fileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
			{
				"Editors",
				this.GetType().FullName,
				path
			});
			return null;
			// return AwesomeControls.Theming.Theme.CurrentTheme.GetImage(fileName);
		}

		public bool Changed { get; private set; } = false;

		/// <summary>
		/// Gets the value of the non-indexed public property named <see cref="Name" /> on this <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="name">The name of the public property whose value should be retrieved.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetProperty<T>(string name, T defaultValue = default(T), object childObject = null)
		{
			if (childObject == null) {
				childObject = ObjectModel;
			}

			if (childObject == null) // ???
				return defaultValue;

			Type t = childObject.GetType ();
			System.Reflection.PropertyInfo pi = t.GetProperty (name);
			try {
				return (T) pi.GetValue(childObject, null);
			}
			catch {
				return defaultValue;
			}
		}
		/// <summary>
		/// Sets the value of the non-indexed public property named <see cref="Name" /> on the specified object and marks the editor as changed.
		/// </summary>
		/// <param name="name">The name of the public property whose value should be set.</param>
		/// <param name="value">The value to set.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void SetProperty<T>(string name, T value, object childObject = null, Control editingControl = null)
		{
			if (childObject == null)
				childObject = ObjectModel;

			if (childObject == null) // ???
				return;

			Type t = childObject.GetType ();
			System.Reflection.PropertyInfo pi = t.GetProperty (name);

			object oldvalue = pi.GetValue (childObject, null);
			BeginEdit (name, oldvalue, childObject, editingControl);

			pi.SetValue(childObject, value, null);
			Changed = true;

			EndEdit ();
		}

		protected virtual SettingsProvider[] GetDocumentPropertiesSettingsProvidersInternal()
		{
			return null;
		}

		protected SettingsProvider[] GetDocumentPropertiesSettingsProviders()
		{
			SettingsProvider[] customs = GetDocumentPropertiesSettingsProvidersInternal();

			List<SettingsProvider> list = null;
			if (customs == null)
			{
				list = new List<SettingsProvider>();
			}
			else
			{
				list = new List<SettingsProvider>(customs);
			}

			if (Document != null)
			{
				if (Document.DataFormat != null)
				{
					DataFormatReference dfr = Document.DataFormat.MakeReference();
					if (dfr.ExportOptions != null)
					{
						// FIXME
						list.Add(dfr.ExportOptions.Clone(dfr.Title));
					}
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// Shows the document properties dialog. This function can be overridden to display a custom document properties dialog, but offers a
		/// built-in implementation based on the UWT <see cref="SettingsDialog" /> which is populated with <see cref="SettingsProvider" />s from a call to
		/// <see cref="GetDocumentPropertiesSettingsProviders" />. It is recommended that subclasses of <see cref="Editor" /> override the
		/// <see cref="GetDocumentPropertiesSettingsProvidersInternal" /> function instead of this one if they do not require a custom dialog layout.
		/// </summary>
		/// <returns><c>true</c>, if document properties dialog was shown (regardless of whether it was accepted or not), <c>false</c> otherwise.</returns>
		protected virtual bool ShowDocumentPropertiesDialogInternal()
		{
			SettingsProvider[] providers = GetDocumentPropertiesSettingsProviders();
			if (providers != null)
			{
				SettingsDialog dialog = new SettingsDialog();
				dialog.Text = "Document Properties";
				dialog.SettingsProviders.Clear();

				for (int i = 0; i < providers.Length; i++)
				{
					providers[i].LoadSettings();
					dialog.SettingsProviders.Add(providers[i]);
				}

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					BeginEdit();
					for (int i = 0; i < providers.Length; i++)
					{
						providers[i].SaveSettings();
					}
					EndEdit();
				}
				return true;
			}
			return false;
		}
		public void ShowDocumentPropertiesDialog()
		{
			if (!ShowDocumentPropertiesDialogInternal())
			{
				MessageDialog.ShowDialog(String.Format("TODO: Implement Document Properties dialog for '{0}'!", GetType().Name), "Not Implemented", MessageDialogButtons.OK, MessageDialogIcon.Error);
			}
		}
		public bool HasDocumentProperties
		{
			get { return GetDocumentPropertiesSettingsProviders().Length > 0; }
		}
	}
}
