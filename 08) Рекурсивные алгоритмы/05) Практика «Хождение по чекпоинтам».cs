using System;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] Checkpoints)
        {
            var NumberOfCheckpoints = Checkpoints.Length;
            var ShortestPath = new int[NumberOfCheckpoints];
            var CurrentPath = new int[NumberOfCheckpoints];
            var ShortestDistance = double.PositiveInfinity;

            GeneratePathPermutations(Checkpoints, ShortestPath, CurrentPath, 1, ShortestDistance);

            return ShortestPath;
        }

        private static double GeneratePathPermutations(Point[] Checkpoints, int[] ShortestPath,
            int[] CurrentPath, int Position, double ShortestDistance)
        {
            var NumberOfCheckpoints = Checkpoints.Length;
            var CurrentDistance = Checkpoints.GetPathLength(CurrentPath);

            if (Position == CurrentPath.Length && CurrentDistance < ShortestDistance)
            {
                ShortestDistance = CurrentDistance;
                Array.Copy(CurrentPath, ShortestPath, NumberOfCheckpoints);
                return ShortestDistance;
            }

            for (var I = 0; I < NumberOfCheckpoints; I++)
            {
                var Index = Array.IndexOf(CurrentPath, I, 0, Position);

                if (Index == -1)
                {
                    CurrentPath[Position] = I;
                    ShortestDistance = GeneratePathPermutations(Checkpoints, ShortestPath, CurrentPath,
                        Position + 1, ShortestDistance);
                }
            }

            return ShortestDistance;
        }
    }
}