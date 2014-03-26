using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.Accessors;

namespace UniversalEditor.Common
{
	public static class Reflection
	{
		private static int _ObjectModelReferenceComparer(ObjectModelReference a, ObjectModelReference b)
		{
			if (a.ObjectModelType.BaseType == typeof(ObjectModel) && b.ObjectModelType.BaseType != typeof(ObjectModel))
			{
				return 1;
			}
			else if (a.ObjectModelType.BaseType != typeof(ObjectModel) && b.ObjectModelType.BaseType == typeof(ObjectModel))
			{
				return -1;
			}
			return a.ObjectModelType.FullName.CompareTo(b.ObjectModelType.FullName);
		}

		#region Initialization
		private static bool mvarInitialized = false;
		private static void Initialize()
		{
			if (mvarInitialized) return;

			Assembly[] asms = GetAvailableAssemblies();
			Type[] types = new Type[0];
			foreach (Assembly asm in asms)
			{
				Type[] types1 = null;
				try
				{
					types1 = asm.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					types1 = ex.Types;
				}

				if (types1 == null) continue;

				Array.Resize<Type>(ref types, types.Length + types1.Length);
				Array.Copy(types1, 0, types, types.Length - types1.Length, types1.Length);
			}

			#region Initializing Object Models
			List<ObjectModelReference> listObjectModels = new List<ObjectModelReference>();
			List<DataFormatReference> listDataFormats = new List<DataFormatReference>();
			List<DocumentTemplate> listDocumentTemplates = new List<DocumentTemplate>();
			List<ProjectTemplate> listProjectTemplates = new List<ProjectTemplate>();
			List<ConverterReference> listConverters = new List<ConverterReference>();
			List<ProjectType> listProjectTypes = new List<ProjectType>();
			{
				foreach (Type type in types)
				{
					if (type == null) continue;
					if (mvarAvailableObjectModels == null && (type.IsSubclassOf(typeof(ObjectModel)) && !type.IsAbstract))
					{
						ObjectModel tmp = null;

						try
						{
							tmp = (type.Assembly.CreateInstance(type.FullName) as ObjectModel);
						}
						catch (TargetInvocationException)
						{
							continue;
						}

						ObjectModelReference omr = tmp.MakeReference();

						listObjectModels.Add(omr);
					}
					else if (mvarAvailableDataFormats == null && (type.IsSubclassOf(typeof(DataFormat)) && !type.IsAbstract))
					{
						try
						{
							DataFormat df = (type.Assembly.CreateInstance(type.FullName) as DataFormat);
							DataFormatReference dfr = df.MakeReference();
							if (dfr != null)
							{
								listDataFormats.Add(dfr);
							}
						}
						catch
						{
						}
					}
					else if (mvarAvailableDocumentTemplates == null && (type.IsSubclassOf(typeof(DocumentTemplate)) && !type.IsAbstract))
					{
						DocumentTemplate template = (type.Assembly.CreateInstance(type.FullName) as DocumentTemplate);
						if (template != null)
						{
							listDocumentTemplates.Add(template);
						}
					}
					else if (mvarAvailableConverters == null && (type.IsSubclassOf(typeof(Converter)) && !type.IsAbstract))
					{
						Converter item = (type.Assembly.CreateInstance(type.FullName) as Converter);
						if (item != null)
						{
							ConverterReference cr = item.MakeReference();
							listConverters.Add(cr);
						}
					}
				}
			}
			listObjectModels.Sort(new Comparison<ObjectModelReference>(_ObjectModelReferenceComparer));
			listDataFormats.Sort(new Comparison<DataFormatReference>(_DataFormatReferenceComparer));
			#endregion

			InitializeFromXML(ref listObjectModels, ref listDataFormats, ref listProjectTypes);
			mvarInitialized = true;


			if (mvarAvailableObjectModels == null) mvarAvailableObjectModels = listObjectModels.ToArray();
			if (mvarAvailableDataFormats == null) mvarAvailableDataFormats = listDataFormats.ToArray();
			if (mvarAvailableProjectTypes == null) mvarAvailableProjectTypes = listProjectTypes.ToArray();
			if (mvarAvailableConverters == null) mvarAvailableConverters = listConverters.ToArray();

			InitializeTemplatesFromXML(ref listDocumentTemplates, ref listProjectTemplates);

			if (mvarAvailableDocumentTemplates == null) mvarAvailableDocumentTemplates = listDocumentTemplates.ToArray();
			if (mvarAvailableProjectTemplates == null) mvarAvailableProjectTemplates = listProjectTemplates.ToArray();
		}
		
