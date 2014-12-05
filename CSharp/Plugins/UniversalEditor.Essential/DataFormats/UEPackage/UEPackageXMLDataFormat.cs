﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.DataFormats.PropertyList.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.UEPackage;

namespace UniversalEditor.DataFormats.UEPackage
{
	public class UEPackageXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(UEPackageObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
				_dfr.Filters.Add("Universal Editor package (XML)", new string[] { "*.uexml" });
			}
			return _dfr;
		}

		private static void LoadProjectFile(MarkupTagElement tag, ProjectFile.ProjectFileCollection coll)
		{
			ProjectFile file = new ProjectFile();
			MarkupAttribute attSourceFileName = tag.Attributes["SourceFileName"];
			if (attSourceFileName != null)
			{
				file.SourceFileName = attSourceFileName.Value;
			}
			MarkupAttribute attDestinationFileName = tag.Attributes["DestinationFileName"];
			if (attDestinationFileName != null)
			{
				file.DestinationFileName = attDestinationFileName.Value;
			}

			MarkupTagElement tagContent = (tag.Elements["Content"] as MarkupTagElement);
			if (tagContent != null && tagContent.Elements.Count == 1)
			{
				MarkupAttribute attObjectModelType = tagContent.Attributes["ObjectModelType"];
				if (attObjectModelType != null)
				{

				}

				MarkupStringElement cdata = (tagContent.Elements[0] as MarkupStringElement);
				file.Content = System.Text.Encoding.Default.GetBytes(cdata.Value);
			}
			else
			{
				return;
			}
			coll.Add(file);
		}

		private bool mvarIncludeProjectTypes = true;
		public bool IncludeProjectTypes { get { return mvarIncludeProjectTypes; } set { mvarIncludeProjectTypes = value; } }
		private bool mvarIncludeTemplates = true;
		public bool IncludeTemplates { get { return mvarIncludeTemplates; } set { mvarIncludeTemplates = value; } }

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			UEPackageObjectModel package = (objectModels.Pop() as UEPackageObjectModel);

			MarkupTagElement tagUniversalEditor = (mom.Elements["UniversalEditor"] as MarkupTagElement);
			if (tagUniversalEditor == null) throw new InvalidDataFormatException("Top-level tag 'UniversalEditor' not found");

			#region Data Formats
			{
				MarkupTagElement tagDataFormats = (tagUniversalEditor.Elements["DataFormats"] as MarkupTagElement);
				if (tagDataFormats != null)
				{
					foreach (MarkupElement elDataFormat in tagDataFormats.Elements)
					{
						MarkupTagElement tagDataFormat = (elDataFormat as MarkupTagElement);
						if (tagDataFormat == null) continue;

						MarkupTagElement tagInformation = (tagDataFormat.Elements["Information"] as MarkupTagElement);

						MarkupTagElement tagFilters = (tagDataFormat.Elements["Filters"] as MarkupTagElement);
						if (tagFilters == null) continue;

						MarkupTagElement tagCapabilities = (tagDataFormat.Elements["Capabilities"] as MarkupTagElement);
						if (tagCapabilities == null) continue;

						MarkupTagElement tagFormat = (tagDataFormat.Elements["Format"] as MarkupTagElement);
						if (tagFormat == null) continue;

						CustomDataFormatReference dfr = new CustomDataFormatReference();

						Dictionary<string, object> localVariables = new Dictionary<string, object>();

						#region Information
						{
							if (tagInformation != null)
							{
								if (tagInformation.Elements["Title"] != null)
								{
									dfr.Title = tagInformation.Elements["Title"].Value;
								}
							}
						}
						#endregion
						#region Capabilities
						{
							foreach (MarkupElement elCapability in tagCapabilities.Elements)
							{
								MarkupTagElement tagCapability = (elCapability as MarkupTagElement);
								if (tagCapability == null) continue;
								if (tagCapability.Name != "Capability") continue;
								if (tagCapability.Attributes["Value"] == null) continue;

								string capability = tagCapability.Attributes["Value"].Value;
								DataFormatCapabilities caps = DataFormatCapabilities.None;
								try
								{
									caps = (DataFormatCapabilities)Enum.Parse(typeof(DataFormatCapabilities), capability);
								}
								catch
								{
								}

								if (tagCapability.Attributes["ObjectModelType"] != null)
								{
									string nam = tagCapability.Attributes["ObjectModelType"].Value;
									Type objectModelType = Type.GetType(nam);
									if (objectModelType == null)
									{
										continue;
									}

									if (objectModelType.IsSubclassOf(typeof(ObjectModel)))
									{
										dfr.Capabilities.Add(objectModelType, caps);
									}
								}
								else if (tagCapability.Attributes["ObjectModelID"] != null)
								{
									dfr.Capabilities.Add(new Guid(tagCapability.Attributes["ObjectModelID"].Value), caps);
								}

							}
						}
						#endregion
						#region Filters
						{
							foreach (MarkupElement elFilter in tagFilters.Elements)
							{
								MarkupTagElement tagFilter = (elFilter as MarkupTagElement);
								if (tagFilter.Name != "Filter") continue;


								DataFormatFilter filter = new DataFormatFilter();
								MarkupAttribute attHintComparison = tagFilter.Attributes["HintComparison"];
								if (attHintComparison != null)
								{
									switch (attHintComparison.Value.ToLower())
									{
										case "always":
										{
											filter.HintComparison = DataFormatHintComparison.Always;
											break;
										}
										case "filteronly":
										{
											filter.HintComparison = DataFormatHintComparison.FilterOnly;
											break;
										}
										case "filterthenmagic":
										{
											filter.HintComparison = DataFormatHintComparison.FilterThenMagic;
											break;
										}
										case "magiconly":
										{
											filter.HintComparison = DataFormatHintComparison.MagicOnly;
											break;
										}
										case "magicthenfilter":
										{
											filter.HintComparison = DataFormatHintComparison.MagicThenFilter;
											break;
										}
										default:
										{
											filter.HintComparison = DataFormatHintComparison.None;
											break;
										}
									}
								}

								MarkupTagElement tagFilterTitle = (tagFilter.Elements["Title"] as MarkupTagElement);
								if (tagFilterTitle != null) filter.Title = tagFilterTitle.Value;

								#region File Name Filters
								{
									MarkupTagElement tagFilterFileNames = (tagFilter.Elements["FileNameFilters"] as MarkupTagElement);
									if (tagFilterFileNames != null)
									{
										foreach (MarkupElement elFilterFileName in tagFilterFileNames.Elements)
										{
											MarkupTagElement tagFilterFileName = (elFilterFileName as MarkupTagElement);
											if (tagFilterFileName.Name != "FileNameFilter") continue;
											filter.FileNameFilters.Add(tagFilterFileName.Value);
										}
									}
								}
								#endregion
								#region Magic Bytes
								{
									MarkupTagElement tagMagicBytes = (tagFilter.Elements["MagicBytes"] as MarkupTagElement);
									if (tagMagicBytes != null)
									{
										foreach (MarkupElement elMagicByteCollection in tagMagicBytes.Elements)
										{
											MarkupTagElement tagMagicByteCollection = (elMagicByteCollection as MarkupTagElement);
											if (tagMagicByteCollection == null) continue;
											if (tagMagicByteCollection.Name != "MagicByteCollection") continue;

											List<byte?> array = new List<byte?>();
											foreach (MarkupElement elMagicByte in tagMagicByteCollection.Elements)
											{
												MarkupTagElement tagMagicByte = (elMagicByte as MarkupTagElement);
												if (tagMagicByte == null) continue;
												if (tagMagicByte.Name != "MagicByte") continue;

												byte? value = null;
												byte tryValue = 0;
												char tryChar = '\0';

												if (Byte.TryParse(tagMagicByte.Value, out tryValue))
												{
													value = tryValue;
												}
												else if (tagMagicByte.Value.StartsWith("0x"))
												{
													if (Byte.TryParse(tagMagicByte.Value.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out tryValue))
													{
														value = tryValue;
													}
												}
												else if (tagMagicByte.Value.Length > 1)
												{
													for (int i = 0; i < tagMagicByte.Value.Length; i++)
													{
														array.Add((byte)(tagMagicByte.Value[i]));
													}
													continue;
												}
												else if (Char.TryParse(tagMagicByte.Value, out tryChar))
												{
													value = (byte)tryChar;
												}
												array.Add(value);
											}
											filter.MagicBytes.Add(array.ToArray());
										}
									}
								}
								#endregion

								dfr.Filters.Add(filter);
							}
						}
						#endregion
						#region Format
						{
							foreach (MarkupElement elField in tagFormat.Elements)
							{
								MarkupTagElement tagField = (elField as MarkupTagElement);
								if (tagField == null) continue;

								switch (tagField.Name)
								{
									case "Field":
									{
										if (tagField.Attributes["DataType"] == null) continue;

										CustomDataFormatItemField cdfif = new CustomDataFormatItemField();
										cdfif.DataType = tagField.Attributes["DataType"].Value;

										if (tagField.Attributes["ID"] != null)
										{
											cdfif.Name = tagField.Attributes["ID"].Value;
										}
										dfr.Items.Add(cdfif);
										break;
									}
									case "Array":
									{
										if (tagField.Attributes["DataType"] == null) continue;

										CustomDataFormatItemArray cdfif = new CustomDataFormatItemArray();
										cdfif.DataType = tagField.Attributes["DataType"].Value;

										if (tagField.Attributes["ID"] != null)
										{
											cdfif.Name = tagField.Attributes["ID"].Value;
										}
										if (tagField.Attributes["Length"] != null)
										{
											string value = tagField.Attributes["Length"].Value;
											value = UniversalEditor.Common.Strings.ReplaceVariables(value, localVariables);
											int length = 0;
											Int32.TryParse(value, out length);

											cdfif.Length = length;
										}
										dfr.Items.Add(cdfif);
										break;
									}
								}
							}
						}
						#endregion

						package.DataFormats.Add(dfr);
					}
				}
			}
			#endregion
			#region Project Types
			{
				if (mvarIncludeProjectTypes)
				{
					MarkupTagElement tagProjectTypes = (tagUniversalEditor.Elements["ProjectTypes"] as MarkupTagElement);
					if (tagProjectTypes != null)
					{
						foreach (MarkupElement elProjectType in tagProjectTypes.Elements)
						{
							MarkupTagElement tagProjectType = (elProjectType as MarkupTagElement);
							if (tagProjectType == null) continue;
							if (tagProjectType.FullName != "ProjectType") continue;

							MarkupAttribute attID = tagProjectType.Attributes["ID"];
							if (attID == null) continue;

							ProjectType projtype = new ProjectType();
							projtype.ID = new Guid(attID.Value);

							MarkupTagElement tagInformation = (tagProjectType.Elements["Information"] as MarkupTagElement);
							if (tagInformation != null)
							{
								MarkupTagElement tagTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
								if (tagTitle != null) projtype.Title = tagTitle.Value;
								MarkupTagElement tagIconPath = (tagInformation.Elements["IconPath"] as MarkupTagElement);
								if (tagIconPath != null)
								{
									MarkupAttribute attFileName = tagIconPath.Attributes["FileName"];
									if (attFileName != null)
									{
										string FileName = attFileName.Value;
										if (System.IO.File.Exists(FileName)) projtype.LargeIconImageFileName = FileName;
										if (System.IO.File.Exists(FileName)) projtype.SmallIconImageFileName = FileName;
									}
									MarkupAttribute attLargeFileName = tagIconPath.Attributes["LargeFileName"];
									if (attLargeFileName != null)
									{
										string FileName = attLargeFileName.Value;
										if (System.IO.File.Exists(FileName)) projtype.LargeIconImageFileName = FileName;
									}
									MarkupAttribute attSmallFileName = tagIconPath.Attributes["SmallFileName"];
									if (attSmallFileName != null)
									{
										string FileName = attSmallFileName.Value;
										if (System.IO.File.Exists(FileName)) projtype.SmallIconImageFileName = FileName;
									}
								}
							}
							#region ItemShortcuts
							{
								MarkupTagElement tagItemShortcuts = (tagProjectType.Elements["ItemShortcuts"] as MarkupTagElement);
								if (tagItemShortcuts != null)
								{
									foreach (MarkupElement el in tagItemShortcuts.Elements)
									{
										MarkupTagElement tag = (el as MarkupTagElement);
										if (tag == null) continue;
										if (tag.FullName != "ItemShortcut") continue;

										ProjectTypeItemShortcut z = new ProjectTypeItemShortcut();
										z.Title = tag.Attributes["Title"].Value;

										string objectModelTypeName = tag.Attributes["ObjectModelTypeName"].Value;
										z.ObjectModelReference = Common.Reflection.GetAvailableObjectModelByTypeName(objectModelTypeName);

										projtype.ItemShortcuts.Add(z);
									}
								}
							}
							#endregion
							#region
							{
								MarkupTagElement tagTasks = (tagProjectType.Elements["Tasks"] as MarkupTagElement);
								if (tagTasks != null)
								{
									foreach (MarkupElement el in tagTasks.Elements)
									{
										MarkupTagElement tag = (el as MarkupTagElement);
										if (tag == null) continue;
										if (tag.FullName != "Task") continue;

										ProjectTask task = new ProjectTask();
										task.Title = tag.Attributes["Title"].Value;

										projtype.Tasks.Add(task);
									}
								}
							}
							#endregion

							package.ProjectTypes.Add(projtype);
						}
					}
				}
			}
			#endregion
			#region Templates
			{
				if (mvarIncludeTemplates)
				{
					#region Document Templates
					{
						MarkupTagElement tagTemplates = (tagUniversalEditor.Elements["DocumentTemplates"] as MarkupTagElement);
						if (tagTemplates != null)
						{
							foreach (MarkupElement elTemplate in tagTemplates.Elements)
							{
								MarkupTagElement tagTemplate = (elTemplate as MarkupTagElement);
								if (tagTemplate == null) continue;

								if (tagTemplate.FullName != "DocumentTemplate") continue;

								DocumentTemplate template = new DocumentTemplate();

								MarkupAttribute attID = tagTemplate.Attributes["ID"];
								if (attID != null)
								{
									template.ID = new Guid(attID.Value);
								}

								MarkupTagElement tagInformation = (tagTemplate.Elements["Information"] as MarkupTagElement);

								#region Information
								{
									if (tagInformation != null)
									{
										if (tagInformation.Elements["Title"] != null)
										{
											template.Title = tagInformation.Elements["Title"].Value;
										}
										if (tagInformation.Elements["Description"] != null)
										{
											template.Description = tagInformation.Elements["Description"].Value;
										}

										MarkupTagElement tagIconPath = (tagInformation.Elements["IconPath"] as MarkupTagElement);
										if (tagIconPath != null)
										{
											MarkupAttribute attFileName = tagIconPath.Attributes["FileName"];
											if (attFileName != null)
											{
												string ImageFileName = attFileName.Value;
												template.LargeIconImageFileName = ImageFileName;
												template.SmallIconImageFileName = ImageFileName;
											}
											MarkupAttribute attLargeFileName = tagIconPath.Attributes["LargeFileName"];
											if (attLargeFileName != null)
											{
												string ImageFileName = attLargeFileName.Value;
												template.LargeIconImageFileName = ImageFileName;
											}
											MarkupAttribute attSmallFileName = tagIconPath.Attributes["SmallFileName"];
											if (attSmallFileName != null)
											{
												string ImageFileName = attSmallFileName.Value;
												template.SmallIconImageFileName = ImageFileName;
											}
										}
									}
								}
								#endregion
								#region Variables
								{
									MarkupTagElement tagVariables = (tagTemplate.Elements["Variables"] as MarkupTagElement);
									if (tagVariables != null)
									{
										foreach (MarkupElement elVariable in tagVariables.Elements)
										{
											MarkupTagElement tagVariable = (elVariable as MarkupTagElement);
											if (tagVariable == null) continue;
											if (tagVariable.FullName != "Variable") continue;

											TemplateVariable varr = new TemplateVariable();

											MarkupAttribute attDataType = tagVariable.Attributes["DataType"];
											if (attDataType != null)
											{
												varr.DataType = attDataType.Value;
											}
											else
											{
												varr.DataType = "String";
											}

											MarkupAttribute attName = tagVariable.Attributes["Name"];
											if (attName != null)
											{
												varr.Name = attName.Value;
											}

											MarkupAttribute attValue = tagVariable.Attributes["Value"];
											if (attValue != null)
											{
												varr.Value = attValue.Value;
											}

											MarkupAttribute attLabel = tagVariable.Attributes["Label"];
											if (attLabel != null)
											{
												varr.Label = attLabel.Value;
											}

											MarkupTagElement tagChoices = (tagVariable.Elements["Choices"] as MarkupTagElement);
											if (tagChoices != null)
											{
												foreach (MarkupElement elChoice in tagChoices.Elements)
												{
													MarkupTagElement tagChoice = (elChoice as MarkupTagElement);
													if (tagChoice == null) continue;
													if (tagChoice.FullName != "Choice") continue;

													MarkupAttribute attChoiceValue = tagChoice.Attributes["Value"];
													if (attChoiceValue == null) continue;

													MarkupAttribute attChoiceName = tagChoice.Attributes["Name"];
													if (attChoiceName != null)
													{
														varr.Choices.Add(attChoiceName.Value, attChoiceValue.Value);
													}
													else
													{
														varr.Choices.Add(attChoiceValue.Value, attChoiceValue.Value);
													}
												}
											}

											template.Variables.Add(varr);
										}
									}
								}
								#endregion
								#region Content
								{
									MarkupTagElement tagContent = (tagTemplate.Elements["Content"] as MarkupTagElement);
									if (tagContent != null)
									{
										if (tagContent.Attributes["ObjectModelID"] != null)
										{
											template.ObjectModelReference = new ObjectModelReference(new Guid(tagContent.Attributes["ObjectModelID"].Value));
										}
										else if (tagContent.Attributes["ObjectModelType"] != null)
										{
											template.ObjectModelReference = new ObjectModelReference(tagContent.Attributes["ObjectModelType"].Value);
										}
										template.TemplateContent.Elements.Add(tagContent);
									}
								}
								#endregion

								package.DocumentTemplates.Add(template);
							}
						}
					}
					#endregion
					#region Project Templates
					{
						MarkupTagElement tagTemplates = (tagUniversalEditor.Elements["ProjectTemplates"] as MarkupTagElement);
						if (tagTemplates != null)
						{
							foreach (MarkupElement elTemplate in tagTemplates.Elements)
							{
								MarkupTagElement tagTemplate = (elTemplate as MarkupTagElement);
								if (tagTemplate == null) continue;

								if (tagTemplate.FullName != "ProjectTemplate") continue;

								ProjectTemplate template = new ProjectTemplate();

								MarkupAttribute attTypeID = tagTemplate.Attributes["TypeID"];
								if (attTypeID != null)
								{
									try
									{
										template.ProjectType = Common.Reflection.GetProjectTypeByTypeID(new Guid(attTypeID.Value));
									}
									catch
									{
									}
								}

								#region Information
								MarkupTagElement tagInformation = (tagTemplate.Elements["Information"] as MarkupTagElement);
								if (tagInformation != null)
								{
									MarkupTagElement tagTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
									if (tagTitle != null) template.Title = tagTitle.Value;

									MarkupTagElement tagDescription = (tagInformation.Elements["Description"] as MarkupTagElement);
									if (tagDescription != null) template.Description = tagDescription.Value;

									if (tagInformation.Elements["ProjectNamePrefix"] != null)
									{
										template.ProjectNamePrefix = tagInformation.Elements["ProjectNamePrefix"].Value;
									}

									MarkupTagElement tagPath = (tagInformation.Elements["Path"] as MarkupTagElement);
									if (tagPath != null)
									{
										List<string> pathParts = new List<string>();
										foreach (MarkupElement elPart in tagPath.Elements)
										{
											MarkupTagElement tagPart = (elPart as MarkupTagElement);
											if (tagPart == null) continue;
											if (tagPart.FullName != "Part") continue;
											pathParts.Add(tagPart.Value);
										}
										template.Path = pathParts.ToArray();
									}

									MarkupTagElement tagIconPath = (tagInformation.Elements["IconPath"] as MarkupTagElement);
									if (tagIconPath != null)
									{
										#region All Icons
										{
											MarkupAttribute attFileName = tagIconPath.Attributes["FileName"];
											if (attFileName != null)
											{
												string FileName = attFileName.Value;
												if (System.IO.File.Exists(FileName)) template.LargeIconImageFileName = FileName;
												if (System.IO.File.Exists(FileName)) template.SmallIconImageFileName = FileName;
											}
										}
										#endregion
										#region Large Icon
										{
											MarkupAttribute attLargeFileName = tagIconPath.Attributes["LargeFileName"];
											if (attLargeFileName != null)
											{
												string FileName = attLargeFileName.Value;
												if (System.IO.File.Exists(FileName)) template.LargeIconImageFileName = FileName;
											}
										}
										#endregion
										#region Small Icon
										{
											MarkupAttribute attSmallFileName = tagIconPath.Attributes["SmallFileName"];
											if (attSmallFileName != null)
											{
												string FileName = attSmallFileName.Value;
												if (System.IO.File.Exists(FileName)) template.SmallIconImageFileName = FileName;
											}
										}
										#endregion
									}
								}
								#endregion
								#region FileSystem
								{
									MarkupTagElement tagFileSystem = (tagTemplate.Elements["FileSystem"] as MarkupTagElement);
									if (tagFileSystem != null)
									{
										MarkupTagElement tagFiles = (tagFileSystem.Elements["Files"] as MarkupTagElement);
										if (tagFiles != null)
										{
											foreach (MarkupElement elFile in tagFiles.Elements)
											{
												MarkupTagElement tagFile = (elFile as MarkupTagElement);
												if (tagFile == null) continue;
												if (tagFile.FullName != "File") continue;

												LoadProjectFile(tagFile, template.FileSystem.Files);
											}
										}
									}
								}
								#endregion
								#region Configuration
								{
									MarkupTagElement tagConfiguration = (tagTemplate.Elements["Configuration"] as MarkupTagElement);
									if (tagConfiguration != null)
									{
										PropertyListObjectModel plom = template.Configuration;
										XMLPropertyListDataFormat xmlplist = new XMLPropertyListDataFormat();
										XMLPropertyListDataFormat.LoadMarkup(tagConfiguration, ref plom);
									}
								}
								#endregion

								package.ProjectTemplates.Add(template);
							}
						}
					}
				}
				#endregion
			}
			#endregion
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			UEPackageObjectModel package = (objectModels.Pop() as UEPackageObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagUniversalEditor = new MarkupTagElement();
			tagUniversalEditor.FullName = "UniversalEditor";
			tagUniversalEditor.Attributes.Add("Version", "4.0");

			#region Project Types
			{
				if (mvarIncludeProjectTypes)
				{
					if (package.ProjectTypes.Count > 0)
					{
						MarkupTagElement tagProjectTypes = new MarkupTagElement();
						tagProjectTypes.FullName = "ProjectTypes";
						foreach (ProjectType projtype in package.ProjectTypes)
						{
							MarkupTagElement tagProjectType = new MarkupTagElement();
							tagProjectType.FullName = "ProjectType";
							tagProjectType.Attributes.Add("ID", projtype.ID.ToString("B"));

							#region Information
							{
								MarkupTagElement tagInformation = new MarkupTagElement();
								tagInformation.FullName = "Information";

								MarkupTagElement tagTitle = new MarkupTagElement();
								tagTitle.FullName = "Title";
								tagTitle.Value = projtype.Title;
								tagInformation.Elements.Add(tagTitle);

								tagProjectType.Elements.Add(tagInformation);
							}
							#endregion
							#region Tasks
							{
								if (projtype.Tasks.Count > 0)
								{
									MarkupTagElement tagTasks = new MarkupTagElement();
									tagTasks.FullName = "Tasks";

									foreach (ProjectTask task in projtype.Tasks)
									{
										MarkupTagElement tagTask = new MarkupTagElement();
										tagTask.FullName = "Task";
										tagTask.Attributes.Add("Title", task.Title);

										if (task.Actions.Count > 0)
										{
											MarkupTagElement tagActions = new MarkupTagElement();
											tagActions.FullName = "Actions";
											foreach (ProjectTaskAction action in task.Actions)
											{
												MarkupTagElement tagAction = new MarkupTagElement();
												tagAction.FullName = "Action";

												// TODO: load the action

												tagActions.Elements.Add(tagAction);
											}
											tagTask.Elements.Add(tagActions);
										}

										tagTasks.Elements.Add(tagTask);
									}

									tagProjectType.Elements.Add(tagTasks);
								}
							}
							#endregion
							#region ItemShortcuts
							{
								if (projtype.ItemShortcuts.Count > 0)
								{
									MarkupTagElement tagItemShortcuts = new MarkupTagElement();
									tagItemShortcuts.FullName = "ItemShortcuts";
									foreach (ProjectTypeItemShortcut shortcut in projtype.ItemShortcuts)
									{
										MarkupTagElement tagItemShortcut = new MarkupTagElement();
										tagItemShortcut.FullName = "ItemShortcut";
										tagItemShortcut.Attributes.Add("Title", shortcut.Title);
										if (shortcut.ObjectModelReference.ObjectModelTypeName != null)
										{
											tagItemShortcut.Attributes.Add("ObjectModelTypeName", shortcut.ObjectModelReference.ObjectModelTypeName);
										}
										if (shortcut.ObjectModelReference.ObjectModelID != Guid.Empty)
										{
											tagItemShortcut.Attributes.Add("ObjectModelID", shortcut.ObjectModelReference.ObjectModelID.ToString("B"));
										}
										tagItemShortcut.Attributes.Add("DocumentTemplateID", shortcut.DocumentTemplate.ID.ToString("B"));
										tagItemShortcuts.Elements.Add(tagItemShortcut);
									}
									tagProjectType.Elements.Add(tagItemShortcuts);
								}
							}
							#endregion
							#region ProjectVariables
							{
								/*
								if (projtype.ProjectVariables.Count > 0)
								{
									MarkupTagElement tagProjectVariables = new MarkupTagElement();
									tagProjectVariables.FullName = "ProjectVariables";

									tagProjectType.Elements.Add(tagProjectVariables);
								}
								*/
							}
							#endregion

							tagProjectTypes.Elements.Add(tagProjectType);
						}
						tagUniversalEditor.Elements.Add(tagProjectTypes);
					}
				}
			}
			#endregion

			mom.Elements.Add(tagUniversalEditor);

			objectModels.Push(mom);
		}
	}
}
