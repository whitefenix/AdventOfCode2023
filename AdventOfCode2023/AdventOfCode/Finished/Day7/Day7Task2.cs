using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day7;

public class Day7Task2 : ITask
{
    private Dictionary<string, string> cardValues = new();
    private Dictionary<long, int> cardToBid = new();
    private List<long> cardRankings = new();
    
    public void RunTask()
    {
        int totalSum = 0;
        
        cardValues.Add("J", "01");
        cardValues.Add("2", "02");
        cardValues.Add("3", "03");
        cardValues.Add("4", "04");
        cardValues.Add("5", "05");
        cardValues.Add("6", "06");
        cardValues.Add("7", "07");
        cardValues.Add("8", "08");
        cardValues.Add("9", "09");
        cardValues.Add("T", "10");
        cardValues.Add("Q", "11");
        cardValues.Add("K", "12");
        cardValues.Add("A", "13");
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            var splitLine = line.Split(" ");
            var hand = splitLine[0];
            var bet = ParseNumber(splitLine[1]);
            
            var handValue = GetHandValue(hand);
            var handType = GetHandType(hand, GetNumberOfJokers(hand));
            
            var handRanking = long.Parse(handType + handValue);
            
            cardRankings.Add(handRanking);
            cardToBid.Add(handRanking, bet);
            
            line = sr.ReadLine();
        }
        
        cardRankings.Sort();

        for (int i = 0; i < cardRankings.Count; i++)
        {
            totalSum += cardToBid[cardRankings[i]] * (i+1);
        }
        
        Console.WriteLine("Total sum is: " + totalSum);
    }

    private string GetHandValue(string hand)
    {
        StringBuilder valueString = new StringBuilder();
        foreach (char card in hand)
        {
            valueString.Append(cardValues[card.ToString()]);
        }
        return valueString.ToString();
    }

    //1 is nothing
    //2 is one pair
    //3 is two pair
    //4 is trips
    //5 is full house
    //6 is quads
    //7 is five of a kind
    private static string GetHandType(string hand, int numberOfJokers)
    {
        Dictionary<char, int> cardMap = new Dictionary<char, int>();
        foreach (char card in hand)
        {
            if (cardMap.ContainsKey(card))
            {
                cardMap[card]++;
            }
            else
            {
                cardMap.Add(card, 1);
            }
        }

        if (cardMap.ContainsKey('J')) //If we have jokers in the hand
        {
            cardMap['J'] = 0; //Set the number of jokers to 0, since we are adding them to the total of the most common card
        }

        var cardNumbers = cardMap.Values.ToArray();
        
        Array.Sort(cardNumbers); //sort lowest to highest
        Array.Reverse(cardNumbers); //reverse to get highest first
        cardNumbers[0] += numberOfJokers;

        //Since the array is sorted highest to lowest, we can just check the first number
        switch (cardNumbers[0])
        {
            case 5:
                return "7";
            case 4:
                return "6";
            case 3:
                if (cardNumbers[1] == 2) return "5"; //if we also have a pair
                return "4";
            case 2:
                if (cardNumbers[1] == 2) return "3"; //if we have two pair
                return "2";
            default:
                return "1";
        }
    }

    private int GetNumberOfJokers(string hand)
    {
        int numberOfJokers = 0;
        var matches = Regex.Matches(hand, "[J]");
        foreach (var _ in matches)
        {
            numberOfJokers++;
        }
        return numberOfJokers;
    }
    
    private static int ParseNumber(string numberString)
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