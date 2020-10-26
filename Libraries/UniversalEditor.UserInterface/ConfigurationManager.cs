using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework;
using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.UserInterface
{
	public enum ConfigurationManagerPropertyScope
	{
		Local,
		Global
	}
	public class ConfigurationManager
	{
		private PropertyListObjectModel mvarGlobalConfiguration = new PropertyListObjectModel();
		private PropertyListObjectModel mvarLocalConfiguration = new PropertyListObjectModel();

		public void SetValue<T>(string propertyName, T propertyValue, ConfigurationManagerPropertyScope scope = ConfigurationManagerPropertyScope.Local)
		{
			SetValue<T>(new string[] { propertyName }, propertyValue, scope);
		}
		public void SetValue<T>(string[] propertyName, T propertyValue, ConfigurationManagerPropertyScope scope = ConfigurationManagerPropertyScope.Local)
		{
			if (scope == ConfigurationManagerPropertyScope.Local)
			{
				mvarLocalConfiguration.SetValue<T>(propertyName, propertyValue);
			}
			else if (scope == ConfigurationManagerPropertyScope.Global)
			{
				mvarGlobalConfiguration.SetValue<T>(propertyName, propertyValue);
			}
		}
		public T GetValue<T>(string propertyName, T defaultValue = default(T))
		{
			return GetValue<T>(new string[] { propertyName }, defaultValue);
		}
		public T GetValue<T>(string[] propertyName, T defaultValue = default(T))
		{
			if (mvarLocalConfiguration.HasValue(propertyName))
			{
				return mvarLocalConfiguration.GetValue<T>(propertyName, defaultValue);
			}
			return mvarGlobalConfiguration.GetValue<T>(propertyName, defaultValue);
		}

		public void Load()
		{
			UniversalEditor.DataFormats.PropertyList.XML.XMLPropertyListDataFormat xdf = new DataFormats.PropertyList.XML.XMLPropertyListDataFormat();
			
			string FileName = ((UIApplication)Application.Instance).BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Configuration.xml";
			if (System.IO.File.Exists(FileName))
			{
				Document.Load(mvarLocalConfiguration, xdf, new Accessors.FileAccessor(FileName));
			}
		}

		public void Save()
		{
			UniversalEditor.DataFormats.PropertyList.XML.XMLPropertyListDataFormat xdf = new DataFormats.PropertyList.XML.XMLPropertyListDataFormat();
			string FileName = ((UIApplication)Application.Instance).BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Configuration.xml";
			string dir = System.IO.Path.GetDirectoryName (FileName);
			if (!System.IO.Directory.Exists(dir))
			{
				System.IO.Directory.CreateDirectory(dir);
			}
			Document.Save(mvarLocalConfiguration, xdf, new Accessors.FileAccessor(FileName, true, true));
		}

		public void AddProperty(Property property, ConfigurationManagerPropertyScope scope)
		{
			switch (scope)
			{
				case ConfigurationManagerPropertyScope.Global:
				{
					mvarGlobalConfiguration.Items.Add(property);
					break;
				}
				case ConfigurationManagerPropertyScope.Local:
				{
					mvarLocalConfiguration.Items.Add(property);
					break;
				}
			}
		}
		public void AddGroup(Group group, ConfigurationManagerPropertyScope scope)
		{
			switch (scope)
			{
				case ConfigurationManagerPropertyScope.Global:
				{
					mvarGlobalConfiguration.Items.Add(group);
					break;
				}
				case ConfigurationManagerPropertyScope.Local:
				{
					mvarLocalConfiguration.Items.Add(group);
					break;
				}
			}
		}
	}
}
