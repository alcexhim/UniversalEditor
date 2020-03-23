using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Drawing
{
    public static class Colors
    {
        static Colors()
        {
            Aqua = Color.FromRGBAByte(0x00, 0xFF, 0xFF);
            Black = Color.FromRGBAByte(0x00, 0x00, 0x00);
            Blue = Color.FromRGBAByte(0x00, 0x00, 0xFF);
            Fuchsia = Color.FromRGBAByte(0xFF, 0x00, 0xFF);
            Gray = Color.FromRGBAByte(0x80, 0x80, 0x80);
            Green = Color.FromRGBAByte(0x00, 0x80, 0x00);
            Lime = Color.FromRGBAByte(0x00, 0xFF, 0x00);
            Maroon = Color.FromRGBAByte(0x80, 0x00, 0x00);
            Navy = Color.FromRGBAByte(0x00, 0x00, 0x80);
            Olive = Color.FromRGBAByte(0x80, 0x80, 0x00);
            Purple = Color.FromRGBAByte(0x80, 0x00, 0x80);
            Red = Color.FromRGBAByte(0xFF, 0x00, 0x00);
            Silver = Color.FromRGBAByte(0xC0, 0xC0, 0xC0);
            Teal = Color.FromRGBAByte(0x00, 0x80, 0x80);
            Yellow = Color.FromRGBAByte(0xFF, 0xFF, 0x00);
            White = Color.FromRGBAByte(0xFF, 0xFF, 0xFF);
            AliceBlue = Color.FromRGBAByte(0xF0, 0xF8, 0xFF);
            AntiqueWhite = Color.FromRGBAByte(0xFA, 0xEB, 0xD7);
            Aquamarine = Color.FromRGBAByte(0x7F, 0xFF, 0xD4);
            Azure = Color.FromRGBAByte(0xF0, 0xFF, 0xFF);
            Beige = Color.FromRGBAByte(0xF5, 0xF5, 0xDC);
            Bisque = Color.FromRGBAByte(0xFF, 0xE4, 0xC4);
            BlanchedAlmond = Color.FromRGBAByte(0xFF, 0xEB, 0xCD);
            BlueViolet = Color.FromRGBAByte(0x8A, 0x2B, 0xE2);
            Brown = Color.FromRGBAByte(0xA5, 0x2A, 0x2A);
            BurlyWood = Color.FromRGBAByte(0xDE, 0xB8, 0x87);
            CadetBlue = Color.FromRGBAByte(0x5F, 0x9E, 0xA0);
            Chartreuse = Color.FromRGBAByte(0x7F, 0xFF, 0x00);
            Chocolate = Color.FromRGBAByte(0xD2, 0x69, 0x1E);
            Coral = Color.FromRGBAByte(0xFF, 0x7F, 0x50);
            CornflowerBlue = Color.FromRGBAByte(0x64, 0x95, 0xED);
            Cornsilk = Color.FromRGBAByte(0xFF, 0xF8, 0xDC);
            Crimson = Color.FromRGBAByte(0xDC, 0x14, 0x3C);
            Cyan = Color.FromRGBAByte(0x00, 0xFF, 0xFF);
            DarkBlue = Color.FromRGBAByte(0x00, 0x00, 0x8B);
            DarkCyan = Color.FromRGBAByte(0x00, 0x8B, 0x8B);
            DarkGoldenrod = Color.FromRGBAByte(0xB8, 0x86, 0x0B);
            DarkGray = Color.FromRGBAByte(0xA9, 0xA9, 0xA9);
            DarkGreen = Color.FromRGBAByte(0x00, 0x64, 0x00);
            DarkKhaki = Color.FromRGBAByte(0xBD, 0xB7, 0x6B);
            DarkMagenta = Color.FromRGBAByte(0x8B, 0x00, 0x8B);
            DarkOliveGreen = Color.FromRGBAByte(0x55, 0x6B, 0x2F);
            DarkOrange = Color.FromRGBAByte(0xFF, 0x8C, 0x00);
            DarkOrchid = Color.FromRGBAByte(0x99, 0x32, 0xCC);
            DarkRed = Color.FromRGBAByte(0x8B, 0x00, 0x00);
            DarkSalmon = Color.FromRGBAByte(0xE9, 0x96, 0x7A);
            DarkSeaGreen = Color.FromRGBAByte(0x8F, 0xBC, 0x8F);
            DarkSlateBlue = Color.FromRGBAByte(0x48, 0x3D, 0x8B);
            DarkSlateGray = Color.FromRGBAByte(0x2F, 0x4F, 0x4F);
            DarkTurquoise = Color.FromRGBAByte(0x00, 0xCE, 0xD1);

            DarkViolet = Color.FromRGBAByte(0x94, 0x00, 0xD3);
            DeepPink = Color.FromRGBAByte(0xFF, 0x14, 0x93);
            DeepSkyBlue = Color.FromRGBAByte(0x00, 0xBF, 0xFF);
            DimGray = Color.FromRGBAByte(0x69, 0x69, 0x69);
            DodgerBlue = Color.FromRGBAByte(0x1E, 0x90, 0xFF);
            Firebrick = Color.FromRGBAByte(0xB2, 0x22, 0x22);
            FloralWhite = Color.FromRGBAByte(0xFF, 0xFA, 0xF0);
            ForestGreen = Color.FromRGBAByte(0x22, 0x8B, 0x22);
            Gainsboro = Color.FromRGBAByte(0xDC, 0xDC, 0xDC);
            GhostWhite = Color.FromRGBAByte(0xF8, 0xF8, 0xFF);
            Gold = Color.FromRGBAByte(0xFF, 0xD7, 0x00);
            Goldenrod = Color.FromRGBAByte(0xDA, 0xA5, 0x20);
            GreenYellow = Color.FromRGBAByte(0xAD, 0xFF, 0x2F);
            Honeydew = Color.FromRGBAByte(0xF0, 0xFF, 0xF0);
            HotPink = Color.FromRGBAByte(0xFF, 0x69, 0xB4);
            IndianRed = Color.FromRGBAByte(0xCD, 0x5C, 0x5C);
            Indigo = Color.FromRGBAByte(0x4B, 0x00, 0x82);
            Ivory = Color.FromRGBAByte(0xFF, 0xFF, 0xF0);
            Khaki = Color.FromRGBAByte(0xF0, 0xE6, 0x8C);
            Lavender = Color.FromRGBAByte(0xE6, 0xE6, 0xFA);
            LavenderBlush = Color.FromRGBAByte(0xFF, 0xF0, 0xF5);
            LawnGreen = Color.FromRGBAByte(0x7C, 0xFC, 0x00);
            LemonChiffon = Color.FromRGBAByte(0xFF, 0xFA, 0xCD);
            LightBlue = Color.FromRGBAByte(0xAD, 0xD8, 0xE6);
            LightCoral = Color.FromRGBAByte(0xF0, 0x80, 0x80);
            LightCyan = Color.FromRGBAByte(0xE0, 0xFF, 0xFF);
            LightGoldenrodYellow = Color.FromRGBAByte(0xFA, 0xFA, 0xD2);
            LightGreen = Color.FromRGBAByte(0x90, 0xEE, 0x90);
            LightGray = Color.FromRGBAByte(0xD3, 0xD3, 0xD3);
            LightPink = Color.FromRGBAByte(0xFF, 0xB6, 0xC1);
            LightSalmon = Color.FromRGBAByte(0xFF, 0xA0, 0x7A);
            LightSeaGreen = Color.FromRGBAByte(0x20, 0xB2, 0xAA);
            LightSkyBlue = Color.FromRGBAByte(0x87, 0xCE, 0xFA);
            LightSlateGray = Color.FromRGBAByte(0x77, 0x88, 0x99);
            LightSteelBlue = Color.FromRGBAByte(0xB0, 0xC4, 0xDE);
            LightYellow = Color.FromRGBAByte(0xFF, 0xFF, 0xE0);
            LimeGreen = Color.FromRGBAByte(0x32, 0xCD, 0x32);

            Linen = Color.FromRGBAByte(0xFA, 0xF0, 0xE6);
            Magenta = Color.FromRGBAByte(0xFF, 0x00, 0xFF);
            MediumAquamarine = Color.FromRGBAByte(0x66, 0xCD, 0xAA);
            MediumBlue = Color.FromRGBAByte(0x00, 0x00, 0xCD);
            MediumOrchid = Color.FromRGBAByte(0xBA, 0x55, 0xD3);
            MediumPurple = Color.FromRGBAByte(0x93, 0x70, 0xDB);
            MediumSeaGreen = Color.FromRGBAByte(0x3C, 0xB3, 0x71);
            MediumSlateBlue = Color.FromRGBAByte(0x7B, 0x68, 0xEE);
            MediumSpringGreen = Color.FromRGBAByte(0x00, 0xFA, 0x9A);
            MediumTurquoise = Color.FromRGBAByte(0x48, 0xD1, 0xCC);
            MediumVioletRed = Color.FromRGBAByte(0xC7, 0x15, 0x85);
            MidnightBlue = Color.FromRGBAByte(0x19, 0x19, 0x70);
            MintCream = Color.FromRGBAByte(0xF5, 0xFF, 0xFA);
            MistyRose = Color.FromRGBAByte(0xFF, 0xE4, 0xE1);
            Moccasin = Color.FromRGBAByte(0xFF, 0xE4, 0xB5);
            NavajoWhite = Color.FromRGBAByte(0xFF, 0xDE, 0xAD);
            OldLace = Color.FromRGBAByte(0xFD, 0xF5, 0xE6);
            OliveDrab = Color.FromRGBAByte(0x6B, 0x8E, 0x23);
            Orange = Color.FromRGBAByte(0xFF, 0xA5, 0x00);
            OrangeRed = Color.FromRGBAByte(0xFF, 0x45, 0x00);
            Orchid = Color.FromRGBAByte(0xDA, 0x70, 0xD6);
            PaleGoldenrod = Color.FromRGBAByte(0xEE, 0xE8, 0xAA);
            PaleTurquoise = Color.FromRGBAByte(0xAF, 0xEE, 0xEE);
            PaleVioletRed = Color.FromRGBAByte(0xDB, 0x70, 0x93);
            PapayaWhip = Color.FromRGBAByte(0xFF, 0xEF, 0xD5);
            PeachPuff = Color.FromRGBAByte(0xFF, 0xDA, 0xB9);
            Peru = Color.FromRGBAByte(0xCD, 0x85, 0x3F);
            Pink = Color.FromRGBAByte(0xFF, 0xC0, 0xCB);
            Plum = Color.FromRGBAByte(0xDD, 0xA0, 0xDD);
            PowderBlue = Color.FromRGBAByte(0xB0, 0xE0, 0xE6);
            RosyBrown = Color.FromRGBAByte(0xBC, 0x8F, 0x8F);
            RoyalBlue = Color.FromRGBAByte(0x41, 0x69, 0xE1);
            SaddleBrown = Color.FromRGBAByte(0x8B, 0x45, 0x13);
            Salmon = Color.FromRGBAByte(0xFA, 0x80, 0x72);
            SandyBrown = Color.FromRGBAByte(0xF4, 0xA4, 0x60);
            SeaGreen = Color.FromRGBAByte(0x2E, 0x8B, 0x57);

            Seashell = Color.FromRGBAByte(0xFF, 0xF5, 0xEE);
            Sienna = Color.FromRGBAByte(0xA0, 0x52, 0x2D);
            SkyBlue = Color.FromRGBAByte(0x87, 0xCE, 0xEB);
            SlateBlue = Color.FromRGBAByte(0x6A, 0x5A, 0xCD);
            SlateGray = Color.FromRGBAByte(0x70, 0x80, 0x90);
            Snow = Color.FromRGBAByte(0xFF, 0xFA, 0xFA);
            SpringGreen = Color.FromRGBAByte(0x00, 0xFF, 0x7F);
            SteelBlue = Color.FromRGBAByte(0x46, 0x82, 0xB4);

            Tan = Color.FromRGBAByte(0xD2, 0xB4, 0x8C);
            Thistle = Color.FromRGBAByte(0xD8, 0xBF, 0xD8);
            Tomato = Color.FromRGBAByte(0xFF, 0x63, 0x47);
            Transparent = Color.FromRGBAByte(0x00, 0x00, 0x00, 0x00);
            Turquoise = Color.FromRGBAByte(0x40, 0xE0, 0xD0);
            Violet = Color.FromRGBAByte(0xEE, 0x82, 0xEE);
            Wheat = Color.FromRGBAByte(0xF5, 0xDE, 0xB3);
            WhiteSmoke = Color.FromRGBAByte(0xF5, 0xF5, 0xF5);
            YellowGreen = Color.FromRGBAByte(0x9A, 0xCD, 0x32);
        }

        // HTML Standard Colors
        public static Color Aqua { get; private set; }
        public static Color Black { get; private set; }
        public static Color Blue { get; private set; }
        public static Color Fuchsia { get; private set; }
        public static Color Gray { get; private set; }
        public static Color Green { get; private set; }
        public static Color Lime { get; private set; }
        public static Color Maroon { get; private set; }
        public static Color Navy { get; private set; }
        public static Color Olive { get; private set; }
        public static Color Purple { get; private set; }
        public static Color Red { get; private set; }
        public static Color Silver { get; private set; }
        public static Color Teal { get; private set; }
        public static Color Yellow { get; private set; }
        public static Color White { get; private set; }

        // HTML Extended Colors

        public static Color AliceBlue { get; private set; }
        public static Color AntiqueWhite { get; private set; }
        public static Color Aquamarine { get; private set; }
        public static Color Azure { get; private set; }
        public static Color Beige { get; private set; }
        public static Color Bisque { get; private set; }
        public static Color BlanchedAlmond { get; private set; }
        public static Color BlueViolet { get; private set; }
        public static Color Brown { get; private set; }
        public static Color BurlyWood { get; private set; }
        public static Color CadetBlue { get; private set; }
        public static Color Chartreuse { get; private set; }
        public static Color Chocolate { get; private set; }
        public static Color Coral { get; private set; }
        public static Color CornflowerBlue { get; private set; }
        public static Color Cornsilk { get; private set; }
        public static Color Crimson { get; private set; }
        public static Color Cyan { get; private set; }
        public static Color DarkBlue { get; private set; }
        public static Color DarkCyan { get; private set; }
        public static Color DarkGoldenrod { get; private set; }
        public static Color DarkGray { get; private set; }
        public static Color DarkGreen { get; private set; }
        public static Color DarkKhaki { get; private set; }
        public static Color DarkMagenta { get; private set; }
        public static Color DarkOliveGreen { get; private set; }
        public static Color DarkOrange { get; private set; }
        public static Color DarkOrchid { get; private set; }
        public static Color DarkRed { get; private set; }
        public static Color DarkSalmon { get; private set; }
        public static Color DarkSeaGreen { get; private set; }
        public static Color DarkSlateBlue { get; private set; }
        public static Color DarkSlateGray { get; private set; }
        public static Color DarkTurquoise { get; private set; }

        public static Color DarkViolet { get; private set; }
        public static Color DeepPink { get; private set; }
        public static Color DeepSkyBlue { get; private set; }
        public static Color DimGray { get; private set; }
        public static Color DodgerBlue { get; private set; }
        public static Color Firebrick { get; private set; }
        public static Color FloralWhite { get; private set; }
        public static Color ForestGreen { get; private set; }
        public static Color Gainsboro { get; private set; }
        public static Color GhostWhite { get; private set; }
        public static Color Gold { get; private set; }
        public static Color Goldenrod { get; private set; }
        public static Color GreenYellow { get; private set; }
        public static Color Honeydew { get; private set; }
        public static Color HotPink { get; private set; }
        public static Color IndianRed { get; private set; }
        public static Color Indigo { get; private set; }
        public static Color Ivory { get; private set; }
        public static Color Khaki { get; private set; }
        public static Color Lavender { get; private set; }
        public static Color LavenderBlush { get; private set; }
        public static Color LawnGreen { get; private set; }
        public static Color LemonChiffon { get; private set; }
        public static Color LightBlue { get; private set; }
        public static Color LightCoral { get; private set; }
        public static Color LightCyan { get; private set; }
        public static Color LightGoldenrodYellow { get; private set; }
        public static Color LightGreen { get; private set; }
        public static Color LightGray { get; private set; }
        public static Color LightPink { get; private set; }
        public static Color LightSalmon { get; private set; }
        public static Color LightSeaGreen { get; private set; }
        public static Color LightSkyBlue { get; private set; }
        public static Color LightSlateGray { get; private set; }
        public static Color LightSteelBlue { get; private set; }
        public static Color LightYellow { get; private set; }
        public static Color LimeGreen { get; private set; }

        public static Color Linen { get; private set; }
        public static Color Magenta { get; private set; }
        public static Color MediumAquamarine { get; private set; }
        public static Color MediumBlue { get; private set; }
        public static Color MediumOrchid { get; private set; }
        public static Color MediumPurple { get; private set; }
        public static Color MediumSeaGreen { get; private set; }
        public static Color MediumSlateBlue { get; private set; }
        public static Color MediumSpringGreen { get; private set; }
        public static Color MediumTurquoise { get; private set; }
        public static Color MediumVioletRed { get; private set; }
        public static Color MidnightBlue { get; private set; }
        public static Color MintCream { get; private set; }
        public static Color MistyRose { get; private set; }
        public static Color Moccasin { get; private set; }
        public static Color NavajoWhite { get; private set; }
        public static Color OldLace { get; private set; }
        public static Color OliveDrab { get; private set; }
        public static Color Orange { get; private set; }
        public static Color OrangeRed { get; private set; }
        public static Color Orchid { get; private set; }
        public static Color PaleGoldenrod { get; private set; }
        public static Color PaleTurquoise { get; private set; }
        public static Color PaleVioletRed { get; private set; }
        public static Color PapayaWhip { get; private set; }
        public static Color PeachPuff { get; private set; }
        public static Color Peru { get; private set; }
        public static Color Pink { get; private set; }
        public static Color Plum { get; private set; }
        public static Color PowderBlue { get; private set; }
        public static Color RosyBrown { get; private set; }
        public static Color RoyalBlue { get; private set; }
        public static Color SaddleBrown { get; private set; }
        public static Color Salmon { get; private set; }
        public static Color SandyBrown { get; private set; }
        public static Color SeaGreen { get; private set; }

        public static Color Seashell { get; private set; }
        public static Color Sienna { get; private set; }
        public static Color SkyBlue { get; private set; }
        public static Color SlateBlue { get; private set; }
        public static Color SlateGray { get; private set; }
        public static Color Snow { get; private set; }
        public static Color SpringGreen { get; private set; }
        public static Color SteelBlue { get; private set; }

        public static Color Tan { get; private set; }
        public static Color Thistle { get; private set; }
        public static Color Tomato { get; private set; }
        public static Color Transparent { get; private set; }
        public static Color Turquoise { get; private set; }
        public static Color Violet { get; private set; }
        public static Color Wheat { get; private set; }
        public static Color WhiteSmoke { get; private set; }
        public static Color YellowGreen { get; private set; }
    }
}
