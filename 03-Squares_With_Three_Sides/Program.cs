using _03_Squares_With_Three_Sides;

var inputText = File.ReadAllText("input.txt");

var numValid = Logic.GetNumValid(inputText);
Console.WriteLine($"{nameof(numValid)}: {numValid}");
