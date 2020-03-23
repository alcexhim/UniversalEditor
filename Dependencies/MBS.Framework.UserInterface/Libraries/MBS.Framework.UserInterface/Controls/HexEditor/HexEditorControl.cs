//
//  HexEditorControl.cs
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

using MBS.Framework;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface.Controls.HexEditor
{
	public class HexEditorControl : CustomControl
	{
		private int editOffset = 0;

		public int HorizontalCellSpacing { get; set; } = 4;
		public int VerticalCellSpacing { get; set; } = 4;

		private byte[] mvarData = new byte[4096];
		public byte[] Data
		{
			get { return mvarData; }
			set
			{
				System.ComponentModel.CancelEventArgs ee = new System.ComponentModel.CancelEventArgs();
				OnChanging(ee);
				if (ee.Cancel)
					return;
				mvarData = value; SelectionStart = SelectionStart;
				OnChanged(EventArgs.Empty);
			}
		}

		public HexEditorHitTestSection SelectedSection { get; private set; } = HexEditorHitTestSection.Hexadecimal;

		public HexEditorPosition SelectionStart
		{
			get { return new HexEditorPosition(mvarSelectionStart, selectedNybble); }
			set
			{
				if (value.ByteIndex < 0 || value.ByteIndex >= Data.Length)
				{
					Console.WriteLine(String.Format("Selection start must be between the bounds of zero and length of data ({0}) minus one", Data.Length));
				}
				else
				{
					mvarSelectionStart = value.ByteIndex;
				}

				if (value.NybbleIndex < 0 || value.NybbleIndex > 1)
				{
					Console.WriteLine(String.Format("Selected nybble must be either 0 or 1, not {0}", value.NybbleIndex));
				}
				else
				{
					selectedNybble = value.NybbleIndex;
				}

				cursorBlinking = true;
				Refresh();
				OnSelectionChanged(EventArgs.Empty);
			}
		}

		private HexEditorPosition mvarSelectionLength = new HexEditorPosition(0, 0);
		public HexEditorPosition SelectionLength
		{
			get { return mvarSelectionLength; }
			set
			{
				mvarSelectionLength = value;

				Refresh();
				OnSelectionChanged(EventArgs.Empty);
			}
		}

		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			SelectionChanged?.Invoke(this, e);
		}

		public int mvarSelectionStart = 0; // where the selection starts

		private int selectedNybble = 0; // 0 = first nybble, 1 = second nybble

		// The maximum number of cells we can display in a single line
		private int mvarMaxDisplayWidth = 16;

		// offsets for data display
		private const int xoffset = 0;
		private const int yoffset = 16 + 16;

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="HexEditorControl"/> allows inserting new data.
		/// </summary>
		/// <value><c>true</c> if inserting data is allowed; otherwise, <c>false</c>.</value>
		public bool EnableInsert { get; set; } = true;

		public HexEditorBackspaceBehavior BackspaceBehavior { get; set; } = HexEditorBackspaceBehavior.EraseNybble;
		/// <summary>
		/// Gets or sets a value indicating whether the backspace or delete key clears a byte instead of deleting it.
		/// </summary>
		/// <value><c>true</c> if backspace deletes; otherwise, <c>false</c>.</value>
		public bool BackspaceDeletes { get; set; } = true;

		public HexEditorControl()
		{
			HighlightAreas = new HexEditorHighlightArea.HexEditorHighlightAreaCollection(this);
		}

		public HexEditorHighlightArea.HexEditorHighlightAreaCollection HighlightAreas { get; private set; } = null;

		int PositionGutterWidth = 72;
		int TextAreaWidth = 128;

		protected internal override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			_tCursorBlinkThread = new System.Threading.Thread(_tCursorBlinkThread_ThreadStart);
			// _tCursorBlinkThread.Start();
		}

		public HexEditorHitTestInfo HitTest(double x, double y)
		{
			int byteIndex = -1, nybbleIndex = -1;
			Rectangle rectPosition = new Rectangle(xoffset, yoffset, PositionGutterWidth, 24);
			Rectangle rectCell = new Rectangle(rectPosition.X + rectPosition.Width + HorizontalCellSpacing, yoffset, 24, 24);

			for (int i = 0; i < mvarData.Length + 1; i++)
			{
				Rectangle rectChar = new Rectangle(this.Size.Width - TextAreaWidth - PositionGutterWidth + ((i % mvarMaxDisplayWidth) * 8), rectCell.Y, 8, 24);

				if (x >= rectCell.X && x <= rectCell.Right && y >= rectCell.Y && y <= rectCell.Bottom)
				{
					byteIndex = i;
					if (x >= (rectCell.X + (rectCell.Width / 2)))
					{
						nybbleIndex = 1;
					}
					else
					{
						nybbleIndex = 0;
					}
					return new HexEditorHitTestInfo(byteIndex, nybbleIndex, HexEditorHitTestSection.Hexadecimal);
				}
				else if (x >= rectChar.X && x <= rectChar.Right && y >= rectChar.Y && y <= rectChar.Bottom)
				{
					byteIndex = i;
					nybbleIndex = 0;
					return new HexEditorHitTestInfo(byteIndex, nybbleIndex, HexEditorHitTestSection.ASCII);
				}

				rectCell.X += rectCell.Width + HorizontalCellSpacing;

				if (((i + 1) % mvarMaxDisplayWidth) == 0)
				{
					rectCell.X = rectPosition.X + rectPosition.Width + HorizontalCellSpacing;
					rectCell.Y += rectCell.Height + VerticalCellSpacing;
					rectPosition.Y += rectPosition.Height + VerticalCellSpacing;
				}
			}
			return new HexEditorHitTestInfo(byteIndex, nybbleIndex, HexEditorHitTestSection.Hexadecimal);
		}

		protected internal override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();

			HexEditorHitTestInfo index = HitTest(e.X, e.Y);
			if (index.ByteIndex > -1)
			{
				mvarSelectionStart = index.ByteIndex;
				selectedNybble = index.NybbleIndex;
				mvarSelectionLength = new HexEditorPosition(0, 0);

				SelectionStart = SelectionStart;
				SelectedSection = index.Section;
				Refresh();
			}
		}
		protected internal override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Buttons == MouseButtons.Primary)
			{
				HexEditorHitTestInfo index = HitTest(e.X, e.Y);
				if (index.ByteIndex > -1)
				{
					SelectionLength = new HexEditorPosition(index.ByteIndex - mvarSelectionStart + 1, index.NybbleIndex);
				}
			}

			if (e.X > this.Size.Width - TextAreaWidth - PositionGutterWidth)
			{
				Cursor = Cursors.Text;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}

		private bool shouldSwitchSectionOnFocus = false;

		protected internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			if (shouldSwitchSectionOnFocus)
			{
				SelectedSection = HexEditorHitTestSection.Hexadecimal;
				shouldSwitchSectionOnFocus = false;
			}
		}

		private bool ShouldBackspaceDelete(KeyboardModifierKey modifierKeys)
		{
			return ((BackspaceDeletes && ((modifierKeys & KeyboardModifierKey.Shift) != KeyboardModifierKey.Shift))
				|| (BackspaceDeletes && ((modifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)));
		}

		protected internal override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			// we should learn how to do this mathematically...
			char next = '\0';

			switch (e.Key)
			{
				case KeyboardKey.Tab:
				{
					if (SelectedSection == HexEditorHitTestSection.ASCII)
					{
						// do nothing; let the owner handle the tab to next window
						// but we should switch the section on the next focus
						shouldSwitchSectionOnFocus = true;
					}
					else if (SelectedSection == HexEditorHitTestSection.Hexadecimal)
					{
						SelectedSection = HexEditorHitTestSection.ASCII;
						e.Cancel = true;
					}
					return;
				}
				case KeyboardKey.Back:
				{
					if (!Editable || ((SelectionStart == 0 && selectedNybble == 0) || SelectionStart < 0))
					{
						return;
					}

					if (BackspaceBehavior == HexEditorBackspaceBehavior.EraseByte || SelectedSection == HexEditorHitTestSection.ASCII)
					{
						// ASCII section ALWAYS erases a whole byte
						if (mvarSelectionStart > 0)
						{
							if (ShouldBackspaceDelete(e.ModifierKeys))
							{
								if (mvarSelectionLength == 0)
								{
									ArrayExtensions.Array_RemoveAt<byte>(ref mvarData, mvarSelectionStart - 1, mvarSelectionLength + 1);
									mvarSelectionStart--;
								}
								else
								{
									ArrayExtensions.Array_RemoveAt<byte>(ref mvarData, mvarSelectionStart, mvarSelectionLength);
									if (mvarSelectionLength < 0)
									{
										mvarSelectionStart += mvarSelectionLength;
									}
								}
							}
							else // if (BackspaceBehavior == HexEditorBackspaceBehavior.EraseByte)
							{
								Data[mvarSelectionStart - 1] = 0x0;
							}
							mvarSelectionLength = 0;

							SelectionStart = SelectionStart; // to fire events
							Refresh();
							e.Cancel = true;
						}
					}
					else if (BackspaceBehavior == HexEditorBackspaceBehavior.EraseNybble)
					{
						if (selectedNybble == 0)
						{
							string curhex = Data[mvarSelectionStart - 1].ToString("X").PadLeft(2, '0');
							Data[mvarSelectionStart - 1] = Byte.Parse(curhex.Substring(0, 1) + '0', System.Globalization.NumberStyles.HexNumber);

							if (ShouldBackspaceDelete(e.ModifierKeys))
							{
								ArrayExtensions.Array_RemoveAt<byte>(ref mvarData, mvarSelectionStart - 1);
								selectedNybble = 0;
							}
							else
							{
								selectedNybble = 1;
							}

							mvarSelectionStart--;
						}
						else if (selectedNybble == 1)
						{
							if (mvarSelectionStart < Data.Length)
							{
								string curhex = Data[mvarSelectionStart].ToString("X").PadLeft(2, '0');
								Data[mvarSelectionStart] = Byte.Parse('0' + curhex.Substring(1, 1), System.Globalization.NumberStyles.HexNumber);
							}
							selectedNybble = 0;
						}
						mvarSelectionLength = 0;

						SelectionStart = SelectionStart; // to fire events
						Refresh();
						e.Cancel = true;
					}
					return;
				}
				case KeyboardKey.ArrowLeft:
				{
					if (SelectedSection == HexEditorHitTestSection.Hexadecimal)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							mvarSelectionLength -= 0.5;
						}
						else
						{
							mvarSelectionLength = 0;
							if (selectedNybble == 1)
							{
								selectedNybble = 0;
							}
							else if (selectedNybble == 0)
							{
								mvarSelectionStart--;
								selectedNybble = 1;
							}

							if (mvarSelectionStart < 0)
							{
								mvarSelectionStart = 0;
								selectedNybble = 0;
							}
						}
					}
					else
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							mvarSelectionLength -= 1;
						}
						else
						{
							mvarSelectionLength = 0;
							mvarSelectionStart--;
							selectedNybble = 0;

							if (mvarSelectionStart < 0)
							{
								mvarSelectionStart = 0;
							}
						}
					}
					SelectionStart = SelectionStart; // to fire events
					e.Cancel = true;
					break;
				}
				case KeyboardKey.ArrowRight:
				{
					if (SelectedSection == HexEditorHitTestSection.Hexadecimal)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							mvarSelectionLength += 0.5;
						}
						else
						{
							mvarSelectionLength = 0;
							if (selectedNybble == 1)
							{
								mvarSelectionStart++;
								selectedNybble = 0;
							}
							else if (selectedNybble == 0)
							{
								selectedNybble = 1;
							}

							int end = Editable ? Data.Length : Data.Length - 1;
							if (mvarSelectionStart > end)
							{
								mvarSelectionStart = end;
								selectedNybble = 1;
							}
						}
					}
					else
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							mvarSelectionLength += 1;
						}
						else
						{
							mvarSelectionLength = 0;
							mvarSelectionStart++;
							selectedNybble = 0;

							int end = Editable ? Data.Length : Data.Length - 1;
							if (mvarSelectionStart > end)
							{
								mvarSelectionStart = end;
								selectedNybble = 0;
							}
						}
					}
					SelectionStart = SelectionStart; // to fire events
					e.Cancel = true;
					break;
				}
				case KeyboardKey.ArrowUp:
				{
					if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
					{
						mvarSelectionLength -= mvarMaxDisplayWidth;
					}
					else
					{
						mvarSelectionLength = 0;
						if (mvarSelectionStart - mvarMaxDisplayWidth >= 0)
						{
							mvarSelectionStart -= mvarMaxDisplayWidth;
						}
					}

					SelectionStart = SelectionStart; // to fire events
					e.Cancel = true;
					break;
				}
				case KeyboardKey.ArrowDown:
				{
					if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
					{
						mvarSelectionLength += mvarMaxDisplayWidth;
					}
					else
					{
						mvarSelectionLength = 0;
						mvarSelectionStart += mvarMaxDisplayWidth;

						if (mvarSelectionStart >= Data.Length)
							mvarSelectionStart = Data.Length - 1;

					}

					SelectionStart = SelectionStart; // to fire events
					e.Cancel = true;
					break;
				}
				case KeyboardKey.Home:
				{
					// let's try something different
					mvarSelectionLength = 0;
					if ((e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
					{
						mvarSelectionStart = 0;
						selectedNybble = 0;
					}
					else
					{
						mvarSelectionStart = (int)((double)mvarSelectionStart / (double)mvarMaxDisplayWidth) * mvarMaxDisplayWidth;
						selectedNybble = 0;
					}

					SelectionStart = SelectionStart; // to fire events
					Refresh();
					e.Cancel = true;
					break;
				}
				case KeyboardKey.End:
				{
					// let's try something different
					mvarSelectionLength = 0;
					if ((e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
					{
						mvarSelectionStart = Data.Length - 1;
						selectedNybble = 1;
					}
					else
					{
						mvarSelectionStart = Math.Min(Data.Length - 1, (((int)((double)mvarSelectionStart / (double)mvarMaxDisplayWidth) * mvarMaxDisplayWidth) + mvarMaxDisplayWidth) - 1);
						selectedNybble = 1;
					}

					SelectionStart = SelectionStart; // to fire events
					Refresh();
					e.Cancel = true;
					break;
				}
			}

			if (Editable)
			{
				if (SelectedSection == HexEditorHitTestSection.Hexadecimal)
				{
					if ((int)e.Key >= (int)KeyboardKey.D0 && (int)e.Key <= (int)KeyboardKey.D9)
					{
						next = (char)((int)'0' + ((int)e.Key - (int)KeyboardKey.D0));
					}
					else if ((int)e.Key >= (int)KeyboardKey.NumPad0 && (int)e.Key <= (int)KeyboardKey.NumPad9)
					{
						next = (char)((int)'0' + ((int)e.Key - (int)KeyboardKey.NumPad0));
					}
					else if ((int)e.Key >= (int)KeyboardKey.A && (int)e.Key <= (int)KeyboardKey.F)
					{
						next = (char)((int)'A' + ((int)e.Key - (int)KeyboardKey.A));
					}
				}
				else if (SelectedSection == HexEditorHitTestSection.ASCII)
				{
					if (((int)e.Key >= (int)KeyboardKey.D0 && (int)e.Key <= (int)KeyboardKey.D9) && e.ModifierKeys == KeyboardModifierKey.None)
					{
						next = (char)((int)'0' + ((int)e.Key - (int)KeyboardKey.D0));
					}
					else if ((int)e.Key >= (int)KeyboardKey.NumPad0 && (int)e.Key <= (int)KeyboardKey.NumPad9)
					{
						next = (char)((int)'0' + ((int)e.Key - (int)KeyboardKey.NumPad0));
					}
					else if ((int)e.Key >= (int)KeyboardKey.A && (int)e.Key <= (int)KeyboardKey.Z)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = (char)((int)'A' + ((int)e.Key - (int)KeyboardKey.A));
						}
						else
						{
							next = (char)((int)'a' + ((int)e.Key - (int)KeyboardKey.A));
						}
					}
					else if (e.Key == KeyboardKey.Space)
					{
						next = ' ';
					}
					else if (e.Key == KeyboardKey.Tilde)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '~';
						}
						else
						{
							next = '`';
						}
					}
					else if (e.Key == KeyboardKey.Backslash)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '|';
						}
						else
						{
							next = '\\';
						}
					}
					else if (e.Key == KeyboardKey.Pipe)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '|';
						}
						else
						{
							next = '\\';
						}
					}
					else if (e.Key == KeyboardKey.Question)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '?';
						}
						else
						{
							next = '/';
						}
					}
					else if (e.Key == KeyboardKey.OpenBrackets)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '{';
						}
						else
						{
							next = '[';
						}
					}
					else if (e.Key == KeyboardKey.CloseBrackets)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '}';
						}
						else
						{
							next = ']';
						}
					}
					else if (e.Key == KeyboardKey.Minus)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '_';
						}
						else
						{
							next = '-';
						}
					}
					else if (e.Key == KeyboardKey.Plus)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '+';
						}
						else
						{
							next = '=';
						}
					}
					else if (e.Key == KeyboardKey.Semicolon)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = ':';
						}
						else
						{
							next = ';';
						}
					}
					else if (e.Key == KeyboardKey.Quotes)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '"';
						}
						else
						{
							next = '\'';
						}
					}
					else if (e.Key == KeyboardKey.Period)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '>';
						}
						else
						{
							next = '.';
						}
					}
					else if (e.Key == KeyboardKey.Comma)
					{
						if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
						{
							next = '<';
						}
						else
						{
							next = ',';
						}
					}
					else if ((e.ModifierKeys & KeyboardModifierKey.Shift) == KeyboardModifierKey.Shift)
					{
						// special case certain !@#$% we're having trouble with
						switch (e.Key)
						{
							case KeyboardKey.D0: next = ')'; break;
							case KeyboardKey.D1: next = '!'; break;
							case KeyboardKey.D2: next = '@'; break;
							case KeyboardKey.D3: next = '#'; break;
							case KeyboardKey.D4: next = '$'; break;
							case KeyboardKey.D5: next = '%'; break;
							case KeyboardKey.D6: next = '^'; break;
							case KeyboardKey.D7: next = '&'; break;
							case KeyboardKey.D8: next = '*'; break;
							case KeyboardKey.D9: next = '('; break;
						}
					}
				}
			}

			if (next != '\0')
			{
				System.ComponentModel.CancelEventArgs ee = new System.ComponentModel.CancelEventArgs();
				OnChanging(ee);
				if (ee.Cancel)
					return;

				if (mvarSelectionStart >= Data.Length)
				{
					if (EnableInsert)
					{
						Array.Resize<byte>(ref mvarData, mvarData.Length + 1);
					}
				}

				if (SelectedSection == HexEditorHitTestSection.Hexadecimal)
				{
					string curhex = Data[mvarSelectionStart].ToString("X").PadLeft(2, '0');
					mvarSelectionLength = 0;
					if (selectedNybble == 0)
					{
						Data[mvarSelectionStart] = Byte.Parse(next.ToString() + curhex.Substring(1, 1), System.Globalization.NumberStyles.HexNumber);
						selectedNybble = 1;
					}
					else if (selectedNybble == 1)
					{
						Data[mvarSelectionStart] = Byte.Parse(curhex.Substring(0, 1) + next.ToString(), System.Globalization.NumberStyles.HexNumber);

						mvarSelectionStart++;
						selectedNybble = 0;
					}
				}
				else
				{
					Data[mvarSelectionStart] = (byte)next;
					mvarSelectionStart++;
					selectedNybble = 0;
				}

				OnChanged(EventArgs.Empty);
				SelectionStart = SelectionStart;
			}
		}

		public event System.ComponentModel.CancelEventHandler Changing;
		protected virtual void OnChanging(System.ComponentModel.CancelEventArgs e)
		{
			Changing?.Invoke(this, e);
		}

		public event EventHandler Changed;
		protected virtual void OnChanged(EventArgs e)
		{
			Changed?.Invoke(this, e);
		}

		private bool cursorBlinking = true;
		public bool BlinkCursor { get; set; } = true;

		private System.Threading.Thread _tCursorBlinkThread = null;
		private void _tCursorBlinkThread_ThreadStart()
		{
			while (true)
			{
				System.Threading.Thread.Sleep(500);
				cursorBlinking = !cursorBlinking;

				if (IsCreated)
					Refresh(); // replace with call to Invalidate(Rect)
			}
		}

		protected internal override void OnUnrealize(EventArgs e)
		{
			base.OnUnrealize(e);
			if (_tCursorBlinkThread != null)
				_tCursorBlinkThread.Abort();
		}

		public bool Editable { get; set; } = true;

		private Brush bForeColor = new SolidBrush(Colors.Black); // SolidBrush(SystemColors.TextBoxForegroundColor);
		private Brush bOffsetColor = new SolidBrush(Colors.DarkRed);

		private static readonly int HexAsciiMargin = 24;

		private static readonly Vector2D textOffset = new Vector2D(4, 16);

		private Pen pSelectionBorderFocused = new Pen(Colors.SteelBlue);
		private Brush bSelectionBorderFocused = new SolidBrush(Colors.SteelBlue);
		private Brush bSelectionBackgroundFocused = new SolidBrush(Colors.LightSteelBlue);

		private Pen pSelectionBorderUnfocused = new Pen(Colors.Gray);
		private Brush bSelectionBorderUnfocused = new SolidBrush(Colors.Gray);
		private Brush bSelectionBackgroundUnfocused = new SolidBrush(Colors.LightGray);

		protected internal override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			mvarMaxDisplayWidth = (int)((this.Size.Width - PositionGutterWidth - TextAreaWidth - 128 - HexAsciiMargin) / 24);

			Font font = Font.FromFamily("Monospace", 14.0);

			Rectangle rectPosition = new Rectangle(xoffset, yoffset, PositionGutterWidth, 24);
			Rectangle rectCell = new Rectangle(rectPosition.X + rectPosition.Width, yoffset, 24, 24);

			int end = mvarData.Length;
			if (Editable)
				end++;

			for (int i = 0; i < mvarMaxDisplayWidth; i++)
			{
				e.Graphics.DrawText(i.ToString("X").PadLeft(2, '0'), font, new Rectangle(rectCell.X + textOffset.X + (i * (24 + HorizontalCellSpacing)), yoffset - 8, 24, 24), bOffsetColor);
			}

			for (int i = 0; i < end; i++)
			{
				Rectangle rectPositionText = new Rectangle(rectPosition.X + textOffset.X, rectPosition.Y + textOffset.Y, rectPosition.Width - (textOffset.X * 2), rectPosition.Height - (textOffset.Y * 2));
				Rectangle rectCellText = new Rectangle(rectCell.X + textOffset.X, rectCell.Y + textOffset.Y, rectCell.Width - (textOffset.X * 2), rectCell.Height - (textOffset.Y * 2));
				Rectangle rectFirstNybbleCell = new Rectangle(rectCell.X, rectCell.Y, rectCell.Width / 2, rectCell.Height);
				Rectangle rectNybbleCell = new Rectangle(rectCell.X + ((rectCell.Width / 2) * selectedNybble), rectCell.Y, rectCell.Width / 2, rectCell.Height);

				Rectangle rectChar = new Rectangle(this.Size.Width - TextAreaWidth - PositionGutterWidth + ((i % mvarMaxDisplayWidth) * 8), rectCell.Y, 8, 24);
				Rectangle rectCharText = new Rectangle(rectChar.X, rectChar.Y + textOffset.Y, rectChar.Width, rectChar.Height);

				bool hasAreaFill = false;
				for (int j = HighlightAreas.Count - 1;  j > -1;  j--)
				{
					HexEditorHighlightArea area = HighlightAreas[j];
					if (i >= area.Start && i < (area.Start + area.Length))
					{
						e.Graphics.FillRectangle(new SolidBrush(area.Color), rectCell);
						e.Graphics.FillRectangle(new SolidBrush(area.Color), rectChar);
						hasAreaFill = true;
						break;
					}
				}

				int start = mvarSelectionStart;
				int length = mvarSelectionLength.ByteIndex;
				if (length < 0)
				{
					length = Math.Abs(length);
					start = mvarSelectionStart - length;
				}

				if (i >= start && i <= (start + length - 1))
				{
					if (mvarSelectionLength.NybbleIndex == 1 || i < (start + length - 1))
					{
						if (!hasAreaFill)
						{
							e.Graphics.FillRectangle(bSelectionBackgroundFocused, rectCell);
						}
						e.Graphics.DrawRectangle(SelectedSection == HexEditorHitTestSection.Hexadecimal ? pSelectionBorderFocused : pSelectionBorderUnfocused, rectCell);
					}
					else if (mvarSelectionLength.NybbleIndex == 0)
					{
						if (!hasAreaFill)
						{
							e.Graphics.FillRectangle(SelectedSection == HexEditorHitTestSection.Hexadecimal ? bSelectionBackgroundFocused : bSelectionBackgroundUnfocused, rectFirstNybbleCell);
						}
						e.Graphics.DrawRectangle(SelectedSection == HexEditorHitTestSection.Hexadecimal ? pSelectionBorderFocused : pSelectionBorderUnfocused, rectFirstNybbleCell);
					}
				}

				if ((i % mvarMaxDisplayWidth) == 0)
				{
					// print the offset of data, once per line
					string strOffset = i.ToString("X").PadLeft(8, '0');
					e.Graphics.DrawText(strOffset, font, rectPositionText, bOffsetColor);
				}

				if (i == mvarSelectionStart)
				{
					e.Graphics.DrawRectangle(SelectedSection == HexEditorHitTestSection.Hexadecimal ? pSelectionBorderFocused : pSelectionBorderUnfocused, rectCell);
					if (!BlinkCursor || cursorBlinking || !Editable)
						e.Graphics.FillRectangle(SelectedSection == HexEditorHitTestSection.Hexadecimal ? bSelectionBorderFocused : bSelectionBorderUnfocused, new Rectangle(rectNybbleCell.X, rectNybbleCell.Bottom - 4, rectNybbleCell.Width, 4));
				}

				if (i < mvarData.Length)
				{
					string hex = mvarData[i].ToString("X").PadLeft(2, '0');
					e.Graphics.DrawText(hex, font, rectCellText, bForeColor);
				}

				rectCell.X += rectCell.Width + HorizontalCellSpacing;

				if (mvarData.Length > 0 && i < end)
				{
					if (i >= start && i <= start + length)
					{
						if (i < start + length)
						{
							// highlight
							e.Graphics.FillRectangle(SelectedSection == HexEditorHitTestSection.ASCII ? bSelectionBackgroundFocused : bSelectionBackgroundUnfocused, new Rectangle(rectChar.X, rectChar.Y, rectChar.Width, rectChar.Height));
						}
						if (i == start)
						{
							// cursor
							e.Graphics.FillRectangle(SelectedSection == HexEditorHitTestSection.ASCII ? bSelectionBorderFocused : bSelectionBorderUnfocused, new Rectangle(rectChar.X, rectChar.Bottom - 4, rectChar.Width, 4));
						}
					}

					if (i < mvarData.Length)
					{
						if (!Char.IsControl((char)mvarData[i])) // (mvarData[i] > 31 && mvarData[i] < 128)
						{
							e.Graphics.DrawText(((char)mvarData[i]).ToString(), font, rectCharText, bForeColor);
						}
						else
						{
							e.Graphics.DrawText(".", font, rectCharText, bForeColor);
						}
					}
				}

				if (((i + 1) % mvarMaxDisplayWidth) == 0)
				{
					rectCell.X = rectPosition.X + rectPosition.Width;
					rectCell.Y += rectCell.Height + VerticalCellSpacing;
					rectPosition.Y += rectPosition.Height + VerticalCellSpacing;
				}

				if (rectPosition.Y > this.Size.Height)
				{
					break;
				}
			}
		}
	}
}
