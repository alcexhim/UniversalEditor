using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;

namespace UniversalEditor.UserInterface.Common
{
	public static class Reflection
	{
		private static void Initialize()
		{
			System.Reflection.Assembly[] asms = UniversalEditor.Common.Reflection.GetAvailableAssemblies();

			List<EditorReference> listEditors = new List<EditorReference>();

			if (mvarAvailableEditors == null)
			{
				foreach (System.Reflection.Assembly asm in asms)
				{
					Type[] types = null;
					try
					{
						types = asm.GetTypes();
					}
					catch (System.Reflection.ReflectionTypeLoadException ex)
					{
						Console.Error.WriteLine("ReflectionTypeLoadException(" + ex.LoaderExceptions.Length.ToString() + "): " + asm.FullName);
						Console.Error.WriteLine(ex.Message);
						
						types = ex.Types;
					}

					foreach (Type type in types)
					{
						if (type == null) continue;

						if (type.IsSubclassOf(typeof(Editor)))
						{
							#region Initializing Editors
							Console.Write("loading editor '" + type.FullName + "'... ");
							
							try
							{
								// TODO: see if there is a way we can MakeReference() without having to create all the UI
								// components of the IEditorImplementation
								Editor editor = (type.Assembly.CreateInstance(type.FullName) as Editor);
								listEditors.Add(editor.MakeReference());
								
								Console.WriteLine("SUCCESS!");
							}
							catch (System.Reflection.TargetInvocationException ex)
							{
								Console.WriteLine("FAILURE!");
								
								Console.WriteLine("binding error: " + ex.InnerException.Message);
							}
							catch (Exception ex)
							{
								Console.WriteLine("FAILURE!");
								
								Console.WriteLine("error while loading editor '" + type.FullName + "': " + ex.Message);
							}
						}
						#endregion
					}
				}
			}

			InitializeFromXML(ref listEditors);

			if (mvarAvailableEditors == null) mvarAvailableEditors = listEditors.ToArray();
		}

