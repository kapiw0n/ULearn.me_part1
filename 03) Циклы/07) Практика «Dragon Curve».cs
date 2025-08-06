using System;
using System.Drawing;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static double GetXCoordinate(double x, double y, double Angle)
        {
            return (x * Math.Cos(Angle) - y * Math.Sin(Angle)) / Math.Sqrt(2);
        }

        public static double GetYCoordinate(double x, double y, double Angle)
        {
            return (x * Math.Sin(Angle) + y * Math.Cos(Angle)) / Math.Sqrt(2);
        }

        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int Seed)
        {
            var x = 1.0;
			var y = 0.0;
			var random = new Random(Seed);
            for (int i = 0; i < iterationsCount; i++)
            {
                var previousX = x;
                if (random.Next(2) == 0)
                {
                    x = GetXCoordinate(x, y, Math.PI / 4);
                    y = GetYCoordinate(previousX, y, Math.PI / 4);
                }
                else
                {
                    x = GetXCoordinate(x, y, Math.PI * 3 / 4) + 1;
                    y = GetYCoordinate(previousX, y, Math.PI * 3 / 4);
                }
                pixels.SetPixel(x, y);
            }
        }
    }
}