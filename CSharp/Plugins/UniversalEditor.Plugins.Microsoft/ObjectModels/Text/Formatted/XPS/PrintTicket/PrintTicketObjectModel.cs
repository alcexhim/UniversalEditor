using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket
{
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
