using System;

namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveInDirection(Robot robot, int distance, Direction direction)
        {
            for (int i = 0; i < distance; i++)
            {
                robot.MoveTo(direction);
            }
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveInDirection(robot, width - 3, Direction.Right);
            MoveInDirection(robot, 2, Direction.Down);
            MoveInDirection(robot, width - 3, Direction.Left);

            if (!robot.Finished)
            {
                MoveInDirection(robot, 2, Direction.Down);
                MoveOut(robot, width, height);
            }
        }
    }
}