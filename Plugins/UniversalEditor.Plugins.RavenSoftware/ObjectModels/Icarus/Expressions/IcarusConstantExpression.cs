//
//  IcarusConstantExpression.cs - represents an ICARUS literal expression
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

using System.Text;

namespace UniversalEditor.ObjectModels.Icarus.Expressions
{
	/// <summary>
	/// Represents an ICARUS literal expression.
	/// </summary>
	public class IcarusConstantExpression : IcarusExpression
	{
		public IcarusConstantExpression(string value)
		{
			DataType = IcarusVariableDataType.String;
			Value = value;
		}
		public IcarusConstantExpression(float value)
		{
			DataType = IcarusVariableDataType.Float;
			Value = value;
		}
		public IcarusConstantExpression(float[] value)
		{
			DataType = IcarusVariableDataType.Vector;
			Value = value;
		}
		public IcarusConstantExpression(float x, float y, float z)
		{
			DataType = IcarusVariableDataType.Vector;
			Value = new float[] { x, y, z };
		}
		public IcarusConstantExpression(IcarusVariableDataType value)
		{
			DataType = IcarusVariableDataType.Float;
			Value = value;
		}
		public IcarusConstantExpression(IcarusAffectType value)
		{
			DataType = IcarusVariableDataType.Float;
			Value = value;
		}
		public IcarusConstantExpression(IcarusCameraOperation value)
		{
			DataType = IcarusVariableDataType.Float;
			Value = value;
		}

		public IcarusVariableDataType DataType { get; set; } = IcarusVariableDataType.String;
		public object Value { get; set; } = null;

		protected override bool GetValueInternal(ref object value)
		{
			value = Value;
			return true;
		}

		public override object Clone()
		{
			IcarusConstantExpression clone = null;
			if (Value is string)
			{
				clone = new IcarusConstantExpression((string)Value);
			}
			else if (Value is float)
			{
				clone = new IcarusConstantExpression((float)Value);
			}
			else if (Value is float[])
			{
				clone = new IcarusConstantExpression((float[])Value);
			}
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			switch (DataType)
			{
				case IcarusVariableDataType.Float:
				{
					sb.Append(Value);
					break;
				}
				case IcarusVariableDataType.String:
				{
					sb.Append("\"");
					sb.Append(Value);
					sb.Append("\"");
					break;
				}
				case IcarusVariableDataType.Vector:
				{
					float[] val = (float[])Value;
					for (int i = 0; i < val.Length; i++)
					{
						sb.Append(val[i]);
						if (i < val.Length - 1)
							sb.Append(' ');
					}
					break;
				}
			}
			return sb.ToString();
		}
	}
}
