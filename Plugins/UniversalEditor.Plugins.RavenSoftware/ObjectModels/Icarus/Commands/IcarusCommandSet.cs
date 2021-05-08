//
//  IcarusCommandSet.cs - represents the ICARUS "set" command
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
	/// Represents the ICARUS "set" command.
	/// </summary>
	public class IcarusCommandSet : IcarusPredefinedCommand
	{
		public IcarusCommandSet()
		{
			Parameters.Add(new IcarusGenericParameter("ObjectName"));
			Parameters.Add(new IcarusGenericParameter("Value"));
		}

		public override string Name
		{
			get { return "set"; }
		}

		public override object Clone()
		{
			IcarusCommandSet clone = new IcarusCommandSet();
			clone.ObjectName = (ObjectName.Clone() as IcarusExpression);
			clone.Value = (Value.Clone() as IcarusExpression);
			return clone;
		}

		public IcarusExpression ObjectName { get { return Parameters["ObjectName"].Value; } set { Parameters["ObjectName"].Value = value; } }
		public IcarusExpression Value { get { return Parameters["Value"].Value; } set { Parameters["Value"].Value = value; } }
	}
}
