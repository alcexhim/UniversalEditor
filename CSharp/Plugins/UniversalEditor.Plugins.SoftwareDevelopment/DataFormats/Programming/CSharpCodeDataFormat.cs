using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElementReferences;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.Programming
{
	public class CSharpCodeDataFormat : CodeDataFormat
	{
		private string mvarNamespaceSeparator = ".";
		public string NamespaceSeparator { get { return mvarNamespaceSeparator; } set { mvarNamespaceSeparator = value; } }

		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("C# code file", new string[] { "*.cs" });
			return dfr;
		}

		protected override CodeElementReference StringToExpression(string value, out CodeDataType dataType)
		{
			if (value == "null")
			{
				dataType = CodeDataType.Empty;
				return new CodeElementReference(new CodeLiteralElement(null));
			}
			else if (value.StartsWith("\"") && value.EndsWith("\""))
			{
				value = value.Substring(1, value.Length - 2);
				dataType = CodeDataType.String;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (value.StartsWith("'") && value.EndsWith("'"))
			{
				value = value.Substring(1, value.Length - 2);
				char actual = Char.Parse(value);
				dataType = CodeDataType.Char;
				return new CodeElementReference(new CodeLiteralElement(actual));
			}
			else
			{
				if (value.EndsWith("f") || value.EndsWith("F"))
				{
					value = value.Substring(0, value.Length - 1);
					float actual = Single.Parse(value);
					dataType = CodeDataType.Single;
					return new CodeElementReference(new CodeLiteralElement(actual));
				}
				else if (value.Contains("."))
				{
					double actual = 0;
					if (Double.TryParse(value, out actual))
					{
						dataType = CodeDataType.Double;
						return new CodeElementReference(new CodeLiteralElement(actual));
					}
					else
					{
						// this is an Enumeration
						string objectName = value.Substring(0, value.LastIndexOf('.'));
						string[] objectNameParts = objectName.Split(mvarNamespaceSeparator);
						string valueName = value.Substring(value.LastIndexOf('.') + 1);

						dataType = CodeDataType.Empty;

						return new CodeElementEnumerationValueReference(objectNameParts, valueName);
					}
				}
				else if (value.EndsWith("l") || value.EndsWith("L"))
				{
					value = value.Substring(0, value.Length - 1);
					long actual = Int64.Parse(value);
					dataType = CodeDataType.Int64;
					return new CodeElementReference(new CodeLiteralElement(actual));
				}
				else if (value.EndsWith("u") || value.EndsWith("U"))
				{
					value = value.Substring(0, value.Length - 1);
					uint actual = UInt32.Parse(value);
					dataType = CodeDataType.UInt32;
					return new CodeElementReference(new CodeLiteralElement(actual));
				}
				else if (value.EndsWith("m") || value.EndsWith("M"))
				{
					value = value.Substring(0, value.Length - 1);
					decimal actual = Decimal.Parse(value);
					dataType = CodeDataType.Decimal;
					return new CodeElementReference(new CodeLiteralElement(actual));
				}
				else
				{
					int actual = Int32.Parse(value);
					dataType = CodeDataType.Int32;
					return new CodeElementReference(new CodeLiteralElement(actual));
				}
			}
		}

		protected override void ProcessToken(string token, Reader tr)
		{
			if (String.IsNullOrEmpty(token.Trim()))
			{
				base.InhibitTokenProcessing = false;
				return;
			}
			else if (token.StartsWith("return "))
			{
				string value = tr.ReadUntil(new string[] { ";" });
			}
			else if (token.StartsWith("using") && token.EndsWith(";"))
			{
				string namespaceName = token.Substring(6, token.Length - 7);
				string[] namespaceNames = namespaceName.Split(mvarNamespaceSeparator);
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(new CodeNamespaceImportElement(namespaceNames));
				}
				base.InhibitTokenProcessing = false;
				return;
			}
			else if (token.EndsWith(")") || token.EndsWith(";"))
			{
				// figure out whether there is a parameter list
				bool isMethodCall = false;
				int indexof = token.IndexOf(" ");
				string datatype = String.Empty;
				if (indexof > -1)
				{
					datatype = token.Substring(0, indexof);
					if (datatype.Contains("("))
					{
						// if there is a parenthesis, this is a method call, not a declaration
						// declarations don't have types (which don't contain parenthesis)
						isMethodCall = true;
					}
				}
				else if (token.Contains("("))
				{
					// method call because token contains a parenthesis but no datatype (no space)
					isMethodCall = true;
				}

				if (isMethodCall)
				{
					if (!token.EndsWith(";"))
					{
						base.InhibitTokenProcessing = true;
						return;
					}

					CodeMethodElement parentMethod = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeMethodElement);
					if (parentMethod == null)
					{
						base.InhibitTokenProcessing = false;
						return;
					}

					string methodName = token.Substring(0, token.IndexOf('('));
					string[] methodNameParts = methodName.Split(mvarNamespaceSeparator);

					CodeMethodCallElement methodCall = new CodeMethodCallElement();
					string[] objNameParts = new string[methodNameParts.Length - 1];
					Array.Copy(methodNameParts, 0, objNameParts, 0, objNameParts.Length);
					methodCall.ObjectName = objNameParts;
					methodCall.MethodName = methodNameParts[methodNameParts.Length - 1];

					string parameterList = token.Substring(token.IndexOf('(') + 1, token.IndexOf(')') - token.IndexOf('(') - 1);
					string[] paramz = parameterList.Split(new string[] { "," }, "\"", "\"", StringSplitOptions.None, -1, false);

					foreach (string param in paramz)
					{
						CodeVariableElement vari = new CodeVariableElement();
						vari.Name = "Parameter" + Array.IndexOf(paramz, param).ToString();

						string value = param.Trim();
						CodeDataType dataType = CodeDataType.Empty;
						vari.Value = StringToExpression(value, out dataType);
						vari.DataType = dataType;

						methodCall.Parameters.Add(vari);
					}

					parentMethod.Elements.Add(methodCall);
					base.InhibitTokenProcessing = false;
				}
				else
				{
					if (token.Contains("("))
					{
						string methodName = token.Substring(datatype.Length + 1, token.IndexOf('(') - datatype.Length - 1);

						CodeMethodElement method = new CodeMethodElement();
						method.Name = methodName;
						if (datatype != "void" && datatype != "System.Void")
						{
							method.DataType = MakeKnownDataType(datatype);
						}
						else
						{
							method.DataType = null;
						}

						method.AccessModifiers = (CodeAccessModifiers)base.TemporaryVariables["NextElementAccessModifier", CodeAccessModifiers.None];

						base.TemporaryVariables["CurrentElement"] = method;
						base.InhibitTokenProcessing = false;
					}
					else
					{
						string fieldName = token.Substring(datatype.Length + 1, token.Length - datatype.Length - 2);

						CodeVariableElement field = new CodeVariableElement();
						field.Name = fieldName;
						field.DataType = MakeKnownDataType(datatype);

						CodeElementContainerElement container = (base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
						container.Elements.Add(field);

						base.InhibitTokenProcessing = false;
					}
				}
			}
			else if (token == "public")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["NextElementAccessModifier"] = CodeAccessModifiers.Public;
			}
			else if (token == "private")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["NextElementAccessModifier"] = CodeAccessModifiers.Private;
			}
			else if (token == "protected")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["LastTokenWasProtected"] = true;
				if ((bool)base.TemporaryVariables["LastTokenWasProtected", false] == true)
				{
					base.TemporaryVariables["NextElementAccessModifier"] = CodeAccessModifiers.FamilyANDAssembly;
				}
				else
				{
					base.TemporaryVariables["NextElementAccessModifier"] = CodeAccessModifiers.Family;
				}
			}
			else if (token == "internal")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["LastTokenWasInternal"] = true;
				if ((bool)base.TemporaryVariables["LastTokenWasProtected", false] == true)
				{
					base.TemporaryVariables["NextElementAccessModifier"] = CodeAccessModifiers.FamilyANDAssembly;
				}
				else
				{
					base.TemporaryVariables["NextElementAccessModifier"] = CodeAccessModifiers.Assembly;
				}
			}
			else if (token == "static")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["NextElementIsStatic"] = true;
			}
			else if (token == "sealed")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["NextElementIsSealed"] = true;
			}
			else if (token == "abstract")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["NextElementIsAbstract"] = true;
			}
			else if (token == "partial")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["NextElementIsPartial"] = true;
			}
			else if (token == "class")
			{
				base.InhibitTokenProcessing = false;

				CodeClassElement clss = new CodeClassElement();
				clss.AccessModifiers = (CodeAccessModifiers)base.TemporaryVariables["NextElementAccessModifier", CodeAccessModifiers.None];
				clss.IsAbstract = (bool)base.TemporaryVariables["NextElementIsAbstract", false];
				clss.IsPartial = (bool)base.TemporaryVariables["NextElementIsPartial", false];
				clss.IsSealed = (bool)base.TemporaryVariables["NextElementIsSealed", false];
				clss.IsStatic = (bool)base.TemporaryVariables["NextElementIsStatic", false];

				base.TemporaryVariables.Remove("NextElementAccessModifier");
				base.TemporaryVariables.Remove("NextElementIsAbstract");
				base.TemporaryVariables.Remove("NextElementIsPartial");
				base.TemporaryVariables.Remove("NextElementIsSealed");
				base.TemporaryVariables.Remove("NextElementIsStatic");

				base.TemporaryVariables["CurrentElement"] = clss;
			}
			else if (token == "enum")
			{
				base.InhibitTokenProcessing = false;

				CodeEnumerationElement enumm = new CodeEnumerationElement();
				enumm.AccessModifiers = (CodeAccessModifiers)base.TemporaryVariables["NextElementAccessModifier", CodeAccessModifiers.None];
				base.TemporaryVariables.Remove("NextElementAccessModifier");

				base.TemporaryVariables["CurrentElement"] = enumm;
			}
			else if (token == "namespace")
			{
				base.InhibitTokenProcessing = false;
				base.TemporaryVariables["CurrentElement"] = new CodeNamespaceElement();
			}
			else if (token == " ")
			{
			}
			else if (token == "{")
			{
				Stack<CodeElement> stack = (Stack<CodeElement>)base.TemporaryVariables["CurrentParent", new Stack<CodeElement>()];

				CodeElement current = (CodeElement)base.TemporaryVariables["CurrentElement", null];
				if (stack.Count > 0)
				{
					CodeElement prev = stack.Pop();
					if (prev is CodeElementContainerElement)
					{
						(prev as CodeElementContainerElement).Elements.Add(current);
					}
					stack.Push(prev);
				}
				stack.Push(current);
			}
			else if (token == "}")
			{
				Stack<CodeElement> stack = (Stack<CodeElement>)base.TemporaryVariables["CurrentParent", new Stack<CodeElement>()];
				CodeElement ele = stack.Pop();

				base.TemporaryVariables["CurrentElement"] = ele;

				if (stack.Count == 0)
				{
					if (ele is CodeEnumerationElement)
					{
						base.TemporaryVariables["CurrentElement"] = null;
					}
					else
					{
						((List<CodeElement>)base.TemporaryVariables["CurrentElements", new List<CodeElement>()]).Add((CodeElement)base.TemporaryVariables["CurrentElement"]);
					}
				}
			}
			else if (token.EndsWith(","))
			{
				string lastToken = token.Substring(0, token.Length - 1);

				CodeEnumerationElement CurrentElement = (base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement);
				if (CurrentElement != null)
				{
					string name = lastToken;
					int value = 0;
					if (CurrentElement.Values.Count > 0)
					{
						value = CurrentElement.Values[CurrentElement.Values.Count - 1].Value + 1;
					}

					CodeEnumerationValue cev = new CodeEnumerationValue();
					if (lastToken.Contains("="))
					{
						string[] lastTokenSplit = lastToken.Split(new char[] { '=' });
						if (lastTokenSplit.Length == 2)
						{
							name = lastTokenSplit[0].Trim();
							value = Int32.Parse(lastTokenSplit[1].Trim());
						}
						cev.IsValueDefined = true;
					}
					cev.Name = name;
					cev.Value = value;
					CurrentElement.Values.Add(cev);

					base.InhibitTokenProcessing = false;
				}
			}
			else if (token.EndsWith("\r") || token.EndsWith("\n"))
			{
				string lastToken = token.Trim();

				CodeEnumerationElement CurrentEnumElement = (base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement);
				if (CurrentEnumElement != null)
				{
					string name = lastToken;
					int value = 0;
					if (CurrentEnumElement.Values.Count > 0)
					{
						value = CurrentEnumElement.Values.Last().Value + 1;
						
						CodeEnumerationValue cev = new CodeEnumerationValue();
						if (lastToken.Contains("="))
						{
							string[] lastTokenSplit = lastToken.Split(new char[] { '=' });
							if (lastTokenSplit.Length == 2)
							{
								name = lastTokenSplit[0].Trim();
								value = Int32.Parse(lastTokenSplit[1].Trim());
							}
							cev.IsValueDefined = true;
						}
						cev.Name = name;
						cev.Value = value;
						CurrentEnumElement.Values.Add(cev);
					}
					else
					{
						CurrentEnumElement.Name = lastToken;
					}
				}
				else
				{
					INamedCodeElement CurrentElement = ((CodeElement)base.TemporaryVariables["CurrentElement", null] as INamedCodeElement);
					if (CurrentElement != null) CurrentElement.Name = lastToken;
				}
				base.InhibitTokenProcessing = false;
			}
			else if (token.StartsWith("//"))
			{
				string commentText = tr.ReadLine().Trim();
				List<CodeElement> CurrentElements = ((List<CodeElement>)base.TemporaryVariables["CurrentElements", new List<CodeElement>()]);
				CodeElementContainerElement CurrentElement = (base.TemporaryVariables["CurrentElement", null] as CodeElementContainerElement);

				CodeCommentElement comment = new CodeCommentElement();
				if (commentText.StartsWith("/"))
				{
					// this is a documentation comment
					commentText = commentText.Substring(1);
					comment.IsDocumentationComment = true;
				}

				comment.Content = commentText;
				comment.Content = comment.Content.Trim();

				if (CurrentElement != null)
				{
					(CurrentElement as CodeElementContainerElement).Elements.Add(comment);
				}
				else
				{
					CurrentElements.Add(comment);
				}
				base.InhibitTokenProcessing = false;
			}
			else
			{
				base.InhibitTokenProcessing = true;
			}
		}

		protected override string MakeKnownDataType(string DataType)
		{
			switch (DataType)
			{
				case "bool": return "System.Boolean";
				case "byte": return "System.Byte";
				case "char": return "System.Char";
				case "decimal": return "System.Decimal";
				case "double": return "System.Double";
				case "short": return "System.Int16";
				case "int": return "System.Int32";
				case "long": return "System.Int64";
				case "sbyte": return "System.SByte";
				case "float": return "System.Single";
				case "string": return "System.String";
				case "ushort": return "System.UInt16";
				case "uint": return "System.UInt32";
				case "ulong": return "System.UInt64";
			}
			return base.MakeKnownDataType(DataType);
		}
		protected override string MakeFriendlyDataType(string DataType)
		{
			switch (DataType)
			{
				case "System.Boolean": return "bool";
				case "System.Byte": return "byte";
				case "System.Char": return "char";
				case "System.Decimal": return "decimal";
				case "System.Double": return "double";
				case "System.Int16": return "short";
				case "System.Int32": return "int";
				case "System.Int64": return "long";
				case "System.SByte": return "sbyte";
				case "System.Single": return "float";
				case "System.String": return "string";
				case "System.UInt16": return "ushort";
				case "System.UInt32": return "uint";
				case "System.UInt64": return "ulong";
			}
			return base.MakeFriendlyDataType(DataType);
		}

		protected internal override string GenerateCode(object obj, int indentCount)
		{
			StringBuilder sb = new StringBuilder();
			string indent = GetIndentString(indentCount);
			if (obj is CodeAccessModifiers)
			{
				switch ((CodeAccessModifiers)obj)
				{
					case CodeAccessModifiers.Assembly:
					{
						sb.Append("internal");
						break;
					}
					case CodeAccessModifiers.Family:
					{
						sb.Append("protected");
						break;
					}
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						sb.Append("protected internal");
						break;
					}
					case CodeAccessModifiers.Private:
					{
						sb.Append("private");
						break;
					}
					case CodeAccessModifiers.Public:
					{
						sb.Append("public");
						break;
					}
				}
			}
			else if (obj is CodeNamespaceElement)
			{
				CodeNamespaceElement namespaceEl = (obj as CodeNamespaceElement);
				sb.Append(indent);
				sb.Append("namespace ");
				// sb.AppendLine(namespaceEl.GetFullName(mvarNamespaceSeparator));
				sb.AppendLine(String.Join(mvarNamespaceSeparator, namespaceEl.Name));
				sb.AppendLine(indent + "{");
				foreach (CodeElement el in namespaceEl.Elements)
				{
					sb.Append(GenerateCode(el, indentCount + 1));
					sb.AppendLine();
				}
				sb.Append(indent + "}");
			}
			else if (obj is CodeClassElement)
			{
				CodeClassElement classEl = (obj as CodeClassElement);
				sb.Append(indent);
				sb.Append(GenerateCode(classEl.AccessModifiers));
				if (classEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				if (classEl.IsAbstract)
				{
					sb.Append("abstract ");
				}
				else if (classEl.IsSealed)
				{
					sb.Append("sealed ");
				}
				if (classEl.IsPartial)
				{
					sb.Append("partial ");
				}
				sb.Append("class " + classEl.Name);
				if (classEl.GenericParameters.Count > 0)
				{
					sb.Append("<");
					foreach (CodeVariableElement param in classEl.GenericParameters)
					{
						sb.Append(param.Name);
						if (classEl.GenericParameters.IndexOf(param) < classEl.GenericParameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append(">");
				}
				sb.AppendLine();
				sb.AppendLine(indent + "{");
				foreach (CodeElement el in classEl.Elements)
				{
					sb.Append(GenerateCode(el, indentCount + 1));
					sb.AppendLine();
				}
				sb.Append(indent + "}");
			}
			else if (obj is CodeMethodElement)
			{
				CodeMethodElement methodEl = (obj as CodeMethodElement);
				sb.Append(indent);
				sb.Append(GenerateCode(methodEl.AccessModifiers));
				if (methodEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				if (String.IsNullOrEmpty(methodEl.DataType))
				{
					sb.Append("void ");
				}
				else
				{
					sb.Append(MakeFriendlyDataType(methodEl.DataType) + " ");
				}
				sb.Append(methodEl.Name);
				if (methodEl.GenericParameters.Count > 0)
				{
					sb.Append("<");
					foreach (CodeVariableElement param in methodEl.GenericParameters)
					{
						sb.Append(param.Name);
						if (methodEl.GenericParameters.IndexOf(param) < methodEl.GenericParameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append(">");
				}
				sb.Append("(");
				foreach (CodeVariableElement param in methodEl.Parameters)
				{
					if (param.PassByReference)
					{
						sb.Append("ref ");
					}
					sb.Append(MakeFriendlyDataType(param.DataType) + " " + param.Name);
					if (methodEl.Parameters.IndexOf(param) < methodEl.Parameters.Count - 1)
					{
						sb.Append(", ");
					}
				}
				sb.AppendLine(")");
				sb.AppendLine(indent + "{");
				foreach (CodeElement el in methodEl.Elements)
				{
					sb.Append(GenerateCode(el, indentCount + 1));
				}
				sb.Append(indent + "}");
			}
			else if (obj is CodePropertyElement)
			{
				sb.Append(indent);
				CodePropertyElement propertyEl = (obj as CodePropertyElement);
				sb.Append(GenerateCode(propertyEl.AccessModifiers));
				if (propertyEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				if (propertyEl.IsAbstract)
				{
					sb.Append("abstract ");
				}
				else if (propertyEl.IsVirtual)
				{
					sb.Append("virtual ");
				}
				else if (propertyEl.IsOverriding)
				{
					sb.Append("override ");
				}
				sb.Append(MakeFriendlyDataType(propertyEl.DataType) + " " + propertyEl.Name);
				if (propertyEl.Parameters.Count > 0)
				{
					sb.Append("[");
					foreach (CodeVariableElement varEl in propertyEl.Parameters)
					{
						sb.Append(MakeFriendlyDataType(varEl.DataType) + " " + varEl.Name);
						if (propertyEl.Parameters.IndexOf(varEl) < propertyEl.Parameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append("]");
				}
			}
			else if (obj is CodeVariableElement)
			{
				sb.Append(indent);
				CodeVariableElement varEl = (obj as CodeVariableElement);
				sb.Append(GenerateCode(varEl.AccessModifiers));
				if (varEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				sb.Append(MakeFriendlyDataType(varEl.DataType) + " " + varEl.Name);
				if (varEl.Value != null)
				{
					sb.Append(" = ");
					sb.Append(GenerateCode(varEl.Value.Value, 0));
				}

				if (!sb.ToString().EndsWith(";"))
				{
					sb.Append(";");
				}
			}
			return sb.ToString();
		}
	}
}
