//
//  AccessoryItem.cs - describes attachment information for a 3D model attached to an existing model as an accessory item
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Accessory
{
	/// <summary>
	/// Describes attachment information for a 3D model attached to an existing model as an accessory item.
	/// </summary>
	public class AccessoryItem
	{
		/// <summary>
		/// Stores a <see cref="System.Collections.ObjectModel.Collection" /> of <see cref="AccessoryItem" /> objects.
		/// </summary>
		public class AccessoryItemCollection
			: System.Collections.ObjectModel.Collection<AccessoryItem>
		{
		}

		/// <summary>
		/// The title of this <see cref="AccessoryItem" />.
		/// </summary>
		/// <remarks>This is the string displayed in the accessory menu upon loading in MikuMikuDance, or in the Accessory Browser upon loading in Concertroid! Editor.</remarks>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// The file name of the model file to associate with this <see cref="AccessoryItem" />.
		/// </summary>
		/// <remarks>For use in MikuMikuDance, the model file must be a DirectX model (*.x file). For use in Concertroid!, may be any model file that is readable by the Universal Editor.</remarks>
		public string ModelFileName { get; set; } = String.Empty;
		/// <summary>
		/// Amount of magnification to apply to this <see cref="AccessoryItem" />.
		/// </summary>
		public PositionVector3 Scale { get; set; } = new PositionVector3(1.0, 1.0, 1.0);
		/// <summary>
		/// The position of this <see cref="AccessoryItem" /> relative to the parent bone.
		/// </summary>
		public PositionVector3 Position { get; set; } = new PositionVector3(0.0, 0.0, 0.0);
		/// <summary>
		/// The angle of rotation of this <see cref="AccessoryItem" /> relative to the parent bone.
		/// </summary>
		public PositionVector4 Rotation { get; set; } = new PositionVector4(0.0, 0.0, 0.0, 1.0);
		/// <summary>
		/// The name of the bone to which this <see cref="AccessoryItem" /> should be attached.
		/// </summary>
		/// <remarks>The <see cref="AccessoryObjectModel" /> stores the bone name in DataFormat-independent UTF-8 encoding. Certain DataFormats (like MikuMikuDance <see cref="VACDataFormat" />) will convert the string to and from Japanese Shift-JIS encoding upon saving and loading.</remarks>
		public string BoneName { get; set; } = String.Empty;
	}
}
