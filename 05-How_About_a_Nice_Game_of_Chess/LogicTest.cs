using FluentAssertions;

namespace _05_How_About_a_Nice_Game_of_Chess
{
  public class LogicTest
  {
    [Fact]
    public void Can_hash_nth_door_id()
    {
      string doorId = "abc";
      long n = 3231929;

      var hash = Logic.HashDoorId(doorId, n);

      hash.Should().StartWith("00000");
    }

    [Fact]
    public void Can_enumerate_password()
    {
      string doorId = "abc";

      var password = Logic.EnumeratePassword(doorId);

      string.Concat(password.Take(3)).Should().Be("18f");
    }

    [Fact]
    public void Can_get_password()
    {
      string doorId = "abc";

      var password = Logic.GetPassword(doorId);

      password.Should().Be("18f47a30");
    }
  }
}
