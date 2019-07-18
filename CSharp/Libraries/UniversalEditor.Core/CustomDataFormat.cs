//
//  CustomDataFormat.cs - provide a way to declare DataFormats in platform-agnostic XML
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor
{
	public class CustomDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (this.Reference == null) return base.MakeReferenceInternal();
			return this.Reference;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CustomDataFormatReference cdfr = (base.Reference as CustomDataFormatReference);
			if (cdfr == null) return;

			Dictionary<string, object> localVariables = new Dictionary<string, object>();

			IO.Reader br = base.Accessor.Reader;
			foreach (CustomDataFormatItem cdfi in cdfr.Items)
			{
				object value = ReadObject(br, cdfi);

				if (cdfi is CustomDataFormatItemField)
				{
					CustomDataFormatItemField fld = (cdfi as CustomDataFormatItemField);
					if (fld.Name != null)
					{
						string fldName = ReplaceVariables(fld.Name, localVariables);
						if (localVariables.ContainsKey(fldName))
						{
							localVariables[fldName] = value;
						}
						else
						{
							localVariables.Add(fldName, value);
						}
					}
					if (fld.ExportTarget != null)
					{
					}
				}
			}
		}

		private object ReadObject(Reader reader, CustomDataFormatItem item)
		{
			object value = null;
			if (item is CustomDataFormatItemField)
			{
				CustomDataFormatItemField itm = (item as CustomDataFormatItemField);
				switch (itm.DataType)
				{
					case "Boolean":
					{
						value = reader.ReadBoolean();
						break;
					}
					case "Byte":
					{
						value = reader.ReadByte();
						break;
					}
					case "Char":
					{
						value = reader.ReadChar();
						break;
					}
					case "DateTime":
					{
						value = reader.ReadDateTime();
						break;
					}
					case "Decimal":
					{
						value = reader.ReadDecimal();
						break;
					}
					case "Double":
					{
						value = reader.ReadDouble();
						break;
					}
					case "Guid":
					{
						value = reader.ReadGuid();
						break;
					}
					case "Int16":
					{
						value = reader.ReadInt16();
						break;
					}
					case "Int32":
					{
						value = reader.ReadInt32();
						break;
					}
					case "Int64":
					{
						value = reader.ReadInt64();
						break;
					}
					case "SByte":
					{
						value = reader.ReadSByte();
						break;
					}
					case "Single":
					{
						value = reader.ReadSingle();
						break;
					}
					case "String":
					{
						value = reader.ReadLengthPrefixedString();
						break;
					}
					case "Structure":
					{
						value = ReadStructure(reader, itm.StructureID);
						break;
					}
					case "UInt16":
					{
						value = reader.ReadUInt16();
						break;
					}
					case "UInt32":
					{
						value = reader.ReadUInt32();
						break;
					}
					case "UInt64":
					{
						value = reader.ReadUInt64();
						break;
					}
					case "TerminatedString":
					{
						IO.Encoding encoding = IO.Encoding.Default;
						if (itm.Encoding != null) encoding = itm.Encoding;

						if (itm.Length.HasValue)
						{
							value = reader.ReadNullTerminatedString(itm.Length.Value, encoding);
						}
						else
						{
							value = reader.ReadNullTerminatedString(encoding);
						}
						break;
					}
					case "FixedString":
					{
						IO.Encoding encoding = IO.Encoding.Default;
						if (itm.Encoding != null) encoding = itm.Encoding;

						if (!itm.Length.HasValue)
						{
							throw new InvalidOperationException();
						}
						value = reader.ReadFixedLengthString(itm.Length.Value, encoding);
						break;
					}
				}
			}
			return value;
		}

		private object ReadStructure(Reader reader, Guid id)
		{
			CustomDataFormatReference cdfr = (base.Reference as CustomDataFormatReference);
			if (cdfr == null) throw new InvalidOperationException("Must have a CustomDataFormatReference");

			CustomDataFormatStructure type = cdfr.Structures[id];
			CustomDataFormatStructureInstance inst = type.CreateInstance();

			foreach (CustomDataFormatItem item in type.Items)
			{
				CustomDataFormatItemField cdfif = (inst.Items[item.Name] as CustomDataFormatItemField);
				if (cdfif != null)
				{
					cdfif.Value = ReadObject(reader, item);
				}
			}
			return inst;
		}

		private void WriteStructure(Writer writer, CustomDataFormatStructure structure)
		{
			CustomDataFormatReference cdfr = (base.Reference as CustomDataFormatReference);
			if (cdfr == null) throw new InvalidOperationException("Must have a CustomDataFormatReference");

			foreach (CustomDataFormatItem item in structure.Items)
			{
				WriteObject(writer, item);
			}
		}

		private string ReplaceVariables(string p, Dictionary<string, object> localVariables)
		{
			string retval = p;
			foreach (string key in localVariables.Keys)
			{
				string value = String.Empty;
				if (localVariables[key] != null) value = localVariables[key].ToString();

				retval = retval.Replace("$(Local:" + key + ")", value);
			}
			return retval;
		}

		private string ExpandVariables(string value)
		{
			StringBuilder sb = new StringBuilder();

			bool isEscaping = false;
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '$')
				{
					if (isEscaping)
					{
						sb.Append(value[i]);
						isEscaping = false;
						continue;
					}

					i++;
					if (i >= value.Length) break;

					if (value[i] == '(')
					{
						i++;
						if (i >= value.Length) break;

						int lastIndex = value.IndexOf(')', i);
						int count = 0;
						for (int j = i; j < value.Length; j++)
						{
							if (value[j] == '(') count++;
							if (value[j] == ')')
							{
								if (count == 0)
								{
									lastIndex = j;
									break;
								}
								else
								{
									count--;
								}
							}
						}

						string variableStr = value.Substring(i, lastIndex - i);
						string sectionName = null;
						if (variableStr.Contains(":")) sectionName = variableStr.Substring(0, variableStr.IndexOf(':'));

						string methodOrPropertyName = variableStr.Substring(variableStr.IndexOf(':') + 1);

						switch (sectionName)
						{
							case "CustomOption":
							{
								// this is a CustomOption property
								CustomOption co = mvarReference.ExportOptions[methodOrPropertyName];
								if (co != null)
								{
									string bw = co.GetValue().ToString();
									sb.Append(bw);
								}
								break;
							}
						}

						i += variableStr.Length + 1; // $(...)
					}
				}
				else if (value[i] == '\\')
				{
					if (isEscaping)
					{
						sb.Append(value[i]);
						isEscaping = false;
						continue;
					}
					else
					{
						isEscaping = true;
					}
				}
				else
				{
					sb.Append(value[i]);
				}
			}
			return sb.ToString();
		}

		private bool EvaluateCondition(CustomDataFormatFieldCondition condition)
		{
			bool retval = false;

			string value = ExpandVariables(condition.Variable);

			bool bValue1 = (value.ToLower().Equals("true"));
			bool bValue2 = (condition.Value.ToLower().Equals("true"));

			retval = (bValue1 == bValue2);

			return retval;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			CustomDataFormatReference cdfr = (this.Reference as CustomDataFormatReference);
			Type type = this.Reference.GetType();
			if (cdfr == null) return;

			Dictionary<string, object> localVariables = new Dictionary<string, object>();
			IO.Writer bw = base.Accessor.Writer;
			foreach (CustomDataFormatItem cdfi in cdfr.Items)
			{
				if (cdfi is CustomDataFormatItemField)
				{
					CustomDataFormatItemField fld = (cdfi as CustomDataFormatItemField);
					
					WriteObject(bw, fld);

					if (fld.Name != null)
					{
						string fldName = ReplaceVariables(fld.Name, localVariables);
						if (localVariables.ContainsKey(fldName))
						{
							localVariables[fldName] = fld.Value;
						}
						else
						{
							localVariables.Add(fldName, fld.Value);
						}
					}
					if (fld.ExportTarget != null)
					{
					}
				}
			}
		}

		private void WriteObject(Writer writer, CustomDataFormatItem item)
		{
			object value = null;

			if (item is CustomDataFormatItemField)
			{ 
				CustomDataFormatItemField fld = (item as CustomDataFormatItemField);
				if (fld != null)
				{
					if (fld.FieldCondition == null)
					{
						if (fld.Value == null) return;
						value = ExpandVariables(fld.Value.ToString());
					}
					else
					{
						if (EvaluateCondition(fld.FieldCondition))
						{
							value = ExpandVariables(fld.FieldCondition.TrueResult);
						}
						else
						{
							value = ExpandVariables(fld.FieldCondition.FalseResult);
						}
					}
				}

				switch (fld.DataType)
				{
					case "Boolean":
					{
						value = (value.ToString().ToLower().Equals("true"));
						writer.WriteBoolean((bool)value);
						break;
					}
					case "Byte":
					{
						value = Byte.Parse(value.ToString());
						writer.WriteByte((byte)value);
						break;
					}
					case "Char":
					{
						value = Char.Parse(value.ToString());
						writer.WriteChar((char)value);
						break;
					}
					case "DateTime":
					{
						value = DateTime.Parse(value.ToString());
						writer.WriteDateTime((DateTime)value);
						break;
					}
					case "Decimal":
					{
						value = Decimal.Parse(value.ToString());
						writer.WriteDecimal((decimal)value);
						break;
					}
					case "Double":
					{
						value = Double.Parse(value.ToString());
						writer.WriteDouble((double)value);
						break;
					}
					case "Guid":
					{
						value = Guid.Parse(value.ToString());
						writer.WriteGuid((Guid)value);
						break;
					}
					case "Int16":
					{
						value = Int16.Parse(value.ToString());
						writer.WriteInt16((short)value);
						break;
					}
					case "Int32":
					{
						value = Int32.Parse(value.ToString());
						writer.WriteInt32((int)value);
						break;
					}
					case "Int64":
					{
						value = Int64.Parse(value.ToString());
						writer.WriteInt64((long)value);
						break;
					}
					case "SByte":
					{
						value = SByte.Parse(value.ToString());
						writer.WriteSByte((sbyte)value);
						break;
					}
					case "Single":
					{
						value = Single.Parse(value.ToString());
						writer.WriteSingle((float)value);
						break;
					}
					case "String":
					{
						writer.WriteLengthPrefixedString((string)value);
						break;
					}
					case "Structure":
					{
						writer.WriteLengthPrefixedString((string)value);
						break;
					}
					case "UInt16":
					{
						value = UInt16.Parse(value.ToString());
						writer.WriteUInt16((ushort)value);
						break;
					}
					case "UInt32":
					{
						value = UInt32.Parse(value.ToString());
						writer.WriteUInt32((uint)value);
						break;
					}
					case "UInt64":
					{
						value = UInt64.Parse(value.ToString());
						writer.WriteUInt64((ulong)value);
						break;
					}
					case "TerminatedString":
					{
						IO.Encoding encoding = IO.Encoding.Default;
						if (fld.Encoding != null) encoding = fld.Encoding;

						if (fld.Length.HasValue)
						{
							writer.WriteNullTerminatedString((string)value, encoding, fld.Length.Value);
						}
						else
						{
							writer.WriteNullTerminatedString((string)value, encoding);
						}
						break;
					}
					case "FixedString":
					{
						IO.Encoding encoding = IO.Encoding.Default;
						if (fld.Encoding != null) encoding = fld.Encoding;
							
						if (fld.Length != null)
						{
							writer.WriteFixedLengthString((string)value, encoding, fld.Length.Value);
						}
						else
						{
							writer.WriteFixedLengthString((string)value, encoding);
						}
						break;
					}
				}
			}
		}
	}
	public class CustomDataFormatReference : DataFormatReference
	{
		public CustomDataFormatReference() : base(typeof(CustomDataFormat))
		{
		}

		private CustomDataFormatStructure.CustomDataFormatStructureCollection mvarStructures = new CustomDataFormatStructure.CustomDataFormatStructureCollection();
		public CustomDataFormatStructure.CustomDataFormatStructureCollection Structures { get { return mvarStructures; } }

		private CustomDataFormatItem.CustomDataFormatItemCollection mvarItems = new CustomDataFormatItem.CustomDataFormatItemCollection();
		public CustomDataFormatItem.CustomDataFormatItemCollection Items
		{
			get { return mvarItems; }
		}
	}
}
