//
//  PrintTicketObjectModel.cs - provides an ObjectModel for manipulating PrintTicket files in an XML Paper Specification (XPS) document
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

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating PrintTicket files in an XML Paper Specification (XPS) document.
	/// </summary>
	public class PrintTicketObjectModel : ObjectModel
	{
		private PrintTicketItem.PrintTicketItemCollection mvarItems = new PrintTicketItem.PrintTicketItemCollection();
		public PrintTicketItem.PrintTicketItemCollection Items { get { return mvarItems; } }

		public override void Clear()
		{
			mvarItems.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			PrintTicketObjectModel clone = (where as PrintTicketObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (PrintTicketItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as PrintTicketItem);
			}
		}
	}
}
