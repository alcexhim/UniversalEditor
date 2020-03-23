using System;
namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK
{
	internal static class Structures
	{
		public struct GtkTreeIter
		{
			public int stamp;
			public IntPtr user_data;
			public IntPtr user_data2;
			public IntPtr user_data3;

			public static readonly GtkTreeIter Empty = new GtkTreeIter();
		}
		public struct GtkTargetEntry
		{
			public string target;
			public Constants.GtkTargetFlags flags;
			public uint info;
		}
		public struct GtkFileFilterInfo
		{
			public Constants.GtkFileFilterFlags contains;

			public string filename;
			public string uri;
			public string display_name;
			public string mime_type;
		}
		public struct GtkStockItem
		{
			public /*string*/ IntPtr stock_id;
			public /*string*/ IntPtr label;
			public GDK.Constants.GdkModifierType modifier;
			public uint keyval;
			public /*string*/ IntPtr translation_domain;
		}

		public struct GtkTextIter
		{
			/* GtkTextIter is an opaque datatype; ignore all these fields.
 * Initialize the iter with gtk_text_buffer_get_iter_*
 * functions
 */
			/*< private >*/
			IntPtr dummy1;
			IntPtr dummy2;
			int dummy3;
			int dummy4;
			int dummy5;
			int dummy6;
			int dummy7;
			int dummy8;
			IntPtr dummy9;
			IntPtr dummy10;
			int dummy11;
			int dummy12;
			/* padding */
			int dummy13;
			IntPtr dummy14;
		}
	}
}
