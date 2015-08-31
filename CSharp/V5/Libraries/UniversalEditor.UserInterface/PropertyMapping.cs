using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public class PropertyMapping
	{
		public class PropertyMappingCollection
			: System.Collections.ObjectModel.Collection<PropertyMapping>
		{
		}

		private string mvarControlName = String.Empty;
		public string ControlName { get { return mvarControlName; } set { mvarControlName = value; } }

		private string mvarControlPropertyName = String.Empty;
		public string ControlPropertyName { get { return mvarControlPropertyName; } set { mvarControlPropertyName = value; } }

		private string mvarObjectModelPropertyName = String.Empty;
		public string ObjectModelPropertyName { get { return mvarObjectModelPropertyName; } set { mvarObjectModelPropertyName = value; } }
	}
}
