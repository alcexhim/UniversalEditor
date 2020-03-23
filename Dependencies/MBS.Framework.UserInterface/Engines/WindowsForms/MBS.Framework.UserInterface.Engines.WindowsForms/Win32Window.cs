using System;
namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public class Win32Window : System.Windows.Forms.IWin32Window
	{
		public IntPtr Handle { get; set; } = IntPtr.Zero;
		public Win32Window(IntPtr handle)
		{
			Handle = handle;
		}
	}
}
