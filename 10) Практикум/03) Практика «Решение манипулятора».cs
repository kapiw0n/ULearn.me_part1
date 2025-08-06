using System;
using NUnit.Framework;
namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double TargetX, double TargetY, double TargetAlpha)
        {
            var WristX = TargetX - Manipulator.Palm * Math.Cos(TargetAlpha);
            var WristY = TargetY + Manipulator.Palm * Math.Sin(TargetAlpha);
            var DistanceToWrist = Math.Sqrt(WristX * WristX + WristY * WristY);
            var ElbowAngle = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, DistanceToWrist);
            var FirstPartAngle = TriangleTask.GetABAngle(Manipulator.UpperArm, DistanceToWrist, Manipulator.Forearm);
            var SecondPartAngle = Math.Atan2(WristY, WristX);
            var ShoulderAngle = FirstPartAngle + SecondPartAngle;
            var WristAngle = -TargetAlpha - ShoulderAngle - ElbowAngle;

            if (double.IsNaN(WristAngle) || double.IsNaN(ShoulderAngle) || double.IsNaN(ElbowAngle))
                return new[] { double.NaN, double.NaN, double.NaN };

            return new[] { ShoulderAngle, ElbowAngle, WristAngle };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            Random RandomGenerator = new Random();

            for (int i = 0; i < 10; i++)
            {
                var TargetX = RandomGenerator.Next();
                var TargetY = RandomGenerator.Next();
                var TargetAngle = 2 * Math.PI * RandomGenerator.NextDouble();
                var Angles = ManipulatorTask.MoveManipulatorTo(TargetX, TargetY, TargetAngle);

                if (!Double.IsNaN(Angles[0]))
                {
                    var Coordinates = AnglesToCoordinatesTask.GetJointPositions(Angles[0], Angles[1], Angles[2]);
                    Assert.AreEqual(TargetX, Coordinates[2].X, 1e-5);
                    Assert.AreEqual(TargetY, Coordinates[2].Y, 1e-5);
                }
            }
        }
    }
}