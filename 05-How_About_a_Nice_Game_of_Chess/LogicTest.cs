using FluentAssertions;

namespace _05_How_About_a_Nice_Game_of_Chess
{
  public class LogicTest
  {
    [Fact]
    public void Can_enumerate_password()
    {
      string doorId = "abc";

      var password = Logic.EnumeratePassword(doorId);

      password.Take(3).Select(c => c.Char5).Should().Equal([0x1, 0x8, 0xf]);
    }

    [Fact]
    public void Can_get_password()
    {
      string doorId = "abc";

      var password = Logic.GetPassword(doorId);

      password.Should().Be("18f47a30");
    }

    [Fact]
    public void Can_get_password2()
    {
      string doorId = "abc";
      var password = Logic.GetPassword2(doorId);
      password.Should().Be("05ace8e3");
    }
  }
}
