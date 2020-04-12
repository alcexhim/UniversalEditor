//
//  PSDLayer.cs - internal structure representing a layer in an Adobe Photoshop PSD image file
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

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop.Internal
{
	/// <summary>
	/// Internal structure representing a layer in an Adobe Photoshop PSD image file.
	/// </summary>
	public struct PSDLayer
	{
		/// <summary>
		/// The rectangle bounds of the layer.
		/// </summary>
		public Rectangle Rectangle;
		/// <summary>
		/// The number of channels in the layer.
		/// </summary>
		public short ChannelCount;
		/// <summary>
		/// The blend mode of the layer.
		/// </summary>
		public PSDBlendModeKey BlendMode;
		/// <summary>
		/// The opacity of the layer.
		/// </summary>
		public byte Opacity;
		/// <summary>
		/// The clipping mode of the layer.
		/// </summary>
		public byte ClippingMode;
		/// <summary>
		/// The flags.
		/// </summary>
		public PSDLayerFlags Flags;

		/// <summary>
		/// The name of the layer.
		/// </summary>
		public string Name;
	}
}
