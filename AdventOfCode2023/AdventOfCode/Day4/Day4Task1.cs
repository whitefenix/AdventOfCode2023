namespace AdventOfCode.Day4;

public class Day4Task1 : ITask
{
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            var cardAndNumbers = line.Split(":");
            var winningNumbersAndHaveNumbers = cardAndNumbers[1].Split("|");
            var winningNumbers = TrimSpaces(winningNumbersAndHaveNumbers[0].Split(" ").ToList());
            var myNumbers = TrimSpaces(winningNumbersAndHaveNumbers[1].Split(" ").ToList());

            int cardSum = 0;
            foreach (var unused in myNumbers.Where(number => winningNumbers.Contains(number)))
            {
                if (cardSum == 0)
                {
                    cardSum = 1;
                    continue;
                }
                cardSum *= 2;
            }
            totalSum += cardSum;
            
            line = sr.ReadLine();
        }
        Console.WriteLine("Totalsum is: " + totalSum);
    }
    
    //parse string numbers to int
    private int ParseNumbers(List<int> inputIndexes, string inputString)
    {
        var numberString = inputString.Substring(inputIndexes[0], inputIndexes[1]-inputIndexes[0]+1);
        return int.Parse(numberString);
    }

    //Removes all entries that are just spaces
    private List<string> TrimSpaces(List<string> input)
    {
        return input.Where(entry => !entry.Equals("")).ToList();
    }
}