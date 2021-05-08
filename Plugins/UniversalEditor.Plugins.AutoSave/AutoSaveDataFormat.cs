using System;
using System.Reflection;
using UniversalEditor.Accessors;
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.AutoSave
{
	internal class AutoSaveDataFormat : DataFormat
	{
		public float Version { get; set; } = 1.0f;

		public static float MAX_SUPPORTED_VERSION = 1.0f;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader r = Accessor.Reader;
			AutoSaveObjectModel autosave = (objectModel as AutoSaveObjectModel);

			string signature = r.ReadFixedLengthString(12);
			if (!signature.Equals("UE4 AutoSave"))
				throw new InvalidDataFormatException("file does not begin with 'UE4 AutoSave'");

			Version = r.ReadSingle();
			if (Version > MAX_SUPPORTED_VERSION)
			{
				throw new InvalidDataFormatException(String.Format("format version {0} not supported!", Version));
			}

			autosave.OriginalFileName = r.ReadNullTerminatedString();
			autosave.LastUpdateDateTime = r.ReadDateTime();

			long offsetToStringTable = r.ReadInt64();
			r.Accessor.SavePosition();

			r.Seek(offsetToStringTable, SeekOrigin.Begin);
			int stringTableCount = r.ReadInt32();
			for (int i = 0; i < stringTableCount; i++)
			{
				string value = r.ReadNullTerminatedString();
				_StringTable.Add(value);
			}

			r.Accessor.LoadPosition();

			object om = ReadObject(r);
			if (om is ObjectModel)
			{
				autosave.ObjectModel = (om as ObjectModel);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer w = Accessor.Writer;
			AutoSaveObjectModel autosave = (objectModel as AutoSaveObjectModel);

			w.AutoFlush = true; // should be removed for release

			w.WriteFixedLengthString("UE4 AutoSave");
			w.WriteSingle(Version);

			if (autosave.OriginalFileName != null)
			{
				w.WriteNullTerminatedString(autosave.OriginalFileName);
			}
			else
			{
				w.WriteByte(0);
			}

			// last update date time
			w.WriteDateTime(DateTime.Now);

			MemoryAccessor ma = new MemoryAccessor();
			WriteObject(ma.Writer, autosave.ObjectModel);

			ma.Flush();
			ma.Close();

			w.WriteInt64(ma.Length); // offset to string table
			w.WriteBytes(ma.ToArray());

			w.WriteInt32(_StringTable.Count);
			for (int i = 0; i < _StringTable.Count; i++)
			{
				w.WriteNullTerminatedString(_StringTable[i]);
			}
		}

		private System.Collections.Specialized.StringCollection _StringTable = new System.Collections.Specialized.StringCollection();
		private int MakeStringTableEntry(string value)
		{
			if (!_StringTable.Contains(value))
				_StringTable.Add(value);

			return _StringTable.IndexOf(value);
		}

		private object ReadObject(Reader r)
		{
			AutoSaveKnownType typeId = (AutoSaveKnownType) r.ReadInt32();
			switch (typeId)
			{
				case AutoSaveKnownType.Null: return null;
				case AutoSaveKnownType.Object:
				{
					int index = r.ReadInt32();
					string typeName = _StringTable[index];
					break;
				}
			}
			return null;
		}

		private void WriteObject(Writer w, object o)
		{
			if (o == null)
			{
				w.WriteInt32((int)AutoSaveKnownType.Null); // null
				return;
			}

			w.WriteInt32((int)AutoSaveKnownType.Object);

			Type t = o.GetType();

			w.WriteInt32(MakeStringTableEntry(t.FullName));

			// public instance properties are really the only things we care about
			System.Reflection.PropertyInfo[] pis = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

			System.Collections.Generic.List<PropertyInfo> list = new System.Collections.Generic.List<PropertyInfo>();
			for (int i = 0; i < pis.Length; i++)
			{
				if (pis[i].GetCustomAttribute<NonSerializedPropertyAttribute>() != null)
				{
					continue;
				}
				list.Add(pis[i]);
			}
			w.WriteInt32(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				WriteProperty(w, list[i], o);
			}
		}

		private void WriteProperty(Writer w, PropertyInfo propertyInfo, object obj)
		{
			w.WriteInt32(MakeStringTableEntry(propertyInfo.Name));
			w.WriteInt32(MakeStringTableEntry(propertyInfo.PropertyType.FullName));

			object val = propertyInfo.GetValue(obj);

			if (val == obj)
				return;

			WriteValue(w, val);
		}

		private void WriteValue(Writer w, object val)
		{
			if (val is string)
			{
				w.WriteInt32((int)AutoSaveKnownType.String);
				w.WriteInt32(MakeStringTableEntry(val as string));
			}
			else if (val is byte)
			{
				w.WriteInt32((int)AutoSaveKnownType.Byte);
				w.WriteByte((byte)val);
			}
			else if (val is sbyte)
			{
				w.WriteInt32((int)AutoSaveKnownType.SByte);
				w.WriteSByte((sbyte)val);
			}
			else if (val is char)
			{
				w.WriteInt32((int)AutoSaveKnownType.Char);
				w.WriteChar((char)val);
			}
			else if (val is short)
			{
				w.WriteInt32((int)AutoSaveKnownType.Int16);
				w.WriteInt16((short)val);
			}
			else if (val is int)
			{
				w.WriteInt32((int)AutoSaveKnownType.Int32);
				w.WriteInt32((int)val);
			}
			else if (val is long)
			{
				w.WriteInt32((int)AutoSaveKnownType.Int64);
				w.WriteInt64((long)val);
			}
			else if (val is ushort)
			{
				w.WriteInt32((int)AutoSaveKnownType.UInt16);
				w.WriteUInt16((ushort)val);
			}
			else if (val is uint)
			{
				w.WriteInt32((int)AutoSaveKnownType.UInt32);
				w.WriteUInt32((uint)val);
			}
			else if (val is ulong)
			{
				w.WriteInt32((int)AutoSaveKnownType.UInt64);
				w.WriteUInt64((ulong)val);
			}
			else if (val is float)
			{
				w.WriteInt32((int)AutoSaveKnownType.Single);
				w.WriteSingle((float)val);
			}
			else if (val is double)
			{
				w.WriteInt32((int)AutoSaveKnownType.Double);
				w.WriteDouble((double)val);
			}
			else if (val is decimal)
			{
				w.WriteInt32((int)AutoSaveKnownType.Decimal);
				w.WriteDecimal((decimal)val);
			}
			else if (val is Guid)
			{
				w.WriteInt32((int)AutoSaveKnownType.Guid);
				w.WriteGuid((Guid)val);
			}
			else if (val is System.Collections.IList)
			{
				w.WriteInt32((int)AutoSaveKnownType.List);

				System.Collections.IList il = (val as System.Collections.IList);
				w.WriteInt32(il.Count);
				for (int i = 0; i < il.Count; i++)
				{
					WriteValue(w, il[i]);
				}
			}
			else
			{
				w.WriteInt32((int)AutoSaveKnownType.Object);
				WriteObject(w, val);
			}
		}
	}
}
