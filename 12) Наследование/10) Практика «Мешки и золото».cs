using Avalonia.Input;
using Digger.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                   TargetY < Game.MapHeight &&
                   Game.Map[TargetX, TargetY] is not Sack;
        }

        public static bool CanGoTo(int X, int Y)
        {
            if (X < 0 || Y < 0 ||
                X >= Game.MapWidth ||
                Y >= Game.MapHeight)
                return false;

            return Game.Map.GetValue(X, Y) is not Sack;
        }

        public bool DeadInConflict(ICreature ConflictedObject) 
            => ConflictedObject is Sack;
    }

    public class Sack : ICreature
    {
        public int FlightTime;

        public string GetImageFileName() => "Sack.png";

        public int GetDrawingPriority() => 10;

        public CreatureCommand Act(int X, int Y)
        {
            var command = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 1,
                TransformTo = this
            };

            if (CanFallTo(X + command.DeltaX, Y + command.DeltaY))
            {
                FlightTime++;
            }
            else
            {
                if (FlightTime > 1)
                    command.TransformTo = new Gold();

                FlightTime = 0;
                command.DeltaY = 0;
            }
            return command;
        }

        public bool CanFallTo(int X, int Y)
        {
            if (X < 0 ||
                Y < 0 ||
                X >= Game.MapWidth ||
                Y >= Game.MapHeight)
                return false;

            var cell = Game.Map.GetValue(X, Y);
            return cell == null ||
                   (cell is Player) && FlightTime > 0;
        }

        public bool DeadInConflict(ICreature ConflictedObject) => false;
    }

    public class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";

        public int GetDrawingPriority() => 10;

        public CreatureCommand Act(int X, int Y) => new()
        {
            DeltaX = 0,
            DeltaY = 0,
            TransformTo = this
        };

        public bool DeadInConflict(ICreature ConflictedObject)
        {
            if (ConflictedObject is Player)
                Game.Scores += 10;

            return true;
        }
    }
}