using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.Cairo
{
	internal static class Methods
	{
		const string LIBRARY_FILENAME = "cairo";

		/// <summary>
		///  Creates a new cairo_t with all graphics state parameters set to default values and with target as a target surface. The target surface should be constructed with a backend-specific function such as cairo_image_surface_create() (or any other cairo_backend_surface_create() variant).
		/// </summary>
		/// <returns>
		/// a newly allocated cairo_t with a reference count of 1. The initial reference count should be released with <see cref="cairo_destroy"/> when you are done using the cairo_t. This function never returns NULL. If memory cannot be allocated, a special cairo_t object will be returned on which <see cref="cairo_status"/> returns CAIRO_STATUS_NO_MEMORY. You can use this object normally, but no drawing will be done.
		/// </returns>
		/// <param name="target">target surface for the context</param>
		/// <remarks>This function references target, so you can immediately call cairo_surface_destroy() on it if you don't need to maintain a separate reference to it.</remarks>
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_t*/ cairo_create(IntPtr /*cairo_surface_t*/ target);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_move_to(IntPtr /*cairo_t*/ cr, double x, double y);

		/// <summary>
		/// Decreases the reference count on cr by one. If the result is zero, then cr and all associated resources are freed. See <see cref="cairo_reference"/>. 
		/// </summary>
		/// <param name="cr">Cr.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_destroy(IntPtr /*cairo_t*/ cr);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_rectangle(IntPtr /*cairo_t*/ cc, double x, double y, double width, double height);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_fill(IntPtr /*cairo_t*/ cc);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_stroke(IntPtr /*cairo_t*/ cc);

		/// <summary>
		/// A drawing operator that strokes the current path according to the current line width, line join, line cap, and dash settings. Unlike <see cref="cairo_stroke"/>, <see cref="cairo_stroke_preserve"/> preserves the path within the cairo context.
		/// </summary>
		/// <param name="cr">a cairo context</param>
		/// <seealso cref="cairo_set_line_width" />
		/// <seealso cref="cairo_set_line_join" />
		/// <seealso cref="cairo_set_line_cap" />
		/// <seealso cref="cairo_set_dash" />
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_stroke_preserve(IntPtr /*cairo_t*/ cr);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_line_to(IntPtr /*cairo_t*/ cr, double x, double y);


		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.CairoStatus cairo_status(IntPtr /*cairo_t*/ cc);

		[DllImport(LIBRARY_FILENAME)]
		public static extern string cairo_status_to_string(Constants.CairoStatus status);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_paint(IntPtr /*cairo_t*/ cr);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_show_page(IntPtr /*cairo_t*/ cr);

		#region Line
		/// <summary>
		/// Sets the current line width within the cairo context. The line width value specifies the diameter of a pen that is circular in user space, (though device-space pen may be an ellipse in general due to scaling/shear/rotation of the CTM).
		/// Note: When the description above refers to user space and CTM it refers to the user space and CTM in effect at the time of the stroking operation, not the user space and CTM in effect at the time of the call to cairo_set_line_width(). The simplest usage makes both of these spaces identical.That is, if there is no change to the CTM between a call to cairo_set_line_width() and the stroking operation, then one can just pass user-space values to cairo_set_line_width() and ignore this note.
		/// As with the other stroke parameters, the current line width is examined by cairo_stroke(), cairo_stroke_extents(), and cairo_stroke_to_path(), but does not have any effect during path construction.
		/// The default line width value is 2.0. 
		/// </summary>
		/// <param name="cr">Cr.</param>
		/// <param name="width">Width.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_set_line_width(IntPtr /*cairo_t*/ cr, double width);
		#endregion

		#region Pattern
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_rgb(double red, double green, double blue);

		#region Raster Pattern
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_raster_source(IntPtr user_data, Constants.CairoContent content, int width, int height);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_raster_source_pattern_set_acquire(IntPtr /*cairo_pattern_t*/ pattern,
			Func<IntPtr /*cairo_pattern_t*/, IntPtr /*void callback_data*/, IntPtr /*cairo_surface_t target*/, Internal.Cairo.Structures.cairo_rectangle_int_t /*extents*/, IntPtr /*cairo_surface_t *retval*/> acquire,
			Action<IntPtr /*cairo_pattern_t*/, IntPtr /*void callback_data*/, IntPtr /*cairo_surface_t surface*/> release);

		#endregion

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_select_font_face(IntPtr /*cairo_t*/ cr, string familyName, Constants.CairoFontSlant slant, Constants.CairoFontWeight weight);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_set_font_size(IntPtr /*cairo_t*/ cr, double size);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_rgba(double red, double green, double blue, double alpha);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_for_surface(IntPtr /*cairo_surface_t*/ surface);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_linear(double x0, double y0, double x1, double y1);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_radial(double cx0, double cy0, double radius0, double cx1, double cy1, double radius1);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_create_mesh();

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_pattern_t*/ cairo_pattern_reference(IntPtr /*cairo_pattern_t*/ pattern);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void /*cairo_pattern_t*/ cairo_pattern_destroy(IntPtr /*cairo_pattern_t*/ pattern);
		#endregion
		#region Source
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_set_source(IntPtr /*cairo_t*/ cr, IntPtr /*cairo_pattern_t*/ source);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_set_source_rgb(IntPtr /*cairo_t*/ cr, double red, double green, double blue);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_set_source_rgba(IntPtr /*cairo_t*/ cr, double red, double green, double blue, double alpha);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_set_source_surface(IntPtr /*cairo_t*/ cr, IntPtr /*cairo_surface_t*/ surface, double x, double y);
		#endregion

		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_text_path(IntPtr /*cairo_t*/ cr, string value);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_show_text(IntPtr /*cairo_t*/ cr, string value);

		#region Surface
		/// <summary>
		/// This function finishes the surface and drops all references to external resources. For example, for the Xlib backend it means that cairo will no longer access the drawable, which can be freed. After calling <see cref="cairo_surface_finish" /> the only valid operations on a surface are getting and setting user, referencing and destroying, and flushing and finishing it. Further drawing to the surface will not affect the surface but will instead trigger a CAIRO_STATUS_SURFACE_FINISHED error.
		/// When the last call to <see cref="cairo_surface_destroy" /> decreases the reference count to zero, cairo will call <see cref="cairo_surface_finish" /> if it hasn't been called already, before freeing the resources associated with the surface. 
		/// </summary>
		/// <param name="surface">Surface.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_surface_finish(IntPtr /*cairo_surface_t*/ surface);

		/// <summary>
		/// Decreases the reference count on surface by one. If the result is zero, then surface and all associated resources are freed. See <see cref="cairo_surface_reference"/>.
		/// </summary>
		/// <param name="surface">a cairo_surface_t.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void cairo_surface_destroy(IntPtr /*cairo_surface_t*/ surface);
		#endregion

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_surface_t*/ cairo_image_surface_create(Constants.CairoFormat format, int width, int height);

		[DllImport(LIBRARY_FILENAME)]
		public static extern int cairo_image_surface_get_height(IntPtr /*cairo_surface_t*/ surface);
		[DllImport(LIBRARY_FILENAME)]
		public static extern int cairo_image_surface_get_width(IntPtr /*cairo_surface_t*/ surface);
	}
}
