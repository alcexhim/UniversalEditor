//
//  StyleSheetObjectModel.cs - provides an ObjectModel for manipulating Cascading Style Sheets
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

using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Web.StyleSheet
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Cascading Style Sheets.
	/// </summary>
	public class StyleSheetObjectModel : ObjectModel
	{
		private StyleSheetRule.StyleSheetRuleCollection mvarRules = new StyleSheetRule.StyleSheetRuleCollection();
		public StyleSheetRule.StyleSheetRuleCollection Rules
		{
			get { return mvarRules; }
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Software development", "Web", "Style sheet" };
				_omr.Description = "A cascading style sheet used for rich HTML style definitions.";
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarRules.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			StyleSheetObjectModel clone = (where as StyleSheetObjectModel);
			foreach (StyleSheetRule rule in mvarRules)
			{
				clone.Rules.Add(rule.Clone() as StyleSheetRule);
			}
		}

		public StyleSheetRule[] GetRulesForSelector(string selector)
		{
			List<StyleSheetRule> rules = new List<StyleSheetRule>();
			foreach (StyleSheetRule rule in mvarRules)
			{
				if (rule.MatchesSelector(selector))
				{
					rules.Add(rule);
				}
			}
			return rules.ToArray();
		}
	}
}
