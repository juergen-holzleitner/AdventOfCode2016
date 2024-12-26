using _01_NoTimeForATaxicab;

var input = File.ReadAllText("input.txt");

var distance = Logic.ProcessInput(input);
Console.WriteLine($"1. {nameof(distance)}: {distance}");
