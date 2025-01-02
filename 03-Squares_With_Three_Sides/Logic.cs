namespace _03_Squares_With_Three_Sides
{
  internal class Logic
  {
    internal static int GetNumValid(string inputText)
    {
      return ParseInput(inputText)
        .Count(IsTriangleValid);
    }

    internal static int GetNumValidVertically(string inputText)
    {
      return ParseInputVertically(inputText)
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

    internal static IEnumerable<Triangle> ParseInputVertically(string inputText)
    {
      var triangles = ParseInput(inputText);

      foreach (var chunk in triangles.Chunk(3))
      {
        if (chunk.Length != 3)
        {
          throw new ApplicationException("invalid chunk size");
        }

        yield return new Triangle([chunk[0].Sides[0], chunk[1].Sides[0], chunk[2].Sides[0]]);
        yield return new Triangle([chunk[0].Sides[1], chunk[1].Sides[1], chunk[2].Sides[1]]);
        yield return new Triangle([chunk[0].Sides[2], chunk[1].Sides[2], chunk[2].Sides[2]]);
      }
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
