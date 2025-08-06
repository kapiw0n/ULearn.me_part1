namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var mainDirection = GetMainDirection(width, height);
            var steps = GetSteps(width, height);

            MoveAlongDiagonal(robot, mainDirection, steps, width, height);
        }

        private static Direction GetMainDirection(int width, int height)
        {
            return width > height ? Direction.Right : Direction.Down;
        }

        private static int GetSteps(int width, int height)
        {
            return width > height ? width / (height - 1) : (height - 3) / (width - 2);
        }

        private static void MoveAlongDiagonal(Robot robot, Direction mainDirection, int steps, int width, int height)
        {
            for (int k = 0; k < (width > height ? height : width) - 2; ++k)
            {
                MoveInMainDirection(robot, mainDirection, steps);

                if (k != (width > height ? height : width) - 3)
                {
                    MoveInOppositeDirection(robot, mainDirection);
                }
            }
        }

        private static void MoveInMainDirection(Robot robot, Direction mainDirection, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                robot.MoveTo(mainDirection);
            }
        }

        private static void MoveInOppositeDirection(Robot robot, Direction mainDirection)
        {
            robot.MoveTo(mainDirection == Direction.Right ? Direction.Down : Direction.Right);
        }
    }
}