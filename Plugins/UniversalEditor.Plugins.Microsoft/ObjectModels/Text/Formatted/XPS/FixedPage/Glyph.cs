using System;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedPage
{
	public class Glyph : FixedPageItem
	{
		public Color FillColor { get; set; } = Color.Empty;
		public double FontRenderingEmSize { get; set; } = 16.0006;
		public XPSStyleSimulations StyleSimulations { get; set; } = XPSStyleSimulations.None;

		public double OriginX { get; set; } = 0.0;
		public double OriginY { get; set; } = 0.0;

		public string Text { get; set; } = String.Empty;
	}
}
