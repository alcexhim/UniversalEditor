//
//  AccessoryObjectModel.cs - provides an ObjectModel for manipulating an accessory attached to a 3D model
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Accessory
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating an accessory attached to a 3D model.
	/// </summary>
	public class AccessoryObjectModel : ObjectModel
	{
		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel where)
		{
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Accessory";
				_omr.Path = new string[] { "Multimedia", "3D Multimedia", "Accessory" };
				_omr.Description = "Defines a 3D model that can be attached to another 3D model that supports the attachment of accessories";
			}
			return _omr;
		}

		/// <summary>
		/// The accessories contained in this file.
		/// </summary>
		public AccessoryItem.AccessoryItemCollection Accessories { get; } = new AccessoryItem.AccessoryItemCollection();
	}
}
