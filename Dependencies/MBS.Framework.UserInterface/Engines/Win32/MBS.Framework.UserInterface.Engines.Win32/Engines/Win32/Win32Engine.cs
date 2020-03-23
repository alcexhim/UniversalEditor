using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.Drawing;
using UniversalWidgetToolkit.Engines.Win32.Drawing;
using UniversalWidgetToolkit.ObjectModels.Theming;

namespace UniversalWidgetToolkit.Engines.Win32
{
    public class Win32Engine : Engine
    {
		private Control mvarPressedControl = null;

		protected override int StartInternal(Window waitForClose = null)
		{
			mvarMainWindow = waitForClose;

			Internal.Windows.Structures.Comctl32.INITCOMMONCONTROLSEX lpInitCtrls = new Internal.Windows.Structures.Comctl32.INITCOMMONCONTROLSEX();
			lpInitCtrls.dwSize = Marshal.SizeOf(lpInitCtrls);
			lpInitCtrls.dwICC = Internal.Windows.Constants.Comctl32.InitCommonControlsFlags.All;
			Internal.Windows.Methods.Comctl32.InitCommonControlsEx(ref lpInitCtrls);

			Internal.Windows.Structures.User32.MSG msg;
			int ret = 0;

			while ((ret = Internal.Windows.Methods.User32.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
			{
				Control ctl = GetControlByHandle(msg.hwnd);
				if (!(ctl is Window) && (ctl != null))
				{
					switch (msg.message)
					{
						case Internal.Windows.Constants.User32.WindowMessages.LeftMouseButtonDown:
						{
							SetPressedControl(ctl);
							break;
						}
						case Internal.Windows.Constants.User32.WindowMessages.LeftMouseButtonUp:
						{
							if (ctl == mvarPressedControl)
							{
								ctl.OnClick(EventArgs.Empty);
							}
							SetPressedControl(null);
							break;
						}
						case Internal.Windows.Constants.User32.WindowMessages.SizeChanging:
						{
							ctl.OnResizing(EventArgs.Empty);
							break;
						}
						case Internal.Windows.Constants.User32.WindowMessages.SizeChanged:
						{
							break;
						}
					}
				}

				Internal.Windows.Methods.User32.TranslateMessage(ref msg);
				Internal.Windows.Methods.User32.DispatchMessage(ref msg);

				for (int i = 0; i< windowProcs.Count;i++)
				{
					GC.KeepAlive(windowProcs[i]);
				}
			}

			return msg.wParam.ToInt32();
		}

		private bool quitting = false;
		protected override void StopInternal(int exitCode)
		{
			if (!quitting)
			{
				Internal.Windows.Methods.User32.PostQuitMessage(exitCode);
				quitting = true;
			}
		}

		private Window mvarMainWindow = null;

		protected override void SetControlVisibilityInternal(Control control, bool visible)
		{
			CreateControl(control);
			if (GetProperty<bool>("Windowless"))
			{
				return;
			}
			IntPtr hWnd = handlesByControl[control];
			
		}

		private Dictionary<Type, string> ControlClassNames = new Dictionary<Type, string>();
		public Win32Engine()
		{
			ControlClassNames.Add(typeof(Window), "#32770");
			ControlClassNames.Add(typeof(Button), "Button");
		}

		private List<Internal.Windows.Delegates.WindowProc> windowProcs = new List<Internal.Windows.Delegates.WindowProc>();

		private void EnsureWindowClassRegistered(string className)
		{
			Internal.Windows.Structures.User32.WNDCLASSEX lpwcx = new Internal.Windows.Structures.User32.WNDCLASSEX();
			lpwcx.cbSize = (uint)Marshal.SizeOf(lpwcx);
			lpwcx.lpszClassName = className;

			Internal.Windows.Delegates.WindowProc windowProc = new Internal.Windows.Delegates.WindowProc(_WindowProc);
			lpwcx.lpfnWndProc = windowProc;
			windowProcs.Add(windowProc);

			lpwcx.hCursor = Internal.Windows.Methods.User32.LoadCursor(IntPtr.Zero, Internal.Windows.Constants.User32.Cursors.Arrow);
			lpwcx.hbrBackground = Internal.Windows.Methods.User32.GetSysColorBrush(Internal.Windows.Constants.User32.SystemColors.ThreeDFace);
			IntPtr atom = Internal.Windows.Methods.User32.RegisterClassEx(ref lpwcx);
		}

		private Dictionary<IntPtr, Control> controlsByHandle = new Dictionary<IntPtr, Control>();
		private Dictionary<Control, IntPtr> handlesByControl = new Dictionary<Control, IntPtr>();

		public Control GetControlByHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero) return null;
			if (controlsByHandle.ContainsKey(handle)) return controlsByHandle[handle];
			return null;
		}
		public IntPtr GetHandleByControl(Control ctl)
		{
			if (ctl == null) return IntPtr.Zero;
			if (handlesByControl.ContainsKey(ctl)) return handlesByControl[ctl];
			return IntPtr.Zero;
		}

