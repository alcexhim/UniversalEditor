using System;
using System.Collections.Generic;
using System.ComponentModel;
using MBS.Framework.Rendering;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Native;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(OpenGLCanvas))]
	public class OpenGLCanvasImplementation : GTKNativeImplementation
	{
		public OpenGLCanvasImplementation(Engine engine, Control control) : base (engine, control)
		{
		}
		/*
		private void Canvas_Realize(IntPtr handle)
		{
			OpenGLCanvas ctl = (Application.Engine.GetControlByHandle(handle) as OpenGLCanvas);
			if (ctl == null)
				return;
			
			// We need to make the context current if we want to call GL API
			Internal.GTK.Methods.GtkGlArea.gtk_gl_area_make_current(handle);

			// If there were errors during the initialization or when trying to make the
			// context current, this function will return a #GError for you to catch
			IntPtr hError = Internal.GTK.Methods.GtkGlArea.gtk_gl_area_get_error(area);
			if (hError != IntPtr.Zero) Console.Error.WriteLine("gtk_gl_area_get_error: " + hError.ToString());

		}
		*/
		private static bool Canvas_Render(IntPtr handle, IntPtr context)
		{
			Internal.GTK.Methods.GtkGlArea.gtk_gl_area_make_current(handle);
			OpenGLCanvas ctl = ((Application.Engine as GTKEngine).GetControlByHandle(handle) as OpenGLCanvas);
			if (ctl == null)
				return true;

			Canvas canvas = Rendering.Engine.GetDefault()?.CreateCanvas();

			OpenGLCanvasRenderEventArgs e = new OpenGLCanvasRenderEventArgs(canvas);
			ctl.OnRender(e);
			return true;
		}

		protected override void OnRealize(EventArgs e)
		{
			IntPtr handle = (Handle as GTKNativeControl).Handle;
			
			// We need to make the context current if we want to call GL API
			Internal.GTK.Methods.GtkGlArea.gtk_gl_area_make_current(handle);

			// do some error checking before we hand control over to UWT
			IntPtr hGErr = Internal.GTK.Methods.GtkGlArea.gtk_gl_area_get_error(handle);
			if (hGErr != IntPtr.Zero)
			{
				Console.Error.WriteLine("OpenGLCanvasImplementation: gtk_gl_area_get_area returned {0}", hGErr);
			}
			
			// Tell NativeImplementation to fire the OnRealize event on our universal control
			base.OnRealize(e);
		}
		

		[System.Diagnostics.DebuggerNonUserCode()]
		protected override NativeControl CreateControlInternal(Control control)
		{
			OpenGLCanvas ctl = (control as OpenGLCanvas);
			if (ctl == null) throw new InvalidOperationException();

			IntPtr handle = Internal.GTK.Methods.GtkGlArea.gtk_gl_area_new();

			Internal.GObject.Methods.g_signal_connect(handle, "render",  (Internal.GTK.Delegates.GtkGlAreaRenderFunc)Canvas_Render);
			return new GTKNativeControl(handle);
		}
	}
}
