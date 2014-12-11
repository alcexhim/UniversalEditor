using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList.XML;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.UEPackage;
using UniversalEditor.DataFormats.UEPackage;

namespace UniversalEditor.Common
{
	public static class Reflection
	{
		private static int _ObjectModelReferenceComparer(ObjectModelReference a, ObjectModelReference b)
		{
			if (a.Type.BaseType == typeof(ObjectModel) && b.Type.BaseType != typeof(ObjectModel))
			{
				return 1;
			}
			else if (a.Type.BaseType != typeof(ObjectModel) && b.Type.BaseType == typeof(ObjectModel))
			{
				return -1;
			}
			return a.Type.FullName.CompareTo(b.Type.FullName);
		}

		private static Type[] mvarAvailableTypes = null;
		public static Type[] GetAvailableTypes()
		{
			if (mvarAvailableTypes == null)
			{
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
				mvarAvailableTypes = types;
			}
			return mvarAvailableTypes;
		}

		#region Initialization
		private static bool mvarInitialized = false;
		private static void Initialize()
		{
			if (mvarInitialized) return;

			Type[] types = GetAvailableTypes();
			
			#region Initializing Object Models
			List<AccessorReference> listAccessors = new List<AccessorReference>();
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
					else if (mvarAvailableAccessors == null && (type.IsSubclassOf(typeof(Accessor)) && !type.IsAbstract))
					{
						try
						{
							Accessor a = (type.Assembly.CreateInstance(type.FullName) as Accessor);
							AccessorReference ar = a.MakeReference();
							if (ar != null)
							{
								listAccessors.Add(ar);
							}
						}
						catch
						{
						}
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

			InitializeFromXML(ref listObjectModels, ref listDataFormats, ref listProjectTypes, ref listDocumentTemplates, ref listProjectTemplates);
			mvarInitialized = true;

			if (mvarAvailableObjectModels == null) mvarAvailableObjectModels = listObjectModels.ToArray();
			if (mvarAvailableDataFormats == null) mvarAvailableDataFormats = listDataFormats.ToArray();
			if (mvarAvailableProjectTypes == null) mvarAvailableProjectTypes = listProjectTypes.ToArray();
			if (mvarAvailableConverters == null) mvarAvailableConverters = listConverters.ToArray();

			if (mvarAvailableDocumentTemplates == null) mvarAvailableDocumentTemplates = listDocumentTemplates.ToArray();
			if (mvarAvailableProjectTemplates == null) mvarAvailableProjectTemplates = listProjectTemplates.ToArray();
			if (mvarAvailableAccessors == null) mvarAvailableAccessors = listAccessors.ToArray();
		}
		
		private static int _DataFormatReferenceComparer(DataFormatReference dfr1, DataFormatReference dfr2)
		{
			if (dfr1.Type.IsAbstract)
			{
				return 1;
			}
			if (dfr2.Type.IsAbstract)
			{
				return -1;
			}
			return dfr2.Priority.CompareTo(dfr1.Priority);
		}

		private static void InitializeFromXML(ref List<ObjectModelReference> listObjectModels, ref List<DataFormatReference> listDataFormats, ref List<ProjectType> listProjectTypes, ref List<DocumentTemplate> listDocumentTemplates, ref List<ProjectTemplate> listProjectTemplates)
		{
			System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
			paths.Add(System.Environment.CurrentDirectory);

			foreach (string path in paths)
			{
				string[] XMLFileNames = null;
				XMLFileNames = System.IO.Directory.GetFiles(path, "*.xml", System.IO.SearchOption.AllDirectories);
				foreach (string fileName in XMLFileNames)
				{
					try
					{
						string basePath = System.IO.Path.GetDirectoryName(fileName);

						UEPackageObjectModel mom = new UEPackageObjectModel();
						UEPackageXMLDataFormat xdf = new UEPackageXMLDataFormat();
						xdf.IncludeTemplates = false;
						ObjectModel om = mom;

						Document.Load(om, xdf, new FileAccessor(fileName, false, false, false), true);

						foreach (ProjectType projtype in mom.ProjectTypes)
						{
							listProjectTypes.Add(projtype);
						}
					}
					catch
					{
					}
				}

				// ensure project types are loaded before running the next pass
				mvarAvailableProjectTypes = listProjectTypes.ToArray();

				foreach (string fileName in XMLFileNames)
				{
					try
					{
						string basePath = System.IO.Path.GetDirectoryName(fileName);

						UEPackageObjectModel mom = new UEPackageObjectModel();
						UEPackageXMLDataFormat xdf = new UEPackageXMLDataFormat();
						xdf.IncludeProjectTypes = false;
						ObjectModel om = mom;

						Document.Load(om, xdf, new FileAccessor(fileName, false, false, false), true);

						foreach (DocumentTemplate template in mom.DocumentTemplates)
						{
							listDocumentTemplates.Add(template);
						}
						foreach (ProjectTemplate template in mom.ProjectTemplates)
						{
							listProjectTemplates.Add(template);
						}

						foreach (Association assoc in mom.Associations)
						{
							Association.Register(assoc);
						}
					}
					catch
					{
					}
				}
			}
		}
		#endregion

		#region Object Models
		private static ObjectModelReference[] mvarAvailableObjectModels = null;
		public static ObjectModelReference[] GetAvailableObjectModels()
		{
			if (mvarAvailableObjectModels == null) Initialize();
			return mvarAvailableObjectModels;
		}

		public static T GetAvailableObjectModel<T>(string filename) where T : ObjectModel
		{
			return GetAvailableObjectModel<T>(new FileAccessor(filename));
		}
		public static T GetAvailableObjectModel<T>(Accessor accessor) where T : ObjectModel
		{
			ObjectModelReference[] omrs = GetAvailableObjectModels(accessor);
			if (omrs.Length == 0)
			{
				// we failed to find an object model from the accessor, so let's try and
				// force the loading of the object model we're told to load in the first place

				Type type = typeof(T);
				ObjectModel om = (ObjectModel)type.Assembly.CreateInstance(type.FullName);
				ObjectModelReference omr = om.MakeReference();

				DataFormatReference[] dfrs = GetAvailableDataFormats(omr);
				
				foreach (DataFormatReference dfr in dfrs)
				{
					try
					{
						DataFormat df = dfr.Create();
						Document.Load(om, df, accessor);
					}
					catch
					{
						accessor.Close();
						continue;
					}
				}
				return (T)om;
			}
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr.Type == typeof(T))
				{
					ObjectModel om = (T)omr.Create();

					DataFormatReference[] dfrs = GetAvailableDataFormats(accessor, omr);
					foreach (DataFormatReference dfr in dfrs)
					{
						DataFormat df = dfr.Create();
						Document doc = new Document(om, df, accessor);
						try
						{
							doc.InputAccessor.Open();
							doc.Load();
							doc.InputAccessor.Close();
							break;
						}
						catch (InvalidDataFormatException ex)
						{
							if (!(Array.IndexOf(dfrs, df) < dfrs.Length - 1))
							{
								throw ex;
							}
							doc.InputAccessor.Close();
						}
					}
					return (T)om;
				}
			}
			return null;
		}
		public static bool GetAvailableObjectModel<T>(string filename, ref T objectToFill) where T : ObjectModel
		{
			return GetAvailableObjectModel<T>(new FileAccessor(filename), ref objectToFill);
		}
		public static bool GetAvailableObjectModel<T>(Accessor accessor, ref T objectToFill) where T : ObjectModel
		{
			ObjectModel om = (T)objectToFill;
			DataFormatReference[] dfrs = GetAvailableDataFormats(accessor);
			if (dfrs.Length == 0)
			{
				return false;
			}

			for (int i = 0; i < dfrs.Length; i++)
			{
				DataFormat df = dfrs[i].Create();
				Document doc = new Document(om, df, accessor);
				try
				{
					doc.InputAccessor.Open();
					doc.Load();
					doc.InputAccessor.Close();

					return true;
				}
				catch (DataFormatException)
				{
					doc.InputAccessor.Close();
				}
				catch (NotSupportedException)
				{
					doc.InputAccessor.Close();
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
				if ((dfr.Capabilities[om.Type] & capabilities) == capabilities)
				{
					list.Add(om);
				}
			}
			return list.ToArray();
		}
		public static ObjectModelReference[] GetAvailableObjectModels(Accessor accessor, DataFormatCapabilities capabilities = DataFormatCapabilities.All)
		{
			ObjectModelReference[] array = GetAvailableObjectModels();
			DataFormatReference[] dfs = GetAvailableDataFormats(accessor);
			List<ObjectModelReference> list = new List<ObjectModelReference>();
			if (dfs.Length == 0) return list.ToArray();

			foreach (ObjectModelReference om in array)
			{
				if (om == null) continue;

				foreach (DataFormatReference df in dfs)
				{
					if (df == null) continue;
					if ((df.Capabilities[om.Type] & capabilities) == capabilities)
					{
						list.Add(om);
					}
				}
			}
			return list.ToArray();
		}
		public static ObjectModelReference GetAvailableObjectModelByTypeName(string TypeName)
		{
			ObjectModelReference[] omrs = GetAvailableObjectModels();
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr.Type.FullName == TypeName)
				{
					return omr;
				}
			}
			return null;
		}
		public static ObjectModelReference GetAvailableObjectModelByID(Guid ID)
		{
			ObjectModelReference[] omrs = GetAvailableObjectModels();
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr.ID == ID)
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
		#region Accessors
		private static AccessorReference[] mvarAvailableAccessors = null;
		public static AccessorReference[] GetAvailableAccessors()
		{
			if (mvarAvailableAccessors == null) Initialize();
			return mvarAvailableAccessors;
		}
		#endregion
		#region Data Formats
		private static DataFormatReference[] mvarAvailableDataFormats = null;
		public static DataFormatReference[] GetAvailableDataFormats()
		{
			if (mvarAvailableDataFormats == null) Initialize();
			return mvarAvailableDataFormats;
		}

		public static DataFormatReference[] GetAvailableDataFormats(string filename)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				foreach (DataFormatFilter filter in df.Filters)
				{
					if (filter.MatchesFile(filename))
					{
						list.Add(df);
						break;
					}
				}
			}
			list.Sort(new Comparison<DataFormatReference>(_DataFormatReferenceComparer));
			return list.ToArray();
		}
		public static DataFormatReference[] GetAvailableDataFormats(Accessor accessor)
		{
			bool needsOpen = false;
			if (!accessor.IsOpen)
			{
				// we need to open the accessor before we can sniff the file
				needsOpen = true;
				accessor.Open();
			}

			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				foreach (DataFormatFilter filter in df.Filters)
				{
					if (filter.MatchesFile(accessor.GetFileName(), accessor))
					{
						list.Add(df);
						break;
					}
				}
			}
			list.Sort(new Comparison<DataFormatReference>(_DataFormatReferenceComparer));

			if (needsOpen)
			{
				// close the accessor since we're done with it
				accessor.Close();
			}
			return list.ToArray();
		}
		
		public static DataFormatReference[] GetAvailableDataFormats(ObjectModelReference objectModelReference)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				if (df.Capabilities[objectModelReference.Type] != DataFormatCapabilities.None)
				{
					list.Add(df);
				}
			}
			return list.ToArray();
		}
		public static DataFormatReference[] GetAvailableDataFormats(Accessor accessor, ObjectModelReference omr)
		{
			List<DataFormatReference> list = new List<DataFormatReference>();
			DataFormatReference[] dfs = GetAvailableDataFormats();
			foreach (DataFormatReference df in dfs)
			{
				if (df.Capabilities[omr.Type] != DataFormatCapabilities.None)
				{
					foreach (DataFormatFilter filter in df.Filters)
					{
						if (filter.MatchesFile(accessor))
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
				if (dfr.Type.FullName == TypeName)
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
					if (omr == null || (template.ObjectModelReference.TypeName == omr.TypeName || (template.ObjectModelReference.ID != Guid.Empty && template.ObjectModelReference.ID == omr.ID)))
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

		public static ObjectModel GetAvailableObjectModel(byte[] data, string filename, string ObjectModelTypeName)
		{
			return GetAvailableObjectModel(new MemoryAccessor(data, filename), ObjectModelTypeName);
		}
		public static ObjectModel GetAvailableObjectModel(Accessor accessor, string ObjectModelTypeName)
		{
			long resetpos = accessor.Position;

			DataFormatReference[] dfrs = GetAvailableDataFormats(accessor);
			foreach (DataFormatReference dfr in dfrs)
			{
				ObjectModelReference[] omrs = GetAvailableObjectModels(dfr);
				foreach (ObjectModelReference omr in omrs)
				{
					if (omr.Type.FullName == ObjectModelTypeName)
					{
						ObjectModel om = omr.Create();
						if (om == null) return null;

						DataFormat df = dfr.Create();
						try
						{
							Document.Load(om, df, accessor);
						}
						catch (InvalidDataFormatException)
						{
							accessor.Position = resetpos;
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
