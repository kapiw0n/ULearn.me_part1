namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MakeHorizontalMove(Robot robot, int Width, Direction Dir)
        {
            for (int i = 0; i < Width - 3; i++)
                robot.MoveTo(Dir);
		}

        public static void MakeVerticalMove(Robot robot, int Height, Direction Dir)
        {
            for (int i = 0; i < Height - 3; i++)
                robot.MoveTo(Dir);
		}

        public static void MoveOut(Robot robot, int Width, int Height)
        {
            MakeHorizontalMove(robot, Width, Direction.Right);
            MakeVerticalMove(robot, Height, Direction.Down);
        }
    }
}