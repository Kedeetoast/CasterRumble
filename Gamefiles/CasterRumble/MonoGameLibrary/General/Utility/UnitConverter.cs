using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Utility
{
    public static class UnitConverter
    {
        public const float PixelsPerMeter = 64f;

        public static float ToMeters(float pixels) => pixels / PixelsPerMeter;
        public static float ToPixels(float meters) => meters * PixelsPerMeter;

        public static Vector2 ToMeters(Vector2 pixels) => pixels / PixelsPerMeter;
        public static Vector2 ToPixels(Vector2 meters) => meters * PixelsPerMeter;
    }
}
