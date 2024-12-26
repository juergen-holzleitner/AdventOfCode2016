using FluentAssertions;

namespace _01_NoTimeForATaxicab
{
  public class LogicTest
  {
    [Fact]
    public void Can_parse_step()
    {
      var input = "R2";

      var step = Logic.ParseStep(input);

      step.Action.Should().Be(Logic.Action.TurnRight);
      step.Distance.Should().Be(2);
    }

    [Fact]
    public void Can_parse_input()
    {
      var input = "R2, L3";

      var steps = Logic.ParseInput(input);

      steps.Should().HaveCount(2);
    }

    [Fact]
    public void Can_get_initial_Position()
    {
      var pos = new Logic.Pos();

      pos.X.Should().Be(0);
      pos.Y.Should().Be(0);

      pos.CurrentDirection.Should().Be(Logic.Pos.Direction.North);
    }

    [Fact]
    public void Can_do_one_step()
    {
      var pos = new Logic.Pos();
      var step = new Logic.Step(Logic.Action.TurnRight, 2);

      pos.DoStep(step);

      pos.CurrentDirection.Should().Be(Logic.Pos.Direction.East);

      pos.X.Should().Be(2);
      pos.Y.Should().Be(0);
    }

    [Theory]
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    public void Can_process_input(string input, int expectedDistance)
    {
      var distance = Logic.ProcessInput(input);

      distance.Should().Be(expectedDistance);
    }

    [Fact]
    public void Can_get_distance_to_visted()
    {
      var input = "R8, R4, R4, R8";

      var distance = Logic.ProcessInputAndStopAtVisited(input);

      distance.Should().Be(4);
    }
  }
}
