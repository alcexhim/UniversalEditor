//
//  IcarusCommandSignal.cs - represents the ICARUS "signal" command
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

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	/// <summary>
	/// Represents the ICARUS "signal" command.
	/// </summary>
	public class IcarusCommandSignal : IcarusPredefinedCommand
	{
		public override string Name { get { return "signal"; } }

		private string mvarTarget = String.Empty;
		public string Target { get { return mvarTarget; } set { mvarTarget = value; } }

		public override object Clone()
		{
			IcarusCommandSignal clone = new IcarusCommandSignal();
			clone.Target = (mvarTarget.Clone() as string);
			return clone;
		}
	}
}
