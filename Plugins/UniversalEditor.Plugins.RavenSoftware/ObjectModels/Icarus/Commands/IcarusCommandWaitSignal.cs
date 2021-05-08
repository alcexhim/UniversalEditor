//
//  IcarusCommandWaitSignal.cs - represents the ICARUS "waitsignal" command
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

using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	/// <summary>
	/// Represents the ICARUS "waitsignal" command.
	/// </summary>
	public class IcarusCommandWaitSignal : IcarusPredefinedCommand
	{
		public override string Name { get { return "waitsignal"; } }
		public IcarusExpression Target { get { return Parameters["Target"].Value; } set { Parameters["Target"].Value = value; } }

		public IcarusCommandWaitSignal()
		{
			Parameters.Add(new IcarusGenericParameter("Target"));
		}

		public override object Clone()
		{
			IcarusCommandWaitSignal clone = new IcarusCommandWaitSignal();
			clone.Target = (Target.Clone() as IcarusExpression);
			return clone;
		}
	}
}
