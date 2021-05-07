//
//  IcarusCommandPrint.cs - represents the ICARUS "print" command
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
	/// Represents the ICARUS "print" command.
	/// </summary>
	public class IcarusCommandPrint : IcarusPredefinedCommand
	{
		public IcarusCommandPrint()
		{
			Parameters.Add(new IcarusGenericParameter("Text", new IcarusConstantExpression("DEFAULT")));
		}

		public override string Name { get { return "print"; } }

		public IcarusExpression Text { get { return Parameters[0].Value; } set { Parameters[0].Value = value; } }

		public override object Clone()
		{
			IcarusCommandPrint clone = new IcarusCommandPrint();
			clone.Text = (Text.Clone() as IcarusExpression);
			return clone;
		}
	}
}
