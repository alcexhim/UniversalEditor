﻿//
//  PianoRollView.cs - provides a UWT-based View for manipulating SynthesizedAudioCommands in a piano roll style
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Linq;

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Layouts;

using UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Dialogs;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Audio.Synthesized.PianoRoll.Views
{
	/// <summary>
	/// Provides a UWT-based <see cref="View" /> for manipulating <see cref="SynthesizedAudioCommand" />s in a piano roll style.
	/// </summary>
	public class PianoRollView : View
	{
		private TextBox txt = null;
		private SynthesizedAudioCommandNote _EditingNote = null;

		public PianoRollView(Editor parentEditor) : base(parentEditor)
		{
			Layout = new AbsoluteLayout();

			txt = new TextBox();
			txt.BorderStyle = ControlBorderStyle.None;
			txt.WidthChars = 0;
			txt.KeyDown += txt_KeyDown;
			Controls.Add(txt, new AbsoluteLayout.Constraints(0, 0, 128, 18));

			this.ContextMenuCommandID = "PianoRollEditor_ContextMenu";

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Arrow", ContextMenuArrow_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Draw", ContextMenuPencil_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Erase", ContextMenuErase_Click);

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_4", ContextMenu_Quantize_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_8", ContextMenu_Quantize_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_16", ContextMenu_Quantize_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_32", ContextMenu_Quantize_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_64", ContextMenu_Quantize_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_Off", ContextMenu_Quantize_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_Quantize_Triplet", ContextMenu_Quantize_Click);

			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_4", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_8", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_16", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_32", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_64", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_Off", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_Default", ContextMenu_NoteLength_Click);
			Application.AttachCommandEventHandler("PianoRollEditor_ContextMenu_NoteLength_Triplet", ContextMenu_NoteLength_Click);

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

		private void txt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == KeyboardKey.Enter)
			{
				if (_EditingNote != null)
				{
					if (_EditingNote.Lyric != txt.Text)
					{
						(Parent as Editor).BeginEdit();
						_EditingNote.Lyric = txt.Text;
						(Parent as Editor).EndEdit();
					}
				}
				txt.Visible = false;
			}
			else if (e.Key == KeyboardKey.Tab)
			{
				if (_EditingNote != null)
				{
					if (_EditingNote.Lyric != txt.Text)
					{
						(Parent as Editor).BeginEdit();
						_EditingNote.Lyric = txt.Text;
						(Parent as Editor).EndEdit();
					}
					if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
					{
						LyricEditNote(SelectedTrack.Commands.PreviousOfType<SynthesizedAudioCommandNote>(_EditingNote));
					}
					else
					{
						LyricEditNote(SelectedTrack.Commands.NextOfType<SynthesizedAudioCommandNote>(_EditingNote));
					}
				}
				e.Cancel = true;
			}
			else if (e.Key == KeyboardKey.Escape)
			{
				txt.Visible = false;
			}
		}

		protected override void OnRealize(EventArgs e)
		{
			base.OnRealize(e);
			// hack
			txt.Visible = false;
		}

		/// <summary>
		/// When greater than -1, specifies the note length at which all newly-created notes are set and prevents dragging to specify note length.
		/// </summary>
		/// <value>The length of a newly-created note.</value>
		public int FixedNoteLength { get; set; } = -1;

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
		private void ContextMenu_Quantize_Click(object sender, EventArgs e)
		{
			Command cmd = (sender as Command);
			string sValue = cmd.ID.Substring("PianoRollEditor_ContextMenu_Quantize_".Length);
			int iValue = 4;  //-1;

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

			PositionQuantization = (int)((double)1920 / iValue);
		}
		private void ContextMenu_NoteLength_Click(object sender, EventArgs e)
		{
			Command cmd = (sender as Command);
			string sValue = cmd.ID.Substring("PianoRollEditor_ContextMenu_NoteLength_".Length);
			int iValue = 4;  //-1;

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
				case "Default":
				{
					iValue = -1;
					break;
				}
				default:
				{
					iValue = Int32.Parse(sValue);
					break;
				}
			}

			LengthQuantization = (int)((double)1920 / iValue);
		}

		// TODO: make this more user-friendly
		private static Dictionary<string, int[]> Scales = new Dictionary<string, int[]>();
		static PianoRollView()
		{
			Scales.Add("Major", new int[] { 1, 3, 5, 6, 8, 10, 11 });
			Scales.Add("MinorHarmonic", new int[] { 1, 3, 4, 6, 7, 10, 11 });
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

		public Dimension2D NoteSize { get; set; } = new Dimension2D(12, 16);

		private Pen pGrid = new Pen(Colors.LightGray);
		private Pen pGridAlt = new Pen(Colors.Gray);

		private Pen pSelectionBorderDrawing = new Pen(Colors.DarkGray);
		private SolidBrush bSelectionFillDrawing = new SolidBrush(Color.FromRGBADouble(Colors.DarkGray.R, Colors.DarkGray.G, Colors.DarkGray.B, 0.5));

		private bool mvarShowKeyboard = true;
		/// <summary>
		/// Gets or sets a value indicating whether the virtual keyboard should be shown on this <see cref="PianoRollView" />.
		/// </summary>
		/// <value><c>true</c> if the virtual keyboard should be shown; otherwise, <c>false</c>.</value>
		public bool ShowKeyboard { get { return mvarShowKeyboard; } set { mvarShowKeyboard = value; Invalidate(); } }

		private int mvarKeyboardWidth = 64;
		public int KeyboardWidth { get { return mvarKeyboardWidth; } set { mvarKeyboardWidth = value; Invalidate(); } }

		private int _LengthQuantization = -1;
		/// <summary>
		/// Gets or sets the quantization for note length (i.e., the width of a quarter note on the grid). All quantization operations are relative to this value.
		/// </summary>
		/// <value>The quantization for note length (i.e., the width of a quarter note on the grid).</value>
		public int LengthQuantization { get { return _LengthQuantization; } set { _LengthQuantization = value; Invalidate(); } }
		/// <summary>
		/// Gets or sets the quantization for note positioning.
		/// </summary>
		/// <value>The quantization for note positioning.</value>
		public int PositionQuantization { get; set; } = 60;

		private int GetLengthQuantization()
		{
			if (LengthQuantization == -1)
				return PositionQuantization;
			return LengthQuantization;
		}

		private int _GridWidth = 120;
		public int GridWidth { get { return _GridWidth;  }set { _GridWidth = value;  Invalidate(); } }

		private int _NoteHeight = 22;
		/// <summary>
		/// Gets or sets the height of a note on the grid.
		/// </summary>
		/// <value>The height of a note on the grid.</value>
		public int NoteHeight { get { return _NoteHeight; } set { _NoteHeight = value;  Invalidate(); } }

		private bool _HighlightOverlappingNotes = true;
		/// <summary>
		/// Gets or sets a value indicating whether overlapping notes on this <see cref="PianoRollView" /> grid are highlighted.
		/// </summary>
		/// <value><c>true</c> if overlapping notes should be highlighted; otherwise, <c>false</c>.</value>
		public bool HighlightOverlappingNotes { get { return _HighlightOverlappingNotes; } set { _HighlightOverlappingNotes = value;  Invalidate(); } }

		private double mvarZoomFactor = 1.0;
		public double ZoomFactor { get { return mvarZoomFactor; } set { mvarZoomFactor = value; Invalidate(); } }

		private bool m_selecting = false;
		private double cx = 0, cy = 0;
		private double dx = 0, dy = 0;

		private Vector2D note_OriginalLocation = new Vector2D(0, 0);
		private Vector2D drag_OriginalLocation = new Vector2D(0, 0);
		private Vector2D drag_CurrentLocation = new Vector2D(0, 0);

		private SynthesizedAudioCommand draggingCommand = null;

		private bool moved = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();

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

				drag_OriginalLocation = Quantize(new Vector2D(cx, cy), QuantizationMode.Position);
				if (cmd is SynthesizedAudioCommandNote)
				{
					note_OriginalLocation = Quantize(GetNoteRect(cmd as SynthesizedAudioCommandNote).Location, QuantizationMode.Position);
				}
				drag_CurrentLocation = drag_OriginalLocation;
			}

			txt.Visible = false;
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
					drag_CurrentLocation = Quantize(new Vector2D(dx, drag_OriginalLocation.Y), QuantizationMode.Length);
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
					drag_CurrentLocation = Quantize(new Vector2D(dx, dy), QuantizationMode.Length);
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
					if (!(drag_CurrentLocation.X - drag_OriginalLocation.X == 0 && drag_CurrentLocation.Y - drag_OriginalLocation.Y == 0))
					{
						(Parent as Editor).BeginEdit();

						foreach (SynthesizedAudioCommand cmd1 in mvarSelectedCommands)
						{
							SynthesizedAudioCommandNote note = (cmd1 as SynthesizedAudioCommandNote);
							if (note != null)
							{
								double x = drag_CurrentLocation.X - drag_OriginalLocation.X;
								double y = drag_CurrentLocation.Y - drag_OriginalLocation.Y;

								Rectangle origNoteRect = GetNoteRect(note);
								Vector2D v = Quantize(origNoteRect.Location, QuantizationMode.Position);
								v.X += x;
								v.Y += y;

								if ((e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
								{
									// copy the note to the current location
									SynthesizedAudioCommandNote note2 = note.Clone() as SynthesizedAudioCommandNote;
									ApplyNoteRect(ref note2, v);
									SelectedTrack.Commands.Add(note2);
								}
								else
								{
									ApplyNoteRect(ref note, v);
								}
							}
						}

						(Parent as Editor).EndEdit();
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
						(Parent as Editor).BeginEdit();

						SynthesizedAudioCommandNote note = new SynthesizedAudioCommandNote();

						note.Position = LocationToNotePosition((int)drag_OriginalLocation.X, QuantizationMode.Position) * 4;
						note.Length = LocationToNotePosition((int)(drag_CurrentLocation.X - drag_OriginalLocation.X), QuantizationMode.Length) * 4;
						note.Frequency = ValueToFrequency((int)drag_OriginalLocation.Y);

						Editor.BeginEdit();
						SelectedTrack.Commands.Add(note);
						Editor.EndEdit();

						mvarSelectedCommands.Clear();
						mvarSelectedCommands.Add(note);

						(Parent as Editor).EndEdit();
					}
				}
				m_selecting = false;
				moved = false;

				Refresh();
			}
		}

		private int LocationToNotePosition(int x, QuantizationMode mode)
		{
			return (int)Unquantize(new MBS.Framework.Drawing.Vector2D(x, 0), mode).X;
		}
		private int NotePositionToLocation(double x)
		{
			return (int)(x / 4);
		}

		private void ApplyNoteRect(ref SynthesizedAudioCommandNote note, Vector2D quantized)
		{
			note.Position = LocationToNotePosition((int)quantized.X, QuantizationMode.Position) * 4;
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
					LyricEditNote(note);

					// ShowNotePropertiesDialog(note);
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

		private void LyricEditNote(SynthesizedAudioCommandNote note)
		{
			_EditingNote = note;
			Rectangle rect = GetNoteRect(note);

			txt.Text = note.Lyric;
			txt.Visible = true;
			Layout.SetControlConstraints(txt, new AbsoluteLayout.Constraints((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
			txt.Focus();

			Invalidate(rect);
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
			dlg.Lyric = note.Lyric;
			dlg.Phoneme = note.Phoneme;
			dlg.NoteOn = (double)note.Position;
			dlg.NoteOff = note.Position + note.Length;
			if (dlg.ShowDialog(ParentWindow) == DialogResult.OK)
			{
				(Parent as Editor).BeginEdit();

				note.Lyric = dlg.Lyric;
				note.Phoneme = dlg.Phoneme;
				note.Position = (int)dlg.NoteOn;
				note.Length = dlg.NoteOff - dlg.NoteOn;

				(Parent as Editor).EndEdit();
			}
		}

		string[] noteNames = new string[]
		{
			"A#", "A", "G#", "G", "F#", "F", "E", "D#", "D", "C#", "C", "B"
		};

		private Vector2D Quantize(Vector2D pt, QuantizationMode mode)
		{
			if (mvarShowKeyboard)
				pt.X -= mvarKeyboardWidth;

			int quant;
			if (mode == QuantizationMode.Length)
				quant = GetLengthQuantization();
			else
				quant = PositionQuantization;

			int qX = (int)((double)(pt.X / (quant * ZoomFactor)));
			int qY = (int)((double)((pt.Y / NoteHeight)));
			return new Vector2D(qX, qY);
		}
		private Vector2D Unquantize(Vector2D pt, QuantizationMode mode)
		{
			int quant;
			if (mode == QuantizationMode.Length)
				quant = GetLengthQuantization();
			else
				quant = PositionQuantization;

			int uqX = (int)(pt.X * (quant * ZoomFactor));
			int uqY = (int)(pt.Y * NoteHeight);
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

			int x = (int)NotePositionToLocation(note.Position);
			int y = FrequencyToValue(note.Frequency);
			int width = (int)NotePositionToLocation((int)note.Length);
			int height = NoteHeight;

			return new Rectangle(gridRect.X + x + 1, gridRect.Y + y + 1, width - 1, height - 1);
		}

		private double ValueToFrequency(int value)
		{
			return (81 - value);
		}
		private int FrequencyToValue(double frequency)
		{
			return (int)((81 - frequency) * NoteHeight);
		}

		private bool _scrollBoundsChanged = true;
		private void RecalcScrollBounds()
		{
			for (int i = 0; i < SelectedTrack.Commands.Count; i++)
			{

			}

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int gridWidth = (int)(GridWidth * mvarZoomFactor);
			int gridHeight = (int)(NoteHeight * mvarZoomFactor);

			if (_scrollBoundsChanged)
			{
				RecalcScrollBounds();
				_scrollBoundsChanged = false;
			}

			Rectangle gridRect = GetGridRect();
			if (mvarShowKeyboard)
			{
				DrawKeyboard(e.Graphics);
			}

			Pen gridPen = new Pen(Colors.Gray.Alpha(0.25));

			Pen pSelectionBorder = new Pen(SystemColors.HighlightBackground);
			SolidBrush bSelectionFill = new SolidBrush(Color.FromRGBADouble(SystemColors.HighlightBackground.R, SystemColors.HighlightBackground.G, SystemColors.HighlightBackground.B, 0.5));

			SolidBrush sbScaleHighlight = new SolidBrush(SystemColors.HighlightBackground.Alpha(0.25));
			SolidBrush sbBlackKeyHighlight = new SolidBrush(Colors.Gray.Alpha(0.125));

			for (int i = 0; i < Size.Height; i += gridHeight)
			{
				int noteValue = (int)(((double)i / gridHeight) % 12);
				string noteName = noteNames[noteValue];

				if (CurrentScale != null && (Array.IndexOf<int>(CurrentScale, noteValue) != -1))
				{
					e.Graphics.FillRectangle(sbScaleHighlight, new Rectangle(gridRect.X, gridRect.Y + i - gridHeight, gridRect.Width, gridHeight));
				}
				if (noteName.EndsWith("#") || noteName.EndsWith("b"))
				{
					if (CurrentScale != null && (Array.IndexOf<int>(CurrentScale, noteValue) != -1))
					{
						e.Graphics.FillRectangle(new SolidBrush(Colors.Black.Alpha(0.125)), new Rectangle(gridRect.X, gridRect.Y + i - gridHeight, gridRect.Width, gridHeight));
					}
					else
					{
						e.Graphics.FillRectangle(sbBlackKeyHighlight, new Rectangle(gridRect.X, gridRect.Y + i - gridHeight, gridRect.Width, gridHeight));
					}
				}

				gridPen.Color = Colors.Gray.Alpha(0.25);
				e.Graphics.DrawLine(gridPen, gridRect.X, gridRect.Y + i, gridRect.Right, gridRect.Y + i);

				if (noteValue >= 0 && noteValue < noteNames.Length)
				{
					// if (noteName.EndsWith("#")) continue;
					if (!noteName.Equals("C")) continue;

					e.Graphics.DrawText(noteName, Font, new Vector2D(mvarKeyboardWidth - 24, i + 1), new SolidBrush(noteName.Equals("C") ? Colors.DarkGray : Colors.LightGray));
					e.Graphics.DrawText(noteValue.ToString(), Font, new Vector2D(0, i), new SolidBrush(Colors.Red));
				}
			}
			for (int i = 0; i < Size.Width; i += (ShowGridLines ? (gridWidth / 2) : gridWidth))
			{
				if ((i % GetLengthQuantization()) == 0)
				{
					gridPen.Color = Colors.Gray.Alpha(0.50);
				}
				else
				{
					gridPen.Color = Colors.Gray.Alpha(0.25);
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
						bool overlaps = false;
						if (HighlightOverlappingNotes) overlaps = NoteOverlaps(note);
						if (!DrawNote(e.Graphics, rect, note.Lyric, note.Phoneme, mvarSelectedCommands.Contains(cmd), _EditingNote == note, overlaps))
							break;
					}
				}
			}

			if (draggingCommand != null)
			{
				if (draggingCommand is SynthesizedAudioCommandNote)
				{
					Rectangle origNoteRect = GetNoteRect(draggingCommand as SynthesizedAudioCommandNote);
					Rectangle newNoteRect = GetNoteRect(draggingCommand as SynthesizedAudioCommandNote);
					newNoteRect.Location = Unquantize(new Vector2D(note_OriginalLocation.X + (drag_CurrentLocation.X - drag_OriginalLocation.X), note_OriginalLocation.Y + (drag_CurrentLocation.Y - drag_OriginalLocation.Y)), QuantizationMode.Position);

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
					Rectangle dragrect = new Rectangle(drag_OriginalLocation.X * PositionQuantization, drag_OriginalLocation.Y * NoteHeight, (drag_CurrentLocation.X * GetLengthQuantization()) - (drag_OriginalLocation.X * GetLengthQuantization()), NoteHeight);
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
		private bool NoteOverlaps(SynthesizedAudioCommandNote note)
		{
			Rectangle rect = GetNoteRect(note);

			for (int i = 0; i < SelectedTrack.Commands.Count; i++)
			{
				SynthesizedAudioCommandNote note2 = SelectedTrack.Commands[i] as SynthesizedAudioCommandNote;
				if (note2 == null) continue;
				if (note2 == note) continue;

				Rectangle rect2 = GetNoteRect(note2);
				if ((rect.X >= rect2.X && rect.X <= rect2.Right) || (rect2.X >= rect.X & rect2.X <= rect.Right))
					return true;
			}
			return false;
		}

		private bool DrawNote(Graphics graphics, Rectangle rect, string lyric, string phoneme, bool selected, bool editing, bool overlaps)
		{
			if (rect.X > Size.Width)
				return false; // no need to continue since we've reached the end of the visible area

			Color noteColor = SystemColors.HighlightBackground.Darken(0.1);
			if (overlaps)
			{
				noteColor = Colors.DarkGray;
			}

			if (selected)
			{
				graphics.FillRectangle(new SolidBrush(SystemColors.HighlightBackground), rect);
			}
			else
			{
				graphics.FillRectangle(new SolidBrush(SystemColors.WindowBackground), rect);
				graphics.FillRectangle(new SolidBrush(noteColor.Alpha(0.3)), rect);
			}
			graphics.DrawRectangle(new Pen(noteColor.Alpha(0.5)), rect);

			Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 10, rect.Width - 4, rect.Height - 2);

			if (!(txt.Visible && editing))
			{
				graphics.DrawText(String.Format("{0}        [ {1} ]", lyric, phoneme), Font, textRect, new SolidBrush(selected ? SystemColors.HighlightForeground : SystemColors.WindowForeground));
			}
			return true;
		}

		private void DrawKeyboard(Graphics g)
		{
			int gridWidth = (int)(GetLengthQuantization() * mvarZoomFactor);
			int gridHeight = (int)(NoteHeight * mvarZoomFactor);

			Rectangle keyboardRect = GetKeyboardRect();
			for (int i = 0; i < keyboardRect.Height; i += gridHeight)
			{
				int noteValue = (int)(((double)i / gridHeight) % 12);
				g.DrawLine(new Pen(Colors.LightGray), keyboardRect.X, i, keyboardRect.Right, i);

				if (noteNames[noteValue].EndsWith("#") || noteNames[noteValue].EndsWith("b"))
				{
					// has a black key
					g.FillRectangle(new SolidBrush(Colors.DarkGray), keyboardRect.X, i - gridHeight, (int)((double)keyboardRect.Width / 2), gridHeight);
				}
			}
		}

		private void ContextMenuProperties_Click(object sender, EventArgs e)
		{
			ShowNotePropertiesDialog();
		}

		private int QuantizeHeight(int cy)
		{
			return (int)((double)cy / NoteHeight);
		}
		private int QuantizeWidth(int cy)
		{
			return (int)((double)cy / GetLengthQuantization());
		}
	}
}
