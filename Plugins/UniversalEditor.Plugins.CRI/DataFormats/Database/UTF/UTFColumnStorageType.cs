//
//  UTFColumnStorageType.cs - CRI Middleware UTF table column storage types
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
	/// The storage type for a column in a UTF table.
	/// </summary>
	public enum UTFColumnStorageType : byte
	{
		/// <summary>
		/// Mask value for combining <see cref="UTFColumnDataType" /> with <see cref="UTFColumnStorageType" />.
		/// </summary>
		Mask = 0xf0,
		/// <summary>
		/// Data in this column is stored per row, with a single value written for each ROW in the table.
		/// </summary>
		PerRow = 0x50,
		/// <summary>
		/// Data in this column is constant regardless of row, with a single value written for each COLUMN in the table.
		/// </summary>
		Constant = 0x30,
		/// <summary>
		/// Data in this column is declared NULL for all rows in the table. No data is written for this column.
		/// </summary>
		Zero = 0x10
	}
}
