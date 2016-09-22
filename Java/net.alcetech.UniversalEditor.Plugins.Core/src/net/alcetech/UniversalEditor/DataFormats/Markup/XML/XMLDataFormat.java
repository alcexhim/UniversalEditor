package net.alcetech.UniversalEditor.DataFormats.Markup.XML;

import java.io.IOException;

import net.alcetech.ApplicationFramework.Collections.Generic.KeyValuePair;
import net.alcetech.ApplicationFramework.Exceptions.*;
import net.alcetech.UniversalEditor.Core.*;
import net.alcetech.UniversalEditor.Core.IO.Reader;
import net.alcetech.UniversalEditor.Core.IO.SeekOrigin;
import net.alcetech.UniversalEditor.ObjectModels.Markup.*;

public class XMLDataFormat extends DataFormat
{
	private static DataFormatReference _dfr = null;
	public DataFormatReference getDataFormatReference() {
		if (_dfr == null)
		{
			_dfr = super.getDataFormatReference();
			_dfr.getSupportedObjectModelCollection().add(ObjectModelReference.fromClass(MarkupObjectModel.class));
		}
		return _dfr;
	}
	
	private XMLDataFormatSettings _settings = new XMLDataFormatSettings();
	public XMLDataFormatSettings getSettings() { return _settings; }
	
	public XMLDataFormat() {
		// this.getSettings().addAutoCloseTagName("hr");
		// this.getSettings().addAutoCloseTagName("br");
		// this.getSettings().addAutoCloseTagName("img");
		this.getSettings().addEntity("amp", "&");
		this.getSettings().addEntity("quot", "\"");
		this.getSettings().addEntity("copy", "©");
		this.getSettings().addEntity("apos", "'");
	}
	
