using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
    public class Style : ICloneable
    {
        public class StyleCollection
            : System.Collections.ObjectModel.Collection<Style>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }


        private string mvarFontName = String.Empty;
        public string FontName { get { return mvarFontName; } set { mvarFontName = value; } }

        private int mvarFontSize = 32;
        public int FontSize { get { return mvarFontSize; } set { mvarFontSize = value; } }

		public Color PrimaryColor { get; set; } = Color.Empty;
		public Color SecondaryColor { get; set; } = Color.Empty;
		public Color OutlineColor { get; set; } = Color.Empty;
		public Color BackgroundColor { get; set; } = Color.Empty;

		private bool mvarBold = false;
        public bool Bold { get { return mvarBold; } set { mvarBold = value; } }

        private bool mvarItalic = false;
        public bool Italic { get { return mvarItalic; } set { mvarItalic = value; } }

        private bool mvarUnderline = false;
        public bool Underline { get { return mvarUnderline; } set { mvarUnderline = value; } }

        private bool mvarStrikethrough = false;
        public bool Strikethrough { get { return mvarStrikethrough; } set { mvarStrikethrough = value; } }

        private double mvarScaleX = 1.0;
        public double ScaleX { get { return mvarScaleX; } set { mvarScaleX = value; } }

        private double mvarScaleY = 1.0;
        public double ScaleY { get { return mvarScaleY; } set { mvarScaleY = value; } }

        private int mvarSpacing = 0;
        public int Spacing { get { return mvarSpacing; } set { mvarSpacing = value; } }

        private int mvarAngle = 0;
        public int Angle { get { return mvarAngle; } set { mvarAngle = value; } }

        private int mvarBorderStyle = 1;
        public int BorderStyle { get { return mvarBorderStyle; } set { mvarBorderStyle = value; } }

        private int mvarOutlineWidth = 2;
        public int OutlineWidth { get { return mvarOutlineWidth; } set { mvarOutlineWidth = value; } }

        private int mvarShadowWidth = 2;
        public int ShadowWidth { get { return mvarShadowWidth; } set { mvarShadowWidth = value; } }

        private int mvarAlignment = 2;
        public int Alignment { get { return mvarAlignment; } set { mvarAlignment = value; } }

        private int mvarMarginLeft = 10;
        public int MarginLeft { get { return mvarMarginLeft; } set { mvarMarginLeft = value; } }

        private int mvarMarginRight = 10;
        public int MarginRight { get { return mvarMarginRight; } set { mvarMarginRight = value; } }

        private int mvarMarginVertical = 10;
        public int MarginVertical { get { return mvarMarginVertical; } set { mvarMarginVertical = value; } }

        private int mvarEncoding = 1;
        public int Encoding { get { return mvarEncoding; } set { mvarEncoding = value; } }

        public object Clone()
        {
            Style clone = new Style();
            clone.Alignment = mvarAlignment;
            clone.Angle = mvarAngle;
            clone.BackgroundColor = BackgroundColor;
            clone.Bold = mvarBold;
            clone.BorderStyle = mvarBorderStyle;
            clone.Encoding = mvarEncoding;
            clone.FontName = (mvarFontName.Clone() as string);
            clone.FontSize = mvarFontSize;
            clone.Italic = mvarItalic;
            clone.MarginLeft = mvarMarginLeft;
            clone.MarginRight = mvarMarginRight;
            clone.MarginVertical = mvarMarginVertical;
            clone.Name = (mvarName.Clone() as string);
            clone.OutlineColor = OutlineColor;
            clone.OutlineWidth = mvarOutlineWidth;
            clone.PrimaryColor = PrimaryColor;
            clone.ScaleX = mvarScaleX;
            clone.ScaleY = mvarScaleY;
            clone.SecondaryColor = SecondaryColor;
            clone.ShadowWidth = mvarShadowWidth;
            clone.Spacing = mvarSpacing;
            clone.Strikethrough = mvarStrikethrough;
            clone.Underline = mvarUnderline;
            return clone;
        }
    }
}
