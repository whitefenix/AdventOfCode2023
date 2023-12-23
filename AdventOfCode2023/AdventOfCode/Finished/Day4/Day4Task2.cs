using System.Security.Principal;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4;

public class Day4Task2 : ITask
{
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();

        var cardAmounts = new int[10000];
        
        while (line != null)
        {
            var cardAndNumbers = line.Split(":");
            var cardIndex = GrabAndParseNumber(cardAndNumbers[0]);
            var winningNumbersAndHaveNumbers = cardAndNumbers[1].Split("|");
            var winningNumbers = TrimSpaces(winningNumbersAndHaveNumbers[0].Split(" ").ToList());
            var myNumbers = TrimSpaces(winningNumbersAndHaveNumbers[1].Split(" ").ToList());
            
            int wonCards = myNumbers.Count(number => winningNumbers.Contains(number));
            
            cardAmounts[cardIndex]++; //add 1 for the card we start with
            
            //For every won card
            
            //for i = index + 1 (for every card after current)
            //while i < cardindex + won cards (until we go to the last card we are adding more to)
            //increase the card amounts of that card with the card amounts of current card
            for (int i = cardIndex+1; i < (cardIndex+1) + wonCards; i++)
            {
                //Add number of current cards to the card total for that index
                cardAmounts[i]+=cardAmounts[cardIndex];
            }
            line = sr.ReadLine();
        }
        totalSum = cardAmounts.Sum();
        Console.WriteLine("Totalsum is: " + totalSum);
    }
    
    //parse string numbers to int
    private int GrabAndParseNumber(string inputString)
    {
        var numbers = Regex.Matches(inputString, "[0-9]");
        var numbersAsString = "";
        foreach (var number in numbers)
        {
            numbersAsString += number.ToString();
        }
        return int.Parse(numbersAsString);
    }

    //Removes all entries that are just spaces
    private List<string> TrimSpaces(List<string> input)
    {
        return input.Where(entry => !entry.Equals("")).ToList();
    }
}