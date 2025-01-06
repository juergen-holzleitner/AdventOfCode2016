using FluentAssertions;

namespace _04_Security_Through_Obscurity
{
  public class LogicTest
  {
    const string inputText = """
        aaaaa-bbb-z-y-x-123[abxyz]
        a-b-c-d-e-f-g-h-987[abcde]
        not-a-real-room-404[oarel]
        totally-real-room-200[decoy]

        """;

    [Fact]
    public void Can_parse_room()
    {
      var line = "aaaaa-bbb-z-y-x-123[abxyz]";

      var room = Logic.ParseRoom(line);

      room.CodeParts.Should().Equal(["aaaaa", "bbb", "z", "y", "x"]);
      room.Id.Should().Be(123);
      room.Checksum.Should().Be("abxyz");
    }

    [Fact]
    public void Can_generate_checksum_of_codeparts()
    {
      var codeParts = new List<string> { "aaaaa", "bbb", "z", "y", "x" };
      var checksum = Logic.GenerateChecksum(codeParts);
      checksum.Should().Be("abxyz");
    }

    [Theory]
    [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", true)]
    [InlineData("a-b-c-d-e-f-g-h-987[abcde]", true)]
    [InlineData("not-a-real-room-404[oarel]", true)]
    [InlineData("totally-real-room-200[decoy]", false)]
    public void Can_check_room_valid(string line, bool expectedValid)
    {
      var room = Logic.ParseRoom(line);
      var isValid = Logic.IsRoomValid(room);
      isValid.Should().Be(expectedValid);
    }

    [Fact]
    public void Can_parse_input()
    {
      var rooms = Logic.ParseInput(inputText);

      rooms.Should().HaveCount(4);
    }

    [Fact]
    public void Can_get_sum_of_valid_room_ids()
    {
      var sum = Logic.GetSumOfValidRoomIds(inputText);
      sum.Should().Be(1514);
    }

  }
}
