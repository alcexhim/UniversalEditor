using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	/// <summary>
	/// Represents an endpoint (<see cref="DataFormat"/>/<see cref="Accessor"/> pair) that
	/// defines how and where data is transferred.
	/// </summary>
	public class Endpoint
	{
		public class EndpointCollection
			: System.Collections.ObjectModel.Collection<Endpoint>
		{
		}

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }
	}
}
