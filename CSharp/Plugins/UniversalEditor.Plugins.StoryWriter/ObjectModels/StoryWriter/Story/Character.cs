using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Character
	{
		public class CharacterCollection
			: System.Collections.ObjectModel.Collection<Character>
		{

		}

		private string mvarGivenName = String.Empty;
		public string GivenName { get { return mvarGivenName; } set { mvarGivenName = value; } }

		private string mvarMiddleName = String.Empty;
		public string MiddleName { get { return mvarMiddleName; } set { mvarMiddleName = value; } }

		private string mvarFamilyName = String.Empty;
		public string FamilyName { get { return mvarFamilyName; } set { mvarFamilyName = value; } }

		private Gender mvarGender = null;
		public Gender Gender { get { return mvarGender; } set { mvarGender = value; } }

		public string Name
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(mvarGivenName);
				if (!String.IsNullOrEmpty(mvarMiddleName))
				{
					sb.Append(" ");
					sb.Append(mvarMiddleName);
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
				mvarGivenName = String.Empty;
				mvarMiddleName = String.Empty;
				mvarFamilyName = String.Empty;

				string[] values = value.Split(new char[] { ' ' }, 3);
				if (values.Length > 0)
				{
					mvarGivenName = values[0];
					if (values.Length > 1)
					{
						if (values.Length > 2)
						{
							mvarMiddleName = values[1];
							mvarFamilyName = values[2];
						}
						else
						{
							mvarFamilyName = values[1];
						}
					}
				}
			}
		}
	}
}
