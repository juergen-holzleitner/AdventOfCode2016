using _05_How_About_a_Nice_Game_of_Chess;

var doorId = File.ReadAllText("input.txt");

var password = Logic.GetPassword(doorId);
Console.WriteLine($"1. {nameof(password)}: {password}");
