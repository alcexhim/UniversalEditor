//
//  PSDImageResourceBlock.cs - internal structure representing the header of an image resource block in an Adobe Photoshop PSD image file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop.Internal
{
	/// <summary>
	/// Internal structure representing the header of an image resource block in an Adobe Photoshop PSD image file.
	/// </summary>
	/// <remarks>
	/// Image resource blocks are the basic building unit of several file formats, including Photoshop's native file format, JPEG, and TIFF. Image resources are
	/// used to store non-pixel data associated with images, such as pen tool paths. They are referred to as resource blocks because they hold data that was
	/// stored in the Macintosh's resource fork in early versions of Photoshop.
	/// </remarks>
	public struct PSDImageResourceBlock
	{
		/// <summary>
		/// Signature: '8BIM'
		/// </summary>
		public string signature;
		/// <summary>
		/// Unique identifier for the resource. <see cref="PSDKnownImageResourceBlockId" /> contains a list of resource IDs used by Photoshop.
		/// </summary>
		public short id;
		/// <summary>
		/// Name: Pascal string, padded to make the size even (a null name consists of two bytes of 0)
		/// </summary>
		public string name;
		/// <summary>
		/// Actual size of resource data that follows
		/// </summary>
		public int datalength;
		/// <summary>
		/// The resource data, described in the sections on the individual resource types. It is padded to make the size even.
		/// </summary>
		public byte[] data;
		/// <summary>
		/// Unique identifier for the resource as a <see cref="PSDKnownImageResourceBlockId" /> value.
		/// </summary>
		/// <value>The unique identifier for the resource.</value>
		public PSDKnownImageResourceBlockId KnownId { get { return (PSDKnownImageResourceBlockId)id; } set { id = (short)value; } }
	}
}
