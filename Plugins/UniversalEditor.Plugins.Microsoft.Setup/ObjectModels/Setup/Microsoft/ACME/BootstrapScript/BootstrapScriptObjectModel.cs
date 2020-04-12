//
//  BootstrapScriptObjectModel.cs - provides an ObjectModel for manipulating Microsoft ACME setup bootstrapper scripts
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

namespace UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Microsoft ACME setup bootstrapper scripts.
	/// </summary>
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
