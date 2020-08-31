//
//  IcarusCommandPlay.cs - represents the ICARUS 'play' command
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

using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	/// <summary>
	/// Represents the ICARUS 'play' command.
	/// </summary>
	public class IcarusCommandPlay : IcarusPredefinedCommand
	{
		public IcarusCommandPlay()
		{
			Parameters.Add(new IcarusGenericParameter("Type"));
			Parameters.Add(new IcarusGenericParameter("Target"));
		}

		public override string Name => "play";

		public IcarusExpression Type { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }
		public IcarusExpression Target { get { return Parameters[1].Value; } set { Parameters[1].Value = value; } }

		public override object Clone()
		{
			IcarusCommandPlay clone = new IcarusCommandPlay();
			clone.Type = (Type.Clone() as IcarusExpression);
			clone.Target = (Target.Clone() as IcarusExpression);
			return clone;
		}
	}
}
