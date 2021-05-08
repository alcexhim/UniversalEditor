//
//  IcarusCommandLoop.cs - represents the ICARUS "loop" command
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

using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	/// <summary>
	/// Represents the ICARUS "loop" command.
	/// </summary>
	public class IcarusCommandLoop : IcarusPredefinedContainerCommand
	{
		public IcarusCommandLoop()
		{
			Parameters.Add(new IcarusGenericParameter("Count", new IcarusConstantExpression((float)0)));
		}

		public override string Name
		{
			get { return "loop"; }
		}

		public override object Clone()
		{
			IcarusCommandLoop clone = new IcarusCommandLoop();
			clone.Count = (Count.Clone() as IcarusExpression);
			return clone;
		}

		public IcarusExpression Count { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }
	}
}
