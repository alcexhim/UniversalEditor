﻿//
//  IcarusChoiceParameter.cs
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
namespace UniversalEditor.ObjectModels.Icarus.Parameters
{
	public class IcarusChoiceParameterValue
	{
		public class IcarusChoiceParameterValueCollection
			: System.Collections.ObjectModel.Collection<IcarusChoiceParameterValue>
		{
		}

		public string Title { get; set; } = null;
		public IcarusExpression Expression { get; set; } = null;

		public IcarusChoiceParameterValue(string title, IcarusExpression expression)
		{
			Title = title;
			Expression = expression;
		}
	}
	public class IcarusChoiceParameter : IcarusGenericParameter
	{
		public IcarusChoiceParameter(string name)
			: base(name)
		{
		}
		public IcarusChoiceParameter(string name, IcarusExpression defaultValue)
			: base(name, defaultValue)
		{
		}
		public IcarusChoiceParameter(string name, string description, IcarusExpression defaultValue = null)
			: base(name, description, defaultValue)
		{
		}
		public IcarusChoiceParameter(string name, IcarusExpression defaultValue, IcarusChoiceParameterValue[] validValues)
			: this(name, null, defaultValue, validValues)
		{
		}
		public IcarusChoiceParameter(string name, string description, IcarusExpression defaultValue, IcarusChoiceParameterValue[] validValues)
			: base(name, description, defaultValue)
		{
		}


	}
}
