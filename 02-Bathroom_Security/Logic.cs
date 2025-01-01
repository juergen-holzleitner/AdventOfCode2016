using System.Text;

namespace _02_Bathroom_Security
{
  internal class Logic
  {
    internal static string GetBathroomCodeOnDiamondKeypad(string inputText) => GetBathroomCodeOnSquareKeypad(Keypad.Shape.Diamond, inputText);

    internal static string GetBathroomCodeOnSquareKeypad(string inputText) => GetBathroomCodeOnSquareKeypad(Keypad.Shape.Square, inputText);

    internal static string GetBathroomCodeOnSquareKeypad(Keypad.Shape shape, string inputText)
    {
      var input = ParseInput(inputText);
      var keypad = new Keypad(shape);

      var sb = new StringBuilder();

      foreach (var inputLine in input)
      {
        keypad.Move(inputLine);
        var ch = keypad.GetKey();
        sb.Append(ch);
      }

      return sb.ToString();
    }

    internal static IEnumerable<InputLine> ParseInput(string inputText)
    {
      return inputText
        .Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(ParseInputLine);
    }

    internal static InputLine ParseInputLine(string input)
    {
      return new(input
        .Trim()
        .ToCharArray()
        );
    }

    internal class Keypad
    {
      public Keypad(Shape shape)
      {
        this.CurrentShape = shape;
        CurrentPos = GetInitialPos();
      }

      public Shape CurrentShape { get; set; }
      public Pos CurrentPos { get; set; }

      public enum Shape
      {
        Square,
        Diamond
      }

      private Pos GetInitialPos()
      {
        return CurrentShape switch
        {
          Shape.Square => new(1, 1),
          Shape.Diamond => new(2, 0),
          _ => throw new ArgumentException("Invalid shape")
        };
      }

      internal char GetKey()
      {
        if (CurrentShape == Shape.Square)
          return GetKeyForSquare();
        else if (CurrentShape == Shape.Diamond)
          return GetKeyForDiamond();
        else
          throw new ApplicationException("Invalid shape");

      }

      private char GetKeyForDiamond()
      {
        return CurrentPos switch
        {
          { Row: 0, Col: 2 } => '1',

          { Row: 1, Col: 1 } => '2',
          { Row: 1, Col: 2 } => '3',
          { Row: 1, Col: 3 } => '4',

          { Row: 2, Col: 0 } => '5',
          { Row: 2, Col: 1 } => '6',
          { Row: 2, Col: 2 } => '7',
          { Row: 2, Col: 3 } => '8',
          { Row: 2, Col: 4 } => '9',

          { Row: 3, Col: 1 } => 'A',
          { Row: 3, Col: 2 } => 'B',
          { Row: 3, Col: 3 } => 'C',

          { Row: 4, Col: 2 } => 'D',

          _ => throw new ArgumentException("Invalid position")
        };
      }

      private char GetKeyForSquare()
      {
        return CurrentPos switch
        {
          { Row: 0, Col: 0 } => '1',
          { Row: 0, Col: 1 } => '2',
          { Row: 0, Col: 2 } => '3',
          { Row: 1, Col: 0 } => '4',
          { Row: 1, Col: 1 } => '5',
          { Row: 1, Col: 2 } => '6',
          { Row: 2, Col: 0 } => '7',
          { Row: 2, Col: 1 } => '8',
          { Row: 2, Col: 2 } => '9',
          _ => throw new ArgumentException("Invalid position")
        };
      }

      internal void Move(char ch)
      {
        Pos newPos = ch switch
        {
          'U' => new(CurrentPos.Row - 1, CurrentPos.Col),
          'D' => new(CurrentPos.Row + 1, CurrentPos.Col),
          'L' => new(CurrentPos.Row, CurrentPos.Col - 1),
          'R' => new(CurrentPos.Row, CurrentPos.Col + 1),
          _ => throw new ArgumentException("Invalid char")
        };

        if (IsOnBoard(newPos))
          CurrentPos = newPos;
      }

      private bool IsOnBoard(Pos newPos)
      {
        if (CurrentShape == Shape.Square)
          return newPos.Row >= 0 && newPos.Row <= 2 && newPos.Col >= 0 && newPos.Col <= 2;
        else if (CurrentShape == Shape.Diamond)
        {
          var distFromCenter = Math.Abs(newPos.Row - 2) + Math.Abs(newPos.Col - 2);
          return distFromCenter <= 2;
        }
        else
          throw new ApplicationException("Invalid shape");
      }

      internal void Move(InputLine inputLine)
      {
        foreach (var s in inputLine.Steps)
        {
          Move(s);
        }
      }

      public record struct Pos(int Row, int Col);
    }

    public record InputLine(IEnumerable<char> Steps);
  }
}
