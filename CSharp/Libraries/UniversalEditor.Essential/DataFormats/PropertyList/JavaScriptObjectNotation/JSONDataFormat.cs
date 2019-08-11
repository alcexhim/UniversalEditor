using System;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.JavaScriptObjectNotation
{
	public class JSONDataFormat : DataFormat
	{
		private class _Context
		{
			public string CurrentStringRaw { get; set; } = String.Empty;

			public Property CurrentProperty { get; set; } = null;

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
						if (ctx.CurrentProperty != null)
						{
							ctx.CurrentProperty.Value = ctx.CurrentString;
						}
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
					plom.Groups.Add(obj as Group);
				}
				else if (obj is Property)
				{
					plom.Properties.Add(obj as Property);
				}
				else if (obj is Array)
				{
					object[] array = (object[])obj;
					foreach (object item in array)
					{
						Group g = (item as Group);
						plom.Groups.Add(g);
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
					list.Add(lastObject);
					lastObject = null;
				}
			}

			if (lastObject != null)
			{
				list.Add(lastObject);
				lastObject = null;
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
					if (ctx.CurrentStringRaw == "null")
					{
						r.Seek(-1, SeekOrigin.Current);
						ctx.CurrentStringRaw = String.Empty;
						return null;
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

					Property p = new Property(propertyName, obj);
					g.Properties.Add(p);
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

			foreach (Group g in plom.Groups)
			{
				w.Write(GroupToString(g, plom.Groups.IndexOf(g) < plom.Groups.Count - 1));
			}
			foreach (Property p in plom.Properties)
			{
				w.Write(PropertyToString(p, plom.Properties.IndexOf(p) < plom.Properties.Count - 1));
			}

			w.Write("}");
		}

		private string GroupToString(Group group, bool more)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			foreach (Group g in group.Groups)
			{
				sb.Append(GroupToString(g, group.Groups.IndexOf(g) < group.Groups.Count - 1));
			}
			foreach (Property p in group.Properties)
			{
				sb.Append(PropertyToString(p, group.Properties.IndexOf(p) < group.Properties.Count - 1));
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
