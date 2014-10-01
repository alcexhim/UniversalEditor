using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class PersonalNameReference : ContentItem
	{
		private PersonalName mvarValue = null;
		public PersonalName Value { get { return mvarValue; } }

		private string mvarFormat = "{GivenName}";
		public string Format { get { return mvarFormat; } set { mvarFormat = value; } }

		protected override string RenderContent()
		{
			if (mvarValue == null) throw new NullReferenceException("Value cannot be null");
			return FormatString(mvarFormat, mvarValue);
		}

		private static string FormatString(string format, PersonalName value)
		{
			string retval = format;
			retval = retval.Replace("{GivenName}", value.GivenName);
			retval = retval.Replace("{FamilyName}", value.FamilyName);
			for (int i = 0; i < value.MiddleNames.Count; i++)
			{
				retval = retval.Replace("{MiddleName:" + i.ToString() + "}", value.MiddleNames[i]);
			}
			retval = retval.Replace("{Nickname}", value.Nickname);
			return retval;
		}
	}
}
