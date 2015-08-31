using System;

namespace UniversalEditor
{
	/// <summary>
	/// The type of comparison to use with the conditional statement.
	/// </summary>
	[Flags()]
	public enum ConditionComparison
	{
		/// <summary>
		/// Returns true if the two values are equal by value.
		/// </summary>
		Equal = 1,
		/// <summary>
		/// Returns true if the two values are equal by reference (or by value if they are value types).
		/// </summary>
		ReferenceEqual = 2,
		/// <summary>
		/// Returns true if the first value is greater than the second value.
		/// </summary>
		GreaterThan = 4,
		/// <summary>
		/// Returns true if the first value is less than the second value.
		/// </summary>
		LessThan = 8,
		/// <summary>
		/// Negates the conditional comparison.
		/// </summary>
		Not = 16
	}
	
	/// <summary>
	/// The type of combination applied to a series of conditional statements.
	/// </summary>
	public enum ConditionCombination
	{
		/// <summary>
		/// Returns true if all of the conditional statements in this group are true.
		/// </summary>
		And,
		/// <summary>
		/// Returns true if at least one of the conditional statements in this group are true.
		/// </summary>
		Or,
		Xor
	}
	
	/// <summary>
	/// Defines the minimum functionality required to implement a conditional statement (either a
	/// <see cref="Condition" /> itself or a <see cref="ConditionGroup" /> of multiple
	/// <see cref="Condition" />s.
	/// </summary>
	public interface IConditionalStatement
	{
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		bool Test(params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues);
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		bool Test(System.Collections.Generic.Dictionary<string, object> propertyValues);
		/// <summary>
		/// Evaluates the conditional statement based on the given criterion.
		/// </summary>
		/// <param name="value">The value against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		bool Test (object value);
	}

	/// <summary>
	/// A collection of <see cref="IConditionalStatement" />s
	/// </summary>
	public class ConditionalStatementCollection
		: System.Collections.ObjectModel.Collection<IConditionalStatement>
	{
		
	}

	/// <summary>
	/// A group of <see cref="IConditionalStatement" />s joined by a <see cref="ConditionalCombination" />.
	/// </summary>
	public class ConditionGroup : IConditionalStatement
	{
		/// <summary>
		/// Creates a new <see cref="ConditionGroup" /> with no conditional statements specified and a
		/// default <see cref="ConditionalCombination" /> of <see cref="ConditionalCombination.And" />.
		/// </summary>
		public ConditionGroup()
		{
			// I know it's initialized to this but I'm doing it here for clarity's sake (and because
			// it's documented here... if you change it, make sure to update the documentation! don't
			// rely on the field initializer)
			mvarCombination = ConditionCombination.And;
		}
		/// <summary>
		/// Creates a new <see cref="ConditionGroup" /> with the specified
		/// <see cref="ConditionCombination" /> and <see cref="IConditionalStatement" />s.
		/// </summary>
		/// <param name="combination">The <see cref="ConditionCombination" /> used to join <see cref="IConditionalStatement" />s when testing this <see cref="ConditionGroup" />.</param>
		/// <param name="statements">The <see cref="Condition" />s and <see cref="ConditionGroup" />s that are part of this <see cref="ConditionGroup" />.</param>
		public ConditionGroup(ConditionCombination combination, params IConditionalStatement[] statements)
		{
			mvarCombination = combination;
			for (int i = 0; i < statements.Length; i++)
			{
				mvarConditions.Add (statements[i]);
			}
		}
		
		private ConditionalStatementCollection mvarConditions = new ConditionalStatementCollection();
		/// <summary>
		/// Gets all <see cref="IConditionalStatement" />s in this <see cref="ConditionGroup" />.
		/// </summary>
		public ConditionalStatementCollection Conditions
		{
			get { return mvarConditions; }
		}
		
		private ConditionCombination mvarCombination = ConditionCombination.And;
		/// <summary>
		/// The type of combination used to join the <see cref="Condition" />s in this
		/// <see cref="ConditionGroup" />.
		/// </summary>
		public ConditionCombination Combination
		{
			get { return mvarCombination; }
			set { mvarCombination = value; }
		}

		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
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
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
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

		/// <summary>
		/// Evaluates the conditional statement based on the given criterion.
		/// </summary>
		/// <param name="value">The value against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
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
		/// <summary>
		/// The name of the property against which to test when the <see cref="Test" /> method is called
		/// passing in a property reference.
		/// </summary>
		public string PropertyName
		{
			get { return mvarPropertyName; }
			set { mvarPropertyName = value; }
		}
		
		private ConditionComparison mvarComparison = ConditionComparison.Equal;
		/// <summary>
		/// The type of comparison to use when testing this <see cref="Condition" />.
		/// </summary>
		public ConditionComparison Comparison
		{
			get { return mvarComparison; }
			set { mvarComparison = value; }
		}
		
		private object mvarValue = null;
		/// <summary>
		/// The value against which to test when the <see cref="Test" /> method is called.
		/// </summary>
		public object Value
		{
			get { return mvarValue; }
			set { mvarValue = value; }
		}
		
		/// <summary>
		/// Creates a <see cref="Condition" /> with the specified property name, comparison, and value.
		/// </summary>
		/// <param name="propertyName">The name of the property against which to test when the <see cref="Test" /> method is called passing in a property reference.</param>
		/// <param name="comparison">The type of comparison to use.</param>
		/// <param name="value">The value against which to test when the <see cref="Test" /> method is called.</param>
		public Condition(string propertyName, ConditionComparison comparison, object value)
		{
			mvarPropertyName = propertyName;
			mvarComparison = comparison;
			mvarValue = value;
		}

		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test(params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues)
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
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test(System.Collections.Generic.Dictionary<string, object> propertyValues)
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
		/// <summary>
		/// Evaluates the conditional statement based on the given criterion.
		/// </summary>
		/// <param name="value">The value against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test (object propertyValue)
		{
			// would you like meatballs with your spaghetti code?
			bool returnValue = false;

			if ((mvarComparison & ConditionComparison.Equal) == ConditionComparison.Equal)
			{
				if (propertyValue == null)
				{
					// our comparison object is null, so we can't .Equals it
					// just do regular == with the constant null in that case
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
					// our comparison object is null, so we can't .Equals it
					// just do regular == with the constant null in that case
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
					// can ANYTHING ever be greater than or less than null?
					returnValue |= false;
				}
				else
				{
					// we need to directly invoke IComparable.CompareTo here since we can't (usually)
					// do > or < on objects... not sure what to do if the object doesn't implement
					// IComparable though
					returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) > 0);
				}
			}
			if (((mvarComparison & ConditionComparison.LessThan) == ConditionComparison.LessThan) && (propertyValue is IComparable))
			{
				if (propertyValue == null)
				{
					// can ANYTHING ever be greater than or less than null?
					returnValue |= false;
				}
				else
				{
					// we need to directly invoke IComparable.CompareTo here since we can't (usually)
					// do > or < on objects... not sure what to do if the object doesn't implement
					// IComparable though
					returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) < 0);
				}
			}
			if ((mvarComparison & ConditionComparison.Not) == ConditionComparison.Not)
			{
				// we have a Not in there, so negate our return value
				returnValue = !returnValue;
			}

			// did you have as much fun reading this as I did writing it?
			bool from_hell = returnValue;
			return from_hell;
		}
	}
}
