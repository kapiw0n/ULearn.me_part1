namespace Recognizer
{
	public static class GrayscaleTask
	{
        const double Red = 0.299;
		const double Blue = 0.114;
		const double Green = 0.587;

        public static double[,] ToGrayscale(Pixel[,] Original)
		{
			var Height = Original.GetLength(1);
            var Width = Original.GetLength(0);
            var GrayScale = new double[Width, Height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
					GrayScale[x, y] = (Red * Original[x, y].R + 
					Green * Original[x, y].G + Blue * Original[x, y].B) / 255;
            }
			return GrayScale;
		}
	}
}