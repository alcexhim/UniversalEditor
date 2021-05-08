//
//  PrintTicketItem.cs - represents an item in a PrintTicket file in an XML Paper Specification (XPS) document
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

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket
{
	/// <summary>
	/// Represents an item in a PrintTicket file in an XML Paper Specification (XPS) document.
	/// </summary>
	public abstract class PrintTicketItem : ICloneable
	{
		public class PrintTicketItemCollection
			: System.Collections.ObjectModel.Collection<PrintTicketItem>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		protected abstract PrintTicketItem CloneInternal();

		public object Clone()
		{
			PrintTicketItem clone = CloneInternal();
			clone.Name = (mvarName.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarName);
			return sb.ToString();
		}
	}
}
