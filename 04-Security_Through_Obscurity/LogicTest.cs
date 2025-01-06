using FluentAssertions;

namespace _04_Security_Through_Obscurity
{
  public class LogicTest
  {
    [Fact]
    public void Test1()
    {
      1.Should().Be(2);
    }
  }
}
