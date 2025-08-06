using NUnit.Framework;
using System;
using static Manipulation.Manipulator;
using Avalonia;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static Point[] GetJointPositions(double Shoulder, double Elbow, double Wrist)
        {
            var Angle = Shoulder;
            var X = UpperArm * Math.Cos(Shoulder);
            var Y = UpperArm * Math.Sin(Shoulder);
            var ElbowPos = new Point((float)X, (float)Y);

            Angle += Elbow - Math.PI;
            X += Forearm * Math.Cos(Angle);
            Y += Forearm * Math.Sin(Angle);
            var WristPos = new Point((float)X, (float)Y);

            Angle += Wrist - Math.PI;
            X += Palm * Math.Cos(Angle);
            Y += Palm * Math.Sin(Angle);
            var PalmEndPos = new Point((float)X, (float)Y);

            return new[]
            {
                ElbowPos,
                WristPos,
                PalmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, 3 * Math.PI, 0, Forearm + UpperArm + Palm)]
        [TestCase(Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, -Forearm, UpperArm - Palm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Forearm, UpperArm - Palm)]
        public void TestGetJointPositions(double Shoulder, double Elbow, double Wrist, double PalmEndX, double PalmEndY)
        {
            var Joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);
            Assert.AreEqual(PalmEndX, Joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(PalmEndY, Joints[2].Y, 1e-5, "palm endY");

            var DistanceShoulderToElbow = Distance(Joints[0], new Point(0, 0));
            var DistanceElbowToWrist = Distance(Joints[1], Joints[0]);
            var DistanceWristToPalmEnd = Distance(Joints[2], Joints[1]);

            Assert.AreEqual(UpperArm, DistanceShoulderToElbow, 1e-5, "upperArm length");
            Assert.AreEqual(Forearm, DistanceElbowToWrist, 1e-5, "forearm length");
            Assert.AreEqual(Palm, DistanceWristToPalmEnd, 1e-5, "palm length");
        }

        public static double Distance(Point First, Point Second)
		{
            var Dy = Second.Y - First.Y;
            var Dx = Second.X - First.X;
            return Math.Sqrt(Dx * Dx + Dy * Dy);
        }
    }
}
