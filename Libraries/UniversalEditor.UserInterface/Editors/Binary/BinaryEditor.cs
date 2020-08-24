//
//  BinaryEditor.cs - provides a UWT-based hex Editor for manipulating binary data
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
using System.Text;

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.HexEditor;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Binary;
using UniversalEditor.ObjectModels.BinaryGrammar;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Binary
{
	/// <summary>
	/// Provides a UWT-based hex <see cref="Editor" /> for manipulating binary data.
	/// </summary>
	[ContainerLayout("~/Editors/Binary/BinaryEditor.glade")]
	public class BinaryEditor : Editor
	{
		private HexEditorControl hexedit = null;
		private TabContainer tabs = null;

		private Toolbar tbFieldDefinitions;
		private ListViewControl lvFieldDefinitions;
		private DefaultTreeModel tmFieldDefinitions;

		private ComboBox cboEndianness;
		private NumericTextBox txtStart;
		private NumericTextBox txtEnd;
		private NumericTextBox txtLength;

		public override void UpdateSelections()
		{
		}

		protected override void PlaceSelection(Selection selection)
		{
			if (selection is BinarySelection)
			{
				byte[] data = ((BinarySelection)selection).Content as byte[];
				hexedit.Data = data;
			}
		}

		private byte[] ParseBinaryBytes(string content)
		{
			string ct = content?.ToUpper();
			if (ct == null) return null;
			if (ct.Length < 2) return null;

			List<byte> list = new List<byte>();
			for (int i = 0; i < ct.Length; i += 2)
			{
				if ((((int)ct[i] >= (int)'0' && (int)ct[i] <= (int)'9') || ((int)ct[i] >= (int)'A' && (int)ct[i] <= (int)'F'))
					&& (((int)ct[i + 1] >= (int)'0' && (int)ct[i + 1] <= (int)'9') || ((int)ct[i + 1] >= (int)'A' && (int)ct[i + 1] <= (int)'F')))
				{
					list.Add(Byte.Parse(ct[i].ToString() + ct[i + 1], System.Globalization.NumberStyles.HexNumber));
				}
				else if (Char.IsWhiteSpace(ct[i]))
				{
					i--;
					continue;
				}
				else
				{
					return null;
				}
			}
			return list.ToArray();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			if (content == null) return null;
			if (content is string)
			{
				/*
				byte[] data = ParseBinaryBytes(content?.ToString());
				if (data == null)
				{
					return null;
				}
				*/
				byte[] data = System.Text.Encoding.UTF8.GetBytes(content as string);
				return new BinarySelection(data);
			}
			return null;
		}

		protected override void OnObjectModelSaving(System.ComponentModel.CancelEventArgs e)
		{
			base.OnObjectModelSaving(e);

			// flush the current data
			BinaryObjectModel bom = (ObjectModel as BinaryObjectModel);
			if (bom == null) return;

			bom.Data = hexedit.Data;
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

		private struct CONVERSION_DATA
		{
			public string Label;
			public Func<byte[], string> ByteToStringFunc;
			public Func<string, byte[]> StringToByteFunc;
			public TextBox TextBox;
			public int MaximumSize;
			public Type DataType;

			public CONVERSION_DATA(Type dataType, string label, Func<byte[], string> byteToStringFunc, Func<string, byte[]> stringToByteFunc, int maximumSize)
			{
				DataType = dataType;
				Label = label;
				ByteToStringFunc = byteToStringFunc;
				StringToByteFunc = stringToByteFunc;
				TextBox = null;
				MaximumSize = maximumSize;
			}
		}

		private CONVERSION_DATA[] converters = null;

		private byte[] ClampWithEndianness(byte[] input, int length)
		{
			if (input.Length < length)
				return input;

			byte[] output = new byte[length];
			for (int i = 0; i < length; i++)
			{
				output[i] = input[i];
			}

			if (Endianness == IO.Endianness.BigEndian)
			{
				byte[] realOutput = new byte[output.Length];
				for (int j = 0; j < output.Length; j++)
				{
					realOutput[j] = output[output.Length - (j + 1)];
				}
				output = realOutput;
			}
			return output;
		}

		public BinaryEditor()
		{
			InitializeConverters();
			ContextMenuCommandID = "BinaryEditorContextMenu";
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			// HACK: needed until we can fetch ToolbarItems as child controls (they are not Controls)
			(tbFieldDefinitions.Items[0] as ToolbarItemButton).Click += tsbFieldDefinitionAdd_Click;
			(tbFieldDefinitions.Items[1] as ToolbarItemButton).Click += tsbFieldDefinitionEdit_Click;
			(tbFieldDefinitions.Items[2] as ToolbarItemButton).Click += tsbFieldDefinitionRemove_Click;
			(tbFieldDefinitions.Items[4] as ToolbarItemButton).Click += tsbFieldDefinitionLoad_Click;

			(cboEndianness.Model as DefaultTreeModel).Rows[0].SetExtraData<IO.Endianness>("value", IO.Endianness.LittleEndian);
			(cboEndianness.Model as DefaultTreeModel).Rows[1].SetExtraData<IO.Endianness>("value", IO.Endianness.BigEndian);
			(cboEndianness.Model as DefaultTreeModel).Rows[2].SetExtraData<IO.Endianness>("value", IO.Endianness.PDPEndian);

			int r = 0, c = 0;
			for (int i = 0; i < converters.Length; i++)
			{
				Label lbl = new Label();
				lbl.Text = converters[i].Label;
				lbl.HorizontalAlignment = HorizontalAlignment.Right;
				tabs.TabPages[0].Controls.Add(lbl, new GridLayout.Constraints(r, c));

				TextBox txt = new TextBox();
				txt.GotFocus += Txt_GotFocus;
				txt.LostFocus += Txt_LostFocus;
				txt.KeyDown += Txt_KeyDown;
				txt.Text = "---";
				tabs.TabPages[0].Controls.Add(txt, new GridLayout.Constraints(r, c + 1));
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

			Context.AttachCommandEventHandler("EditPasteBinary", delegate (object sender, EventArgs ee)
			{
				string content = Clipboard.Default.GetText();
				byte[] data = ParseBinaryBytes(content);
				hexedit.Data = data;
			});

			OnObjectModelChanged(EventArgs.Empty);
		}

		private void InitializeConverters()
		{
			// why the hell can't I reference non-static METHODS in DELEGATE METHODS in a field initializer?
			// on the off chance they might actually be called within the constructor ???
			// no wait, that doesn't make sense, this is a frickin' FIELD INITIALIZER...
			converters = new CONVERSION_DATA[]
			{
				new CONVERSION_DATA(typeof(sbyte), "Signed 8-bit", delegate(byte[] input)
				{
					if (input.Length < 1)
						return null;

					sbyte b = (sbyte)input[0];
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					sbyte b = SByte.Parse(input, ns);
					return new byte[] { (byte)b };
				}, 1),
				new CONVERSION_DATA(typeof(byte), "Unsigned 8-bit", delegate(byte[] input)
				{
					if (input.Length < 1)
						return null;

					byte b = input[0];
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					byte b = Byte.Parse(input, ns);
					return new byte[] { b };
				}, 1),
				new CONVERSION_DATA(typeof(short), "Signed 16-bit", delegate(byte[] input)
				{
					if (input.Length < 2)
						return null;

					input = ClampWithEndianness(input, 2);

					short b = BitConverter.ToInt16(input, 0);
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					short b = Int16.Parse(input, ns);
					return BitConverter.GetBytes(b);
				}, 2),
				new CONVERSION_DATA(typeof(ushort), "Unsigned 16-bit", delegate(byte[] input)
				{
					if (input.Length < 2)
						return null;

					input = ClampWithEndianness(input, 2);

					ushort b = BitConverter.ToUInt16(input, 0);
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					ushort b = UInt16.Parse(input);
					return BitConverter.GetBytes(b);
				}, 2),
				// Second column
				new CONVERSION_DATA(typeof(int), "Signed 32-bit", delegate(byte[] input)
				{
					if (input.Length < 4)
						return null;

					input = ClampWithEndianness(input, 4);

					int b = BitConverter.ToInt32(input, 0);
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					int b = Int32.Parse(input, ns);
					return BitConverter.GetBytes(b);
				}, 4),
				new CONVERSION_DATA(typeof(uint), "Unsigned 32-bit", delegate(byte[] input)
				{
					if (input.Length < 4)
						return null;

					input = ClampWithEndianness(input, 4);

					uint b = BitConverter.ToUInt32(input, 0);
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}

					uint b = UInt32.Parse(input, ns);
					return BitConverter.GetBytes(b);
				}, 4),
				new CONVERSION_DATA(typeof(float), "Float 32-bit", delegate(byte[] input)
				{
					if (input.Length < 4)
						return null;

					input = ClampWithEndianness(input, 4);

					float b = BitConverter.ToSingle(input, 0);
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else
					{
						ns |= System.Globalization.NumberStyles.AllowDecimalPoint;
					}

					float b = Single.Parse(input, ns);
					return BitConverter.GetBytes(b);
				}, 4),
				new CONVERSION_DATA(typeof(double), "Float 64-bit", delegate(byte[] input)
				{
					if (input.Length < 8)
						return null;

					input = ClampWithEndianness(input, 8);

					double b = BitConverter.ToDouble(input, 0);
					return b.ToString();
				}, delegate(string input)
				{
					System.Globalization.NumberStyles ns = System.Globalization.NumberStyles.None;
					if (input.StartsWith("&H") && input.EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
						ns |= System.Globalization.NumberStyles.HexNumber;
					}
					else
					{
						ns |= System.Globalization.NumberStyles.AllowDecimalPoint;
					}
					double b = Double.Parse(input, ns);
					return BitConverter.GetBytes(b);
				}, 8),
				// Third column
				new CONVERSION_DATA(null, "Hexadecimal", delegate(byte[] input)
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
					if (input.ToLower().StartsWith("0x"))
					{
						input = input.Substring(2);
					}
					else if (input.ToLower().StartsWith("&h") && input.ToLower().EndsWith("&"))
					{
						input = input.Substring(2, input.Length - 3);
					}
					input = input.Replace(" ", String.Empty);

					List<byte> list = new List<byte>();
					if ((input.Length % 2) == 0)
					{
						int nbytes = (input.Length / 2);
						for (int i = 0; i < nbytes; i++)
						{
							string s = input.Substring(i * 2, 2);
							byte b = Byte.Parse(s, System.Globalization.NumberStyles.HexNumber);
							list.Add(b);
						}
					}
					return list.ToArray();
				}, 4),
				new CONVERSION_DATA(null, "Decimal", delegate(byte[] input)
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
					long b = Int64.Parse(input);
					return BitConverter.GetBytes(b);
				}, 4),
				new CONVERSION_DATA(null, "Octal", delegate(byte[] input)
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
				new CONVERSION_DATA(null, "Binary", delegate(byte[] input)
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
		}

		private IO.Endianness _Endianness = IO.Endianness.LittleEndian;
		public IO.Endianness Endianness
		{
			get { return _Endianness; }
			set
			{
				_Endianness = value;
				Hexedit_SelectionChanged(this, EventArgs.Empty);
			}
		}

		[EventHandler(nameof(cboEndianness), "Changed")]
		private void cboEndianness_Changed(object sender, EventArgs e)
		{
			Endianness = cboEndianness.SelectedItem.GetExtraData<IO.Endianness>("value", IO.Endianness.LittleEndian);
		}

		[EventHandler(nameof(txtStart), "Changed")]
		private void txtStart_Changed(object sender, EventArgs e)
		{
			if (hexedit.SelectionStart.ByteIndex != (int)txtStart.Value)
				hexedit.SelectionStart = new HexEditorPosition((int)txtStart.Value, 0);
		}
		[EventHandler(nameof(txtEnd), "Changed")]
		private void txtEnd_Changed(object sender, EventArgs e)
		{
			if (hexedit.SelectionLength.ByteIndex != (int)txtEnd.Value - hexedit.SelectionStart)
				hexedit.SelectionLength = new HexEditorPosition((int)txtEnd.Value - hexedit.SelectionStart, 0);
		}
		[EventHandler(nameof(txtLength), "Changed")]
		private void txtLength_Changed(object sender, EventArgs e)
		{
			if (hexedit.SelectionLength.ByteIndex != (int)txtLength.Value)
				hexedit.SelectionLength = new HexEditorPosition((int)txtLength.Value, 0);
		}

		[EventHandler(nameof(hexedit), "Changing")]
		private void hexedit_Changing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			BeginEdit();
		}
		[EventHandler(nameof(hexedit), "Changed")]
		private void hexedit_Changed(object sender, EventArgs e)
		{
			(ObjectModel as BinaryObjectModel).Data = hexedit.Data;

			EndEdit();

			foreach (TreeModelRow row in tmFieldDefinitions.Rows)
			{
				FieldDefinition def = row.GetExtraData<FieldDefinition>("def");
				if (def == null) continue;

				row.RowColumns[3].Value = GetFieldValue(def);
			}
		}


		[EventHandler(nameof(lvFieldDefinitions), "RowActivated")]
		private void lvFieldDefinitions_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			tsbFieldDefinitionEdit_Click(sender, e);
		}

		private string GetFieldValue(FieldDefinition definition)
		{
			if (definition.DataType == typeof(string))
			{
				byte[] data = new byte[definition.Length];
				if (definition.Offset < hexedit.Data.Length)
					Array.Copy(hexedit.Data, definition.Offset, data, 0, Math.Min(data.Length, hexedit.Data.Length - definition.Offset));

				string value = System.Text.Encoding.UTF8.GetString(data);
				return value;
			}
			else
			{
				foreach (CONVERSION_DATA converter in converters)
				{
					if (converter.DataType == definition.DataType)
					{
						byte[] data = new byte[converter.MaximumSize];
						if (definition.Offset < hexedit.Data.Length)
							Array.Copy(hexedit.Data, definition.Offset, data, 0, Math.Min(data.Length, hexedit.Data.Length - definition.Offset));

						string value = converter.ByteToStringFunc(data);
						return value;
					}
				}
			}
			return String.Empty;
		}

		// [EventHandler(nameof(tsbFieldDefinitionAdd), "Click")]
		private void tsbFieldDefinitionAdd_Click(object sender, EventArgs e)
		{
			FieldDefinitionPropertiesDialog2 dlg = new FieldDefinitionPropertiesDialog2();
			dlg.FieldDefinition.Offset = hexedit.SelectionStart;
			dlg.FieldDefinition.Length = hexedit.SelectionLength;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				tmFieldDefinitions.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmFieldDefinitions.Columns[0], dlg.FieldDefinition.Name),
					new TreeModelRowColumn(tmFieldDefinitions.Columns[1], dlg.FieldDefinition.Offset),
					new TreeModelRowColumn(tmFieldDefinitions.Columns[2], dlg.FieldDefinition.DataType?.Name + " [" + dlg.FieldDefinition.DataTypeSizeString + "]"),
					new TreeModelRowColumn(tmFieldDefinitions.Columns[3], GetFieldValue(dlg.FieldDefinition))
				}));
				tmFieldDefinitions.Rows[tmFieldDefinitions.Rows.Count - 1].SetExtraData<FieldDefinition>("def", dlg.FieldDefinition);

				hexedit.HighlightAreas.Add(new HexEditorHighlightArea(dlg.FieldDefinition.Name, dlg.FieldDefinition.Name, dlg.FieldDefinition.Offset, dlg.FieldDefinition.DataTypeSize, dlg.FieldDefinition.Color));
			}
		}
		// [EventHandler(nameof(tsbFieldDefinitionEdit), "Click")]
		private void tsbFieldDefinitionEdit_Click(object sender, EventArgs e)
		{
			if (lvFieldDefinitions.SelectedRows.Count != 1)
				return;

			FieldDefinition def = lvFieldDefinitions.SelectedRows[0].GetExtraData<FieldDefinition>("def");
			int highlightAreaIndex = hexedit.HighlightAreas.IndexOf(hexedit.HighlightAreas[def.Name]);

			if (def != null)
			{
				FieldDefinitionPropertiesDialog2 dlg = new FieldDefinitionPropertiesDialog2();
				dlg.FieldDefinition = def;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					def = dlg.FieldDefinition;

					hexedit.HighlightAreas[highlightAreaIndex].Name = def.Name;
					hexedit.HighlightAreas[highlightAreaIndex].BackColor = dlg.FieldDefinition.Color;
					hexedit.HighlightAreas[highlightAreaIndex].Start = def.Offset;
					hexedit.HighlightAreas[highlightAreaIndex].Length = def.DataTypeSize;

					lvFieldDefinitions.SelectedRows[0].RowColumns[0].Value = def.Name;
					lvFieldDefinitions.SelectedRows[0].RowColumns[1].Value = def.Offset;
					lvFieldDefinitions.SelectedRows[0].RowColumns[2].Value = def.DataType?.Name + " [" + dlg.FieldDefinition.DataTypeSizeString + "]";
					lvFieldDefinitions.SelectedRows[0].RowColumns[3].Value = GetFieldValue(def);
				}
			}
		}
		// [EventHandler(nameof(tsbFieldDefinitionRemove), "Click")]
		private void tsbFieldDefinitionRemove_Click(object sender, EventArgs e)
		{
		}
		// [EventHandler(nameof(tsbFieldDefinitionLoad), "Click")]
		private void tsbFieldDefinitionLoad_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;

			BinaryGrammarObjectModel om = new BinaryGrammarObjectModel();
			Association[] assocs = Association.FromObjectModelOrDataFormat(om.MakeReference());
			dlg.AddFileNameFilterFromAssociations("Grammar definition files", assocs);

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				FileAccessor fa = new FileAccessor(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1]);
				DataFormat df = assocs[0].DataFormats[0].Create(); // FIXME: THIS SHOULD BE PROPERLY INFERRED
				Document.Load(om, df, fa);
			}
		}

		private void Txt_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Enter)
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
					StringBuilder sb = new StringBuilder();
					if (converter.DataType != null)
					{
						object minValue = null;
						object maxValue = null;

						System.Reflection.FieldInfo fiMaxValue = converter.DataType.GetField("MaxValue");
						System.Reflection.FieldInfo fiMinValue = converter.DataType.GetField("MinValue");
						if (fiMaxValue != null && fiMinValue != null)
						{
							minValue = fiMinValue.GetValue(null);
							maxValue = fiMaxValue.GetValue(null);
						}

						if (!(ex is OverflowException) || (fiMinValue == null || fiMaxValue == null))
						{
							sb.AppendLine(ex.Message);
							sb.AppendLine();
						}

						if (fiMinValue != null && fiMaxValue != null)
						{
							minValue = fiMinValue.GetValue(null);
							maxValue = fiMaxValue.GetValue(null);
							sb.AppendLine(String.Format("Value for {0} must be between {1} and {2}", converter.DataType.Name, minValue, maxValue));
						}
					}
					MessageDialog.ShowDialog(sb.ToString(), "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				}

				if (data != null)
				{
					if (hexedit.SelectionStart + data.Length >= hexedit.Data.Length)
					{
						byte[] odata = hexedit.Data;
						Array.Resize<byte>(ref odata, odata.Length + data.Length);
						hexedit.Data = odata;
					}
					Array.Copy(data, 0, hexedit.Data, hexedit.SelectionStart.ByteIndex, data.Length);

					Refresh();
				}
				else
				{
					data = new byte[Math.Min(hexedit.Data.Length - hexedit.SelectionStart.ByteIndex, 8)];
					Array.Copy(hexedit.Data, hexedit.SelectionStart.ByteIndex, data, 0, data.Length);
				}

				// update the rest of the converters
				Hexedit_SelectionChanged(sender, e);
			}
		}


		private void Txt_LostFocus(object sender, EventArgs e)
		{
			HexEditorHighlightArea area = hexedit.HighlightAreas["conversion"];
			if (area != null)
				hexedit.HighlightAreas.Remove(area);
		}


		private void Txt_GotFocus(object sender, EventArgs e)
		{
			TextBox ctl = sender as TextBox;
			CONVERSION_DATA converter = ctl.GetExtraData<CONVERSION_DATA>("converter");

			HexEditorHighlightArea area = hexedit.HighlightAreas["conversion"];
			if (area == null) area = new HexEditorHighlightArea();

			area.Start = hexedit.SelectionStart.ByteIndex;
			area.Length = converter.MaximumSize;
			area.BackColor = Colors.LightGray;
			area.ForeColor = Colors.Black;
			hexedit.HighlightAreas["conversion"] = area;
		}

		[EventHandler(nameof(hexedit), "SelectionChanged")]
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
			txtStart.Value = hexedit.SelectionStart.ByteIndex;
			txtEnd.Value = (hexedit.SelectionStart.ByteIndex + hexedit.SelectionLength.ByteIndex);
			txtLength.Value = hexedit.SelectionLength.ByteIndex;

			UpdateFieldDefinitions();
		}

		private void UpdateFieldDefinitions()
		{
			foreach (TreeModelRow row in tmFieldDefinitions.Rows)
			{
				FieldDefinition def = row.GetExtraData<FieldDefinition>("def");
				if (def == null) continue;

				row.RowColumns[3].Value = GetFieldValue(def);
			}
		}


		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			BinaryObjectModel om = (ObjectModel as BinaryObjectModel);
			if (om == null) return;

			if (!IsCreated) return;

			hexedit.Data = om.Data;
		}
	}
}
