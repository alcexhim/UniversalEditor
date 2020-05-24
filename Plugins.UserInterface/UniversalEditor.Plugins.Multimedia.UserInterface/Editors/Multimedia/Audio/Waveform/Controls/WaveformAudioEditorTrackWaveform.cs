//
//  WaveformAudioEditorTrackWaveform.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

using System;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Editors.Multimedia.Audio.Waveform.Controls
{
	public class WaveformAudioEditorTrackWaveform : CustomControl
	{
		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			ScrollBounds = new MBS.Framework.Drawing.Dimension2D(4096, 0);
			Cursor = Cursors.Text;
		}

		private static bool preventScroll = false;

		private int _SelectionStart = 0;
		public int SelectionStart { get { return _SelectionStart; } set { _SelectionStart = value; Refresh(); } }
		private int _SelectionLength = 0;
		public int SelectionLength { get { return _SelectionLength; } set { _SelectionLength = value; Refresh(); } }

		public double ScaleFactorY { get; set; } = 0.5;// 1.0;

		private int origStart = 0;
		private bool appendSelection = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if ((e.ModifierKeys & MBS.Framework.UserInterface.Input.Keyboard.KeyboardModifierKey.Control) == MBS.Framework.UserInterface.Input.Keyboard.KeyboardModifierKey.Control)
			{
				appendSelection = true;
			}
			else
			{
				(Parent.Parent as WaveformAudioEditor).Selections.Clear();
			}

			origStart = (int)e.X;
			_SelectionStart = (int)e.X;
			_SelectionLength = 0;
			Refresh();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Buttons == MouseButtons.Primary)
			{
				if ((int)(e.X - origStart) < 0)
				{
					_SelectionStart = origStart + (int)(e.X - origStart);
					_SelectionLength = (int)(origStart - e.X);
				}
				else
				{
					_SelectionStart = origStart;
					_SelectionLength = (int)(e.X - _SelectionStart);
				}
				Refresh();
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			WaveformAudioEditorSelection sel = new WaveformAudioEditorSelection((Parent.Parent as WaveformAudioEditor), SelectionStart, SelectionLength);
			if (!appendSelection)
			{
				(Parent.Parent as WaveformAudioEditor).Selections.Clear();
			}
			(Parent.Parent as WaveformAudioEditor).Selections.Add(sel);
		}

		private const int WAVEFORM_MIDPOINT = 128;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			WaveformAudioEditorTrack track = (Parent as WaveformAudioEditorTrack);
			WaveformAudioObjectModel wave = track.ObjectModel;

			ScrollBounds = new MBS.Framework.Drawing.Dimension2D(wave.RawSamples.Length, 0);

			Vector2D lastPoint = new Vector2D(0, WAVEFORM_MIDPOINT);
			Pen pen = new Pen(SystemColors.HighlightBackground);
			Pen pen2 = new Pen(SystemColors.HighlightForeground);

			// draw the existing selections backgrounds
			WaveformAudioEditor ed = (Parent.Parent as WaveformAudioEditor);
			for (int i = 0; i < ed.Selections.Count; i++)
			{
				if ((ed.Selections[i] as WaveformAudioEditorSelection).SelectionLength > 0)
				{
					e.Graphics.FillRectangle(new SolidBrush(SystemColors.HighlightBackground), new Rectangle((ed.Selections[i] as WaveformAudioEditorSelection).SelectionStart, 0, (ed.Selections[i] as WaveformAudioEditorSelection).SelectionLength, Size.Height));
				}
			}

			// draw the currently active selection background
			if (SelectionLength > 0)
			{
				e.Graphics.FillRectangle(new SolidBrush(SystemColors.HighlightBackground), new Rectangle(SelectionStart, 0, SelectionLength, Size.Height));
			}

			// draw the midpoint line
			e.Graphics.DrawLine(new Pen(Colors.Black), 0, WAVEFORM_MIDPOINT * ScaleFactorY, HorizontalAdjustment.Value + Size.Width, WAVEFORM_MIDPOINT * ScaleFactorY);
			// draw the currently active selection start line
			e.Graphics.DrawLine(new Pen(Colors.White), SelectionStart, 0, SelectionStart, Size.Height);

			for (int i = 0; i < wave.RawSamples.Length; i++)
			{
				double x = i;
				double y = (ScaleFactorY * wave.RawSamples[i]);

				if (x >= SelectionStart && x <= (SelectionStart + SelectionLength))
				{
					e.Graphics.DrawLine(pen2, lastPoint.X, lastPoint.Y, x, y);
				}
				else
				{
					bool drawn = false;
					for (int j = 0; j < ed.Selections.Count; j++)
					{
						if (x >= (ed.Selections[j] as WaveformAudioEditorSelection).SelectionStart && x <= (((ed.Selections[j] as WaveformAudioEditorSelection).SelectionLength) + (ed.Selections[j] as WaveformAudioEditorSelection).SelectionLength))
						{
							e.Graphics.DrawLine(pen2, lastPoint.X, lastPoint.Y, x, y);
							drawn = true;
							break;
						}
					}

					if (!drawn)
						e.Graphics.DrawLine(pen, lastPoint.X, lastPoint.Y, x, y);
				}
				lastPoint = new Vector2D(x, y);
			}
		}

		protected override void OnScrolled(ScrolledEventArgs e)
		{
			if (preventScroll) return;

			preventScroll = true;
			WaveformAudioEditor ed = (Parent.Parent as WaveformAudioEditor);
			for (int i = 0; i < ed.Controls.Count; i++)
			{
				if (ed.Controls[i] is WaveformAudioEditorTrack)
				{
					WaveformAudioEditorTrackWaveform wvff = ((WaveformAudioEditorTrack)ed.Controls[i]).Controls[1] as WaveformAudioEditorTrackWaveform;
					wvff.HorizontalAdjustment.Value = HorizontalAdjustment.Value;
				}
			}
			preventScroll = false;
			base.OnScrolled(e);
		}
	}
}
