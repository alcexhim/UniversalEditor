using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Lighting.Fixture;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
	public abstract class TrackEntryCommand : ICloneable
	{
		public class TrackEntryCommandCollection
			: System.Collections.ObjectModel.Collection<TrackEntryCommand>
		{
		}

		public abstract object Clone();
	}
	public class TrackEntryChannelCommand : TrackEntryCommand
	{
		private Channel mvarChannel = null;
		public Channel Channel { get { return mvarChannel; } set { mvarChannel = value; } }

		private byte mvarStartValue = 0;
		public byte StartValue { get { return mvarStartValue; } set { mvarStartValue = value; } }

		private byte mvarEndValue = 0;
		public byte EndValue { get { return mvarEndValue; } set { mvarEndValue = value; } }

		// Interpolation mode?

		public override object Clone()
		{
			TrackEntryChannelCommand clone = new TrackEntryChannelCommand();
			clone.Channel = (Channel?.Clone() as Channel);
			clone.StartValue = StartValue;
			clone.EndValue = EndValue;
			return clone;
		}
	}
}
