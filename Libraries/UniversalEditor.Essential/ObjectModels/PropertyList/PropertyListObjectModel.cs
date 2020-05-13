//
//  PropertyListObjectModel.cs - provides an ObjectModel for manipulating property list documents (e.g. Windows INI format)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Reflection;

namespace UniversalEditor.ObjectModels.PropertyList
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating property list documents (e.g. Windows INI format).
	/// </summary>
	public class PropertyListObjectModel : ObjectModel, IPropertyListContainer
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Property List";
			omr.Path = new string[] { "General", "Property List" };

			return omr;
		}

		public PropertyListItem.PropertyListItemCollection Items { get; set; } = new PropertyListItem.PropertyListItemCollection(null);

		public override void Clear()
		{
			Items.Clear();
		}
		public override void CopyTo(ObjectModel objectModel)
		{
			PropertyListObjectModel omb = objectModel as PropertyListObjectModel;
			if (omb == null) throw new ObjectModelNotSupportedException();

			// omb.Combine(this);
		}
		public T GetValue<T>(string propertyName)
		{
			return this.GetValue<T>(new string[]
			{
				propertyName
			});
		}
		public T GetValue<T>(string propertyPath, char propertyPathSeparator)
		{
			return this.GetValue<T>(propertyPath, new char[]
			{
				propertyPathSeparator
			});
		}
		public T GetValue<T>(string propertyPath, char[] propertyPathSeparator)
		{
			return this.GetValue<T>(propertyPath.Split(propertyPathSeparator));
		}
		public T GetValue<T>(string[] propertyPath)
		{
			return this.GetValue<T>(propertyPath, default(T));
		}
		public T GetValue<T>(string propertyName, T defaultValue)
		{
			return this.GetValue<T>(new string[]
			{
				propertyName
			}, defaultValue);
		}
		public T GetValue<T>(string propertyPath, char propertyPathSeparator, T defaultValue)
		{
			return this.GetValue<T>(propertyPath, new char[]
			{
				propertyPathSeparator
			}, defaultValue);
		}
		public T GetValue<T>(string propertyPath, char[] propertyPathSeparator, T defaultValue)
		{
			return this.GetValue<T>(propertyPath.Split(propertyPathSeparator), defaultValue);
		}
		public T GetValue<T>(string[] propertyPath, T defaultValue)
		{
			T result;
			if (propertyPath.Length == 0)
			{
				result = defaultValue;
			}
			else
			{
				if (propertyPath.Length == 1)
				{
					Property p = Items.OfType<Property>(propertyPath[0]);
					if (p != null)
					{
						result = (T)p.Value;
					}
					else
					{
						result = defaultValue;
					}
				}
				else
				{
					Group parent = Items.OfType<Group>(propertyPath[0]);
					for (int i = 1; i < propertyPath.Length - 1; i++)
					{
						if (parent == null)
						{
							result = defaultValue;
							return result;
						}
						parent = parent.Items.OfType<Group>(propertyPath[i]);
					}
					if (parent == null)
					{
						result = defaultValue;
					}
					else
					{
						Property p = parent.Items.OfType<Property>(propertyPath[propertyPath.Length - 1]);
						if (p == null || p.Value == null)
						{
							result = defaultValue;
						}
						else if (!(p.Value is T) && p.Value is string)
						{
							Type t = typeof(T);
							MethodInfo miParse = t.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
							{
								typeof(string)
							}, null);
							object retvalobj = miParse.Invoke(null, new object[]
							{
								parent.Items.OfType<Property>(propertyPath[propertyPath.Length - 1]).Value
							});
							result = (T)retvalobj;
						}
						else
						{
							try
							{
								result = (T)parent.Items.OfType<Property>(propertyPath[propertyPath.Length - 1]).Value;
							}
							catch (NullReferenceException)
							{
								result = defaultValue;
							}
							catch (InvalidCastException)
							{
								Type t = typeof(T);
								MethodInfo miParse = t.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
								{
									typeof(string)
								}, null);
								object retvalobj = miParse.Invoke(null, new object[]
								{
									parent.Items.OfType<Property>(propertyPath[propertyPath.Length - 1]).Value
								});
								result = (T)retvalobj;
							}
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Finds the property with the specified name. If more than one string is specified,
		/// searches for groups in hierarchy with the property name being the last in the list.
		/// </summary>
		/// <param name="names"></param>
		/// <returns></returns>
		public Property FindProperty(params string[] names)
		{
			Group group = null;
			for (int i = 0; i < names.Length - 1; i++)
			{
				if (group == null)
				{
					group = Items.OfType<Group>(names[i]);
				}
				else
				{
					group = group.Items.OfType<Group>(names[i]);
				}
			}

			if (group == null)
			{
				return Items.OfType<Property>(names[names.Length - 1]);
			}
			else
			{
				return group.Items.OfType<Property>(names[names.Length - 1]);
			}
		}

		/// <summary>
		/// Finds the property with the specified name. If more than one string is specified,
		/// searches for groups in hierarchy with the property name being the last in the list.
		/// If the property does not exist, it is created.
		/// </summary>
		/// <param name="names"></param>
		/// <returns></returns>
		public Property FindOrCreateProperty<T>(string[] propertyName, T defaultValue)
		{
			Group group = null;
			for (int i = 0; i < propertyName.Length - 1; i++)
			{
				if (group == null)
				{
					Group newgroup = Items.OfType<Group>(propertyName[i]);
					if (newgroup == null) newgroup = Items.AddGroup(propertyName[i]);
					group = newgroup;
				}
				else
				{
					Group newgroup = group.Items.OfType<Group>(propertyName[i]);
					if (newgroup == null) newgroup = group.Items.AddGroup(propertyName[i]);
					group = newgroup;
				}
			}

			if (group == null)
			{
				string propName = propertyName[propertyName.Length - 1];
				Property prop = Items.OfType<Property>(propName);
				if (prop == null) prop = Items.AddProperty(propName, defaultValue);
				return prop;
			}
			else
			{
				string propName = propertyName[propertyName.Length - 1];
				Property prop = group.Items.OfType<Property>(propName);
				if (prop == null) prop = group.Items.AddProperty(propName, defaultValue);
				return prop;
			}
		}

		public bool HasValue(string[] propertyName)
		{
			return (FindProperty(propertyName) != null);
		}

		public void SetValue<T>(string[] p, T value)
		{
			Property prop = FindOrCreateProperty(p, value);
			prop.Value = value;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
