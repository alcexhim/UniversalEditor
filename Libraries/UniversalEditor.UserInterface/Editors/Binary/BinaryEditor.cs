﻿//
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.HexEditor;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Layouts;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.BinaryGrammar;

namespace UniversalEditor.Editors.Binary
{
	public class BinaryEditor : Editor
	{
		public override void UpdateSelections()
		{
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
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

		private HexEditorControl hexedit = null;
		private Container conversionPanel = null;
		private TabContainer tabs = null;

		private Toolbar tbFieldDefinitions;
		private ListView lvFieldDefinitions;
		private DefaultTreeModel tmFieldDefinitions;

		private Container pnlSettings;
		private Label lblEndianness;
		private ComboBox cboEndianness;
		private Label lblStart;
		private TextBox txtStart;
		private Label lblEnd;
		private TextBox txtEnd;
		private Label lblLength;
		private TextBox txtLength;

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
			this.Layout = new BoxLayout(MBS.Framework.UserInterface.Orientation.Vertical);

			this.hexedit = new HexEditorControl();
			this.hexedit.SelectionChanged += Hexedit_SelectionChanged;

			this.hexedit.Changing += hexedit_Changing;
			this.hexedit.Changed += hexedit_Changed;

			this.conversionPanel = new Container();
			this.conversionPanel.Layout = new GridLayout();

			InitializeConverters();

			int r = 0, c = 0;
			for (int i = 0; i < converters.Length; i++)
			{
				Label lbl = new Label();
				lbl.Text = converters[i].Label;
				lbl.HorizontalAlignment = HorizontalAlignment.Right;
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

			this.pnlSettings = new Container();
			this.pnlSettings.Layout = new BoxLayout(Orientation.Horizontal);

			this.lblEndianness = new Label();
			this.lblEndianness.Text = "_Endianness: ";
			this.pnlSettings.Controls.Add(this.lblEndianness, new BoxLayout.Constraints(false, false));
			this.cboEndianness = new ComboBox();
			this.cboEndianness.ReadOnly = true;

			DefaultTreeModel tmEndianness = new DefaultTreeModel(new Type[] { typeof(string) });
			tmEndianness.Rows.AddRange(new TreeModelRow[]
			{
				new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmEndianness.Columns[0], "Little-endian")
				}),
				new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmEndianness.Columns[0], "Big-endian")
				})
			});
			tmEndianness.Rows[0].SetExtraData<IO.Endianness>("value", IO.Endianness.LittleEndian);
			tmEndianness.Rows[1].SetExtraData<IO.Endianness>("value", IO.Endianness.BigEndian);

			this.cboEndianness.Model = tmEndianness;
			this.cboEndianness.SelectedItem = tmEndianness.Rows[0];
			this.cboEndianness.Changed += cboEndianness_Changed;

			this.pnlSettings.Controls.Add(this.cboEndianness, new BoxLayout.Constraints(false, false));


			this.lblStart = new Label();
			this.lblStart.Text = "_Start: ";
			this.pnlSettings.Controls.Add(this.lblStart, new BoxLayout.Constraints(false, false));

			this.txtStart = new TextBox();
			this.txtStart.KeyDown += txtStart_KeyDown;
			this.pnlSettings.Controls.Add(this.txtStart, new BoxLayout.Constraints(false, false));

			this.lblEnd = new Label();
			this.lblEnd.Text = "_End: ";
			this.pnlSettings.Controls.Add(this.lblEnd, new BoxLayout.Constraints(false, false));

			this.txtEnd = new TextBox();
			this.txtEnd.KeyDown += txtEnd_KeyDown;
			this.pnlSettings.Controls.Add(this.txtEnd, new BoxLayout.Constraints(false, false));

			this.lblLength = new Label();
			this.lblLength.Text = "_Length: ";
			this.pnlSettings.Controls.Add(this.lblLength, new BoxLayout.Constraints(false, false));

			this.txtLength = new TextBox();
			this.txtLength.KeyDown += txtLength_KeyDown;
			this.pnlSettings.Controls.Add(this.txtLength, new BoxLayout.Constraints(false, false));

			this.tabs = new TabContainer();

			TabPage tabPageConverters = new TabPage();
			tabPageConverters.Layout = new BoxLayout(Orientation.Vertical);
			tabPageConverters.Text = "Numeric Conversion";
			tabPageConverters.Controls.Add(this.conversionPanel);
			this.tabs.TabPages.Add(tabPageConverters);

			TabPage tabPageFields = new TabPage();
			tabPageFields.Layout = new BoxLayout(Orientation.Vertical);
			tabPageFields.Text = "Field Definitions";

			this.tbFieldDefinitions = new Toolbar();
			this.tbFieldDefinitions.Items.Add(new ToolbarItemButton("tsbFieldDefinitionAdd", StockType.Add, tsbFieldDefinitionAdd_Click));
			this.tbFieldDefinitions.Items.Add(new ToolbarItemButton("tsbFieldDefinitionEdit", StockType.Edit, tsbFieldDefinitionEdit_Click));
			this.tbFieldDefinitions.Items.Add(new ToolbarItemButton("tsbFieldDefinitionRemove", StockType.Remove, tsbFieldDefinitionRemove_Click));
			this.tbFieldDefinitions.Items.Add(new ToolbarItemSeparator());
			this.tbFieldDefinitions.Items.Add(new ToolbarItemButton("tsbFieldDefinitionLoad", StockType.Open, tsbFieldDefinitionLoad_Click));
			this.tbFieldDefinitions.Items.Add(new ToolbarItemButton("tsbFieldDefinitionSave", StockType.Save, tsbFieldDefinitionLoad_Click));
			tabPageFields.Controls.Add(this.tbFieldDefinitions, new BoxLayout.Constraints(false, true));

			this.tmFieldDefinitions = new DefaultTreeModel(new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) });

			this.lvFieldDefinitions = new ListView();
			this.lvFieldDefinitions.Model = tmFieldDefinitions;
			this.lvFieldDefinitions.RowActivated += lvFieldDefinitions_RowActivated;
			this.lvFieldDefinitions.Columns.Add(new ListViewColumnText(tmFieldDefinitions.Columns[0], "Name"));
			this.lvFieldDefinitions.Columns.Add(new ListViewColumnText(tmFieldDefinitions.Columns[1], "Offset"));
			this.lvFieldDefinitions.Columns.Add(new ListViewColumnText(tmFieldDefinitions.Columns[2], "Data Type [Size]"));
			this.lvFieldDefinitions.Columns.Add(new ListViewColumnText(tmFieldDefinitions.Columns[3], "Value"));
			tabPageFields.Controls.Add(this.lvFieldDefinitions, new BoxLayout.Constraints(true, true));
			this.tabs.TabPages.Add(tabPageFields);


			this.Controls.Add(pnlSettings, new BoxLayout.Constraints(false, false, 0, BoxLayout.PackType.Start));
			this.Controls.Add(hexedit, new BoxLayout.Constraints(true, true));
			this.Controls.Add(tabs, new BoxLayout.Constraints(false, false, 0, BoxLayout.PackType.End));
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

		void cboEndianness_Changed(object sender, EventArgs e)
		{
			Endianness = cboEndianness.SelectedItem.GetExtraData<IO.Endianness>("value", IO.Endianness.LittleEndian);
		}

		void txtStart_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Enter)
			{
				if (Int32.TryParse(txtStart.Text, out int val))
				{
					hexedit.SelectionStart = new HexEditorPosition(val, 0);
				}
				else
				{
					if (MessageDialog.ShowDialog("Invalid value for Int32", "error", MessageDialogButtons.RetryCancel, MessageDialogIcon.Error) == DialogResult.Cancel)
					{
						txtStart.Text = hexedit.SelectionStart.ByteIndex.ToString();
					}
				}
			}
		}
		void txtEnd_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Enter)
			{
				if (Int32.TryParse(txtEnd.Text, out int val))
				{
					hexedit.SelectionLength = new HexEditorPosition(val - hexedit.SelectionStart, 0);
				}
				else
				{
					if (MessageDialog.ShowDialog("Invalid value for Int32", "error", MessageDialogButtons.RetryCancel, MessageDialogIcon.Error) == DialogResult.Cancel)
					{
						txtEnd.Text = (hexedit.SelectionStart.ByteIndex + hexedit.SelectionLength.ByteIndex).ToString();
					}
				}
			}
		}
		void txtLength_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Enter)
			{
				if (Int32.TryParse(txtLength.Text, out int val))
				{
					hexedit.SelectionLength = new HexEditorPosition(val, 0);
				}
				else
				{
					if (MessageDialog.ShowDialog("Invalid value for Int32", "error", MessageDialogButtons.RetryCancel, MessageDialogIcon.Error) == DialogResult.Cancel)
					{
						txtEnd.Text = hexedit.SelectionLength.ByteIndex.ToString();
					}
				}
			}
		}

		private void hexedit_Changing(object sender, EventArgs e)
		{
			BeginEdit();
		}
		private void hexedit_Changed(object sender, EventArgs e)
		{
			EndEdit();

			foreach (TreeModelRow row in tmFieldDefinitions.Rows)
			{
				FieldDefinition def = row.GetExtraData<FieldDefinition>("def");
				if (def == null) continue;

				row.RowColumns[3].Value = GetFieldValue(def);
			}
		}


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

		private void tsbFieldDefinitionAdd_Click(object sender, EventArgs e)
		{
			FieldDefinitionPropertiesDialog dlg = new FieldDefinitionPropertiesDialog();
			dlg.FieldDefinition.Offset = hexedit.SelectionStart;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				tmFieldDefinitions.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmFieldDefinitions.Columns[0], dlg.FieldDefinition.Name),
					new TreeModelRowColumn(tmFieldDefinitions.Columns[1], dlg.FieldDefinition.Offset),
					new TreeModelRowColumn(tmFieldDefinitions.Columns[2], dlg.FieldDefinition.DataType.Name + " [" + dlg.FieldDefinition.DataTypeSizeString + "]"),
					new TreeModelRowColumn(tmFieldDefinitions.Columns[3], GetFieldValue(dlg.FieldDefinition))
				}));
				tmFieldDefinitions.Rows[tmFieldDefinitions.Rows.Count - 1].SetExtraData<FieldDefinition>("def", dlg.FieldDefinition);

				hexedit.HighlightAreas.Add(new HexEditorHighlightArea(dlg.FieldDefinition.Name, dlg.FieldDefinition.Name, dlg.FieldDefinition.Offset, dlg.FieldDefinition.DataTypeSize, dlg.FieldDefinition.Color));
			}
		}
		private void tsbFieldDefinitionEdit_Click(object sender, EventArgs e)
		{
			if (lvFieldDefinitions.SelectedRows.Count != 1)
				return;

			FieldDefinition def = lvFieldDefinitions.SelectedRows[0].GetExtraData<FieldDefinition>("def");
			if (def != null)
			{
				FieldDefinitionPropertiesDialog dlg = new FieldDefinitionPropertiesDialog();
				dlg.FieldDefinition = def;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					def = dlg.FieldDefinition;
				}
			}
		}
		private void tsbFieldDefinitionRemove_Click(object sender, EventArgs e)
		{
		}
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

		void Txt_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
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
			txtStart.Text = hexedit.SelectionStart.ByteIndex.ToString();
			txtEnd.Text = (hexedit.SelectionStart.ByteIndex + hexedit.SelectionLength.ByteIndex).ToString();
			txtLength.Text = hexedit.SelectionLength.ByteIndex.ToString();

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

			hexedit.Data = om.Data;
		}
	}
}