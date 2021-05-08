//
//  CustomDataFormatFieldCondition.cs - conditional statements used in CDF
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

namespace UniversalEditor
{
	public class CustomDataFormatFieldCondition
	{
		private string mvarVariable = String.Empty;
		public string Variable { get { return mvarVariable; } set { mvarVariable = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private string mvarTrueResult = String.Empty;
		public string TrueResult { get { return mvarTrueResult; } set { mvarTrueResult = value; } }

		private string mvarFalseResult = String.Empty;
		public string FalseResult { get { return mvarFalseResult; } set { mvarFalseResult = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("`");
			sb.Append(mvarVariable);
			sb.Append("` == `");
			sb.Append(mvarValue);
			sb.Append("` ? `");
			sb.Append(mvarTrueResult);
			sb.Append("` : `");
			sb.Append(mvarFalseResult);
			sb.Append("`");
			return sb.ToString();
		}
	}
}
