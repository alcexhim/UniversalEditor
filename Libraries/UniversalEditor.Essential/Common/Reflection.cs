//
//  Reflection.cs - common reflection methods for the Universal Editor platform
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
using System.Reflection;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.UEPackage;
using UniversalEditor.DataFormats.UEPackage;

namespace UniversalEditor.Common
{
	/// <summary>
	/// Common reflection methods for the Universal Editor platform.
	/// </summary>
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

		#region Initialization
		private static bool mvarInitialized = false;
		private static void Initialize()
		{
			if (mvarInitialized) return;

			Type[] types = MBS.Framework.Reflection.GetAvailableTypes();

			#region Initializing Object Models
			List<AccessorReference> listAccessors = new List<AccessorReference>();
			List<ObjectModelReference> listObjectModels = new List<ObjectModelReference>();
			List<DataFormatReference> listDataFormats = new List<DataFormatReference>();
			List<DocumentTemplate> listDocumentTemplates = new List<DocumentTemplate>();
			List<ProjectTemplate> listProjectTemplates = new List<ProjectTemplate>();
			List<ConverterReference> listConverters = new List<ConverterReference>();
			List<ProjectType> listProjectTypes = new List<ProjectType>();
			{
				for (int iTyp = 0; iTyp < types.Length; iTyp++)
				{
					Type type = types[iTyp];
					if (type == null) continue;
					if (mvarAvailableObjectModels == null && (type.IsSubclassOf(typeof(ObjectModel)) && !type.IsAbstract))
					{
						ObjectModel tmp = null;

						try
						{
							tmp = (type.Assembly.CreateInstance(type.FullName) as ObjectModel);
						}
						catch (TargetInvocationException ex)
						{
							Console.WriteLine("ObjectModel could not be loaded ({0}): {1}", type.FullName, ex.Message);
							continue;
						}

						try
						{
							ObjectModelReference omr = tmp.MakeReference();

							if (!listObjectModels.Contains(omr))
								listObjectModels.Add(omr);
						}
						catch (Exception ex)
						{
							Console.WriteLine("ObjectModel ({0}) was loaded, but could not be referenced: {1}", type.FullName, ex.Message);
							continue;
						}
					}
					else if (mvarAvailableAccessors == null && (type.IsSubclassOf(typeof(Accessor)) && !type.IsAbstract))
					{
						try
						{
							Accessor a = (type.Assembly.CreateInstance(type.FullName) as Accessor);
							AccessorReference ar = a.MakeReference();
							if (ar != null)
							{
								if (!listAccessors.Contains(ar))
									listAccessors.Add(ar);
							}
						}
						catch (Exception ex)
						{
							// Console.WriteLine("Accessor could not be loaded ({0}): {1}", type.FullName, ex.Message);
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
								if (!listDataFormats.Contains(dfr))
									listDataFormats.Add(dfr);
							}
						}
						catch (Exception ex)
						{
							// Console.WriteLine("DataFormat could not be loaded ({0}): {1}", type.FullName, ex.Message);
						}
					}
					else if (mvarAvailableDocumentTemplates == null && (type.IsSubclassOf(typeof(DocumentTemplate)) && !type.IsAbstract))
					{
						DocumentTemplate template = (type.Assembly.CreateInstance(type.FullName) as DocumentTemplate);
						if (template != null)
						{
							if (!listDocumentTemplates.Contains(template))
								listDocumentTemplates.Add(template);
						}
					}
					else if (mvarAvailableConverters == null && (type.IsSubclassOf(typeof(Converter)) && !type.IsAbstract))
					{
						Converter item = (type.Assembly.CreateInstance(type.FullName) as Converter);
						if (item != null)
						{
							ConverterReference cr = item.MakeReference();
							if (!listConverters.Contains(cr))
								listConverters.Add(cr);
						}
					}
				}
			}
			listObjectModels.Sort(new Comparison<ObjectModelReference>(_ObjectModelReferenceComparer));
			listDataFormats.Sort(new Comparison<DataFormatReference>(_DataFormatReferenceComparer));
			#endregion

			InitializeFromXML(ref listObjectModels, ref listDataFormats, ref listProjectTypes, ref listDocumentTemplates, ref listProjectTemplates);
			InitializeFromEmbeddedXML(ref listObjectModels, ref listDataFormats, ref listProjectTypes, ref listDocumentTemplates, ref listProjectTemplates);
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

