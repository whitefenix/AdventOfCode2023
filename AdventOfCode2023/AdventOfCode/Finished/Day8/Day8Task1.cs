namespace AdventOfCode.Day8;

public class Day8Task1 : ITask
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
        
        while (line != null)
        {
            line = line.Replace(" ", ""); //Remove spaces
            var splitLine = line.Split("=");
            splitLine[1] = splitLine[1].Replace("(", "");
            splitLine[1] = splitLine[1].Replace(")", ""); //Remove parenthesis
            var leftAndRight = splitLine[1].Split(",");
            nodeToRightLeftSet.Add(splitLine[0], leftAndRight);
            
            line = sr.ReadLine();
        }

        var currentString = "AAA";
        var stepsTaken = 0;

        while (!currentString.Equals("ZZZ"))
        {
            foreach (char currentChar in rightLeftLine)
            {
                currentString = currentChar == 'L' ? nodeToRightLeftSet[currentString][0] : //Go left
                    nodeToRightLeftSet[currentString][1]; //Go right

                stepsTaken++;
            }
        }
        
        
        Console.WriteLine("Steps taken is: " + stepsTaken);
    }
}