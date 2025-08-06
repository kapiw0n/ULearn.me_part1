using System;
namespace Geometry
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector Vector)
        {
            return Geometry.Add(this, Vector);
        }

        public bool Belongs(Segment Segment)
        {
            return Geometry.IsVectorInSegment(this, Segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector Vector)
        {
            return Geometry.IsVectorInSegment(Vector, this);
        }
    }

    public static class Geometry
    {
        public static double ComputeLength(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static double GetLength(Vector Vector)
        {
            return ComputeLength(Vector.X, Vector.Y);
        }

        public static Vector Add(Vector Vector1, Vector Vector2)
        {
            return new Vector { X = Vector1.X + Vector2.X, Y = Vector1.Y + Vector2.Y };
        }

        public static double GetLength(Segment Segment)
        {
            var DiffX = Math.Abs(Segment.End.X - Segment.Begin.X);
            var DiffY = Math.Abs(Segment.End.Y - Segment.Begin.Y);

            return ComputeLength(DiffX, DiffY);
        }

		public static bool IsVectorInSegment(Vector vector, Segment segment)
		{
    		double segmentLength = segment.GetLength();
    		double lengthToBegin = new Vector { X = vector.X - segment.Begin.X, Y = vector.Y - segment.Begin.Y }.GetLength();
    		double lengthToEnd = new Vector { X = vector.X - segment.End.X, Y = vector.Y - segment.End.Y }.GetLength();
    		return Math.Abs(segmentLength - (lengthToBegin + lengthToEnd)) < 1e-10;
		}
    }
}