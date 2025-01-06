using _04_Security_Through_Obscurity;

var inputText = File.ReadAllText("input.txt");

var sum = Logic.GetSumOfValidRoomIds(inputText);
Console.WriteLine($"1. {nameof(sum)}: {sum}");
