﻿namespace _01_NoTimeForATaxicab
{
  internal class Logic
  {
    internal static Step ParseStep(string input)
    {
      var action = GetAction(input[0]);
      var distance = GetDistance(input[1..]);
      return new(action, distance);
    }

    private static int GetDistance(string str)
    {
      return int.Parse(str);
    }

    private static Action GetAction(char ch)
    {
      return ch switch
      {
        'R' => Action.TurnRight,
        'L' => Action.TurnLeft,
        _ => throw new ArgumentException("Invalid char")
      };
    }

    internal static IEnumerable<Step> ParseInput(string input)
    {
      return input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(ParseStep);
    }

    internal static int ProcessInput(string input)
    {
      var steps = ParseInput(input);
      var pos = new Pos();
      foreach (var step in steps)
      {
        pos.DoStep(step);
      }
      return pos.GetManhattanDistance();
    }

    public enum Action
    {
      TurnRight,
      TurnLeft
    }

    public record Step(Action Action, int Distance);

    public class Pos
    {
      public int X { get; set; } = 0;
      public int Y { get; set; } = 0;

      public enum Direction
      {
        North,
        East,
        South,
        West
      }

      public Direction CurrentDirection { get; set; } = Direction.North;

      internal void DoStep(Step step)
      {
        CurrentDirection = GetNewDirection(CurrentDirection, step.Action);
        DoMove(step);
      }

      private void DoMove(Step step)
      {
        switch (CurrentDirection)
        {
          case Direction.North:
            Y += step.Distance;
            break;
          case Direction.East:
            X += step.Distance;
            break;
          case Direction.South:
            Y -= step.Distance;
            break;
          case Direction.West:
            X -= step.Distance;
            break;
          default:
            throw new ArgumentException("Invalid direction");
        }
      }

      private static Direction GetNewDirection(Direction currentDirection, Action action)
      {
        return (currentDirection, action) switch
        {
          (Direction.North, Action.TurnRight) => Direction.East,
          (Direction.North, Action.TurnLeft) => Direction.West,
          (Direction.East, Action.TurnRight) => Direction.South,
          (Direction.East, Action.TurnLeft) => Direction.North,
          (Direction.South, Action.TurnRight) => Direction.West,
          (Direction.South, Action.TurnLeft) => Direction.East,
          (Direction.West, Action.TurnRight) => Direction.North,
          (Direction.West, Action.TurnLeft) => Direction.South,
          _ => throw new ArgumentException("Invalid direction")
        };
      }

      internal int GetManhattanDistance()
      {
        return Math.Abs(X) + Math.Abs(Y);
      }
    }
  }

}