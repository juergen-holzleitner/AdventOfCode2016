using System.Text.RegularExpressions;

namespace _04_Security_Through_Obscurity
{
  internal partial class Logic
  {
    internal static Room ParseRoom(string line)
    {
      var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
      var roomData = parts[^1];

      var regex = regexRoomData();
      var match = regex.Match(roomData);
      if (!match.Success)
      {
        throw new ApplicationException("Invalid room data");
      }

      var id = int.Parse(match.Groups["id"].Value);
      var checksum = match.Groups["checksum"].Value;
      return new([.. parts[..^1]], id, checksum);
    }

    public record Room(List<string> CodeParts, int Id, string Checksum);

    [GeneratedRegex(@"(?<id>\d+)\[(?<checksum>\w+)\]")]
    private static partial Regex regexRoomData();

    internal static string GenerateChecksum(List<string> codeParts)
    {
      var charCount = codeParts.SelectMany(x => x).GroupBy(x => x).Select(x => new { Char = x.Key, Count = x.Count() });
      var ordered = charCount.OrderByDescending(x => x.Count).ThenBy(x => x.Char);
      return string.Join("", ordered.Select(x => x.Char).Take(5));
    }

    internal static bool IsRoomValid(Room room)
    {
      return room.Checksum == GenerateChecksum(room.CodeParts);
    }

    internal static IEnumerable<Room> ParseInput(string inputText)
    {
      return inputText
        .Split(["\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(ParseRoom);
    }

    internal static long GetSumOfValidRoomIds(string inputText)
    {
      var room = ParseInput(inputText);
      return room.Where(IsRoomValid).Sum(x => (long)x.Id);
    }
  }
}
