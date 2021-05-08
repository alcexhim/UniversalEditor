using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	class MfcSerialisableAttribute : Attribute
	{
		private string _serialisedName;

		/// <summary>
		/// Marks a type as serialisable with an explicit name.
		/// </summary>
		public MfcSerialisableAttribute(string serialisedName)
		{
			this._serialisedName = serialisedName;
		}

		/// <summary>
		/// Marks a type as serialisable but with no name.
		/// Such types cannot be automatically deserialised;
		/// you must explicitly name the type for each serialise call.
		/// </summary>
		public MfcSerialisableAttribute()
		{
			_serialisedName = string.Format("Anonymous_{0}", Guid.NewGuid().ToString());
		}

		public string SerialisedName
		{
			get
			{
				return _serialisedName;
			}
		}
	}
}
