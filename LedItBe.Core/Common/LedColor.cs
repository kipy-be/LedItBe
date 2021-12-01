using System;

namespace LedItBe.Core.Common
{
    public class LedColor
    {
        private static WhiteColorTemperature _whiteColorTemperature;
        private static Random _random = new Random();

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte W { get; set; }

        public static void SetWhiteColorTemperature(WhiteColorTemperature value)
        {
            _whiteColorTemperature = value;
        }

        private LedColor(byte r, byte g, byte b)
        {
            if (_whiteColorTemperature == null)
            {
                R = r;
                G = g;
                B = b;
                W = 0;
            }
            else
            {
                RgbToRgbw(r, g, b);
            }
        }

        private void RgbToRgbw(byte r, byte g, byte b)
        {
            double wR = r * 255.0 / _whiteColorTemperature.R;
            double wG = g * 255.0 / _whiteColorTemperature.G;
            double wB = b * 255.0 / _whiteColorTemperature.B;

            double minW = Math.Min(wR, Math.Min(wG, wB));
            W = (byte)(minW <= 255 ? minW : 255);
            R = (byte)(r - minW * _whiteColorTemperature.R / 255);
            G = (byte)(g - minW * _whiteColorTemperature.G / 255);
            B = (byte)(b - minW * _whiteColorTemperature.B / 255);
        }

        public void SetColor(byte r, byte g, byte b)
        {
            RgbToRgbw(r, g, b);
        }

        public void SetColor(LedColor color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            W = color.W;
        }

        public static LedColor FromRgb(byte r, byte g, byte b) => new LedColor(r, g, b);

