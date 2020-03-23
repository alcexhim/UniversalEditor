using System;
namespace UniversalEditor.DataFormats.Text.Formatted.XPS
{
	public struct XPSGenerator
	{
		public string Name;
		public Version Version;

		public XPSGenerator(string name, Version version)
		{
			Name = name;
			Version = version;
		}
	}
}
