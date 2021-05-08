//
//  XPSSchemaKey.cs - represents a tuple of XPS schema version and XPS schema type
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

namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
	/// <summary>
	/// Represents a tuple of <see cref="XPSSchemaVersion" /> and <see cref="XPSSchemaType" />.
	/// </summary>
	public struct XPSSchemaKey
	{
		public XPSSchemaVersion Version;
		public XPSSchemaType Type;

		public XPSSchemaKey(XPSSchemaVersion version, XPSSchemaType type)
		{
			Version = version;
			Type = type;
		}
	}
}
