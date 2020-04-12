//
//  ResourceTableObjectModel.cs - provides an ObjectModel to store data for a Kronosaur TRDB resource table
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

namespace UniversalEditor.ObjectModels.Kronosaur.ResourceTable
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> to store data for a Kronosaur TRDB resource table.
	/// </summary>
	public class ResourceTableObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
			}
			return _omr;
		}

		private ResourceTableEntry.ResourceTableEntryCollection mvarEntries = new ResourceTableEntry.ResourceTableEntryCollection();
		public ResourceTableEntry.ResourceTableEntryCollection Entries { get { return mvarEntries; } }

		public override void Clear()
		{
			mvarEntries.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			ResourceTableObjectModel clone = (where as ResourceTableObjectModel);
			foreach (ResourceTableEntry item in mvarEntries)
			{
				clone.Entries.Add(item.Clone() as ResourceTableEntry);
			}
		}
	}
}
