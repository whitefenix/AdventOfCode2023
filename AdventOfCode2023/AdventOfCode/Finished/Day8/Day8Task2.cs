using System.Linq;

namespace AdventOfCode.Day8;

public class Day8Task2 : ITask
{
    private Dictionary<string, string[]> nodeToRightLeftSet = new(); //left = [0], right = [1]
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();

        var rightLeftLine = line;

        sr.ReadLine(); //Skip empty line
        line = sr.ReadLine();

        var startingPoints = new List<string>();
        
        //Read and parse input
        while (line != null)
        {
            line = line.Replace(" ", ""); //Remove spaces
            var splitLine = line.Split("=");
            splitLine[1] = splitLine[1].Replace("(", "");
            splitLine[1] = splitLine[1].Replace(")", ""); //Remove parenthesis
            var leftAndRight = splitLine[1].Split(",");
            nodeToRightLeftSet.Add(splitLine[0], leftAndRight);

            if (splitLine[0].Last().Equals('A'))
            {
                startingPoints.Add(splitLine[0]);
            }
            
            line = sr.ReadLine();
        }
        
        var stepsTaken = 0;
        var pathLengths = new List<long>();
        
        //Find lengths of each path
        for (int i = 0; i < startingPoints.Count(); i++)
        {
            var currentString = "AAA";
            stepsTaken = 0;
            while (!currentString.Last().Equals('Z'))
            {
                foreach (char currentChar in rightLeftLine)
                {
                    if (currentChar.Equals('L'))
                    {
                        currentString = nodeToRightLeftSet[startingPoints[i]][0];
                    }
                    else
                    {
                        currentString = nodeToRightLeftSet[startingPoints[i]][1];
                    }

                    startingPoints[i] = currentString;
                    stepsTaken++;

                    if (currentString.Last().Equals('Z')) break;
                }
            }
            pathLengths.Add(stepsTaken);
        }

        Console.Write("LCM: " + GetLowestCommonMultipleOfList(pathLengths.ToArray()));

      
    }
    
    private long GetLowestCommonMultipleOfList(long[] numbers)
    {
        return numbers.Aggregate(GetLowestCommonMultiple);
    }
    
    private long GetLowestCommonMultiple(long a, long b)
    {
        return Math.Abs(a * b) / GetGreatestCommonDivisor(a, b);
    }
    
    private long GetGreatestCommonDivisor(long a, long b)
    {
        return b == 0 ? a : GetGreatestCommonDivisor(b, a % b);
    }
}