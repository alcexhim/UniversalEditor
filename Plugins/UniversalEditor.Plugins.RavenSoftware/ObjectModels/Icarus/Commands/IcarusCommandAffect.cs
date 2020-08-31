//
//  IcarusCommandAffect.cs - represents the ICARUS "affect" command
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
	/// Represents the ICARUS "affect" command.
	/// </summary>
	public class IcarusCommandAffect : IcarusPredefinedContainerCommand
    {
        public override string Name { get { return "affect"; } }

		public IcarusCommandAffect()
		{
			Parameters.Add(new IcarusGenericParameter("Target"));
			Parameters.Add(new IcarusChoiceParameter("AffectType",  new IcarusConstantExpression(IcarusAffectType.Flush), new IcarusChoiceParameterValue[]
			{
				new IcarusChoiceParameterValue("Flush", new IcarusConstantExpression(IcarusAffectType.Flush)),
				new IcarusChoiceParameterValue("Insert", new IcarusConstantExpression(IcarusAffectType.Insert))
			}));
		}

        public IcarusExpression Target { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }
        public IcarusExpression AffectType { get { return Parameters[1].Value; } set { Parameters[1].Value = value; } }

        public override object Clone()
        {
            IcarusCommandAffect clone = new IcarusCommandAffect();
            clone.Target = (Target.Clone() as IcarusExpression);
            clone.AffectType = (AffectType.Clone() as IcarusExpression);
            foreach (IcarusCommand command in Commands)
            {
                clone.Commands.Add(command.Clone() as IcarusCommand);
            }
            return clone;
        }
    }
}
