//
//  IcarusFunctionExpression.cs - the base class from which all ICARUS function expressions derive
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
	/// The base class from which all ICARUS function expressions derive.
	/// </summary>
	public abstract class IcarusFunctionExpression : IcarusExpression
	{
		protected abstract string FunctionName { get; }
		protected abstract string[] Parameters { get; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(FunctionName);
			sb.Append("( ");
			for (int i = 0; i < Parameters.Length; i++)
			{
				sb.Append(Parameters[i]);
				if (i < Parameters.Length - 1)
					sb.Append(", ");
			}
			sb.Append(" )");
			return sb.ToString();
			{
			}
		}
	}

}
