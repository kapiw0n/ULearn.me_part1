using System;
namespace Geometry
{
    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector {X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y};
        }

        public static double GetLength(Segment segment)
        {
            double difX = Math.Abs(segment.End.X - segment.Begin.X);
            double difY = Math.Abs(segment.End.Y - segment.Begin.Y);

            return Math.Sqrt((difX * difX) + (difY * difY));
        }

		public static bool IsVectorInSegment(Vector vector, Segment segment)
		{
		    double segmentLength = GetLength(segment);
		    double lengthToBegin = GetLength(new Vector { X = vector.X - segment.Begin.X, Y = vector.Y - segment.Begin.Y });
		    double lengthToEnd = GetLength(new Vector { X = vector.X - segment.End.X, Y = vector.Y - segment.End.Y });
    		return Math.Abs(segmentLength - (lengthToBegin + lengthToEnd)) < 1e-10;
		}
    }
}