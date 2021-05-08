//
//  IcarusCommandDeclare.cs - represents the ICARUS "declare" command
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
	/// Represents the ICARUS "declare" command.
	/// </summary>
	public class IcarusCommandDeclare : IcarusPredefinedCommand
	{
		public IcarusCommandDeclare()
		{
			Parameters.Add(new IcarusChoiceParameter("DataType", new IcarusConstantExpression("FLOAT"), new IcarusChoiceParameterValue[]
			{
				new IcarusChoiceParameterValue("FLOAT", new IcarusConstantExpression(IcarusVariableDataType.Float)),
				new IcarusChoiceParameterValue("STRING", new IcarusConstantExpression(IcarusVariableDataType.String)),
				new IcarusChoiceParameterValue("VECTOR", new IcarusConstantExpression(IcarusVariableDataType.Vector))
			}));
			Parameters.Add(new IcarusGenericParameter("VariableName", new IcarusConstantExpression("variablename")));
		}

		public override string Name { get { return "declare"; } }

		public IcarusVariableDataType DataType { get { return (IcarusVariableDataType)((IcarusConstantExpression)Parameters["DataType"].Value)?.Value; } set { Parameters["DataType"].Value = new IcarusConstantExpression(value); } }
		public IcarusExpression VariableName { get { return Parameters["VariableName"].Value; } set { Parameters["VariableName"].Value = value; } }

		public override object Clone()
		{
			IcarusCommandDeclare clone = new IcarusCommandDeclare();
			clone.VariableName = (VariableName?.Clone() as IcarusExpression);
			clone.DataType = DataType;
			return clone;
		}
	}
}
