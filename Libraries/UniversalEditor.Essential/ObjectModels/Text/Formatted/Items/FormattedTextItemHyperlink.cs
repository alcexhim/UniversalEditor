//
//  FormattedTextItemHyperlink.cs - represents a FormattedTextItemContainer which causes a hyperlink to appear for all FormattedTextItems it contains
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

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	/// <summary>
	/// Represents a <see cref="FormattedTextItemContainer" /> which causes a hyperlink to appear for all <see cref="FormattedTextItem" />s it contains.
	/// </summary>
	public class FormattedTextItemHyperlink : FormattedTextItemContainer
	{
		/// <summary>
		/// Gets or sets the target URL for the hyperlink.
		/// </summary>
		/// <value>The target URL for the hyperlink.</value>
		public string TargetURL { get; set; } = String.Empty;

		public FormattedTextItemHyperlink(string targetURL = "", string title = "")
		{
			TargetURL = targetURL;
			base.Items.Add(new FormattedTextItemLiteral(title));
		}

		public override object Clone()
		{
			FormattedTextItemHyperlink clone = new FormattedTextItemHyperlink();
			clone.TargetURL = (TargetURL.Clone() as string);
			foreach (FormattedTextItem item in Items)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
			return clone;
		}
	}
}
