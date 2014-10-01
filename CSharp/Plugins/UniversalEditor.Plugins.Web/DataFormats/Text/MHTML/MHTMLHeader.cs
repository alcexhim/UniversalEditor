using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Text.MHTML
{
    public class MHTMLHeader
    {
        public class MHTMLHeaderCollection
            : System.Collections.ObjectModel.Collection<MHTMLHeader>
        {
            public MHTMLHeader Add(string Name, string Value)
            {
                MHTMLHeader header = new MHTMLHeader();
                header.Name = Name;
                header.Value = Value;
                base.Add(header);
                return header;
            }

            private Dictionary<string, MHTMLHeader> headersByName = new Dictionary<string, MHTMLHeader>();
            public MHTMLHeader this[string Name]
            {
                get
                {
                    if (headersByName.ContainsKey(Name))
                    {
                        return headersByName[Name];
                    }
                    return null;
                }
            }
            public bool Contains(string Name)
            {
                return headersByName.ContainsKey(Name);
            }
            public bool Remove(string Name)
            {
                MHTMLHeader header = this[Name];
                if (header != null)
                {
                    Remove(header);
                    return true;
                }
                return false;
            }

            protected override void InsertItem(int index, MHTMLHeader item)
            {
                base.InsertItem(index, item);
                headersByName.Add(item.Name, item);
            }
            protected override void RemoveItem(int index)
            {
                headersByName.Remove(this[index].Name);
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                headersByName.Clear();
                base.ClearItems();
            }

            /// <summary>
            /// Notifies the internal keyed collection of an update to the Name property.
            /// </summary>
            /// <param name="oldName"></param>
            /// <param name="newName"></param>
            internal void NotifyUpdate(string oldName, string newName)
            {
                MHTMLHeader header = headersByName[oldName];
                headersByName.Remove(oldName);
                headersByName.Add(newName, header);
            }
        }

        public MHTMLHeader()
        {
        }
        private MHTMLHeader(MHTMLHeaderCollection parent)
        {
            _parent = parent;
        }

        private MHTMLHeaderCollection _parent = null;

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { if (_parent != null) _parent.NotifyUpdate(mvarName, value); mvarName = value; } }

        private string mvarValue = String.Empty;
        public string Value { get { return mvarValue; } set { mvarValue = value; } }
    }
}
