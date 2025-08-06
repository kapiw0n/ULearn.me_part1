using Avalonia.Input;
using Digger.Architecture;
using System;
namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";

        public int GetDrawingPriority() => 100;

        public CreatureCommand Act(int X, int Y) => new()
        {
            DeltaX = 0,
            DeltaY = 0,
            TransformTo = this
        };

        public bool DeadInConflict(ICreature ConflictedObject) => ConflictedObject is Player;
    }

    public class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";

        public int GetDrawingPriority() => 0;

        public CreatureCommand Act(int X, int Y)
        {
            CreatureCommand diggerCommand = new()
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = this
            };

            switch (Game.KeyPressed)
            {
                case Key.Up:
                    if (IsMoveValid(X, Y - 1))
                        diggerCommand.DeltaY--;
                    break;
                case Key.Down:
                    if (IsMoveValid(X, Y + 1))
                        diggerCommand.DeltaY++;
                    break;
                case Key.Right:
                    if (IsMoveValid(X + 1, Y))
                        diggerCommand.DeltaX++;
                    break;
                case Key.Left:
                    if (IsMoveValid(X - 1, Y))
                        diggerCommand.DeltaX--;
                    break;
            }
            return diggerCommand;
        }

        private static bool IsMoveValid(int TargetX, int TargetY)
        {
            return TargetX >= 0 &&
                   TargetX < Game.MapWidth &&
                   TargetY >= 0 &&
                   TargetY < Game.MapHeight;
        }

        public static bool CanGoTo(int X, int Y)
        {
            return X < 0 || Y < 0 ||
                X >= Game.MapWidth ||
                Y >= Game.MapHeight;
            return true;
		}

        public bool DeadInConflict(ICreature ConflictedObject) 
            => false;
    }
}