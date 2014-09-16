using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

namespace UniversalEditor.UserInterface.Common
{
	public static class Reflection
	{
		
		#region Option Panels
		private static IOptionPanelImplementation[] mvarAvailableOptionPanels = null;
		public static IOptionPanelImplementation[] GetAvailableOptionPanels()
		{
			if (mvarAvailableOptionPanels == null) Initialize();
			return mvarAvailableOptionPanels;
		}
		#endregion

		private static void Initialize()
		{
			System.Reflection.Assembly[] asms = UniversalEditor.Common.Reflection.GetAvailableAssemblies();

			List<EditorReference> listEditors = new List<EditorReference>();
			List<IOptionPanelImplementation> listOptionPanels = new List<IOptionPanelImplementation>();
			if (mvarAvailableEditors == null || mvarAvailableOptionPanels == null)
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
						types = ex.Types;
					}

					foreach (Type type in types)
					{
						if (type == null) continue;
						Type[] interfaces = type.GetInterfaces();
						foreach (Type typeInt in interfaces)
						{
							if (type.IsAbstract) continue;

							#region Initializing Editors
							if (typeInt == typeof(IEditorImplementation))
							{
								try
								{
									IEditorImplementation editor = (type.Assembly.CreateInstance(type.FullName) as IEditorImplementation);
									listEditors.Add(editor.MakeReference());
								}
								catch (System.Reflection.TargetInvocationException ex)
								{
									Console.WriteLine("binding error: " + ex.InnerException.Message);
								}
								break;
							}
							#endregion
							#region Initializing Option Panels
							else if (typeInt == typeof(IOptionPanelImplementation))
							{
								try
								{
									IOptionPanelImplementation editor = (type.Assembly.CreateInstance(type.FullName) as IOptionPanelImplementation);
									listOptionPanels.Add(editor);
								}
								catch (System.Reflection.TargetInvocationException ex)
								{
									throw ex.InnerException;
								}
								break;
							}
							#endregion
						}
					}
				}
			}
			#region Initializing Option Panels
			{
				if (mvarAvailableOptionPanels == null)
				{

				}
			}
			#endregion

			if (mvarAvailableEditors == null) mvarAvailableEditors = listEditors.ToArray();
			if (mvarAvailableOptionPanels == null) mvarAvailableOptionPanels = listOptionPanels.ToArray();
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
				if (editor.SupportedObjectModels.Contains(objectModelReference.ObjectModelType) || editor.SupportedObjectModels.Contains(objectModelReference.ObjectModelID))
				{
					list.Add(editor);
				}
			}
			return list.ToArray();
		}

		public static EditorReference GetAvailableEditorByID(Guid guid)
		{
			EditorReference[] editors = GetAvailableEditors();
			foreach (EditorReference editor in editors)
			{
				if (editor.ID == guid) return editor;
			}
			return null;
		}
	}
}
