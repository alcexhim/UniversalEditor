//
//  IcarusGenericParameter.cs - represents an ICARUS parameter
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

namespace UniversalEditor.ObjectModels.Icarus.Parameters
{
	/// <summary>
	/// Represents an ICARUS parameter.
	/// </summary>
	public class IcarusGenericParameter : IcarusParameter
	{
		public IcarusGenericParameter(string name)
			: base(name)
		{
		}
		public IcarusGenericParameter(string name, IcarusExpression defaultValue)
			: base(name, defaultValue)
		{
		}
		public IcarusGenericParameter(string name, string description, IcarusExpression defaultValue = null)
			: base(name, description, defaultValue)
		{
		}

		public override object Clone()
		{
			IcarusGenericParameter clone = new IcarusGenericParameter(Name.Clone() as string);
			clone.Description = Description?.Clone() as string;
			clone.EnumerationName = EnumerationName?.Clone() as string;
			clone.ReadOnly = ReadOnly;
			clone.Value = Value.Clone() as IcarusExpression;

			clone.AutoCompleteCommandType = AutoCompleteCommandType;
			clone.AutoCompleteParameterIndex = AutoCompleteParameterIndex;
			return clone;
		}
	}
}
