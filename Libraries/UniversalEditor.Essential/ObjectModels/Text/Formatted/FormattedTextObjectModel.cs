//
//  FormattedTextObjectModel.cs - provides an ObjectModel for manipulating formatted text (e.g. PDF, Rich Text Format, DOC, XPS, DOCX, ODT)
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

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating formatted text (e.g. PDF, Rich Text Format, DOC, XPS, DOCX, ODT).
	/// </summary>
	public class FormattedTextObjectModel : ObjectModel, IFormattedTextItemParent
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Text", "Formatted text document" };
			}
			return _omr;
		}

		/// <summary>
		/// Gets or sets the default font for the <see cref="FormattedTextObjectModel" /> document.
		/// </summary>
		/// <value>The default font for the <see cref="FormattedTextObjectModel" /> document.</value>
		public FormattedTextFont DefaultFont { get; set; } = null;
		/// <summary>
		/// Gets a collection of <see cref="FormattedTextFont" /> instances representing the fonts embedded in this
		/// <see cref="FormattedTextObjectModel" /> document.
		/// </summary>
		/// <value>The fonts embedded in this <see cref="FormattedTextObjectModel" /> document.</value>
		public FormattedTextFont.FormattedTextFontCollection Fonts { get; } = new FormattedTextFont.FormattedTextFontCollection();
		/// <summary>
		/// Gets a collection of <see cref="FormattedTextItem" /> instances representing the formatting commands executed in this
		/// <see cref="FormattedTextObjectModel" /> document.
		/// </summary>
		/// <value>The formatting commands executed in this <see cref="FormattedTextObjectModel" /> document.</value>
		public FormattedTextItem.FormattedTextItemCollection Items { get; } = new FormattedTextItem.FormattedTextItemCollection();

		public override void Clear()
		{
			Fonts.Clear();
			Items.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FormattedTextObjectModel clone = (where as FormattedTextObjectModel);
			foreach (FormattedTextFont font in Fonts)
			{
				clone.Fonts.Add(font.Clone() as FormattedTextFont);
			}
			foreach (FormattedTextItem item in Items)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
		}
	}
}
