//
//  AutoStartCommand.cs - a command that is automatically executed when the installation application is launched
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles
{
	/// <summary>
	/// A command that is automatically executed when the installation application is launched.
	/// </summary>
	public class AutoStartCommand
	{
		private char _key = '\0';
		private AutoStartCommand(char _key)
		{

		}

		private static AutoStartCommand m_Install = new AutoStartCommand('I');
		/// <summary>
		/// Installs the application.
		/// </summary>
		public static AutoStartCommand Install { get { return m_Install; } }

		private static AutoStartCommand m_Catalog = new AutoStartCommand('C');
		/// <summary>
		/// Displays the product catalog.
		/// </summary>
		public static AutoStartCommand Catalog { get { return m_Catalog; } }

		private static AutoStartCommand m_Exit = new AutoStartCommand('X');
		/// <summary>
		/// Exits the Setup program.
		/// </summary>
		public static AutoStartCommand Exit { get { return m_Exit; } }

		private static AutoStartCommand m_Restart = new AutoStartCommand('S');
		/// <summary>
		/// Restarts the operating system (actually just logs you off and back on again under Win2k).
		/// </summary>
		public static AutoStartCommand Restart { get { return m_Restart; } }

		public class AutoStartCommandCollection
			: System.Collections.ObjectModel.Collection<AutoStartCommand>
		{
		}
	}
}
