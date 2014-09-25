using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.StyleSheet
{
	public class StyleSheetObjectModel : ObjectModel
	{
		private StyleSheetRule.StyleSheetRuleCollection mvarRules = new StyleSheetRule.StyleSheetRuleCollection();
		public StyleSheetRule.StyleSheetRuleCollection Rules
		{
			get { return mvarRules; }
		}

		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Style sheet";
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
