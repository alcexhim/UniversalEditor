//
//  Program.cs - the main entry point for the Universal Editor Platform Bootstrapper
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
				string path =
					System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
					+ System.IO.Path.DirectorySeparatorChar.ToString()
					+ "UniversalEditor.UserInterface.dll";

				Assembly asm = System.Reflection.Assembly.LoadFile(path);
			}
			catch
			{
				MessageBox.Show("The file 'UniversalEditor.UserInterface.dll' is required for this software to run, but is either missing or corrupted.  Please re-install the software and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// why do we do this? because, if the class was static, it tries to load the 'Engine' type
			// from another library immediately... if it can't be found, it crashes. this way, if it
			// can't be found, we can still catch it since it's loaded on-demand rather than
			// immediately.
			(new BootstrapperInstance()).Main();
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
