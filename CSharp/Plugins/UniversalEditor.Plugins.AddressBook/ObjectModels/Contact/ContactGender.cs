using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public class ContactGender
	{
		private static ContactGender mvarMale = new ContactGender("Male", "Male");
		public static ContactGender Male { get { return Male; } }

		private static ContactGender mvarFemale = new ContactGender("Female", "Female");
		public static ContactGender Female { get { return Female; } }

		private static ContactGender mvarUnspecified = new ContactGender("Unspecified", "Unspecified");
		public static ContactGender Unspecified { get { return mvarUnspecified; } }

		private string mvarValue = String.Empty;
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public ContactGender(string value, string title)
		{
			mvarValue = value;
			mvarTitle = title;
		}

		public override string ToString()
		{
			return mvarTitle;
		}
	}
}
