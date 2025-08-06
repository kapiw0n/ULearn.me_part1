using NUnit.Framework;
using System;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double A, double B, double C)
        {
            if (!IsValidTriangle(A, B, C))
                return double.NaN;

            var Angle = CalculateCosineValue(A, B, C);

            return Math.Acos(Angle);
        }

        private static bool IsValidTriangle(double A, double B, double C)
        {
            return (A + B) >= C && (B + C) >= A && (C + A) >= B;
        }

        private static double CalculateCosineValue(double A, double B, double C)
        {
            return (A * A + B * B - C * C) / (2 * A * B);
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(0, 2, 3, double.NaN)]
        [TestCase(2, 0, 5, double.NaN)]
        public void TestGetABAngle(double A, double B, double C, double ExpectedAngle)
        {
            Assert.AreEqual(TriangleTask.GetABAngle(A, B, C), ExpectedAngle, 1e-9);
        }
    }
}