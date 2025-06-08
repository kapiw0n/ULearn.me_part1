public static bool IsCorrectMove(string from, string to)
{
    var dx = Math.Abs(to[0] - from[0]);
    var dy = Math.Abs(to[1] - from[1]);
    return (dx != 0 || dy != 0) && (dx == 0 || dy == 0 || dx == dy);
}