using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles
{
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
