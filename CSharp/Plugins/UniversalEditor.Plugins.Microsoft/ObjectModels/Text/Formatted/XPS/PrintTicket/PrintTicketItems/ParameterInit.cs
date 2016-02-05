using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket.PrintTicketItems
{
	public class ParameterInit : PrintTicketItem
	{
		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		protected override PrintTicketItem CloneInternal()
		{
			ParameterInit clone = new ParameterInit();
			clone.Value = mvarValue;
			return clone;
		}
	}
}
