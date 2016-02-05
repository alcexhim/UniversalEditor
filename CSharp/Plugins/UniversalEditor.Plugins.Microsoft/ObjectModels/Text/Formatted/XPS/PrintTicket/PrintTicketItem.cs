using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket
{
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
