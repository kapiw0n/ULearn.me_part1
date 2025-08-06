using System;
namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return r1.Left <= r2.Right && r2.Left <= r1.Right && r1.Top <= r2.Bottom && r2.Top <= r1.Bottom;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (!AreIntersected(r1, r2)) return 0;
            var left = Math.Max(r1.Left, r2.Left);
            var top = Math.Max(r1.Top, r2.Top);
            var right = Math.Min(r1.Right, r2.Right);
            var bottom = Math.Min(r1.Bottom, r2.Bottom);
            return (right - left) * (bottom - top);
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (r1.Left >= r2.Left && r1.Right <= r2.Right && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom) return 0;
            if (r2.Left >= r1.Left && r2.Right <= r1.Right && r2.Top >= r1.Top && r2.Bottom <= r1.Bottom) return 1;
            return -1;
        }
    }
}