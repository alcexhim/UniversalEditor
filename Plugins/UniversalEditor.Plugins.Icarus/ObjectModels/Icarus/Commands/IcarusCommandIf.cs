//
//  IcarusCommandIf.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker
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
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	public class IcarusCommandIf : IcarusPredefinedContainerCommand
	{
		public IcarusExpression Source { get; set; } = null;
		public IcarusExpression Comparison { get; set; } = null;
		public IcarusExpression Target { get; set; } = null;

		public override string Name => "if";

		public IcarusCommandIf()
		{
			Parameters.Add(new IcarusGenericParameter("Source"));
			Parameters.Add(new IcarusGenericParameter("Comparison"));
			Parameters.Add(new IcarusGenericParameter("Target"));
		}

		public override object Clone()
		{
			IcarusCommandIf clone = new IcarusCommandIf();
			clone.Source = (Source.Clone() as IcarusExpression);
			clone.Comparison = (Comparison.Clone() as IcarusExpression);
			clone.Target = (Target.Clone() as IcarusExpression);
			return clone;
		}
	}
}
