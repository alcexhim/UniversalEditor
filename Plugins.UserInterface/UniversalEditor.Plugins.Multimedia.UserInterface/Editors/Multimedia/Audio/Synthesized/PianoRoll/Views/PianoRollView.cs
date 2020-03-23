//
//  PianoRollControl.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using System.Collections.Generic;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Dialogs;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Controls
{
	public class PianoRollView : View
	{
		public PianoRollView(Editor parentEditor) : base(parentEditor)
		{
			this.ContextMenuCommandID = "PianoRollEditor_ContextMenu";

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Arrow", ContextMenuArrow_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Draw", ContextMenuPencil_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Erase", ContextMenuErase_Click);

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_1", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_2", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_4", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_8", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_16", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_32", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_64", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_Off", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_Triplet", ContextMenu_NoteFixedLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteFixedLength_Dot", ContextMenu_NoteFixedLength_Click);

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_None", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Major", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_MinorHarmonic", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_MinorMelodic", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Diminished", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_BebopMajor", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_BebopDominant", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Arabic", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Enigmatic", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Neopolitan", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_NeopolitanMinor", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Hungarian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Dorian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Phyrogolydian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Lydian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Mixolydian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Aeolian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Locrian", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Minor", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_Chromatic", ContextMenu_Scale_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Scale_HalfWholeDiminished", ContextMenu_Scale_Click);

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_ShowGridLines", ContextMenuToggleGridLines_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Properties", ContextMenuProperties_Click);
		}

		/// <summary>
		/// When greater than -1, specifies the note length at which all newly-created notes are set and prevents dragging to specify note length.
		/// </summary>
		/// <value>The length of a newly-created note.</value>
		public int FixedNoteLength { get; set; } = -1;

		private int _PositionQuantization = -1;
		public int PositionQuantization
		{
			get { return _PositionQuantization; }
			set
			{
				_PositionQuantization = value;
				Refresh();
			}
		}

		private void ContextMenu_NoteFixedLength_Click(object sender, EventArgs e)
		{
			Command cmd = (sender as Command);
			string sValue = cmd.ID.Substring("PianoRollEditor_ContextMenu_NoteFixedLength_".Length);
			int iValue = -1;

			switch (sValue)
			{
				case "Off":
				{
					break;
				}
				case "Triplet":
				{
					break;
				}
				case "Dot":
				{
					break;
				}
				default:
				{
					iValue = Int32.Parse(sValue);
					break;
				}
			}
		}

		// TODO: make this more user-friendly
		private static Dictionary<string, int[]> Scales = new Dictionary<string, int[]>();
		static PianoRollView()
		{
			Scales.Add("Major", new int[] { 1, 3, 5, 6, 8, 10, 12 });
			Scales.Add("MinorHarmonic", new int[] { 1, 3, 4, 6, 7, 10, 12 });
		}

		private int[] _CurrentScale = null;
		public int[] CurrentScale
		{
			get { return _CurrentScale; }
			set
			{
				_CurrentScale = value;
				Refresh();
			}
		}

		private void ContextMenu_Scale_Click(object sender, EventArgs e)
		{
			Command cmd = (sender as Command);
			string sValue = cmd.ID.Substring("PianoRollEditor_ContextMenu_Scale_".Length);
			if (Scales.ContainsKey(sValue))
			{
				CurrentScale = Scales[sValue];
			}
			else
			{
				CurrentScale = null;
			}
		}

		protected override void OnBeforeContextMenu(EventArgs e)
		{
			base.OnBeforeContextMenu(e);

			this.ContextMenu.Items["EditCut"].Visible = (SelectedCommands.Count > 0);
			this.ContextMenu.Items["EditCopy"].Visible = (SelectedCommands.Count > 0);
			this.ContextMenu.Items["EditDelete"].Visible = (SelectedCommands.Count > 0);
		}

		private void ContextMenuArrow_Click(object sender, EventArgs e)
		{
			SelectionMode = PianoRollSelectionMode.Select;
		}
		private void ContextMenuPencil_Click(object sender, EventArgs e)
		{
			SelectionMode = PianoRollSelectionMode.Insert;
		}
		private void ContextMenuErase_Click(object sender, EventArgs e)
		{
			SelectionMode = PianoRollSelectionMode.Delete;
		}
		private void ContextMenuToggleGridLines_Click(object sender, EventArgs e)
		{
			ShowGridLines = !ShowGridLines;
		}

		private bool _ShowGridLines = false;
		public bool ShowGridLines { get { return _ShowGridLines; } set { _ShowGridLines = value; Refresh(); } }

		private PianoRollSelectionMode _SelectionMode;
		public PianoRollSelectionMode SelectionMode
		{
			get { return _SelectionMode; }
			set
			{
				_SelectionMode = value;
				switch (_SelectionMode)
				{
					case PianoRollSelectionMode.Insert:
					{
						Cursor = Cursors.Pencil;
						break;
					}
					case PianoRollSelectionMode.Select:
					{
						Cursor = Cursors.Default;
						break;
					}
					case PianoRollSelectionMode.Delete:
					{
						Cursor = Cursors.Eraser;
						break;
					}
				}
			}
		}

		private SynthesizedAudioCommand.SynthesizedAudioCommandCollection mvarSelectedCommands = new SynthesizedAudioCommand.SynthesizedAudioCommandCollection();
		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection SelectedCommands { get { return mvarSelectedCommands; } }

		public SynthesizedAudioCommand HitTest(Vector2D pt)
		{
			return HitTest((int)pt.X, (int)pt.Y);
		}
		public SynthesizedAudioCommand HitTest(int x, int y)
		{
			if (SelectedTrack == null) return null;
			foreach (SynthesizedAudioCommand cmd in SelectedTrack.Commands)
			{
				Rectangle rect = GetCommandRect(cmd);
				if (rect.Contains(x, y)) return cmd;
			}
			return null;
		}
		public SynthesizedAudioCommand[] HitTest(Rectangle rect)
		{
			if (SelectedTrack == null) return new SynthesizedAudioCommand[0];
			List<SynthesizedAudioCommand> list = new List<SynthesizedAudioCommand>();
			foreach (SynthesizedAudioCommand cmd in SelectedTrack.Commands)
			{
				Rectangle rect1 = GetCommandRect(cmd);
				if (rect.Normalize().IntersectsWith(rect1)) list.Add(cmd);
			}
			return list.ToArray();
		}

		private SynthesizedAudioTrack _SelectedTrack = null;
		public SynthesizedAudioTrack SelectedTrack
		{
			get { return _SelectedTrack; }
			set { _SelectedTrack = value; Refresh(); }
		}

		public int GridSize { get; set; } = 4;
		public Dimension2D NoteSize { get; set; } = new Dimension2D(12, 16);

		private Pen pGrid = new Pen(Colors.LightGray);
		private Pen pGridAlt = new Pen(Colors.Gray);

		private Pen pSelectionBorder = new Pen(Colors.SteelBlue);
		private SolidBrush bSelectionFill = new SolidBrush(Color.FromRGBADouble(Colors.SteelBlue.R, Colors.SteelBlue.G, Colors.SteelBlue.B, 0.5));

		private Pen pSelectionBorderDrawing = new Pen(Colors.DarkGray);
		private SolidBrush bSelectionFillDrawing = new SolidBrush(Color.FromRGBADouble(Colors.DarkGray.R, Colors.DarkGray.G, Colors.DarkGray.B, 0.5));

		private bool mvarShowKeyboard = true;
		public bool ShowKeyboard { get { return mvarShowKeyboard; } set { mvarShowKeyboard = value; Invalidate(); } }

		private int mvarKeyboardWidth = 64;
		public int KeyboardWidth { get { return mvarKeyboardWidth; } set { mvarKeyboardWidth = value; Invalidate(); } }

		private int mvarQuarterNoteWidth = 48;
		private int mvarNoteHeight = 14;

		private double mvarZoomFactor = 1.0;
		public double ZoomFactor { get { return mvarZoomFactor; } set { mvarZoomFactor = value; Invalidate(); } }

		private Dimension2D mvarQuantizationSize = new Dimension2D(16, 16);
		public Dimension2D QuantizationSize { get { return mvarQuantizationSize; } set { mvarQuantizationSize = value; Invalidate(); } }

		private bool m_selecting = false;
		private double cx = 0, cy = 0;
		private double dx = 0, dy = 0;

		private Vector2D drag_OriginalLocation = new Vector2D(0, 0);
		private Vector2D drag_CurrentLocation = new Vector2D(0, 0);

		private SynthesizedAudioCommand draggingCommand = null;

		private bool moved = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (SelectedTrack == null) return;

			SynthesizedAudioCommand cmd = HitTest(e.Location);
			if ((e.ModifierKeys == KeyboardModifierKey.None && (cmd != null && !mvarSelectedCommands.Contains(cmd))) || cmd == null)
			{
				mvarSelectedCommands.Clear();
			}
			if (cmd != null)
			{
				bool remove = false;
				if (e.ModifierKeys == KeyboardModifierKey.Shift)
				{
					if (mvarSelectedCommands.Count > 0)
					{
						int startIndex = SelectedTrack.Commands.IndexOf(mvarSelectedCommands[mvarSelectedCommands.Count - 1]);
						int endIndex = SelectedTrack.Commands.IndexOf(cmd);

						mvarSelectedCommands.Clear();
						for (int i = startIndex; i < endIndex; i++)
						{
							mvarSelectedCommands.Add(SelectedTrack.Commands[i]);
						}
					}
				}
				else if (e.ModifierKeys == KeyboardModifierKey.Control)
				{
					remove = mvarSelectedCommands.Contains(cmd);
				}
				if (remove)
				{
					mvarSelectedCommands.Remove(cmd);
				}
				else if (!mvarSelectedCommands.Contains(cmd))
				{
					mvarSelectedCommands.Add(cmd);
				}

				if (e.Buttons == MouseButtons.Primary)
					draggingCommand = cmd;
			}

			if (e.Buttons == MouseButtons.Primary)
			{
				if (mvarShowKeyboard && e.X < mvarKeyboardWidth)
				{
					return;
				}

				cx = e.X;
				cy = e.Y;
				dx = e.X;
				dy = e.Y;
				m_selecting = true;

				drag_OriginalLocation = Quantize(new Vector2D(cx, cy));
				drag_CurrentLocation = drag_OriginalLocation;
			}
			Refresh();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			SynthesizedAudioCommand cmd = HitTest(e.Location);
			if (ShowKeyboard && e.X < KeyboardWidth || cmd != null)
			{
				Cursor = Cursors.Default;
			}
			else if (SelectionMode == PianoRollSelectionMode.Insert)
			{
				Cursor = Cursors.Pencil;
			}

			if (e.Buttons == MouseButtons.Primary)
			{
				moved = true;
				dx = e.X;
				dy = e.Y;

				if (ShowKeyboard && dx < KeyboardWidth)
					dx = KeyboardWidth;

				if (draggingCommand == null && (SelectionMode == PianoRollSelectionMode.Select || SelectionMode == PianoRollSelectionMode.Insert))
				{
					drag_CurrentLocation = Quantize(new Vector2D(dx, drag_OriginalLocation.Y));
					if (SelectionMode == PianoRollSelectionMode.Select)
					{
						Rectangle rectSelection = new Rectangle(cx, cy, dx - cx, dy - cy);
						SynthesizedAudioCommand[] cmds = HitTest(rectSelection);

						mvarSelectedCommands.Clear();
						foreach (SynthesizedAudioCommand cmd1 in cmds)
						{
							mvarSelectedCommands.Add(cmd1);
						}
					}
				}
				else
				{
					drag_CurrentLocation = Quantize(new Vector2D(dx, dy));
				}
				Refresh();
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (SelectedTrack == null) return;

			if (e.Buttons == MouseButtons.Primary)
			{
				SynthesizedAudioCommand cmd = HitTest(e.Location);
				if (cmd != null && e.ModifierKeys == KeyboardModifierKey.None && !moved)
				{
					mvarSelectedCommands.Clear();
					mvarSelectedCommands.Add(cmd);
				}

				if (draggingCommand != null)
				{
					foreach (SynthesizedAudioCommand cmd1 in mvarSelectedCommands)
					{
						SynthesizedAudioCommandNote note = (cmd1 as SynthesizedAudioCommandNote);
						if (note != null)
						{
							double x = drag_CurrentLocation.X - drag_OriginalLocation.X;
							double y = drag_CurrentLocation.Y - drag_OriginalLocation.Y;

							Rectangle origNoteRect = GetNoteRect(note);
							Vector2D v = Quantize(origNoteRect.Location);
							v.X += x;
							v.Y += y;

							ApplyNoteRect(ref note, v);
						}
					}
					/*
					SynthesizedAudioCommandNote note = (draggingCommand as SynthesizedAudioCommandNote);
					if (note != null)
					{
						note.Position = (int)drag_CurrentLocation.X;
						note.Frequency = ValueToFrequency((int)drag_CurrentLocation.Y);
					}
					*/
					draggingCommand = null;
				}
				else if (SelectionMode == PianoRollSelectionMode.Insert)
				{
					if (drag_CurrentLocation.X - drag_OriginalLocation.X > 0)
					{
						SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();

						note.Position = (int)drag_OriginalLocation.X;
						note.Length = (drag_CurrentLocation.X - drag_OriginalLocation.X);
						note.Frequency = ValueToFrequency((int)drag_OriginalLocation.Y);

						Editor.BeginEdit();
						SelectedTrack.Commands.Add(note);
						Editor.EndEdit();

						mvarSelectedCommands.Clear();
						mvarSelectedCommands.Add(note);
					}
				}
				m_selecting = false;
				moved = false;

				Refresh();
			}
		}

		private void ApplyNoteRect(ref SynthesizedAudioCommandNote note, Vector2D quantized)
		{
			note.Position = (int)quantized.X;
			note.Frequency = ValueToFrequency((int)quantized.Y);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (e.Buttons == MouseButtons.Primary)
			{
				SynthesizedAudioCommand cmd = HitTest(e.Location);
				if (cmd is SynthesizedAudioCommandNote)
				{
					SynthesizedAudioCommandNote note = (cmd as SynthesizedAudioCommandNote);
					Rectangle rect = GetNoteRect(note);

					ShowNotePropertiesDialog(note);
					/*
					txtLyric.Location = rect.Location;
					txtLyric.Size = rect.Size;
					txtLyric.Text = note.Lyric;

					txtLyric.Enabled = true;
					txtLyric.Visible = true;

					txtLyric.Focus();
					*/				
				}
			}
		}

		public void ShowNotePropertiesDialog(SynthesizedAudioCommandNote note = null)
		{
			if (note == null && mvarSelectedCommands.Count > 0)
			{
				note = (mvarSelectedCommands[0] as SynthesizedAudioCommandNote);
			}
			if (note == null)
				return;

			// FIXME: ParentWindow is null, probably because this isn't a "normal" parented control but rather it's inside a DockingWindow...
			NotePropertiesDialog dlg = new NotePropertiesDialog();
			if (dlg.ShowDialog(ParentWindow) == DialogResult.OK)
			{

			}
		}
		int[] noteValuesBlackKeys = new int[] { 1, 3, 5, 8, 10 };

		private Vector2D Quantize(Vector2D pt)
		{
			if (mvarShowKeyboard)
				pt.X -= mvarKeyboardWidth;

			int qX = (int)((double)(pt.X / mvarQuarterNoteWidth));
			int qY = (int)((double)((pt.Y / mvarNoteHeight)));
			return new Vector2D(qX, qY);
		}
		private Vector2D Unquantize(Vector2D pt)
		{
			int uqX = (int)(pt.X * mvarQuarterNoteWidth);
			int uqY = (int)(pt.Y * mvarNoteHeight);
			return new Vector2D(uqX, uqY);
		}

		private Rectangle GetKeyboardRect()
		{
			Rectangle keyboardRect = new Rectangle(0, 0, Size.Width, Size.Height); // ClientRectangle
			keyboardRect.Width = mvarKeyboardWidth;
			return keyboardRect;
		}
		private Rectangle GetGridRect()
		{
			Rectangle gridRect = new Rectangle(0, 0, Size.Width, Size.Height); // ClientRectangle;
			if (mvarShowKeyboard)
			{
				Rectangle keyboardRect = GetKeyboardRect();
				gridRect.X += keyboardRect.X + keyboardRect.Width;
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
			Vector2D pt1 = Unquantize(new Vector2D((int)(note.Position), FrequencyToValue(note.Frequency)));
			Vector2D pt2 = Unquantize(new Vector2D((int)note.Length, FrequencyToValue(note.Frequency)));
			return new Rectangle(gridRect.X + pt1.X + 1, gridRect.Y + pt1.Y + 1, pt2.X - 1, mvarNoteHeight - 1);
		}

		private double ValueToFrequency(int value)
		{
			return value;
		}
		private int FrequencyToValue(double frequency)
		{
			return (int)frequency;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int gridWidth = (int)(mvarQuarterNoteWidth * mvarZoomFactor);
			int gridHeight = (int)(mvarNoteHeight * mvarZoomFactor);

			Rectangle gridRect = GetGridRect();
			if (mvarShowKeyboard)
			{
				DrawKeyboard(e.Graphics);
			}

			Pen gridPen = new Pen(Colors.LightGray);

			for (int i = 0; i < Size.Height; i += gridHeight)
			{
				int noteValue = (int)(((double)i / gridHeight) % 12) + 1;
				if (Array.IndexOf<int>(noteValuesBlackKeys, noteValue) != -1)
				{
					if (CurrentScale != null && (Array.IndexOf<int>(CurrentScale, noteValue) != -1))
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBAByte(0xAA, 0xCC, 0xAA)), new Rectangle(gridRect.X, gridRect.Y + i, gridRect.Width, gridHeight));
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBAByte(0xDD, 0xDD, 0xDD)), new Rectangle(gridRect.X, gridRect.Y + i, gridRect.Width, gridHeight));
					}
				}
				else
				{
					if (CurrentScale != null && (Array.IndexOf<int>(CurrentScale, noteValue) != -1))
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBAByte(0xAA, 0xDD, 0xAA)), new Rectangle(gridRect.X, gridRect.Y + i, gridRect.Width, gridHeight));
					}
				}

				gridPen.Color = Colors.LightGray;
				e.Graphics.DrawLine(gridPen, gridRect.X, gridRect.Y + i, gridRect.Right, gridRect.Y + i);

				string[] noteNames = new string[]
				{
					"A#", "A", "G#", "G", "F#", "F", "E", "D#", "D", "C#", "C", "B"
				};
				if ((noteValue - 1) >= 0 && (noteValue - 1) < noteNames.Length)
				{
					string noteName = noteNames[noteValue - 1];
					if (noteName.EndsWith("#")) continue;
					if (!noteName.Equals("C")) continue;

					e.Graphics.DrawText(noteName, Font, new Vector2D(mvarKeyboardWidth - 24, i + 1), new SolidBrush(noteName.Equals("C") ? Colors.DarkGray : Colors.LightGray));
				}

				e.Graphics.DrawText(noteValue.ToString(), Font, new Vector2D(0, i), new SolidBrush(Colors.Red));
			}
			for (int i = 0; i < Size.Width; i += (ShowGridLines ? (gridWidth / 2) : gridWidth))
			{
				if ((i % (mvarQuarterNoteWidth * 4)) == 0)
				{
					gridPen.Color = Colors.DarkGray;
				}
				else
				{
					gridPen.Color = Colors.LightGray;
				}
				e.Graphics.DrawLine(gridPen, gridRect.X + i, gridRect.Y, gridRect.X + i, gridRect.Bottom);
			}

			if (SelectedTrack != null)
			{
				foreach (SynthesizedAudioCommand cmd in SelectedTrack.Commands)
				{
					if (cmd is SynthesizedAudioCommandNote)
					{
						SynthesizedAudioCommandNote note = (cmd as SynthesizedAudioCommandNote);

						Rectangle rect = GetNoteRect(note);

						if (mvarSelectedCommands.Contains(cmd))
						{
							e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBAByte(0xCC, 0xCC, 0xFF)), rect);
						}
						else
						{
							e.Graphics.FillRectangle(new SolidBrush(Color.FromRGBAByte(0xCC, 0xFF, 0xCC)), rect);
						}
						e.Graphics.DrawRectangle(new Pen(Colors.DarkGray), rect);

						Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 1, rect.Width - 4, rect.Height - 2);
						e.Graphics.DrawText(note.Lyric, Font, textRect, new SolidBrush(Colors.Black));
					}
				}
			}

			if (draggingCommand != null)
			{
				if (draggingCommand is SynthesizedAudioCommandNote)
				{
					Rectangle origNoteRect = GetNoteRect(draggingCommand as SynthesizedAudioCommandNote);
					Rectangle newNoteRect = GetNoteRect(draggingCommand as SynthesizedAudioCommandNote);
					newNoteRect.Location = Unquantize(drag_CurrentLocation);

					Vector2D diff = new Vector2D(newNoteRect.X - origNoteRect.X, newNoteRect.Y - origNoteRect.Y);
					foreach (SynthesizedAudioCommand cmd in mvarSelectedCommands)
					{
						if (cmd is SynthesizedAudioCommandNote)
						{
							Rectangle noteRect = GetNoteRect(cmd as SynthesizedAudioCommandNote);
							noteRect.Location = new Vector2D(noteRect.X + diff.X, noteRect.Y + diff.Y);
							if (ShowKeyboard)
								noteRect.X += KeyboardWidth;

							e.Graphics.DrawRectangle(pSelectionBorder, noteRect);
						}
					}
				}
			}
			else if (m_selecting)
			{
				if (SelectionMode == PianoRollSelectionMode.Select)
				{
					e.Graphics.DrawRectangle(pSelectionBorder, new Rectangle(cx, cy, dx - cx, dy - cy));
					e.Graphics.FillRectangle(bSelectionFill, new Rectangle(cx, cy, dx - cx, dy - cy));
				}
				else if (SelectionMode == PianoRollSelectionMode.Insert)
				{
					Rectangle dragrect = new Rectangle(drag_OriginalLocation.X * mvarQuarterNoteWidth, drag_OriginalLocation.Y * mvarNoteHeight, (drag_CurrentLocation.X * mvarQuarterNoteWidth) - (drag_OriginalLocation.X * mvarQuarterNoteWidth), mvarNoteHeight);
					if (mvarShowKeyboard)
					{
						dragrect.X += mvarKeyboardWidth;
					}

					e.Graphics.DrawRectangle(pSelectionBorderDrawing, dragrect);
					e.Graphics.FillRectangle(bSelectionFillDrawing, dragrect);
				}
			}

			/*
			// draw grids
			for (int i = 0; i < Size.Width; i += (int) NoteSize.Width)
			{
				if (!ShowGridLines && (i % (int)(8 * NoteSize.Width) != 0))
					continue;
				e.Graphics.DrawLine(((i % (int)(8 * NoteSize.Width)) == 0) ? pGridAlt : pGrid, i, 0, i, Size.Height);
			}
			for (int i = 0; i < Size.Height; i += (int)NoteSize.Height)
			{
				e.Graphics.DrawLine(pGrid, 0, i, Size.Width, i);
			}
			*/
		}

		private void DrawKeyboard(Graphics g)
		{
			int gridWidth = (int)(mvarQuarterNoteWidth * mvarZoomFactor);
			int gridHeight = (int)(mvarNoteHeight * mvarZoomFactor);

			Rectangle keyboardRect = GetKeyboardRect();
			for (int i = 0; i < keyboardRect.Height; i += gridHeight)
			{
				int noteValue = (int)(((double)i / gridHeight) % 12) + 1;
				g.DrawLine(new Pen(Colors.LightGray), keyboardRect.X, i, keyboardRect.Right, i);

				if (Array.IndexOf<int>(noteValuesBlackKeys, noteValue) != -1)
				{
					// has a black key
					g.FillRectangle(new SolidBrush(Colors.DarkGray), keyboardRect.X, i, (int)((double)keyboardRect.Width / 2), gridHeight);
				}
			}
		}

		private void ContextMenuProperties_Click(object sender, EventArgs e)
		{
			ShowNotePropertiesDialog();
		}

		private int QuantizeHeight(int cy)
		{
			return (int)((double)cy / mvarNoteHeight);
		}
		private int QuantizeWidth(int cy)
		{
			return (int)((double)cy / mvarQuarterNoteWidth);
		}
	}
}