		protected override void InvalidateControlInternal(Control control)
		{
			if (!GetProperty<bool>("Windowless") || control is Window)
			{
				IntPtr handle = handlesByControl[control];

				Container container = control.Parent;
				Rectangle rect;
				if (container == null && control is Window)
				{
					container = (control as Window);
					rect = (control as Window).Bounds;
				}
				else
				{
					rect = container.Layout.GetControlBounds(control);
				}
				Internal.Windows.Structures.User32.RECT rct = RectangleToRECT(rect);

				Internal.Windows.Methods.User32.InvalidateRect(handle, ref rct, false);
				Internal.Windows.Methods.User32.UpdateWindow(handle);
			}
			else
			{
				IntPtr handle = GetHandleByControl(control.ParentWindow);
				Rectangle rect = control.Parent.Layout.GetControlBounds(control);
				
				Internal.Windows.Structures.User32.RECT rct = RectangleToRECT(rect);

				Internal.Windows.Methods.User32.InvalidateRect(handle, ref rct, false);
				Internal.Windows.Methods.User32.UpdateWindow(handle);
			}
		}

		private Control mvarFocusedControl = null;
		private Control mvarHoverControl = null;

		private int _WindowProc(IntPtr hwnd, Internal.Windows.Constants.User32.WindowMessages uMsg, IntPtr wParam, IntPtr lParam)
		{
			Control ctl = GetControlByHandle(hwnd);
			if (ctl != null)
			{
				switch (uMsg)
				{
					case Internal.Windows.Constants.User32.WindowMessages.LeftMouseButtonDown:
					{
						byte[] xy = BitConverter.GetBytes(lParam.ToInt32());

						int x = BitConverter.ToInt16(xy, 0);
						int y = BitConverter.ToInt16(xy, 2);

						if (GetProperty<bool>("Windowless"))
						{
							if (ctl is Container)
							{
								Container cnt = (ctl as Container);
								Control ctl1 = cnt.HitTest(x, y);
								if (ctl1 != null)
								{
									SetPressedControl(ctl1);
									SetFocusedControl(ctl1);
								}
								else
								{
									SetFocusedControl(null);
								}
							}
						}
						break;
					}
					case Internal.Windows.Constants.User32.WindowMessages.MouseMove:
					{
						byte[] xy = BitConverter.GetBytes(lParam.ToInt32());

						int x = BitConverter.ToInt16(xy, 0);
						int y = BitConverter.ToInt16(xy, 2);

						if (GetProperty<bool>("Windowless"))
						{
							if (ctl is Container)
							{
								Container cnt = (ctl as Container);
								Control ctl1 = cnt.HitTest(x, y);
								SetHoverControl(ctl1);
							}
						}
						break;
					}
					case Internal.Windows.Constants.User32.WindowMessages.LeftMouseButtonUp:
					{
						byte[] xy = BitConverter.GetBytes(lParam.ToInt32());

						int x = BitConverter.ToInt16(xy, 0);
						int y = BitConverter.ToInt16(xy, 2);

						SetPressedControl(null);
						if (GetProperty<bool>("Windowless"))
						{
							if (ctl is Container)
							{
								Container cnt = (ctl as Container);
								Control ctl1 = cnt.HitTest(x, y);
								if (ctl1 != null) ctl1.OnClick(EventArgs.Empty);
							}
						}
						else
						{
							ctl.OnClick(EventArgs.Empty);
						}
						break;
					}
					case Internal.Windows.Constants.User32.WindowMessages.Close:
					{
						CancelEventArgs e = new CancelEventArgs();
						if (ctl is Window)
						{
							(ctl as Window).OnClosing(e);
							if (e.Cancel) return 1;
						}
						break;
					}
					case Internal.Windows.Constants.User32.WindowMessages.DESTROY:
					{
						if (ctl is Window)
						{
							(ctl as Window).OnClosed(EventArgs.Empty);
							if ((ctl as Window) == mvarMainWindow)
							{
								Stop();
							}
						}
						break;
					}
					case Internal.Windows.Constants.User32.WindowMessages.SizeChanging:
					{
						if (ctl is Window)
						{
							RecalculateControlBounds(ctl as Window);
						}
						ctl.OnResizing(EventArgs.Empty);
						ctl.Invalidate();
						break;
					}
					case Internal.Windows.Constants.User32.WindowMessages.Paint:
					{
						Graphics graphics = CreateGraphics(ctl);
						PaintEventArgs e = new PaintEventArgs(graphics);

						if (GetProperty<bool>("Windowless"))
						{
							WLPaintControl(ctl, e.Graphics);
						}
						else
						{
							// draw the control itself
							ctl.OnPaint(e);
						}
						break;
					}
				}
			}
			return Internal.Windows.Methods.User32.DefWindowProc(hwnd, uMsg, wParam, lParam);
		}

