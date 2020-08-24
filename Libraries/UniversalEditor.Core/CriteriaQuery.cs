//
//  CriteriaQuery.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.Logic.Conditional;

namespace UniversalEditor
{
	public class CriteriaQuery
	{
		public Criterion.CriterionCollection Criteria { get; } = new Criterion.CriterionCollection();

		public bool Check(CriteriaProperty property, object value)
		{
			bool ret = false;
			for (int i = 0; i < Criteria.Count; i++)
			{
				if (Criteria[i].Property == property)
				{
					if ((Criteria[i].Comparison & ConditionComparison.Equal) == ConditionComparison.Equal)
					{
						ret |= Criteria[i].Value.ToString().Equals(value.ToString());
					}
					if (Criteria[i].Property.DataType == typeof(string))
					{
						if ((Criteria[i].Comparison & ConditionComparison.StartsWith) == ConditionComparison.StartsWith)
						{
							ret |= Criteria[i].Value.ToString().StartsWith(value.ToString());
						}
						if ((Criteria[i].Comparison & ConditionComparison.EndsWith) == ConditionComparison.EndsWith)
						{
							ret |= Criteria[i].Value.ToString().EndsWith(value.ToString());
						}
						if ((Criteria[i].Comparison & ConditionComparison.Contains) == ConditionComparison.Contains)
						{
							ret |= Criteria[i].Value.ToString().Contains(value.ToString());
						}
					}
				}
			}
			return ret;
		}
	}
}
