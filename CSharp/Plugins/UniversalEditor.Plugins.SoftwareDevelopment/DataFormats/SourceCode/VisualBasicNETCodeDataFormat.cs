using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElementReferences;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;
using UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements;

namespace UniversalEditor.DataFormats.SourceCode
{
	public class VisualBasicNETCodeDataFormat : CodeDataFormat
	{
		private string mvarNamespaceSeparator = ".";
		public string NamespaceSeparator { get { return mvarNamespaceSeparator; } set { mvarNamespaceSeparator = value; } }

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
			}
			return _dfr;
		}

		private List<CodeElement> emptyStack = new List<CodeElement>();
		private string[] lineEndings = new string[] { "\r\n", ":", "\r", "\n" };

		protected override void ProcessToken(string token, Reader tr)
		{
			base.ProcessToken(token, tr);

			if (String.IsNullOrEmpty(token.Trim()))
			{
				base.InhibitTokenProcessing = false;
				return;
			}
			else if (token.ToLower() == "end ")
			{
				string endWhat = tr.ReadUntil(lineEndings).Trim();
				switch (endWhat.ToLower())
				{
					case "enum":
					case "sub":
					case "function":
					case "if":
					case "class":
					{
						CodeElement element = (base.TemporaryVariables["CurrentElement"] as CodeElement);
						base.TemporaryVariables["CurrentElement"] = element.Parent;
						break;
					}
					default:
					{
						throw new InvalidOperationException();
						break;
					}
				}
				base.InhibitTokenProcessing = false;
			}
			else if (token.StartsWith("'"))
			{
				string commentText = tr.ReadLine();

				CodeCommentElement comment = new CodeCommentElement();
				comment.Multiline = false;
				if (commentText.StartsWith("''"))
				{
					commentText = commentText.Substring(2);
					comment.Content = commentText;
					comment.IsDocumentationComment = true;
				}
				else
				{
					comment.Content = commentText;
				}
				comment.Content = comment.Content.Trim();

				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(comment);
				}
				else
				{
					CodeMethodElement method = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeMethodElement);
					if (method != null)
					{
						method.Elements.Add(comment);
					}
					else
					{
						CodeConditionalStatementActionElement cond = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeConditionalStatementActionElement);
						if (cond != null)
						{
							if ((bool)base.TemporaryVariables["CurrentElementActionSet"] == true)
							{
								cond.Elements.Add(comment);
							}
							else
							{
								throw new InvalidOperationException();
							}
						}
						else
						{
							elements.Add(comment);
						}
					}
				}
				#endregion
			}
			else if (token.ToLower() == "imports ")
			{
				string importedName = tr.ReadUntil(lineEndings, "\"", "\"");
				string aliasName = null;
				if (importedName.Contains("="))
				{
					aliasName = importedName.Substring(0, importedName.IndexOf('=')).Trim();
					importedName = importedName.Substring(importedName.IndexOf('=') + 1).Trim();
				}

				string[] importedNames = importedName.Split(mvarNamespaceSeparator);
				List<CodeElement> elements = (base.TemporaryVariables["CurrentElements", emptyStack] as List<CodeElement>);
				elements.Add(new CodeNamespaceImportElement(importedNames, aliasName));

				base.InhibitTokenProcessing = false;
			}
			#region Access Modifiers
			else if (token.ToLower() == "friend ")
			{
				if ((bool)base.TemporaryVariables["NextAccessModifierWasFamily", false])
				{
					base.TemporaryVariables["NextAccessModifier"] = CodeAccessModifiers.FamilyANDAssembly;
					base.TemporaryVariables.Remove("NextAccessModifierWasAssembly");
					base.TemporaryVariables.Remove("NextAccessModifierWasFamily");
				}
				else
				{
					base.TemporaryVariables["NextAccessModifier"] = CodeAccessModifiers.Assembly;
					base.TemporaryVariables["NextAccessModifierWasAssembly"] = true;
				}
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "protected ")
			{
				if ((bool)base.TemporaryVariables["NextAccessModifierWasAssembly", false])
				{
					base.TemporaryVariables["NextAccessModifier"] = CodeAccessModifiers.FamilyANDAssembly;
					base.TemporaryVariables.Remove("NextAccessModifierWasAssembly");
					base.TemporaryVariables.Remove("NextAccessModifierWasFamily");
				}
				else
				{
					base.TemporaryVariables["NextAccessModifier"] = CodeAccessModifiers.Family;
					base.TemporaryVariables["NextAccessModifierWasFamily"] = true;
				}
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "public ")
			{
				base.TemporaryVariables["NextAccessModifier"] = CodeAccessModifiers.Public;
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "private ")
			{
				base.TemporaryVariables["NextAccessModifier"] = CodeAccessModifiers.Private;
				base.InhibitTokenProcessing = false;
			}
			#endregion
			else if (token.ToLower() == "shared ")
			{
				base.TemporaryVariables["NextShared"] = true;
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "inherits ")
			{
				CodeClassElement clss = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeClassElement);
				if (clss == null)
				{
				}

				string inheritsWhat = tr.ReadUntil(new string[] { " ", ":", "\r\n", "\r", "\n" });
				string[] baseClassName = inheritsWhat.Split(mvarNamespaceSeparator);
				clss.BaseClassName = baseClassName;

				base.InhibitTokenProcessing = false;
			}
			#region Inline Elements
			else if (token.ToLower() == "for ")
			{
				string declaration = tr.ReadUntil(lineEndings);

				int indexOfEquals = declaration.IndexOf('=');
				int indexOfTo = declaration.IndexOf("To");
				string init = declaration.Substring(0, indexOfEquals).Trim();
				string sFrom = declaration.Substring(indexOfEquals + 1, indexOfTo - indexOfEquals - 1).Trim();
				string sTo = declaration.Substring(indexOfTo + 2).Trim();

				CodeElementReference cerFrom = StringToExpression(sFrom);
				CodeElementReference cerTo = StringToExpression(sTo);

				CodeForLoopActionElement item = new CodeForLoopActionElement();
				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(item);
				}
				else
				{
					elements.Add(item);
				}
				#endregion
				#region Make the element current
				base.TemporaryVariables["CurrentElement"] = item;
				#endregion
			}
			else if (token.ToLower() == "next")
			{
				string declaration = tr.ReadUntil(lineEndings);

				base.TemporaryVariables["CurrentElement"] = ((CodeElement)base.TemporaryVariables["CurrentElement"]).Parent;
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "dim ")
			{
				string declaration = tr.ReadUntil(lineEndings);
				string[] nameAndType = declaration.Split(new string[] { " As " }, StringSplitOptions.None);

				CodeVariableElement var = new CodeVariableElement();
				var.Name = nameAndType[0];
				if (nameAndType.Length == 2)
				{
					string typeName = nameAndType[1];
					string defaultValue = null;
					if (typeName.ToLower().StartsWith("new "))
					{
						int ctorParamsStart = typeName.IndexOf('(') + 1;
						int ctorParamsLength = typeName.IndexOf(')') - ctorParamsStart;
						string constructorParams = typeName.Substring(ctorParamsStart, ctorParamsLength);

						typeName = typeName.Substring(4, ctorParamsStart - 1 - 4);
					}
					else if (typeName.Contains("="))
					{
						defaultValue = typeName.Substring(typeName.IndexOf('=') + 1).Trim();
						typeName = typeName.Substring(0, typeName.IndexOf('=')).Trim();
					}

					typeName = MakeKnownDataType(typeName);
					string[] typeNames = typeName.Split(mvarNamespaceSeparator);
					var.DataType = new CodeDataType(typeNames);

					if (defaultValue != null)
					{
						var.Value = StringToExpression(defaultValue);
					}
				}

				CodeVariableDeclarationElement cvde = new CodeVariableDeclarationElement(var);

				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(cvde);
				}
				else
				{
					elements.Add(var);
				}
				#endregion
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "else")
			{
				// base.TemporaryVariables["CurrentElementActionSet"] = false;
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "if ")
			{
				string condition = tr.ReadUntil("Then").Trim();

				// create a condition from this string
				CodeConditionalStatementActionElement item = CreateConditionFromString(condition);

				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(item);
				}
				else
				{
					CodeMethodElement method = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeMethodElement);
					if (method != null)
					{
						method.Elements.Add(item);
					}
					else
					{
						elements.Add(item);
					}
				}
				#endregion

				#region Make the element current
				base.TemporaryVariables["CurrentElement"] = item;
				base.TemporaryVariables["CurrentElementActionSet"] = true;
				#endregion

				base.InhibitTokenProcessing = false;
			}
			#endregion
			#region Code Element Containers
			else if (token.ToLower() == "namespace ")
			{
				string namespaceName = tr.ReadUntil(lineEndings, "\"", "\"");

				List<CodeElement> elements = (base.TemporaryVariables["CurrentElements", emptyStack] as List<CodeElement>);
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				CodeNamespaceElement ns = new CodeNamespaceElement(namespaceName);
				if (parent != null)
				{
					parent.Elements.Add(ns);
				}
				else
				{
					elements.Add(ns);
				}
				base.TemporaryVariables["CurrentElement"] = ns;

				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "class ")
			{
				string name = tr.ReadUntil(new string[] { " ", ":", "\r\n", "\r", "\n" });

				CodeClassElement clss = new CodeClassElement();
				clss.Name = name;
				clss.AccessModifiers = ((CodeAccessModifiers)base.TemporaryVariables["NextAccessModifier", CodeAccessModifiers.None]);
				clss.IsAbstract = ((bool)base.TemporaryVariables["NextMustInherit", false]);
				clss.IsPartial = ((bool)base.TemporaryVariables["NextPartial", false]);
				clss.IsSealed = ((bool)base.TemporaryVariables["NextNotInheritable", false]);
				clss.IsStatic = ((bool)base.TemporaryVariables["NextShared", false]);

				base.TemporaryVariables.Remove("NextAccessModifier");
				base.TemporaryVariables.Remove("NextMustInherit");
				base.TemporaryVariables.Remove("NextPartial");
				base.TemporaryVariables.Remove("NextNotInheritable");
				base.TemporaryVariables.Remove("NextShared");
				base.TemporaryVariables.Remove("NextAccessModifierWasFamily");
				base.TemporaryVariables.Remove("NextAccessModifierWasAssembly");

				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(clss);
				}
				else
				{
					elements.Add(clss);
				}
				#endregion
				base.TemporaryVariables["CurrentElement"] = clss;

				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "enum ")
			{
				string name = tr.ReadUntil(new string[] { " ", ":", "\r\n", "\r", "\n" });

				CodeEnumerationElement enumm = new CodeEnumerationElement();
				enumm.Name = name;
				enumm.AccessModifiers = ((CodeAccessModifiers)base.TemporaryVariables["NextAccessModifier", CodeAccessModifiers.None]);

				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(enumm);
				}
				else
				{
					elements.Add(enumm);
				}
				#endregion

				base.TemporaryVariables["CurrentElement"] = enumm;
				base.InhibitTokenProcessing = false;
			}
			else if (token.ToLower() == "sub " || token.ToLower() == "function ")
			{
				string methName = tr.ReadUntil(new string[] { "(" });

				CodeMethodElement item = new CodeMethodElement();
				item.Name = methName;
				item.AccessModifiers = ((CodeAccessModifiers)base.TemporaryVariables["NextAccessModifier", CodeAccessModifiers.None]);
				item.IsAbstract = ((bool)base.TemporaryVariables["NextMustInherit", false]);
				item.IsStatic = ((bool)base.TemporaryVariables["NextShared", false]);

				base.TemporaryVariables.Remove("NextAccessModifier");
				base.TemporaryVariables.Remove("NextMustInherit");
				base.TemporaryVariables.Remove("NextShared");
				base.TemporaryVariables.Remove("NextAccessModifierWasFamily");
				base.TemporaryVariables.Remove("NextAccessModifierWasAssembly");

				string ParameterList = tr.ReadLine();
				if (token.ToLower() == "function ")
				{
					int iDataTypeStart = ParameterList.LastIndexOf(" As ") + 4;
					string DataType = ParameterList.Substring(iDataTypeStart, ParameterList.Length - iDataTypeStart);
					item.DataType = MakeKnownDataType(DataType);

					ParameterList = ParameterList.Substring(0, iDataTypeStart - 4);
				}

				if (ParameterList.EndsWith(")")) ParameterList = ParameterList.Substring(0, ParameterList.Length - 1);

				if (!String.IsNullOrEmpty(ParameterList))
				{
					string[] ParameterListEntries = ParameterList.Split(new char[] { ',' });
					foreach (string ParameterListEntry in ParameterListEntries)
					{
						CodeVariableElement varr = new CodeVariableElement();
						string ParamName = ParameterListEntry.Substring(0, ParameterListEntry.IndexOf(" As "));
						if (ParameterListEntry.StartsWith("ByRef "))
						{
							varr.PassByReference = true;
							ParamName = ParamName.Substring(7);
						}
						else if (ParameterListEntry.StartsWith("ByVal "))
						{
							ParamName = ParamName.Substring(7);
						}

						string ParamDataType = ParameterListEntry.Substring(ParameterListEntry.IndexOf(" As ") + 4);
						if (ParamDataType.EndsWith("()"))
						{
							ParamDataType = ParamDataType.Substring(0, ParamDataType.Length - 2);
							varr.DataType = new CodeDataType(true, ParamDataType.Split(mvarNamespaceSeparator));
						}
						else
						{
							varr.DataType = new CodeDataType(ParamDataType.Split(mvarNamespaceSeparator));
						}
						varr.Name = ParamName;
						item.Parameters.Add(varr);
					}
				}

				#region Add the element to the list
				List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
				CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
				if (parent != null)
				{
					parent.Elements.Add(item);
				}
				else
				{
					elements.Add(item);
				}
				#endregion

				base.TemporaryVariables["CurrentElement"] = item;

				base.InhibitTokenProcessing = false;
			}
			#endregion
			else if (token.EndsWith("("))
			{
				string parameterList = tr.ReadUntil(new string[] { ")" });
				string[] parameters = parameterList.Split(new string[] { "," }, "\"", "\"", StringSplitOptions.None, -1, false);

				CodeMethodCallElement action = new CodeMethodCallElement();
				string actionName = token.Substring(0, token.Length - 1);
				string[] objectAndMethodName = actionName.Split(mvarNamespaceSeparator);
				string[] objectName = new string[objectAndMethodName.Length - 1];
				Array.Copy(objectAndMethodName, 0, objectName, 0, objectName.Length);

				action.ObjectName = objectName;
				action.MethodName = objectAndMethodName[objectAndMethodName.Length - 1];

				foreach (string parameter in parameters)
				{
					string param = parameter.Trim();

					CodeDataType dataType = CodeDataType.Object;
					object paramValue = StringToExpression(param, out dataType);
					action.Parameters.Add("Parameter" + Array.IndexOf<string>(parameters, parameter), dataType, new CodeElementReference(new CodeLiteralElement(paramValue)));
				}

				CodeMethodElement meth = (base.TemporaryVariables["CurrentElement", null] as CodeMethodElement);
				if (meth != null)
				{
					meth.Elements.Add(action);
				}
				else
				{
					#region Add the element to the list
					List<CodeElement> elements = (List<CodeElement>)base.TemporaryVariables["CurrentElements", emptyStack];
					CodeElementContainerElement parent = ((CodeElement)base.TemporaryVariables["CurrentElement"] as CodeElementContainerElement);
					if (parent != null)
					{
						parent.Elements.Add(action);
					}
					else
					{
						elements.Add(action);
					}
					#endregion
				}
				base.InhibitTokenProcessing = false;
			}
			else if (token.Contains("=") && ((base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement) != null))
			{
				string name = token.Substring(0, token.IndexOf('=')).Trim();
				string value = tr.ReadUntil(new string[] { ":", "\r\n", "\r", "\n" }).Trim();

				Int32 valueValue = Int32.Parse(value);

				CodeEnumerationElement enumm = (base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement);
				enumm.Values.Add(name, valueValue);
				base.InhibitTokenProcessing = false;
			}
			else if (token.ContainsAny("\r", "\n") && ((base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement) != null))
			{
				int idx = token.IndexOf('=');
				if (idx > -1)
				{
					string name = token.Substring(0, token.IndexOf('=')).Trim();
					string value = tr.ReadUntil(new string[] { ":", "\r\n", "\r", "\n" }).Trim();

					Int32 valueValue = Int32.Parse(value);
					CodeEnumerationElement enumm = (base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement);
					enumm.Values.Add(name, valueValue);
				}
				else
				{
					string name = token.Substring(0, token.IndexOfAny(new char[] { '\r', '\n' }));
					CodeEnumerationElement enumm = (base.TemporaryVariables["CurrentElement", null] as CodeEnumerationElement);
					enumm.Values.Add(name);
				}
				base.InhibitTokenProcessing = false;
			}
			else
			{
				base.InhibitTokenProcessing = true;
			}
		}

		private CodeConditionalStatementActionElement CreateConditionFromString(string condition)
		{
			CodeConditionalStatementActionElement cond = new CodeConditionalStatementActionElement();

			if (condition.StartsWith("Not "))
			{
				cond.Negate = true;
				condition = condition.Substring(4);
			}

			if (condition.Contains("=") || condition.Contains("<") || condition.Contains(">") || condition.Contains(" "))
			{
				// this is a complex condition

			}
			else
			{
				// this is a variable
				string[] objectNames = condition.Split(new string[] { mvarNamespaceSeparator });
				string[] realObjectNames = new string[objectNames.Length - 1];
				Array.Copy(objectNames, 0, realObjectNames, 0, realObjectNames.Length);

				string propertyName = objectNames[objectNames.Length - 1];
				cond.Expression = new CodeElementDynamicReference(propertyName, realObjectNames);
			}
			return cond;
		}

		/// <summary>
		/// Converts a string to a <see cref="CodeElementReference" /> and indicates the
		/// <see cref="CodeDataType" /> of the resulting expression.
		/// </summary>
		/// <param name="param"></param>
		/// <param name="dataType"></param>
		/// <returns></returns>
		protected override CodeElementReference StringToExpression(string param, out CodeDataType dataType)
		{
			if (param.ToLower() == "nothing")
			{
				dataType = CodeDataType.Empty;
				return new CodeElementReference(new CodeLiteralElement(null));
			}
			else if (param.ToLower() == "true")
			{
				dataType = CodeDataType.Boolean;
				return new CodeElementReference(new CodeLiteralElement(true));
			}
			else if (param.ToLower() == "false")
			{
				dataType = CodeDataType.Boolean;
				return new CodeElementReference(new CodeLiteralElement(false));
			}
			else if (param.StartsWith("\"") && param.EndsWith("\""))
			{
				dataType = CodeDataType.String;
				return new CodeElementReference(new CodeLiteralElement(param.Substring(1, param.Length - 2)));
			}
			else if (param.StartsWith("#") && param.EndsWith("#"))
			{
				dataType = CodeDataType.DateTime;
				return new CodeElementReference(new CodeLiteralElement(DateTime.Parse(param.Substring(1, param.Length - 2))));
			}
			else if (param.StartsWith("\"") && (param.EndsWith("\"c") || param.EndsWith("\"C")))
			{
				dataType = CodeDataType.Char;
				return new CodeElementReference(new CodeLiteralElement(param.Substring(1, 1)[0]));
			}
			else if (param.EndsWith("f") || param.EndsWith("F"))
			{
				float value = Single.Parse(param.Substring(0, param.Length - 1));
				dataType = CodeDataType.Single;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (param.EndsWith("r") || param.EndsWith("R"))
			{
				double value = Double.Parse(param.Substring(0, param.Length - 1));
				dataType = CodeDataType.Double;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (param.EndsWith("d") || param.EndsWith("D"))
			{
				decimal value = Decimal.Parse(param.Substring(0, param.Length - 1));
				dataType = CodeDataType.Decimal;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (param.EndsWith("ui") || param.EndsWith("UI"))
			{
				uint value = UInt32.Parse(param.Substring(0, param.Length - 1));
				dataType = CodeDataType.UInt32;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (param.EndsWith("us") || param.EndsWith("US"))
			{
				ushort value = UInt16.Parse(param.Substring(0, param.Length - 1));
				dataType = CodeDataType.UInt16;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (param.EndsWith("s") || param.EndsWith("S"))
			{
				short value = Int16.Parse(param.Substring(0, param.Length - 1));
				dataType = CodeDataType.Int16;
				return new CodeElementReference(new CodeLiteralElement(value));
			}
			else if (param.Contains("."))
			{
				double value = -1;
				if (Double.TryParse(param, out value))
				{
					dataType = CodeDataType.Double;
					return new CodeElementReference(new CodeLiteralElement(value));
				}
				else
				{
					string[] objectName = param.Split(new string[] { mvarNamespaceSeparator });
					string[] objectNames = new string[objectName.Length - 1];
					Array.Copy(objectName, 0, objectNames, 0, objectNames.Length);
					
					string name = objectName[objectName.Length - 1];

					dataType = CodeDataType.Empty;
					return new CodeElementDynamicReference(name, objectNames);
				}
			}
			else
			{
				int value = -1;
				if (Int32.TryParse(param, out value))
				{
					dataType = CodeDataType.Int32;
					return new CodeElementReference(new CodeLiteralElement(value));
				}
				else
				{
					dataType = CodeDataType.Empty;
					return new CodeElementDynamicReference(param, new string[0]);
				}
			}
		}

		protected override string MakeFriendlyDataTypeInternal(string DataType)
		{
			switch (DataType)
			{
				case "System.Boolean":
				{
					return "Boolean";
				}
				case "System.Byte":
				{
					return "Byte";
				}
				case "System.Char":
				{
					return "Char";
				}
				case "System.DateTime":
				{
					return "Date";
				}
				case "System.Decimal":
				{
					return "Decimal";
				}
				case "System.Double":
				{
					return "Double";
				}
				case "System.Int16":
				{
					return "Short";
				}
				case "System.Int32":
				{
					return "Integer";
				}
				case "System.Int64":
				{
					return "Long";
				}
				case "System.SByte":
				{
					return "SByte";
				}
				case "System.Single":
				{
					return "Single";
				}
				case "System.String":
				{
					return "String";
				}
				case "System.UInt16":
				{
					return "UShort";
				}
				case "System.UInt32":
				{
					return "UInteger";
				}
				case "System.UInt64":
				{
					return "ULong";
				}
			}
			return base.MakeFriendlyDataType(DataType);
		}
		protected override string MakeKnownDataType(string DataType)
		{
			switch (DataType)
			{
				case "Boolean":
				{
					return "System.Boolean";
				}
				case "Byte":
				{
					return "System.Byte";
				}
				case "Char":
				{
					return "System.Char";
				}
				case "Date":
				{
					return "System.DateTime";
				}
				case "Decimal":
				{
					return "System.Decimal";
				}
				case "Double":
				{
					return "System.Double";
				}
				case "Short":
				{
					return "System.Int16";
				}
				case "Integer":
				{
					return "System.Int32";
				}
				case "Long":
				{
					return "System.Int64";
				}
				case "SByte":
				{
					return "System.SByte";
				}
				case "Single":
				{
					return "System.Single";
				}
				case "String":
				{
					return "System.String";
				}
				case "UShort":
				{
					return "System.UInt16";
				}
				case "UInteger":
				{
					return "System.UInt32";
				}
				case "ULong":
				{
					return "System.UInt64";
				}
			}
			return base.MakeKnownDataType(DataType);
		}

		protected internal override string GenerateCode(object obj, int indentCount)
		{
			string indent = GetIndentString(indentCount);
			StringBuilder sb = new StringBuilder();
			if (obj is CodeAccessModifiers)
			{
				switch ((CodeAccessModifiers)obj)
				{
					case CodeAccessModifiers.Assembly:
					{
						sb.Append("Friend");
						break;
					}
					case CodeAccessModifiers.Family:
					{
						sb.Append("Protected");
						break;
					}
					case CodeAccessModifiers.FamilyANDAssembly:
					case CodeAccessModifiers.FamilyORAssembly:
					{
						sb.Append("Protected Friend");
						break;
					}
					case CodeAccessModifiers.Private:
					{
						sb.Append("Private");
						break;
					}
					case CodeAccessModifiers.Public:
					{
						sb.Append("Public");
						break;
					}
				}
			}
			else if (obj is CodeNamespaceElement)
			{
				CodeNamespaceElement namespaceEl = (obj as CodeNamespaceElement);
				sb.Append(indent + "Namespace ");
				sb.AppendLine(namespaceEl.GetFullName(mvarNamespaceSeparator));
				foreach (CodeElement el in namespaceEl.Elements)
				{
					sb.AppendLine(GenerateCode(el, indentCount + 1));
				}
				sb.Append(indent + "End Namespace");
			}
			else if (obj is CodeClassElement)
			{
				CodeClassElement classEl = (obj as CodeClassElement);
				sb.Append(indent);
				sb.Append(GenerateCode(classEl.AccessModifiers));
				if (classEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				if (classEl.IsAbstract)
				{
					sb.Append("MustInherit ");
				}
				else if (classEl.IsSealed)
				{
					sb.Append("NotInheritable ");
				}
				if (classEl.IsPartial)
				{
					sb.Append("Partial ");
				}
				sb.Append("Class " + classEl.Name);
				if (classEl.GenericParameters.Count > 0)
				{
					sb.Append("(Of ");
					foreach (CodeVariableElement param in classEl.GenericParameters)
					{
						sb.Append(param.Name);
						if (classEl.GenericParameters.IndexOf(param) < classEl.GenericParameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append(")");
				}
				sb.AppendLine();
				if (classEl.BaseClassName.Length > 0)
				{
					sb.Append(GetIndentString(indentCount + 1));
					sb.Append("Inherits ");
					sb.AppendLine(String.Join(".", classEl.BaseClassName));
				}
				foreach (CodeElement el in classEl.Elements)
				{
					sb.AppendLine(GenerateCode(el, indentCount + 1));
				}
				sb.Append(indent + "End Class");
			}
			else if (obj is CodeMethodElement)
			{
				CodeMethodElement methodEl = (obj as CodeMethodElement);
				sb.Append(indent);
				sb.Append(GenerateCode(methodEl.AccessModifiers));
				if (methodEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				if (String.IsNullOrEmpty(methodEl.DataType))
				{
					sb.Append("Sub ");
				}
				else
				{
					sb.Append("Function ");
				}
				sb.Append(methodEl.Name);
				if (methodEl.GenericParameters.Count > 0)
				{
					sb.Append("(Of ");
					foreach (CodeVariableElement param in methodEl.GenericParameters)
					{
						sb.Append(param.Name);
						if (methodEl.GenericParameters.IndexOf(param) < methodEl.GenericParameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append(")");
				}
				sb.Append("(");
				foreach (CodeVariableElement param in methodEl.Parameters)
				{
					if (param.PassByReference)
					{
						sb.Append("ByRef");
					}
					else
					{
						sb.Append("ByVal");
					}
					sb.Append(" " + param.Name + " As " + MakeFriendlyDataType(param.DataType));
					if (methodEl.Parameters.IndexOf(param) < methodEl.Parameters.Count - 1)
					{
						sb.Append(", ");
					}
				}
				sb.Append(")");
				if (!String.IsNullOrEmpty(methodEl.DataType))
				{
					sb.Append(" As " + MakeFriendlyDataType(methodEl.DataType));
				}
				sb.AppendLine();
				foreach (CodeElement el in methodEl.Elements)
				{
					sb.AppendLine(GenerateCode(el, indentCount + 1));
				}
				sb.Append(indent + "End ");
				if (String.IsNullOrEmpty(methodEl.DataType))
				{
					sb.Append("Sub");
				}
				else
				{
					sb.Append("Function");
				}
			}
			else if (obj is CodePropertyElement)
			{
				sb.Append(indent);
				CodePropertyElement propertyEl = (obj as CodePropertyElement);
				sb.Append(GenerateCode(propertyEl.AccessModifiers));
				if (propertyEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
				if (propertyEl.IsAbstract)
				{
					sb.Append("MustOverride ");
				}
				else if (propertyEl.IsVirtual)
				{
					sb.Append("Overridable ");
				}
				else if (propertyEl.IsOverriding)
				{
					sb.Append("Overrides ");
				}
				sb.Append("Property " + propertyEl.Name);
				sb.Append("(");
				if (propertyEl.Parameters.Count > 0)
				{
					foreach (CodeVariableElement varEl in propertyEl.Parameters)
					{
						if (varEl.PassByReference)
						{
							sb.Append("ByRef");
						}
						else
						{
							sb.Append("ByVal");
						}
						sb.Append(varEl.Name + " As " + MakeFriendlyDataType(varEl.DataType));
						if (propertyEl.Parameters.IndexOf(varEl) < propertyEl.Parameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
				}
				sb.Append(")");
				sb.Append(" As " + MakeFriendlyDataType(propertyEl.DataType));
			}
			else if (obj is CodeVariableElement)
			{
				sb.Append(indent);
				CodeVariableElement varEl = (obj as CodeVariableElement);
				sb.Append(GenerateCode(varEl.AccessModifiers));
				if (varEl.AccessModifiers == CodeAccessModifiers.None) sb.Append("Dim");
				sb.Append(" " + varEl.Name);
				if (!String.IsNullOrEmpty(varEl.DataType))
				{
					sb.Append(" As " + MakeFriendlyDataType(varEl.DataType));
				}
				if (varEl.Value != null)
				{
					sb.Append(" = ");
					sb.Append(GenerateCode(varEl.Value.Value));
				}
			}
			else if (obj is CodeEnumerationElement)
			{
				sb.Append(indent);
				CodeEnumerationElement enumEL = (obj as CodeEnumerationElement);
				sb.Append(GenerateCode(enumEL.AccessModifiers));
				sb.AppendLine(" Enum " + enumEL.Name);

				string indent2 = GetIndentString(indentCount + 1);
				foreach (CodeEnumerationValue value in enumEL.Values)
				{
					sb.Append(indent2 + value.Name.Trim());
					if (value.IsValueDefined) sb.Append(" = " + value.Value);
					sb.AppendLine();
				}
				sb.Append(indent + "End Enum");
			}
			else if (obj is CodeMethodCallElement)
			{
				CodeMethodCallElement call = (obj as CodeMethodCallElement);
				sb.Append(indent);
				if (call.ObjectName.Length > 0)
				{
					string objname = String.Join(".", call.ObjectName);
					sb.Append(objname);
					sb.Append('.');
				}
				sb.Append(call.MethodName + "(");
				foreach (CodeVariableElement var in call.Parameters)
				{
					if (var.Value is CodeElementEnumerationValueReference)
					{
						CodeElementEnumerationValueReference cevr = (var.Value as CodeElementEnumerationValueReference);
						if (cevr.ObjectName.Length > 0)
						{
							sb.Append(String.Join(".", cevr.ObjectName));
							sb.Append(".");
						}
						sb.Append(cevr.ValueName);
					}
					else if (var.Value.Value is CodeLiteralElement)
					{
						CodeLiteralElement lit = (var.Value.Value as CodeLiteralElement);
						if (lit.Value.GetType() == typeof(DateTime))
						{
							string dt = ((DateTime)lit.Value).ToString("G");
							sb.Append("#");
							sb.Append(dt);
							sb.Append("#");
						}
						else if (lit.Value.GetType() == typeof(String))
						{
							string val = lit.Value.ToString();
							val = val.Replace("\"", "\"\"");

							sb.Append("\"");
							sb.Append(val);
							sb.Append("\"");
						}
						else if (lit.Value.GetType() == typeof(Int16))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("S");
						}
						else if (lit.Value.GetType() == typeof(Int32))
						{
							sb.Append(lit.Value.ToString());
						}
						else if (lit.Value.GetType() == typeof(Int64))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("L");
						}
						else if (lit.Value.GetType() == typeof(UInt16))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("US");
						}
						else if (lit.Value.GetType() == typeof(UInt32))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("UI");
						}
						else if (lit.Value.GetType() == typeof(UInt64))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("UL");
						}
						else if (lit.Value.GetType() == typeof(Single))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("F");
						}
						else if (lit.Value.GetType() == typeof(Double))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("R");
						}
						else if (lit.Value.GetType() == typeof(Decimal))
						{
							sb.Append(lit.Value.ToString());
							sb.Append("D");
						}
						else if (lit.Value.GetType() == typeof(Char))
						{
							sb.Append("\"");
							sb.Append(lit.Value.ToString());
							sb.Append("\"");
							sb.Append("c");
						}
					}
					else
					{
						sb.Append(var.Value);
					}

					if (call.Parameters.IndexOf(var) < call.Parameters.Count - 1)
					{
						sb.Append(", ");
					}
				}
				sb.Append(")");
			}
			else if (obj is CodeCommentElement)
			{
				CodeCommentElement comment = (obj as CodeCommentElement);
				string[] commentLines = comment.Content.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
				foreach (string commentLine in commentLines)
				{
					if (comment.IsDocumentationComment)
					{
						sb.Append("''' ");
					}
					else
					{
						sb.Append("' ");
					}
					sb.Append(commentLine);
					if (Array.IndexOf<string>(commentLines, commentLine) < commentLines.Length - 1) sb.AppendLine();
				}
			}
			else
			{
			}
			return sb.ToString();
		}
	}
}
