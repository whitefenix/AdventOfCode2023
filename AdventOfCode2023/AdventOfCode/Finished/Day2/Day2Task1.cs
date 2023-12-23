using System.Text.RegularExpressions;

namespace AdventOfCode.Day2;

public class Day2Task1 : ITask
{
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            var splitOnGameNumber = line.Split(":");
            var gameNumber = ParseNumbers(splitOnGameNumber[0]);
            int highestGreenNumber = 0, highestRedNumber = 0, highestBlueNumber = 0, colorNumber = 0;

            var splitToPulls = splitOnGameNumber[1].Split(";");
            foreach (var pullSet in splitToPulls)
            {
                var splitToIndividualPulls = pullSet.Split(",");
                foreach (string pull in splitToIndividualPulls)
                {
                    colorNumber = ParseNumbers(pull);
                    if (pull.Contains("red", StringComparison.OrdinalIgnoreCase))
                    {
                        if (colorNumber > highestRedNumber) highestRedNumber = colorNumber;
                    }
                    else if (pull.Contains("green", StringComparison.OrdinalIgnoreCase))
                    {
                        if (colorNumber > highestGreenNumber) highestGreenNumber = colorNumber;
                    }
                    else if (pull.Contains("blue", StringComparison.OrdinalIgnoreCase))
                    {
                        if (colorNumber > highestBlueNumber) highestBlueNumber = colorNumber;
                    }
                }
            }

            if (highestRedNumber <= 12 && highestBlueNumber <= 14 && highestGreenNumber <= 13)
            {
                totalSum += gameNumber;
                Console.WriteLine("Added line " + line + " , totalsum is now: " + totalSum);
            }
                
            line = sr.ReadLine();
        }
        Console.WriteLine("Total sum is: " + totalSum);
    }

    private static int ParseNumbers(string numberString)
    {
        var combinedString = "";
        var matches = Regex.Matches(numberString, "[0-9]");
        foreach (var match in matches)
        {
            combinedString += match.ToString();
        }
        return int.Parse(combinedString);
    }
}