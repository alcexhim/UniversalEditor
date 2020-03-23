using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Gender
	{
		public class GenderCollection
			: System.Collections.ObjectModel.Collection<Gender>
		{

		}

		private static Gender mvarMasculine = new Gender(new Guid("{0CA5B741-4DFE-4A10-A51D-A29A4503DF62}"), "Masculine", "he", "they", "his", "their", "him", "them");
		public static Gender Masculine { get { return mvarMasculine; } }

		private static Gender mvarFeminine = new Gender(new Guid("{231A45E1-5DCC-4C6C-892C-AE6F69CAAE36}"), "Feminine", "she", "they", "hers", "their", "her", "them");
		public static Gender Feminine { get { return mvarFeminine; } }

		private static Gender mvarNeutral = new Gender(new Guid("{6E90BE20-B142-4902-B560-5B3A77546B17}"), "Neutral", "it", "them", "its", "theirs", "it", "them");
		public static Gender Neutral { get { return mvarNeutral; } }

		public Gender()
		{

		}
		public Gender(Guid id, string name)
		{
			mvarID = id;
			mvarName = name;
		}
		public Gender(Guid id, string name, string singularSubject, string pluralSubject, string singularPossessive, string pluralPossessive, string singularObject, string pluralObject)
		{
			mvarID = id;
			mvarName = name;
			mvarSingularSubject = singularSubject;
			mvarSingularPossessive = singularPossessive;
			mvarSingularObject = singularObject;
			mvarPluralSubject = pluralSubject;
			mvarPluralPossessive = pluralPossessive;
			mvarPluralObject = pluralObject;
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarSingularSubject = String.Empty;
		/// <summary>
		/// He, she, it.
		/// </summary>
		public string SingularSubject { get { return mvarSingularSubject; } set { mvarSingularSubject = value; } }

		private string mvarPluralSubject = String.Empty;
		/// <summary>
		/// they
		/// </summary>
		public string PluralSubject { get { return mvarPluralSubject; } set { mvarPluralSubject = value; } }

		private string mvarSingularPossessive = String.Empty;
		/// <summary>
		/// His, hers, its.
		/// </summary>
		public string SingularPossessive { get { return mvarSingularPossessive; } set { mvarSingularPossessive = value; } }

		private string mvarPluralPossessive = String.Empty;
		/// <summary>
		/// Theirs
		/// </summary>
		public string PluralPossessive { get { return mvarPluralPossessive; } set { mvarPluralPossessive = value; } }

		private string mvarSingularObject = String.Empty;
		/// <summary>
		/// Him, her, it.
		/// </summary>
		public string SingularObject { get { return mvarSingularObject; } set { mvarSingularObject = value; } }

		private string mvarPluralObject = String.Empty;
		/// <summary>
		/// Them
		/// </summary>
		public string PluralObject { get { return mvarPluralObject; } set { mvarPluralObject = value; } }
	}
}