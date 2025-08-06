using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] Original, double Threshold)
		{
            var Height = Original.GetLength(1);
            var Width = Original.GetLength(0);
            var FilteredPixels = new double[Width, Height];

            var ThresholdValue = CalculateThresholdValue(Original, Threshold);

            for (var X = 0; X < Width; X++)
                for (var Y = 0; Y < Height; Y++)
                    FilteredPixels[X, Y] = (Original[X, Y] >= ThresholdValue) ? 1.0 : 0.0;

            return FilteredPixels;
        }

        private static double CalculateThresholdValue(double[,] Original, double Threshold)
		{
            var Height = Original.GetLength(1);
            var Width = Original.GetLength(0);
			var Pixels = new List<double>(Width * Height);
            var NumberOfPixels = Width * Height;

            var PixelsToChange = (int)(NumberOfPixels * Threshold);

            for (var X = 0; X < Width; X++)
                for (var Y = 0; Y < Height; Y++)
                    Pixels.Add(Original[X, Y]);
            Pixels.Sort();
            return GetThresholdValue(Pixels, NumberOfPixels, PixelsToChange);
        }

        private static double GetThresholdValue(List<double> Pixels, int NumberOfPixels, int PixelsToChange)
        {
            if (PixelsToChange == NumberOfPixels)
                return -1.0;
            else if (PixelsToChange == 0)
                return double.MaxValue;
            else
                return Pixels[NumberOfPixels - PixelsToChange];
        }
    }
}