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
			if (!Engine.Execute())
			{
				MessageBox.Show("No engines are available to launch this application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}
	}
}
