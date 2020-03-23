using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GDK
{
	internal static class Structures
	{
		public struct GdkRGBA
		{
			public double red;
			public double green;
			public double blue;
			public double alpha;
		}
		public struct GdkColor
		{
			public uint pixel;
			public ushort red;
			public ushort green;
			public ushort blue;
		}
		public struct GdkRectangle
		{
			public int x;
			public int y;
			public int width;
			public int height;
		}
		public struct GdkEventButton
		{
			#region GdkEventTimed members
			#region GdkEvent members
			/// <summary>
			/// the type of the event (%GDK_KEY_PRESS or %GDK_KEY_RELEASE).
			/// </summary>
			public Constants.GdkEventType type;
			/// <summary>
			/// the window which received the event.
			/// </summary>
			public IntPtr /*GdkWindow*/ window;
			/// <summary>
			/// <c>true</c> if the event was sent explicitly.
			/// </summary>
			public byte send_event;
			#endregion
			/// <summary>
			/// the time of the event in milliseconds.
			/// </summary>
			public uint time;
			#endregion

			public double x;
			public double y;
			public IntPtr /*double[]*/ axes;
			public Constants.GdkModifierType state;
			public uint button;
			public IntPtr /*GdkDevice*/ device;
			public double x_root, y_root;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct GdkEventMotion
		{
			#region GdkEventTimed members
			#region GdkEvent members
			/// <summary>
			/// the type of the event (%GDK_KEY_PRESS or %GDK_KEY_RELEASE).
			/// </summary>
			public Constants.GdkEventType type;
			/// <summary>
			/// the window which received the event.
			/// </summary>
			public IntPtr /*GdkWindow*/ window;
			/// <summary>
			/// <c>true</c> if the event was sent explicitly.
			/// </summary>
			public byte send_event;
			#endregion
			/// <summary>
			/// the time of the event in milliseconds.
			/// </summary>
			public uint time;
			#endregion

			public double x;
			public double y;
			public IntPtr /*double[]*/ axes;
			public Constants.GdkModifierType state;
			public short is_hint;
			public IntPtr device;
			public double x_root, y_root;
		}

		/// <summary>
		/// Describes a key press or key release event.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct GdkEventKey
		{
			#region GdkEventTimed members
			#region GdkEvent members
			/// <summary>
			/// the type of the event (%GDK_KEY_PRESS or %GDK_KEY_RELEASE).
			/// </summary>
			public Constants.GdkEventType type;
			/// <summary>
			/// the window which received the event.
			/// </summary>
			public IntPtr /*GdkWindow*/ window;
			/// <summary>
			/// <c>true</c> if the event was sent explicitly.
			/// </summary>
			public byte send_event;
			#endregion
			/// <summary>
			/// the time of the event in milliseconds.
			/// </summary>
			public uint time;
			#endregion

			/// <summary>
			/// a bit-mask representing the state of the modifier keys
			/// (e.g.Control, Shift and Alt) and the pointer buttons.
			/// </summary>
			public Constants.GdkModifierType state;
			/// <summary>
			/// the key that was pressed or released. See the
			/// `gdk/gdkkeysyms.h` header file for a complete list of
			/// GDK key codes.
			/// </summary>
			public uint keyval;
			/// <summary>
			/// the length of <see cref="str"/>.
			/// </summary>
			public int strlength;
			/// <summary>
			/// a string containing an approximation of the text that
			/// would result from this keypress.
			/// </summary>
			/// <remarks>
			/// The only correct way to handle text input of text is using
			/// input methods(see #GtkIMContext), so this field is deprecated
			/// and should never be used. (gdk_unicode_to_keyval() provides a
			/// non-deprecated way of getting an approximate translation for a
			/// key.) The string is encoded in the encoding of the current
			/// locale (Note: this for backwards compatibility: strings in
			/// GTK+ and GDK are typically in UTF-8.) and NUL-terminated. In
			/// some cases, the translation of the key code will be a single
			/// NUL byte, in which case looking at @length is necessary to
			/// distinguish it from the an empty translation.
			/// </remarks>
			[Obsolete("DON'T DEREFERENCE THIS")]
			public IntPtr str;
			/// <summary>
			/// the raw code of the key that was pressed or released.
			/// </summary>
			public ushort hardware_keycode;
			/// <summary>
			/// the keyboard group.
			/// </summary>
			public byte group;
		}

		/// <summary>
		/// Generated when a window size or position has changed.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct GdkEventConfigure
		{
			#region GdkEvent members
			/// <summary>
			/// the type of the event (%GDK_KEY_PRESS or %GDK_KEY_RELEASE).
			/// </summary>
			public Constants.GdkEventType type;
			/// <summary>
			/// the window which received the event.
			/// </summary>
			public IntPtr /*GdkWindow*/ window;
			/// <summary>
			/// <c>true</c> if the event was sent explicitly.
			/// </summary>
			public byte send_event;
			#endregion

			/// <summary>
			/// the new x coordinate of the window, relative to its parent.
			/// </summary>
			public int x;
			/// <summary>
			/// the new y coordinate of the window, relative to its parent.
			/// </summary>
			// public int y;
			/// <summary>
			/// the new width of the window.
			/// </summary>
			public int width;
			/// <summary>
			/// the new height of the window.
			/// </summary>
			public int height;
		}
	}
}

