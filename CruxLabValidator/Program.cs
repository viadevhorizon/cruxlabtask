


using System.Diagnostics;
var numberOfValidPasswords = 0;
var pathToFolder = ".";
var fileNameAndExtension = "taskvalidator.txt";
var fullPathToFile = Path.Combine(pathToFolder, fileNameAndExtension);
var splitSymbols = new char[] { '-', ':', ' ' };
var timer = Stopwatch.StartNew();
using (var reader = new StreamReader(fullPathToFile))
{
    while (!reader.EndOfStream)
    {
        var fileLine = await reader.ReadLineAsync();
        var partsOfFileLine = fileLine?.ToLower().Split(splitSymbols, StringSplitOptions.RemoveEmptyEntries);
        if (partsOfFileLine?.Length == 4 && int.TryParse(partsOfFileLine?[1], out var minimumNumberOfSymbol) &&
            int.TryParse(partsOfFileLine[2], out var maximumNumberOfSymbol) &&
            char.TryParse(partsOfFileLine[0], out var requiredSymbol))
        {
            var countSymbol = partsOfFileLine[3].Count(s => s == requiredSymbol);
            if (countSymbol >= minimumNumberOfSymbol && countSymbol <= maximumNumberOfSymbol) ++numberOfValidPasswords;
        }
        else
        {
            Console.Error.WriteLine("Invalid data format in the line.");
        }
    }
}
timer.Stop();
Console.WriteLine($"Count of corrected passwords = {numberOfValidPasswords}\n{timer.ElapsedMilliseconds} ms");