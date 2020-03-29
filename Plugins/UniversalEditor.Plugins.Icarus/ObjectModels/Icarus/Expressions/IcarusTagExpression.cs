//
//  IcarusTagExpression.cs
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
namespace UniversalEditor.ObjectModels.Icarus.Expressions
{
	public class IcarusTagExpression : IcarusFunctionExpression
	{
		public string TargetName { get; set; } = null;
		public IcarusTagType Type { get; set; } = IcarusTagType.Origin;

		protected override string FunctionName => "tag";
		protected override string[] Parameters => new string[] { TargetName, Type.ToString().ToUpper() };

		public IcarusTagExpression(string targetName, IcarusTagType type)
		{
			TargetName = targetName;
			Type = type;
		}

		protected override bool GetValueInternal(ref object value)
		{
			value = String.Format("$tag(\"{0}\", {1}$", TargetName, Type.ToString().ToUpper());
			return true;
		}

		public override object Clone()
		{
			IcarusTagExpression clone = new IcarusTagExpression(TargetName.Clone() as string, Type);
			return clone;
		}
	}
}
