using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class PersonalName : ICloneable
	{
		private string mvarGivenName = String.Empty;
		public string GivenName { get { return mvarGivenName; } set { mvarGivenName = value; } }

		private System.Collections.Specialized.StringCollection mvarMiddleNames = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection MiddleNames { get { return mvarMiddleNames; } }

		private string mvarFamilyName = String.Empty;
		public string FamilyName { get { return mvarFamilyName; } set { mvarFamilyName = value; } }

		private string mvarNickname = String.Empty;
		public string Nickname { get { return mvarNickname; } set { mvarNickname = value; } }

		public PersonalName()
		{
		}
		public PersonalName(string fullName)
		{
			Name = fullName;
		}
		public PersonalName(string givenName, string familyName) : this(givenName, String.Empty, familyName)
		{
		}
		public PersonalName(string givenName, string middleName, string familyName)
		{
			mvarGivenName = givenName;
			string[] mn = middleName.Split(new char[] { ' ' });
			foreach (string m in mn)
			{
				if (!String.IsNullOrEmpty(m)) mvarMiddleNames.Add(m);
			}
			mvarFamilyName = familyName;
		}

		public string Name
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(mvarGivenName);
				foreach (string s in mvarMiddleNames)
				{
					sb.Append(" ");
					sb.Append(s);
				}
				if (!String.IsNullOrEmpty(mvarFamilyName))
				{
					sb.Append(" ");
					sb.Append(mvarFamilyName);
				}
				return sb.ToString();
			}
			set
			{
				string[] values = value.Split(new char[] { ' ' });
				StringBuilder sb = new StringBuilder();
				if (values.Length > 0)
				{
					sb.Append(values[0]);
					if (values.Length > 2)
					{
						for (int i = 1; i < values.Length - 1; i++)
						{
							mvarMiddleNames.Add(values[i]);
						}
					}
					if (values.Length > 1)
					{
						sb.Append(" ");
						sb.Append(values[values.Length - 1]);
					}
				}
			}
		}

		public object Clone()
		{
			PersonalName clone = new PersonalName();
			clone.GivenName = (mvarGivenName.Clone() as string);
			foreach (string s in mvarMiddleNames)
			{
				clone.MiddleNames.Add(s.Clone() as string);
			}
			clone.FamilyName = (mvarFamilyName.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
