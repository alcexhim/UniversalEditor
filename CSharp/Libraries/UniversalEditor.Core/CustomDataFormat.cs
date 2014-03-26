using System;
using System.Collections.Generic;

namespace UniversalEditor
{
	public class CustomDataFormat : DataFormat
	{
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

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
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
