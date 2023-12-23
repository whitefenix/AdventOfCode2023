using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public class Day3Task2 : ITask
{
    private Dictionary<(int, int), List<int>> starNumbers = new();
    int totalSum;
    
    public void RunTask()
    {
        var lastLine = "";
        var currentLine = "";
        var nextLine = "";
        
        int lineIndex = 0;

        StreamReader sr = new StreamReader("../../../input.txt");

        currentLine = sr.ReadLine();
        nextLine = sr.ReadLine();

    while (currentLine != null)
    {
            //find number index pairs for line
            var indexList = GetIndexes(currentLine);

            //go over all index pairs
            for (int i = 0; i < indexList.Count-1; i += 2)
            {
                List<int> indexPair = new List<int> { indexList[i], indexList[i + 1] };

                //if the number has an adjacent star on current line
                var adjacentStarIndex = GetAdjacentStarIndex(indexPair, currentLine);
                if (adjacentStarIndex != null)
                {
                    AddToDictionary((lineIndex, (int)adjacentStarIndex), ParseNumbers(indexPair, currentLine));
                }

                //if the number has an adjacent star on previous line
                if (!lastLine.Equals(""))
                {
                    adjacentStarIndex = GetAdjacentStarIndex(indexPair, lastLine);
                    if (adjacentStarIndex != null)
                    {
                        AddToDictionary((lineIndex-1, (int)adjacentStarIndex), ParseNumbers(indexPair, currentLine));
                    }
                }

                if (nextLine is null or "") continue;
                
                //if number is has an adjacent star on next line
                adjacentStarIndex = GetAdjacentStarIndex(indexPair, nextLine);
                if (adjacentStarIndex != null)
                {
                    AddToDictionary((lineIndex+1, (int)adjacentStarIndex), ParseNumbers(indexPair, currentLine));
                }
            }
            lineIndex++;
            lastLine = currentLine;
            currentLine = nextLine;
            nextLine = sr.ReadLine();
        }
        CountStars();
        Console.WriteLine("Total sum is: " + totalSum);
    }

    //If a star has two adjacent numbers, multiply and add to total
    private void CountStars()
    {
        foreach (var key in starNumbers.Keys.Where(key => starNumbers[key].Count > 1))
        {
            totalSum += starNumbers[key][0] * starNumbers[key][1];
        }
    }
    
    //with star index as key add number to the value list
    private void AddToDictionary((int, int) starIndex, int number)
    {
        if (starNumbers.ContainsKey(starIndex))
        {
            if (!starNumbers[starIndex].Contains(number))
            {
                starNumbers[starIndex].Add(number);
            }
        }
        else
        {
            starNumbers.Add(starIndex, new List<int>{number});
        }
    }

    //parse string numbers to int
    private int ParseNumbers(List<int> inputIndexes, string inputString)
    {
        var numberString = inputString.Substring(inputIndexes[0], inputIndexes[1]-inputIndexes[0]+1);
        return int.Parse(numberString);
    }

    //Send a pair of indexes and check if there are any adjacent stars
    private static int? GetAdjacentStarIndex(List<int> inputIndexes, string inputString)
    {
        int startIndex = inputIndexes[0], endIndex = inputIndexes[1];
        
        if (inputIndexes[0] != 0) //If we can
        {
            startIndex--; //Reduce start index by one
        }

        if (inputIndexes[1] != inputString.Length-1) //If we can
        {
            endIndex++; //Increase end index by one
        }
        
        for (int i = startIndex; i <= endIndex; i++)
        {
            var starPattern = "\\*";
            if (Regex.IsMatch(inputString[i].ToString(), starPattern))
            {
                return i;
            }
        }

        return null;
    }
    
    //Get indexes for numbers in the string
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