		private static int _DataFormatReferenceComparer(DataFormatReference dfr1, DataFormatReference dfr2)
		{
			if (dfr1.DataFormatType.IsAbstract)
			{
				return 1;
			}
			if (dfr2.DataFormatType.IsAbstract)
			{
				return -1;
			}
			return dfr2.Priority.CompareTo(dfr1.Priority);
		}

		private static void InitializeFromXML(ref List<ObjectModelReference> listObjectModels, ref List<DataFormatReference> listDataFormats, ref List<ProjectType> listProjectTypes)
		{
			System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
			paths.Add(System.Environment.CurrentDirectory);

			foreach (string path in paths)
			{
				string[] XMLFileNames = null;
				try
				{
					XMLFileNames = System.IO.Directory.GetFiles(path, "*.xml", System.IO.SearchOption.AllDirectories);
					foreach (string fileName in XMLFileNames)
					{
						string basePath = System.IO.Path.GetDirectoryName(fileName);

						MarkupObjectModel mom = new MarkupObjectModel();
						XMLDataFormat xdf = new XMLDataFormat();
						ObjectModel om = mom;
						
						Document doc = new Document(om, xdf, new FileAccessor(fileName, false, false, false));
						doc.Accessor.Open();
						doc.Load();
						doc.Accessor.Close();

						MarkupTagElement tagUniversalEditor = (mom.Elements["UniversalEditor"] as MarkupTagElement);
						if (tagUniversalEditor == null) continue;

						#region Object Models
						{
							MarkupTagElement tagObjectModels = (tagUniversalEditor.Elements["ObjectModels"] as MarkupTagElement);
							if (tagObjectModels != null)
							{

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
															value = Strings.ReplaceVariables(value, localVariables);
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

									listDataFormats.Add(dfr);
								}
							}
						}
						#endregion
						#region Project Types
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

									listProjectTypes.Add(projtype);
								}
							}
						}
						#endregion
					}
				}
				catch
				{
				}
			}
		}
		#region Template Initialization
		private static void InitializeTemplatesFromXML(ref List<DocumentTemplate> listDocumentTemplates, ref List<ProjectTemplate> listProjectTemplates)
		{
			System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
			paths.Add(System.Environment.CurrentDirectory);

			foreach (string path in paths)
			{
				string[] XMLFileNames = null;
				try
				{
					XMLFileNames = System.IO.Directory.GetFiles(path, "*.xml", System.IO.SearchOption.AllDirectories);
					foreach (string fileName in XMLFileNames)
					{
						string basePath = System.IO.Path.GetDirectoryName(fileName);

						MarkupObjectModel mom = new MarkupObjectModel();
						XMLDataFormat xdf = new XMLDataFormat();
						ObjectModel om = mom;

						Document doc = new Document(om, xdf, new FileAccessor(fileName, false, false, false));
						doc.Accessor.Open();
						doc.Load();
						doc.Accessor.Close();

						MarkupTagElement tagUniversalEditor = (mom.Elements["UniversalEditor"] as MarkupTagElement);
						if (tagUniversalEditor == null) continue;

						#region Templates
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

										listDocumentTemplates.Add(template);
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
												template.ProjectType = GetProjectTypeByTypeID(new Guid(attTypeID.Value));
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
										#endregion

										listProjectTemplates.Add(template);
									}
								}
							}
							#endregion
						}
						#endregion
					}
				}
				catch
				{
				}
			}
		}

		private static void LoadProjectFile(MarkupTagElement tag, ObjectModels.Solution.ProjectFile.ProjectFileCollection coll)
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
		#endregion
		#endregion
		#region Object Models
		private static ObjectModelReference[] mvarAvailableObjectModels = null;
		public static ObjectModelReference[] GetAvailableObjectModels()
		{
			if (mvarAvailableObjectModels == null) Initialize();
			return mvarAvailableObjectModels;
		}
		public static ObjectModelReference[] GetAvailableObjectModels(string FileName)
		{
			return GetAvailableObjectModels(FileName, DataFormatCapabilities.All);
		}

		public static T GetAvailableObjectModel<T>(string FileName) where T : ObjectModel
		{
			ObjectModelReference[] omrs = GetAvailableObjectModels(FileName);
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr.ObjectModelType == typeof(T))
				{
					ObjectModel om = (T)omr.Create();

					DataFormatReference[] dfrs = GetAvailableDataFormats(FileName, omr);
					foreach (DataFormatReference dfr in dfrs)
					{
						DataFormat df = dfr.Create();
						Document doc = new Document(om, df, new FileAccessor(FileName, false, false, false));
						try
						{
							doc.Accessor.Open();
							doc.Load();
							doc.Accessor.Close();
							break;
						}
						catch (InvalidDataFormatException ex)
						{
							if (!(Array.IndexOf(dfrs, df) < dfrs.Length - 1))
							{
								throw ex;
							}
							doc.Accessor.Close();
						}
					}
					return (T)om;
				}
			}
			return null;
		}
		public static bool GetAvailableObjectModel<T>(string FileName, ref T objectToFill) where T : ObjectModel
		{
			ObjectModel om = (T)objectToFill;
			DataFormatReference[] dfrs = GetAvailableDataFormats(FileName);
			if (dfrs.Length == 0)
			{
				return false;
			}

			for (int i = 0; i < dfrs.Length; i++)
			{
				DataFormat df = dfrs[i].Create();
				Document doc = new Document(om, df, new FileAccessor(FileName, false, false, false));
				try
				{
					doc.Accessor.Open();
					doc.Load();
					doc.Accessor.Close();

					return true;
				}
				catch (DataFormatException)
				{
					doc.Accessor.Close();
				}
				catch (NotSupportedException)
				{
					doc.Accessor.Close();
				}
			}
			return true;
		}

		public static ObjectModelReference[] GetAvailableObjectModels(DataFormatReference dfr)
		{
			return GetAvailableObjectModels(dfr, DataFormatCapabilities.All);
		}
		public static ObjectModelReference[] GetAvailableObjectModels(DataFormatReference dfr, DataFormatCapabilities capabilities)
		{
			ObjectModelReference[] array = GetAvailableObjectModels();
			List<ObjectModelReference> list = new List<ObjectModelReference>();

			foreach (ObjectModelReference om in array)
			{
				if ((dfr.Capabilities[om.ObjectModelType] & capabilities) == capabilities)
				{
					list.Add(om);
				}
			}
			return list.ToArray();
		}
		public static ObjectModelReference[] GetAvailableObjectModels(string FileName, DataFormatCapabilities capabilities)
		{
			ObjectModelReference[] array = GetAvailableObjectModels();
			DataFormatReference[] dfs = GetAvailableDataFormats(FileName);
			List<ObjectModelReference> list = new List<ObjectModelReference>();

			foreach (ObjectModelReference om in array)
			{
				if (om == null) continue;

				foreach (DataFormatReference df in dfs)
				{
					if (df == null) continue;
					if ((df.Capabilities[om.ObjectModelType] & capabilities) == capabilities)
					{
						list.Add(om);
					}
				}
			}
			return list.ToArray();
		}
		public static ObjectModelReference GetObjectModelByTypeName(string TypeName)
		{
			ObjectModelReference[] omrs = GetAvailableObjectModels();
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr.ObjectModelType.FullName == TypeName)
				{
					return omr;
				}
			}
			return null;
		}
		#endregion
		#region Converters
		private static ConverterReference[] mvarAvailableConverters = null;
		public static ConverterReference[] GetAvailableConverters()
		{
			if (mvarAvailableConverters == null) Initialize();
			return mvarAvailableConverters;
		}
		public static ConverterReference[] GetAvailableConverters(Type from, Type to)
		{
			ConverterReference[] crs = GetAvailableConverters();
			List<ConverterReference> list = new List<ConverterReference>();
			foreach (ConverterReference cr in crs)
			{
				if (cr.Capabilities.Contains(from, to)) list.Add(cr);
			}
			return list.ToArray();
		}
		#endregion
		#region Data Formats
		private static DataFormatReference[] mvarAvailableDataFormats = null;
		public static DataFormatReference[] GetAvailableDataFormats()
		{
			if (mvarAvailableDataFormats == null) Initialize();
			return mvarAvailableDataFormats;
		}
		public static DataFormatReference[] GetAvailableDataFormats(string FileName)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				foreach (DataFormatFilter filter in df.Filters)
				{
					if (filter.MatchesFile(FileName))
					{
						list.Add(df);
						break;
					}
				}
			}
			list.Sort(new Comparison<DataFormatReference>(_DataFormatReferenceComparer));
			return list.ToArray();
		}
		
		public static DataFormatReference[] GetAvailableDataFormats(System.IO.Stream stream, string FileName = null)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				foreach (DataFormatFilter filter in df.Filters)
				{
					if (filter.MatchesFile(FileName, stream))
					{
						list.Add(df);
						break;
					}
				}
			}
			return list.ToArray();
		}
		public static DataFormatReference[] GetAvailableDataFormats(ObjectModelReference objectModelReference)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				if (df.Capabilities[objectModelReference.ObjectModelType] != DataFormatCapabilities.None)
				{
					list.Add(df);
				}
			}
			return list.ToArray();
		}
		public static DataFormatReference[] GetAvailableDataFormats(string FileName, ObjectModelReference omr)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				if (df.Capabilities[omr.ObjectModelType] != DataFormatCapabilities.None)
				{
					foreach (DataFormatFilter filter in df.Filters)
					{
						if (filter.MatchesFile(FileName))
						{
							list.Add(df);
							break;
						}
					}
				}
			}
			return list.ToArray();
		}
		public static DataFormatReference GetDataFormatByTypeName(string TypeName)
		{
			DataFormatReference[] dfrs = GetAvailableDataFormats();
			foreach (DataFormatReference dfr in dfrs)
			{
				if (dfr.DataFormatType.FullName == TypeName)
				{
					return dfr;
				}
			}
			return null;
		}
		#endregion
		#region Assemblies
		private static Assembly[] mvarAvailableAssemblies = null;
		public static Assembly[] GetAvailableAssemblies()
		{
			if (mvarAvailableAssemblies == null)
			{
				List<Assembly> list = new List<Assembly>();

				List<string> asmdirs = new List<string>();
				string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
				asmdirs.Add(dir);
				asmdirs.Add(dir + System.IO.Path.DirectorySeparatorChar.ToString() + "Plugins");

				foreach (string asmdir in asmdirs)
				{
					if (!System.IO.Directory.Exists(asmdir)) continue;

					string[] FileNamesEXE = System.IO.Directory.GetFiles(asmdir, "*.exe", System.IO.SearchOption.TopDirectoryOnly);
					string[] FileNamesDLL = System.IO.Directory.GetFiles(asmdir, "*.dll", System.IO.SearchOption.TopDirectoryOnly);

					string[] FileNames = new string[FileNamesEXE.Length + FileNamesDLL.Length];
					Array.Copy(FileNamesEXE, 0, FileNames, 0, FileNamesEXE.Length);
					Array.Copy(FileNamesDLL, 0, FileNames, FileNamesEXE.Length, FileNamesDLL.Length);

					foreach (string FileName in FileNames)
					{
						try
						{
							Assembly asm = Assembly.LoadFile(FileName);
							list.Add(asm);
						}
						catch
						{
						}
					}
				}

				mvarAvailableAssemblies = list.ToArray();
			}
			return mvarAvailableAssemblies;
		}
		#endregion

		private static DocumentTemplate[] mvarAvailableDocumentTemplates = null;
		public static DocumentTemplate[] GetAvailableDocumentTemplates()
		{
			if (mvarAvailableDocumentTemplates == null) Initialize();
			return mvarAvailableDocumentTemplates;
		}
		public static DocumentTemplate[] GetAvailableDocumentTemplates(ObjectModelReference omr)
		{
			DocumentTemplate[] templates = GetAvailableDocumentTemplates();
			List<DocumentTemplate> retval = new List<DocumentTemplate>();
			foreach (DocumentTemplate template in templates)
			{
				if (template.ObjectModelReference != null)
				{
					if (omr == null || (template.ObjectModelReference.ObjectModelTypeName == omr.ObjectModelTypeName || (template.ObjectModelReference.ObjectModelID != Guid.Empty && template.ObjectModelReference.ObjectModelID == omr.ObjectModelID)))
					{
						retval.Add(template);
					}
				}
			}
			return retval.ToArray();
		}

		private static ProjectTemplate[] mvarAvailableProjectTemplates = null;
		public static ProjectTemplate[] GetAvailableProjectTemplates()
		{
			if (mvarAvailableProjectTemplates == null) Initialize();
			return mvarAvailableProjectTemplates;
		}

		private static ProjectType[] mvarAvailableProjectTypes = null;
		public static ProjectType GetProjectTypeByTypeID(Guid guid)
		{
			if (mvarAvailableProjectTypes == null) Initialize();
			foreach (ProjectType type in mvarAvailableProjectTypes)
			{
				if (type.ID == guid) return type;
			}
			return null;
		}

