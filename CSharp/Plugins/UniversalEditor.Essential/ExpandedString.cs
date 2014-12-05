using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class ExpandedString
	{
		public static readonly ExpandedString Empty = new ExpandedString();

		public ExpandedString()
		{

		}
		public ExpandedString(params ExpandedStringSegment[] segments)
		{
			foreach (ExpandedStringSegment segment in segments)
			{
				mvarSegments.Add(segment);
			}
		}

		private ExpandedStringSegment.ExpandedStringSegmentCollection mvarSegments = new ExpandedStringSegment.ExpandedStringSegmentCollection();
		public ExpandedStringSegment.ExpandedStringSegmentCollection Segments { get { return mvarSegments; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (ExpandedStringSegment segment in mvarSegments)
			{
				sb.Append(segment.ToString());
			}
			return sb.ToString();
		}
	}
	public abstract class ExpandedStringSegment
	{
		public class ExpandedStringSegmentCollection
			: System.Collections.ObjectModel.Collection<ExpandedStringSegment>
		{

		}

		private static readonly Dictionary<string, string> _empty = new Dictionary<string, string>();

		public abstract string ToString(Dictionary<string, string> variables);
		public override string ToString()
		{
			return ToString(_empty);
		}
	}
	public class ExpandedStringSegmentLiteral : ExpandedStringSegment
	{
		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString(Dictionary<string, string> variables)
		{
			return mvarValue;
		}
	}
	/// <summary>
	/// Represents a string segment whose value is populated by a variable.
	/// </summary>
	public class ExpandedStringSegmentVariable : ExpandedStringSegment
	{
		private string mvarVariableName = String.Empty;
		/// <summary>
		/// The name of the variable in the variables dictionary from which to retrieve the value for this <see cref="ExpandedStringSegment" />.
		/// </summary>
		public string VariableName { get { return mvarVariableName; } set { mvarVariableName = value; } }

		/// <summary>
		/// Creates a new instance of <see cref="ExpandedStringSegmentVariable" /> with the given variable name.
		/// </summary>
		/// <param name="variableName">The name of the variable in the variables dictionary from which to retrieve the value for this <see cref="ExpandedStringSegment" />.</param>
		public ExpandedStringSegmentVariable(string variableName)
		{
			mvarVariableName = variableName;
		}

		/// <summary>
		/// Returns the value of this <see cref="ExpandedStringSegment" /> with the given variables.
		/// </summary>
		/// <param name="variables">The variables to pass into the <see cref="ExpandedStringSegment" />.</param>
		/// <returns>A <see cref="String" /> that contains the value of this <see cref="ExpandedStringSegment" />.</returns>
		public override string ToString(Dictionary<string, string> variables)
		{
			return variables[mvarVariableName];
		}
	}
}
