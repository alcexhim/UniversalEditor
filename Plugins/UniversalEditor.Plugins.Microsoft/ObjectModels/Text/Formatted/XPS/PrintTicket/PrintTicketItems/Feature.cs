using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket.PrintTicketItems
{
	public class ScoredProperty : ICloneable
	{
		public class ScoredPropertyCollection
			: System.Collections.ObjectModel.Collection<ScoredProperty>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ScoredProperty clone = new ScoredProperty();
			clone.Name = (mvarName.Clone() as string);
			clone.Value = mvarValue;
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarName);
			sb.Append(" = '");
			sb.Append(mvarValue.ToString());
			sb.Append("'");
			return sb.ToString();
		}
	}
	public class FeatureOption : ICloneable
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private ScoredProperty.ScoredPropertyCollection mvarScoredProperties = new ScoredProperty.ScoredPropertyCollection();
		public ScoredProperty.ScoredPropertyCollection ScoredProperties { get { return mvarScoredProperties; } }

		public object Clone()
		{
			FeatureOption clone = new FeatureOption();
			clone.Name = (mvarName.Clone() as string);
			foreach (ScoredProperty item in mvarScoredProperties)
			{
				clone.ScoredProperties.Add(item.Clone() as ScoredProperty);
			}
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarName);
			return sb.ToString();
		}
	}
	public class Feature : PrintTicketItem
	{
		private FeatureOption mvarOption = null;
		public FeatureOption Option { get { return mvarOption; } set { mvarOption = value; } }

		protected override PrintTicketItem CloneInternal()
		{
			Feature clone = new Feature();
			clone.Option = (mvarOption.Clone() as FeatureOption);
			return clone;
		}
	}
}