		private static string[] EnumerateDataPaths()
		{
			// ripped from MBS.Framework.UserInterface, but should be refactored into common DLL
			string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			return new string[]
			{
				// first look in the application root directory since this will be overridden by everything else
				basePath,
				// then look in /usr/share/universal-editor or C:\ProgramData\Mike Becker's Software\Universal Editor
				String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData),
					"universal-editor"
				}),
				// then look in ~/.local/share/universal-editor or C:\Users\USERNAME\AppData\Local\Mike Becker's Software\Universal Editor
				String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
					"universal-editor"
				}),
				// then look in ~/.universal-editor or C:\Users\USERNAME\AppData\Roaming\Mike Becker's Software\Universal Editor
				String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
					"universal-editor"
				})
			};
		}

		private static void InitializeFromEmbeddedXML(ref List<ObjectModelReference> listObjectModels, ref List<DataFormatReference> listDataFormats, ref List<ProjectType> listProjectTypes, ref List<DocumentTemplate> listDocumentTemplates, ref List<ProjectTemplate> listProjectTemplates)
		{
			MBS.Framework.Reflection.ManifestResourceStream[] streams = MBS.Framework.Reflection.GetAvailableManifestResourceStreams();
			for (int j = 0; j < streams.Length; j++)
			{
				if (streams[j].Name.EndsWith(".uexml"))
				{
					StreamAccessor sa = new StreamAccessor(streams[j].Stream);
					UEPackageObjectModel mom = new UEPackageObjectModel();
					UEPackageXMLDataFormat xdf = new UEPackageXMLDataFormat();
					xdf.IncludeTemplates = false;
					ObjectModel om = mom;

					try
					{
						Document.Load(om, xdf, sa, true);
					}
					catch (InvalidDataFormatException ex)
					{
						// ignore it
					}

					for (int kAssoc = 0; kAssoc < mom.Associations.Count; kAssoc++)
					{
						Association.Register(mom.Associations[kAssoc]);
					}
				}
			}
		}

		// FIXME: refactor this into a single XML configuration file loader at the beginning of engine launch
		private static void InitializeFromXML(ref List<ObjectModelReference> listObjectModels, ref List<DataFormatReference> listDataFormats, ref List<ProjectType> listProjectTypes, ref List<DocumentTemplate> listDocumentTemplates, ref List<ProjectTemplate> listProjectTemplates)
		{
			string[] paths = EnumerateDataPaths();
			for (int iPath = 0; iPath < paths.Length; iPath++)
			{
				string path = paths[iPath];
				if (!System.IO.Directory.Exists(path))
				{
					// Console.WriteLine("skipping nonexistent directory {0}", path);
					continue;
				}

				string configurationFileNameFilter = System.Configuration.ConfigurationManager.AppSettings["UniversalEditor.Configuration.ConfigurationFileNameFilter"];
				if (configurationFileNameFilter == null) configurationFileNameFilter = "*.uexml";

				string[] XMLFileNames = null;
				XMLFileNames = System.IO.Directory.GetFiles(path, configurationFileNameFilter, System.IO.SearchOption.AllDirectories);
				for (int jFileName = 0; jFileName < XMLFileNames.Length; jFileName++)
				{
					string fileName = XMLFileNames[jFileName];
					// Console.WriteLine("loading XML configuration file: {0} ", fileName);
#if !DEBUG
					try
					{
#endif
					string basePath = System.IO.Path.GetDirectoryName(fileName);

					UEPackageObjectModel mom = new UEPackageObjectModel();
					UEPackageXMLDataFormat xdf = new UEPackageXMLDataFormat();
					xdf.IncludeTemplates = false;
					ObjectModel om = mom;

					try
					{
						Document.Load(om, xdf, new FileAccessor(fileName, false, false, false), true);
					}
					catch (InvalidDataFormatException ex)
					{
						// ignore it
					}

					foreach (ProjectType projtype in mom.ProjectTypes)
					{
						listProjectTypes.Add(projtype);
					}
#if !DEBUG
					}
					catch
					{
					}
#endif
				}

				// ensure project types are loaded before running the next pass
				mvarAvailableProjectTypes = listProjectTypes.ToArray();

				for (int jFileName = 0; jFileName < XMLFileNames.Length; jFileName++)
				{
					string fileName = XMLFileNames[jFileName];
					try
					{
						UEPackageObjectModel mom = new UEPackageObjectModel();
						UEPackageXMLDataFormat xdf = new UEPackageXMLDataFormat();
						xdf.IncludeProjectTypes = false;
						ObjectModel om = mom;

						Document.Load(om, xdf, new FileAccessor(fileName, false, false, false), true);

						for (int kTemp = 0; kTemp < mom.DocumentTemplates.Count; kTemp++)
						{
							listDocumentTemplates.Add(mom.DocumentTemplates[kTemp]);
						}
						for (int kTemp = 0; kTemp < mom.ProjectTemplates.Count; kTemp++)
						{
							listProjectTemplates.Add(mom.ProjectTemplates[kTemp]);
						}

						for (int kAssoc = 0; kAssoc < mom.Associations.Count; kAssoc++)
						{
							Association.Register(mom.Associations[kAssoc]);
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
		public static ObjectModelReference[] GetAvailableObjectModels(Accessor accessor = null)
		{
			if (mvarAvailableObjectModels == null) Initialize();
			if (accessor == null)
			{
				return mvarAvailableObjectModels;
			}
			List<ObjectModelReference> list = new List<ObjectModelReference>();
			Association[] assocs = GetAvailableAssociations(accessor);
			for (int i = 0; i < assocs.Length; i++)
			{
				for (int j = 0; j < assocs[i].ObjectModels.Count; j++)
				{
					if (list.Contains(assocs[i].ObjectModels[j]))
						continue;
					list.Add(assocs[i].ObjectModels[j]);
				}
			}
			return list.ToArray();
		}

		public static T GetAvailableObjectModel<T>(string filename) where T : ObjectModel, new()
		{
			return GetAvailableObjectModel<T>(new FileAccessor(filename));
		}
		public static T GetAvailableObjectModel<T>(Accessor accessor) where T : ObjectModel, new()
		{
			if (GetAvailableObjectModel<T>(accessor, out T objectToFill))
				return objectToFill;
			return null;
		}
		public static bool GetAvailableObjectModel<T>(string filename, out T objectToFill) where T : ObjectModel, new()
		{
			return GetAvailableObjectModel<T>(new FileAccessor(filename), out objectToFill);
		}
		public static bool GetAvailableObjectModel<T>(Accessor accessor, out T objectToFill) where T : ObjectModel, new()
		{
			Initialize();

			Association[] assocs = GetAvailableAssociations(accessor);
			if (assocs.Length == 0)
			{
				objectToFill = null;
				return false;
			}

			ObjectModel om = new T();

			for (int i = 0; i < assocs.Length; i++)
			{
				if (assocs[i].ObjectModels.Contains(om.GetType()) || assocs[i].ObjectModels.Contains(om.MakeReference().ID))
				{
					if (assocs[i].DataFormats.Count > 0)
					{
						DataFormat df = assocs[i].DataFormats[0].Create();
						Document doc = new Document(om, df, accessor);
						try
						{
							doc.InputAccessor.Open();
							doc.Load();
							doc.InputAccessor.Close();

							objectToFill = (T)doc.ObjectModel;
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
				}
			}
			objectToFill = null;
			return false;
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
		public static AccessorReference[] GetAvailableAccessors(string fileName)
		{
			AccessorReference[] accrefs = GetAvailableAccessors();
			List<AccessorReference> _list = new List<AccessorReference>();
			foreach (AccessorReference ar in accrefs)
			{
				foreach (string schema in ar.Schemas)
				{
					if (fileName.StartsWith(schema + "://"))
					{
						_list.Add(ar);
						break;
					}
				}
			}
			return _list.ToArray();
		}
		#endregion
		#region Data Formats
		private static DataFormatReference[] mvarAvailableDataFormats = null;
		public static DataFormatReference[] GetAvailableDataFormats(Accessor accessor = null)
		{
			if (mvarAvailableDataFormats == null) Initialize();
			if (accessor == null)
			{
				return mvarAvailableDataFormats;
			}
			List<DataFormatReference> list = new List<DataFormatReference>();
			Association[] assocs = GetAvailableAssociations(accessor);
			for (int i = 0; i < assocs.Length; i++)
			{
				for (int j = 0; j < assocs[i].DataFormats.Count; j++)
				{
					if (list.Contains(assocs[i].DataFormats[j]))
						continue;
					list.Add(assocs[i].DataFormats[j]);
				}
			}
			return list.ToArray();
		}

		public static Association[] GetAvailableAssociations(Accessor accessor)
		{
			bool needsOpen = false;
			if (!accessor.IsOpen)
			{
				// we need to open the accessor before we can sniff the file
				needsOpen = true;
				accessor.Open();
			}

			Association[] associations = Association.FromCriteria(new AssociationCriteria() { Accessor = accessor });
			List<Association> listAssocs = new List<Association>(associations);
			listAssocs.Sort();

			if (needsOpen)
			{
				// close the accessor since we're done with it
				accessor.Close();
			}
			return listAssocs.ToArray();
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
			AssociationCriteria ac = new AssociationCriteria() { Accessor = accessor, ObjectModel = omr };
			Association[] associations = Association.FromCriteria(ac);
			List<DataFormatReference> dfrs = new List<DataFormatReference>();
			foreach (Association assocs in associations)
			{
				for (int i = 0; i < assocs.DataFormats.Count; i++)
				{
					dfrs.Add(assocs.DataFormats[i]);
				}
			}
			return dfrs.ToArray();
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

	}
}
