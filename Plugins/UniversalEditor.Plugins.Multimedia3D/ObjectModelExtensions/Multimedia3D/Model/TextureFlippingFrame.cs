//
//  TextureFlippingFrame.cs - represents an individual texture image frame used in the texture animation process
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

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
	/// <summary>
	/// Represents an individual texture image frame used in the texture animation process.
	/// </summary>
	public class TextureFlippingFrame : ICloneable
	{
		public class TextureFlippingFrameCollection : System.Collections.ObjectModel.Collection<TextureFlippingFrame>
		{
		}

		/// <summary>
		/// Gets or sets the frame index at which this frame should be presented.
		/// </summary>
		/// <value>The frame index at which this frame should be presented.</value>
		public ulong Timestamp { get; set; } = 0uL;
		/// <summary>
		/// Gets or sets the full path to the texture image file used in this frame.
		/// </summary>
		/// <value>The full path to the texture image file used in this frame.</value>
		public string FileName { get; set; } = string.Empty;

		public object Clone()
		{
			TextureFlippingFrame clone = new TextureFlippingFrame();
			clone.Timestamp = Timestamp;
			clone.FileName = (FileName.Clone() as string);
			return clone;
		}
	}
}