#if HANDLER
		private static HandlerReference[] mvarAvailableHandlers = null;
		public static HandlerReference[] GetAvailableHandlers()
		{
			if (mvarAvailableHandlers == null) Initialize();
			return mvarAvailableHandlers;
		}
		public static HandlerReference[] GetAvailableHandlers(string FileName)
		{
			if (mvarAvailableHandlers == null) Initialize();
			List<HandlerReference> handlers = new List<HandlerReference>();
			foreach (HandlerReference hr in mvarAvailableHandlers)
			{
				if (hr is FileHandlerReference)
				{
					FileHandlerReference fh = (hr as FileHandlerReference);
					if (!fh.Supports(FileName)) continue;

					handlers.Add(fh);
				}
			}
			return handlers.ToArray();
		}
#endif
		
		public static T GetAvailableObjectModel<T>(byte[] data) where T : ObjectModel
		{
			return GetAvailableObjectModel<T>(data, null);
		}
		public static T GetAvailableObjectModel<T>(byte[] data, string FileName) where T : ObjectModel
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
			return GetAvailableObjectModel<T>(ms, FileName);
		}

		public static T GetAvailableObjectModel<T>(System.IO.Stream stream) where T : ObjectModel
		{
			return GetAvailableObjectModel<T>(stream, null);
		}
		public static T GetAvailableObjectModel<T>(System.IO.Stream stream, string FileName) where T : ObjectModel
		{
			long resetpos = stream.Position;

			DataFormatReference[] dfrs = GetAvailableDataFormats(stream, FileName);
			foreach (DataFormatReference dfr in dfrs)
			{
				ObjectModelReference[] omrs = GetAvailableObjectModels(dfr);
				foreach (ObjectModelReference omr in omrs)
				{
					if (omr.ObjectModelType == typeof(T))
					{
						T om = (omr.Create() as T);
						if (om == null) return null;

						DataFormat df = dfr.Create();
						try
						{
							Document doc = new Document(om, df, new StreamAccessor(stream));
							doc.Accessor.Open();
							doc.Load();
							doc.Accessor.Close();
						}
						catch (InvalidDataFormatException)
						{
							stream.Position = resetpos;
							break;
						}

						return om;
					}
				}
			}
			return null;
		}
		public static ObjectModel GetAvailableObjectModel(byte[] data, string FileName, string ObjectModelTypeName)
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
			return GetAvailableObjectModel(ms, FileName, ObjectModelTypeName);
		}
		public static ObjectModel GetAvailableObjectModel(System.IO.Stream stream, string FileName, string ObjectModelTypeName)
		{
			long resetpos = stream.Position;

			DataFormatReference[] dfrs = GetAvailableDataFormats(stream, FileName);
			foreach (DataFormatReference dfr in dfrs)
			{
				ObjectModelReference[] omrs = GetAvailableObjectModels(dfr);
				foreach (ObjectModelReference omr in omrs)
				{
					if (omr.ObjectModelType.FullName == ObjectModelTypeName)
					{
						ObjectModel om = omr.Create();
						if (om == null) return null;

						DataFormat df = dfr.Create();
						try
						{
							Document doc = new Document(om, df, new StreamAccessor(stream));
							doc.Accessor.Open();
							doc.Load();
							doc.Accessor.Close();
						}
						catch (InvalidDataFormatException)
						{
							stream.Position = resetpos;
							break;
						}

						return om;
					}
				}
			}
			return null;
		}
	}
}
