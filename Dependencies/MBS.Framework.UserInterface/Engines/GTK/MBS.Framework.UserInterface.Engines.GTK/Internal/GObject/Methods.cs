using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GObject
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "gobject-2.0";

		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkGlAreaRenderFunc c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool g_type_check_instance_is_a(IntPtr /*GTypeInstance*/ instance, GType iface_type);

		public static bool G_TYPE_CHECK_INSTANCE_TYPE(IntPtr handle, GType typeEntry)
		{
			IntPtr __inst = handle;
			GType __t = typeEntry;
			bool __r;
			if (__inst == IntPtr.Zero)
			{
				__r = false;
			}
			/*
			else if (__inst->g_class && __inst->g_class->g_type == __t)
			{
				__r = true;
			}
			*/
			else
			{
				__r = g_type_check_instance_is_a(__inst, __t);
			}
			return __r;
		}

		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkWidgetEvent c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}

		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler, IntPtr data)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.ConnectAfter);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler, IntPtr data)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}

		#region GtkTreeView
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkTreeViewFunc c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);

		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkTreeViewFunc c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkTreeViewFunc c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.ConnectAfter);
		}
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkTreeViewRowActivatedFunc c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkTreeViewRowActivatedFunc c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkTreeViewRowActivatedFunc c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.ConnectAfter);
		}
		#endregion

		#region Cairo
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Delegates.DrawFunc c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Delegates.DrawFunc c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkGlAreaRenderFunc c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);

		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkWidgetEvent c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Internal.GTK.Delegates.GtkWidgetEvent c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.ConnectAfter);
		}


		#endregion

#region GCallbackV1I
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Delegates.GCallbackV1I c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Delegates.GCallbackV1I c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Delegates.GCallbackV1I c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region GCallbackV3I
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Delegates.GCallbackV3I c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Delegates.GCallbackV3I c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Delegates.GCallbackV3I c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Action<IntPtr>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Action<IntPtr> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Action<IntPtr> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Action<IntPtr> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Func<IntPtr, IntPtr, bool>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, bool> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, bool> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, bool> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Action<IntPtr, string, string, IntPtr>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Action<IntPtr, string, string, IntPtr> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Action<IntPtr, string, string, IntPtr> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Action<IntPtr, string, string, IntPtr> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Action<IntPtr, IntPtr, uint>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Action<IntPtr, IntPtr, uint> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Action<IntPtr, IntPtr, uint> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Action<IntPtr, IntPtr, uint> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Func<IntPtr, int, IntPtr, bool>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Func<IntPtr, int, IntPtr, bool> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Func<IntPtr, int, IntPtr, bool> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Func<IntPtr, int, IntPtr, bool> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region Func<IntPtr, IntPtr, IntPtr, bool>
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, IntPtr, bool> c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, IntPtr, bool> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Func<IntPtr, IntPtr, IntPtr, bool> c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region GApplicationCommandLine
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Delegates.GApplicationCommandLineHandler c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, Delegates.GApplicationCommandLineHandler c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, Delegates.GApplicationCommandLineHandler c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion


#region drag
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, GTK.Delegates.GtkDragEvent c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, GTK.Delegates.GtkDragEvent c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, GTK.Delegates.GtkDragEvent c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion
#region GtkDragDataGetEvent
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, GTK.Delegates.GtkDragDataGetEvent c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, GTK.Delegates.GtkDragDataGetEvent c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, GTK.Delegates.GtkDragDataGetEvent c_handler)
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, IntPtr.Zero, null, Constants.GConnectFlags.ConnectAfter);
		}
#endregion

		#region GdlMoveFocusChildCallback
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, GDL.Delegates.GdlMoveFocusChildCallback c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		public static uint g_signal_connect(IntPtr instance, string detailed_signal, GDL.Delegates.GdlMoveFocusChildCallback c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}
		public static uint g_signal_connect_after(IntPtr instance, string detailed_signal, GDL.Delegates.GdlMoveFocusChildCallback c_handler, IntPtr data = default(IntPtr))
		{
			return g_signal_connect_data(instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.ConnectAfter);
		}
		#endregion

		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_object(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler, IntPtr gobject, Constants.GConnectFlags connect_flags);

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool g_type_check_instance_is_a(IntPtr instance, IntPtr instance_type);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_type_init();

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_init(ref GLib.Structures.Value val, IntPtr gtype);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_boolean(ref GLib.Structures.Value val, bool data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_uchar(ref GLib.Structures.Value val, byte data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_char(ref GLib.Structures.Value val, sbyte data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_boxed(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_double(ref GLib.Structures.Value val, double data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_float(ref GLib.Structures.Value val, float data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_int(ref GLib.Structures.Value val, int data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_int64(ref GLib.Structures.Value val, long data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_uint64(ref GLib.Structures.Value val, ulong data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_object(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_param(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_pointer(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_string(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_uint(ref GLib.Structures.Value val, uint data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_enum(ref GLib.Structures.Value val, int data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_flags(ref GLib.Structures.Value val, uint data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool g_value_get_boolean(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte g_value_get_uchar(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern sbyte g_value_get_char(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_boxed(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern double g_value_get_double(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern float g_value_get_float(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern int g_value_get_int(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern long g_value_get_int64(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_unset(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong g_value_get_uint64(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_object(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_param(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_pointer(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_string(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint g_value_get_uint(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern int g_value_get_enum(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint g_value_get_flags(ref GLib.Structures.Value val);


		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern string g_type_name(IntPtr raw);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_type_from_name(string name);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool g_type_is_a(IntPtr type, IntPtr is_a_type);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_strv_get_type();

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_unref(IntPtr obj);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_set_property (IntPtr /*GObject*/ obj, string property_name, ref GLib.Structures.Value value);
		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_get_property (IntPtr /*GObject*/ obj, string property_name, ref GLib.Structures.Value value);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_set_property(IntPtr /*GObject*/ obj, string property_name, IntPtr value);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_object_new(IntPtr type, string prop1, int val1, IntPtr zero);
		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_object_new(IntPtr type, string prop1, IntPtr val1, string prop2, int val2, IntPtr zero);
	}
}