		private static void InitializeFromXML(ref List<EditorReference> listEditors)
		{
			string[] paths = MBS.Framework.UserInterface.Application.EnumerateDataPaths();
			foreach (string path in paths)
			{
				if (!System.IO.Directory.Exists(path))
					continue;

				string configurationFileNameFilter = System.Configuration.ConfigurationManager.AppSettings["UniversalEditor.Configuration.ConfigurationFileNameFilter"];
				if (configurationFileNameFilter == null) configurationFileNameFilter = "*.uexml";

				string[] XMLFileNames = null;
				XMLFileNames = System.IO.Directory.GetFiles(path, configurationFileNameFilter, System.IO.SearchOption.AllDirectories);
				foreach (string fileName in XMLFileNames)
				{
					string basePath = System.IO.Path.GetDirectoryName(fileName);

					MarkupObjectModel mom = new MarkupObjectModel();
					XMLDataFormat xdf = new XMLDataFormat();

					try
					{
						Document.Load(mom, xdf, new FileAccessor(fileName, false, false, false), true);
					}
					catch (InvalidDataFormatException ex)
					{
						// ignore it
					}

					MarkupTagElement tagUniversalEditor = (mom.Elements["UniversalEditor"] as MarkupTagElement);
					if (tagUniversalEditor == null) continue;

					MarkupTagElement tagEditors = (tagUniversalEditor.Elements["Editors"] as MarkupTagElement);
					if (tagEditors != null)
					{
						foreach (MarkupElement elEditor in tagEditors.Elements)
						{
							MarkupTagElement tagEditor = (elEditor as MarkupTagElement);
							if (tagEditor == null) continue;
							if (tagEditor.Name != "Editor") continue;

							EditorReference er = null;

							MarkupAttribute attTypeName = tagEditor.Attributes["TypeName"];
							MarkupAttribute attID = tagEditor.Attributes["ID"];
							if (attTypeName != null)
							{
								er = GetAvailableEditorByTypeName(attTypeName.Value, listEditors);
							}
							else if (attID != null)
							{
								Guid id = new Guid(attID.Value);
								er = GetAvailableEditorByID(id, listEditors);
							}
							else
							{
								continue;
							}

							if (er != null)
							{
								er.Configuration = tagEditor;

								MarkupTagElement tagCommands = (tagEditor.Elements["Commands"] as MarkupTagElement);
								if (tagCommands != null)
								{
									foreach (MarkupElement elCommand in tagCommands.Elements)
									{
										MarkupTagElement tagCommand = (elCommand as MarkupTagElement);
										if (tagCommand != null)
										{
											string id = tagCommand.Attributes["ID"]?.Value;
											string title = tagCommand.Attributes["Title"]?.Value;

											MBS.Framework.UserInterface.Command cmd = new MBS.Framework.UserInterface.Command(id, title != null ? title : id);
											MarkupTagElement tagItems = tagCommand.Elements["Items"] as MarkupTagElement;
											if (tagItems != null)
											{
												foreach (MarkupElement elItem in tagItems.Elements)
												{
													MarkupTagElement tagItem = (elItem as MarkupTagElement);
													if (tagItem == null) continue;
													switch (tagItem.Name)
													{
														case "CommandReference":
														{
															MBS.Framework.UserInterface.CommandReferenceCommandItem crci = new MBS.Framework.UserInterface.CommandReferenceCommandItem(tagItem.Attributes["CommandID"]?.Value);
															cmd.Items.Add(crci);
															break;
														}
														case "Separator":
														{
															cmd.Items.Add(new MBS.Framework.UserInterface.SeparatorCommandItem());
															break;
														}
													}
												}
											}
											er.Commands.Add(cmd);
										}
									}
								}
								MarkupTagElement tagMenuBar = (tagEditor.Elements["MenuBar"] as MarkupTagElement);
								if (tagMenuBar != null)
								{
									MarkupTagElement tagItems = tagMenuBar.Elements["Items"] as MarkupTagElement;
									if (tagItems != null)
									{
										foreach (MarkupElement elItem in tagItems.Elements)
										{
											MarkupTagElement tagItem = (elItem as MarkupTagElement);
											if (tagItem == null) continue;
											switch (tagItem.Name)
											{
												case "CommandReference":
												{
													MBS.Framework.UserInterface.CommandReferenceCommandItem crci = new MBS.Framework.UserInterface.CommandReferenceCommandItem(tagItem.Attributes["CommandID"]?.Value);
													er.MenuBar.Items.Add(crci);
													break;
												}
												case "Separator":
												{
													er.MenuBar.Items.Add(new MBS.Framework.UserInterface.SeparatorCommandItem());
													break;
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
		}

		private static Dictionary<string, Type> TypesByName = new Dictionary<string, Type>();
		private static Type FindType(string TypeName)
		{
			if (!TypesByName.ContainsKey(TypeName))
			{
				System.Reflection.Assembly[] asms = GetAvailableAssemblies();
				bool found = false;
				foreach (System.Reflection.Assembly asm in asms)
				{
					Type[] types = null;
					try
					{
						types = asm.GetTypes();
					}
					catch (System.Reflection.ReflectionTypeLoadException ex)
					{
						Console.Error.WriteLine("ReflectionTypeLoadException(" + ex.LoaderExceptions.Length.ToString() + "): " + asm.FullName);
						Console.Error.WriteLine(ex.Message);

						types = ex.Types;
					}
					foreach (Type type in types)
					{
						if (type == null) continue;
						if (type.FullName == TypeName)
						{
							TypesByName.Add(TypeName, type);
							found = true;
							break;
						}
					}
					if (found) break;
				}
				if (!found) return null;
			}
			return TypesByName[TypeName];
		}

		private static System.Reflection.Assembly[] mvarAvailableAssemblies = null;
		private static System.Reflection.Assembly[] GetAvailableAssemblies()
		{
			if (mvarAvailableAssemblies == null)
			{
				List<System.Reflection.Assembly> list = new List<System.Reflection.Assembly>();
				string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
				string[] dllfiles = System.IO.Directory.GetFiles(dir, "*.dll", System.IO.SearchOption.AllDirectories);
				// string[] exefiles = System.IO.Directory.GetFiles(dir, "*.exe", System.IO.SearchOption.AllDirectories);

				foreach (string dllfile in dllfiles)
				{
					try
					{
						System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFile(dllfile);
						list.Add(asm);
					}
					catch
					{
					}
				}
				mvarAvailableAssemblies = list.ToArray();
			}
			return mvarAvailableAssemblies;
		}


		private static EditorReference[] mvarAvailableEditors = null;
		public static EditorReference[] GetAvailableEditors()
		{
			if (mvarAvailableEditors == null) Initialize();
			return mvarAvailableEditors;
		}

		/*
		private static Dictionary<Type, IEditorImplementation[]> editorsByObjectModelType = new Dictionary<Type, IEditorImplementation[]>();
		public static IEditorImplementation[] GetAvailableEditors(ObjectModelReference objectModelReference)
		{
			if (!editorsByObjectModelType.ContainsKey(objectModelReference.ObjectModelType))
			{
				List<IEditorImplementation> list = new List<IEditorImplementation>();
				IEditorImplementation[] editors = GetAvailableEditors();
				foreach (IEditorImplementation editor in editors)
				{
					if (editor.SupportedObjectModels.Contains(objectModelReference.ObjectModelType) || editor.SupportedObjectModels.Contains(objectModelReference.ObjectModelID))
					{
						list.Add(editor);
					}
				}
				if (!editorsByObjectModelType.ContainsKey(objectModelReference.ObjectModelType))
				{
					editorsByObjectModelType.Add(objectModelReference.ObjectModelType, list.ToArray());
				}
			}
			// editorsByObjectModelType.Clear();
			return editorsByObjectModelType[objectModelReference.ObjectModelType];
		}
		*/
		
		public static EditorReference[] GetAvailableEditors(ObjectModelReference objectModelReference)
		{
			List<EditorReference> list = new List<EditorReference>();
			EditorReference[] editors = GetAvailableEditors();
			foreach (EditorReference editor in editors)
			{
				if (list.Contains (editor))
					continue;
				
				if (editor.SupportedObjectModels.Contains(objectModelReference.Type) || editor.SupportedObjectModels.Contains(objectModelReference.ID))
				{
					list.Add(editor);
				}
			}
			return list.ToArray();
		}

		public static EditorReference GetAvailableEditorByID(Guid guid, List<EditorReference> list = null)
		{
			EditorReference[] editors = null;
			if (list != null)
			{
				editors = list.ToArray();
			}
			else
			{
				editors = GetAvailableEditors();
			}
			foreach (EditorReference editor in editors)
			{
				if (editor.ID == guid) return editor;
			}
			return null;
		}
		public static EditorReference GetAvailableEditorByTypeName(string typeName, List<EditorReference> list = null)
		{
			EditorReference[] editors = null;
			if (list != null)
			{
				editors = list.ToArray();
			}
			else
			{
				editors = GetAvailableEditors();
			}
			foreach (EditorReference editor in editors)
			{
				if (editor.EditorType == FindType(typeName)) return editor;
			}
			return null;
		}
	}
}