        public static LedColor Random => new LedColor((byte)_random.Next(255), (byte)_random.Next(255), (byte)_random.Next(255));
        public static LedColor AliceBlue => new LedColor(0xF0, 0xF8, 0xFF);
        public static LedColor AntiqueWhite => new LedColor(0xFA, 0xEB, 0xD7);
        public static LedColor Aqua => new LedColor(0x0, 0xFF, 0xFF);
        public static LedColor Aquamarine => new LedColor(0x7F, 0xFF, 0xD4);
        public static LedColor Azure => new LedColor(0xF0, 0xFF, 0xFF);
        public static LedColor Beige => new LedColor(0xF5, 0xF5, 0xDC);
        public static LedColor Bisque => new LedColor(0xFF, 0xE4, 0xC4);
        public static LedColor Black => new LedColor(0x0, 0x0, 0x0);
        public static LedColor BlanchedAlmond => new LedColor(0xFF, 0xFF, 0xCD);
        public static LedColor Blue => new LedColor(0x0, 0x0, 0xFF);
        public static LedColor BlueViolet => new LedColor(0x8A, 0x2B, 0xE2);
        public static LedColor Brown => new LedColor(0xA5, 0x2A, 0x2A);
        public static LedColor BurlyWood => new LedColor(0xDE, 0xB8, 0x87);
        public static LedColor CadetBlue => new LedColor(0x5F, 0x9E, 0xA0);
        public static LedColor Chartreuse => new LedColor(0x7F, 0xFF, 0x0);
        public static LedColor Chocolate => new LedColor(0xD2, 0x69, 0x1E);
        public static LedColor Coral => new LedColor(0xFF, 0x7F, 0x50);
        public static LedColor CornflowerBlue => new LedColor(0x64, 0x95, 0xED);
        public static LedColor Cornsilk => new LedColor(0xFF, 0xF8, 0xDC);
        public static LedColor Crimson => new LedColor(0xDC, 0x14, 0x3C);
        public static LedColor Cyan => new LedColor(0x0, 0xFF, 0xFF);
        public static LedColor DarkBlue => new LedColor(0x0, 0x0, 0x8B);
        public static LedColor DarkCyan => new LedColor(0x0, 0x8B, 0x8B);
        public static LedColor DarkGoldenrod => new LedColor(0xB8, 0x86, 0xB);
        public static LedColor DarkGray => new LedColor(0xA9, 0xA9, 0xA9);
        public static LedColor DarkGreen => new LedColor(0x0, 0x64, 0x0);
        public static LedColor DarkKhaki => new LedColor(0xBD, 0xB7, 0x6B);
        public static LedColor DarkMagena => new LedColor(0x8B, 0x0, 0x8B);
        public static LedColor DarkOliveGreen => new LedColor(0x55, 0x6B, 0x2F);
        public static LedColor DarkOrange => new LedColor(0xFF, 0x8C, 0x0);
        public static LedColor DarkOrchid => new LedColor(0x99, 0x32, 0xCC);
        public static LedColor DarkRed => new LedColor(0x8B, 0x0, 0x0);
        public static LedColor DarkSalmon => new LedColor(0xE9, 0x96, 0x7A);
        public static LedColor DarkSeaGreen => new LedColor(0x8F, 0xBC, 0x8F);
        public static LedColor DarkSlateBlue => new LedColor(0x48, 0x3D, 0x8B);
        public static LedColor DarkSlateGray => new LedColor(0x28, 0x4F, 0x4F);
        public static LedColor DarkTurquoise => new LedColor(0x0, 0xCE, 0xD1);
        public static LedColor DarkViolet => new LedColor(0x94, 0x0, 0xD3);
        public static LedColor DeepPink => new LedColor(0xFF, 0x14, 0x93);
        public static LedColor DeepSkyBlue => new LedColor(0x0, 0xBF, 0xFF);
        public static LedColor DimGray => new LedColor(0x69, 0x69, 0x69);
        public static LedColor DodgerBlue => new LedColor(0x1E, 0x90, 0xFF);
        public static LedColor Firebrick => new LedColor(0xB2, 0x22, 0x22);
        public static LedColor FloralWhite => new LedColor(0xFF, 0xFA, 0xF0);
        public static LedColor ForestGreen => new LedColor(0x22, 0x8B, 0x22);
        public static LedColor Fuschia => new LedColor(0xFF, 0x0, 0xFF);
        public static LedColor Gainsboro => new LedColor(0xDC, 0xDC, 0xDC);
        public static LedColor GhostWhite => new LedColor(0xF8, 0xF8, 0xFF);
        public static LedColor Gold => new LedColor(0xFF, 0xD7, 0x0);
        public static LedColor Goldenrod => new LedColor(0xDA, 0xA5, 0x20);
        public static LedColor Gray => new LedColor(0x80, 0x80, 0x80);
        public static LedColor Green => new LedColor(0x0, 0x80, 0x0);
        public static LedColor GreenYellow => new LedColor(0xAD, 0xFF, 0x2F);
        public static LedColor Honeydew => new LedColor(0xF0, 0xFF, 0xF0);
        public static LedColor HotPink => new LedColor(0xFF, 0x69, 0xB4);
        public static LedColor IndianRed => new LedColor(0xCD, 0x5C, 0x5C);
        public static LedColor Indigo => new LedColor(0x4B, 0x0, 0x82);
        public static LedColor Ivory => new LedColor(0xFF, 0xF0, 0xF0);
        public static LedColor Khaki => new LedColor(0xF0, 0xE6, 0x8C);
        public static LedColor Lavender => new LedColor(0xE6, 0xE6, 0xFA);
        public static LedColor LavenderBlush => new LedColor(0xFF, 0xF0, 0xF5);
        public static LedColor LawnGreen => new LedColor(0x7C, 0xFC, 0x0);
        public static LedColor LemonChiffon => new LedColor(0xFF, 0xFA, 0xCD);
        public static LedColor LightBlue => new LedColor(0xAD, 0xD8, 0xE6);
        public static LedColor LightCoral => new LedColor(0xF0, 0x80, 0x80);
        public static LedColor LightCyan => new LedColor(0xE0, 0xFF, 0xFF);
        public static LedColor LightGoldenrodYellow => new LedColor(0xFA, 0xFA, 0xD2);
        public static LedColor LightGray => new LedColor(0xD3, 0xD3, 0xD3);
        public static LedColor LightGreen => new LedColor(0x90, 0xEE, 0x90);
        public static LedColor LightPink => new LedColor(0xFF, 0xB6, 0xC1);
        public static LedColor LightSalmon => new LedColor(0xFF, 0xA0, 0x7A);
        public static LedColor LightSeaGreen => new LedColor(0x20, 0xB2, 0xAA);
        public static LedColor LightSkyBlue => new LedColor(0x87, 0xCE, 0xFA);
        public static LedColor LightSlateGray => new LedColor(0x77, 0x88, 0x99);
        public static LedColor LightSteelBlue => new LedColor(0xB0, 0xC4, 0xDE);
        public static LedColor LightYellow => new LedColor(0xFF, 0xFF, 0xE0);
        public static LedColor Lime => new LedColor(0x0, 0xFF, 0x0);
        public static LedColor LimeGreen => new LedColor(0x32, 0xCD, 0x32);
        public static LedColor Linen => new LedColor(0xFA, 0xF0, 0xE6);
        public static LedColor Magenta => new LedColor(0xFF, 0x0, 0xFF);
        public static LedColor Maroon => new LedColor(0x80, 0x0, 0x0);
        public static LedColor MediumAquamarine => new LedColor(0x66, 0xCD, 0xAA);
        public static LedColor MediumBlue => new LedColor(0x0, 0x0, 0xCD);
        public static LedColor MediumOrchid => new LedColor(0xBA, 0x55, 0xD3);
        public static LedColor MediumPurple => new LedColor(0x93, 0x70, 0xDB);
        public static LedColor MediumSeaGreen => new LedColor(0x3C, 0xB3, 0x71);
        public static LedColor MediumSlateBlue => new LedColor(0x7B, 0x68, 0xEE);
        public static LedColor MediumSpringGreen => new LedColor(0x0, 0xFA, 0x9A);
        public static LedColor MediumTurquoise => new LedColor(0x48, 0xD1, 0xCC);
        public static LedColor MediumVioletRed => new LedColor(0xC7, 0x15, 0x70);
        public static LedColor MidnightBlue => new LedColor(0x19, 0x19, 0x70);
        public static LedColor MintCream => new LedColor(0xF5, 0xFF, 0xFA);
        public static LedColor MistyRose => new LedColor(0xFF, 0xE4, 0xE1);
        public static LedColor Moccasin => new LedColor(0xFF, 0xE4, 0xB5);
        public static LedColor NavajoWhite => new LedColor(0xFF, 0xDE, 0xAD);
        public static LedColor Navy => new LedColor(0x0, 0x0, 0x80);
        public static LedColor OldLace => new LedColor(0xFD, 0xF5, 0xE6);
        public static LedColor Olive => new LedColor(0x80, 0x80, 0x0);
        public static LedColor OliveDrab => new LedColor(0x6B, 0x8E, 0x2D);
        public static LedColor Orange => new LedColor(0xFF, 0xA5, 0x0);
        public static LedColor OrangeRed => new LedColor(0xFF, 0x45, 0x0);
        public static LedColor Orchid => new LedColor(0xDA, 0x70, 0xD6);
        public static LedColor PaleGoldenrod => new LedColor(0xEE, 0xE8, 0xAA);
        public static LedColor PaleGreen => new LedColor(0x98, 0xFB, 0x98);
        public static LedColor PaleTurquoise => new LedColor(0xAF, 0xEE, 0xEE);
        public static LedColor PaleVioletRed => new LedColor(0xDB, 0x70, 0x93);
        public static LedColor PapayaWhip => new LedColor(0xFF, 0xEF, 0xD5);
        public static LedColor PeachPuff => new LedColor(0xFF, 0xDA, 0x9B);
        public static LedColor Peru => new LedColor(0xCD, 0x85, 0x3F);
        public static LedColor Pink => new LedColor(0xFF, 0xC0, 0xCB);
        public static LedColor Plum => new LedColor(0xDD, 0xA0, 0xDD);
        public static LedColor PowderBlue => new LedColor(0xB0, 0xE0, 0xE6);
        public static LedColor Purple => new LedColor(0x80, 0x0, 0x80);
        public static LedColor Red => new LedColor(0xFF, 0x0, 0x0);
        public static LedColor RosyBrown => new LedColor(0xBC, 0x8F, 0x8F);
        public static LedColor RoyalBlue => new LedColor(0x41, 0x69, 0xE1);
        public static LedColor SaddleBrown => new LedColor(0x8B, 0x45, 0x13);
        public static LedColor Salmon => new LedColor(0xFA, 0x80, 0x72);
        public static LedColor SandyBrown => new LedColor(0xF4, 0xA4, 0x60);
        public static LedColor SeaGreen => new LedColor(0x2E, 0x8B, 0x57);
        public static LedColor Seashell => new LedColor(0xFF, 0xF5, 0xEE);
        public static LedColor Sienna => new LedColor(0xA0, 0x52, 0x2D);
        public static LedColor Silver => new LedColor(0xC0, 0xC0, 0xC0);
        public static LedColor SkyBlue => new LedColor(0x87, 0xCE, 0xEB);
        public static LedColor SlateBlue => new LedColor(0x6A, 0x5A, 0xCD);
        public static LedColor SlateGray => new LedColor(0x70, 0x80, 0x90);
        public static LedColor Snow => new LedColor(0xFF, 0xFA, 0xFA);
        public static LedColor SpringGreen => new LedColor(0x0, 0xFF, 0x7F);
        public static LedColor SteelBlue => new LedColor(0x46, 0x82, 0xB4);
        public static LedColor Tan => new LedColor(0xD2, 0xB4, 0x8C);
        public static LedColor Teal => new LedColor(0x0, 0x80, 0x80);
        public static LedColor Thistle => new LedColor(0xD8, 0xBF, 0xD8);
        public static LedColor Tomato => new LedColor(0xFD, 0x63, 0x47);
        public static LedColor Turquoise => new LedColor(0x40, 0xE0, 0xD0);
        public static LedColor Violet => new LedColor(0xEE, 0x82, 0xEE);
        public static LedColor Wheat => new LedColor(0xF5, 0xDE, 0xB3);
        public static LedColor White => new LedColor(0xFF, 0xFF, 0xFF);
        public static LedColor WhiteSmoke => new LedColor(0xF5, 0xF5, 0xF5);
        public static LedColor Yellow => new LedColor(0xFF, 0xFF, 0x0);
        public static LedColor YellowGreen => new LedColor(0x9A, 0xCD, 0x32);
    }
}
