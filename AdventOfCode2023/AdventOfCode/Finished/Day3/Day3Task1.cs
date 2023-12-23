using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public class Day3Task1 : ITask
{
    public void RunTask()
    {
        var lastLine = "";
        var currentLine = "";
        var nextLine = "";
        
        int totalSum = 0;

        StreamReader sr = new StreamReader("../../../input.txt");

        currentLine = sr.ReadLine();
        nextLine = sr.ReadLine();

    while (currentLine != null)
        {
            if (currentLine == null)
            {
                Console.WriteLine("Error: CurrentLine is null");
                break;
            }
            
            //find number indexes for line
            var indexList = GetIndexes(currentLine);

            //check previous line
            for (int i = 0; i < indexList.Count-1; i += 2)
            {
                List<int> indexPair = new List<int>() { indexList[i], indexList[i + 1] };
                if (AdjacentToSymbol(indexPair, currentLine))
                {
                    var addNumber = ParseNumbers(indexPair, currentLine);
                    totalSum += addNumber;
                    Console.WriteLine("Add number " + addNumber + " to totalSum " + totalSum);
                    continue;
                }

                if (!lastLine.Equals(""))
                {
                    if (AdjacentToSymbol(indexPair, lastLine))
                    {
                        var addNumber = ParseNumbers(indexPair, currentLine);
                        totalSum += addNumber;
                        Console.WriteLine("Add number " + addNumber + " to totalSum " + totalSum);
                        continue;
                    }
                }

                if (null == nextLine || nextLine.Equals("")) continue;
                
                if (AdjacentToSymbol(indexPair, nextLine))
                {
                    var addNumber = ParseNumbers(indexPair, currentLine);
                    totalSum += addNumber;
                    Console.WriteLine("Add number " + addNumber + " to totalSum " + totalSum);
                }
            }
            
            lastLine = currentLine;
            currentLine = nextLine;
            nextLine = sr.ReadLine();
        }
        Console.WriteLine("Total sum is: " + totalSum);
    }

    private int ParseNumbers(List<int> inputIndexes, string inputString)
    {
        var numberString = inputString.Substring(inputIndexes[0], inputIndexes[1]-inputIndexes[0]+1);
        return int.Parse(numberString);
    }

    //Send a pair of indexes and the string to check them on
    private static bool AdjacentToSymbol(List<int> inputIndexes, string inputString)
    {
        var symbolPattern = "[^.0-9]";

        int startIndex = inputIndexes[0], endIndex = inputIndexes[1];
        
        if (inputIndexes[0] != 0) //If we can
        {
            startIndex--; //Reduce start index by one
        }

        if (inputIndexes[1] != inputString.Length-1) //If we can
        {
            endIndex++; //Increase end index by one
        }

        var length = (endIndex - startIndex) + 1;
        if (length >= inputString.Length) length = inputString.Length - 1;
        return Regex.IsMatch(inputString.Substring(startIndex, length), symbolPattern);
    }
    
    
    private static List<int> GetIndexes(string input)
    {
        var returnIndexes = new List<int>();
        var numberPattern = "[0-9]";
        var currentlyWritingIndex = false;
        
        for (int i = 0; i < input.Length; i++)
        {
            if (Regex.IsMatch(input.Substring(i, 1), numberPattern)) //if a number
            {
                if (!currentlyWritingIndex) //if on a number
                {
                    currentlyWritingIndex = true; 
                    returnIndexes.Add(i); //add start-index to list
                }

                if (currentlyWritingIndex && i == input.Length - 1) //if the last character is a number
                {
                    currentlyWritingIndex = false;
                    returnIndexes.Add(i);
                }
            }
            else //Not a number
            {
                if (currentlyWritingIndex)
                {
                    currentlyWritingIndex = false;
                    returnIndexes.Add(i-1);
                }
            }  
        }

        return returnIndexes;
    }
}