//
//  MyClass.cs
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
using MBS.Framework.UserInterface;

namespace UniversalEditor.Plugins.AutoSave
{
	public class AutoSavePlugin : UserInterfacePlugin
	{
		private Timer tmr = new Timer();

		private void tmr_Tick(object sender, EventArgs e)
		{
			Console.WriteLine("autosave: looking for dirty documents...");
			Console.WriteLine("autosave: saving dirty documents in /tmp/autosave/universal-editor/...");
			Console.WriteLine("autosave: going back to sleep");
		}


		protected override void InitializeInternal()
		{
			base.InitializeInternal();

			tmr.Duration = 5 /*minutes*/ * 60 /*seconds in a minute*/ * 1000 /*milliseconds in a second*/;
			tmr.Tick += tmr_Tick;
			tmr.Enabled = true;
		}
	}
}
