namespace ReadOnlyVector
{
    public class ReadOnlyVector
    {
        public readonly double X;
        public readonly double Y;

        public ReadOnlyVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public ReadOnlyVector Add(ReadOnlyVector otherVector)
        {
            return new ReadOnlyVector(X + otherVector.X, Y + otherVector.Y);
        }

        public ReadOnlyVector WithX(double newX)
        {
            return new ReadOnlyVector(newX, Y);
        }

        public ReadOnlyVector WithY(double newY)
        {
            return new ReadOnlyVector(X, newY);
        }
    }
}