//
//  ControlImplementation.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.Drawing;
using System.Text;

namespace MBS.Framework.UserInterface
{
	public abstract class ControlImplementation
	{
		protected static void InvokeMethod(object obj, string meth, params object[] parms)
		{
			if (obj == null)
			{
				Console.WriteLine("NativeImplementation::InvokeMethod: obj is null");
				return;
			}

			Type t = obj.GetType();
			System.Reflection.MethodInfo mi = t.GetMethod(meth, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			if (mi != null)
			{
				mi.Invoke(obj, parms);
			}
			else
			{
				StringBuilder methSig = new StringBuilder();
				for (int i = 0; i < parms.Length; i++)
				{
					if (parms[i] == null)
					{
						methSig.Append("null");
					}
					else
					{
						methSig.Append(parms[i].GetType().FullName);
					}
					if (i < parms.Length - 1)
						methSig.Append(", ");
				}
				Console.WriteLine("NativeImplementation::InvokeMethod: not found '{0}({1})' on '{2}'", meth, methSig.ToString(), t.FullName);
			}
		}

		private Control mvarControl = null;
		public Control Control { get { return mvarControl; } }

		private Engine mvarEngine = null;
		public Engine Engine { get { return mvarEngine; } }

		public ControlImplementation (Engine engine, Control control)
		{
			mvarEngine = engine;
			mvarControl = control;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ControlImplementation" /> supports the <see cref="Engine" /> represented by the given type.
		/// </summary>
		/// <returns><c>true</c>, if engine was supportsed, <c>false</c> otherwise.</returns>
		/// <param name="engineType">Engine type.</param>
		protected abstract bool SupportsEngineInternal(Type engineType);

		protected abstract void InvalidateInternal(int x, int y, int width, int height);
		public void Invalidate(int x, int y, int width, int height)
		{
			InvalidateInternal(x, y, width, height);
		}

		protected abstract void DestroyInternal();
		/// <summary>
		/// Destroys the handle associated with this <see cref="ControlImplementation"/>'s <see cref="Control" />.
		/// </summary>
		public void Destroy()
		{
			DestroyInternal();
		}

		private Dictionary<Control, string> _controlText = new Dictionary<Control, string> ();
		protected virtual string GetControlTextInternal (Control control)
		{
			if (_controlText.ContainsKey (control))
				return _controlText [control];
			return null;
		}
		public string GetControlText (Control control)
		{
			if (!Application.Engine.IsControlCreated (control))
				return null;
			return GetControlTextInternal (control);
		}
		protected virtual void SetControlTextInternal (Control control, string text)
		{
			_controlText [control] = text;
		}
		public void SetControlText (Control control, string text)
		{
			if (!control.IsCreated)
				return;
			SetControlTextInternal (control, text);
		}

		protected abstract NativeControl CreateControlInternal (Control control);

		protected abstract Dimension2D GetControlSizeInternal();
		public Dimension2D GetControlSize()
		{
			return GetControlSizeInternal();
		}

		private NativeControl mvarHandle = null;
		public NativeControl Handle { get { return mvarHandle; } }

		public NativeControl CreateControl (Control control)
		{
			control.ControlImplementation = this;
			OnCreating (EventArgs.Empty);

			NativeControl handle = CreateControlInternal (control);
			if (handle == null) throw new InvalidOperationException ();

			mvarHandle = handle;
			OnCreated (EventArgs.Empty);
			return handle;
		}

		protected virtual void AfterCreateControl ()
		{

		}

		protected internal virtual void OnCreating (EventArgs e)
		{
		}
		protected internal virtual void OnCreated (EventArgs e)
		{
		}

		protected abstract bool IsControlVisibleInternal();
		public bool IsControlVisible()
		{
			return IsControlVisibleInternal();
		}

		public void SetControlVisibility (bool visible)
		{
			SetControlVisibilityInternal (visible);
		}
		protected abstract void SetControlVisibilityInternal (bool visible);

		protected abstract void RegisterDragSourceInternal (Control control, DragDrop.DragDropTarget [] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys);
		public void RegisterDragSource (Control control, DragDrop.DragDropTarget [] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
			RegisterDragSourceInternal (control, targets, actions, buttons, modifierKeys);
		}

		protected abstract void RegisterDropTargetInternal (Control control, DragDrop.DragDropTarget [] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys);
		public void RegisterDropTarget (Control control, DragDrop.DragDropTarget [] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
			RegisterDropTargetInternal (control, targets, actions, buttons, modifierKeys);
		}

		protected internal virtual void OnDragDropDataRequest (DragDropDataRequestEventArgs e)
		{
			Control.OnDragDropDataRequest (e);
		}
		protected internal virtual void OnDragBegin (DragEventArgs e)
		{
			Control.OnDragBegin (e);
		}
		protected internal virtual void OnDragDataDelete (EventArgs e)
		{
			Control.OnDragDataDelete (e);
		}

		protected internal virtual void OnKeyDown (KeyEventArgs e)
		{
			Control.OnKeyDown (e);
		}
		protected internal virtual void OnKeyUp (KeyEventArgs e)
		{
			Control.OnKeyUp (e);
		}

		protected internal virtual void OnClick (EventArgs e)
		{
			Control.OnClick (e);
		}

		protected internal virtual void OnMouseEnter(MouseEventArgs e)
		{
			Control.OnMouseEnter(e);
		}
		protected internal virtual void OnMouseDown (MouseEventArgs e)
		{
			Control.OnMouseDown (e);
		}
		protected internal virtual void OnMouseMove (MouseEventArgs e)
		{
			Control.OnMouseMove (e);
		}
		protected internal virtual void OnMouseUp (MouseEventArgs e)
		{
			Control.OnMouseUp (e);
		}
		protected internal virtual void OnMouseDoubleClick (MouseEventArgs e)
		{
			Control.OnMouseDoubleClick (e);
		}
		protected internal virtual void OnMouseLeave(MouseEventArgs e)
		{
			Control.OnMouseLeave(e);
		}

		protected internal virtual void OnRealize (EventArgs e)
		{
			Control.OnRealize (e);
		}
		protected internal virtual void OnUnrealize (EventArgs e)
		{
			Control.OnUnrealize (e);
		}

		protected internal virtual void OnResizing(ResizingEventArgs e)
		{
			Control.OnResizing(e);
		}
		protected internal virtual void OnResized(ResizedEventArgs e)
		{
			Control.OnResized(e);
		}


		protected virtual void OnBeforeContextMenu(EventArgs e)
		{
			Control.OnBeforeContextMenu(e);
		}

		protected virtual void OnAfterContextMenu(EventArgs e)
		{
			Control.OnAfterContextMenu(e);
		}

		protected internal virtual void OnMapping (EventArgs e)
		{
			Control.OnMapping (e);
		}
		protected internal virtual void OnMapped (EventArgs e)
		{
			Control.OnMapped (e);
		}
		protected internal virtual void OnShown (EventArgs e)
		{
			Control.OnShown (e);
		}

		protected internal virtual void OnGotFocus (EventArgs e)
		{
			Control.OnGotFocus (e);
		}
		protected internal virtual void OnLostFocus (EventArgs e)
		{
			Control.OnLostFocus (e);
		}

		protected abstract void SetFocusInternal ();
		public void SetFocus ()
		{
			SetFocusInternal ();
		}

		public void UpdateControlLayout ()
		{
			UpdateControlLayoutInternal ();
		}
		protected virtual void UpdateControlLayoutInternal ()
		{
		}

		protected abstract string GetTooltipTextInternal();
		public string GetTooltipText()
		{
			return GetTooltipTextInternal();
		}

		protected abstract void SetTooltipTextInternal(string value);
		public void SetTooltipText(string value)
		{
			SetTooltipTextInternal(value);
		}

		protected abstract Cursor GetCursorInternal();
		public Cursor GetCursor()
		{
			return GetCursorInternal();
		}

		protected abstract void SetCursorInternal(Cursor value);
		public void SetCursor(Cursor value)
		{
			SetCursorInternal(value);
		}

		protected abstract bool HasFocusInternal();
		public bool HasFocus()
		{
			return HasFocusInternal();
		}
	}
	public class ControlImplementationAttribute : Attribute
	{
		public Type ControlType { get; private set; } = null;

		/// <summary>
		/// Determines if an exact match of <see cref="ControlType" /> must be made for this <see cref="NativeImplementationAttribute" /> to apply to a particular <see cref="NativeImplementation" />.
		/// </summary>
		/// <value><c>true</c> if exact match required; otherwise, <c>false</c>.</value>
		public bool Exact { get; set; } = false;

		public ControlImplementationAttribute (Type controlType, bool exact = false)
		{
			if (!controlType.IsSubclassOf (typeof (Control)))
				throw new InvalidOperationException ();

			ControlType = controlType;
			Exact = exact;
		}
	}
}

