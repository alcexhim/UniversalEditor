//
//  IcarusRandomExpression.cs - represents an ICARUS "random()" function expression
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

namespace UniversalEditor.ObjectModels.Icarus.Expressions
{
	/// <summary>
	/// Represents an ICARUS "random()" function expression.
	/// </summary>
	public class IcarusRandomExpression : IcarusFunctionExpression
	{
		public IcarusRandomExpression(double min, double max)
		{
			Minimum = min;
			Maximum = max;
		}

		public double Minimum { get; set; } = 0.0;
		public double Maximum { get; set; } = 1.0;

		protected override string FunctionName => "random";
		protected override string[] Parameters => new string[] { Minimum.ToString(), Maximum.ToString() };

		private Random rnd = new Random();
		private double? _val = null;

		protected override bool GetValueInternal(ref object value)
		{
			if (_val == null)
			{
				_val = rnd.NextDouble();
			}
			value = (float)_val;
			return true;
		}

		public override object Clone()
		{
			IcarusRandomExpression clone = new IcarusRandomExpression(Minimum, Maximum);
			return clone;
		}
	}
}
