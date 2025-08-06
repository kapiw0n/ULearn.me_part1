using Avalonia.Input;
using Digger.Architecture;
namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";

        public int GetDrawingPriority() => 100;

        public CreatureCommand Act(int positionX, int positionY) => new()
        {
            DeltaX = 0,
            DeltaY = 0,
            TransformTo = this
        };

        public bool DeadInConflict(ICreature otherCreature) => otherCreature is Player;
    }

    public class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";

        public int GetDrawingPriority() => 0;

        public CreatureCommand Act(int positionX, int positionY)
        {
            var playerCommand = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = this
            };

            switch (Game.KeyPressed)
            {
                case Key.Up:
                    if (IsMoveValid(positionX, positionY - 1))
                        playerCommand.DeltaY--;
                    break;
                case Key.Down:
                    if (IsMoveValid(positionX, positionY + 1))
                        playerCommand.DeltaY++;
                    break;
                case Key.Right:
                    if (IsMoveValid(positionX + 1, positionY))
                        playerCommand.DeltaX++;
                    break;
                case Key.Left:
                    if (IsMoveValid(positionX - 1, positionY))
                        playerCommand.DeltaX--;
                    break;
            }
            return playerCommand;
        }

        private static bool IsMoveValid(int targetX, int targetY)
        {
            return targetX >= 0 &&
                   targetX < Game.MapWidth &&
                   targetY >= 0 &&
                   targetY < Game.MapHeight &&
                   Game.Map[targetX, targetY] is not Sack;
        }

        public static bool CanGoTo(int targetX, int targetY)
        {
            return targetX >= 0 && targetY >= 0 &&
                   targetX < Game.MapWidth && targetY < Game.MapHeight &&
                   Game.Map.GetValue(targetX, targetY) is not Sack;
        }

        public bool DeadInConflict(ICreature otherCreature) 
            => otherCreature is Sack || otherCreature is Monster;
    }

    public class Sack : ICreature
    {
        public int FallTime;

        public string GetImageFileName() => "Sack.png";

        public int GetDrawingPriority() => 10;

        public CreatureCommand Act(int positionX, int positionY)
        {
            var sackCommand = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 1,
                TransformTo = this
            };

            if (CanFallTo(positionX + sackCommand.DeltaX, positionY + sackCommand.DeltaY))
            {
                FallTime++;
            }
            else
            {
                if (FallTime > 1)
                    sackCommand.TransformTo = new Gold();

                FallTime = 0;
                sackCommand.DeltaY = 0;
            }
            return sackCommand;
        }

        public bool CanFallTo(int targetX, int targetY)
        {
            if (targetX < 0 || targetY < 0 || 
                targetX >= Game.MapWidth || targetY >= Game.MapHeight)
                return false;

            var cell = Game.Map.GetValue(targetX, targetY);
            return cell == null || 
                   (cell is Monster || cell is Player) && FallTime > 0;
        }

        public bool DeadInConflict(ICreature otherCreature) => false;
    }

    public class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";

        public int GetDrawingPriority() => 10;

        public CreatureCommand Act(int positionX, int positionY) => new()
        {
            DeltaX = 0,
            DeltaY = 0,
            TransformTo = this
        };

        public bool DeadInConflict(ICreature otherCreature)
        {
            if (otherCreature is Player)
                Game.Scores += 10;

            return true;
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName() => "Monster.png";

        public int GetDrawingPriority() => 20;

        public CreatureCommand Act(int positionX, int positionY)
        {
            var monsterCommand = new CreatureCommand
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = this
            };

            if (IsPlayerInSection(0, 0, positionX, Game.MapHeight) &&
				CanGoTo(positionX - 1, positionY)
				&& CanGoTo(positionX - 1, positionY))
				monsterCommand.DeltaX = -1;
            else if (IsPlayerInSection(positionX + 1, 0, Game.MapWidth, Game.MapHeight)
					 && CanGoTo(positionX + 1, positionY))
                monsterCommand.DeltaX = 1;
            else if (IsPlayerInSection(0, 0, Game.MapWidth, positionY)
					 && CanGoTo(positionX, positionY - 1))
                monsterCommand.DeltaY = -1;
            else if (IsPlayerInSection(0, positionY + 1, Game.MapWidth, Game.MapHeight)
					 && CanGoTo(positionX, positionY + 1))
                monsterCommand.DeltaY = 1;
            return monsterCommand;
        }

        private bool IsPlayerInSection(int startX, int startY, int endX, int endY)
        {
            for (var x = startX; x < endX; x++)
                for (var y = startY; y < endY; y++)
                    if (Game.Map.GetValue(x, y) is Player)
                        return true;

            return false;
        }

        private bool CanGoTo(int targetX, int targetY)
        {
            if (targetX < 0 || targetY < 0 || targetX >= Game.MapWidth || targetY >= Game.MapHeight)
                return false;

            var cell = Game.Map.GetValue(targetX, targetY);
            return cell == null || !(cell is Sack || cell is Monster || cell is Terrain);
        }

        public bool DeadInConflict(ICreature otherCreature) 
            => otherCreature is Monster || 
               (otherCreature is Sack && ((Sack)otherCreature).FallTime > 0);
    }
}