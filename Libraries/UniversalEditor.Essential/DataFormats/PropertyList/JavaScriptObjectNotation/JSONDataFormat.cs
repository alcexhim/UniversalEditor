//
//  JSONDataFormat.cs - provides a DataFormat for manipulating markup in JavaScript Object Notation (JSON) format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.JavaScriptObjectNotation
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating markup in JavaScript Object Notation (JSON) format.
	/// </summary>
	public class JSONDataFormat : DataFormat
	{
		private class _Context
		{
			public bool CloseGroup { get; set; } = false;
			public string CurrentStringRaw { get; set; } = String.Empty;

			public bool InsideArray { get; set; } = false;
			public System.Collections.Generic.List<object> CurrentList { get; } = new System.Collections.Generic.List<object>();

			public bool AtPropertyValue { get; set; } = false;

			public Group CurrentGroup { get; set; } = null;
			public System.Collections.Generic.Stack<Group> GroupStack { get; } = new System.Collections.Generic.Stack<Group>();

			public String CurrentString { get; set; } = String.Empty;
			public bool Escaped { get; set; } = false;
			public bool InsideString { get; set; } = false;

			public bool CheckWhiteSpace(char c)
			{
				if (Char.IsWhiteSpace(c))
				{
					if (InsideString)
						CurrentString += c;

					return true;
				}
				return false;
			}
		}

		private object ReadPropertyValue(_Context ctx, Reader r)
		{
			while (!r.EndOfStream)
			{
				char c = r.ReadChar();
				if (ctx.CheckWhiteSpace(c))
					continue;
				if (CheckString(c, r, ctx))
					continue;

				if (c == '[')
				{
					// we begin a new array
					return ReadArray(ctx, r);
				}
				else if (c == ']')
				{
					// we close current array
					ctx.InsideArray = false;
				}
				else if (c == '{')
				{
					// we begin a new object
					ctx.CurrentGroup = new Group();
					ctx.GroupStack.Push(ctx.CurrentGroup);
				}
				else if (c == '}')
				{
					// we close current object
					if (ctx.CurrentString != null)
					{
						/*
						if (ctx.CurrentProperty != null)
						{
							ctx.CurrentProperty.Value = ctx.CurrentString;
						}
						*/
					}
				}

			}
			return null;
		}

		private bool CheckString(char c, Reader r, _Context ctx)
		{
			if (c == '"')
			{
				ReadString(r, ctx);
				return true;
			}
			return false;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);

			_Context ctx = new _Context();
			Reader r = base.Accessor.Reader;
			while (!r.EndOfStream)
			{
				object obj = ReadNextObject(ctx, r);
				if (obj is Group)
				{
					plom.Items.Add(obj as Group);
				}
				else if (obj is Property)
				{
					plom.Items.Add(obj as Property);
				}
				else if (obj is Array)
				{
					object[] array = (object[])obj;
					foreach (object item in array)
					{
						Group g = (item as Group);
						plom.Items.Add(g);
					}
				}
			}
		}

		private object[] ReadArray(_Context ctx, Reader r)
		{
			System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
			object lastObject = null;
			while (!r.EndOfStream)
			{
				char c = r.ReadChar();
				if (ctx.CheckWhiteSpace(c))
					continue;
				if (CheckString(c, r, ctx))
					continue;

				if (c == '{')
				{
					lastObject = ReadGroup(ctx, r);
				}
				else if (c == ']')
				{
					break;
				}
				else if (c == ',')
				{
					if (lastObject == null && !String.IsNullOrEmpty(ctx.CurrentString))
					{
						list.Add(ctx.CurrentString);
						ctx.CurrentString = String.Empty;
					}
					else
					{
						list.Add(lastObject);
						lastObject = null;
					}
				}
			}

			if (lastObject != null)
			{
				list.Add(lastObject);
				lastObject = null;
			}
			else if (!String.IsNullOrEmpty(ctx.CurrentString))
			{
				list.Add(ctx.CurrentString);
				ctx.CurrentString = String.Empty;
			}
			// hack hack hack
			ctx.CurrentString = String.Empty;
			return list.ToArray();
		}

		private object ReadNextObject(_Context ctx, Reader r)
		{
			while (!r.EndOfStream)
			{
				char c = r.ReadChar();

				if (ctx.CheckWhiteSpace(c))
					continue;
				if (CheckString(c, r, ctx))
				{
					if (ctx.AtPropertyValue)
					{
						string retval = ctx.CurrentString;
						ctx.CurrentString = String.Empty;
						return retval;
					}
					continue;
				}

				if (c == '[')
				{
					// we are starting an array
					return ReadArray(ctx, r);
				}
				else if (c == '{')
				{
					return ReadGroup(ctx, r);
				}
				else if (c == '}' || c == ',')
				{
					// could be null?
					ctx.CloseGroup = (c == '}');
					if (ctx.CurrentStringRaw == "null")
					{
						r.Seek(-1, SeekOrigin.Current);
						ctx.CurrentStringRaw = String.Empty;
						return null;
					}
					else if (ctx.CurrentStringRaw == "true")
					{
						r.Seek(-1, SeekOrigin.Current);
						ctx.CurrentStringRaw = String.Empty;
						return true;
					}
					else if (ctx.CurrentStringRaw == "false")
					{
						r.Seek(-1, SeekOrigin.Current);
						ctx.CurrentStringRaw = String.Empty;
						return false;
					}
					else
					{
						object value = null;
						if (Byte.TryParse(ctx.CurrentStringRaw, out byte r_Byte))
						{
							value = r_Byte;
						}
						else if (Int16.TryParse(ctx.CurrentStringRaw, out short r_Int16))
						{
							value = r_Int16;
						}
						else if (Int32.TryParse(ctx.CurrentStringRaw, out int r_Int32))
						{
							value = r_Int32;
						}
						else if (Int64.TryParse(ctx.CurrentStringRaw, out long r_Int64))
						{
							value = r_Int64;
						}
						else if (Single.TryParse(ctx.CurrentStringRaw, out float r_Single))
						{
							value = r_Single;
						}
						else if (Double.TryParse(ctx.CurrentStringRaw, out double r_Double))
						{
							value = r_Double;
						}
						else if (Decimal.TryParse(ctx.CurrentStringRaw, out decimal r_Decimal))
						{
							value = r_Decimal;
						}
						ctx.CurrentStringRaw = String.Empty;
						return value;
					}
				}
				else
				{
					ctx.CurrentStringRaw += c;
				}
			}
			return null;
		}

		private Group ReadGroup(_Context ctx, Reader r)
		{
			Group g = new Group();
			while (!r.EndOfStream)
			{
				char c = r.ReadChar();
				if (ctx.CheckWhiteSpace(c))
					continue;

				if (CheckString(c, r, ctx))
					continue;

				if (c == '{')
				{
					// ok, we're in the group
				}
				else if (c == '}')
				{
					// close group
					break;
				}
				else if (c == ':')
				{
					// split property name and value
					ctx.AtPropertyValue = true;
					string propertyName = ctx.CurrentString;
					ctx.CurrentString = String.Empty;

					object obj = ReadNextObject(ctx, r);
					if (obj is Group)
					{
						Group g2 = (obj as Group);
						g2.Name = propertyName;
						g.Items.Add(g2);
					}
					else
					{
						Property p = new Property(propertyName, obj);
						g.Items.Add(p);
					}
				}

				if (ctx.CloseGroup)
				{
					ctx.CloseGroup = false;
					break;
				}
			}
			return g;
		}

		private void ReadString(Reader r, _Context ctx)
		{
			while (!r.EndOfStream)
			{
				char c = r.ReadChar();
				if (c == '\\' && !ctx.Escaped)
				{
					ctx.Escaped = true;
					continue;
				}
				else if (c == '\\' && ctx.Escaped)
				{
					ctx.Escaped = false;
					ctx.CurrentString += '\\';
					continue;
				}
				else if (c == '"' && !ctx.Escaped)
				{
					return;
				}

				if (ctx.Escaped)
				{
					switch (c)
					{
						case 'a':
						{
							ctx.CurrentString += '\a';
							continue;
						}
						case '0':
						{
							ctx.CurrentString += '\0';
							continue;
						}
						case 'b':
						{
							ctx.CurrentString += '\b';
							continue;
						}
						case 'f':
						{
							ctx.CurrentString += '\f';
							continue;
						}
						case 'n':
						{
							ctx.CurrentString += '\r';
							continue;
						}
						case 'r':
						{
							ctx.CurrentString += '\r';
							continue;
						}
						case 't':
						{
							ctx.CurrentString += '\t';
							continue;
						}
						case 'v':
						{
							ctx.CurrentString += '\v';
							continue;
						}
						case 'x':
						{
							Console.WriteLine("TODO: implement \\x");
							continue;
						}
					}
				}

				ctx.CurrentString += c;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);

			Writer w = base.Accessor.Writer;
			w.Write("{");

			foreach (PropertyListItem g in plom.Items)
			{
				if (g is Group)
				{
					w.Write(GroupToString(g as Group, plom.Items.IndexOf(g) < plom.Items.Count - 1));
				}
				else if (g is Property)
				{
					w.Write(PropertyToString(g as Property, plom.Items.IndexOf(g) < plom.Items.Count - 1));
				}
			}

			w.Write("}");
		}

		private string GroupToString(Group group, bool more)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			foreach (PropertyListItem g in group.Items)
			{
				if (g is Group)
				{
					sb.Append(GroupToString(g as Group, group.Items.IndexOf(g) < group.Items.Count - 1));
				}
				else if (g is Property)
				{
					sb.Append(PropertyToString(g as Property, group.Items.IndexOf(g) < group.Items.Count - 1));
				}
			}
			sb.Append("}");
			if (more) sb.Append(',');
			return sb.ToString();
		}
		private string PropertyToString(Property p, bool more)
		{
			return "\"" + p.Name + "\": " + ValueToString(p.Value) + (more ? "," : "");
		}
		private string ValueToString(object value)
		{
			if (value == null) return "null";
			if ((value is Int16) || (value is Int32) || (value is Int64) ||
				(value is UInt16) || (value is UInt32) || (value is UInt64) ||
				(value is Single) || (value is Double) ||
				(value is SByte) || (value is Byte))
			{
				return value.ToString();
			}
			else if (value is Group)
			{
				return GroupToString(value as Group, false);
			}
			return "\"" + value + "\"";
		}
	}
}
