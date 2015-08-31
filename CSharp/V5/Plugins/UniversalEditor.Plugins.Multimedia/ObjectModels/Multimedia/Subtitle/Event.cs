using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
    public class Event : ICloneable
    {
        public class EventCollection
            : System.Collections.ObjectModel.Collection<Event>
        {
        }

        private Actor mvarActor = null;
        public Actor Actor { get { return mvarActor; } set { mvarActor = value; } }

        private Style mvarStyle = null;
        public Style Style { get { return mvarStyle; } set { mvarStyle = value; } }

        private DateTime mvarStartTimestamp = new DateTime();
        public DateTime StartTimestamp { get { return mvarStartTimestamp; } set { mvarStartTimestamp = value; } }

        private DateTime mvarEndTimestamp = new DateTime();
        public DateTime EndTimestamp { get { return mvarEndTimestamp; } set { mvarEndTimestamp = value; } }

        private PositionVector2 mvarPosition = PositionVector2.Empty;
        public PositionVector2 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private string mvarText = String.Empty;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        public object Clone()
        {
            Event clone = new Event();
            clone.Actor = mvarActor;
            clone.EndTimestamp = mvarEndTimestamp;
            clone.Position = (PositionVector2)(mvarPosition.Clone());
            clone.StartTimestamp = mvarStartTimestamp;
            clone.Style = mvarStyle;
            clone.Text = (mvarText.Clone() as string);
            return clone;
        }
    }
}
