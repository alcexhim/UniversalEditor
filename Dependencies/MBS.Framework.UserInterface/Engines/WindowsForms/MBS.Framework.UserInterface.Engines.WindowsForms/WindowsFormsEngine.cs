using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Engines.WindowsForms.Printing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Printing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public class WindowsFormsEngine : Engine
	{
		protected override int Priority => (System.Environment.OSVersion.Platform == PlatformID.Win32NT ? 1 : -1);

		public static System.Windows.Forms.DialogResult DialogResultToSWFDialogResult(DialogResult dialogResult)
		{
			switch (dialogResult)
			{
			case DialogResult.Abort: return System.Windows.Forms.DialogResult.Abort;
			case DialogResult.Cancel: return System.Windows.Forms.DialogResult.Cancel;
			case DialogResult.Ignore: return System.Windows.Forms.DialogResult.Ignore;
			case DialogResult.No: return System.Windows.Forms.DialogResult.No;
			case DialogResult.None: return System.Windows.Forms.DialogResult.None;
			case DialogResult.OK: return System.Windows.Forms.DialogResult.OK;
			case DialogResult.Retry: return System.Windows.Forms.DialogResult.Retry;
			case DialogResult.Yes: return System.Windows.Forms.DialogResult.Yes;
			}
			return System.Windows.Forms.DialogResult.None;
		}
		public static DialogResult SWFDialogResultToDialogResult(System.Windows.Forms.DialogResult dialogResult)
		{
			switch (dialogResult)
			{
			case System.Windows.Forms.DialogResult.Abort: return DialogResult.Abort;
			case System.Windows.Forms.DialogResult.Cancel: return DialogResult.Cancel;
			case System.Windows.Forms.DialogResult.Ignore: return DialogResult.Ignore;
			case System.Windows.Forms.DialogResult.No: return DialogResult.No;
			case System.Windows.Forms.DialogResult.None: return DialogResult.None;
			case System.Windows.Forms.DialogResult.OK: return DialogResult.OK;
			case System.Windows.Forms.DialogResult.Retry: return DialogResult.Retry;
			case System.Windows.Forms.DialogResult.Yes: return DialogResult.Yes;
			}
			return DialogResult.None;
		}

		protected override void UpdateSystemColorsInternal()
		{
			UpdateSystemColor(SystemColor.HighlightBackgroundColor, System.Drawing.SystemColors.Highlight);
			UpdateSystemColor(SystemColor.HighlightForegroundColor, System.Drawing.SystemColors.HighlightText);
			UpdateSystemColor(SystemColor.TextBoxForegroundColor, System.Drawing.SystemColors.WindowText);
		}

		public static System.Drawing.ContentAlignment HorizontalVerticalAlignmentToContentAlignment(HorizontalAlignment ha, VerticalAlignment va)
		{
			if (ha == HorizontalAlignment.Default) ha = HorizontalAlignment.Left;
			if (va == VerticalAlignment.Default) va = VerticalAlignment.Middle;

			if (ha == HorizontalAlignment.Center && va == VerticalAlignment.Bottom) return System.Drawing.ContentAlignment.BottomCenter;
			if (ha == HorizontalAlignment.Center && va == VerticalAlignment.Middle) return System.Drawing.ContentAlignment.MiddleCenter;
			if (ha == HorizontalAlignment.Center && va == VerticalAlignment.Top) return System.Drawing.ContentAlignment.TopCenter;
			if (ha == HorizontalAlignment.Left && va == VerticalAlignment.Bottom) return System.Drawing.ContentAlignment.BottomLeft;
			if (ha == HorizontalAlignment.Left && va == VerticalAlignment.Middle) return System.Drawing.ContentAlignment.MiddleLeft;
			if (ha == HorizontalAlignment.Left && va == VerticalAlignment.Top) return System.Drawing.ContentAlignment.TopLeft;
			if (ha == HorizontalAlignment.Right && va == VerticalAlignment.Bottom) return System.Drawing.ContentAlignment.BottomRight;
			if (ha == HorizontalAlignment.Right && va == VerticalAlignment.Middle) return System.Drawing.ContentAlignment.MiddleRight;
			if (ha == HorizontalAlignment.Right && va == VerticalAlignment.Top) return System.Drawing.ContentAlignment.TopRight;

			throw new NotSupportedException();
		}

		public static System.Windows.Forms.Orientation OrientationToWindowsFormsOrientation(Orientation orientation)
		{
			switch (orientation)
			{
			case Orientation.Horizontal: return System.Windows.Forms.Orientation.Horizontal;
			case Orientation.Vertical: return System.Windows.Forms.Orientation.Vertical;
			}
			throw new NotSupportedException();
		}
		public static Orientation WindowsFormsOrientationToOrientation(System.Windows.Forms.Orientation orientation)
		{
			switch (orientation)
			{
			case System.Windows.Forms.Orientation.Horizontal: return Orientation.Horizontal;
			case System.Windows.Forms.Orientation.Vertical: return Orientation.Vertical;
			}
			throw new NotSupportedException();
		}

		internal static Input.Mouse.MouseButtons SWFMouseButtonsToMouseButtons(System.Windows.Forms.MouseButtons button)
		{
			Input.Mouse.MouseButtons buttons = Input.Mouse.MouseButtons.None;
			if ((button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left) buttons |= Input.Mouse.MouseButtons.Primary;
			if ((button & System.Windows.Forms.MouseButtons.Right) == System.Windows.Forms.MouseButtons.Right) buttons |= Input.Mouse.MouseButtons.Secondary;
			if ((button & System.Windows.Forms.MouseButtons.Middle) == System.Windows.Forms.MouseButtons.Left) buttons |= Input.Mouse.MouseButtons.Wheel;
			if ((button & System.Windows.Forms.MouseButtons.XButton1) == System.Windows.Forms.MouseButtons.Left) buttons |= Input.Mouse.MouseButtons.XButton1;
			if ((button & System.Windows.Forms.MouseButtons.XButton2) == System.Windows.Forms.MouseButtons.Left) buttons |= Input.Mouse.MouseButtons.XButton2;
			return buttons;
		}

		internal ToolStripItem InitMenuItem(MenuItem menuItem, string accelPath = null)
		{
			if (menuItem is CommandMenuItem)
			{
				CommandMenuItem cmi = (menuItem as CommandMenuItem);
				if (accelPath != null)
				{

					string cmiName = cmi.Name;
					if (String.IsNullOrEmpty(cmiName))
					{
						cmiName = cmi.Text;
					}

					// clear out the possible mnemonic definitions
					cmiName = cmiName.Replace("_", String.Empty);

					accelPath += "/" + cmiName;
					if (cmi.Shortcut != null)
					{

					}
				}

				System.Windows.Forms.ToolStripMenuItem hMenuFile = new ToolStripMenuItem();
				hMenuFile.Tag = cmi;
				hMenuFile.Text = cmi.Text.Replace('_', '&');
				hMenuFile.Enabled = cmi.Enabled;
				hMenuFile.Click += HMenuFile_Click;

				if (menuItem.HorizontalAlignment == MenuItemHorizontalAlignment.Right)
				{
					hMenuFile.Alignment = ToolStripItemAlignment.Right;
				}

				if (cmi.Items.Count > 0)
				{
					for (int i = 0; i < cmi.Items.Count; i++)
					{
						hMenuFile.DropDownItems.Add(InitMenuItem(cmi.Items[i]));
					}
				}

				RegisterMenuItemHandle(cmi, new WindowsFormsNativeMenuItem(hMenuFile));
				return hMenuFile;
			}
			else if (menuItem is SeparatorMenuItem)
			{
				System.Windows.Forms.ToolStripSeparator hMenuFile = new ToolStripSeparator();
				RegisterMenuItemHandle(menuItem, new WindowsFormsNativeMenuItem(hMenuFile));
				return hMenuFile;
			}
			return null;
		}

		void HMenuFile_Click(object sender, EventArgs e)
		{
			((sender as ToolStripMenuItem).Tag as CommandMenuItem).OnClick(e);
		}


		public ContextMenuStrip BuildContextMenuStrip(Menu menu, string accelPath = null)
		{
			System.Windows.Forms.ContextMenuStrip hMenuFileMenu = new ContextMenuStrip();
			if (menu.EnableTearoff)
			{
				// TODO: Implement this for Windows Forms
			}

			if (accelPath != null)
			{
			}

			foreach (MenuItem menuItem1 in menu.Items)
			{
				System.Windows.Forms.ToolStripItem hMenuItem = InitMenuItem(menuItem1, accelPath);
				hMenuFileMenu.Items.Add(hMenuItem);
			}
			return hMenuFileMenu;
		}
		public ContextMenuStrip BuildContextMenuStrip(CommandMenuItem cmi, IntPtr hMenuFile, string accelPath = null)
		{
			ContextMenuStrip hMenuFileMenu = new ContextMenuStrip();
			if (cmi.EnableTearoff)
			{
				// TODO: implement this for Windows Forms
			}

			foreach (MenuItem menuItem1 in cmi.Items)
			{
				ToolStripItem hMenuItem = InitMenuItem(menuItem1, accelPath);
				hMenuFileMenu.Items.Add(hMenuItem);
			}
			return hMenuFileMenu;
		}

		public void UpdateSystemColor(SystemColor color, System.Drawing.Color value)
		{
			UpdateSystemColor(color, Color.FromRGBAByte(value.R, value.G, value.B, value.A));
		}

		protected override void UpdateControlLayoutInternal(Control control)
		{
		}
		protected override void UpdateControlPropertiesInternal(Control control, NativeControl handle)
		{
		}
		protected override void UpdateNotificationIconInternal(NotificationIcon nid, bool updateContextMenu)
		{
			throw new NotImplementedException();
		}

		protected override Graphics CreateGraphicsInternal(Image image)
		{
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(((WindowsFormsNativeImage)image).Handle);
			return new WindowsFormsNativeGraphics(g);
		}

		protected override Image CreateImage(int width, int height)
		{
			return new WindowsFormsNativeImage(new System.Drawing.Bitmap(width, height));
		}
		protected override Image LoadImageByName(string name, int size)
		{
			return null;
		}
		protected override Image LoadImage(StockType stockType, int size)
		{
			// TODO: figure out which images to use for stock images on WinForms
			// because WinForms does not have any concept of "stock images"
			return null;
		}
		protected override Image LoadImage(byte[] filedata, string type)
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream(filedata);
			System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
			return new WindowsFormsNativeImage(image);
		}

		internal static Input.Keyboard.KeyEventArgs SWFKeyEventArgsToKeyEventArgs(System.Windows.Forms.KeyEventArgs e)
		{
			return new Input.Keyboard.KeyEventArgs(SWFKeysToKeyboardKey(e.KeyCode), SWFKeysToKeyboardModifierKey(e.KeyCode), e.KeyValue, e.KeyValue);
		}

		protected override Image LoadImage(string filename, string type = null)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
			return new WindowsFormsNativeImage(image);
		}

		protected override bool WindowHasFocusInternal(Window window)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.Win32NT:
				{
					IntPtr hWndActive = Internal.Windows.Methods.GetActiveWindow();
					if (window.ControlImplementation != null)
					{
						if (window.ControlImplementation.Handle is Win32NativeControl)
						{
							return (window.ControlImplementation.Handle as Win32NativeControl).Handle.Equals(hWndActive);
						}
						else if (window.ControlImplementation.Handle is WindowsFormsNativeControl)
						{
							return (window.ControlImplementation.Handle as WindowsFormsNativeControl).Handle.Handle.Equals(hWndActive);
						}
					}
					else
					{
						NativeControl nc = GetHandleForControl(window);
						IntPtr hWnd = IntPtr.Zero;
						if (nc is Win32NativeControl)
						{
							hWnd = (nc as Win32NativeControl).Handle;
						}
						else if (nc is WindowsFormsNativeControl)
						{
							hWnd = (nc as WindowsFormsNativeControl).Handle.Handle;
						}
						return hWnd.Equals(hWndActive);
					}
					break;
				}
			}
			return false;
		}

		protected override DialogResult ShowDialogInternal(Dialog dialog, Window parent)
		{
			Console.WriteLine("dialog is {0}", dialog);
			System.Windows.Forms.IWin32Window parentHandle = null;
			parent = null;
			if (parent == null)
			{
				Console.WriteLine("uwt: wf: warning: ShowDialogInternal called with NULL parent, please fix!");

				if (dialog.Parent != null)
				{
					parentHandle = (GetHandleForControl(dialog.Parent) as WindowsFormsNativeControl)?.Handle;
				}
				else
				{
					Window focusedTopLevelWindow = GetFocusedToplevelWindow();
					Console.WriteLine("focusedTopLevelWindow is {0}", focusedTopLevelWindow);

					parentHandle = new Win32Window((GetHandleForControl(focusedTopLevelWindow) as Win32NativeControl).Handle);
				}
			}
			else
			{
				parentHandle = (GetHandleForControl(parent) as WindowsFormsNativeControl)?.Handle;
			}
			Type[] types = Reflection.GetAvailableTypes(new Type[] { typeof(WindowsFormsDialogImplementation) });
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i].IsAbstract) continue;

				object[] atts = (types[i].GetCustomAttributes(typeof(ControlImplementationAttribute), false));
				if (atts.Length > 0)
				{
					ControlImplementationAttribute cia = (atts[0] as ControlImplementationAttribute);
					if (cia != null)
					{
						// yeah... that's a hack right ---------------------------------->there
						// it can be fixed, but we'd have to figure out the best way to implement CustomDialog vs. CommonDialog without
						// having the GenericDialogImplementation hijack the CommonDialog stuff if it comes up first in the list
						if (dialog.GetType().IsSubclassOf(cia.ControlType) || dialog.GetType() == cia.ControlType || (dialog.GetType().BaseType == typeof(Dialog) && cia.ControlType.BaseType == typeof(Dialog)))
						{
							WindowsFormsDialogImplementation di = (types[i].Assembly.CreateInstance(types[i].FullName, false, System.Reflection.BindingFlags.Default, null, new object[] { this, dialog }, System.Globalization.CultureInfo.CurrentCulture, null) as WindowsFormsDialogImplementation);
							WindowsFormsNativeDialog nc = (di.CreateControl(dialog) as WindowsFormsNativeDialog);
							DialogResult result1 = di.Run(parentHandle);
							return result1;
						}
					}
				}
			}
			return DialogResult.None;

			/*


			if (dialog is FileDialog)
			{
				FileDialog fd = (dialog as FileDialog);
				switch (fd.Mode)
				{
					case FileDialogMode.CreateFolder:
					case FileDialogMode.SelectFolder:
					{
						System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
						System.Windows.Forms.DialogResult result = fbd.ShowDialog();
						return SWFDialogResultToDialogResult(result);
					}
					case FileDialogMode.Open:
					{
						System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
						ofd.Title = fd.Text;
						ofd.FileName = fd.SelectedFileNames[fd.SelectedFileNames.Count - 1];
						System.Windows.Forms.DialogResult result = ofd.ShowDialog();
						foreach (string fn in ofd.FileNames)
						{
							fd.SelectedFileNames.Add(fn);
						}
						return SWFDialogResultToDialogResult(result);
					}
					case FileDialogMode.Save:
					{
						System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
						sfd.Title = fd.Text;
						sfd.FileName = fd.SelectedFileNames[fd.SelectedFileNames.Count - 1];
						System.Windows.Forms.DialogResult result = sfd.ShowDialog();
						foreach (string fn in sfd.FileNames)
						{
							fd.SelectedFileNames.Add(fn);
						}
						return SWFDialogResultToDialogResult(result);
					}
				}
			}
			else
			{
				if (!dialog.IsCreated)
					CreateControl(dialog);

				NativeControl nc = GetHandleForControl(dialog);
				if (nc == null)
				{
					Console.WriteLine("uwt: wf: error: dialog container did not get created");
					return DialogResult.None;
				}

				WindowsFormsNativeControl wfnc = (nc as WindowsFormsNativeControl);
				if (wfnc == null)
				{
					Console.WriteLine("uwt: wf: error: dialog container handle is not a WindowsFormsNativeControl");
					return DialogResult.None;
				}

				System.Windows.Forms.Form pnl = wfnc.Handle as System.Windows.Forms.Form;
				if (pnl == null)
				{
					Console.WriteLine("uwt: wf: error: wfnc.Handle is NULL or not a System.Windows.Forms.Form ({0})", wfnc.Handle?.GetType()?.FullName);
					return DialogResult.None;
				}

				System.Windows.Forms.Panel pnlButtons = new System.Windows.Forms.Panel();
				pnlButtons.Text = "<ButtonBox>";
				pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
				for (int i = 0; i < dialog.Buttons.Count; i++)
				{
					CreateControl(dialog.Buttons[i]);

					WindowsFormsNativeControl ncButton = (GetHandleForControl(dialog.Buttons[i]) as WindowsFormsNativeControl);
					if (ncButton == null)
					{
						Console.WriteLine("uwt: wf: error: button not registered '{0}'", dialog.Buttons[i].Text);
						continue;
					}
					pnlButtons.Controls.Add(ncButton.Handle);
				}
				pnl.Controls.Add(pnlButtons);
				pnl.Font = System.Drawing.SystemFonts.MenuFont;

				System.Windows.Forms.DialogResult result = pnl.ShowDialog();
				return SWFDialogResultToDialogResult(result);
			}
			throw new NotImplementedException();
			*/
		}

		internal static KeyboardKey SWFKeysToKeyboardKey(System.Windows.Forms.Keys keys)
		{
			// we should be able to do this...
			return (KeyboardKey)((int)keys);
		}
		internal static KeyboardModifierKey SWFKeysToKeyboardModifierKey(System.Windows.Forms.Keys keys)
		{
			KeyboardModifierKey modifiers = KeyboardModifierKey.None;
			if ((keys & System.Windows.Forms.Keys.Shift) == System.Windows.Forms.Keys.Shift) modifiers |= KeyboardModifierKey.Shift;
			if ((keys & System.Windows.Forms.Keys.Menu) == System.Windows.Forms.Keys.Menu) modifiers |= KeyboardModifierKey.Alt;
			if ((keys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control) modifiers |= KeyboardModifierKey.Control;
			return modifiers;
		}

		private List<Window> _GetToplevelWindowsRetval = null;
		private Window[] GTK_GetToplevelWindowsInternal()
		{
			if (_GetToplevelWindowsRetval != null)
			{
				// should not happen
				throw new InvalidOperationException();
			}

			_GetToplevelWindowsRetval = new List<Window>();
			IntPtr hList = Internal.Linux.GTK.Methods.gtk_window_list_toplevels();
			Internal.Linux.GLib.Methods.g_list_foreach(hList, GTK_AddToList, IntPtr.Zero);

			Window[] retval = _GetToplevelWindowsRetval.ToArray();
			Internal.Linux.GLib.Methods.g_list_free(hList);

			_GetToplevelWindowsRetval = null;
			return retval;
		}
		private void /*GFunc*/ GTK_AddToList(IntPtr data, IntPtr user_data)
		{
			if (_GetToplevelWindowsRetval == null)
			{
				throw new InvalidOperationException("_AddToList called before initializing the list");
			}

			Control ctl = null; // GetControlByHandle(data);
			Window window = (ctl as Window);

			if (window == null)
			{
				window = new Window();
				RegisterControlHandle(window, new Internal.Linux.GTKNativeControl(data));
			}

			_GetToplevelWindowsRetval.Add(window);
		}

		protected override Window[] GetToplevelWindowsInternal()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.Unix: return GTK_GetToplevelWindowsInternal();
			case PlatformID.Win32NT: return W32_GetToplevelWindowsInternal();
			default: throw new PlatformNotSupportedException();
			}
		}

		private Internal.Windows.Delegates.EnumWindowsProc W32_GetToplevelWindowsCallback;
		public WindowsFormsEngine()
		{
			W32_GetToplevelWindowsCallback = new Internal.Windows.Delegates.EnumWindowsProc(W32_GetToplevelWindowsCallbackFunc);

			RegisterStockType(StockType.OK, "ok", "_OK");
		}

		private bool W32_GetToplevelWindowsCallbackFunc(IntPtr hWnd, IntPtr lParam)
		{
			if (_GetToplevelWindowsRetval != null)
			{
				Window window = new Window();
				RegisterControlHandle(window, new Win32NativeControl(hWnd), true);

				_GetToplevelWindowsRetval.Add(window);
			}
			return true;
		}

		private Window[] W32_GetToplevelWindowsInternal()
		{
			if (_GetToplevelWindowsRetval != null)
				return new Window[0];

			_GetToplevelWindowsRetval = new List<Window>();
			bool success = Internal.Windows.Methods.EnumWindows(W32_GetToplevelWindowsCallback, IntPtr.Zero);
			Window[] list = _GetToplevelWindowsRetval.ToArray();
			_GetToplevelWindowsRetval = null;
			return list;
		}

		protected override int StartInternal(Window waitForClose = null)
		{
			InvokeStaticMethod(typeof(Application), "OnStartup", new object[] { EventArgs.Empty });

			if (waitForClose != null)
			{
				WindowsFormsNativeControl ncWaitForClose = (GetHandleForControl(waitForClose) as WindowsFormsNativeControl);
				System.Windows.Forms.Application.Run(ncWaitForClose.Handle as System.Windows.Forms.Form);
			}
			else
			{

				ApplicationActivatedEventArgs e = new ApplicationActivatedEventArgs();
				e.CommandLine = new WindowsFormsCommandLine(Environment.GetCommandLineArgs());
				InvokeStaticMethod(typeof(Application), "OnActivated", new object[] { e });
				System.Windows.Forms.Application.Run();
			}
			return 0;
		}
		protected override void StopInternal(int exitCode)
		{
			System.Windows.Forms.Application.Exit();
		}

		protected override void ShowNotificationPopupInternal(NotificationPopup popup)
		{
			throw new NotImplementedException();
		}

		protected override void SetControlEnabledInternal(Control control, bool value)
		{
			if (handlesByControl[control] is Win32NativeControl)
			{
				// this is exactly why this should be handled by the ControlImplementation and not the Engine >.>
				// Internal.Windows.Methods.EnableWindow((handlesByControl[control] as Win32NativeControl).Handle, value ? 1 : 0);
			}
			else
			{
				(handlesByControl[control] as WindowsFormsNativeControl).Handle.Enabled = value;
			}
		}

		protected override Vector2D ClientToScreenCoordinatesInternal(Control control, Vector2D point)
		{
			if (!control.IsCreated)
				return point;
			return SystemDrawingPointToVector2D((((control.ControlImplementation as WindowsFormsNativeImplementation).Handle as WindowsFormsNativeControl)?.Handle.PointToScreen(Vector2DToSystemDrawingPoint(point))).GetValueOrDefault());
		}

		public static Vector2D SystemDrawingPointToVector2D(System.Drawing.Point v)
		{
			return new Vector2D(v.X, v.Y);
		}
		public static Vector2D SystemDrawingPointFToVector2D(System.Drawing.PointF v)
		{
			return new Vector2D(v.X, v.Y);
		}
		public static System.Drawing.PointF Vector2DToSystemDrawingPointF(Vector2D point)
		{
			return new System.Drawing.PointF((float)point.X, (float)point.Y);
		}
		public static System.Drawing.Point Vector2DToSystemDrawingPoint(Vector2D point)
		{
			return new System.Drawing.Point((int)point.X, (int)point.Y);
		}

		public static System.Drawing.SizeF Dimension2DToSystemDrawingSizeF(Dimension2D size)
		{
			return new System.Drawing.SizeF((float)size.Width, (float)size.Height);
		}
		public static System.Drawing.Size Dimension2DToSystemDrawingSize(Dimension2D size)
		{
			return new System.Drawing.Size((int)size.Width, (int)size.Height);
		}

		protected override void DoEventsInternal()
		{
			System.Windows.Forms.Application.DoEvents();
		}

		protected override Monitor[] GetMonitorsInternal()
		{
			List<Monitor> list = new List<Monitor>();
			foreach (System.Windows.Forms.Screen scr in System.Windows.Forms.Screen.AllScreens)
			{
				list.Add(new Monitor(scr.DeviceName, SDRectangleToUWTRectangle(scr.Bounds), SDRectangleToUWTRectangle(scr.WorkingArea)/*, scr.Primary*/));
			}
			return list.ToArray();
		}

		private Rectangle SDRectangleToUWTRectangle(System.Drawing.Rectangle rect)
		{
			return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
		}

		protected override void RepaintCustomControl(CustomControl control, int x, int y, int width, int height)
		{
			throw new NotImplementedException();
		}

		protected override bool IsControlDisposedInternal(Control ctl)
		{
			if (!IsControlCreated(ctl)) return true;
			return (GetHandleForControl(ctl) as WindowsFormsNativeControl).Handle.IsDisposed;
		}
		protected override bool IsControlEnabledInternal(Control control)
		{
			return (GetHandleForControl(control) as WindowsFormsNativeControl).Handle.Enabled;
		}

		protected override bool InitializeInternal()
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

			System.Windows.Forms.ToolStripManager.Renderer = new CBRenderer();
			return true;
		}

		protected override Printer[] GetPrintersInternal()
		{
			List<Printer> list = new List<Printer>();
			foreach (string p in PrinterSettings.InstalledPrinters)
			{
				list.Add(new WindowsFormsPrinter(p));
			}
			return list.ToArray();
		}

		public static System.Windows.Forms.ColumnHeaderStyle HeaderStyleToSWFHeaderStyle(ColumnHeaderStyle headerStyle)
		{
			switch (headerStyle)
			{
			case ColumnHeaderStyle.Clickable: return System.Windows.Forms.ColumnHeaderStyle.Clickable;
			case ColumnHeaderStyle.Nonclickable: return System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			case ColumnHeaderStyle.None: return System.Windows.Forms.ColumnHeaderStyle.None;
			}
			throw new NotSupportedException();
		}

		protected override void PrintInternal(PrintJob job)
		{
			PrintDocument doc = new PrintDocument();
			doc.BeginPrint += Doc_BeginPrint;
			doc.PrintPage += Doc_PrintPage;
			doc.Print();
		}

		void Doc_PrintPage(object sender, PrintPageEventArgs e)
		{
		}


		void Doc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
		}

		protected override NativeTreeModel CreateTreeModelInternal(TreeModel model)
		{
			return new WindowsFormsNativeTreeModel();
		}

		private static WindowsFormsClipboard _Clipboard = null;
		protected override Clipboard GetDefaultClipboardInternal()
		{
			if (_Clipboard == null)
			{
				_Clipboard = new WindowsFormsClipboard();
			}
			return _Clipboard;
		}

		internal static Dimension2D SystemDrawingSizeToDimension2D(System.Drawing.Size size)
		{
			return new Dimension2D(size.Width, size.Height);
		}

		protected override void SetMenuItemVisibilityInternal(MenuItem item, bool visible)
		{
			throw new NotImplementedException();
		}

		protected override bool ShowHelpInternal(HelpTopic topic)
		{
			return false;
		}

	}
}
