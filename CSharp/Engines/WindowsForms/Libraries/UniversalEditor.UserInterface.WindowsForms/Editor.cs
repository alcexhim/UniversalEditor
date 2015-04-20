using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList.XML;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public partial class Editor : UserControl, IEditorImplementation
	{
		private static EditorReference _er = null;
		public virtual EditorReference MakeReference()
		{
			return new EditorReference(GetType());

			if (_er == null)
			{
				_er = new EditorReference(GetType());
			}
			return _er;
		}

		private bool mvarInhibitUndo = false;
		protected bool InhibitUndo { get { return mvarInhibitUndo; } set { mvarInhibitUndo = value; } }

		private Toolbox mvarToolbox = new Toolbox();
		protected Toolbox Toolbox { get { return mvarToolbox; } }

		private AwesomeControls.PropertyGrid.PropertyGroup.PropertyGroupCollection mvarPropertyGroups = new AwesomeControls.PropertyGrid.PropertyGroup.PropertyGroupCollection(null);
		public AwesomeControls.PropertyGrid.PropertyGroup.PropertyGroupCollection PropertyGroups { get { return mvarPropertyGroups; } }

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
		/// The event raised when a toolbox item is added to the Editor. Use this to adjust the content of the ObjectModel
		/// based on which toolbox item was added.
		/// </summary>
		public event ToolboxItemEventHandler ToolboxItemAdded;
		protected virtual void OnToolboxItemAdded(ToolboxItemEventArgs e)
		{
			if (ToolboxItemAdded != null) ToolboxItemAdded(this, e);
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
		
		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			if (e.Data.GetDataPresent(typeof(ToolboxItem)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (e.Data.GetDataPresent(typeof(ToolboxItem)))
			{
				ToolboxItem item = (e.Data.GetData(typeof(ToolboxItem)) as ToolboxItem);
				ToolboxItemEventArgs e1 = new ToolboxItemEventArgs(item);
				OnToolboxItemAdded(e1);
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
			InitializeComponent();

			mvarLargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			mvarLargeImageList.ImageSize = new System.Drawing.Size(32, 32);
			mvarLargeImageList.PopulateSystemIcons();

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
						Image image = Image.FromFile(fileName);
						mvarLargeImageList.Images.Add(System.IO.Path.GetFileNameWithoutExtension(fileName), image);
					}
					catch (System.OutOfMemoryException)
					{
					}
				}
			}

			mvarSmallImageList.ColorDepth = ColorDepth.Depth32Bit;
			mvarSmallImageList.ImageSize = new System.Drawing.Size(16, 16);
			mvarSmallImageList.PopulateSystemIcons();

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
						Image image = Image.FromFile(fileName);
						mvarSmallImageList.Images.Add(System.IO.Path.GetFileNameWithoutExtension(fileName), image);
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

			if (System.IO.Directory.Exists(configurationPath))
			{
				string[] fileNames = System.IO.Directory.GetFiles(configurationPath, "*.xml");
				XMLPropertyListDataFormat xmpl = new XMLPropertyListDataFormat();
				
				foreach (string fileName in fileNames)
				{
					try
					{
						PropertyListObjectModel plom = new PropertyListObjectModel();
						Document.Load(plom, xmpl, new FileAccessor(fileName), true);
						plom.CopyTo(mvarConfiguration);
					}
					catch (InvalidDataFormatException ex)
					{
					}
				}
			}
		}

		private ImageList mvarLargeImageList = new ImageList();
		protected ImageList LargeImageList { get { return mvarLargeImageList; } }

		private ImageList mvarSmallImageList = new ImageList();
		protected ImageList SmallImageList { get { return mvarSmallImageList; } }

		#region IEditorImplementation Members
		public virtual string Title { get { return String.Empty; } }

		public virtual void Copy()
		{
		}
		public virtual void Paste()
		{
		}
		/// <summary>
		/// Causes the editor to delete the currently-selected item.
		/// </summary>
		public virtual void Delete()
		{
		}

		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel
		{
			get { return mvarObjectModel; }
			set
			{
				ObjectModelChangingEventArgs omce = new ObjectModelChangingEventArgs(mvarObjectModel, value);
				OnObjectModelChanging(omce);
				if (omce.Cancel) return;

				mvarObjectModel = omce.NewObjectModel;
				OnObjectModelChanged(EventArgs.Empty);
			}
		}
		#endregion
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

			public EDITINFO(object item, string propertyName, object oldValue)
			{
				this.item = item;
				this.propertyName = propertyName;
				this.oldValue = oldValue;
				this.closed = false;
			}
		}

		private Stack<EDITINFO> undo = new Stack<EDITINFO>();
		public int UndoItemCount { get { return undo.Count; } }
		private Stack<EDITINFO> redo = new Stack<EDITINFO>();
		public int RedoItemCount { get { return redo.Count; } }

		public event EventHandler DocumentEdited;
		protected virtual void OnDocumentEdited(EventArgs e)
		{
			if (DocumentEdited != null) DocumentEdited(this, e);
		}

		private int mvarEditing = 0;

		protected void BeginEdit()
		{
			if (mvarEditing > 0)
			{
				mvarEditing++;
				return;
			}
			mvarEditing++;

			// check to see if this property has been edited before
			if (undo.Count > 0)
			{
				EDITINFO oldedit = undo.Pop();
				if (oldedit.closed) undo.Push(oldedit);
			}

			// push the new edit
			EDITINFO edit = new EDITINFO(null, null, mvarObjectModel);
			undo.Push(edit);

			// clear out all the redos
			redo.Clear();
		}
		protected void BeginEdit(string PropertyName, object Value = null, object ParentObject = null)
		{
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
				if (oldedit.propertyName != PropertyName || oldedit.closed) undo.Push(oldedit);
			}

			// push the new edit
			if (Value == null)
			{
				System.Reflection.PropertyInfo pi = ParentObject.GetType().GetProperty(PropertyName);
				if (pi != null)
				{
					Value = ParentObject.GetType().GetProperty(PropertyName).GetValue(ParentObject, null);
				}
			}

			EDITINFO edit = new EDITINFO(ParentObject, PropertyName, Value);
			undo.Push(edit);

			// clear out all the redos
			redo.Clear();
		}
		protected void EndEdit()
		{
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
		}

		public virtual void Undo()
		{
			if (undo.Count == 0) return;

			EDITINFO edi = undo.Pop();
			EDITINFO newedi = edi;

			if (edi.propertyName != null)
			{
				// get the property that owns this edit
				System.Reflection.PropertyInfo pi = edi.item.GetType().GetProperty(edi.propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

				// get the current value of the property, for a "redo"
				object newValue = pi.GetValue(edi.item, null);
				newedi = new EDITINFO(edi.item, edi.propertyName, newValue);

				// set the current value to the "un-done" value
				pi.SetValue(edi.item, edi.oldValue, null);
			}
			else
			{
				newedi = new EDITINFO(null, null, mvarObjectModel);
				mvarObjectModel = (edi.oldValue as ObjectModel);
			}

			// cause a refresh of the editor
			OnObjectModelChanged(EventArgs.Empty);

			// push the previous value into the redo log
			redo.Push(newedi);
		}
		public virtual void Redo()
		{
			// this is EXACTLY like undo, only in reverse ;)
			if (redo.Count == 0) return;

			EDITINFO edi = redo.Pop();
			EDITINFO newedi = edi;

			if (edi.propertyName != null)
			{
				// get the property that owns this edit
				System.Reflection.PropertyInfo pi = edi.item.GetType().GetProperty(edi.propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

				// get the current value of the property, for a "redo"
				object newValue = pi.GetValue(edi.item, null);
				newedi = new EDITINFO(edi.item, edi.propertyName, newValue);

				// set the current value to the "un-done" value
				pi.SetValue(edi.item, edi.oldValue, null);
			}
			else
			{
				newedi = new EDITINFO(null, null, mvarObjectModel);
				mvarObjectModel = (edi.oldValue as ObjectModel);
			}

			// cause a refresh of the editor
			OnObjectModelChanged(EventArgs.Empty);

			// push the previous value into the undo log
			undo.Push(newedi);
		}
		#endregion

		private MenuBar mvarMenuBar = new MenuBar();
		public MenuBar MenuBar { get { return mvarMenuBar; } }

		private Toolbar.ToolbarCollection mvarToolbars = new Toolbar.ToolbarCollection();
		public Toolbar.ToolbarCollection Toolbars { get { return mvarToolbars; } }

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

		private Command.CommandCollection mvarCommands = new Command.CommandCollection();
		public Command.CommandCollection Commands { get { return mvarCommands; } }

		protected override bool ProcessKeyPreview(ref Message m)
		{
			Keys keys = (Keys)m.WParam;
			OnKeyDown(new KeyEventArgs(keys));

			return base.ProcessKeyPreview(ref m);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			// look at this editor's configuration to see if we have any registered keybindings
			foreach (Command cmd in mvarCommands)
			{
				if (cmd.ShortcutKey.CompareTo(e.KeyData))
				{
					cmd.Execute();
				}
			}
		}
	}
}
