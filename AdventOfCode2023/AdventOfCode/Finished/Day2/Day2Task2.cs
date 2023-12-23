using System.Text.RegularExpressions;

namespace AdventOfCode.Day2;

public class Day2Task2 : ITask
{
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            var splitOnGameNumber = line.Split(":");
            int lowestGreenNumber = -1, lowestRedNumber = -1, lowestBlueNumber = -1, colorNumber = int.MaxValue;

            var splitToPulls = splitOnGameNumber[1].Split(";");
            foreach (var pullSet in splitToPulls)
            {
                var splitToIndividualPulls = pullSet.Split(",");
                foreach (string pull in splitToIndividualPulls)
                {
                    colorNumber = ParseNumbers(pull);
                    if (pull.Contains("red", StringComparison.OrdinalIgnoreCase))
                    {
                        if (colorNumber > lowestRedNumber) lowestRedNumber = colorNumber;
                    }
                    else if (pull.Contains("green", StringComparison.OrdinalIgnoreCase))
                    {
                        if (colorNumber > lowestGreenNumber) lowestGreenNumber = colorNumber;
                    }
                    else if (pull.Contains("blue", StringComparison.OrdinalIgnoreCase))
                    {
                        if (colorNumber > lowestBlueNumber) lowestBlueNumber = colorNumber;
                    }
                }
            }

            int power = lowestBlueNumber * lowestGreenNumber * lowestRedNumber;
            totalSum += power;
            Console.WriteLine("Added line " + line + " , totalsum is now: " + totalSum);
        
                
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