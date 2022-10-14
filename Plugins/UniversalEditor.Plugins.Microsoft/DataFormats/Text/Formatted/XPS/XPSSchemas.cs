//
//  XPSSchemas.cs - provides a set of known XML Paper Specification (XPS) schemas
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

using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
	/// <summary>
	/// Provides a set of known XML Paper Specification (XPS) schemas.
	/// </summary>
	public static class XPSSchemas
	{
		private static Dictionary<XPSSchemaKey, string> _Schema = new Dictionary<XPSSchemaKey, string>();

		public static string GetSchema(XPSSchemaVersion version, XPSSchemaType type)
		{
			if (_Schema.ContainsKey(new XPSSchemaKey(version, type)))
				return _Schema[new XPSSchemaKey(version, type)];
			return null;
		}

		static XPSSchemas()
		{
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.FixedRepresentation)] = "http://schemas.microsoft.com/xps/2005/06/fixedrepresentation";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.PrintTicket)] = "http://schemas.microsoft.com/xps/2005/06/printticket";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.Thumbnail)] = "http://schemas.microsoft.com/xps/2005/06/printticket";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.FixedDocument)] = "http://schemas.microsoft.com/xps/2005/06";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.FixedDocumentSequence)] = "http://schemas.microsoft.com/xps/2005/06";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.FixedPage)] = "http://schemas.microsoft.com/xps/2005/06";

			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedRepresentation)] = "http://schemas.openxps.org/oxps/v1.0/fixedrepresentation";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.PrintTicket)] = "http://schemas.openxps.org/oxps/v1.0/printticket";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.Thumbnail)] = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail"; //yes this is the same as OpenXPS
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedDocument)] = "http://schemas.openxps.org/oxps/v1.0";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedDocumentSequence)] = "http://schemas.openxps.org/oxps/v1.0";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedPage)] = "http://schemas.openxps.org/oxps/v1.0";
		}
	}
}
