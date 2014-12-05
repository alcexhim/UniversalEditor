﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

using UniversalEditor.Collections.Generic;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.PropertyList;

//TODO: Fix attribute parsing in XML. Got fucked up somehow.


namespace UniversalEditor.DataFormats.Markup.XML
{
	public class XMLDataFormat : DataFormat
	{
		public XMLDataFormat()
		{
			// Settings.AutoCloseTagNames.Add("hr");
			// Settings.AutoCloseTagNames.Add("br");
			// Settings.AutoCloseTagNames.Add("img");
			Settings.Entities.Add("amp", "&");
			Settings.Entities.Add("quot", "\"");
			Settings.Entities.Add("copy", "©");
			Settings.Entities.Add("apos", "'");
		}

		private XMLDataFormatSettings mvarSettings = new XMLDataFormatSettings();
		public XMLDataFormatSettings Settings { get { return mvarSettings; } }

		private string mvarCompensateTopLevelTagName = null;
		/// <summary>
		/// The name of the top-level tag to insert if there exists more than one top-level element in the document. If this value is null, no compensation will be performed.
		/// </summary>
		public string CompensateTopLevelTagName { get { return mvarCompensateTopLevelTagName; } set { mvarCompensateTopLevelTagName = value; } }

		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Title = "eXtensible Markup Language";

			dfr.Filters.Clear();
			dfr.Filters.Add("eXtensible Markup Language", new byte?[][] { new byte?[] { (byte)'<', (byte)'?', (byte)'x', (byte)'m', (byte)'l' } }, new string[] { "*.xml" });

