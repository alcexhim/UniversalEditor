using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class Character : ICloneable
    {
        public class CharacterCollection
            : System.Collections.ObjectModel.Collection<Character>
        {
        }

        private string mvarGivenName = String.Empty;
        public string GivenName { get { return mvarGivenName; } set { mvarGivenName = value; } }

        private string mvarFamilyName = String.Empty;
        public string FamilyName { get { return mvarFamilyName; } set { mvarFamilyName = value; } }

        public string FullName
        {
            get { return mvarGivenName + " " + mvarFamilyName; }
            set
            {
                if (value.Contains(" "))
                {
                    string[] dup = value.Split(new char[] { ' ' }, 2);
                    if (dup.Length > 1)
                    {
                        mvarGivenName = dup[0];
                        mvarFamilyName = dup[1];
                    }
                    else
                    {
                        mvarGivenName = dup[0];
                        mvarFamilyName = String.Empty;
                    }
                }
            }
        }

        private string mvarBaseModelFileName = String.Empty;
        public string BaseModelFileName { get { return mvarBaseModelFileName; } set { mvarBaseModelFileName = value; } }

        private Language.LanguageCollection mvarLanguages = new Language.LanguageCollection();
        public Language.LanguageCollection Languages { get { return mvarLanguages; } }

        private Costume.CostumeCollection mvarCostumes = new Costume.CostumeCollection();
        public Costume.CostumeCollection Costumes { get { return mvarCostumes; } }

        public object Clone()
        {
            Character clone = new Character();
            clone.GivenName = (mvarGivenName.Clone() as string);
            clone.FamilyName = (mvarFamilyName.Clone() as string);
            clone.BaseModelFileName = (mvarBaseModelFileName.Clone() as string);

            foreach (Costume costume in mvarCostumes)
            {
                clone.Costumes.Add(costume.Clone() as Costume);
            }
            return clone;
        }
    }
}
