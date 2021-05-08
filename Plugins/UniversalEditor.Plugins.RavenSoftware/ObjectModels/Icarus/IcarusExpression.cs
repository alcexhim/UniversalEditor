//
//  IcarusExpression.cs - the abstract base class from which all ICARUS expression implementations derive
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

using System;
using UniversalEditor.ObjectModels.Icarus.Expressions;

namespace UniversalEditor.ObjectModels.Icarus
{
	/// <summary>
	/// The abstract base class from which all ICARUS expression implementations derive.
	/// </summary>
	public abstract class IcarusExpression : ICloneable
	{
		public class IcarusExpressionCollection
			: System.Collections.ObjectModel.Collection<IcarusExpression>
		{
		}

		public abstract object Clone();

		protected abstract bool GetValueInternal(ref object value);
		public T GetValue<T>(T defaultValue = default(T))
		{
			object val = defaultValue;
			bool ret = GetValueInternal(ref val);
			if (ret) return (T)val;
			return defaultValue;
		}

		public static IcarusExpression Parse(string parm)
		{
			if (parm.StartsWith("\"") && parm.EndsWith("\""))
			{
				return new IcarusConstantExpression(parm.Substring(1, parm.Length - 2));
			}
			else if (parm.StartsWith("$") && parm.EndsWith("$"))
			{
				return new IcarusConstantExpression(parm);
			}
			else
			{
				switch (parm)
				{
					case "FLOAT":
					{
						return new IcarusConstantExpression(IcarusVariableDataType.Float);
					}
					case "STRING":
					{
						return new IcarusConstantExpression(IcarusVariableDataType.String);
					}
					case "VECTOR":
					{
						return new IcarusConstantExpression(IcarusVariableDataType.Vector);
					}
				}
			}
			return null;
		}
	}
}
