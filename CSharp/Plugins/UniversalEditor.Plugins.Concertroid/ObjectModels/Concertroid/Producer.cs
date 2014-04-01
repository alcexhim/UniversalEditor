using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Concertroid.Library;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class Producer : ICloneable
    {
        public class ProducerCollection
            : System.Collections.ObjectModel.Collection<Producer>
        {
        }

        private static Producer[] _list = null;
        public static Producer[] GetProducers()
        {
            if (_list == null)
            {
                List<string> ConfigurationPaths = new List<string>();
                ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
            {
                System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Mike Becker's Software",
                "Concertroid"
            }));
                ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
            {
                System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Mike Becker's Software",
                "Concertroid"
            }));
                ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
            {
                System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Mike Becker's Software",
                "Concertroid"
            }));
                ConfigurationPaths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
            {
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "..",
                "Configuration"
            }));

                List<Producer> list = new List<Producer>();
                foreach (string ConfigurationPath in ConfigurationPaths)
                {
                    if (!System.IO.Directory.Exists(ConfigurationPath)) continue;

                    string[] fileNames = System.IO.Directory.GetFiles(ConfigurationPath, "*.library", System.IO.SearchOption.AllDirectories);
                    foreach (string fileName in fileNames)
                    {
                        LibraryObjectModel library = UniversalEditor.Common.Reflection.GetAvailableObjectModel<LibraryObjectModel>(fileName);
                        foreach (Producer p in library.Producers)
                        {
                            list.Add(p);
                        }
                    }
                }
                _list = list.ToArray();
            }
            return _list;
        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

        private string mvarFirstName = String.Empty;
        public string FirstName { get { return mvarFirstName; } set { mvarFirstName = value; } }

        private string mvarLastName = String.Empty;
        public string LastName { get { return mvarLastName; } set { mvarLastName = value; } }

        public object Clone()
        {
            Producer clone = new Producer();
            clone.ID = mvarID;
            clone.FirstName = (mvarFirstName.Clone() as string);
            clone.LastName = (mvarLastName.Clone() as string);
            return clone;
        }

        public override string ToString()
        {
            return mvarFirstName + " " + mvarLastName;
        }
    }
}
