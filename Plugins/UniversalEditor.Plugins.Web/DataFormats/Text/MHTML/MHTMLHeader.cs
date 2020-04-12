//
//  MHTMLHeader.cs - represents an HTTP header in an MHTML document
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Text.MHTML
{
	/// <summary>
	/// Represents a HTTP header in an <see cref="MHTMLDataFormat" />.
	/// </summary>
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
