using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public interface IContactComplexType
	{
		bool IsEmpty { get; set; }
		Guid ElementID { get; set; }
		DateTime? ModificationDate { get; set; }
	}
}
