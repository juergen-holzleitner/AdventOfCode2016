using _03_Squares_With_Three_Sides;

var inputText = File.ReadAllText("input.txt");

var numValid = Logic.GetNumValid(inputText);
Console.WriteLine($"{nameof(numValid)}: {numValid}");

var numValidVertically = Logic.GetNumValidVertically(inputText);
Console.WriteLine($"{nameof(numValidVertically)}: {numValidVertically}");
