using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface
{
	// Realize, Map, and Show, oh my...
	// From Havoc Pennington / Jim Nelson. Clear as mud.
	/*
	* Realize means to create the GDK resources for a widget. i.e. to instantiate the widget on the
	* display. This is more useful once we have multiple display support in GTK.
	*  
	* Map means to actually pop the widget’s window onscreen. It requires the widget to be realized,
	* since the window is created in realize.
	*  
	* Show means the widget should be mapped when its toplevel is mapped, or in the case of a toplevel,
	* should be mapped immediately.
	*  
	* Mapping is asynchronous though; that is, gtk_widget_map() and the map signal are emitted when the
	* map is requested. When the map actually occurs you get a map_event (distinct from plain map). But
	* you are not allowed to draw on widgets until you get the first expose, map_event is insufficient.
	*/

	public abstract class Control : IDisposable, ISupportsExtraData
	{
		public class ControlCollection
			: System.Collections.ObjectModel.Collection<Control>
		{
			private Container _parent = null;
			public ControlCollection(Container parent = null)
			{
				_parent = parent;
			}

			protected override void ClearItems()
			{
				foreach (Control ctl in this)
				{
					ctl.Parent = null;
				}
				base.ClearItems();
				// if (_parent != null) Application.Engine.UpdateControlCollection(_parent);
			}
			protected override void InsertItem(int index, Control item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
				// if (_parent != null) Application.Engine.UpdateControlCollection(_parent);
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
				// if (_parent != null) Application.Engine.UpdateControlCollection(_parent);
			}
			protected override void SetItem(int index, Control item)
			{
				this[index].Parent = null;
				base.SetItem(index, item);
				item.Parent = _parent;
				// if (_parent != null) Application.Engine.UpdateControlCollection(_parent);
			}

			public void Add(Control item, Constraints constraints)
			{
				Add(item);
				if (constraints != null)
				{
					_parent.Layout?.SetControlConstraints(item, constraints);
				}
			}
		}

		public string Name { get; set; } = String.Empty;

		public Rectangle ClientRectangle
		{
			get
			{
				if (this is Window)
				{
					return (this as Window).Bounds;
				}
				else if (Parent == null)
				{
					return Rectangle.Empty;
				}
				return Parent.Layout.GetControlBounds(this);
			}
		}
		

		private Vector2D mvarLocation = new Vector2D(0, 0);
		public Vector2D Location { get { return mvarLocation; } set { mvarLocation = value; } }

		/// <summary>
		/// Translates the given <see cref="Vector2D" /> from client coordinates into screen coordinates.
		/// </summary>
		/// <returns>The to screen coordinates.</returns>
		/// <param name="point">Point.</param>
		public Vector2D ClientToScreenCoordinates(Vector2D point)
		{
			return Application.Engine.ClientToScreenCoordinates(this, point);
		}

		private Dimension2D mvarSize = new Dimension2D(0, 0);
		public Dimension2D Size
		{
			get
			{
				if (IsCreated)
				{
					mvarSize = ControlImplementation.GetControlSize();
				}
				return mvarSize;
			}
			set
			{
				mvarSize = value;
			}
		}

		public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Default;
		public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Default;

		private ControlImplementation mvarControlImplementation = null;
		public ControlImplementation ControlImplementation
		{
			get
			{
				/*
				if (!IsCreated && !Application.Engine.IsControlCreating (this)) {
					Application.Engine.CreateControl (this);
				}
				*/
				return mvarControlImplementation;
			}
			internal set { mvarControlImplementation = value; }
		}

		public bool IsCreated { get { return Application.Engine.IsControlCreated(this); } }

		private Padding mvarMargin = new Padding();
		public Padding Margin { get { return mvarMargin; } set { mvarMargin = value; } }

		private Padding mvarPadding = new Padding();
		public Padding Padding { get { return mvarPadding; } set { mvarPadding = value; } }

		private Brush mvarBackgroundBrush = new SolidBrush(Colors.White);
		public Brush BackgroundBrush { get; set; }

		private string mvarClassName = null;
		public string ClassName { get { return mvarClassName; } set { mvarClassName = value; } }

		private bool mvarEnabled = true;
		public bool Enabled
		{
			get
			{
				if (Application.Engine == null) return mvarEnabled;
				if (!Application.Engine.IsControlCreated(this)) return mvarEnabled;
				return Application.Engine.IsControlEnabled(this);
			}
			set
			{
				if (Application.Engine != null && Application.Engine.IsControlCreated(this))
				{
					Application.Engine.SetControlEnabled(this, value);
				}
				mvarEnabled = value;
			}
		}

		private Font mvarFont = null;
		public Font Font { get { return mvarFont; } set { mvarFont = value; } }


		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <value>The attributes.</value>
		public System.Collections.Generic.Dictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

		private Container mvarParent = null;
		public Container Parent {
			get { return mvarParent; }
			internal set {
				mvarParent = value;
				Application.Engine.UpdateControlLayout (this);
			}
		}

		internal void SetParent(Container parent)
		{
			mvarParent = parent;
		}

		public bool FocusOnClick { get; set; } = true;

		private Cursor mvarCursor = Cursors.Default;
		public Cursor Cursor
		{
			get
			{
				if (!IsCreated) return mvarCursor;
				if (ControlImplementation != null)
					mvarCursor = ControlImplementation.GetCursor();
				return mvarCursor;
			}
			set
			{
				if (mvarCursor == value)
				{
					// do not set cursor if we don't really have to, save some CPU
					return;
				}
				mvarCursor = value;
				ControlImplementation?.SetCursor(value);
			}
		}

		private string mvarTooltipText = String.Empty;
		public string TooltipText
		{
			get
			{
				if (!IsCreated) return mvarTooltipText;
				if (ControlImplementation != null)
					mvarTooltipText = ControlImplementation.GetTooltipText();

				return mvarTooltipText;
			}
			set
			{
				mvarTooltipText = value;

				if (IsCreated)
					ControlImplementation?.SetTooltipText(value);
			}
		}

		public bool UseMarkup { get; set; } = false;

		private string mvarText = null;
		public string Text
		{
			get
			{
				string text = mvarText;
				if (ControlImplementation != null) {
					text = ControlImplementation.GetControlText (this);
					if (text == null) {
						return mvarText;
					}
				}

				if (text == null)
					text = String.Empty;
				return text;
			}
			set
			{
				mvarText = value;
				ControlImplementation?.SetControlText(this, value);
			}
		}

		private bool mvarVisible = true;
		public bool Visible
		{
			get
			{
				//  if (IsCreated)
					// mvarVisible = ((this.ControlImplementation)?.IsControlVisible()).GetValueOrDefault(mvarVisible);
				return mvarVisible;
			}
			set
			{
				mvarVisible = value;
				if (!IsCreated) Application.Engine.CreateControl (this);
				(this.ControlImplementation)?.SetControlVisibility (value);
			}
		}

		/// <summary>
		/// Shows this <see cref="Window" />.
		/// </summary>
		public void Show()
		{
			Visible = true;
		}
		/// <summary>
		/// Hides this <see cref="Window" />, keeping the native object around for later use.
		/// </summary>
		public void Hide()
		{
			Visible = false;
		}

		/// <summary>
		/// Destroys the handle associated with this <see cref="Control" />.
		/// </summary>
		public void Destroy()
		{
			ControlImplementation?.Destroy();
		}

		public void Focus()
		{
			ControlImplementation?.SetFocus ();
		}


		#region Drag-n-Drop
		public event DragEventHandler DragBegin;
		protected internal virtual void OnDragBegin(DragEventArgs e)
		{
			DragBegin?.Invoke(this, e);
		}
		public event DragEventHandler DragEnter;
		protected internal virtual void OnDragEnter(DragEventArgs e)
		{
			DragEnter?.Invoke(this, e);
		}
		public event DragEventHandler DragDrop;
		protected internal virtual void OnDragDrop(DragEventArgs e)
		{
			DragDrop?.Invoke(this, e);
		}
		public event DragDropDataRequestEventHandler DragDropDataRequest;
		protected internal virtual void OnDragDropDataRequest(DragDropDataRequestEventArgs e)
		{
			DragDropDataRequest?.Invoke(this, e);
		}
		public event EventHandler DragDataDelete;
		protected internal virtual void OnDragDataDelete(EventArgs e)
		{
			DragDataDelete?.Invoke(this, e);
		}
		
		public void RegisterDragSource(DragDrop.DragDropTarget[] targets, DragDropEffect actions, MouseButtons buttons = MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey modifierKeys = KeyboardModifierKey.None)
		{
			ControlImplementation.RegisterDragSource(this, targets, actions, buttons, modifierKeys);
		}
		public void RegisterDropTarget(DragDrop.DragDropTarget[] targets, DragDropEffect actions, MouseButtons buttons = MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey modifierKeys = KeyboardModifierKey.None)
		{
			ControlImplementation.RegisterDropTarget(this, targets, actions, buttons, modifierKeys);
		}


		#endregion
		#region Mouse Events
		public event MouseEventHandler MouseEnter;
		protected internal virtual void OnMouseEnter(MouseEventArgs e)
		{
			MouseEnter?.Invoke(this, e);
		}
		public event MouseEventHandler MouseDown;
		protected internal virtual void OnMouseDown(MouseEventArgs e)
		{
			MouseDown?.Invoke(this, e);
		}
		public event MouseEventHandler MouseMove;
		protected internal virtual void OnMouseMove(MouseEventArgs e)
		{
			MouseMove?.Invoke(this, e);
		}
		public event MouseEventHandler MouseUp;
		protected internal virtual void OnMouseUp(MouseEventArgs e)
		{
			MouseUp?.Invoke(this, e);
		}
		public event MouseEventHandler MouseLeave;
		protected internal virtual void OnMouseLeave(MouseEventArgs e)
		{
			MouseLeave?.Invoke(this, e);
		}

		public event MouseEventHandler MouseDoubleClick;
		protected internal virtual void OnMouseDoubleClick(MouseEventArgs e)
		{
			MouseDoubleClick?.Invoke (this, e);
		}
		#endregion
		#region Keyboard Events
		public event KeyEventHandler KeyDown;
		protected internal virtual void OnKeyDown(KeyEventArgs e)
		{
			KeyDown?.Invoke(this, e);
		}
		public event KeyEventHandler KeyPress;
		protected internal virtual void OnKeyPress(KeyEventArgs e)
		{
			KeyPress?.Invoke(this, e);
		}
		public event KeyEventHandler KeyUp;
		protected internal virtual void OnKeyUp(KeyEventArgs e)
		{
			KeyUp?.Invoke(this, e);
		}
		#endregion
		public event PaintEventHandler Paint;
		protected internal virtual void OnPaint(PaintEventArgs e)
		{
			Paint?.Invoke(this, e);
		}

		public event EventHandler Creating;
		protected internal virtual void OnCreating(EventArgs e)
		{
			Creating?.Invoke(this, e);
		}
		public event EventHandler Created;
		protected internal virtual void OnCreated(EventArgs e)
		{
			Created?.Invoke(this, e);
		}

		public event EventHandler Click;
		protected internal virtual void OnClick(EventArgs e)
		{
			Click?.Invoke(this, e);
		}

		public event EventHandler Realize;
		protected internal virtual void OnRealize(EventArgs e)
		{
			Realize?.Invoke(this, e);
		}
		public event EventHandler Unrealize;
		protected internal virtual void OnUnrealize(EventArgs e)
		{
			Unrealize?.Invoke(this, e);
		}

		public event EventHandler BeforeContextMenu;
		protected internal virtual void OnBeforeContextMenu(EventArgs e)
		{
			BeforeContextMenu?.Invoke(this, e);
		}

		public event EventHandler AfterContextMenu;
		protected internal virtual void OnAfterContextMenu(EventArgs e)
		{
			AfterContextMenu?.Invoke(this, e);
		}

		public event EventHandler Mapping;
		protected internal virtual void OnMapping(EventArgs e)
		{
			Mapping?.Invoke(this, e);
		}
		public event EventHandler Mapped;
		protected internal virtual void OnMapped(EventArgs e)
		{
			Mapped?.Invoke(this, e);
		}

		public event EventHandler Shown;
		protected internal virtual void OnShown(EventArgs e)
		{
			Shown?.Invoke(this, e);
		}

		public event EventHandler GotFocus;
		protected internal virtual void OnGotFocus(EventArgs e)
		{
			GotFocus?.Invoke(this, e);
		}
		public event EventHandler LostFocus;
		protected internal virtual void OnLostFocus(EventArgs e)
		{
			LostFocus?.Invoke(this, e);
		}

		public event ResizingEventHandler Resizing;
		protected internal virtual void OnResizing(ResizingEventArgs e)
		{
			Resizing?.Invoke(this, e);
		}
		public event ResizedEventHandler Resized;
		protected internal virtual void OnResized(ResizedEventArgs e)
		{
			Resized?.Invoke(this, e);
		}

		private bool _ContextMenuCommandIDChanged = false;
		private Menu _ContextMenu = null;
		public Menu ContextMenu
		{
			get
			{
				if (_ContextMenu == null && (_ContextMenuCommandID != null && _ContextMenuCommandIDChanged))
				{
					ReloadContextMenu();
					_ContextMenuCommandIDChanged = false;
				}
				return _ContextMenu;
			}
			set { _ContextMenu = value; }
		}

		private string _ContextMenuCommandID = null;
		public string ContextMenuCommandID
		{
			get { return _ContextMenuCommandID; }
			set
			{
				_ContextMenuCommandIDChanged = (_ContextMenuCommandID != value);
				_ContextMenuCommandID = value;

				if (_ContextMenuCommandIDChanged)
					ReloadContextMenu();
			}
		}

		private void ReloadContextMenu()
		{
			Command cmd = Application.Commands[_ContextMenuCommandID];
			if (cmd == null)
			{
				_ContextMenu = null;
			}
			else
			{
				_ContextMenu = Menu.FromCommand(cmd);
			}
		}

		public Window ParentWindow
		{
			get
			{
				Control ctl = Parent;
				if (ctl == null)
					return null;

				while (ctl.Parent != null)
				{
					ctl = ctl.Parent;
				}
				if (ctl is Window) return (ctl as Window);
				return null;
			}
		}

		public Dimension2D MaximumSize { get; set; } = Dimension2D.Empty;
		public Dimension2D MinimumSize { get; set; } = Dimension2D.Empty;

		public void Invalidate()
		{
			// TODO: actually get dimensions of this Control
			Invalidate(0, 0, 4096, 4096);
		}
		public void Invalidate(int x, int y, int width, int height)
		{
			ControlImplementation?.Invalidate(x, y, width, height);
		}
		public void Refresh()
		{
			// convenience method
			Invalidate();
		}
		
		private bool mvarIsDisposed = false;
		public bool IsDisposed
		{
			get
			{
				if (Application.Engine != null)
					return Application.Engine.IsControlDisposed(this);
				return mvarIsDisposed;
			}
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		protected virtual void DisposeManagedInternal()
		{
		}
		protected virtual void DisposeUnmanagedInternal()
		{
		}
		
		protected void Dispose(bool disposing)
		{
			if (mvarIsDisposed)
				return;
			
			if (disposing) {
				// free any managed objects here
				DisposeManagedInternal();
			}

			// free any unmanaged objects here
			DisposeUnmanagedInternal();
			
			mvarIsDisposed = true;
		}

		public Control()
		{
			mvarStyle = new ControlStyle(this);
		}
		
		~Control()
		{
			Dispose(false);
		}

		private ControlStyle mvarStyle = null;
		public ControlStyle Style { get { return mvarStyle; } }

		public bool Focused { get { return (ControlImplementation?.HasFocus()).GetValueOrDefault(); } }

		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key)) return (T)_ExtraData[key];
			return defaultValue;
		}
		public object GetExtraData(string key, object defaultValue = null)
		{
			return GetExtraData<object>(key, defaultValue);
		}
		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData[key] = value;
		}
		public void SetExtraData(string key, object value)
		{
			SetExtraData<object>(key, value);
		}
	}
}
