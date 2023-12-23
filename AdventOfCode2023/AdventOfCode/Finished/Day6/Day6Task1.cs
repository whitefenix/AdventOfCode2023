namespace AdventOfCode.Day6;

public class Day6Task1 : ITask
{

    public class Race
    {
        public Race(int length, int recordDistance)
        {
            Length = length;
            RecordDistance = recordDistance;
        }
        public int Length { get; set; }
        public int RecordDistance { get; set; }
    }
    
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var timeLine = sr.ReadLine();
        var distanceLine = sr.ReadLine();
 
        var timeNumbers = RemoveEmptyStrings(timeLine[5..].Split(" "));
        var distanceNumbers = RemoveEmptyStrings(distanceLine[10..].Split(" "));

        var raceList = new List<Race>();
        for (int i = 0; i < timeNumbers.Count; i++)
        {
            raceList.Add(new Race(int.Parse(timeNumbers[i]), int.Parse(distanceNumbers[i])));
        }

        var totalWaysToWin = new List<int>();
        foreach (var race in raceList)
        {
            int waysToWin = 0;
            bool startedWinning = false;
            for (int timeHeld = 0; timeHeld < race.Length; timeHeld++)
            {
                var winner = CheckIfWinner(timeHeld, race);
                if (winner)
                {
                    startedWinning = true;
                    waysToWin++;
                }
                else if (!winner && startedWinning)
                {
                    break;
                }
            }
            totalWaysToWin.Add(waysToWin);
        }

        totalSum = totalWaysToWin[0];
        for (int i = 1; i < totalWaysToWin.Count; i++)
        {
            totalSum *= totalWaysToWin[i];
        }

        Console.WriteLine("TotalSum is : " + totalSum);
    }

    //holdtime = speed
    private bool CheckIfWinner(int holdTime, Race race)
    {
        int distance = holdTime * (race.Length - holdTime);

        return distance > race.RecordDistance;
    }
    
    //Removes all entries that are just spaces
    private static List<string> RemoveEmptyStrings(IEnumerable<string> input)
    {
        return input.Where(entry => !entry.Equals("")).ToList();
    }
}