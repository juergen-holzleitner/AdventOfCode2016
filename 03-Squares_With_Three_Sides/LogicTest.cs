using FluentAssertions;

namespace _03_Squares_With_Three_Sides
{
  public class LogicTest
  {
    const string inputText = """
        541  588  421
        827  272  126
        660  514  367
         39  703  839

        """;

    [Fact]
    public void Can_parse_triangle()
    {
      var input = "5 10 25";

      var triangle = Logic.ParseTriangle(input);

      triangle.Sides.Should().Equal([5, 10, 25]);
    }

    [Fact]
    public void Can_parse_input()
    {
      var triangles = Logic.ParseInput(inputText);

      triangles.Should().HaveCount(4);
    }

    [Fact]
    public void Can_check_valid_triangle()
    {
      var triangle = new Logic.Triangle([5, 10, 25]);

      var isValid = Logic.IsTriangleValid(triangle);

      isValid.Should().BeFalse();
    }

    [Fact]
    public void Can_get_num_valid()
    {
      var numValid = Logic.GetNumValid(inputText);

      numValid.Should().Be(2);
    }

  }
}