			dfr.Capabilities.Clear();
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		public void WriteElement(MarkupElement element)
		{
			this.WriteElement(element, 0);
		}
		public void WriteElement(MarkupElement element, int indentLevel)
		{
			IO.Writer tw = base.Accessor.Writer;

			string indent = String.Empty;
			if (mvarSettings.PrettyPrint) indent = new string(' ', indentLevel * 4);

			if (element is MarkupLiteralElement)
			{
				tw.Write(ReplaceEntitiesOutput(element.Value));
			}
			else
			{
				if (element is MarkupStringElement)
				{
					tw.Write(Settings.TagBeginChar.ToString() + Settings.TagSpecialDeclarationStartChar.ToString() + "[" + element.Name + "[" + element.Value + "]]" + Settings.TagEndChar);
				}
				else
				{
					if (element is MarkupPreprocessorElement)
					{
						if (element.Value.Contains(Environment.NewLine))
						{
							tw.Write(Settings.TagBeginChar.ToString() + Settings.PreprocessorChar.ToString() + element.Name + " ");
							
							if (mvarSettings.PrettyPrint) tw.WriteLine();
							tw.Write(element.Value);
							if (mvarSettings.PrettyPrint) tw.WriteLine();

							tw.Write(Settings.PreprocessorChar.ToString() + Settings.TagEndChar.ToString());
							if (mvarSettings.PrettyPrint) tw.WriteLine();
						}
						else
						{
							tw.Write(indent + Settings.TagBeginChar.ToString() + Settings.PreprocessorChar.ToString() + element.Name + " ");
							tw.Write(element.Value + " ");
							tw.Write(Settings.PreprocessorChar.ToString() + Settings.TagEndChar.ToString());
							if (mvarSettings.PrettyPrint) tw.WriteLine();
						}
					}
					else
					{
						if (element is MarkupTagElement)
						{
							MarkupTagElement tag = element as MarkupTagElement;
							tw.Write(indent + Settings.TagBeginChar.ToString() + element.FullName);
							if (tag.Attributes.Count > 0)
							{
								tw.Write(" ");
							}
							foreach (MarkupAttribute att in tag.Attributes)
							{
								tw.Write(att.FullName + "=\"" + this.ReplaceEntitiesOutput(att.Value) + "\"");
								if (tag.Attributes.IndexOf(att) < tag.Attributes.Count - 1)
								{
									tw.Write(" ");
								}
							}
							if (tag.Elements.Count == 0)
							{
								if (String.IsNullOrEmpty(element.Value))
								{
									tw.Write(" " + Settings.TagCloseChar.ToString() + Settings.TagEndChar.ToString());
									if (mvarSettings.PrettyPrint) tw.WriteLine();
								}
								else
								{
									tw.Write(Settings.TagEndChar.ToString());
									tw.Write(ReplaceEntitiesOutput(element.Value) + Settings.TagBeginChar.ToString() + Settings.TagCloseChar.ToString() + element.FullName + Settings.TagEndChar.ToString());
									if (mvarSettings.PrettyPrint) tw.WriteLine();
								}
							}
							else
							{
								tw.Write(Settings.TagEndChar.ToString());
								if (mvarSettings.PrettyPrint) tw.WriteLine();
							}
						}
						else
						{
							if (element is MarkupCommentElement)
							{
								tw.Write(Settings.TagBeginChar);
								tw.Write(Settings.TagSpecialDeclarationStartChar.ToString());
								tw.Write(Settings.TagSpecialDeclarationCommentStart);
								bool containsNewline = element.Value.ContainsAny(new string[]
								{
									"\r", 
									"\n"
								});
								if (containsNewline)
								{
									tw.WriteLine();
								}
								else
								{
									tw.Write(' ');
								}
								tw.Write(element.Value.Trim());
								if (containsNewline)
								{
									tw.WriteLine();
								}
								else
								{
									tw.Write(' ');
								}
								tw.Write(Settings.TagSpecialDeclarationCommentStart);
								tw.Write(Settings.TagEndChar);
								if (mvarSettings.PrettyPrint) tw.WriteLine();
							}
						}
					}
				}
			}
			if (element is MarkupContainerElement)
			{
				MarkupContainerElement ce = element as MarkupContainerElement;
				for (int i = 0; i < ce.Elements.Count; i++)
				{
					WriteElement(ce.Elements[i], indentLevel + 1);
				}
				if (ce.Elements.Count > 0)
				{
					tw.Write(indent + Settings.TagBeginChar.ToString() + Settings.TagCloseChar.ToString() + element.FullName + Settings.TagEndChar.ToString());
					if (mvarSettings.PrettyPrint) tw.WriteLine();
				}
			}
		}
		public void WriteStartDocument()
		{
			this.WriteStartPreprocessor("xml");
			this.WriteAttribute("version", "1.0");
			this.WriteAttribute("encoding", "UTF-8");
			if (Settings.IsStandalone)
			{
				this.WriteAttribute("standalone", "yes");
			}
			this.WriteEndPreprocessor();
		}
		public void WriteStartPreprocessor(string name)
		{
			IO.Writer tw = base.Accessor.Writer;
			tw.Write(this.Settings.TagBeginChar);
			tw.Write(this.Settings.PreprocessorChar);
			tw.Write(name + " ");
		}
		public void Write(string content)
		{
			this.Write(content, 0);
		}
		public void Write(string content, int indentLevel)
		{
			IO.Writer tw = base.Accessor.Writer;
			string indent = new string(' ', indentLevel * 4);
			tw.Write(indent + content);
		}
		public void WriteEndPreprocessor()
		{
			IO.Writer tw = base.Accessor.Writer;
			tw.Write(" " + this.Settings.PreprocessorChar + this.Settings.TagEndChar);
		}
		public void WritePreprocessor(string name, string content)
		{
			this.WriteStartPreprocessor(name);
			this.Write(content);
			this.WriteEndPreprocessor();
		}
		public void WriteStartTag(string tag)
		{
			this.WriteStartTag(null, tag);
		}
		public void WriteStartTag(string nameSpace, string tag)
		{
			IO.Writer tw = base.Accessor.Writer;
			tw.Write(this.Settings.TagBeginChar);
			if (nameSpace != null)
			{
				tw.Write(nameSpace);
				tw.Write(this.Settings.TagNamespaceSeparatorChar);
			}
			tw.Write(tag);
			tw.Write(this.Settings.TagEndChar);
		}
		public void WriteEndTag(string tag)
		{
			this.WriteEndTag(null, tag);
		}
		public void WriteEndTag(string nameSpace, string tag)
		{
			IO.Writer tw = base.Accessor.Writer;
			tw.Write(Settings.TagBeginChar);
			tw.Write(Settings.TagCloseChar);
			if (nameSpace != null)
			{
				tw.Write(nameSpace);
				tw.Write(Settings.TagNamespaceSeparatorChar);
			}
			tw.Write(tag);
			tw.Write(Settings.TagEndChar);
		}
		public void WriteTag(string tag)
		{
			this.WriteTag(tag, null);
		}
		public void WriteTag(string tag, string value)
		{
			this.WriteTag(null, tag, value);
		}
		public void WriteTag(string nameSpace, string tag, string value)
		{
			IO.Writer tw = base.Accessor.Writer;
			tw.Write(this.Settings.TagBeginChar);
			if (nameSpace != null)
			{
				tw.Write(nameSpace);
				tw.Write(this.Settings.TagNamespaceSeparatorChar);
			}
			tw.Write(tag);
			if (value == null)
			{
				tw.Write(" " + this.Settings.TagCloseChar);
			}
			tw.Write(this.Settings.TagEndChar);
			if (value != null)
			{
				tw.Write(value);
				tw.Write(this.Settings.TagBeginChar);
				tw.Write(this.Settings.TagCloseChar);
				if (nameSpace != null)
				{
					tw.Write(nameSpace);
					tw.Write(this.Settings.TagNamespaceSeparatorChar);
				}
				tw.Write(tag);
				tw.Write(this.Settings.TagEndChar);
			}
		}
		public void WriteAttribute(string name, string value)
		{
			this.WriteAttribute(null, name, value);
		}
		public void WriteAttribute(string nameSpace, string name, string value)
		{
			IO.Writer tw = base.Accessor.Writer;
			if (nameSpace != null)
			{
				tw.Write(nameSpace + this.Settings.TagNamespaceSeparatorChar);
			}
			tw.Write(name);
			tw.Write(this.Settings.AttributeNameValueSeparatorChar);
			tw.Write(value);
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader tr = base.Accessor.Reader;
			MarkupObjectModel mom = new MarkupObjectModel();
			bool insideTagDecl = false;
			int insideString = 0;
			bool closingCurrentElement = false;
			string currentString = string.Empty;
			MarkupElement currentElement = null;
			bool insidePreprocessor = false;
			bool insideAttributeArea = false;
			bool insideSpecialDeclaration = false;
			bool insideTagValue = false;
			char prevChar = '\0';
			string nextAttributeName = null;
			bool loaded = false;

			char c = tr.ReadChar();
			int times = 0, maxtimes = 5;
			while (c != '<')
			{
				// clear out junk
				c = tr.ReadChar();
				times++;
				if (times == maxtimes) break;
			}
			tr.Accessor.Seek(-1, IO.SeekOrigin.Current);

			while (!tr.EndOfStream)
			{
				c = tr.ReadChar();
				if (!loaded && (c != '<'))
				{
					return;
				}
				else
				{
					loaded = true;
				}
				if (c == this.Settings.TagBeginChar)
				{
					if (insideString == 0 && !insidePreprocessor)
					{
						insideTagDecl = true;
						insideTagValue = false;
						if (!string.IsNullOrEmpty(currentString) && currentElement != null)
						{
							MarkupElement expr_8F = currentElement;
							expr_8F.Value += currentString;
							currentElement.Value = this.ReplaceEntitiesInput(currentElement.Value.Trim());
							currentString = string.Empty;
						}
					}
					else
					{
						currentString += c;
					}
				}
				else
				{
					if (c == this.Settings.TagSpecialDeclarationStartChar)
					{
						if (insideString == 0 && !insidePreprocessor && !insideTagValue && !insideAttributeArea && prevChar == this.Settings.TagBeginChar)
						{
							insideSpecialDeclaration = true;
							continue;
						}
						currentString += c;
					}
					else
					{
						if (c == this.Settings.TagEndChar)
						{
							if (insideString == 0 && ((insidePreprocessor && prevChar == this.Settings.PreprocessorChar) || prevChar != this.Settings.PreprocessorChar))
							{
								insideTagDecl = false;
								if (currentString.EndsWith(this.Settings.TagSpecialDeclarationCommentStart))
								{
									currentString = currentString.Substring(0, currentString.Length - this.Settings.TagSpecialDeclarationCommentStart.Length);
									currentElement.Value = this.ReplaceEntitiesInput(currentString);
									currentString = string.Empty;
									currentElement = currentElement.Parent;
									continue;
								}
								if (insidePreprocessor && prevChar == this.Settings.PreprocessorChar)
								{
									MarkupElement prevElement = currentElement;
									currentElement = new MarkupPreprocessorElement();
									string name = string.Empty;
									string content = currentString;
									if (currentString.Contains(" "))
									{
										name = currentString.Substring(0, currentString.IndexOf(' '));
										content = currentString.Substring(name.Length + 1);
									}
									currentElement.Name = name.Trim();
									currentElement.Value = content.Trim();
									if (prevElement != null && prevElement is MarkupContainerElement)
									{
										(prevElement as MarkupContainerElement).Elements.Add(currentElement);
									}
									else
									{
										mom.Elements.Add(currentElement);
									}
									currentElement = prevElement;
									insidePreprocessor = false;
								}
								else
								{
									if (closingCurrentElement)
									{
										if (currentElement != null)
										{
											if (currentElement.FullName == currentString || prevChar == this.Settings.TagCloseChar)
											{
												currentElement = currentElement.Parent;
											}
											else
											{
												if (Settings.AutoCloseTagNames.Contains(currentElement.Name))
												{
													currentElement = currentElement.Parent;
													if (currentElement != null)
													{
														currentElement = currentElement.Parent;
													}
												}
												else
												{
													Console.WriteLine("uds: XMLDataFormat: attempted to close element '" + currentElement.Name + "'; invalid XML?");
												}
											}
										}
										else
										{
											Console.WriteLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:144     currentElement is null");
										}
										closingCurrentElement = false;
									}
									else
									{
										if (insideSpecialDeclaration)
										{
											insideSpecialDeclaration = false;
										}
										else
										{
											insideTagValue = true;
											if (insideAttributeArea)
											{
												(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(currentString));
												nextAttributeName = null;
												insideAttributeArea = false;
											}
											else
											{
												MarkupElement prevElement = currentElement;
												currentElement = new MarkupTagElement();
												currentElement.FullName = currentString.Trim();
												if (prevElement != null && prevElement is MarkupContainerElement)
												{
													(prevElement as MarkupContainerElement).Elements.Add(currentElement);
												}
												else
												{
													mom.Elements.Add(currentElement);
												}
												if (Settings.AutoCloseTagNames.Contains(currentElement.Name))
												{
													currentElement = currentElement.Parent;
												}
											}
										}
									}
								}
								currentString = string.Empty;
								insideAttributeArea = false;
							}
							else
							{
								currentString += c;
							}
						}
						else
						{
							if (c == this.Settings.TagCloseChar)
							{
								if (insideString == 0 && !insidePreprocessor && insideTagDecl)
								{
									if (currentElement == null)
									{
										goto IL_4CC;
									}
								IL_4CC:
									if (insideAttributeArea && currentElement is MarkupTagElement && nextAttributeName != null)
									{
										if (nextAttributeName != null)
										{
											(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(currentString));
											nextAttributeName = null;
											currentString = string.Empty;
										}
									}
									else
									{
										if (!insideAttributeArea)
										{
											if (currentElement != null)
											{
												if (currentElement is MarkupTagElement && !string.IsNullOrEmpty(currentString))
												{
													MarkupTagElement e = new MarkupTagElement();
													e.Name = currentString;
													(currentElement as MarkupTagElement).Elements.Add(e);
													currentElement = e;
												}
											}
											else
											{
												Console.WriteLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:199     currentElement is null");
											}
										}
									}
									currentString = string.Empty;
									closingCurrentElement = true;
								}
								else
								{
									currentString += c;
								}
							}
							else
							{
								if (c == this.Settings.AttributeNameValueSeparatorChar)
								{
									if (insideString == 0 && !insidePreprocessor && !insideTagValue)
									{
										if (insideAttributeArea)
										{
											nextAttributeName = currentString;
											currentString = string.Empty;
											char ccq = tr.PeekChar();
											if (ccq != '"' && ccq != '\'')
											{
												char cc = '\0';
												string nnn = string.Empty;
												while (cc != this.Settings.TagEndChar)
												{
													cc = tr.PeekChar();
													if (cc == this.Settings.TagEndChar)
													{
														break;
													}
													nnn += tr.ReadChar();
													if (tr.EndOfStream)
													{
														break;
													}
												}
												string value = nnn;
												string arg_6A8_0 = nnn;
												char c2 = this.Settings.AttributeNameValueSeparatorChar;
												if (arg_6A8_0.Contains(c2.ToString()))
												{
													int l = nnn.IndexOf(' ');
													value = nnn.Substring(0, l);
													string nnnn = nnn.Substring(value.Length).Trim();
													string nnnnAttributeName = string.Empty;
													string nnnnAttributeValue = string.Empty;
													bool nnnnLookForValue = false;
													int nnnnIsInsideString = 0;
													for (int i = 0; i < nnnn.Length; i++)
													{
														char r = nnnn[i];
														if (r == this.Settings.AttributeNameValueSeparatorChar)
														{
															if (nnnnIsInsideString == 0)
															{
																nnnnLookForValue = true;
															}
														}
														else
														{
															if (r == '\'' || r == '"')
															{
																if ((r == '\'' && nnnnIsInsideString == 2) || (r == '"' && nnnnIsInsideString == 1))
																{
																	nnnnIsInsideString = 0;
																}
																else
																{
																	if (r == '"')
																	{
																		nnnnIsInsideString = 1;
																	}
																	else
																	{
																		if (r == '\'')
																		{
																			nnnnIsInsideString = 2;
																		}
																	}
																}
															}
															else
															{
																if (r == ' ' && nnnnIsInsideString == 0)
																{
																	if (currentElement != null)
																	{
																		if (currentElement is MarkupTagElement)
																		{
																			(currentElement as MarkupTagElement).Attributes.Add(nnnnAttributeName, this.ReplaceEntitiesInput(nnnnAttributeValue));
																			nnnnAttributeName = string.Empty;
																			nnnnAttributeValue = string.Empty;
																			nnnnLookForValue = false;
																		}
																	}
																}
																else
																{
																	if (nnnnLookForValue)
																	{
																		nnnnAttributeValue += r;
																	}
																	else
																	{
																		nnnnAttributeName += r;
																	}
																}
															}
														}
													}
													if (!string.IsNullOrEmpty(nnnnAttributeName))
													{
														(currentElement as MarkupTagElement).Attributes.Add(nnnnAttributeName, this.ReplaceEntitiesInput(nnnnAttributeValue));
														nnnnAttributeName = string.Empty;
														nnnnAttributeValue = string.Empty;
													}
												}
												if (currentElement != null)
												{
													if (currentElement is MarkupTagElement)
													{
														(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(value));
														nextAttributeName = null;
													}
												}
												currentString = string.Empty;
												continue;
											}
										}
									}
									else
									{
										currentString += c;
									}
								}
								else
								{
									if (c == '"' || c == '\'')
									{
										if (!insidePreprocessor)
										{
											if (!insideTagDecl)
											{
												currentString += c;
												continue;
											}
											int cur = 0;
											if (c == '"')
											{
												cur = 1;
											}
											if (c == '\'')
											{
												cur = 2;
											}
											if (insideString == 0)
											{
												insideString = cur;
											}
											else
											{
												if (insideString == cur)
												{
													insideString = 0;
												}
												else
												{
													currentString += c;
												}
											}
										}
										else
										{
											currentString += c;
										}
									}
									else
									{
										if (char.IsWhiteSpace(c))
										{
											if (insideString == 0 && !insidePreprocessor)
											{
												if (char.IsWhiteSpace(prevChar))
												{
													continue;
												}
												if (insideTagDecl)
												{
													if (insideSpecialDeclaration)
													{
														if (currentString.Trim() == "DOCTYPE")
														{
															char c2 = this.mvarSettings.TagEndChar;
															string doctype = tr.ReadUntil(c2.ToString(), "\"", "\"");
															doctype = doctype.Substring(0, doctype.Length - 1);
															string[] doctypeParts = doctype.Split(new string[]
															{
																" "
															}, "\"", "\"");
															insideTagDecl = false;
															insideSpecialDeclaration = false;
														}
													}
													else
													{
														if (!insideAttributeArea)
														{
															MarkupElement prevElement = currentElement;
															currentElement = new MarkupTagElement();
															currentElement.Name = currentString.Trim();
															if (prevElement == null)
															{
																mom.Elements.Add(currentElement);
															}
															else
															{
																if (!(prevElement is MarkupContainerElement))
																{
																	throw new InvalidOperationException("Cannot add element to non-container object");
																}
																(prevElement as MarkupContainerElement).Elements.Add(currentElement);
															}
															insideAttributeArea = true;
														}
														else
														{
															if (nextAttributeName != null)
															{
																if (currentElement != null && currentElement is MarkupTagElement)
																{
																	(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(currentString));
																	nextAttributeName = null;
																}
															}
														}
													}
													currentString = string.Empty;
												}
												else
												{
													if (currentString.Length == 0 || !char.IsWhiteSpace(currentString[currentString.Length - 1]))
													{
														currentString += ' ';
													}
												}
											}
											else
											{
												if (insidePreprocessor || currentString.Length == 0 || !char.IsWhiteSpace(currentString[currentString.Length - 1]))
												{
													currentString += c;
												}
											}
										}
										else
										{
											if (c == this.Settings.PreprocessorChar && insideString == 0 && insideTagDecl)
											{
												if (prevChar == this.Settings.TagBeginChar)
												{
													insidePreprocessor = true;
												}
											}
											else if (insideSpecialDeclaration && c == this.Settings.CDataBeginChar)
											{
												string specialSectionName = tr.ReadUntil(this.Settings.CDataBeginChar.ToString());
												specialSectionName = specialSectionName.Substring(0, specialSectionName.Length);
												tr.Accessor.Seek(1, IO.SeekOrigin.Current);

												string specialSectionContent = tr.ReadUntil(this.Settings.CDataEndChar.ToString());
												if (specialSectionContent.Length > 0)
												{
													specialSectionContent = specialSectionContent.Substring(0, specialSectionContent.Length); 
												}

												MarkupStringElement tag = new MarkupStringElement();
												tag.FullName = specialSectionName;
												tag.Value = specialSectionContent;

												if (currentElement != null && (currentElement is MarkupContainerElement))
												{
													(currentElement as MarkupContainerElement).Elements.Add(tag);
												}
												else
												{
													mom.Elements.Add(tag);
												}

												char padNext = tr.ReadChar();
												padNext = tr.ReadChar();

												// insideSpecialDeclaration = false;
												continue;
											}
											else
											{
												currentString += c;
												if (insideSpecialDeclaration)
												{
													if (currentString.Trim().Equals(this.Settings.TagSpecialDeclarationCommentStart))
													{
														currentString = string.Empty;
														MarkupCommentElement comment = new MarkupCommentElement();
														string commentContent = tr.ReadUntil(this.Settings.TagSpecialDeclarationCommentStart + this.Settings.TagEndChar, "", "");
														comment.Value = commentContent;
														if (currentElement == null)
														{
															mom.Elements.Add(comment);
														}
														else
														{
															if (!(currentElement is MarkupContainerElement))
															{
																throw new InvalidOperationException("Cannot add element to non-container object");
															}
															(currentElement as MarkupContainerElement).Elements.Add(comment);
														}
														insideSpecialDeclaration = false;
														insideTagDecl = false;
													}
												}
												else
												{
													if (insideString != 0 || !char.IsWhiteSpace(c))
													{
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				prevChar = c;
			}

			if (objectModel is MarkupObjectModel)
			{
				mom.CopyTo(objectModel);
			}
		}

		private string ReplaceEntitiesInput(string value)
		{
			string result;
			if (string.IsNullOrEmpty(value))
			{
				result = value;
			}
			else
			{
				string old = value;
				foreach (KeyValuePair<string, string> kvp in Settings.Entities)
				{
					string arg_6C_0 = old;
					char c = this.mvarSettings.EntityBeginChar;
					string arg_60_0 = c.ToString();
					string arg_60_1 = kvp.Key;
					c = this.mvarSettings.EntityEndChar;
					old = arg_6C_0.Replace(arg_60_0 + arg_60_1 + c.ToString(), kvp.Value);
				}
				result = old;
			}
			return result;
		}
		private string ReplaceEntitiesOutput(string value)
		{
			string result;
			if (string.IsNullOrEmpty(value))
			{
				result = value;
			}
			else
			{
				string old = value;
				foreach (KeyValuePair<string, string> kvp in Settings.Entities)
				{
					string arg_6C_0 = old;
					string arg_6C_1 = kvp.Value;
					char c = this.mvarSettings.EntityBeginChar;
					string arg_67_0 = c.ToString();
					string arg_67_1 = kvp.Key;
					c = this.mvarSettings.EntityEndChar;
					old = arg_6C_0.Replace(arg_6C_1, arg_67_0 + arg_67_1 + c.ToString());
				}
				result = old;
			}
			return result;
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer tw = base.Accessor.Writer;
			if (objectModel is MarkupObjectModel)
			{
				MarkupObjectModel mom = objectModel as MarkupObjectModel;

				if (mom.Elements.Count > 1 && (mvarCompensateTopLevelTagName != null))
				{
					WriteStartTag(mvarCompensateTopLevelTagName);
				}

				foreach (MarkupElement element in mom.Elements)
				{
					WriteElement(element, 0);
				}

				if (mom.Elements.Count > 1 && (mvarCompensateTopLevelTagName != null))
				{
					WriteEndTag(mvarCompensateTopLevelTagName);
				}
			}
			else
			{
				if (objectModel is PropertyListObjectModel)
				{
					PropertyListObjectModel plom = objectModel as PropertyListObjectModel;
					tw.Write(this.Settings.TagBeginChar.ToString());
					tw.Write(this.Settings.PreprocessorChar.ToString());
					tw.Write("xml version=\"1.0\" encoding=\"UTF-8\" ");
					tw.Write(this.Settings.PreprocessorChar.ToString());
					tw.Write(this.Settings.TagEndChar.ToString());
					if (mvarSettings.PrettyPrint) tw.WriteLine();

					tw.Write(this.Settings.TagBeginChar.ToString());
					tw.Write("document");
					tw.Write(this.Settings.TagEndChar.ToString());
					if (mvarSettings.PrettyPrint) tw.WriteLine();

					foreach (Group g in plom.Groups)
					{
						this.WriteXMLPropertyGroup(tw, g, 1);
					}
					foreach (Property p in plom.Properties)
					{
						this.WriteXMLProperty(tw, p, 1);
					}

					tw.Write(this.Settings.TagBeginChar.ToString());
					tw.Write(this.Settings.TagCloseChar.ToString());
					tw.Write("document");
					tw.Write(this.Settings.TagEndChar.ToString());
					if (mvarSettings.PrettyPrint) tw.WriteLine();
				}
			}
			tw.Flush();
		}
		private void WriteXMLPropertyGroup(IO.Writer tw, Group g, int indentLevel)
		{
			string indent = new string('\t', indentLevel);
			char c = this.Settings.TagBeginChar;
			string arg_37_0 = c.ToString();
			string arg_37_1 = g.Name;
			c = this.Settings.TagEndChar;
			tw.Write(arg_37_0 + arg_37_1 + c.ToString());
			if (mvarSettings.PrettyPrint) tw.WriteLine();

			foreach (Group g2 in g.Groups)
			{
				this.WriteXMLPropertyGroup(tw, g2, indentLevel + 1);
			}
			foreach (Property p in g.Properties)
			{
				this.WriteXMLProperty(tw, p, indentLevel + 1);
			}
			c = this.Settings.TagBeginChar;
			string arg_116_0 = c.ToString();
			c = this.Settings.TagCloseChar;
			string arg_116_1 = c.ToString();
			string arg_116_2 = g.Name;
			c = this.Settings.TagEndChar;
			tw.Write(arg_116_0 + arg_116_1 + arg_116_2 + c.ToString());
			if (mvarSettings.PrettyPrint) tw.WriteLine();
		}
		private void WriteXMLProperty(IO.Writer tw, Property p, int indentLevel)
		{
			string indent = new string('\t', indentLevel);
			string[] array = new string[8];
			string[] arg_27_0 = array;
			int arg_27_1 = 0;
			char c = this.Settings.TagBeginChar;
			arg_27_0[arg_27_1] = c.ToString();
			array[1] = p.Name;
			string[] arg_46_0 = array;
			int arg_46_1 = 2;
			c = this.Settings.TagEndChar;
			arg_46_0[arg_46_1] = c.ToString();
			array[3] = this.ReplaceEntitiesOutput(p.Value.ToString());
			string[] arg_70_0 = array;
			int arg_70_1 = 4;
			c = this.Settings.TagBeginChar;
			arg_70_0[arg_70_1] = c.ToString();
			string[] arg_86_0 = array;
			int arg_86_1 = 5;
			c = this.Settings.TagCloseChar;
			arg_86_0[arg_86_1] = c.ToString();
			array[6] = p.Name;
			string[] arg_A5_0 = array;
			int arg_A5_1 = 7;
			c = this.Settings.TagEndChar;
			arg_A5_0[arg_A5_1] = c.ToString();
			tw.Write(string.Concat(array));
			if (mvarSettings.PrettyPrint) tw.WriteLine();
		}

		public MarkupElement ReadElement()
		{
			// Read the next element from the XML stream
			IO.Reader tr = base.Accessor.Reader;

			#region This code is blatantly copied from LoadInternal() - is there a better way to do this?
			bool insideTagDecl = false;
			int insideString = 0;
			bool closingCurrentElement = false;
			string currentString = string.Empty;
			MarkupElement currentElement = null;
			bool insidePreprocessor = false;
			bool insideAttributeArea = false;
			bool insideSpecialDeclaration = false;
			bool insideTagValue = false;
			char prevChar = '\0';
			string nextAttributeName = null;
			bool loaded = false;
			while (!tr.EndOfStream)
			{
				char c = (char)tr.ReadChar();
				if (!loaded && (c != '<'))
				{
					return null;
				}
				else
				{
					loaded = true;
				}
				if (c == this.Settings.TagBeginChar)
				{
					if (insideString == 0 && !insidePreprocessor)
					{
						insideTagDecl = true;
						insideTagValue = false;
						if (!string.IsNullOrEmpty(currentString) && currentElement != null)
						{
							currentElement.Value += currentString;
							currentElement.Value = this.ReplaceEntitiesInput(currentElement.Value.Trim());
							currentString = string.Empty;
						}
					}
					else
					{
						currentString += c;
					}
				}
				else
				{
					if (c == this.Settings.TagSpecialDeclarationStartChar)
					{
						if (insideString == 0 && !insidePreprocessor && !insideTagValue && !insideAttributeArea)
						{
							insideSpecialDeclaration = true;
							continue;
						}
						if (insideTagValue || insideAttributeArea)
						{
							currentString += c;
						}
					}
					else
					{
						if (c == this.Settings.TagEndChar)
						{
							if (insideString == 0 && ((insidePreprocessor && prevChar == this.Settings.PreprocessorChar) || prevChar != this.Settings.PreprocessorChar))
							{
								insideTagDecl = false;
								if (currentString.EndsWith(this.Settings.TagSpecialDeclarationCommentStart))
								{
									currentString = currentString.Substring(0, currentString.Length - this.Settings.TagSpecialDeclarationCommentStart.Length);
									currentElement.Value = this.ReplaceEntitiesInput(currentString);
									currentString = string.Empty;
									currentElement = currentElement.Parent;
									continue;
								}
								if (insidePreprocessor && prevChar == this.Settings.PreprocessorChar)
								{
									MarkupElement prevElement = currentElement;
									currentElement = new MarkupPreprocessorElement();
									string name = string.Empty;
									string content = currentString;
									if (currentString.Contains(" "))
									{
										name = currentString.Substring(0, currentString.IndexOf(' '));
										content = currentString.Substring(name.Length + 1);
									}
									currentElement.Name = name.Trim();
									currentElement.Value = content.Trim();
									if (prevElement != null && prevElement is MarkupContainerElement)
									{
										(prevElement as MarkupContainerElement).Elements.Add(currentElement);
									}
									else
									{
										return currentElement;
									}
									currentElement = prevElement;
									insidePreprocessor = false;
								}
								else
								{
									if (closingCurrentElement)
									{
										if (currentElement != null)
										{
											if (currentElement.FullName == currentString || prevChar == this.Settings.TagCloseChar)
											{
												currentElement = currentElement.Parent;
											}
											else
											{
												if (Settings.AutoCloseTagNames.Contains(currentElement.Name))
												{
													currentElement = currentElement.Parent;
													if (currentElement != null)
													{
														currentElement = currentElement.Parent;
													}
												}
												else
												{
													Console.WriteLine("uds: XMLDataFormat: attempted to close element '" + currentElement.Name + "'; invalid XML?");
												}
											}
										}
										else
										{
											Console.WriteLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:144     currentElement is null");
										}
										closingCurrentElement = false;
									}
									else
									{
										insideTagValue = true;
										if (insideAttributeArea)
										{
											(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(currentString));
											nextAttributeName = null;
											insideAttributeArea = false;
										}
										else
										{
											MarkupElement prevElement = currentElement;
											currentElement = new MarkupTagElement();
											currentElement.FullName = currentString.Trim();
											if (prevElement != null && prevElement is MarkupContainerElement)
											{
												(prevElement as MarkupContainerElement).Elements.Add(currentElement);
											}
											else
											{
												return currentElement;
											}
											if (Settings.AutoCloseTagNames.Contains(currentElement.Name))
											{
												currentElement = currentElement.Parent;
											}
										}
									}
								}
								currentString = string.Empty;
								insideAttributeArea = false;
							}
							else
							{
								currentString += c;
							}
						}
						else
						{
							if (c == this.Settings.TagCloseChar)
							{
								if (insideString == 0 && !insidePreprocessor && insideTagDecl)
								{
									if (currentElement == null)
									{
										goto IL_4CC;
									}
								IL_4CC:
									if (insideAttributeArea && currentElement is MarkupTagElement && nextAttributeName != null)
									{
										if (nextAttributeName != null)
										{
											(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(currentString));
											nextAttributeName = null;
											currentString = string.Empty;
										}
									}
									else
									{
										if (!insideAttributeArea)
										{
											if (currentElement != null)
											{
												if (currentElement is MarkupTagElement && !string.IsNullOrEmpty(currentString))
												{
													MarkupTagElement e = new MarkupTagElement();
													e.Name = currentString;
													(currentElement as MarkupTagElement).Elements.Add(e);
													currentElement = e;
												}
											}
											else
											{
												Console.WriteLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:199     currentElement is null");
											}
										}
									}
									currentString = string.Empty;
									closingCurrentElement = true;
								}
								else
								{
									currentString += c;
								}
							}
							else
							{
								if (c == this.Settings.AttributeNameValueSeparatorChar)
								{
									if (insideString == 0 && !insidePreprocessor && !insideTagValue)
									{
										if (insideAttributeArea)
										{
											nextAttributeName = currentString;
											currentString = string.Empty;
											char ccq = tr.PeekChar();
											if (ccq != '"' && ccq != '\'')
											{
												char cc = '\0';
												string nnn = string.Empty;
												while (cc != this.Settings.TagEndChar)
												{
													cc = tr.PeekChar();
													if (cc == this.Settings.TagEndChar)
													{
														break;
													}
													nnn += tr.ReadChar();
													if (tr.EndOfStream)
													{
														break;
													}
												}
												string value = nnn;
												string arg_6A8_0 = nnn;
												char c2 = this.Settings.AttributeNameValueSeparatorChar;
												if (arg_6A8_0.Contains(c2.ToString()))
												{
													int l = nnn.IndexOf(' ');
													value = nnn.Substring(0, l);
													string nnnn = nnn.Substring(value.Length).Trim();
													string nnnnAttributeName = string.Empty;
													string nnnnAttributeValue = string.Empty;
													bool nnnnLookForValue = false;
													int nnnnIsInsideString = 0;
													for (int i = 0; i < nnnn.Length; i++)
													{
														char r = nnnn[i];
														if (r == this.Settings.AttributeNameValueSeparatorChar)
														{
															if (nnnnIsInsideString == 0)
															{
																nnnnLookForValue = true;
															}
														}
														else
														{
															if (r == '\'' || r == '"')
															{
																if ((r == '\'' && nnnnIsInsideString == 2) || (r == '"' && nnnnIsInsideString == 1))
																{
																	nnnnIsInsideString = 0;
																}
																else
																{
																	if (r == '"')
																	{
																		nnnnIsInsideString = 1;
																	}
																	else
																	{
																		if (r == '\'')
																		{
																			nnnnIsInsideString = 2;
																		}
																	}
																}
															}
															else
															{
																if (r == ' ' && nnnnIsInsideString == 0)
																{
																	if (currentElement != null)
																	{
																		if (currentElement is MarkupTagElement)
																		{
																			(currentElement as MarkupTagElement).Attributes.Add(nnnnAttributeName, this.ReplaceEntitiesInput(nnnnAttributeValue));
																			nnnnAttributeName = string.Empty;
																			nnnnAttributeValue = string.Empty;
																			nnnnLookForValue = false;
																		}
																	}
																}
																else
																{
																	if (nnnnLookForValue)
																	{
																		nnnnAttributeValue += r;
																	}
																	else
																	{
																		nnnnAttributeName += r;
																	}
																}
															}
														}
													}
													if (!string.IsNullOrEmpty(nnnnAttributeName))
													{
														(currentElement as MarkupTagElement).Attributes.Add(nnnnAttributeName, this.ReplaceEntitiesInput(nnnnAttributeValue));
														nnnnAttributeName = string.Empty;
														nnnnAttributeValue = string.Empty;
													}
												}
												if (currentElement != null)
												{
													if (currentElement is MarkupTagElement)
													{
														(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(value));
														nextAttributeName = null;
													}
												}
												currentString = string.Empty;
												continue;
											}
										}
									}
									else
									{
										currentString += c;
									}
								}
								else
								{
									if (c == '"' || c == '\'')
									{
										if (!insidePreprocessor)
										{
											if (!insideTagDecl)
											{
												currentString += c;
												continue;
											}
											int cur = 0;
											if (c == '"')
											{
												cur = 1;
											}
											if (c == '\'')
											{
												cur = 2;
											}
											if (insideString == 0)
											{
												insideString = cur;
											}
											else
											{
												if (insideString == cur)
												{
													insideString = 0;
												}
												else
												{
													currentString += c;
												}
											}
										}
										else
										{
											currentString += c;
										}
									}
									else
									{
										if (char.IsWhiteSpace(c))
										{
											if (insideString == 0 && !insidePreprocessor)
											{
												if (char.IsWhiteSpace(prevChar))
												{
													continue;
												}
												if (insideTagDecl)
												{
													if (insideSpecialDeclaration)
													{
														if (currentString.Trim() == "DOCTYPE")
														{
															char c2 = this.mvarSettings.TagEndChar;
															string doctype = tr.ReadUntil(c2.ToString(), "\"", "\"");
															doctype = doctype.Substring(0, doctype.Length - 1);
															string[] doctypeParts = doctype.Split(new string[]
															{
																" "
															}, "\"", "\"");
															insideTagDecl = false;
															insideSpecialDeclaration = false;
														}
													}
													else
													{
														if (!insideAttributeArea)
														{
															MarkupElement prevElement = currentElement;
															currentElement = new MarkupTagElement();
															currentElement.Name = currentString.Trim();
															if (prevElement == null)
															{
																return currentElement;
															}
															else
															{
																if (!(prevElement is MarkupContainerElement))
																{
																	throw new InvalidOperationException("Cannot add element to non-container object");
																}
																(prevElement as MarkupContainerElement).Elements.Add(currentElement);
															}
															insideAttributeArea = true;
														}
														else
														{
															if (nextAttributeName != null)
															{
																if (currentElement != null && currentElement is MarkupTagElement)
																{
																	(currentElement as MarkupTagElement).Attributes.Add(nextAttributeName, this.ReplaceEntitiesInput(currentString));
																	nextAttributeName = null;
																}
															}
														}
													}
													currentString = string.Empty;
												}
												else
												{
													if (currentString.Length == 0 || !char.IsWhiteSpace(currentString[currentString.Length - 1]))
													{
														currentString += ' ';
													}
												}
											}
											else
											{
												if (insidePreprocessor || currentString.Length == 0 || !char.IsWhiteSpace(currentString[currentString.Length - 1]))
												{
													currentString += c;
												}
											}
										}
										else
										{
											if (c == this.Settings.PreprocessorChar && insideString == 0 && insideTagDecl)
											{
												if (prevChar == this.Settings.TagBeginChar)
												{
													insidePreprocessor = true;
												}
											}
											else
											{
												currentString += c;
												if (insideSpecialDeclaration)
												{
													if (currentString.Trim().Equals(this.Settings.TagSpecialDeclarationCommentStart))
													{
														currentString = string.Empty;
														MarkupCommentElement comment = new MarkupCommentElement();
														string commentContent = tr.ReadUntil(this.Settings.TagSpecialDeclarationCommentStart + this.Settings.TagEndChar, "", "");
														comment.Value = commentContent;
														if (currentElement == null)
														{
															return comment;
														}
														else
														{
															if (!(currentElement is MarkupContainerElement))
															{
																throw new InvalidOperationException("Cannot add element to non-container object");
															}
															(currentElement as MarkupContainerElement).Elements.Add(comment);
														}
														insideSpecialDeclaration = false;
														insideTagDecl = false;
													}
												}
												else
												{
													if (insideString != 0 || !char.IsWhiteSpace(c))
													{
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				prevChar = c;
			}
			#endregion

			return null;
		}

		public static string GetXSDTypeFromNativeType(Type type)
		{
			if (type == typeof(String))
			{
				return "xsd:string";
			}
			else if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64) || type == typeof(UInt16) || type == typeof(UInt32) || type == typeof(UInt64))
			{
				return "xsd:integer";
			}
			return String.Empty;
		}
	}
}
