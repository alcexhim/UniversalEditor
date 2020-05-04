﻿//
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
						catch (TargetInvocationException)
						{
							continue;
						}

						ObjectModelReference omr = tmp.MakeReference();

						if (!listObjectModels.Contains(omr))
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
								if (!listAccessors.Contains(ar))
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
								if (!listDataFormats.Contains(dfr))
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

		// FIXME: refactor this into a single XML configuration file loader at the beginning of engine launch
		private static void InitializeFromXML(ref List<ObjectModelReference> listObjectModels, ref List<DataFormatReference> listDataFormats, ref List<ProjectType> listProjectTypes, ref List<DocumentTemplate> listDocumentTemplates, ref List<ProjectTemplate> listProjectTemplates)
		{
			string[] paths = EnumerateDataPaths();
			for (int iPath = 0; iPath < paths.Length; iPath++)
			{
				string path = paths[iPath];
				if (!System.IO.Directory.Exists(path))
				{
					Console.WriteLine("skipping nonexistent directory {0}", path);
					continue;
				}

				string configurationFileNameFilter = System.Configuration.ConfigurationManager.AppSettings["UniversalEditor.Configuration.ConfigurationFileNameFilter"];
				if (configurationFileNameFilter == null) configurationFileNameFilter = "*.uexml";

				string[] XMLFileNames = null;
				XMLFileNames = System.IO.Directory.GetFiles(path, configurationFileNameFilter, System.IO.SearchOption.AllDirectories);
				for (int jFileName = 0; jFileName < XMLFileNames.Length; jFileName++)
				{
					string fileName = XMLFileNames[jFileName];
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

				for (int i = 0; i < dfrs.Length; i++)
				{
					DataFormatReference dfr = dfrs[i];
					try
					{
						DataFormat df = dfr.Create();
						Document.Load(om, df, accessor);
						break;
					}
					catch (InvalidDataFormatException ex)
					{
						accessor.Close();
						continue;
					}
					catch (NotImplementedException ex)
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
		public static DataFormatReference[] GetAvailableDataFormats()
		{
			if (mvarAvailableDataFormats == null) Initialize();
			return mvarAvailableDataFormats;
		}

		public static DataFormatReference[] GetAvailableDataFormats(string filename)
		{
			Association[] associations = Association.FromCriteria(new AssociationCriteria() { FileName = filename });
			List<DataFormatReference> list = new List<DataFormatReference>();
			foreach (Association association in associations)
			{
				for (int i = 0; i < association.DataFormats.Count; i++)
				{
					list.Add(association.DataFormats[i]);
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

			Association[] associations = Association.FromCriteria(new AssociationCriteria() { Accessor = accessor });
			List<DataFormatReference> list = new List<DataFormatReference>();
			foreach (Association association in associations)
			{
				for (int i = 0; i < association.DataFormats.Count; i++)
				{
					list.Add(association.DataFormats[i]);
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