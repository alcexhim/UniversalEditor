using System;
using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
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
			_Schema[new XPSSchemaKey(XPSSchemaVersion.XPS, XPSSchemaType.FixedPage)] = "http://schemas.microsoft.com/xps/2005/06";

			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedRepresentation)] = "http://schemas.openxps.org/oxps/v1.0/fixedrepresentation";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.PrintTicket)] = "http://schemas.openxps.org/oxps/v1.0/printticket";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.Thumbnail)] = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail"; //yes this is the same as OpenXPS
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedDocument)] = "http://schemas.openxps.org/oxps/v1.0";
			_Schema[new XPSSchemaKey(XPSSchemaVersion.OpenXPS, XPSSchemaType.FixedPage)] = "http://schemas.openxps.org/oxps/v1.0";
		}
	}
}
