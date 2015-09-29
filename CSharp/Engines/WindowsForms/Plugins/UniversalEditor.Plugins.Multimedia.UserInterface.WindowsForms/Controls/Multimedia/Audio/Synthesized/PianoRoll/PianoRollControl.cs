using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AwesomeControls.Theming;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.Controls.Multimedia.Audio.Synthesized.PianoRoll
{
    public partial class PianoRollControl : UserControl
    {
        public PianoRollControl()
        {
            InitializeComponent();
			DoubleBuffered = true;

			mvarCommands.ItemsChanged += mvarCommands_ItemsChanged;
        }

		private void mvarCommands_ItemsChanged(object sender, EventArgs e)
		{
			List<SynthesizedAudioCommand> cmdsToRemove = new List<SynthesizedAudioCommand>();
			foreach (SynthesizedAudioCommand cmd in mvarSelectedCommands)
			{
				if (!mvarCommands.Contains(cmd)) cmdsToRemove.Add(cmd);
			}
			foreach (SynthesizedAudioCommand cmd in cmdsToRemove)
			{
				mvarSelectedCommands.Remove(cmd);
			}
			Invalidate();
		}

		private SynthesizedAudioCommand.SynthesizedAudioCommandCollection mvarCommands = new SynthesizedAudioCommand.SynthesizedAudioCommandCollection();
		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection Commands { get { return mvarCommands; } }

		private SynthesizedAudioCommand.SynthesizedAudioCommandCollection mvarSelectedCommands = new SynthesizedAudioCommand.SynthesizedAudioCommandCollection();
		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection SelectedCommands { get { return mvarSelectedCommands; } }

		private bool mvarShowKeyboard = true;
		public bool ShowKeyboard { get { return mvarShowKeyboard; } set { mvarShowKeyboard = value; Invalidate(); } }

		private int mvarKeyboardWidth = 64;
		public int KeyboardWidth { get { return mvarKeyboardWidth; } set { mvarKeyboardWidth = value; Invalidate(); } }

		private int mvarQuarterNoteWidth = 48;
		private int mvarNoteHeight = 14;

		private double mvarZoomFactor = 1.0;
		public double ZoomFactor { get { return mvarZoomFactor; } set { mvarZoomFactor = value; Invalidate(); } }

        private Size mvarQuantizationSize = new Size(16, 16);
		public Size QuantizationSize { get { return mvarQuantizationSize; } set { mvarQuantizationSize = value; Invalidate(); } }

		private bool drag_Dragging = false;
		private Point drag_OriginalLocation = new Point();
		private Point drag_CurrentLocation = new Point();

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			Point pt = e.Location;
			if (mvarShowKeyboard)
			{
				pt.X -= mvarKeyboardWidth;
			}

			SynthesizedAudioCommand cmd = HitTest(e.Location);
			if (Control.ModifierKeys == Keys.None)
			{
				mvarSelectedCommands.Clear();
			}
			if (cmd != null)
			{
				bool remove = false;
				if (Control.ModifierKeys == Keys.Shift)
				{
					if (mvarSelectedCommands.Count > 0)
					{
						int startIndex = mvarCommands.IndexOf(mvarSelectedCommands[mvarSelectedCommands.Count - 1]);
						int endIndex = mvarCommands.IndexOf(cmd);

						mvarSelectedCommands.Clear();
						for (int i = startIndex; i < endIndex; i++)
						{
							mvarSelectedCommands.Add(mvarCommands[i]);
						}
					}
				}
				else if (Control.ModifierKeys == Keys.Control)
				{
					remove = mvarSelectedCommands.Contains(cmd);
				}
				if (remove)
				{
					mvarSelectedCommands.Remove(cmd);
				}
				else
				{
					mvarSelectedCommands.Add(cmd);
				}
			}

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				drag_OriginalLocation = Quantize(pt);
				drag_CurrentLocation = drag_OriginalLocation;
				drag_Dragging = true;
			}

			txtLyric.Visible = false;
			txtLyric.Enabled = false;
		}
		
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				Point pt = e.Location;
				if (mvarShowKeyboard)
				{
					pt.X -= mvarKeyboardWidth;
				}
				
				drag_CurrentLocation = Quantize(new Point(pt.X, drag_OriginalLocation.Y));
				Invalidate();
			}
		}

		private double ValueToFrequency(int value)
		{
			return value;
		}
		private int FrequencyToValue(double frequency)
		{
			return (int)frequency;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			drag_Dragging = false;

			if (drag_CurrentLocation.X - drag_OriginalLocation.X > 0)
			{
				SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();
				note.Position = drag_OriginalLocation.X;
				note.Length = (drag_CurrentLocation.X - drag_OriginalLocation.X);
				note.Frequency = ValueToFrequency(drag_OriginalLocation.Y);
				mvarCommands.Add(note);

				mvarSelectedCommands.Clear();
				mvarSelectedCommands.Add(note);
			}

			Invalidate();
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				SynthesizedAudioCommand cmd = HitTest(e.Location);
				if (cmd is SynthesizedAudioCommandNote)
				{
					SynthesizedAudioCommandNote note = (cmd as SynthesizedAudioCommandNote);
					Rectangle rect = GetNoteRect(note);
					txtLyric.Location = rect.Location;
					txtLyric.Size = rect.Size;
					txtLyric.Text = note.Lyric;

					txtLyric.Enabled = true;
					txtLyric.Visible = true;

					txtLyric.Focus();
				}
			}
		}

		private Point Quantize(Point pt)
		{
			int qX = (int)((double)(pt.X / mvarQuarterNoteWidth));
			int qY = (int)((double)((pt.Y / mvarNoteHeight)));
			return new Point(qX, qY);
		}
		private Point Unquantize(Point pt)
		{
			int uqX = (int)(pt.X * mvarQuarterNoteWidth);
			int uqY = (int)(pt.Y * mvarNoteHeight);
			return new Point(uqX, uqY);
		}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

			List<int> noteValuesBlackKeys = new List<int>(new int[]
			{
				1, 3, 5, 8, 10
			});

			int gridWidth = (int)(mvarQuarterNoteWidth * mvarZoomFactor);
			int gridHeight = (int)(mvarNoteHeight * mvarZoomFactor);
			
			Rectangle gridRect = GetGridRect();
			if (mvarShowKeyboard)
			{
				Rectangle keyboardRect = GetKeyboardRect();

				for (int i = 0; i < keyboardRect.Height; i += gridHeight)
				{
					int noteValue = (int)(((double)i / gridHeight) % 12) + 1;
					e.Graphics.DrawLine(new Pen(Colors.LightGray.ToGdiColor()), keyboardRect.Left, i, keyboardRect.Right, i);

					if (noteValuesBlackKeys.Contains(noteValue))
					{
						// has a black key
						e.Graphics.FillRectangle(new SolidBrush(Colors.DarkGray.ToGdiColor()), keyboardRect.Left, i, (int)((double)keyboardRect.Width / 2), gridHeight);
					}
				}
			}

			Pen gridPen = new Pen(Colors.LightGray.ToGdiColor());

			for (int i = 0; i < this.Height; i += gridHeight)
			{
				int noteValue = (int)(((double)i / gridHeight) % 12) + 1;
				if (noteValuesBlackKeys.Contains(noteValue))
				{
					e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBA(0xDD, 0xDD, 0xDD).ToGdiColor()), new Rectangle(gridRect.Left, gridRect.Top + i, gridRect.Width, gridHeight));
				}

				gridPen.Color = Colors.LightGray.ToGdiColor();
				e.Graphics.DrawLine(gridPen, gridRect.Left, gridRect.Top + i, gridRect.Right, gridRect.Top + i);

				string[] noteNames = new string[]
				{
					"A#", "A", "G#", "G", "F#", "F", "E", "D#", "D", "C#", "C", "B"
				};
				if ((noteValue - 1) >= 0 && (noteValue - 1) < noteNames.Length)
				{
					string noteName = noteNames[noteValue - 1];
					if (noteName.EndsWith("#")) continue;
					if (!noteName.Equals("C")) continue;

					e.Graphics.DrawString(noteName, Font, new SolidBrush(noteName.Equals("C") ? Colors.DarkGray.ToGdiColor() : Colors.LightGray.ToGdiColor()), new Point(mvarKeyboardWidth - 24, i + 1));
				}

				// e.Graphics.DrawString(noteValue.ToString(), Font, new SolidBrush(Colors.Red.ToGdiColor()), new Point(0, i));
			}
			for (int i = 0; i < this.Width; i += gridWidth)
			{
				if ((i % (mvarQuarterNoteWidth * 4)) == 0)
				{
					gridPen.Color = Colors.DarkGray.ToGdiColor();
				}
				else
				{
					gridPen.Color = Colors.LightGray.ToGdiColor();
				}
				e.Graphics.DrawLine(gridPen, gridRect.Left + i, gridRect.Top, gridRect.Left + i, gridRect.Bottom);
			}

			if (drag_Dragging)
			{
				Pen draggingPen = new Pen(Colors.DarkGray.ToGdiColor());
				draggingPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

				Rectangle draggingRect = new Rectangle(drag_OriginalLocation.X * mvarQuarterNoteWidth, drag_OriginalLocation.Y * mvarNoteHeight, (drag_CurrentLocation.X * mvarQuarterNoteWidth) - (drag_OriginalLocation.X * mvarQuarterNoteWidth), mvarNoteHeight);
				draggingRect.Offset(gridRect.X, gridRect.Y);

				e.Graphics.DrawRectangle(draggingPen, draggingRect);
			}

			foreach (SynthesizedAudioCommand cmd in mvarCommands)
			{
				if (cmd is SynthesizedAudioCommandNote)
				{
					SynthesizedAudioCommandNote note = (cmd as SynthesizedAudioCommandNote);

					Rectangle rect = GetNoteRect(note);

					if (mvarSelectedCommands.Contains(cmd))
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBA(0xCC, 0xCC, 0xFF).ToGdiColor()), rect);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBA(0xCC, 0xFF, 0xCC).ToGdiColor()), rect);
					}
					e.Graphics.DrawRectangle(new Pen(Colors.DarkGray.ToGdiColor()), rect);

					Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 1, rect.Width - 4, rect.Height - 2);
					e.Graphics.DrawString(note.Lyric, Font, new SolidBrush(Colors.Black.ToGdiColor()), textRect);
				}
			}
        }

		public SynthesizedAudioCommand HitTest(Point pt)
		{
			return HitTest(pt.X, pt.Y);
		}
		public SynthesizedAudioCommand HitTest(int x, int y)
		{
			foreach (SynthesizedAudioCommand cmd in mvarCommands)
			{
				Rectangle rect = GetCommandRect(cmd);
				if (rect.Contains(x, y)) return cmd;
			}
			return null;
		}

		private Rectangle GetKeyboardRect()
		{
			Rectangle keyboardRect = ClientRectangle;
			keyboardRect.Width = mvarKeyboardWidth;
			return keyboardRect;
		}
		private Rectangle GetGridRect()
		{
			Rectangle gridRect = ClientRectangle;
			if (mvarShowKeyboard)
			{
				Rectangle keyboardRect = GetKeyboardRect();
				gridRect.X += keyboardRect.Left + keyboardRect.Width;
			}
			return gridRect;
		}
		private Rectangle GetCommandRect(SynthesizedAudioCommand cmd)
		{
			if (cmd is SynthesizedAudioCommandNote)
			{
				return GetNoteRect(cmd as SynthesizedAudioCommandNote);
			}
			return Rectangle.Empty;
		}
		private Rectangle GetNoteRect(SynthesizedAudioCommandNote note)
		{
			Rectangle gridRect = GetGridRect();
			Point pt1 = Unquantize(new Point((int)(note.Position), FrequencyToValue(note.Frequency)));
			Point pt2 = Unquantize(new Point((int)note.Length, FrequencyToValue(note.Frequency)));
			return new Rectangle(gridRect.X + pt1.X + 1, gridRect.Y + pt1.Y + 1, pt2.X - 1, mvarNoteHeight - 1);
		}

		private void txtLyric_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				foreach (SynthesizedAudioCommand cmd in mvarSelectedCommands)
				{
					if (cmd is SynthesizedAudioCommandNote)
					{
						(cmd as SynthesizedAudioCommandNote).Lyric = txtLyric.Text;
					}
				}
				txtLyric.Visible = false;
				txtLyric.Enabled = false;
			}
		}

		public void Delete()
		{
			while (mvarSelectedCommands.Count > 0)
			{
				mvarCommands.Remove(mvarSelectedCommands[0]);
			}
		}

		private void mnuContextCut_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextCopy_Click(object sender, EventArgs e)
		{

		}
		private void mnuContextPaste_Click(object sender, EventArgs e)
		{
			IDataObject data = Clipboard.GetDataObject();
			if (data != null)
			{
				string[] formats = data.GetFormats();
			}
		}

		private void mnuContextDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void mnuContextProperties_Click(object sender, EventArgs e)
		{

		}

		private void mnuContext_Opening(object sender, CancelEventArgs e)
		{
			bool enableCmds = (mvarSelectedCommands.Count > 0);
			mnuContextCut.Enabled = enableCmds;
			mnuContextCopy.Enabled = enableCmds;
			// mnuContextPaste.Enabled = Clipboard.ContainsData(...);
			mnuContextPaste.Enabled = false;
			mnuContextDelete.Enabled = enableCmds;
			mnuContextProperties.Enabled = enableCmds;
		}

    }
}
