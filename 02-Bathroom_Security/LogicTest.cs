using FluentAssertions;

namespace _02_Bathroom_Security
{
  public class LogicTest
  {
    const string inputText = """
        ULL
        RRDDD
        LURDL
        UUUUD

        """;

    [Fact]
    public void Can_parse_instruction()
    {
      var input = "ULL";

      var instruction = Logic.ParseInputLine(input);

      instruction.Steps.Should().Equal('U', 'L', 'L');
    }

    [Fact]
    public void Can_parse_input()
    {
      var input = Logic.ParseInput(inputText);

      input.Should().HaveCount(4);
    }

    [Fact]
    public void Can_create_initial_keypad()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Square);

      keypad.CurrentPos.Should().Be(new Logic.Keypad.Pos(1, 1));
    }

    [Fact]
    public void Can_create_initial_keypad_of_diamond_shape()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Diamond);

      keypad.CurrentPos.Should().Be(new Logic.Keypad.Pos(2, 0));
    }

    [Fact]
    public void Can_move_on_keypad()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Square);

      keypad.Move('U');

      keypad.CurrentPos.Should().Be(new Logic.Keypad.Pos(0, 1));
    }

    [Fact]
    public void Can_not_move_out()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Square);

      keypad.Move('U');
      keypad.Move('U');

      keypad.CurrentPos.Should().Be(new Logic.Keypad.Pos(0, 1));
    }

    [Fact]
    public void Can_move_InputLine()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Square);
      var inputLine = new Logic.InputLine(['U', 'L', 'L']);

      keypad.Move(inputLine);

      keypad.CurrentPos.Should().Be(new Logic.Keypad.Pos(0, 0));
    }

    [Fact]
    public void Can_move_InputLine_on_diamond_shape()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Diamond);
      var inputLine = new Logic.InputLine(['U', 'L', 'L']);

      keypad.Move(inputLine);

      keypad.CurrentPos.Should().Be(new Logic.Keypad.Pos(2, 0));
    }

    [Fact]
    public void Can_get_curren_key()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Square);

      var key = keypad.GetKey();

      key.Should().Be('5');
    }

    [Fact]
    public void Can_get_curren_key_on_diamond_shape()
    {
      var keypad = new Logic.Keypad(Logic.Keypad.Shape.Diamond);
      keypad.Move('R');
      keypad.Move('R');

      var key = keypad.GetKey();

      key.Should().Be('7');
    }

    [Fact]
    public void Can_get_bathroom_code()
    {
      var bathroomCode = Logic.GetBathroomCodeOnSquareKeypad(inputText);

      bathroomCode.Should().Be("1985");
    }

    [Fact]
    public void Can_get_bathroom_code_on_diamond_shape()
    {
      var bathroomCode = Logic.GetBathroomCodeOnDiamondKeypad(inputText);

      bathroomCode.Should().Be("5DB3");
    }

  }
}
