using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class Musician
    {
        public class MusicianCollection
            : System.Collections.ObjectModel.Collection<Musician>
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

        private string mvarInstrument = String.Empty;
        public string Instrument { get { return mvarInstrument; } set { mvarInstrument = value; } }

        public object Clone()
        {
            Musician clone = new Musician();
            clone.GivenName = (mvarGivenName.Clone() as string);
            clone.FamilyName = (mvarFamilyName.Clone() as string);
            clone.Instrument = (mvarInstrument.Clone() as string);
            return clone;
        }
    }
}
