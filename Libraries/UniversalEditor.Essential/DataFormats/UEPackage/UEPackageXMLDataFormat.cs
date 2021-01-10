//
//  UEPackageXMLDataFormat.cs - provides a DataFormat for manipulating Universal Editor package files in XML format
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
using System.Collections.Generic;
using System.Diagnostics;

using MBS.Framework.Logic;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.DataFormats.PropertyList.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.UEPackage;

namespace UniversalEditor.DataFormats.UEPackage
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Universal Editor package files in XML format.
	/// </summary>
	public class UEPackageXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(UEPackageObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
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

			#region Accssors
			{
				MarkupTagElement tagAccessors = (tagUniversalEditor.Elements["Accessors"] as MarkupTagElement);
				if (tagAccessors != null)
				{
					foreach (MarkupElement elAccessor in tagAccessors.Elements)
					{
						MarkupTagElement tagAccessor = (elAccessor as MarkupTagElement);
						if (tagAccessor == null) continue;

						AccessorReference accref = null;
						MarkupAttribute attTypeName = tagAccessor.Attributes["TypeName"];
						if (attTypeName != null)
						{
							Type type = MBS.Framework.Reflection.FindType(attTypeName.Value);
							if (type != null)
							{
								Accessor acc = (type.Assembly.CreateInstance(type.FullName) as Accessor);
								if (acc != null)
								{
									accref = acc.MakeReference();
								}
							}
						}

						if (accref != null)
						{
							MarkupTagElement tagSchemas = tagAccessor.Elements["Schemas"] as MarkupTagElement;
							if (tagSchemas != null)
							{
								foreach (MarkupElement elSchema in tagSchemas.Elements)
								{
									MarkupTagElement tagSchema = (elSchema as MarkupTagElement);
									if (tagSchema == null) continue;

									MarkupAttribute attSchemaValue = tagSchema.Attributes["Value"];
									if (attSchemaValue == null) continue;

									accref.Schemas.Add(attSchemaValue.Value);

									MarkupAttribute attSchemaTitle = tagSchema.Attributes["Title"];
									if (attSchemaTitle != null)
									{

									}
								}
							}
						}
					}
				}

			}
			#endregion
			#region Data Formats
			{
				MarkupTagElement tagDataFormats = (tagUniversalEditor.Elements["DataFormats"] as MarkupTagElement);
				if (tagDataFormats != null)
				{
					foreach (MarkupElement elDataFormat in tagDataFormats.Elements)
					{
						MarkupTagElement tagDataFormat = (elDataFormat as MarkupTagElement);
						if (tagDataFormat == null) continue;

						MarkupAttribute attID = tagDataFormat.Attributes["ID"];
						if (attID == null) continue;

						MarkupTagElement tagInformation = (tagDataFormat.Elements["Information"] as MarkupTagElement);

						MarkupTagElement tagFilters = (tagDataFormat.Elements["Filters"] as MarkupTagElement);
						// if (tagFilters == null) continue;

						MarkupTagElement tagCapabilities = (tagDataFormat.Elements["Capabilities"] as MarkupTagElement);
						// if (tagCapabilities == null) continue;

						MarkupTagElement tagFormat = (tagDataFormat.Elements["Format"] as MarkupTagElement);
						if (tagFormat == null) continue;

						CustomDataFormatReference dfr = new CustomDataFormatReference();
						dfr.ID = new Guid(attID.Value);

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
							if (tagCapabilities != null)
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
						}
						#endregion
						#region Export Options
						{
							MarkupTagElement tagCustomOptions = (tagDataFormat.Elements["ExportOptions"] as MarkupTagElement);
							if (tagCustomOptions != null)
							{
								foreach (MarkupElement elCustomOption in tagCustomOptions.Elements)
								{
									MarkupTagElement tagCustomOption = (elCustomOption as MarkupTagElement);
									if (tagCustomOption == null) continue;

									CustomOption co = LoadCustomOption(tagCustomOption);
									if (co == null) continue;

									dfr.ExportOptions.Add(co);
								}
							}
						}
						#endregion
						#region Import Options
						{
							MarkupTagElement tagCustomOptions = (tagDataFormat.Elements["ImportOptions"] as MarkupTagElement);
							if (tagCustomOptions != null)
							{
								foreach (MarkupElement elCustomOption in tagCustomOptions.Elements)
								{
									MarkupTagElement tagCustomOption = (elCustomOption as MarkupTagElement);
									if (tagCustomOption == null) continue;

									CustomOption co = LoadCustomOption(tagCustomOption);
									if (co == null) continue;

									dfr.ImportOptions.Add(co);
								}
							}
						}
						#endregion
						#region Structures
						MarkupTagElement tagStructures = (tagDataFormat.Elements["Structures"] as MarkupTagElement);
						if (tagStructures != null)
						{
							foreach (MarkupElement elStructure in tagStructures.Elements)
							{
								MarkupTagElement tagStructure = (elStructure as MarkupTagElement);
								if (tagStructure == null) continue;
								if (tagStructure.FullName != "Structure") continue;

								CustomDataFormatStructure cdfi = CreateStructure(tagStructure, localVariables);
								if (cdfi != null) dfr.Structures.Add(cdfi);
							}
						}
						#endregion
						#region Format
						{
							foreach (MarkupElement elField in tagFormat.Elements)
							{
								MarkupTagElement tagField = (elField as MarkupTagElement);
								if (tagField == null) continue;

								CustomDataFormatItem cdfi = CreateField(tagField, localVariables);
								if (cdfi != null) dfr.Items.Add(cdfi);
							}
						}
						#endregion

						DataFormatReference.Register(dfr);
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

								MarkupTagElement tagProjectFileExtension = (tagInformation.Elements["ProjectFileExtension"] as MarkupTagElement);
								if (tagProjectFileExtension != null) projtype.ProjectFileExtension = tagProjectFileExtension.Value;

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
							#region Tasks
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
										task.Title = tag.Attributes["Title"]?.Value;

										MarkupTagElement tagActions = (tag.Elements["Actions"] as MarkupTagElement);
										if (tagActions != null)
										{
											foreach (MarkupElement elAction in tagActions.Elements)
											{
												MarkupTagElement tagAction = (elAction as MarkupTagElement);
												if (tagAction == null) continue;
												if (tagAction.FullName != "Action") continue;

												MarkupAttribute attTypeID = tagAction.Attributes["TypeID"];
												if (attTypeID != null)
												{
													Guid id = new Guid(attTypeID.Value);

													ProjectTaskActionReference ptar = ProjectTaskActionReference.GetByTypeID(id);
													ProjectTaskAction pta = ptar.Create();
													pta.LoadFromMarkup(tagAction);
													task.Actions.Add(pta);
												}
											}
										}

										projtype.Tasks.Add(task);
									}
								}
							}
							#endregion
							#region Variables
							{
								MarkupTagElement tagVariables = (tagProjectType.Elements["Variables"] as MarkupTagElement);
								if (tagVariables != null)
								{
									foreach (MarkupElement el in tagVariables.Elements)
									{
										MarkupTagElement tag = (el as MarkupTagElement);
										if (tag == null) continue;
										if (tag.FullName != "Variable") continue;

										ProjectTypeVariable varr = new ProjectTypeVariable();
										varr.Name = (tag.Attributes["Name"] == null ? String.Empty : tag.Attributes["Name"].Value);
										varr.Title = (tag.Attributes["Title"] == null ? String.Empty : tag.Attributes["Title"].Value);

										MarkupAttribute attType = tag.Attributes["Type"];
										if (attType != null)
										{
											switch (attType.Value.ToLower())
											{
												case "choice":
												{
													varr.Type = ProjectTypeVariableType.Choice;
													break;
												}
												case "fileopen":
												{
													varr.Type = ProjectTypeVariableType.FileOpen;
													break;
												}
												case "filesave":
												{
													varr.Type = ProjectTypeVariableType.FileSave;
													break;
												}
												default:
												{
													varr.Type = ProjectTypeVariableType.Text;
													break;
												}
											}
										}

										MarkupTagElement tagValidValues = (tag.Elements["ValidValues"] as MarkupTagElement);
										if (tagValidValues != null)
										{
											foreach (MarkupElement elValidValue in tagValidValues.Elements)
											{
												MarkupTagElement tagValidValue = (elValidValue as MarkupTagElement);
												if (tagValidValue == null) continue;
												if (tagValidValue.FullName != "ValidValue") continue;

												string value = (tagValidValue.Attributes["Value"] == null ? String.Empty : tagValidValue.Attributes["Value"].Value);
												string title = (tagValidValue.Attributes["Title"] == null ? value : tagValidValue.Attributes["Title"].Value);

												varr.ValidValues.Add(title, value);
											}
										}
										projtype.Variables.Add(varr);
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

										if (tagContent.Attributes["FileName"] != null)
										{
											// TODO: load the specified file as the given ObjectModel type
										}
										else
										{
											if (template.ObjectModelReference.Type != null)
											{
												System.Reflection.ParameterModifier pmodType = new System.Reflection.ParameterModifier();
												System.Reflection.MethodInfo miFromMarkup = template.ObjectModelReference.Type.GetMethod("FromMarkup", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, new Type[] { typeof(MarkupTagElement) }, new System.Reflection.ParameterModifier[] { pmodType });
												if (miFromMarkup != null)
												{
													ObjectModel om = (miFromMarkup.Invoke(null, new object[] { tagContent }) as ObjectModel);
													template.ObjectModel = om;
												}
											}
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
										ProjectType projectType = Common.Reflection.GetProjectTypeByTypeID(new Guid(attTypeID.Value));
										if (projectType != null)
										{
											template.ProjectTypes.Add(projectType);
										}
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
										MarkupAttribute attCopyFrom = tagFileSystem.Attributes["CopyFrom"];
										if (attCopyFrom != null)
										{
											string dir = System.IO.Path.GetDirectoryName(Accessor.GetFileName());
											string copyFrom = System.IO.Path.Combine(new string[] { dir, attCopyFrom.Value });
											if (System.IO.Directory.Exists(copyFrom))
											{
												string[] files = System.IO.Directory.GetFiles(copyFrom, "*", System.IO.SearchOption.AllDirectories);
												for (int i = 0; i < files.Length; i++)
												{
													ProjectFile pf = new ProjectFile();
													pf.SourceFileAccessor = new MemoryAccessor(System.IO.File.ReadAllBytes(files[i]), System.IO.Path.GetFileName(files[i]));
													pf.DestinationFileName = files[i].Substring(copyFrom.Length + 1);
													template.FileSystem.Files.Add(pf);
												}
											}
										}
										else
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
			#region Associations
			{
				MarkupTagElement tagAssociations = (tagUniversalEditor.Elements["Associations"] as MarkupTagElement);
				if (tagAssociations != null)
				{
					foreach (MarkupElement elAssociation in tagAssociations.Elements)
					{
						MarkupTagElement tagAssociation = (elAssociation as MarkupTagElement);
						if (tagAssociation == null) continue;
						if (tagAssociation.FullName != "Association") continue;

						Association association = new Association();

						MarkupTagElement tagObjectModels = (tagAssociation.Elements["ObjectModels"] as MarkupTagElement);
						if (tagObjectModels != null)
						{
							foreach (MarkupElement elObjectModel in tagObjectModels.Elements)
							{
								MarkupTagElement tagObjectModel = (elObjectModel as MarkupTagElement);
								if (tagObjectModel == null) continue;
								if (tagObjectModel.FullName != "ObjectModel") continue;

								MarkupAttribute attTypeName = tagObjectModel.Attributes["TypeName"];
								MarkupAttribute attID = tagObjectModel.Attributes["ID"];

								ObjectModelReference omr = null;
								if (attTypeName != null)
								{
									omr = ObjectModelReference.FromTypeName(attTypeName.Value);
								}
								else if (attID != null)
								{
									omr = ObjectModelReference.FromGUID(new Guid(attID.Value));
								}

								if (omr != null)
								{
									association.ObjectModels.Add(omr);
								}
							}
						}

						MarkupTagElement tagDataFormats = (tagAssociation.Elements["DataFormats"] as MarkupTagElement);
						if (tagDataFormats != null)
						{
							foreach (MarkupElement elDataFormat in tagDataFormats.Elements)
							{
								MarkupTagElement tagDataFormat = (elDataFormat as MarkupTagElement);
								if (tagDataFormat == null) continue;
								if (tagDataFormat.FullName != "DataFormat") continue;

								MarkupAttribute attTypeName = tagDataFormat.Attributes["TypeName"];
								MarkupAttribute attID = tagDataFormat.Attributes["ID"];

								DataFormatReference dfr = null;
								if (attTypeName != null)
								{
									dfr = DataFormatReference.FromTypeName(attTypeName.Value);
								}
								else if (attID != null)
								{
									dfr = DataFormatReference.FromGUID(new Guid(attID.Value));
								}

								if (dfr != null)
								{
									association.DataFormats.Add(dfr);
								}
								else
								{
									if (attTypeName != null)
									{
										// Console.WriteLine("DataFormat could not be associated: " + attTypeName.Value);
									}
									else if (attID != null)
									{
										// Console.WriteLine("DataFormat could not be associated: " + attID.Value);
									}
								}
							}
						}

						MarkupTagElement tagFilters = (tagAssociation.Elements["Filters"] as MarkupTagElement);
						if (tagFilters != null)
						{
							foreach (MarkupElement elFilter in tagFilters.Elements)
							{
								MarkupTagElement tagFilter = (elFilter as MarkupTagElement);
								if (tagFilter == null) continue;
								if (tagFilter.FullName != "Filter") continue;

								DataFormatFilter filter = new DataFormatFilter();
								MarkupAttribute attTitle = tagFilter.Attributes["Title"];
								if (attTitle != null)
								{
									filter.Title = attTitle.Value;
								}
								MarkupAttribute attContentType = tagFilter.Attributes["ContentType"];
								if (attContentType != null)
								{
									filter.ContentType = attContentType.Value;
								}
								MarkupAttribute attPerceivedType = tagFilter.Attributes["PerceivedType"];
								if (attPerceivedType != null)
								{
									filter.PerceivedType = attPerceivedType.Value;
								}

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
										case "never":
										{
											filter.HintComparison = DataFormatHintComparison.Never;
											break;
										}
										case "both":
										{
											filter.HintComparison = DataFormatHintComparison.Both;
											break;
										}
										default:
										{
											filter.HintComparison = DataFormatHintComparison.Unspecified;
											break;
										}
									}
								}

								MarkupTagElement tagFileNameFilters = (tagFilter.Elements["FileNameFilters"] as MarkupTagElement);
								if (tagFileNameFilters != null)
								{
									foreach (MarkupElement elFileNameFilter in tagFileNameFilters.Elements)
									{
										MarkupTagElement tagFileNameFilter = (elFileNameFilter as MarkupTagElement);
										if (tagFileNameFilter == null) continue;
										if (tagFileNameFilter.FullName != "FileNameFilter") continue;

										filter.FileNameFilters.Add(tagFileNameFilter.Value);
									}
								}

								MarkupTagElement tagMagicByteSequences = (tagFilter.Elements["MagicByteSequences"] as MarkupTagElement);
								if (tagMagicByteSequences != null)
								{
									foreach (MarkupElement elMagicByteSequence in tagMagicByteSequences.Elements)
									{
										MarkupTagElement tagMagicByteSequence = (elMagicByteSequence as MarkupTagElement);
										if (tagMagicByteSequence == null) continue;
										if (tagMagicByteSequence.FullName != "MagicByteSequence") continue;

										List<byte?> magicByteSequence = new List<byte?>();

										foreach (MarkupElement elMagicByte in tagMagicByteSequence.Elements)
										{
											MarkupTagElement tagMagicByte = (elMagicByte as MarkupTagElement);
											if (tagMagicByte == null) continue;
											if (tagMagicByte.FullName != "MagicByte") continue;

											MarkupAttribute attType = tagMagicByte.Attributes["Type"];
											if (attType == null) continue;

											switch (attType.Value.ToLower())
											{
												case "blank":
												{
													MarkupAttribute attLength = tagMagicByte.Attributes["Length"];
													if (attLength == null) continue;

													int iLength = 0;
													if (!Int32.TryParse(attLength.Value, out iLength)) continue;

													for (int i = 0; i < iLength; i++)
													{
														magicByteSequence.Add(null);
													}
													break;
												}
												case "string":
												{
													string value = tagMagicByte.Value;
													for (int i = 0; i < value.Length; i++)
													{
														magicByteSequence.Add((byte)value[i]);
													}
													break;
												}
												case "byte":
												{
													string value = tagMagicByte.Value.ToLower();
													byte realvalue = 0;
													if (value.StartsWith("0x") || value.StartsWith("&h"))
													{
														value = value.Substring(2);
														realvalue = Byte.Parse(value, System.Globalization.NumberStyles.HexNumber);
													}
													else
													{
														realvalue = Byte.Parse(value);
													}
													for (int i = 0; i < value.Length; i++)
													{
														magicByteSequence.Add(realvalue);
													}
													break;
												}
												case "hexstring":
												// case "hexadecimal": // I don't know which one is correct
												// okay, apparently it's "hexstring"
												{
													string value = tagMagicByte.Value.ToLower();
													byte realvalue = 0;
													for (int i = 0; i < value.Length; i += 2)
													{
														string val = value.Substring(i, 2);
														realvalue = Byte.Parse(val, System.Globalization.NumberStyles.HexNumber);
														magicByteSequence.Add(realvalue);
													}
													break;
												}
											}
										}

										filter.MagicBytes.Add(magicByteSequence.ToArray());
									}
								}

								association.Filters.Add(filter);
							}
						}

						package.Associations.Add(association);
					}
				}
			}
			#endregion
		}

		private CustomDataFormatStructure CreateStructure(MarkupTagElement tag, Dictionary<string, object> localVariables)
		{
			MarkupAttribute attID = tag.Attributes["ID"];
			if (attID == null) return null;

			CustomDataFormatStructure struc = new CustomDataFormatStructure();
			struc.ID = new Guid(attID.Value);

			MarkupTagElement tagInformation = (tag.Elements["Information"] as MarkupTagElement);
			if (tagInformation != null)
			{
				MarkupTagElement tagTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
				if (tagTitle != null)
				{
					struc.Title = tagTitle.Value;
				}
			}

			MarkupTagElement tagFormat = (tag.Elements["Format"] as MarkupTagElement);
			for (int i = 0; i < tagFormat.Elements.Count; i++)
			{
				MarkupTagElement tagItem = (tagFormat.Elements[i] as MarkupTagElement);
				if (tagItem == null) continue;
				if (tagItem.FullName == "Field")
				{
					struc.Items.Add(CreateField(tagItem, localVariables));
				}
			}
			return struc;
		}

		private CustomOption LoadCustomOption(MarkupTagElement tag)
		{
			CustomOption co = null;

			Type[] tCustomOptions = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(CustomOption) });
			foreach (Type tCustomOption in tCustomOptions)
			{
				if (tCustomOption.Name == tag.FullName)
				{
					MarkupAttribute attID = tag.Attributes["ID"];
					if (attID == null) continue;

					MarkupAttribute attTitle = tag.Attributes["Title"];
					if (attTitle == null) continue;

					co = (CustomOption)tCustomOption.Assembly.CreateInstance(tCustomOption.FullName, false, System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.OptionalParamBinding, null, new object[] { attID.Value, attTitle.Value.Replace("_", "&") }, null, null);

					foreach (MarkupAttribute att in tag.Attributes)
					{
						if (att.Name == "ID" || att.Name == "Title")
						{
							// already initialized
							continue;
						}
						else
						{
							tCustomOption.GetProperty(att.Name).SetValue(co, att.Value, null);
						}
					}
				}
			}
			return co;
		}

		private CustomDataFormatItem CreateField(MarkupTagElement tagField, Dictionary<string, object> localVariables)
		{
			switch (tagField.Name)
			{
				case "Field":
				{
					if (tagField.Attributes["DataType"] == null) return null;

					CustomDataFormatItemField cdfif = new CustomDataFormatItemField();
					cdfif.DataType = tagField.Attributes["DataType"].Value;
					if (cdfif.DataType == "Structure")
					{
						MarkupAttribute attStructureID = tagField.Attributes["StructureID"];
						if (attStructureID != null) cdfif.StructureID = new Guid(attStructureID.Value);
					}

					MarkupAttribute attFieldID = tagField.Attributes["ID"];
					if (attFieldID != null)
					{
						cdfif.Name = attFieldID.Value;
					}
					MarkupAttribute attLength = tagField.Attributes["Length"];
					if (attLength != null)
					{
						cdfif.Length = Int32.Parse(attLength.Value);
					}

					MarkupAttribute attValue = tagField.Attributes["Value"];
					if (attValue != null)
					{
						cdfif.Value = attValue.Value;
					}

					MarkupAttribute attConditionalVariable = tagField.Attributes["Conditional-Variable"];
					if (attConditionalVariable != null)
					{
						CustomDataFormatFieldCondition cond = new CustomDataFormatFieldCondition();
						cond.Variable = attConditionalVariable.Value;

						MarkupAttribute attConditionalValue = tagField.Attributes["Conditional-Value"];
						if (attConditionalValue != null) cond.Value = attConditionalValue.Value;

						MarkupAttribute attConditionalTrueResult = tagField.Attributes["Conditional-TrueResult"];
						if (attConditionalTrueResult != null) cond.TrueResult = attConditionalTrueResult.Value;

						MarkupAttribute attConditionalFalseResult = tagField.Attributes["Conditional-FalseResult"];
						if (attConditionalFalseResult != null) cond.FalseResult = attConditionalFalseResult.Value;

						cdfif.FieldCondition = cond;
					}
					return cdfif;
				}
				case "Array":
				{
					if (tagField.Attributes["DataType"] == null) return null;

					CustomDataFormatItemArray cdfif = new CustomDataFormatItemArray();
					cdfif.DataType = tagField.Attributes["DataType"].Value;
					if (cdfif.DataType == "Structure")
					{
						MarkupAttribute attStructureID = tagField.Attributes["StructureID"];
						if (attStructureID != null) cdfif.StructureID = new Guid(attStructureID.Value);
					}

					if (tagField.Attributes["ID"] != null)
					{
						cdfif.Name = tagField.Attributes["ID"].Value;
					}
					if (tagField.Attributes["Length"] != null)
					{
						cdfif.Length = tagField.Attributes["Length"].Value;
					}
					if (tagField.Attributes["MaximumSize"] != null)
					{
						cdfif.MaximumSize = tagField.Attributes["MaximumSize"].Value;
					}
					return cdfif;
				}
				case "Loop":
				{
					CustomDataFormatItemLoop cdfif = new CustomDataFormatItemLoop();
					if (tagField.Attributes["From"] != null)
					{
						// FROM is an integer which is the initial value of the loop counter.
						cdfif.From = Expression.Parse(tagField.Attributes["From"].Value);
					}
					if (tagField.Attributes["To"] != null)
					{
						// TO is an expression which evaluates to an integer. The loop is stopped when the loop counter equals the value provided by the TO expression.
						cdfif.To = Expression.Parse(tagField.Attributes["To"].Value);
					}
					if (tagField.Attributes["Until"] != null)
					{
						// UNTIL is an expression which stops the loop when it is evaluated TRUE.
						cdfif.Until = Expression.Parse(tagField.Attributes["Until"].Value);
					}
					return cdfif;
				}
			}
			return null;
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
										if (shortcut.ObjectModelReference.TypeName != null)
										{
											tagItemShortcut.Attributes.Add("ObjectModelTypeName", shortcut.ObjectModelReference.TypeName);
										}
										if (shortcut.ObjectModelReference.ID != Guid.Empty)
										{
											tagItemShortcut.Attributes.Add("ObjectModelID", shortcut.ObjectModelReference.ID.ToString("B"));
										}
										tagItemShortcut.Attributes.Add("DocumentTemplateID", shortcut.DocumentTemplate.ID.ToString("B"));
										tagItemShortcuts.Elements.Add(tagItemShortcut);
									}
									tagProjectType.Elements.Add(tagItemShortcuts);
								}
							}
							#endregion
							#region Variables
							{
								if (projtype.Variables.Count > 0)
								{
									MarkupTagElement tagVariables = new MarkupTagElement();
									tagVariables.FullName = "Variables";

									tagProjectType.Elements.Add(tagVariables);
								}
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
