using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable.CodeProviders
{
	public class CSharpCodeProvider : CodeProvider
	{
		public override string Title => "C#";
		public override string CodeFileExtension => ".cs";

		protected override string GetAccessModifiersInternal(bool isPublic, bool isFamily, bool isAssembly, bool isPrivate, bool isAbstract, bool isSealed)
		{
			StringBuilder sb = new StringBuilder();
			if (isPublic) sb.Append("public ");
			if (isFamily) sb.Append("protected ");
			if (isAssembly) sb.Append("internal ");
			if (isPrivate) sb.Append("private ");
			if (isAbstract) sb.Append("abstract ");
			if (isSealed) sb.Append("sealed ");
			return sb.ToString().Trim();
		}
		protected override string GetBeginBlockInternal(Type type, int indentLevel)
		{
			return GetBeginBlockInternal(indentLevel);
		}
		protected override string GetBeginBlockInternal(int indentLevel)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(new string('\t', indentLevel));
			sb.Append('{');
			return sb.ToString();
		}
		protected override string GetEndBlockInternal(string elementName, int indentLevel)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(new string('\t', indentLevel));
			sb.Append('}');
			return sb.ToString();
		}

		protected override string GetBeginBlockInternal(MethodInfo mi, int indentLevel)
		{
			return GetBeginBlockInternal(indentLevel);
		}
		protected override string GetEndBlockInternal(MethodInfo mi, int indentLevel)
		{
			return GetEndBlockInternal(GetElementName(mi), indentLevel);
		}

		protected override string GetElementNameInternal(Type type)
		{
			if (type.IsClass)
			{
				return "class";
			}
			else if (type.IsInterface)
			{
				return "interface";
			}
			else if (type.IsEnum)
			{
				return "enum";
			}
			else if (type.IsValueType)
			{
				return "struct";
			}
			throw new NotSupportedException();
		}
		protected override string GetElementNameInternal(MethodInfo mi)
		{
			return String.Empty;
		}

		protected override string GetMethodSignatureInternal(MethodInfo mi)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(GetAccessModifiers(mi));
			sb.Append(' ');
			sb.Append(GetTypeName(mi.ReturnType));
			sb.Append(' ');
			sb.Append(mi.Name);
			sb.Append('(');
			ParameterInfo[] pis = mi.GetParameters();
			for (int i = 0; i < pis.Length; i++)
			{
				if (pis[i].ParameterType.IsByRef && pis[i].IsOut)
				{
					sb.Append("out ");
				}
				else if (pis[i].ParameterType.IsByRef)
				{
					sb.Append("ref ");
				}

				sb.Append(GetTypeName(pis[i].ParameterType));
				sb.Append(' ');
				sb.Append(pis[i].Name);

				if (i < pis.Length - 1)
					sb.Append(", ");
			}
			sb.Append(')');
			return sb.ToString();
		}

		protected override string GetSourceCodeInternal(MethodInfo mi, int indentLevel)
		{
			StringBuilder sb = new StringBuilder();
			string indentStr = new string('\t', indentLevel);
			sb.Append(indentStr);
			sb.Append(GetMethodSignature(mi));

			MethodBody mb = mi.GetMethodBody();
			if (mb != null)
			{
				sb.AppendLine();
				sb.Append(GetBeginBlock(mi, indentLevel));
				sb.AppendLine();

				for (int i = 0; i < mb.LocalVariables.Count; i++)
				{
					sb.Append(new string('\t', indentLevel + 1));
					sb.Append(GetTypeName(mb.LocalVariables[i].LocalType));
					sb.Append(' ');
					sb.Append(GetVariableName(mi, mb.LocalVariables[i]));
					sb.AppendLine(";");
				}

				if (mb.LocalVariables.Count > 0)
					sb.AppendLine();

				byte[] bytecode = mb.GetILAsByteArray();
				MemoryAccessor ma = new MemoryAccessor(bytecode);
				Reader r = new Reader(ma);
				while (!r.EndOfStream)
				{
					int sz = 0;
					ILOpcode opcode = ReadOpcode(r, out sz);
					ILOpcode opcode2 = PeekOpcode(r);

					sb.Append(new string('\t', indentLevel + 1));


					if (opcode2 == ILOpcode.NewArr)
					{
						int? lit = OpcodeToLiteralInt(opcode);
						sb.Append(String.Format("X[] = new X[{0}]", lit.GetValueOrDefault(0)));
					}
					else
					{
						sb.AppendLine(GetILOpcodeStr(opcode));
						continue;
					}
				}

				sb.Append(GetEndBlock(mi, indentLevel));
			}
			else
			{
				sb.Append(';');
			}
			return sb.ToString();
		}

		private int? OpcodeToLiteralInt(ILOpcode opcode)
		{
			switch (opcode)
			{
				case ILOpcode.LdCI4_0:
				{
					return 0;
				}
				case ILOpcode.LdCI4_1:
				{
					return 1;
				}
				case ILOpcode.LdCI4_2:
				{
					return 2;
				}
				case ILOpcode.LdCI4_3:
				{
					return 3;
				}
				case ILOpcode.LdCI4_4:
				{
					return 4;
				}
				case ILOpcode.LdCI4_5:
				{
					return 5;
				}
				case ILOpcode.LdCI4_6:
				{
					return 6;
				}
				case ILOpcode.LdCI4_7:
				{
					return 7;
				}
				case ILOpcode.LdCI4_8:
				{
					return 8;
				}
				case ILOpcode.LdCI4_M1:
				{
					return -1;
				}
			}
			return null;
		}

		private ILOpcode PeekOpcode(Reader r)
		{
			int sz = 0;
			ILOpcode opcode = ReadOpcode(r, out sz);
			if (sz == 0)
				return opcode;

			r.Seek(-sz, SeekOrigin.Current);
			return opcode;
		}
		private ILOpcode ReadOpcode(Reader r, out int size)
		{
			if (r.EndOfStream)
			{
				size = 0;
				return ILOpcode.Nop;
			}

			byte bytecode_b = r.ReadByte();
			short bytecode_s = bytecode_b;
			size = 1;

			if (bytecode_s == 0xFE)
			{
				bytecode_s = BitConverter.ToInt16(new byte[] { bytecode_b, r.ReadByte() }, 0);
				size = 2;
			}
			return (ILOpcode)bytecode_s;
		}

		protected override string GetSourceCodeInternal(FieldInfo fi, int indentLevel)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(new string('\t', indentLevel));
			sb.Append(GetAccessModifiers(fi));
			sb.Append(' ');
			sb.Append(fi.FieldType.FullName);
			sb.Append(' ');
			sb.Append(fi.Name);
			sb.Append(';');
			return sb.ToString();
		}

		protected override string GetTypeNameInternal(Type type)
		{
			string fullyQualifiedTypeName = type.FullName ?? type.Name;
			if (fullyQualifiedTypeName.Equals("System.Void")) return "void";
			if (fullyQualifiedTypeName.Equals("System.Byte")) return "byte";
			if (fullyQualifiedTypeName.Equals("System.SByte")) return "sbyte";
			if (fullyQualifiedTypeName.Equals("System.Char")) return "char";
			if (fullyQualifiedTypeName.Equals("System.Int16")) return "short";
			if (fullyQualifiedTypeName.Equals("System.UInt16")) return "ushort";
			if (fullyQualifiedTypeName.Equals("System.Int32")) return "int";
			if (fullyQualifiedTypeName.Equals("System.UInt32")) return "uint";
			if (fullyQualifiedTypeName.Equals("System.Int64")) return "long";
			if (fullyQualifiedTypeName.Equals("System.UInt64")) return "ulong";
			if (fullyQualifiedTypeName.Equals("System.String")) return "string";
			if (fullyQualifiedTypeName.Equals("System.Single")) return "float";
			if (fullyQualifiedTypeName.Equals("System.Double")) return "double";
			if (fullyQualifiedTypeName.Equals("System.Decimal")) return "decimal";
			if (fullyQualifiedTypeName.Equals("System.Boolean")) return "bool";
			if (fullyQualifiedTypeName.Equals("System.Object")) return "object";
			return fullyQualifiedTypeName;
		}

		protected override string GetSourceCodeInternal(Type item, int indentLevel)
		{
			string indentStr = new string('\t', indentLevel);
			StringBuilder sb = new StringBuilder();

			sb.Append(indentStr);
			sb.Append(GetAccessModifiers(item));
			sb.Append(' ');
			sb.Append(GetElementName(item));
			sb.Append(' ');
			sb.Append(item.Name);

			if (!item.IsValueType && (item.BaseType != null && item.BaseType != typeof(object) && item.BaseType != typeof(Enum)))
			{
				sb.Append(" : ");
				sb.Append(GetTypeName(item.BaseType));
			}
			sb.AppendLine();

			sb.Append(GetBeginBlock(indentLevel));
			sb.AppendLine();

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
			if (item.IsEnum)
			{
				FieldInfo[] fields = item.GetFields(bindingFlags);
				for (int j = 0; j < fields.Length; j++)
				{
					if (fields[j].FieldType == item)
					{
						sb.Append(new string('\t', indentLevel + 1));
						sb.Append(fields[j].Name);
						if (j < fields.Length - 1)
							sb.Append(',');

						sb.AppendLine();
					}
				}
			}
			else
			{
				List<string> AutoPropertyNames;
				FieldInfo[] fields = GetFields(item, out AutoPropertyNames);
				for (int j = 0; j < fields.Length; j++)
				{
					sb.AppendLine(GetSourceCode(fields[j], indentLevel + 1));
					sb.AppendLine();
				}

				EventInfo[] events = item.GetEvents(bindingFlags);
				for (int j = 0; j < events.Length; j++)
				{
					sb.AppendLine(GetSourceCode(events[j], indentLevel + 1));
					sb.AppendLine();
				}

				PropertyInfo[] props = item.GetProperties(bindingFlags);
				for (int j = 0; j < props.Length; j++)
				{
					sb.AppendLine(GetSourceCode(props[j], AutoPropertyNames.Contains(props[j].Name), indentLevel + 1));
					sb.AppendLine();
				}

				MethodInfo[] meths = GetMethods(item);
				for (int j = 0; j < meths.Length; j++)
				{
					sb.AppendLine(GetSourceCode(meths[j], indentLevel + 1));
					sb.AppendLine();
				}
			}
			sb.Append(GetEndBlock(item, indentLevel));
			return sb.ToString();
		}

		protected override string GetSourceCodeInternal(EventInfo item, int indentLevel)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(new string('\t', indentLevel));
			sb.Append(GetAccessModifiers(item));
			sb.Append(" event ");
			sb.Append(GetTypeName(item.EventHandlerType));
			sb.Append(' ');
			sb.Append(item.Name);
			return sb.ToString();
		}

		protected override string GetSourceCodeInternal(PropertyInfo item, bool autoProperty, int indentLevel)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(new string('\t', indentLevel));
			string amProp = GetAccessModifiers(item);
			sb.Append(amProp);
			sb.Append(' ');
			sb.Append(GetTypeName(item.PropertyType));
			sb.Append(' ');
			sb.Append(item.Name);
			sb.Append(' ');
			sb.Append("{ ");
			MethodInfo miGet = item.GetGetMethod();
			if (miGet != null)
			{
				string am = GetAccessModifiers(miGet);
				if (!String.IsNullOrEmpty(am) && amProp != am)
				{
					sb.Append(am);
					sb.Append(' ');
				}

				sb.Append("get");

				if (autoProperty)
				{
					sb.Append("; ");
				}
				else
				{
					sb.Append(" { ");
					sb.Append(" } ");
				}
			}
			MethodInfo miSet = item.GetSetMethod();
			if (miSet != null)
			{
				string am = GetAccessModifiers(miSet);
				if (!String.IsNullOrEmpty(am) && amProp != am)
					sb.Append(am);

				sb.Append(" set");

				if (autoProperty)
				{
					sb.Append("; ");
				}
				else
				{
					sb.Append(" { ");
					sb.Append(" }");
				}
			}

			sb.Append('}');
			return sb.ToString();
		}
	}
}
