using System.Collections.Generic;
using System.Linq;
namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] Original)
		{
            var Height = Original.GetLength(1);
            var Width = Original.GetLength(0);
            var MedianPixels = new double[Width, Height];

            for (int X = 0; X < Width; X++)
                for (int Y = 0; Y < Height; Y++)
                    MedianPixels[X, Y] = CalculateMedianPixelValue(X, Y, Original);
			return MedianPixels;
		}

        public static double CalculateMedianPixelValue(int X, int Y, double[,] Pixels)
		{
            var Height = Pixels.GetLength(1);
            var Width = Pixels.GetLength(0);
            var PixelValues = new List<double>();

            for (int I = 0; I < 3; I++)
                for (int J = 0; J < 3; J++)
                    if (CheckPixelWithinBoundaries(X - 1 + I, Y - 1 + J, Width, Height))
                        PixelValues.Add(Pixels[X - 1 + I, Y - 1 + J]);

            PixelValues.Sort();
            if (PixelValues.Count % 2 == 1)
                return PixelValues[PixelValues.Count / 2];
            else
                return (PixelValues[(PixelValues.Count / 2) - 1] + PixelValues[PixelValues.Count / 2]) / 2;
        }

        public static bool CheckPixelWithinBoundaries(int X, int Y, int Width, int Height)
        {
            return (X > -1 && Y > -1) && (X < Width && Y < Height);
        }
	}
}