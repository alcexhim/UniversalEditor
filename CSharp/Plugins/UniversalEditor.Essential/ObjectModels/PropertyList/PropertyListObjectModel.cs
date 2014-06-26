using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace UniversalEditor.ObjectModels.PropertyList
{
	public class PropertyListObjectModel : ObjectModel
	{
		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Property List";
			omr.Path = new string[] { "General", "Property List" };

			return omr;
		}

		private Group.GroupCollection mvarGroups = new Group.GroupCollection();
		private Property.PropertyCollection mvarProperties = new Property.PropertyCollection();
		public Group.GroupCollection Groups
		{
			get
			{
				return this.mvarGroups;
			}
		}
		public Property.PropertyCollection Properties
		{
			get
			{
				return this.mvarProperties;
			}
		}
		public int Count
		{
			get
			{
				return this.mvarProperties.Count + this.mvarGroups.Count;
			}
		}
		public override void Clear()
		{
			this.mvarGroups.Clear();
			this.mvarProperties.Clear();
		}
		public override void CopyTo(ObjectModel objectModel)
		{
			PropertyListObjectModel omb = objectModel as PropertyListObjectModel;
			if (omb != null)
			{
				foreach (Group grp in mvarGroups)
				{
					if (omb.Groups.Contains(grp.Name))
					{
						omb.Groups.Append(grp);
					}
					else
					{
						omb.Groups.Add(grp);
					}
				}
				foreach (Property prp in mvarProperties)
				{
					if (omb.Properties.Contains(prp.Name))
					{
						omb.Properties[prp.Name].Value = prp.Value;
					}
					else
					{
						omb.Properties.Add(prp.Name, prp.Value);
					}
				}
			}
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
					Property p = mvarProperties[propertyPath[0]];
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
					Group parent = this.mvarGroups[propertyPath[0]];
					for (int i = 1; i < propertyPath.Length - 1; i++)
					{
						parent = parent.Groups[propertyPath[i]];
						if (parent == null)
						{
							result = defaultValue;
							return result;
						}
					}
					if (parent == null)
					{
						result = defaultValue;
					}
					else
					{
						try
						{
							result = (T)parent.Properties[propertyPath[propertyPath.Length - 1]].Value;
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
								parent.Properties[propertyPath[propertyPath.Length - 1]].Value
							});
							result = (T)retvalobj;
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
					group = mvarGroups[names[i]];
				}
				else
				{
					group = group.Groups[names[i]];
				}
			}

			if (group == null)
			{
				return mvarProperties[names[names.Length - 1]];
			}
			else
			{
				return group.Properties[names[names.Length - 1]];
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
					Group newgroup = mvarGroups[propertyName[i]];
					if (newgroup == null) newgroup = mvarGroups.Add(propertyName[i]);
					group = newgroup;
				}
				else
				{
					Group newgroup = group.Groups[propertyName[i]];
					if (newgroup == null) newgroup = group.Groups.Add(propertyName[i]);
					group = newgroup;
				}
			}

			if (group == null)
			{
				string propName = propertyName[propertyName.Length - 1];
				Property prop = mvarProperties[propName];
				if (prop == null) prop = mvarProperties.Add(propName, defaultValue);
				return prop;
			}
			else
			{
				string propName = propertyName[propertyName.Length - 1];
				Property prop = group.Properties[propName];
				if (prop == null) prop = group.Properties.Add(propName, defaultValue);
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
