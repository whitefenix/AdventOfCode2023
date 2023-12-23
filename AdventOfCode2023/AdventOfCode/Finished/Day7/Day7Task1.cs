using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day7;

public class Day7Task1 : ITask
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
            
        StreamReader sr = new StreamReader("../../../test.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            var splitLine = line.Split(" ");
            var hand = splitLine[0];
            var bet = ParseNumber(splitLine[1]);
            
            var handValue = GetHandValue(hand);
            var handType = GetHandType(hand);
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
    private static string GetHandType(string hand)
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

        var cardNumbers = cardMap.Values.ToArray();

        if (cardNumbers.Contains(5))
        {
            return "7"; //five of a kind
        }

        if (cardNumbers.Contains(4))
        {
            return "6"; //four of a kind
        }

        if (cardNumbers.Contains(3))
        {
            return cardNumbers.Contains(2) ? "5" : "4"; //full house or trips
        }

        if (cardNumbers.Contains(2))
        {
            var numberOfPairs =
                from num in cardNumbers
                where num == 2
                select num;

            return numberOfPairs.Count() == 2 ? "3" : "2"; //two pair or one pair
        }

        return "1"; //high card
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