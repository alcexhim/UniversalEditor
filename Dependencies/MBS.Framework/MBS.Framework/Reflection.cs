//
//  Reflection.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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

namespace MBS.Framework
{
	public class Reflection
	{
		private static Dictionary<string, Type> TypesByName = new Dictionary<string, Type>();
		public static Type FindType(string TypeName)
		{
			if (!TypesByName.ContainsKey(TypeName))
			{
				Assembly[] asms = GetAvailableAssemblies();
				bool found = false;
				for (int i = 0;  i < asms.Length;  i++)
				{
					Type[] types = null;
					try
					{
						types = asms[i].GetTypes();
					}
					catch (ReflectionTypeLoadException ex)
					{
						Console.Error.WriteLine("ReflectionTypeLoadException(" + ex.LoaderExceptions.Length.ToString() + "): " + asms[i].FullName);
						Console.Error.WriteLine(ex.Message);

						types = ex.Types;
					}
					for (int j = 0;  j < types.Length; j++)
					{
						if (types[j] == null) continue;
						if (types[j].FullName == TypeName)
						{
							TypesByName.Add(TypeName, types[j]);
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

		private static Type[] mvarAvailableTypes = null;
		public static Type[] GetAvailableTypes(Type[] inheritsFrom = null)
		{
			if (mvarAvailableTypes == null)
			{
				List<Type> types = new List<Type>();
				Assembly[] asms = GetAvailableAssemblies();
				for (int iAsm = 0; iAsm < asms.Length; iAsm++)
				{
					Assembly asm = asms[iAsm];
					Type[] types1 = null;
					try
					{
						types1 = asm.GetTypes();
					}
					catch (ReflectionTypeLoadException ex)
					{
						Console.Error.WriteLine("ReflectionTypeLoadException(" + ex.LoaderExceptions.Length.ToString() + "): " + asm.FullName);
						for (int i = 0; i < ex.LoaderExceptions.Length; i++)
						{
							Console.Error.WriteLine("\t" + ex.LoaderExceptions[i].Message);
							Console.Error.WriteLine();
						}

						types1 = ex.Types;
					}

					if (types1 == null) continue;

					for (int jTyp = 0; jTyp < types1.Length; jTyp++)
					{
						if (types1[jTyp] == null) continue;
						types.Add(types1[jTyp]);
					}
				}
				mvarAvailableTypes = types.ToArray();
			}

			if (inheritsFrom != null)
			{
				List<Type> retval = new List<Type>();
				for (int iTyp = 0; iTyp < mvarAvailableTypes.Length; iTyp++)
				{
					for (int jInh = 0; jInh < inheritsFrom.Length; jInh++)
					{
						if (mvarAvailableTypes[iTyp].IsSubclassOf(inheritsFrom[jInh])) retval.Add(mvarAvailableTypes[iTyp]);
					}
				}
				return retval.ToArray();
			}
			return mvarAvailableTypes;
		}

		public static FieldInfo GetField(Type type, string name, BindingFlags flags)
		{
			FieldInfo fi = type.GetField(name, flags);
			if (fi == null)
			{
				if ((flags & BindingFlags.FlattenHierarchy) == BindingFlags.FlattenHierarchy)
				{
					Type typObj = typeof(object);

					// this is the way FlattenHierarchy SHOULD work.. for Instance binding
					while (type.BaseType != typObj)
					{
						fi = GetField(type.BaseType, name, flags);
						if (fi != null) return fi; // we found it
					}
				}
			}
			return fi;
		}
		public static void SetField(object obj, string name, BindingFlags flags, object value)
		{
			if (obj == null) return;

			FieldInfo fi = GetField(obj.GetType(), name, flags);
			if (fi != null)
			{
				fi.SetValue(obj, value);
			}
		}
	}
}
