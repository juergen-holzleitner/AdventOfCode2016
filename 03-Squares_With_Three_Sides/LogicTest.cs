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

    const string inputTextPart2 = """
      101 301 501
      102 302 502
      103 303 503
      201 401 601
      202 402 602
      203 403 603

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

    [Fact]
    public void Can_parse_triangles_vertically()
    {
      var triangles = Logic.ParseInputVertically(inputTextPart2);
      triangles.Should().HaveCount(6);
    }

    [Fact]
    public void Can_get_num_valid_vertically()
    {
      var numValid = Logic.GetNumValidVertically(inputTextPart2);

      numValid.Should().Be(6);
    }

  }
}
