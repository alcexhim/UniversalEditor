using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public interface IContactLabelContainer
	{
		ContactLabel.ContactLabelCollection Labels { get; }
	}
}
