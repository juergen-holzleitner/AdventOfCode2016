using _02_Bathroom_Security;

var inputText = File.ReadAllText("input.txt");

var bathroomCodeOnSquare = Logic.GetBathroomCodeOnSquareKeypad(inputText);
Console.WriteLine($"1. {nameof(bathroomCodeOnSquare)}: {bathroomCodeOnSquare}");

var bathroomCodeOnDiamond = Logic.GetBathroomCodeOnDiamondKeypad(inputText);
Console.WriteLine($"2. {nameof(bathroomCodeOnDiamond)}: {bathroomCodeOnDiamond}");
