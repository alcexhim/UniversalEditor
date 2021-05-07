using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Lighting.Fixture;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
	public abstract class TrackEntryCommand
	{
		public class TrackEntryCommandCollection
			: System.Collections.ObjectModel.Collection<TrackEntryCommand>
		{
		}

	}
	public class TrackEntryChannelCommand : TrackEntryCommand
	{
		private Channel mvarChannel = null;
		public Channel Channel { get { return mvarChannel; } }

		private byte mvarStartValue = 0;
		public byte StartValue { get { return mvarStartValue; } set { mvarStartValue = value; } }

		private byte mvarEndValue = 0;
		public byte EndValue { get { return mvarEndValue; } set { mvarEndValue = value; } }

		// Interpolation mode?
	}
}
