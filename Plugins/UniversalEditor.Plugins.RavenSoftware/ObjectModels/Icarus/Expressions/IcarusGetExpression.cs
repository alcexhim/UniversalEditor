//
//  IcarusGetExpression.cs - represents an ICARUS "get()" function expression
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

namespace UniversalEditor.ObjectModels.Icarus.Expressions
{
	/// <summary>
	/// Represents an ICARUS "get()" function expression.
	/// </summary>
	public class IcarusGetExpression : IcarusFunctionExpression
	{
		public IcarusGetExpression(IcarusVariableDataType dataType, string variableName)
		{
			Type = dataType;
			VariableName = variableName;
		}

		public IcarusVariableDataType Type { get; set; } = IcarusVariableDataType.Float;
		public string VariableName { get; set; } = null;

		protected override string FunctionName => "get";
		protected override string[] Parameters => new string[] { Type.ToString().ToUpper(), VariableName };

		protected override bool GetValueInternal(ref object value)
		{
			return false;
		}

		public override object Clone()
		{
			IcarusGetExpression clone = new IcarusGetExpression(Type, VariableName.Clone() as string);
			return clone;
		}
	}
}