		private void WLPaintControl(Control ctl, Graphics graphics)
		{
			// draw the control and any child controls
			if (ctl is Container)
			{
				foreach (Control ctl1 in (ctl as Container).Controls)
				{
					WLPaintControl(ctl1, graphics);
				}
			}
			else
			{
				Guid id = ThemeComponentGuid.FromControlType(ctl.GetType());
				Guid stateId = ThemeComponentStateGuids.Normal;

				if (ctl == mvarFocusedControl)
				{
					if (ctl == mvarPressedControl)
					{
						stateId = ThemeComponentStateGuids.PressedFocused;
					}
					else if (ctl == mvarHoverControl)
					{
						stateId = ThemeComponentStateGuids.HoverFocused;
					}
					else
					{
						stateId = ThemeComponentStateGuids.NormalFocused;
					}
				}
				else if (ctl == mvarPressedControl)
				{
					stateId = ThemeComponentStateGuids.Pressed;
				}
				else if (ctl == mvarHoverControl)
				{
					stateId = ThemeComponentStateGuids.Hover;
				}

				Dictionary<string, object> dict = new Dictionary<string, object>();
				dict.Add("Component.Text", ctl.Text);
				graphics.DrawThemeComponent(new ThemeComponentReference(id, stateId), ctl, dict);

				ctl.OnPaint(new PaintEventArgs(graphics));
			}
		}

		private void SetFocusedControl(Control ctl)
		{
			if (ctl == null)
			{
				Control prevFocusedCtrl = mvarFocusedControl;
				mvarFocusedControl = null;
				if (prevFocusedCtrl != null) prevFocusedCtrl.Invalidate();
			}
			else
			{
				bool changed = (mvarFocusedControl != ctl);
				mvarFocusedControl = ctl;
				if (changed) mvarFocusedControl.Invalidate();
			}
		}
		private void SetHoverControl(Control ctl)
		{
			if (ctl == null)
			{
				Control prevCtrl = mvarHoverControl;
				mvarHoverControl = null;
				if (prevCtrl != null) prevCtrl.Invalidate();
			}
			else
			{
				bool changed = (mvarHoverControl != ctl);
				mvarHoverControl = ctl;
				if (changed) mvarHoverControl.Invalidate();
			}
		}

		private void SetPressedControl(Control ctl)
		{
			if (ctl == null)
			{
				Control prevCtrl = mvarPressedControl;
				mvarPressedControl = null;
				if (prevCtrl != null) prevCtrl.Invalidate();
			}
			else
			{
				bool changed = (mvarPressedControl != ctl);
				mvarPressedControl = ctl;
				if (changed) mvarPressedControl.Invalidate();
			}
		}

