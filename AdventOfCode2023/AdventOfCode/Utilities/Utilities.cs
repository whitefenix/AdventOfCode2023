using System.Text.RegularExpressions;

namespace AdventOfCode.Utilities;

public class Utilities
{
    // //Removes all entries of the specified character
    // public static string RemoveCharacterFromString(string input, string characterToRemove)
    // {
    //     return input.Where(entry => !entry.Equals(characterToRemove)).ToString();
    // }
    
    //Returns all numbers in the string as an int
    public static int ParseNumber(string numberString)
    {
        var combinedString = "";
        var matches = Regex.Matches(numberString, "[0-9]");
        foreach (var match in matches)
        {
            combinedString += match.ToString();
        }
        return int.Parse(combinedString);
    }

    public static List<int> ParseNumbersToList(IEnumerable<string> numberString)
    {
        return numberString.Select(int.Parse).ToList();
    }

    public static bool ListHasAnyValues(IEnumerable<int> input)
    {
        return input.Any(number => number != 0);
    }
}