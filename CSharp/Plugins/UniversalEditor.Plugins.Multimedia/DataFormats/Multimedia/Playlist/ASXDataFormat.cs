using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.Markup;
namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	public class ASXDataFormat : XMLDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();

			dfr.Filters.Add("Advanced Stream Redirector", new string[] { "*.asx" });
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			dfr.ContentTypes.Add("video/x-ms-asf");
			return dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			MarkupTagElement tagASX = mom.Elements["asx"] as MarkupTagElement;
			if (tagASX != null)
			{
				foreach (MarkupElement el in tagASX.Elements)
				{
					MarkupTagElement tag = el as MarkupTagElement;
					if (tag != null)
					{
						string name2 = tag.Name;
						if (name2 != null)
						{
							if (!(name2 == "title"))
							{
								if (!(name2 == "author"))
								{
									if (!(name2 == "abstract"))
									{
										if (!(name2 == "copyright"))
										{
											if (!(name2 == "param"))
											{
												if (name2 == "entry")
												{
													PlaylistEntry entry = new PlaylistEntry();
													foreach (MarkupElement entryEl in tag.Elements)
													{
														MarkupTagElement tagEl = entryEl as MarkupTagElement;
														if (tagEl != null)
														{
															name2 = tagEl.Name;
															if (name2 != null)
															{
																if (!(name2 == "title"))
																{
																	if (!(name2 == "author"))
																	{
																		if (!(name2 == "abstract"))
																		{
																			if (!(name2 == "copyright"))
																			{
																				if (!(name2 == "ref"))
																				{
																					if (name2 == "param")
																					{
																						if (tagEl.Attributes["name"] != null)
																						{
																							string name = tagEl.Attributes["name"].Value;
																							string value = string.Empty;
																							if (tagEl.Attributes["value"] != null)
																							{
																								value = tagEl.Attributes["value"].Value;
																							}
																							entry.CustomInformation.Add(name, value);
																						}
																					}
																				}
																				else
																				{
																					if (tagEl.Attributes["href"] != null)
																					{
																						entry.FileName = tagEl.Attributes["href"].Value;
																					}
																				}
																			}
																			else
																			{
																				entry.Copyright = tagEl.Value;
																			}
																		}
																		else
																		{
																			entry.Abstract = tagEl.Value;
																		}
																	}
																	else
																	{
																		entry.Author = tagEl.Value;
																	}
																}
																else
																{
																	entry.Title = tagEl.Value;
																}
															}
														}
													}
													pom.Entries.Add(entry);
												}
											}
											else
											{
												if (tag.Attributes["name"] != null)
												{
													string name = tag.Attributes["name"].Value;
													string value = string.Empty;
													if (tag.Attributes["value"] != null)
													{
														value = tag.Attributes["value"].Value;
													}
													pom.CustomInformation.Add(name, value);
												}
											}
										}
										else
										{
											pom.Copyright = tag.Value;
										}
									}
									else
									{
										pom.Abstract = tag.Value;
									}
								}
								else
								{
									pom.Author = tag.Value;
								}
							}
							else
							{
								pom.Title = tag.Value;
							}
						}
					}
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			if (pom != null)
			{
				MarkupObjectModel mom = new MarkupObjectModel();
				MarkupTagElement tagASX = new MarkupTagElement();
				tagASX.Name = "asx";
				tagASX.Attributes.Add("version", "3.0");
				tagASX.Attributes.Add("previewmode", "No");
				if (!string.IsNullOrEmpty(pom.Abstract))
				{
					MarkupTagElement tagAbstract = new MarkupTagElement();
					tagAbstract.Name = "abstract";
					tagAbstract.Value = pom.Abstract;
					tagASX.Elements.Add(tagAbstract);
				}
				if (!string.IsNullOrEmpty(pom.Title))
				{
					MarkupTagElement tagTitle = new MarkupTagElement();
					tagTitle.Name = "title";
					tagTitle.Value = pom.Title;
					tagASX.Elements.Add(tagTitle);
				}
				if (!string.IsNullOrEmpty(pom.Author))
				{
					MarkupTagElement tagAuthor = new MarkupTagElement();
					tagAuthor.Name = "author";
					tagAuthor.Value = pom.Author;
					tagASX.Elements.Add(tagAuthor);
				}
				if (!string.IsNullOrEmpty(pom.Copyright))
				{
					MarkupTagElement tagCopyright = new MarkupTagElement();
					tagCopyright.Name = "copyright";
					tagCopyright.Value = pom.Copyright;
					tagASX.Elements.Add(tagCopyright);
				}
				foreach (PlaylistEntry entry in pom.Entries)
				{
					MarkupTagElement tagEntry = new MarkupTagElement();
					tagEntry.Name = "entry";
					if (!string.IsNullOrEmpty(entry.FileName))
					{
						MarkupTagElement tagRef = new MarkupTagElement();
						tagRef.Name = "ref";
						tagRef.Attributes.Add("href", entry.FileName);
						tagEntry.Elements.Add(tagRef);
					}
					if (!string.IsNullOrEmpty(entry.Abstract))
					{
						MarkupTagElement tagAbstract = new MarkupTagElement();
						tagAbstract.Name = "abstract";
						tagAbstract.Value = entry.Abstract;
						tagEntry.Elements.Add(tagAbstract);
					}
					if (!string.IsNullOrEmpty(entry.Title))
					{
						MarkupTagElement tagTitle = new MarkupTagElement();
						tagTitle.Name = "title";
						tagTitle.Value = entry.Title;
						tagEntry.Elements.Add(tagTitle);
					}
					if (!string.IsNullOrEmpty(entry.Author))
					{
						MarkupTagElement tagAuthor = new MarkupTagElement();
						tagAuthor.Name = "author";
						tagAuthor.Value = entry.Author;
						tagEntry.Elements.Add(tagAuthor);
					}
					if (!string.IsNullOrEmpty(entry.Copyright))
					{
						MarkupTagElement tagCopyright = new MarkupTagElement();
						tagCopyright.Name = "copyright";
						tagCopyright.Value = entry.Copyright;
						tagEntry.Elements.Add(tagCopyright);
					}
					tagASX.Elements.Add(tagEntry);
				}
				mom.Elements.Add(tagASX);
				objectModels.Push(mom);
			}
		}
	}
}
