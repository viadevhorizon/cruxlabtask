using System.Diagnostics;

// Define a constant that specifies the expected number of parts per line of the file
const int ExpectedNumberOfParts = 4;

// Initialize variables
var numberOfValidPasswords = 0;
var pathToFolder = ".";
var fileNameAndExtension = "taskvalidator.txt";
var fullPathToFile = Path.Combine(pathToFolder, fileNameAndExtension);
// Define an array of characters used to split each line of the file into parts
var splitSymbols = new[] { '-', ':', ' ' };
var timer = Stopwatch.StartNew();

if (!File.Exists(fullPathToFile))
{
    Console.Error.WriteLine($"File {fullPathToFile} not exists");
    Environment.Exit(1);
}

using (var reader = new StreamReader(fullPathToFile))
{
    // Read each line of the file until the end
    while (!reader.EndOfStream)
    {
        var fileLine = await reader.ReadLineAsync();
        // Split the line into parts based on certain symbols
        var partsOfFileLine = fileLine?.ToLower().Split(splitSymbols, StringSplitOptions.RemoveEmptyEntries);
        // Check if the line has the expected number of parts and if each part can be parsed to the correct data type
        int minimumNumberOfSymbol;
        int maximumNumberOfSymbol;
        char requiredSymbol;
        /*
        Check if the line has the expected number of parts and if each part can be parsed to the correct data type
        The first part should be a single character, the second and third parts should be integers,
        and the fourth part should be a string.
        */
        if (partsOfFileLine?.Length != ExpectedNumberOfParts ||
            !int.TryParse(partsOfFileLine[1], out minimumNumberOfSymbol) ||
            !int.TryParse(partsOfFileLine[2], out maximumNumberOfSymbol) ||
            !char.TryParse(partsOfFileLine[0], out requiredSymbol) ||
            // If the minimum is greater than the maximum, skip to the next line.
            minimumNumberOfSymbol > maximumNumberOfSymbol)
        {
            Console.Error.WriteLine("File line has invalid content format");
            continue;
        }

        // Count the number of occurrences of the required symbol in the fourth part of the line.
        var requiredSymbolCount = partsOfFileLine[3].Count(s => s == requiredSymbol);

        // Сheck if the number of occurrences of a required character in a string is within a given range
        if (requiredSymbolCount >= minimumNumberOfSymbol && requiredSymbolCount <= maximumNumberOfSymbol)
            ++numberOfValidPasswords;
    }
}

timer.Stop();
Console.WriteLine($"Number of valid passwords = {numberOfValidPasswords}\n{timer.ElapsedMilliseconds} ms");