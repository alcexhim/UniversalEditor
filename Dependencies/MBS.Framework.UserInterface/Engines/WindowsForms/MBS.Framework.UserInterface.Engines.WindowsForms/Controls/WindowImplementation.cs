using System;
using System.Drawing;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Native;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(Window))]
	public class WindowImplementation : ContainerImplementation, IWindowNativeImplementation
	{
		public WindowImplementation (Engine engine, Window control) : base(engine, control)
		{
		}

		protected override void DestroyInternal()
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Form).Close();
		}

		public bool GetStatusBarVisible()
		{
			if (sb != null)
				return sb.Visible;
			return true;
		}
		public void SetStatusBarVisible(bool value)
		{
			if (sb != null)
				sb.Visible = value;
		}

		public System.Windows.Forms.FormStartPosition WindowStartPositionToFormStartPosition(WindowStartPosition value)
		{
			switch (value)
			{
				case WindowStartPosition.CenterParent: return System.Windows.Forms.FormStartPosition.CenterParent;
				case WindowStartPosition.Center: return System.Windows.Forms.FormStartPosition.CenterScreen;
				case WindowStartPosition.Manual: return System.Windows.Forms.FormStartPosition.Manual;
				case WindowStartPosition.DefaultBounds: return System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
				case WindowStartPosition.Default: return System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			}
			throw new NotSupportedException();
		}

		private System.Windows.Forms.StatusStrip sb = null;

		protected override NativeControl CreateControlInternal (Control control)
		{
			Window window = (control as Window);

			System.Windows.Forms.Form form = new System.Windows.Forms.Form ();

			if (window.Decorated)
			{
				if (true) // window.Resizable)
				{
					form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
				}
				else
				{
					form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
				}
			}
			else
			{
				form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			}
			form.Location = new Point((int)window.Location.X, (int)window.Location.Y);
			form.Size = new System.Drawing.Size((int)window.Size.Width, (int)window.Size.Height);
			form.StartPosition = WindowStartPositionToFormStartPosition(window.StartPosition);
			form.Shown += form_Shown;

			Internal.CommandBars.ToolBarManager tbm = new Internal.CommandBars.ToolBarManager(form, form);

			// System.Windows.Forms.ToolStripContainer tsc = new System.Windows.Forms.ToolStripContainer();
			// tsc.Dock = System.Windows.Forms.DockStyle.Fill;

			System.Windows.Forms.MenuStrip mb = new System.Windows.Forms.MenuStrip();
			// mb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			mb.Stretch = true;
			tbm.AddControl(mb);

			// tsc.TopToolStripPanel.Controls.Add(mb);

			foreach (MenuItem m in window.MenuBar.Items)
			{
				System.Windows.Forms.ToolStripItem tsmi = CreateToolStripItem(m);
				if (tsmi != null)
					mb.Items.Add(tsmi);
			}

			if (window.CommandDisplayMode == CommandDisplayMode.CommandBar || window.CommandDisplayMode == CommandDisplayMode.Both)
			{
				foreach (CommandBar cb in Application.CommandBars)
				{
					Toolbar tbCommandBar = window.LoadCommandBar(cb);
					if (!tbCommandBar.IsCreated)
					{
						Engine.CreateControl(tbCommandBar);
					}
					System.Windows.Forms.ToolStrip ts = ((tbCommandBar.ControlImplementation.Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.ToolStrip);
					ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
					ts.Text = cb.Title;

					// tsc.TopToolStripPanel.Controls.Add(ts);
					tbm.AddControl(ts);
				}
			}

			mb.Text = "Menu Bar";
			mb.Visible = window.MenuBar.Visible && (mb.Items.Count > 0);

			sb = new System.Windows.Forms.StatusStrip();
			sb.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			// tsc.BottomToolStripPanel.Controls.Add(sb);

			sb.Text = "Status Bar";
			// sb.Visible = window.StatusBar.Visible;

			Container container = new Container();
			for (int i = 0; i < window.Controls.Count; i++)
			{
				container.Controls.Add(window.Controls[i]);
			}
			Engine.CreateControl(container);

			WindowsFormsNativeControl ncContainer = (Engine.GetHandleForControl(container) as WindowsFormsNativeControl);
			// WindowsFormsNativeControl ncContainer = (base.CreateControlInternal(control) as WindowsFormsNativeControl);

			ncContainer.Handle.Dock = System.Windows.Forms.DockStyle.Fill;

			// tsc.TopToolStripPanel.Text = "MSOCommandBarTop";
			// tsc.LeftToolStripPanel.Text = "MSOCommandBarLeft";
			// tsc.BottomToolStripPanel.Text = "MSOCommandBarBottom";
			// tsc.RightToolStripPanel.Text = "MSOCommandBarRight";
			// tsc.ContentPanel.Controls.Add(ncContainer.Handle);
			form.Controls.Add(ncContainer.Handle);
			ncContainer.Handle.BringToFront();

			// tsc.Dock = System.Windows.Forms.DockStyle.Fill;
			// form.Controls.Add(tsc);
			form.Controls.Add(sb);
			form.Text = window.Text;
			form.AutoSize = true;
			return new WindowsFormsNativeControl (form);
		}

		private System.Windows.Forms.ToolStripItem CreateToolStripItem(MenuItem m)
		{
			if (m is CommandMenuItem)
			{
				System.Windows.Forms.ToolStripMenuItem mi = new System.Windows.Forms.ToolStripMenuItem();
				CommandMenuItem cmi = (m as CommandMenuItem);
				mi.Tag = cmi;
				mi.Click += Mi_Click;
				mi.Text = cmi.Text.Replace('_', '&');
				try
				{
					mi.ShortcutKeys = ShortcutToShortcutKeys(cmi.Shortcut);
				}
				catch (Exception ex)
				{
					Console.WriteLine("could not set shortcut keys value from uwt {0} to winforms {1}", cmi.Shortcut, ShortcutToShortcutKeys(cmi.Shortcut));
				}
				foreach (MenuItem mi1 in cmi.Items)
				{
					if (mi1 == null)
					{
						Console.WriteLine("uwt: wf: ERROR: MenuItem is null in {0} ({1})", cmi.Text, cmi.Name);
					}
					System.Windows.Forms.ToolStripItem tsiChild = CreateToolStripItem(mi1);
					if (tsiChild != null)
						mi.DropDownItems.Add(tsiChild);
				}
				return mi;
			}
			else if (m is SeparatorMenuItem)
			{
				System.Windows.Forms.ToolStripSeparator mi = new System.Windows.Forms.ToolStripSeparator();
				return mi;
			}
			else if (m == null)
			{
				return null;
			}

			Console.WriteLine("uwt: wf: ERROR: could not create ToolStripMenuItem {0}", m.GetType().FullName);
			return null;
		}

		void Mi_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.ToolStripItem tsi = (sender as System.Windows.Forms.ToolStripItem);
			CommandMenuItem cmi = (tsi.Tag as CommandMenuItem);
			cmi.OnClick(e);
		}


		private System.Windows.Forms.Keys ShortcutToShortcutKeys(Shortcut shortcut)
		{
			System.Windows.Forms.Keys keys = System.Windows.Forms.Keys.None;
			if (shortcut == null)
				return keys;

			if ((shortcut.ModifierKeys & KeyboardModifierKey.Alt) == KeyboardModifierKey.Alt)
				keys |= System.Windows.Forms.Keys.Alt;
			if ((shortcut.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
				keys |= System.Windows.Forms.Keys.Control;
			if ((shortcut.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
				keys |= System.Windows.Forms.Keys.Shift;
			if ((shortcut.ModifierKeys & KeyboardModifierKey.Meta) == KeyboardModifierKey.Meta)
				keys |= System.Windows.Forms.Keys.Alt;
			if ((shortcut.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Super)
				keys |= System.Windows.Forms.Keys.LWin;

			switch (shortcut.Key)
			{
				case KeyboardKey.A: keys |= System.Windows.Forms.Keys.A; break;
				case KeyboardKey.B: keys |= System.Windows.Forms.Keys.B; break;
				case KeyboardKey.C: keys |= System.Windows.Forms.Keys.C; break;
				case KeyboardKey.D: keys |= System.Windows.Forms.Keys.D; break;
				case KeyboardKey.E: keys |= System.Windows.Forms.Keys.E; break;
				case KeyboardKey.F: keys |= System.Windows.Forms.Keys.F; break;
				case KeyboardKey.G: keys |= System.Windows.Forms.Keys.G; break;
				case KeyboardKey.H: keys |= System.Windows.Forms.Keys.H; break;
				case KeyboardKey.I: keys |= System.Windows.Forms.Keys.I; break;
				case KeyboardKey.J: keys |= System.Windows.Forms.Keys.J; break;
				case KeyboardKey.K: keys |= System.Windows.Forms.Keys.K; break;
				case KeyboardKey.L: keys |= System.Windows.Forms.Keys.L; break;
				case KeyboardKey.M: keys |= System.Windows.Forms.Keys.M; break;
				case KeyboardKey.N: keys |= System.Windows.Forms.Keys.N; break;
				case KeyboardKey.O: keys |= System.Windows.Forms.Keys.O; break;
				case KeyboardKey.P: keys |= System.Windows.Forms.Keys.P; break;
				case KeyboardKey.Q: keys |= System.Windows.Forms.Keys.Q; break;
				case KeyboardKey.R: keys |= System.Windows.Forms.Keys.R; break;
				case KeyboardKey.S: keys |= System.Windows.Forms.Keys.S; break;
				case KeyboardKey.T: keys |= System.Windows.Forms.Keys.T; break;
				case KeyboardKey.U: keys |= System.Windows.Forms.Keys.U; break;
				case KeyboardKey.V: keys |= System.Windows.Forms.Keys.V; break;
				case KeyboardKey.W: keys |= System.Windows.Forms.Keys.W; break;
				case KeyboardKey.X: keys |= System.Windows.Forms.Keys.X; break;
				case KeyboardKey.Y: keys |= System.Windows.Forms.Keys.Y; break;
				case KeyboardKey.Z: keys |= System.Windows.Forms.Keys.Z; break;
				case KeyboardKey.D0: keys |= System.Windows.Forms.Keys.D0; break;
				case KeyboardKey.D1: keys |= System.Windows.Forms.Keys.D1; break;
				case KeyboardKey.D2: keys |= System.Windows.Forms.Keys.D2; break;
				case KeyboardKey.D3: keys |= System.Windows.Forms.Keys.D3; break;
				case KeyboardKey.D4: keys |= System.Windows.Forms.Keys.D4; break;
				case KeyboardKey.D5: keys |= System.Windows.Forms.Keys.D5; break;
				case KeyboardKey.D6: keys |= System.Windows.Forms.Keys.D6; break;
				case KeyboardKey.D7: keys |= System.Windows.Forms.Keys.D7; break;
				case KeyboardKey.D8: keys |= System.Windows.Forms.Keys.D8; break;
				case KeyboardKey.D9: keys |= System.Windows.Forms.Keys.D9; break;
				case KeyboardKey.NumPad0: keys |= System.Windows.Forms.Keys.NumPad0; break;
				case KeyboardKey.NumPad1: keys |= System.Windows.Forms.Keys.NumPad1; break;
				case KeyboardKey.NumPad2: keys |= System.Windows.Forms.Keys.NumPad2; break;
				case KeyboardKey.NumPad3: keys |= System.Windows.Forms.Keys.NumPad3; break;
				case KeyboardKey.NumPad4: keys |= System.Windows.Forms.Keys.NumPad4; break;
				case KeyboardKey.NumPad5: keys |= System.Windows.Forms.Keys.NumPad5; break;
				case KeyboardKey.NumPad6: keys |= System.Windows.Forms.Keys.NumPad6; break;
				case KeyboardKey.NumPad7: keys |= System.Windows.Forms.Keys.NumPad7; break;
				case KeyboardKey.NumPad8: keys |= System.Windows.Forms.Keys.NumPad8; break;
				case KeyboardKey.NumPad9: keys |= System.Windows.Forms.Keys.NumPad9; break;
			}
			return keys;
		}

		private void form_Shown(object sender, EventArgs e)
		{
			OnShown(e);
			OnMapped(e);
		}


		protected override void RegisterDragSourceInternal (Control control, DragDropTarget [] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKey)
		{
			Console.Error.WriteLine ("uwt: wf: error: registration of drag source / drop target not implemented yet");
		}

		protected override void RegisterDropTargetInternal (Control control, DragDropTarget [] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
			Console.Error.WriteLine ("uwt: wf: error: registration of drag source / drop target not implemented yet");
		}

		protected override void SetControlVisibilityInternal (bool visible)
		{
			if (visible)
			{
				(Handle as WindowsFormsNativeControl).Handle.Show();
			}
			else
			{
				// this doesn't work on linux, but it is how we're supposed to do on WinForms
				if ((Handle as WindowsFormsNativeControl).Handle.InvokeRequired)
				{

					(Handle as WindowsFormsNativeControl).Handle.Invoke(new Action<System.Windows.Forms.Form>(delegate (System.Windows.Forms.Form parm)
					{
						parm.Hide();
					}), new object[] { (Handle as WindowsFormsNativeControl).Handle });
				}
				else
				{
					(Handle as WindowsFormsNativeControl).Handle.Hide();
				}
			}
		}

		protected override void SetFocusInternal ()
		{
			(Handle as WindowsFormsNativeControl).Handle.Focus ();
		}

		protected override Dimension2D GetControlSizeInternal()
		{
			return WindowsFormsEngine.SystemDrawingSizeToDimension2D((Handle as WindowsFormsNativeControl).Handle.Size);
		}

		protected override string GetTooltipTextInternal()
		{
			throw new NotSupportedException();
		}
		protected override void SetTooltipTextInternal(string value)
		{
			throw new NotSupportedException();
		}

		protected override void SetCursorInternal(Cursor value)
		{
			throw new NotImplementedException();
		}
		protected override Cursor GetCursorInternal()
		{
			throw new NotImplementedException();
		}

		public Window[] GetToplevelWindows()
		{
			throw new NotImplementedException();
		}

		public void SetIconName(string value)
		{
			throw new NotImplementedException();
		}

		public string GetIconName()
		{
			throw new NotImplementedException();
		}

		public void InsertMenuItem(int index, MenuItem item)
		{
			throw new NotImplementedException();
		}

		public void ClearMenuItems()
		{
			throw new NotImplementedException();
		}

		public void RemoveMenuItem(MenuItem item)
		{
			throw new NotImplementedException();
		}
	}
}
