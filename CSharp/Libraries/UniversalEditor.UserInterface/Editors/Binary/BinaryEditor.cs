//
//  BinaryEditor.cs
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
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.ObjectModels.Binary;
using UniversalEditor.UserInterface;
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Controls.HexEditor;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.Layouts;

namespace UniversalEditor.Editors.Binary
{
	public class BinaryEditor : Editor
	{
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(BinaryObjectModel));
			}
			return _er;
		}

		private HexEditorControl hexedit = null;
		private Container conversionPanel = null;
		private TabContainer tabs = null;

		private struct CONVERSION_DATA
		{
			public string Label;
			public Func<byte[], string> ByteToStringFunc;
			public Func<string, byte[]> StringToByteFunc;
			public TextBox TextBox;
			public int MaximumSize;

			public CONVERSION_DATA(string label, Func<byte[], string> byteToStringFunc, Func<string, byte[]> stringToByteFunc, int maximumSize)
			{
				Label = label;
				ByteToStringFunc = byteToStringFunc;
				StringToByteFunc = stringToByteFunc;
				TextBox = null;
				MaximumSize = maximumSize;
			}
		}

		private CONVERSION_DATA[] converters = new CONVERSION_DATA[]
		{
			new CONVERSION_DATA("Signed 8-bit", delegate(byte[] input)
			{
				if (input.Length < 1)
					return null;

				sbyte b = (sbyte)input[0];
				return b.ToString();
			}, delegate(string input)
			{
				sbyte b = SByte.Parse(input);
				return new byte[] { (byte)b };
			}, 1),
			new CONVERSION_DATA("Unsigned 8-bit", delegate(byte[] input)
			{
				if (input.Length < 1)
					return null;

				byte b = input[0];
				return b.ToString();
			}, delegate(string input)
			{
				byte b = Byte.Parse(input);
				return new byte[] { b };
			}, 1),
			new CONVERSION_DATA("Signed 16-bit", delegate(byte[] input)
			{
				if (input.Length < 2)
					return null;

				short b = BitConverter.ToInt16(input, 0);
				return b.ToString();
			}, delegate(string input)
			{
				short b = Int16.Parse(input);
				return BitConverter.GetBytes(b);
			}, 2),
			new CONVERSION_DATA("Unsigned 16-bit", delegate(byte[] input)
			{
				if (input.Length < 2)
					return null;

				ushort b = BitConverter.ToUInt16(input, 0);
				return b.ToString();
			}, delegate(string input)
			{
				ushort b = UInt16.Parse(input);
				return BitConverter.GetBytes(b);
			}, 2),
			// Second column
			new CONVERSION_DATA("Signed 32-bit", delegate(byte[] input)
			{
				if (input.Length < 4)
					return null;

				int b = BitConverter.ToInt32(input, 0);
				return b.ToString();
			}, delegate(string input)
			{
				int b = Int32.Parse(input);
				return BitConverter.GetBytes(b);
			}, 4),
			new CONVERSION_DATA("Unsigned 32-bit", delegate(byte[] input)
			{
				if (input.Length < 4)
					return null;

				uint b = BitConverter.ToUInt32(input, 0);
				return b.ToString();
			}, delegate(string input)
			{
				if (input.Length < 2)
					return null;

				uint b = UInt32.Parse(input, 0);
				return BitConverter.GetBytes(b);
			}, 4),
			new CONVERSION_DATA("Float 32-bit", delegate(byte[] input)
			{
				if (input.Length < 4)
					return null;

				float b = BitConverter.ToSingle(input, 0);
				return b.ToString();
			}, delegate(string input)
			{
				if (input.Length < 2)
					return null;

				float b = Single.Parse(input);
				return BitConverter.GetBytes(b);
			}, 4),
			new CONVERSION_DATA("Float 64-bit", delegate(byte[] input)
			{
				if (input.Length < 8)
					return null;

				double b = BitConverter.ToDouble(input, 0);
				return b.ToString();
			}, delegate(string input)
			{
				double b = Double.Parse(input);
				return BitConverter.GetBytes(b);
			}, 8),
			// Third column
			new CONVERSION_DATA("Hexadecimal", delegate(byte[] input)
			{
				StringBuilder sb = new StringBuilder();
				if (input.Length < 1)
					return null;

				int len = Math.Min(input.Length, 4);
				for (int i = 0; i < len; i++)
				{
					sb.Append(input[i].ToString("X").PadLeft(2, '0'));
					if (i < len - 1)
						sb.Append(' ');
				}
				return sb.ToString();
			}, delegate(string input)
			{
				// unsupported right now
				return null;
			}, 4),
			new CONVERSION_DATA("Decimal", delegate(byte[] input)
			{
				StringBuilder sb = new StringBuilder();
				if (input.Length < 1)
					return null;

				int len = Math.Min(input.Length, 4);
				for (int i = 0; i < len; i++)
				{
					sb.Append(input[i].ToString().PadLeft(3, '0'));
					if (i < len - 1)
						sb.Append(' ');
				}
				return sb.ToString();
			}, delegate(string input)
			{
				// unsupported right now
				return null;
			}, 4),
			new CONVERSION_DATA("Octal", delegate(byte[] input)
			{
				StringBuilder sb = new StringBuilder();
				if (input.Length < 1)
					return null;

				int len = Math.Min(input.Length, 4);
				for (int i = 0; i < len; i++)
				{
					sb.Append(Convert.ToString(input[i], 8).PadLeft(3, '0'));
					if (i < len - 1)
						sb.Append(' ');
				}
				return sb.ToString();
			}, delegate(string input)
			{
				// unsupported right now
				return null;
			}, 4),
			new CONVERSION_DATA("Binary", delegate(byte[] input)
			{
				StringBuilder sb = new StringBuilder();
				if (input.Length < 1)
					return null;

				int len = Math.Min(input.Length, 4);
				for (int i = 0; i < len; i++)
				{
					sb.Append(Convert.ToString(input[i], 2).PadLeft(8, '0'));
					if (i < len - 1)
						sb.Append(' ');
				}
				return sb.ToString();
			}, delegate(string input)
			{
				// unsupported right now
				return null;
			}, 4)
		};

		public BinaryEditor()
		{
			this.Layout = new BoxLayout(UniversalWidgetToolkit.Orientation.Vertical);

			this.hexedit = new HexEditorControl();
			this.hexedit.SelectionChanged += Hexedit_SelectionChanged;
			this.Controls.Add(hexedit, new BoxLayout.Constraints(true, true));

			this.conversionPanel = new Container();
			this.conversionPanel.Layout = new GridLayout();

			int r = 0, c = 0;
			for (int i = 0; i < converters.Length; i++)
			{
				Label lbl = new Label();
				lbl.Text = converters[i].Label;
				this.conversionPanel.Controls.Add(lbl, new GridLayout.Constraints(r, c));

				TextBox txt = new TextBox();
				txt.GotFocus += Txt_GotFocus;
				txt.LostFocus += Txt_LostFocus;
				txt.KeyDown += Txt_KeyDown;
				txt.Text = "---";
				this.conversionPanel.Controls.Add(txt, new GridLayout.Constraints(r, c + 1));
				converters[i].TextBox = txt;
				txt.SetExtraData<CONVERSION_DATA>("converter", converters[i]);

				if ((i + 1) % 4 == 0)
				{
					c += 2; // damn
					r = 0;
				}
				else
				{
					r++;
				}
			}

			this.tabs = new TabContainer();

			TabPage tabPageConverters = new TabPage();
			tabPageConverters.Layout = new BoxLayout(Orientation.Horizontal);
			tabPageConverters.Text = "Numeric Conversion";
			tabPageConverters.Controls.Add(this.conversionPanel);

			this.tabs.TabPages.Add(tabPageConverters);

			this.Controls.Add(tabs, new BoxLayout.Constraints(false, false, 0, BoxLayout.PackType.End));
		}

		void Txt_KeyDown(object sender, UniversalWidgetToolkit.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == UniversalWidgetToolkit.Input.Keyboard.KeyboardKey.Enter)
			{
				TextBox ctl = sender as TextBox;
				CONVERSION_DATA converter = ctl.GetExtraData<CONVERSION_DATA>("converter");

				byte[] data = null;
				try
				{
					data = converter.StringToByteFunc(ctl.Text);
				}
				catch (Exception ex)
				{
					MessageDialog.ShowDialog(ex.Message, "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				}

				if (data != null)
				{
					Array.Copy(data, 0, hexedit.Data, hexedit.SelectionStart.ByteIndex, data.Length);
					Refresh();
				}
				else
				{
					data = new byte[Math.Min(hexedit.Data.Length - hexedit.SelectionStart.ByteIndex, 8)];
					Array.Copy(hexedit.Data, hexedit.SelectionStart.ByteIndex, data, 0, data.Length);

					ctl.Text = converter.ByteToStringFunc(data);
				}
			}
		}


		void Txt_LostFocus(object sender, EventArgs e)
		{
			HexEditorHighlightArea area = hexedit.HighlightAreas["conversion"];
			if (area != null)
				hexedit.HighlightAreas.Remove(area);
		}


		void Txt_GotFocus(object sender, EventArgs e)
		{
			TextBox ctl = sender as TextBox;
			CONVERSION_DATA converter = ctl.GetExtraData<CONVERSION_DATA>("converter");

			HexEditorHighlightArea area = hexedit.HighlightAreas["conversion"];
			if (area == null) area = new HexEditorHighlightArea();

			area.Start = hexedit.SelectionStart.ByteIndex;
			area.Length = converter.MaximumSize;
			area.Color = Colors.LightGray;
			hexedit.HighlightAreas["conversion"] = area;
		}


		private void Hexedit_SelectionChanged(object sender, EventArgs e)
		{
			// this actually worked on the very first try. holy crap.
			byte[] data = new byte[Math.Min(hexedit.Data.Length - hexedit.SelectionStart.ByteIndex, 8)];
			Array.Copy(hexedit.Data, hexedit.SelectionStart.ByteIndex, data, 0, data.Length);

			for (int i = 0; i < converters.Length; i++)
			{
				if (converters[i].TextBox != null)
					converters[i].TextBox.Text = converters[i].ByteToStringFunc(data);
			}
		}


		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			BinaryObjectModel om = (ObjectModel as BinaryObjectModel);
			if (om == null) return;

			this.hexedit.Data = om.Data;
		}
	}
}
