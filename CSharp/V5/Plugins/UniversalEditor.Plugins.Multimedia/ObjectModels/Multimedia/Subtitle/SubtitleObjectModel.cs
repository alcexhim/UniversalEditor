using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
	public class SubtitleObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Subtitle";
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarStyles.Clear();
			mvarStyles.Add(mvarDefaultStyle);
		}

		public override void CopyTo(ObjectModel where)
		{
			SubtitleObjectModel clone = (where as SubtitleObjectModel);
			foreach (Style style in mvarStyles)
			{
				clone.Styles.Add(style.Clone() as Style);
			}
			foreach (Event evt in mvarEvents)
			{
				clone.Events.Add(evt.Clone() as Event);
			}
		}

		private static Style mvarDefaultStyle = null;
		static SubtitleObjectModel()
		{
			mvarDefaultStyle = new Style();
			mvarDefaultStyle.FontName = "Arial";
			mvarDefaultStyle.FontSize = 32;
			mvarDefaultStyle.PrimaryColor = Color.FromRGBA(0, 255, 255, 255);
			mvarDefaultStyle.SecondaryColor = Color.FromRGBA(0, 0, 0, 255);
			mvarDefaultStyle.OutlineColor = Color.FromRGBA(0, 0, 0, 0);
			mvarDefaultStyle.BackgroundColor = Color.FromRGBA(0, 0, 0, 0);
			mvarDefaultStyle.Bold = false;
			mvarDefaultStyle.Italic = false;
			mvarDefaultStyle.Underline = false;
			mvarDefaultStyle.Strikethrough = false;
			mvarDefaultStyle.ScaleX = 1.0;
			mvarDefaultStyle.ScaleY = 1.0;
			mvarDefaultStyle.Spacing = 0;
			mvarDefaultStyle.Angle = 0;
			mvarDefaultStyle.BorderStyle = 1;
			mvarDefaultStyle.OutlineWidth = 2;
			mvarDefaultStyle.ShadowWidth = 2;
			mvarDefaultStyle.Alignment = 2;
			mvarDefaultStyle.MarginLeft = 10;
			mvarDefaultStyle.MarginRight = 10;
			mvarDefaultStyle.MarginVertical = 10;
			mvarDefaultStyle.Encoding = 1;
		}

		private Actor.ActorCollection mvarActors = new Actor.ActorCollection();
		public Actor.ActorCollection Actors { get { return mvarActors; } }

		private Style.StyleCollection mvarStyles = new Style.StyleCollection();
		public Style.StyleCollection Styles { get { return mvarStyles; } }

		private Event.EventCollection mvarEvents = new Event.EventCollection();
		public Event.EventCollection Events { get { return mvarEvents; } }
	}
}
