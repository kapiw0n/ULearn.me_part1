using System;
namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] Original, double[,] Sx)
		{
            var Height = Original.GetLength(1);
            var Width = Original.GetLength(0);
            var FilteredPixels = new double[Width, Height];
            var OffsetX = Sx.GetLength(0) / 2;
            var OffsetY = Sx.GetLength(1) / 2;
			var Sy = GetTransposedMatrix(Sx);

            for (var X = OffsetX; X < Width - OffsetX; X++)
            {
                for (var Y = OffsetY; Y < Height - OffsetY; Y++)
                {
                    var Gx = GetConvolution(Original, Sx, X, Y, OffsetX);
                    var Gy = GetConvolution(Original, Sy, X, Y, OffsetY);
                    FilteredPixels[X, Y] = Math.Sqrt(Gx * Gx + Gy * Gy);
                }
            }
            return FilteredPixels;
        }

        private static double[,] GetTransposedMatrix(double[,] Matrix)
		{
            var Height = Matrix.GetLength(1);
            var Width = Matrix.GetLength(0);
            var TransposedMatrix = new double[Width, Height];

            for (var X = 0; X < Width; X++)
            {
                for (var Y = 0; Y < Height; Y++)
                {
                    TransposedMatrix[X, Y] = Matrix[Y, X];
                }
            }
            return TransposedMatrix;
        }

        private static double GetConvolution(double[,] Original, double[,] S, int X, int Y, int Offset)
		{
            var Height = S.GetLength(1);
            var Width = S.GetLength(0);
            var Result = 0.0;

            for (var Sx = 0; Sx < Width; Sx++)
            {
                for (var Sy = 0; Sy < Height; Sy++)
                {
                    Result += S[Sx, Sy] * Original[X + Sx - Offset, Y + Sy - Offset];
                }
            }
            return Result;
        }
    }
}