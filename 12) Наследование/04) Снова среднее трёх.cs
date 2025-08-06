static T MiddleOfThree<T>(T a, T b, T c) where T : IComparable<T>
    {
        if (a.CompareTo(b) > 0)
        {
            if (a.CompareTo(c) < 0)
                return a; 
            else if (b.CompareTo(c) > 0)
                return b;
            else
                return c;
        }
        else
        {
            if (a.CompareTo(c) > 0)
                return a;
            else if (b.CompareTo(c) < 0)
                return b; 
            else
                return c; 
        }
    }