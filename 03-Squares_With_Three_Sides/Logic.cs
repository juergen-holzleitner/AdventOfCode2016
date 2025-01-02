namespace _03_Squares_With_Three_Sides
{
  internal class Logic
  {
    internal static int GetNumValid(string inputText)
    {
      return ParseInput(inputText)
        .Count(IsTriangleValid);
    }

    internal static bool IsTriangleValid(Triangle triangle)
    {
      var sortedSides = triangle.Sides.OrderBy(x => x).ToArray();
      return sortedSides[0] + sortedSides[1] > sortedSides[2];
    }

    internal static IEnumerable<Triangle> ParseInput(string inputText)
    {
      return inputText.Split(["\n", "\r\n"], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        .Select(ParseTriangle);
    }

    internal static Triangle ParseTriangle(string input)
    {
      var sides = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(int.Parse)
        .ToArray();

      return new(sides);
    }

    public record Triangle(int[] Sides);
  }
}
