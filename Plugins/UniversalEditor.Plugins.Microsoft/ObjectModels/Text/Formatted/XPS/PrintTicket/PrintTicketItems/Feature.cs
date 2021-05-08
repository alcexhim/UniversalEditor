//
//  Feature.cs - support for PrintTicket features, options, and ScoredProperties
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

using System;
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
