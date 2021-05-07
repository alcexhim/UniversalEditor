using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	public class IcarusCommandDo : IcarusPredefinedCommand
	{
		public override string Name
		{
			get { return "do"; }
		}

		public override object Clone()
		{
			throw new NotImplementedException();
		}

		private string mvarTarget = String.Empty;
		public string Target { get { return mvarTarget; } set { mvarTarget = value; } }
	}
}
