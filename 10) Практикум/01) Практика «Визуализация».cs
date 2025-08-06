using System;
using System.Globalization;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
namespace Manipulation;

public static class VisualizerTask
{
    public static double X = 220;
    public static double Y = -100;
    public static double Alpha = 0.05;
    public static double Wrist = 2 * Math.PI / 3;
    public static double Elbow = 3 * Math.PI / 4;
    public static double Shoulder = Math.PI / 2;
    public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230));
    public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 230, 255, 230));
    public static Pen ManipulatorPen = new Pen(Brushes.Black, 3);
    public static Brush JointBrush = new SolidColorBrush(Colors.Gray);

    public static void KeyDown(Visual visual, KeyEventArgs key)
    {
        const double deltaAngle = 0.05;
        switch (key.Key)
        {
            case Key.Q:
                Shoulder += deltaAngle;
                break;
            case Key.A:
                Shoulder -= deltaAngle;
                break;
            case Key.W:
                Elbow += deltaAngle;
                break;
            case Key.S:
                Elbow -= deltaAngle;
                break;
        }
        Wrist = -Alpha - Shoulder - Elbow;

        visual.InvalidateVisual();
    }

    public static void MouseMove(Visual visual, PointerEventArgs e)
    {
        var shoulderPosition = GetShoulderPos(visual);
        var logicalCoordinates = ConvertWindowToMath(new Point(e.GetPosition(visual).X, 
        e.GetPosition(visual).Y), 
        shoulderPosition);

        X = logicalCoordinates.X;
        Y = logicalCoordinates.Y;

        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void MouseWheel(Visual visual, PointerWheelEventArgs e)
    {
        Alpha += e.Delta.Y * 0.05;

        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void UpdateManipulator()
    {
        var result = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
        if (result != null && result.Length >= 3)
        {
            Shoulder = result[0];
            Elbow = result[1];
            Wrist = result[2];
        }
    }

    public static void DrawManipulator(DrawingContext context, Point shoulderPos)
    {
        var jointPositions = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

        DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, jointPositions);
        DrawManipulatorInfo(context);
        DrawManipulatorArms(context, shoulderPos, jointPositions);
        DrawManipulatorJoints(context, shoulderPos, jointPositions);
    }

    private static void DrawManipulatorInfo(DrawingContext context)
    {
        var formattedText = new FormattedText(
            $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            Typeface.Default,
            18,
            Brushes.DarkRed
        )
        {
            TextAlignment = TextAlignment.Center
        };
        context.DrawText(formattedText, new Point(10, 10));
    }

    private static void DrawManipulatorArms(DrawingContext context, Point shoulderPos, Point[] joints)
    {
        for (int i = 0; i < joints.Length - 1; i++)
        {
            var startPoint = ConvertMathToWindow(joints[i], shoulderPos);
            var endPoint = ConvertMathToWindow(joints[i + 1], shoulderPos);
            context.DrawLine(ManipulatorPen, startPoint, endPoint);
        }
    }

    private static void DrawManipulatorJoints(DrawingContext context, Point shoulderPos, Point[] joints)
    {
        foreach (var joint in joints)
        {
            var windowJointPosition = ConvertMathToWindow(joint, shoulderPos);
            context.DrawEllipse(JointBrush, null, windowJointPosition, 5, 5);
        }
    }

    private static void DrawReachableZone(
        DrawingContext context,
        Brush reachableBrush,
        Brush unreachableBrush,
        Point shoulderPos,
        Point[] joints)
    {
        var minReach = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
        var maxReach = Manipulator.UpperArm + Manipulator.Forearm;
        context.DrawEllipse(reachableBrush, null, shoulderPos, maxReach, maxReach);
        context.DrawEllipse(unreachableBrush, null, shoulderPos, minReach, minReach);
    }

    public static Point GetShoulderPos(Visual visual)
	{
        return new Point(visual.Bounds.Width / 2, visual.Bounds.Height / 2);
    }

    public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
    {
        return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
    }

    public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
    {
        return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
    }
}