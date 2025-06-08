using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class Drawer
    {
        static float X, Y;
        static IGraphics Graphics;

        public static void Initialize(IGraphics NewGraphics)
        {
            Graphics = NewGraphics;
            Graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float X0, float Y0)
        {
            X = X0;
            Y = Y0;
        }

        public static void DrawLine(Pen Brush, double Length, double Angle)
        {
            // Делает шаг длиной Length в направлении Angle и рисует пройденную траекторию
            var X1 = (float)(X + Length * Math.Cos(Angle));
            var Y1 = (float)(Y + Length * Math.Sin(Angle));
            Graphics.DrawLine(Brush, X, Y, X1, Y1);
            X = X1;
            Y = Y1;
        }

        public static void Move(double Length, double Angle)
        {
            X = (float)(X + Length * Math.Cos(Angle));
            Y = (float)(Y + Length * Math.Sin(Angle));
        }
    }

    public class ImpossibleSquare
    {
        private const double MainSideRatio = 0.375;
        private const double ConnectorRatio = 0.04;
        private static readonly Pen SquareBrush = new Pen(Brushes.Yellow);

        public static void Draw(int Width, int Height, double RotationAngle, IGraphics Graphics)
        {
            Drawer.Initialize(Graphics);

            var Size = Math.Min(Width, Height);
            var MainLength = Size * MainSideRatio;
            var ConnectorLength = Size * ConnectorRatio;
            var ConnectorDiagonal = ConnectorLength * Math.Sqrt(2);
            var HalfDiagonalLength = Math.Sqrt(2) * (MainLength + ConnectorLength) / 2;

            var X0 = (float)(HalfDiagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + Width / 2f;
            var Y0 = (float)(HalfDiagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + Height / 2f;

            Drawer.SetPosition(X0, Y0);

            // Рисуем стороны квадрата
            DrawSide(MainLength, ConnectorLength, ConnectorDiagonal, 0);
            DrawSide(MainLength, ConnectorLength, ConnectorDiagonal, -Math.PI / 2);
            DrawSide(MainLength, ConnectorLength, ConnectorDiagonal, Math.PI);
            DrawSide(MainLength, ConnectorLength, ConnectorDiagonal, Math.PI / 2);
        }

        private static void DrawSide(double MainLength, double ConnectorLength,
                             double ConnectorDiagonal, double StartAngle)
        {
            // Рисуем основную часть стороны
            Drawer.DrawLine(SquareBrush, MainLength, StartAngle);
            Drawer.DrawLine(SquareBrush, ConnectorDiagonal, StartAngle + Math.PI / 4);
            Drawer.DrawLine(SquareBrush, MainLength, StartAngle + Math.PI);
            Drawer.DrawLine(SquareBrush, MainLength - ConnectorLength, StartAngle + Math.PI / 2);

            // Перемещаемся к началу следующей стороны
            Drawer.Move(ConnectorLength, StartAngle - Math.PI);
            Drawer.Move(ConnectorDiagonal, StartAngle + 3 * Math.PI / 4);
        }
    }
}