using System;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public abstract class WindowsFormsNativeImplementation : NativeImplementation
	{
		public WindowsFormsNativeImplementation (Engine engine, Control control) : base(engine, control)
		{
		}

		protected override void InvalidateInternal(int x, int y, int width, int height)
		{
			if (Handle is WindowsFormsNativeControl)
				(Handle as WindowsFormsNativeControl).Handle.Invalidate(new System.Drawing.Rectangle(x, y, width, height));
		}

		protected override void DestroyInternal()
		{
			if (Control is Dialog)
			{
				System.Windows.Forms.Form handle = ((Handle as WindowsFormsNativeControl).GetNamedHandle("dialog") as System.Windows.Forms.Form);
				handle.Close();
			}
			else if (Handle is WindowsFormsNativeDialog)
			{
				if ((Handle as WindowsFormsNativeDialog)?.Form != null)
				{
					(Handle as WindowsFormsNativeDialog)?.Form.Close();
				}
				else if ((Handle as WindowsFormsNativeDialog)?.Handle != null)
				{
					(Handle as WindowsFormsNativeDialog)?.Handle.Dispose();
				}
			}
			else
			{
				if ((Handle as WindowsFormsNativeControl).Handle is System.Windows.Forms.Form)
				{
					((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Form).Close();
				}
				else
				{
					(Handle as WindowsFormsNativeControl).Handle.Dispose();
				}
			}
		}

		protected override bool SupportsEngineInternal(Type engineType)
		{
			return (engineType == typeof(WindowsFormsEngine));
		}

		protected override bool HasFocusInternal()
		{
			return ((Handle as WindowsFormsNativeControl).Handle).Focused;
		}

		protected override Dimension2D GetControlSizeInternal()
		{
			return new Framework.Drawing.Dimension2D((Handle as WindowsFormsNativeControl).Handle.Size.Width, ((Handle as WindowsFormsNativeControl).Handle.Size.Height));
		}

		protected override Cursor GetCursorInternal()
		{
			throw new NotImplementedException();
		}

		protected override string GetTooltipTextInternal()
		{
			throw new NotImplementedException();
		}

		protected override void RegisterDragSourceInternal(Control control, DragDropTarget[] targets, DragDropEffect actions, Input.Mouse.MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
		}

		protected override void RegisterDropTargetInternal(Control control, DragDropTarget[] targets, DragDropEffect actions, Input.Mouse.MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
		}

		protected override void SetControlVisibilityInternal(bool visible)
		{
			if (Handle is Win32NativeControl)
			{
				Internal.Windows.Methods.ShowWindow((Handle as Win32NativeControl).Handle, visible ? Internal.Windows.Constants.ShowWindowCommand.Show : Internal.Windows.Constants.ShowWindowCommand.Hide);
				return;
			}
			else if (Handle is WindowsFormsNativeControl)
			{
				(Handle as WindowsFormsNativeControl).Handle.Visible = visible;
				return;
			}
			throw new NotSupportedException();
		}
		protected override bool IsControlVisibleInternal()
		{
			if (Handle is Win32NativeControl)
			{
				return Internal.Windows.Methods.IsWindowVisible((Handle as Win32NativeControl).Handle);
			}
			else if (Handle is WindowsFormsNativeControl)
			{
				return (Handle as WindowsFormsNativeControl).Handle.Visible;
			}
			throw new NotSupportedException();
		}

		protected override void SetCursorInternal(Cursor value)
		{
			// TODO: Implement cursors
			// System.Windows.Forms.Cursor.Current = _HCursors[value];
		}

		protected override void SetFocusInternal()
		{
			if (Handle is WindowsFormsNativeControl)
			{
				(Handle as WindowsFormsNativeControl).Handle.Focus();
			}
		}

		protected override void SetTooltipTextInternal(string value)
		{
			throw new NotImplementedException();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			SetupCommonEvents();
		}

		private void SetupCommonEvents()
		{
			System.Windows.Forms.Control ctl = ((Handle as WindowsFormsNativeControl)?.Handle as System.Windows.Forms.Control);
			if (ctl == null)
				return;

			ctl.Text = Control.Text;

			ctl.Click += ctl_Click;
			ctl.MouseDown += ctl_MouseDown;
			ctl.MouseMove += ctl_MouseMove;
			ctl.MouseUp += ctl_MouseUp;
			ctl.MouseEnter += ctl_MouseEnter;
			ctl.MouseLeave += ctl_MouseLeave;
			ctl.KeyUp += ctl_KeyUp;
			ctl.KeyDown += ctl_KeyDown;
		}

		protected override void SetControlTextInternal(Control control, string text)
		{
			if (text == null) text = String.Empty;
			if (Control.IsCreated)
			{
				if ((Handle as WindowsFormsNativeControl).Handle != null)
				{
					(Handle as WindowsFormsNativeControl).Handle.Text = text.Replace('_', '&');
				}
			}
		}

		void ctl_Click(object sender, EventArgs e)
		{
			InvokeMethod(this, "OnClick", new object[] { e });
		}
		void ctl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Input.Mouse.MouseEventArgs ee = new Input.Mouse.MouseEventArgs(e.X, e.Y, WindowsFormsEngine.SWFMouseButtonsToMouseButtons(e.Button), WindowsFormsEngine.SWFKeysToKeyboardModifierKey(System.Windows.Forms.Control.ModifierKeys));
			InvokeMethod(this, "OnMouseDown", new object[] { ee });

			if (ee.Handled)
				return;

			if (ee.Buttons == MouseButtons.Secondary)
			{
				// default implementation - display a context menu if we have one set
				// moved this up here to give us a chance to add a context menu if we don't have one associated yet
				OnBeforeContextMenu(ee);

				if (Control.ContextMenu != null)
				{
					System.Windows.Forms.ContextMenuStrip hMenu = (Engine as WindowsFormsEngine).BuildContextMenuStrip(Control.ContextMenu);

					foreach (MenuItem mi in Control.ContextMenu.Items)
					{
						RecursiveApplyMenuItemVisibility(mi);
					}
					hMenu.Show(System.Windows.Forms.Cursor.Position);

					OnAfterContextMenu(ee);
				}
			}
		}

		private void RecursiveApplyMenuItemVisibility(MenuItem mi)
		{
			if (mi == null)
				return;

			System.Windows.Forms.ToolStripItem hMi = ((Engine as WindowsFormsEngine).GetHandleForMenuItem(mi) as WindowsFormsNativeMenuItem).Handle as System.Windows.Forms.ToolStripItem;
			// hMi.Enabled = mi.Enabled;
			hMi.Visible = mi.Visible;

			if (mi is CommandMenuItem)
			{
				foreach (MenuItem mi1 in (mi as CommandMenuItem).Items)
				{
					RecursiveApplyMenuItemVisibility(mi1);
				}
			}
		}
		void ctl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Input.Mouse.MouseEventArgs ee = new Input.Mouse.MouseEventArgs(e.X, e.Y, WindowsFormsEngine.SWFMouseButtonsToMouseButtons(e.Button), WindowsFormsEngine.SWFKeysToKeyboardModifierKey(System.Windows.Forms.Control.ModifierKeys));
			InvokeMethod(this, "OnMouseMove", new object[] { ee });
		}
		void ctl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Input.Mouse.MouseEventArgs ee = new Input.Mouse.MouseEventArgs(e.X, e.Y, WindowsFormsEngine.SWFMouseButtonsToMouseButtons(e.Button), WindowsFormsEngine.SWFKeysToKeyboardModifierKey(System.Windows.Forms.Control.ModifierKeys));
			InvokeMethod(this, "OnMouseUp", new object[] { ee });
		}
		void ctl_MouseEnter(object sender, EventArgs e)
		{
			Input.Mouse.MouseEventArgs ee = new Input.Mouse.MouseEventArgs(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y, WindowsFormsEngine.SWFMouseButtonsToMouseButtons(System.Windows.Forms.Control.MouseButtons), WindowsFormsEngine.SWFKeysToKeyboardModifierKey(System.Windows.Forms.Control.ModifierKeys));
			InvokeMethod(this, "OnMouseEnter", new object[] { ee });
		}
		void ctl_MouseLeave(object sender, EventArgs e)
		{
			Input.Mouse.MouseEventArgs ee = new Input.Mouse.MouseEventArgs(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y, WindowsFormsEngine.SWFMouseButtonsToMouseButtons(System.Windows.Forms.Control.MouseButtons), WindowsFormsEngine.SWFKeysToKeyboardModifierKey(System.Windows.Forms.Control.ModifierKeys));
			InvokeMethod(this, "OnMouseLeave", new object[] { ee });
		}
		void ctl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Input.Keyboard.KeyEventArgs ee = WindowsFormsEngine.SWFKeyEventArgsToKeyEventArgs(e);
			InvokeMethod(this, "OnKeyDown", new object[] { ee });

			if (ee.Cancel)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}
		void ctl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Input.Keyboard.KeyEventArgs ee = WindowsFormsEngine.SWFKeyEventArgsToKeyEventArgs(e);
			InvokeMethod(this, "OnKeyUp", new object[] { ee });

			if (ee.Cancel)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

	}
}
