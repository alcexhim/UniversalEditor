//
//  ExecutableSection.cs - represents a section in an executable file
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

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Represents a section in an executable file.
	/// </summary>
	public class ExecutableSection : ICloneable
	{
		public class ExecutableSectionCollection
			: System.Collections.ObjectModel.Collection<ExecutableSection>
		{
			private Dictionary<string, ExecutableSection> sectionsByName = new Dictionary<string, ExecutableSection>();

			public ExecutableSection Add(string name, byte[] data)
			{
				ExecutableSection section = new ExecutableSection();
				section.Name = name;
				section.Data = data;
				Add(section);
				return section;
			}

			public ExecutableSection this[string name]
			{
				get
				{
					if (sectionsByName.ContainsKey(name))
					{
						return sectionsByName[name];
					}
					return null;
				}
			}

			public bool Contains(string name)
			{
				return sectionsByName.ContainsKey(name);
			}
			public bool Remove(string name)
			{
				ExecutableSection sect = this[name];
				if (sect != null)
				{
					Remove(sect);
					return true;
				}
				return false;
			}

			internal void UpdateSectionName(ExecutableSection item, string oldName)
			{
				sectionsByName.Remove(oldName);
				sectionsByName.Add(item.Name, item);
			}

			protected override void InsertItem(int index, ExecutableSection item)
			{
				base.InsertItem(index, item);
				item.mvarParent = this;
				if (!sectionsByName.ContainsKey(item.Name))
				{
					sectionsByName.Add(item.Name, item);
				}
			}
			protected override void RemoveItem(int index)
			{
				string name = this[index].Name;
				if (sectionsByName.ContainsKey(name))
				{
					sectionsByName.Remove(name);
				}
				this[index].mvarParent = null;
				base.RemoveItem(index);
			}


			public void SaveAll(string Path)
			{
				foreach (ExecutableSection sect in this)
				{
					string FileName = Path + "_" + sect.Name;
					sect.Save(FileName);
				}
			}
		}

		private ExecutableSectionCollection mvarParent = null;

		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set
			{
				string oldName = mvarName;
				mvarName = value;
				if (mvarParent != null)
				{
					mvarParent.UpdateSectionName(this, oldName);
				}
			}
		}

		public event FileSystem.DataRequestEventHandler DataRequest;

		private byte[] mvarData = null;
		public byte[] Data
		{
			get
			{
				if (mvarData == null && DataRequest != null)
				{
					FileSystem.DataRequestEventArgs e = new FileSystem.DataRequestEventArgs();
					DataRequest(this, e);

					mvarData = e.Data;
				}
				if (mvarData == null) return new byte[0];
				return mvarData;
			}
			set { mvarData = value; }
		}

		private long mvarPhysicalAddress = 0;
		public long PhysicalAddress { get { return mvarPhysicalAddress; } set { mvarPhysicalAddress = value; } }

		private long mvarVirtualAddress = 0;
		public long VirtualAddress { get { return mvarVirtualAddress; } set { mvarVirtualAddress = value; } }

		public object Clone()
		{
			ExecutableSection clone = new ExecutableSection();
			clone.Characteristics = mvarCharacteristics;
			clone.Data = (mvarData.Clone() as byte[]);
			clone.LineNumberCount = mvarLineNumberCount;
			clone.LineNumberOffset = mvarLineNumberOffset;
			clone.Name = mvarName;
			clone.PhysicalAddress = mvarPhysicalAddress;
			clone.RelocationCount = mvarRelocationCount;
			clone.RelocationOffset = mvarRelocationOffset;
			clone.VirtualAddress = mvarVirtualAddress;
			clone.VirtualSize = mvarVirtualSize;
			return clone;
		}

		public override string ToString()
		{
			return mvarName + " (" + mvarCharacteristics.ToString() + "; " + mvarData.Length + " bytes)";
		}

		private ExecutableSectionCharacteristics mvarCharacteristics = ExecutableSectionCharacteristics.None;
		public ExecutableSectionCharacteristics Characteristics { get { return mvarCharacteristics; } set { mvarCharacteristics = value; } }

		public void Load(string FileName)
		{
			mvarData = System.IO.File.ReadAllBytes(FileName);
		}
		public void Save(string FileName)
		{
			System.IO.File.WriteAllBytes(FileName, mvarData);
		}

		private ulong mvarVirtualSize = 0;
		public ulong VirtualSize { get { return mvarVirtualSize; } set { mvarVirtualSize = value; } }

		private uint mvarRelocationOffset = 0;
		public uint RelocationOffset { get { return mvarRelocationOffset; } set { mvarRelocationOffset = value; } }

		private uint mvarLineNumberOffset = 0;
		public uint LineNumberOffset { get { return mvarLineNumberOffset; } set { mvarLineNumberOffset = value; } }

		private ushort mvarRelocationCount = 0;
		public ushort RelocationCount { get { return mvarRelocationCount; } set { mvarRelocationCount = value; } }

		private ushort mvarLineNumberCount = 0;
		public ushort LineNumberCount { get { return mvarLineNumberCount; } set { mvarLineNumberCount = value; } }
	}
}
