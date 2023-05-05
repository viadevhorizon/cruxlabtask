using System.Diagnostics;

var numberValid = 0;
var folderPath = ".";
var fileName = "taskvalidator.txt";
var fullPath = Path.Combine(folderPath, fileName);
var splitCharacters = new[] { '-', ':', ' ' };
var sw = Stopwatch.StartNew();
using (var reader = new StreamReader(fullPath))
{
    while (!reader.EndOfStream)
    {
        var line = await reader.ReadLineAsync();
        var parts = line?.ToLower().Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
        if (parts?.Length == 4 && int.TryParse(parts?[1], out var minCount) &&
            int.TryParse(parts[2], out var maxCount) && char.TryParse(parts[0], out var symbol))
        {
            var countSymbol = parts[3].Count(s => s == symbol);
            if (countSymbol >= minCount && countSymbol <= maxCount) ++numberValid;
        }
        else
        {
            Console.Error.WriteLine("Invalid data format in the line.");
        }
    }
}
sw.Stop();
Console.WriteLine($"Count of corrected passwords = {numberValid}\n{sw.ElapsedMilliseconds} ms");