	@Override
	protected void loadInternal(ObjectModel objectModel) throws IOException {
		Reader tr = super.getAccessor().getReader();
		
		MarkupObjectModel mom = new MarkupObjectModel();
		boolean insideTagDecl = false;
		int insideString = 0;
		boolean closingCurrentElement = false;
		String currentString = "";
		MarkupElement currentElement = null;
		boolean insidePreprocessor = false;
		boolean insideAttributeArea = false;
		boolean insideSpecialDeclaration = false;
		boolean insideTagValue = false;
		char prevChar = '\0';
		String nextAttributeName = null;
		boolean loaded = false;

		char c = tr.readChar();
		int times = 0, maxtimes = 5;
		while (c != '<')
		{
			// clear out junk
			c = tr.readChar();
			times++;
			if (times == maxtimes) break;
		}
		tr.getAccessor().seek(-1, SeekOrigin.CURRENT);

		boolean seenBeginChar = false;
		
		while (!tr.getEndOfStream())
		{
			c = tr.readChar();
			if (!loaded && (c != '<'))
			{
				return;
			}
			else
			{
				loaded = true;
			}

			if (c == this.getSettings().getTagEndChar() && !seenBeginChar)
			{
				currentString += c;
				continue;
			}
			else if (c == this.getSettings().getTagEndChar() && seenBeginChar)
			{
				seenBeginChar = false;
			}

			if (c == this.getSettings().getTagBeginChar())
			{
				if (insideString == 0 && !insidePreprocessor)
				{
					insideTagDecl = true;
					insideTagValue = false;
					if (!("".equals(currentString)) && currentElement != null)
					{
						currentElement.setValue(currentElement.getValue() + currentString);
						currentElement.setValue(this.replaceEntitiesInput(currentElement.getValue().trim()));
						currentString = "";
					}
				}
				else
				{
					currentString += c;
				}
				seenBeginChar = true;
			}
			else
			{
				if (c == this.getSettings().getTagSpecialDeclarationStartChar())
				{
					if (insideString == 0 && !insidePreprocessor && !insideTagValue && !insideAttributeArea && prevChar == this.getSettings().getTagBeginChar())
					{
						insideSpecialDeclaration = true;
						continue;
					}
					currentString += c;
				}
				else
				{
					if (c == this.getSettings().getTagEndChar())
					{
						if (insideString == 0 && ((insidePreprocessor && prevChar == this.getSettings().getPreprocessorChar()) || prevChar != this.getSettings().getPreprocessorChar()))
						{
							insideTagDecl = false;
							if (currentString.endsWith(this.getSettings().getTagSpecialDeclarationCommentStart()))
							{
								currentString = currentString.substring(0, currentString.length() - this.getSettings().getTagSpecialDeclarationCommentStart().length());
								currentElement.setValue(this.replaceEntitiesInput(currentString));
								currentString = "";
								// currentElement = currentElement.getParentElement();
								continue;
							}
							if (insidePreprocessor && prevChar == this.getSettings().getPreprocessorChar())
							{
								MarkupElement prevElement = currentElement;
								String name = "";
								String content = currentString;
								if (currentString.contains(" "))
								{
									name = currentString.substring(0, currentString.indexOf(' '));
									content = currentString.substring(name.length() + 1);
								}
								currentElement = new MarkupPreprocessorElement(name, content);
								if (prevElement != null && prevElement.getClass().isAssignableFrom(MarkupTagElement.class))
								{
									((MarkupTagElement)prevElement).addElement(currentElement);
								}
								else
								{
									mom.addElement(currentElement);
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
										if (currentElement.getFullName().equals(currentString) || prevChar == this.getSettings().getTagCloseChar())
										{
											currentElement = currentElement.getParentElement();
										}
										else
										{
											if (this.getSettings().containsAutoCloseTagName(currentElement.getName()))
											{
												currentElement = currentElement.getParentElement();
												if (currentElement != null)
												{
													currentElement = currentElement.getParentElement();
												}
											}
											else
											{
												System.err.println("uds: XMLDataFormat: in " + this.getAccessor().getFileName());
												System.err.println("uds: XMLDataFormat: attempted to close element '" + currentElement.getName() + "' with '" + currentString + "'; invalid XML?");
											}
										}
									}
									else
									{
										// Console.WriteLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:144     currentElement is null");
										System.err.println("FIXME: UniversalEditor/DataFormats/Markup/XML/XMLDataFormat.java:184     currentElement is null");
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
											((MarkupTagElement)currentElement).addAttribute(nextAttributeName, this.replaceEntitiesInput(currentString));
											nextAttributeName = null;
											insideAttributeArea = false;
										}
										else
										{
											MarkupElement prevElement = currentElement;
											currentElement = new MarkupTagElement(currentString.trim());
											if (prevElement != null && prevElement.getClass().isAssignableFrom(MarkupTagElement.class))
											{
												((MarkupTagElement)prevElement).addElement(currentElement);
											}
											else
											{
												mom.addElement(currentElement);
											}
											if (this.getSettings().containsAutoCloseTagName(currentElement.getName()))
											{
												currentElement = currentElement.getParentElement();
											}
										}
									}
								}
							}
							currentString = "";
							insideAttributeArea = false;
						}
						else
						{
							currentString += c;
						}
					}
					else
					{
						if (c == this.getSettings().getTagCloseChar())
						{
							if (insideString == 0 && !insidePreprocessor && insideTagDecl)
							{
								if (insideAttributeArea && currentElement.getClass().isAssignableFrom(MarkupTagElement.class) && nextAttributeName != null)
								{
									if (nextAttributeName != null)
									{
										((MarkupTagElement)currentElement).addAttribute(nextAttributeName, this.replaceEntitiesInput(currentString));
										nextAttributeName = null;
										currentString = "";
									}
								}
								else
								{
									if (!insideAttributeArea)
									{
										if (currentElement != null)
										{
											if (currentElement.getClass().isAssignableFrom(MarkupTagElement.class) && !"".equals(currentString))
											{
												MarkupTagElement e = new MarkupTagElement(currentString);
												((MarkupTagElement)currentElement).addElement(e);
												currentElement = e;
											}
										}
										else
										{
											// Console.WriteLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:199     currentElement is null");
											System.out.println("FIXME: UniversalEditor/DataFormats/Markup/XML/XMLDataFormat.java:265     currentElement is null");
										}
									}
								}
								currentString = "";
								closingCurrentElement = true;
							}
							else
							{
								currentString += c;
							}
						}
						else
						{
							if (c == this.getSettings().getAttributeNameValueSeparatorChar())
							{
								if (insideString == 0 && !insidePreprocessor && !insideTagValue)
								{
									if (insideAttributeArea)
									{
										nextAttributeName = currentString;
										currentString = "";
										char ccq = tr.peekChar();
										if (ccq != '"' && ccq != '\'')
										{
											char cc = '\0';
											String nnn = "";
											while (cc != this.getSettings().getTagEndChar())
											{
												cc = tr.peekChar();
												if (cc == this.getSettings().getTagEndChar())
												{
													break;
												}
												nnn += tr.readChar();
												if (tr.getEndOfStream())
												{
													break;
												}
											}
											String value = nnn;
											if (nnn.contains(String.valueOf(this.getSettings().getAttributeNameValueSeparatorChar())))
											{
												int l = nnn.indexOf(' ');
												value = nnn.substring(0, l);
												String nnnn = nnn.substring(value.length()).trim();
												String nnnnAttributeName = "";
												String nnnnAttributeValue = "";
												boolean nnnnLookForValue = false;
												int nnnnIsInsideString = 0;
												for (int i = 0; i < nnnn.length(); i++)
												{
													char r = nnnn.charAt(i);
													if (r == this.getSettings().getAttributeNameValueSeparatorChar())
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
																	if (currentElement.getClass().isAssignableFrom(MarkupTagElement.class))
																	{
																		((MarkupTagElement)currentElement).addAttribute(nnnnAttributeName, this.replaceEntitiesInput(nnnnAttributeValue));
																		nnnnAttributeName = "";
																		nnnnAttributeValue = "";
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
												if (!("".equals(nnnnAttributeName)))
												{
													((MarkupTagElement)currentElement).addAttribute(nnnnAttributeName, this.replaceEntitiesInput(nnnnAttributeValue));
													nnnnAttributeName = "";
													nnnnAttributeValue = "";
												}
											}
											if (currentElement != null)
											{
												if (currentElement.getClass().isAssignableFrom(MarkupTagElement.class))
												{
													((MarkupTagElement)currentElement).addAttribute(nextAttributeName, this.replaceEntitiesInput(value));
													nextAttributeName = null;
												}
											}
											currentString = "";
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
									if (Character.isWhitespace(c))
									{
										if (insideString == 0 && !insidePreprocessor)
										{
											if (Character.isWhitespace(prevChar))
											{
												continue;
											}
											if (insideTagDecl)
											{
												if (insideSpecialDeclaration)
												{
													if (currentString.trim() == "DOCTYPE")
													{
														String doctype = tr.readStringUntil(this.getSettings().getTagEndChar(), "\"", "\"");
														doctype = doctype.substring(0, doctype.length() - 1);
														/*
														String[] doctypeParts = doctype.split(new String[]
														{
															" "
														}, "\"", "\"");
														*/
														insideTagDecl = false;
														insideSpecialDeclaration = false;
													}
												}
												else
												{
													if (!insideAttributeArea)
													{
														MarkupElement prevElement = currentElement;
														currentElement = new MarkupTagElement(currentString.trim());
														if (prevElement == null)
														{
															mom.addElement(currentElement);
														}
														else
														{
															if (prevElement.getClass().isAssignableFrom(MarkupTagElement.class))
															{
																((MarkupTagElement)prevElement).addElement(currentElement);
															}
														}
														insideAttributeArea = true;
													}
													else
													{
														if (nextAttributeName != null)
														{
															if (currentElement != null && (currentElement.getClass().isAssignableFrom(MarkupTagElement.class)))
															{
																((MarkupTagElement)currentElement).addAttribute(nextAttributeName, this.replaceEntitiesInput(currentString));
																nextAttributeName = null;
															}
														}
													}
												}
												currentString = "";
											}
											else
											{
												if (currentString.length() == 0 || !Character.isWhitespace(currentString.charAt(currentString.length() - 1)))
												{
													currentString += ' ';
												}
											}
										}
										else
										{
											if (insidePreprocessor || currentString.length() == 0 || !Character.isWhitespace(currentString.charAt(currentString.length() - 1)))
											{
												currentString += c;
											}
										}
									}
									else
									{
										if (c == this.getSettings().getPreprocessorChar() && insideString == 0 && insideTagDecl)
										{
											if (prevChar == this.getSettings().getTagBeginChar())
											{
												insidePreprocessor = true;
											}
										}
										else if (insideSpecialDeclaration && c == this.getSettings().getCDataBeginChar())
										{
											String specialSectionName = tr.readStringUntil(this.getSettings().getCDataBeginChar());
											specialSectionName = specialSectionName.substring(0, specialSectionName.length());
											tr.getAccessor().seek(1, SeekOrigin.CURRENT);

											String specialSectionContent = tr.readStringUntil(this.getSettings().getCDataEndChar());
											if (specialSectionContent.length() > 0)
											{
												specialSectionContent = specialSectionContent.substring(0, specialSectionContent.length()); 
											}

											MarkupStringElement tag = new MarkupStringElement(specialSectionName, specialSectionContent);
											if (currentElement != null && (currentElement.getClass().isAssignableFrom(MarkupTagElement.class)))
											{
												((MarkupTagElement)currentElement).addElement(tag);
											}
											else
											{
												mom.addElement(tag);
											}

											char padNext = tr.readChar();
											padNext = tr.readChar();

											// insideSpecialDeclaration = false;
											continue;
										}
										else
										{
											currentString += c;
											if (insideSpecialDeclaration)
											{
												if (currentString.trim().equals(this.getSettings().getTagSpecialDeclarationCommentStart()))
												{
													currentString = "";
													String commentContent = tr.readStringUntil(this.getSettings().getTagSpecialDeclarationCommentStart() + this.getSettings().getTagEndChar());
													MarkupCommentElement comment = new MarkupCommentElement(commentContent);
													if (currentElement == null)
													{
														mom.addElement(comment);
													}
													else
													{
														if (currentElement.getClass().isAssignableFrom(MarkupTagElement.class))
														{
															((MarkupTagElement)currentElement).addElement(comment);
														}
													}
													insideSpecialDeclaration = false;
													insideTagDecl = false;
												}
											}
											else
											{
												if (insideString != 0 || !Character.isWhitespace(c))
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

		if (objectModel.getClass().isAssignableFrom(MarkupObjectModel.class))
		{
			mom.copyTo(objectModel);
		}
	}
	@Override
	protected void saveInternal(ObjectModel objectModel) throws IOException {
		// TODO Auto-generated method stub
		
	}
	
	private String replaceEntitiesInput(String value) {
		if (value != "")
		{
			KeyValuePair<String, String>[] ents = this.getSettings().getEntities();
			for (int i = 0; i < ents.length; i++)
			{
				KeyValuePair<String, String> kvp = ents[i];
				value = value.replaceAll(String.valueOf(this.getSettings().getEntityBeginChar()) + kvp.getKey() + String.valueOf(this.getSettings().getEntityEndChar()), kvp.getValue());
			}
		}
		return value;
	}
	private String replaceEntitiesOutput(String value) {
		if (value != "")
		{
			KeyValuePair<String, String>[] ents = this.getSettings().getEntities();
			for (int i = 0; i < ents.length; i++)
			{
				KeyValuePair<String, String> kvp = ents[i];
				value = value.replaceAll(kvp.getValue(), String.valueOf(this.getSettings().getEntityBeginChar()) + kvp.getKey() + String.valueOf(this.getSettings().getEntityEndChar()));
			}
		}
		return value;
	}
}
