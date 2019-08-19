using System;
namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
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
