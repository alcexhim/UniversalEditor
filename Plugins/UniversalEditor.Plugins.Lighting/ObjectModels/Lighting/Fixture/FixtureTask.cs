using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
	public class FixtureTask : ICloneable
	{

		public class FixtureTaskCollection
			: System.Collections.ObjectModel.Collection<FixtureTask>
		{
			public FixtureTask Add(Guid id, string title)
			{
				FixtureTask item = new FixtureTask();
				item.ID = id;
				item.Title = title;
				Add(item);
				return item;
			}

			public FixtureTask this[Guid id]
			{
				get
				{
					foreach (FixtureTask task in this)
					{
						if (task.ID == id) return task;
					}
					return null;
				}
			}
			public bool Contains(Guid id)
			{
				return (this[id] != null);
			}
			public bool Remove(Guid id)
			{
				FixtureTask task = this[id];
				if (task != null)
				{
					Remove(task);
					return true;
				}
				return false;
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private FixtureTaskChannel.FixtureTaskChannelCollection mvarChannels = new FixtureTaskChannel.FixtureTaskChannelCollection();
		public FixtureTaskChannel.FixtureTaskChannelCollection Channels { get { return mvarChannels; } }

		public override string ToString()
		{
			return mvarTitle;
		}

		public object Clone()
		{
			FixtureTask clone = new FixtureTask();
			clone.ID = mvarID;
			clone.Title = (mvarTitle.Clone() as string);
			foreach (FixtureTaskChannel channel in mvarChannels)
			{
				clone.Channels.Add(channel.Clone() as FixtureTaskChannel);
			}
			return clone;
		}
	}
}
