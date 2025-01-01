using System.Text;

namespace _02_Bathroom_Security
{
  internal class Logic
  {
    internal static string GetBathroomCode(string inputText)
    {
      var input = ParseInput(inputText);
      var keypad = new Keypad();

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
      public Keypad()
      {
      }

      public Pos CurrentPos { get; set; } = new(1, 1);

      internal char GetKey()
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

      private static bool IsOnBoard(Pos newPos)
      {
        return newPos.Row >= 0 && newPos.Row <= 2 && newPos.Col >= 0 && newPos.Col <= 2;
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
