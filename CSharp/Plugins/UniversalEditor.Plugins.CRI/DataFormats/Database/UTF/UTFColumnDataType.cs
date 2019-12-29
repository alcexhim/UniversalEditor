//
//  UTFColumnDataType.cs - CRI Middleware UTF table column data types
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace UniversalEditor.Plugins.CRI.DataFormats.Database.UTF
{
	/// <summary>
	/// The data type for a column in a UTF table.
	/// </summary>
	public enum UTFColumnDataType : byte
	{
		/// <summary>
		/// Mask value for combining <see cref="UTFColumnDataType" /> with <see cref="UTFColumnStorageType" />.
		/// </summary>
		Mask = 0x0f,
		/// <summary>
		/// The column represents a variable-length array of <see cref="System.Byte" /> data.
		/// </summary>
		Data = 0x0b,
		/// <summary>
		/// The column represents a variable-length <see cref="System.String" />.
		/// </summary>
		String = 0x0a,
		/// <summary>
		/// The column represents a <see cref="System.Single" /> value.
		/// </summary>
		Float = 0x08,
		/// <summary>
		/// The column represents a <see cref="System.Int64" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Long2 = 0x07,
		/// <summary>
		/// The column represents a <see cref="System.Int64" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Long = 0x06,
		/// <summary>
		/// The column represents a <see cref="System.Int32" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Int2 = 0x05,
		/// <summary>
		/// The column represents a <see cref="System.Int32" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Int = 0x04,
		/// <summary>
		/// The column represents a <see cref="System.Int16" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Short2 = 0x03,
		/// <summary>
		/// The column represents a <see cref="System.Int16" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Short = 0x02,
		/// <summary>
		/// The column represents a <see cref="System.Byte" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Byte2 = 0x01,
		/// <summary>
		/// The column represents a <see cref="System.Byte" /> value. There may or may not be a distinction between signed and unsigned types.
		/// </summary>
		Byte = 0x00
	}
}
