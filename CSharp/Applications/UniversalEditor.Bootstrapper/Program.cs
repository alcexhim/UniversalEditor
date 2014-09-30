using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Bootstrapper
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				// why do we do this? because, if the class was static, it tries to load the 'Engine' type
				// from another library immediately... if it can't be found, it crashes. this way, if it
				// can't be found, we can still catch it since it's loaded on-demand rather than
				// immediately.
				(new BootstrapperInstance()).Main();
			}
			catch
			{
				MessageBox.Show("The file 'UniversalEditor.UserInterface.dll' is required for this software to run, but is either missing or corrupted.  Please re-install the software and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		private class BootstrapperInstance
		{
			public void Main()
			{
				if (!Engine.Execute())
				{
					MessageBox.Show("No engines are available to launch this application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
		}
	}
}
