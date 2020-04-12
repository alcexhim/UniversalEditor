//
//  CoreObjectObjectModel.cs - provides an ObjectModel for manipulating data in Core Object format
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

namespace UniversalEditor.ObjectModels.CoreObject
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating data in Core Object format.
	/// </summary>
	public class CoreObjectObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Core Object";
			}
			return _omr;
		}

		private CoreObjectGroup.CoreObjectGroupCollection mvarGroups = new CoreObjectGroup.CoreObjectGroupCollection();
		public CoreObjectGroup.CoreObjectGroupCollection Groups { get { return mvarGroups; } }

		private CoreObjectProperty.CoreObjectPropertyCollection mvarProperties = new CoreObjectProperty.CoreObjectPropertyCollection();
		public CoreObjectProperty.CoreObjectPropertyCollection Properties { get { return mvarProperties; } }

		public override void Clear()
		{
			mvarGroups.Clear();
			mvarProperties.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			CoreObjectObjectModel clone = (where as CoreObjectObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (CoreObjectGroup item in mvarGroups)
			{
				clone.Groups.Add(item.Clone() as CoreObjectGroup);
			}
			foreach (CoreObjectProperty item in mvarProperties)
			{
				clone.Properties.Add(item.Clone() as CoreObjectProperty);
			}
		}
	}
}
