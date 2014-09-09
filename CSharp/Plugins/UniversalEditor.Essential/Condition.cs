using System;
namespace UniversalEditor
{
	[Flags()]
	public enum ConditionComparison
	{
		Equal = 1,
		ReferenceEqual = 2,
		GreaterThan = 4,
		LessThan = 8,
		Not = 16
	}
	
	public enum ConditionCombination
	{
		And,
		Or,
		Xor
	}
	
	public interface IConditionalStatement
	{
		bool Test (params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues);
		bool Test (System.Collections.Generic.Dictionary<string, object> propertyValues);
		bool Test (object value);
	}
	public class ConditionalStatementCollection
		: System.Collections.ObjectModel.Collection<IConditionalStatement>
	{
		
	}
	public class ConditionGroup : IConditionalStatement
	{
		public ConditionGroup ()
		{
		}
		public ConditionGroup (ConditionCombination combination, params IConditionalStatement[] statements)
		{
			mvarCombination = combination;
			for (int i = 0; i < statements.Length; i++)
			{
				mvarConditions.Add (statements[i]);
			}
		}
		
		private ConditionalStatementCollection mvarConditions = new ConditionalStatementCollection();
		public ConditionalStatementCollection Conditions
		{
			get { return mvarConditions; }
		}
		
		private ConditionCombination mvarCombination = ConditionCombination.And;
		public ConditionCombination Combination
		{
			get { return mvarCombination; }
			set { mvarCombination = value; }
		}
		
		public bool Test (params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues)
		{
			bool retval = false;
			if (mvarCombination == ConditionCombination.And)
			{
				retval = true;
			}
			for (int i = 0; i < mvarConditions.Count; i++)
			{
				switch (mvarCombination)
				{
				case ConditionCombination.And:
					retval &= mvarConditions[i].Test (propertyValues);
					break;
				case ConditionCombination.Or:
					retval |= mvarConditions[i].Test (propertyValues);
					break;
				case ConditionCombination.Xor:
					retval ^= mvarConditions[i].Test (propertyValues);
					break;
				}
			}
			return retval;
		}
		public bool Test(System.Collections.Generic.Dictionary<string, object> propertyValues)
		{
			bool retval = false;
			if (mvarCombination == ConditionCombination.And)
			{
				retval = true;
			}
			for (int i = 0; i < mvarConditions.Count; i++)
			{
				switch (mvarCombination)
				{
					case ConditionCombination.And:
						retval &= mvarConditions[i].Test(propertyValues);
						break;
					case ConditionCombination.Or:
						retval |= mvarConditions[i].Test(propertyValues);
						break;
					case ConditionCombination.Xor:
						retval ^= mvarConditions[i].Test(propertyValues);
						break;
				}
			}
			return retval;
		}
		public bool Test (object value)
		{
			bool retval = true;
			
			for (int i = 0; i < mvarConditions.Count; i++)
			{
				switch (mvarCombination)
				{
				case ConditionCombination.And:
					retval &= mvarConditions[i].Test (value);
					break;
				case ConditionCombination.Or:
					retval |= mvarConditions[i].Test (value);
					break;
				case ConditionCombination.Xor:
					retval ^= mvarConditions[i].Test (value);
					break;
				}
			}
			return retval;
		}
	}
	public class Condition : IConditionalStatement
	{
		private string mvarPropertyName = String.Empty;
		public string PropertyName
		{
			get { return mvarPropertyName; }
			set { mvarPropertyName = value; }
		}
		
		private ConditionComparison mvarComparison = ConditionComparison.Equal;
		public ConditionComparison Comparison
		{
			get { return mvarComparison; }
			set { mvarComparison = value; }
		}
		
		private object mvarValue = null;
		public object Value
		{
			get { return mvarValue; }
			set { mvarValue = value; }
		}
		
		public Condition (string propertyName, ConditionComparison comparison, object value)
		{
			mvarPropertyName = propertyName;
			mvarComparison = comparison;
			mvarValue = value;
		}
		
		public bool Test (params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues)
		{
			bool retval = true;
			foreach (System.Collections.Generic.KeyValuePair<string, object> propertyValue in propertyValues)
			{
				if (propertyValue.Key == mvarPropertyName)
				{
					retval &= Test(propertyValue.Value);
				}
			}
			return retval;
		}
		public bool Test (System.Collections.Generic.Dictionary<string, object> propertyValues)
		{
			bool retval = true;
			foreach (System.Collections.Generic.KeyValuePair<string, object> propertyValue in propertyValues)
			{
				if (propertyValue.Key == mvarPropertyName)
				{
					retval &= Test(propertyValue.Value);
				}
			}
			return retval;
		}
		public bool Test (object propertyValue)
		{
			bool returnValue = false;
			if ((mvarComparison & ConditionComparison.Equal) == ConditionComparison.Equal)
			{
				if (propertyValue == null)
				{
					returnValue |= (mvarValue == null);
				}
				else
				{
					returnValue |= (propertyValue.Equals(mvarValue));
				}
			}
			if ((mvarComparison & ConditionComparison.ReferenceEqual) == ConditionComparison.ReferenceEqual)
			{
				if (propertyValue == null)
				{
					returnValue |= (mvarValue == null);
				}
				else
				{
					returnValue |= (propertyValue == mvarValue);
				}
			}
			if (((mvarComparison & ConditionComparison.GreaterThan) == ConditionComparison.GreaterThan) && (propertyValue is IComparable))
			{
				if (propertyValue == null)
				{
					returnValue |= false;
				}
				else
				{
					returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) > 0);
				}
			}
			if (((mvarComparison & ConditionComparison.LessThan) == ConditionComparison.LessThan) && (propertyValue is IComparable))
			{
				if (propertyValue == null)
				{
					returnValue |= false;
				}
				else
				{
					returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) < 0);
				}
			}
			if ((mvarComparison & ConditionComparison.Not) == ConditionComparison.Not)
			{
				returnValue = !returnValue;
			}
			return returnValue;
		}
	}
}

