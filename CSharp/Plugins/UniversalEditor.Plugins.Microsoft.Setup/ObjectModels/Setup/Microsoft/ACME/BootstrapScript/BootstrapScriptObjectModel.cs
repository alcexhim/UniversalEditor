using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript
{
	public class BootstrapScriptObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Microsoft ACME Setup Bootstrapper Script";
				_omr.Path = new string[]
				{
					"Setup",
					"Microsoft",
					"ACME Setup"
				};
			}
			return _omr;
		}

		private BootstrapOperatingSystem.BootstrapOperatingSystemCollection mvarOperatingSystems = new BootstrapOperatingSystem.BootstrapOperatingSystemCollection();
		public BootstrapOperatingSystem.BootstrapOperatingSystemCollection OperatingSystems { get { return mvarOperatingSystems; } }

		public override void Clear()
		{
			mvarOperatingSystems.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			BootstrapScriptObjectModel clone = (where as BootstrapScriptObjectModel);
			foreach (BootstrapOperatingSystem item in mvarOperatingSystems)
			{
				clone.OperatingSystems.Add(item.Clone() as BootstrapOperatingSystem);
			}
		}
	}
}
