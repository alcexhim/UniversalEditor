using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Controls.Multimedia.Audio.Waveform.WaveformTrackList
{
	public partial class WaveformTrackListControl : UserControl
	{
		public WaveformTrackListControl()
		{
			InitializeComponent();
		}

		private class DrawingTools
		{
			public static class Pens
			{
				private static Pen _TrackHighlightPen = new Pen(System.Drawing.Color.FromKnownColor(KnownColor.Highlight), 3);
				public static Pen TrackHighlightPen { get { return _TrackHighlightPen; } }
			}
			public static class Brushes
			{
				private static SolidBrush _TrackBackgroundBrush = new SolidBrush(System.Drawing.Color.FromKnownColor(KnownColor.Control));
				public static SolidBrush TrackBackgroundBrush { get { return _TrackBackgroundBrush; } }
			}
		}

		private int mvarTrackHeight = 152;
		public int TrackHeight { get { return mvarTrackHeight; } }

		private int mvarTrackSpacing = 5;
		public int TrackSpacing { get { return mvarTrackSpacing; } }

		private Track.TrackCollection mvarTracks = new Track.TrackCollection();
		public Track.TrackCollection Tracks { get { return mvarTracks; } }

		private Track mvarFocusedTrack = null;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int x = 0, y = 0, w = base.Width, h = mvarTrackHeight;

			foreach (Track track in mvarTracks)
			{
				Rectangle rect = new Rectangle(x, y, w, h);
				if (track == mvarFocusedTrack)
				{
					e.Graphics.DrawRectangle(DrawingTools.Pens.TrackHighlightPen, rect);
				}

				rect.Width -= 5;
				rect.X += 132;

				rect.Height = mvarTrackHeight + (mvarTrackSpacing * (track.Waveform.Header.ChannelCount - 1));
				int offsetX = 0;
				int k = 0;
				int zoom = 200;
				for (int i = 0; i < track.Waveform.Header.ChannelCount; i++)
				{
					e.Graphics.FillRectangle(DrawingTools.Brushes.TrackBackgroundBrush, rect);

					List<Point> points = new List<Point>();
					for (int j = offsetX; j < track.Waveform.RawSamples.Length / track.Waveform.Header.ChannelCount; j += zoom)
					{
						int z = track.Waveform.RawSamples[j];
						z = (128 - z) + (rect.Bottom / 2);

						points.Add(new Point(rect.X + k, z));
						k += 5;
					}
					e.Graphics.DrawPolygon(Pens.Blue, points.ToArray());
				}
			}
		}
	}

}
