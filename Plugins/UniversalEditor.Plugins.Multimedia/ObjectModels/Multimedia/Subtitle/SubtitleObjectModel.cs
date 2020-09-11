//
//  SubtitleObjectModel.cs - provides an ObjectModel for manipulating subtitle files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating subtitle files.
	/// </summary>
	public class SubtitleObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "Subtitle" };
			}
			return _omr;
		}

		public override void Clear()
		{
			Styles.Clear();
			Styles.Add(mvarDefaultStyle);
		}

		public override void CopyTo(ObjectModel where)
		{
			SubtitleObjectModel clone = (where as SubtitleObjectModel);
			foreach (Style style in Styles)
			{
				clone.Styles.Add(style.Clone() as Style);
			}
			foreach (Event evt in Events)
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
			mvarDefaultStyle.PrimaryColor = Color.FromRGBAInt32(0, 255, 255, 255);
			mvarDefaultStyle.SecondaryColor = Color.FromRGBAInt32(0, 0, 0, 255);
			mvarDefaultStyle.OutlineColor = Color.FromRGBAInt32(0, 0, 0, 0);
			mvarDefaultStyle.BackgroundColor = Color.FromRGBAInt32(0, 0, 0, 0);
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
		public Actor.ActorCollection Actors { get; } = new Actor.ActorCollection();
		public Style.StyleCollection Styles { get; } = new Style.StyleCollection();
		public Event.EventCollection Events { get; } = new Event.EventCollection();
	}
}