		private void RecalculateControlBounds(Window window)
		{
			Internal.Windows.Structures.User32.RECT rect1 = new Internal.Windows.Structures.User32.RECT();
			Internal.Windows.Methods.User32.GetWindowRect(handlesByControl[window], ref rect1);

			window.Bounds = PhysicalBoundsToClientBounds(RECTToRectangle(rect1));
			window.Layout.ResetControlBounds();

			foreach (Control ctl in window.Controls)
			{
				if (GetProperty<bool>("Windowless"))
				{
				}
				else
				{
					IntPtr hWnd = handlesByControl[ctl];
					Rectangle rect = window.Layout.GetControlBounds(ctl);
					Internal.Windows.Methods.User32.SetWindowPos(hWnd, IntPtr.Zero, (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height, 0);
				}
			}
		}

		private Graphics CreateGraphics(Control ctl)
		{
			Graphics graphics = new Win32Graphics(GetHandleByControl(ctl));
			return graphics;
		}

		protected override void CreateControlInternal(Control control)
		{
			if (GetProperty<bool>("Windowless") && !(control is Window)) return;
			if (handlesByControl.ContainsKey(control)) return;

			string className = control.ClassName;
			bool classNameRequiresRegistration = true;
			if (className == null)
			{
				if (ControlClassNames.ContainsKey(control.GetType()))
				{
					className = ControlClassNames[control.GetType()];
					classNameRequiresRegistration = false;
				}
			}
			else if (ControlClassNames.ContainsValue(className))
			{
				classNameRequiresRegistration = false;
			}

			if (classNameRequiresRegistration)
			{
				EnsureWindowClassRegistered(className);
			}


			IntPtr hWndParent = IntPtr.Zero;
			if (control.Parent != null)
			{
				hWndParent = handlesByControl[control.Parent];
			}

			Rectangle bounds = new Rectangle();
			if (control is Window)
			{
				bounds = ClientBoundsToPhysicalBounds((control as Window).Bounds);
			}
			else
			{
				bounds = control.Parent.Layout.GetControlBounds(control);
			}

			IntPtr handle = Internal.Windows.Methods.User32.CreateWindowEx(GetWindowStylesExForControl(control), className, control.Text, GetWindowStylesForControl(control), (int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height, hWndParent, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

			if (handle != IntPtr.Zero)
			{
				Font font = control.Font;
				if (font == null) font = SystemFonts.MenuFont;
				{
					font = Font.FromFamily("Tahoma", 10);
				}

				IntPtr hFont = GetHandleByFont(Internal.Windows.Methods.User32.GetDC(handle), font);
				Internal.Windows.Methods.User32.SendMessage(handle, Internal.Windows.Constants.User32.WindowMessages.SetFont, hFont, new IntPtr(1));

				controlsByHandle[handle] = control;
				handlesByControl[control] = handle;
			}
		}

		private Rectangle ClientBoundsToPhysicalBounds(Rectangle rectangle)
		{
			double x = rectangle.X - (double)(System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2);
			double y = rectangle.Y - (double)(System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2);
			double w = rectangle.Width + (double)(System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2);
			double h = rectangle.Height + (double)((System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2) + System.Windows.Forms.SystemInformation.CaptionHeight);

			return new Rectangle(x, y, w, h);
		}
		private Rectangle PhysicalBoundsToClientBounds(Rectangle rectangle)
		{
			double x = rectangle.X + (double)(System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2);
			double y = rectangle.Y + (double)(System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2);
			double w = rectangle.Width - (double)(System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2);
			double h = rectangle.Height - (double)((System.Windows.Forms.SystemInformation.HorizontalResizeBorderThickness * 2) + System.Windows.Forms.SystemInformation.CaptionHeight);

			return new Rectangle(x, y, w, h);
		}

		private IntPtr GetHandleByFont(IntPtr hdc, Font font)
		{
			Internal.Windows.Structures.GDI.LOGFONT lplf = new Internal.Windows.Structures.GDI.LOGFONT();
			lplf.lfFaceName = font.FamilyName;
			lplf.lfCharSet = Internal.Windows.Constants.GDI.LogFontCharSet.Default;
			lplf.lfItalic = (byte)(font.Italic ? 1 : 0);
			lplf.lfQuality = Internal.Windows.Constants.GDI.LogFontQuality.ClearType;
			int lpy = Internal.Windows.Methods.GDI.GetDeviceCaps(hdc, Internal.Windows.Constants.GDI.DeviceCapsIndex.LogPixelsY);

			// 72 points/inch, lpy pixels/inch

			// thanks https://support.microsoft.com/en-us/kb/74299
			lplf.lfHeight = (int)(Math.Round(-(font.Size * lpy) / (double)72));
			lplf.lfWeight = (int)font.Weight;
			
			IntPtr retval = Internal.Windows.Methods.GDI.CreateFontIndirect(ref lplf);
			return retval;
		}

		private Internal.Windows.Constants.User32.WindowStylesEx GetWindowStylesExForControl(Control control)
		{
			Internal.Windows.Constants.User32.WindowStylesEx styles = Internal.Windows.Constants.User32.WindowStylesEx.None;
			styles |= Internal.Windows.Constants.User32.WindowStylesEx.NoParentNotify;
			return styles;
		}

		private Internal.Windows.Constants.User32.WindowStyles GetWindowStylesForControl(Control control)
		{
			Internal.Windows.Constants.User32.WindowStyles styles = Internal.Windows.Constants.User32.WindowStyles.None;
			if (control is Window)
			{
				styles |= Internal.Windows.Constants.User32.WindowStyles.OverlappedWindow;
			}
			if (control.Visible)
			{
				styles |= Internal.Windows.Constants.User32.WindowStyles.Visible;
			}
			if (control.Parent != null)
			{
				styles |= Internal.Windows.Constants.User32.WindowStyles.Child;
			}
			styles |= Internal.Windows.Constants.User32.WindowStyles.TabStop;
			return styles;
		}

		protected override CommonDialogResult ShowDialogInternal(CommonDialog dialog)
		{
			if (dialog is MessageDialog)
			{
				return ShowMessageDialogInternal(dialog as MessageDialog);
			}
			return CommonDialogResult.None;
		}

		private CommonDialogResult ShowMessageDialogInternal(MessageDialog dialog)
		{
			Internal.Windows.Constants.User32.MessageDialogStyles styles = Win32MessageDialog.GetMessageDialogStyles(dialog);

			string title = dialog.Title;
			if (String.IsNullOrEmpty(dialog.Title)) title = " ";

			IntPtr hWnd = GetHandleByControl(dialog.Parent);
			Internal.Windows.Constants.User32.MessageDialogResponses retval = Internal.Windows.Methods.User32.MessageBox(hWnd, dialog.Content, title, styles);
			return CommonDialogResultFromWin32(retval);
		}

		private CommonDialogResult CommonDialogResultFromWin32(Internal.Windows.Constants.User32.MessageDialogResponses value)
		{
			switch (value)
			{
				case Internal.Windows.Constants.User32.MessageDialogResponses.Abort: return CommonDialogResult.Abort;
				case Internal.Windows.Constants.User32.MessageDialogResponses.Cancel: return CommonDialogResult.Cancel;
				case Internal.Windows.Constants.User32.MessageDialogResponses.Continue: return CommonDialogResult.Continue;
				case Internal.Windows.Constants.User32.MessageDialogResponses.Ignore: return CommonDialogResult.Ignore;
				case Internal.Windows.Constants.User32.MessageDialogResponses.No: return CommonDialogResult.No;
				case Internal.Windows.Constants.User32.MessageDialogResponses.OK: return CommonDialogResult.OK;
				case Internal.Windows.Constants.User32.MessageDialogResponses.Retry: return CommonDialogResult.Retry;
				case Internal.Windows.Constants.User32.MessageDialogResponses.TryAgain: return CommonDialogResult.TryAgain;
				case Internal.Windows.Constants.User32.MessageDialogResponses.Yes: return CommonDialogResult.Yes;
			}
			return CommonDialogResult.None;
		}

		private Monitor GetMonitorInternal(IntPtr hMonitor)
		{
			Internal.Windows.Structures.User32.MONITORINFOEX lpmi = new Internal.Windows.Structures.User32.MONITORINFOEX();
			lpmi.cbSize = Marshal.SizeOf(lpmi) - 4;
			Internal.Windows.Methods.User32.GetMonitorInfo(hMonitor, ref lpmi);

			Monitor monitor = new Monitor
			(
				lpmi.szDevice,
				RECTToRectangle(lpmi.rcMonitor),
				RECTToRectangle(lpmi.rcWork)
			);
			return monitor;
		}

		private Internal.Windows.Structures.User32.RECT RectangleToRECT(Rectangle rect)
		{
			return new Internal.Windows.Structures.User32.RECT((int)rect.X, (int)rect.Y, (int)rect.Right, (int)rect.Bottom);
		}
		private Rectangle RECTToRectangle(Internal.Windows.Structures.User32.RECT rect)
		{
			return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
		}

		private bool GetMonitorsInternalCallback(IntPtr hMonitor, IntPtr hdcMonitor, Internal.Windows.Structures.User32.RECT lprcMonitor, IntPtr dwData)
		{
			Monitor monitor = GetMonitorInternal(hMonitor);
			_monitorList.Add(monitor);
			return true;
		}

		private List<Monitor> _monitorList = null;
		protected override Monitor[] GetMonitorsInternal()
		{
			if (_monitorList != null) throw new InvalidOperationException("Already in use");

			_monitorList = new List<Monitor>();

			Internal.Windows.Structures.User32.RECT rect = new Internal.Windows.Structures.User32.RECT(-32000, -32000, 64000, 64000);
			bool retval = Internal.Windows.Methods.User32.EnumDisplayMonitors(IntPtr.Zero, ref rect, GetMonitorsInternalCallback, IntPtr.Zero);

			Monitor[] items = _monitorList.ToArray();
			_monitorList = null;

			return items;
		}

    }
}
