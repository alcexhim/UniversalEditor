using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;
using MBS.Framework.UserInterface;
using MBS.Framework.Logic;
using MBS.Framework;

namespace UniversalEditor.UserInterface.Common
{
	public static class Reflection
	{
		private static void Initialize()
		{
			List<EditorReference> listEditors = new List<EditorReference>();

			if (mvarAvailableEditors == null)
			{
				Type[] types = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(Editor) });

				foreach (Type type in types)
				{
					if (type == null) continue;
					if (type.IsAbstract) continue;

					if (type.IsSubclassOf(typeof(Editor)))
					{
						#region Initializing Editors
						try
						{
							// TODO: see if there is a way we can MakeReference() without having to create all the UI
							// components of the IEditorImplementation
							Editor editor = (type.Assembly.CreateInstance(type.FullName) as Editor);
							listEditors.Add(editor.MakeReference());
						}
						catch (System.Reflection.TargetInvocationException ex)
						{
							Console.WriteLine("binding error while loading editor '{0}': {1}", type.FullName, ex.InnerException.Message);
							if (ex.InnerException.InnerException != null)
							{
								Console.WriteLine("^--- {0}", ex.InnerException.InnerException.Message);
								Console.WriteLine();
								Console.WriteLine(" *** STACK TRACE *** ");
								Console.WriteLine(ex.StackTrace);
								Console.WriteLine(" ******************* ");
								Console.WriteLine();
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine("error while loading editor '{0}': {1}", type.FullName, ex.Message);
						}
					}
					#endregion
				}
			}

			InitializeFromXML(ref listEditors);

			for (int i = 0; i < listEditors.Count; i++)
			{
				listEditors[i].InitializeConfiguration();
			}

			if (mvarAvailableEditors == null) mvarAvailableEditors = listEditors.ToArray();
		}

		private static void InitializeFromXML(ref List<EditorReference> listEditors)
		{
			string[] paths = ((UIApplication)Application.Instance).EnumerateDataPaths();
			foreach (string path in paths)
			{
				if (!System.IO.Directory.Exists(path))
					continue;

				string configurationFileNameFilter = System.Configuration.ConfigurationManager.AppSettings["UniversalEditor.Configuration.ConfigurationFileNameFilter"];
				if (configurationFileNameFilter == null) configurationFileNameFilter = "*.uexml";

				string[] XMLFileNames = null;
				try
				{
					XMLFileNames = System.IO.Directory.GetFiles(path, configurationFileNameFilter, System.IO.SearchOption.AllDirectories);
				}
				catch (UnauthorizedAccessException ex)
				{
					Console.WriteLine("UE: warning: access to data path {0} denied", path);
					continue;
				}

				// HACK: QUICK AND DIRTY!! make this more efficient before UE5 release
				List<Accessor> accs = new List<Accessor>();
				for (int i = 0; i < XMLFileNames.Length; i++)
				{
					accs.Add(new FileAccessor(XMLFileNames[i], false, false, false));
				}

				MBS.Framework.Reflection.ManifestResourceStream[] streams = MBS.Framework.Reflection.GetAvailableManifestResourceStreams();
				for (int j = 0; j < streams.Length; j++)
				{
					if (streams[j].Name.EndsWith(".uexml"))
					{
						StreamAccessor sa = new StreamAccessor(streams[j].Stream);
						accs.Add(sa);
					}
				}

				for (int i = 0; i < accs.Count; i++)
				{
					MarkupObjectModel mom = new MarkupObjectModel();
					XMLDataFormat xdf = new XMLDataFormat();

					try
					{
						Document.Load(mom, xdf, accs[i], true);
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
								if (er.Configuration == null)
								{
									er.Configuration = tagEditor;
								}
								else
								{
									er.Configuration.Combine(tagEditor);
								}

								MarkupAttribute attTitle = tagEditor.Attributes["Title"];
								if (attTitle != null)
								{
									er.Title = attTitle.Value;
								}

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

											Command cmd = er.Commands[id];
											if (cmd == null)
											{
												cmd = new Command(id, title != null ? title : id);
												er.Commands.Add(cmd);
											}

											MarkupTagElement tagItems = tagCommand.Elements["Items"] as MarkupTagElement;
											if (tagItems != null)
											{
												foreach (MarkupElement elItem in tagItems.Elements)
												{
													MarkupTagElement tagItem = (elItem as MarkupTagElement);
													if (tagItem == null) continue;

													CommandItem ci = CommandItemLoader.FromMarkup(tagItem);
													ci.AddToCommandBar(cmd, null);
												}
											}
										}
									}
								}

								MarkupTagElement tagCommandBindings = (tagEditor.Elements["CommandBindings"] as MarkupTagElement);
								if (tagCommandBindings != null)
								{
									foreach (MarkupElement elCommandBinding in tagCommandBindings.Elements)
									{
										CommandBinding cb = CommandBinding.FromXML(elCommandBinding as MarkupTagElement);
										if (cb == null) continue;

										(Application.Instance as UIApplication).CommandBindings.Add(cb);
									}
								}

								MarkupTagElement tagContexts = (tagEditor.Elements["Contexts"] as MarkupTagElement);
								if (tagContexts != null)
								{
									// load the contexts defined by this editor
									// these are NOT contexts that will automatically be added to the application when the editor is focused!

									// these are for advertising Editor-specific contexts that aren't loaded by the rest of the application
									// so that we can associate settings with them

									foreach (MarkupElement elContext in tagContexts.Elements)
									{
										MarkupTagElement tagContext = elContext as MarkupTagElement;
										if (tagContext == null) continue;
										if (tagContext.FullName != "Context") continue;

										MarkupAttribute attContextID = tagContext.Attributes["ID"];
										MarkupAttribute attContextName = tagContext.Attributes["Name"];
										if (attContextID == null || attContextName == null) continue;

										Context ctx = new Context(new Guid(attContextID.Value), attContextName.Value);
										er.Contexts.Add(ctx);
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

											CommandItem ci = CommandItemLoader.FromMarkup(tagItem);
											if (ci != null)
											{
												er.MenuBar.Items.Add(ci);
											}
										}
									}
								}
								MarkupTagElement tagVariables = (tagEditor.Elements["Variables"] as MarkupTagElement);
								if (tagVariables != null)
								{
									for (int j = 0; j < tagVariables.Elements.Count; j++)
									{
										MarkupTagElement tagVariable = tagVariables.Elements[j] as MarkupTagElement;
										if (tagVariable == null) continue;
										if (tagVariable.FullName != "Variable") continue;

										MarkupAttribute attName = tagVariable.Attributes["Name"];
										if (attName == null) continue;

										MarkupAttribute attValue = tagVariable.Attributes["Value"];

										Variable varr = new MBS.Framework.Logic.Variable(attName.Value);
										if (attValue != null)
											varr.Expression = Expression.Parse(attValue.Value);

										er.Variables.Add(varr);
									}
								}
							}
						}
					}
				}
			}
		}

		private static EditorReference[] mvarAvailableEditors = null;
		public static EditorReference[] GetAvailableEditors()
		{
			if (mvarAvailableEditors == null) Initialize();
			return mvarAvailableEditors;
		}

		public static bool Initialized { get { return mvarAvailableEditors != null; } }

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
				if (editor.EditorType == MBS.Framework.Reflection.FindType(typeName)) return editor;
			}
			return null;
		}
	}
}
