using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface
{
	public class Language
	{
		public class LanguageCollection
			: System.Collections.ObjectModel.Collection<Language>
		{
			public Language this[string ID]
			{
				get
				{
					foreach (Language language in this)
					{
						if (language.ID == ID) return language;
					}
					return null;
				}
			}
		}

		private string mvarID = String.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Dictionary<string, string> mvarCommandTitles = new Dictionary<string, string>();
		private Dictionary<string, string> mvarStringTableEntries = new Dictionary<string, string>();

		public string GetCommandTitle(string id, string defaultValue = null)
		{
			if (mvarCommandTitles.ContainsKey(id)) return mvarCommandTitles[id];
			return defaultValue;
		}
		public void SetCommandTitle(string id, string value)
		{
			mvarCommandTitles[id] = value;
		}
		public string GetStringTableEntry(string id, string defaultValue = null)
		{
			if (mvarStringTableEntries.ContainsKey(id)) return mvarStringTableEntries[id];
			return defaultValue;
		}
		public void SetStringTableEntry(string id, string value)
		{
			mvarStringTableEntries[id] = value;
		}
	}
}
