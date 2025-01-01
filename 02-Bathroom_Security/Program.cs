using _02_Bathroom_Security;

var inputText = File.ReadAllText("input.txt");

var bathroomCode = Logic.GetBathroomCode(inputText);
Console.WriteLine($"1. {nameof(bathroomCode)}: {bathroomCode}");
