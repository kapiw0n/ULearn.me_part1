using System;
namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double V, double Distance)
    {
        var g = 9.8;
        return Math.Asin(Distance * g / (V*V)) * 0.5;
    }
}