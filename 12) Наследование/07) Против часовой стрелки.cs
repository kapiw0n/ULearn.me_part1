public class ClockwiseComparer : IComparer
{
    public int Compare(object x, object y)
    {
        Point point1 = (Point)x;
        Point point2 = (Point)y;

        double angle1 = Math.Atan2(point1.Y, point1.X);
        double angle2 = Math.Atan2(point2.Y, point2.X);

        if (angle1 < 0) angle1 += 2 * Math.PI;
        if (angle2 < 0) angle2 += 2 * Math.PI;

        return angle1.CompareTo(angle2);
    }
}