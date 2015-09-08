using System;
using System.Collections.Generic;
using System.Text;

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
				if (cdfi is CustomDataFormatItemField)
				{
					CustomDataFormatItemField fld = (cdfi as CustomDataFormatItemField);
					object value = null;
					switch (fld.DataType)
					{
						case "Boolean":
						{
							value = br.ReadBoolean();
							break;
						}
						case "Byte":
						{
							value = br.ReadByte();
							break;
						}
						case "Char":
						{
							value = br.ReadChar();
							break;
						}
						case "DateTime":
						{
							value = br.ReadDateTime();
							break;
						}
						case "Decimal":
						{
							value = br.ReadDecimal();
							break;
						}
						case "Double":
						{
							value = br.ReadDouble();
							break;
						}
						case "Guid":
						{
							value = br.ReadGuid();
							break;
						}
						case "Int16":
						{
							value = br.ReadInt16();
							break;
						}
						case "Int32":
						{
							value = br.ReadInt32();
							break;
						}
						case "Int64":
						{
							value = br.ReadInt64();
							break;
						}
						case "SByte":
						{
							value = br.ReadSByte();
							break;
						}
						case "Single":
						{
							value = br.ReadSingle();
							break;
						}
						case "String":
						{
							value = br.ReadLengthPrefixedString();
							break;
						}
						case "UInt16":
						{
							value = br.ReadUInt16();
							break;
						}
						case "UInt32":
						{
							value = br.ReadUInt32();
							break;
						}
						case "UInt64":
						{
							value = br.ReadUInt64();
							break;
						}
						case "TerminatedString":
						{
							IO.Encoding encoding = IO.Encoding.Default;
							if (fld.Encoding != null) encoding = fld.Encoding;

							if (fld.Length.HasValue)
							{
								value = br.ReadNullTerminatedString(fld.Length.Value, encoding);
							}
							else
							{
								value = br.ReadNullTerminatedString(encoding);
							}
							break;
						}
						case "FixedString":
						{
							IO.Encoding encoding = IO.Encoding.Default;
							if (fld.Encoding != null) encoding = fld.Encoding;

							if (!fld.Length.HasValue)
							{
								throw new InvalidOperationException();
							}
							value = br.ReadFixedLengthString(fld.Length.Value, encoding);
							break;
						}
					}

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

		private string ReplaceVariables(string p, Dictionary<string, object> localVariables)
		{
			string retval = p;
			foreach (string key in localVariables.Keys)
			{
				retval = retval.Replace("$(" + key + ")", localVariables[key].ToString());
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

						string methodName = variableStr.Substring(variableStr.IndexOf(':'));

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
					object value = ExpandVariables(fld.Value);

					switch (fld.DataType)
					{
						case "Boolean":
						{
							value = (value.ToString().ToLower().Equals("true"));
							bw.WriteBoolean((bool)value);
							break;
						}
						case "Byte":
						{
							value = Byte.Parse(value.ToString());
							bw.WriteByte((byte)value);
							break;
						}
						case "Char":
						{
							value = Char.Parse(value.ToString());
							bw.WriteChar((char)value);
							break;
						}
						case "DateTime":
						{
							value = DateTime.Parse(value.ToString());
							bw.WriteDateTime((DateTime)value);
							break;
						}
						case "Decimal":
						{
							value = Decimal.Parse(value.ToString());
							bw.WriteDecimal((decimal)value);
							break;
						}
						case "Double":
						{
							value = Double.Parse(value.ToString());
							bw.WriteDouble((double)value);
							break;
						}
						case "Guid":
						{
							value = Guid.Parse(value.ToString());
							bw.WriteGuid((Guid)value);
							break;
						}
						case "Int16":
						{
							value = Int16.Parse(value.ToString());
							bw.WriteInt16((short)value);
							break;
						}
						case "Int32":
						{
							value = Int32.Parse(value.ToString());
							bw.WriteInt32((int)value);
							break;
						}
						case "Int64":
						{
							value = Int64.Parse(value.ToString());
							bw.WriteInt64((long)value);
							break;
						}
						case "SByte":
						{
							value = SByte.Parse(value.ToString());
							bw.WriteSByte((sbyte)value);
							break;
						}
						case "Single":
						{
							value = Single.Parse(value.ToString());
							bw.WriteSingle((float)value);
							break;
						}
						case "String":
						{
							bw.WriteLengthPrefixedString((string)value);
							break;
						}
						case "UInt16":
						{
							value = UInt16.Parse(value.ToString());
							bw.WriteUInt16((ushort)value);
							break;
						}
						case "UInt32":
						{
							value = UInt32.Parse(value.ToString());
							bw.WriteUInt32((uint)value);
							break;
						}
						case "UInt64":
						{
							value = UInt64.Parse(value.ToString());
							bw.WriteUInt64((ulong)value);
							break;
						}
						case "TerminatedString":
						{
							IO.Encoding encoding = IO.Encoding.Default;
							if (fld.Encoding != null) encoding = fld.Encoding;

							if (fld.Length.HasValue)
							{
								bw.WriteNullTerminatedString((string)value, encoding, fld.Length.Value);
							}
							else
							{
								bw.WriteNullTerminatedString((string)value, encoding);
							}
							break;
						}
						case "FixedString":
						{
							IO.Encoding encoding = IO.Encoding.Default;
							if (fld.Encoding != null) encoding = fld.Encoding;
							if (fld.Value == null) continue;

							if (fld.Length != null)
							{
								bw.WriteFixedLengthString(fld.Value, encoding, fld.Length.Value);
							}
							else
							{
								bw.WriteFixedLengthString(fld.Value, encoding);
							}
							break;
						}
					}

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
	}
	public class CustomDataFormatReference : DataFormatReference
	{
		public CustomDataFormatReference() : base(typeof(CustomDataFormat))
		{
		}

		private CustomDataFormatItem.CustomDataFormatItemCollection mvarItems = new CustomDataFormatItem.CustomDataFormatItemCollection();
		public CustomDataFormatItem.CustomDataFormatItemCollection Items
		{
			get { return mvarItems; }
		}
	}
}
