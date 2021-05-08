//
//  PSDColorSamplerColorSpace.cs - indicates the color space for the color sampler in an Adobe Photoshop PSD image file
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop
{
	/// <summary>
	/// Indicates the color space for the color sampler in an Adobe Photoshop PSD image file.
	/// </summary>
	public enum PSDColorSamplerColorSpace
	{
		RGB = 0,
		HSB,
		CMYK,
		Pantone,
		Focoltone,
		Trumatch,
		Toyo,
		Lab,
		Gray,
		WideCMYK,
		HKS,
		DIC,
		TotalInk,
		MonitorRGB,
		Duotone,
		Opacity,
		Web,
		GrayFloat,
		RGBFloat,
		OpacityFloat
	}
}
