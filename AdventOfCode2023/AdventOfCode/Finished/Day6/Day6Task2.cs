using System.Text.RegularExpressions;

namespace AdventOfCode.Day6;

public class Day6Task2 : ITask
{
    private class Race
    {
        public Race(long length, long recordDistance)
        {
            Length = length;
            RecordDistance = recordDistance;
        }
        public long Length { get; }
        public long RecordDistance { get; }
    }
    
    public void RunTask()
    {
        long waysToWin = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var timeLine = sr.ReadLine();
        var distanceLine = sr.ReadLine();
 
        var timeNumber = Regex.Replace(timeLine[5..], " ", "");
        var distanceNumber = Regex.Replace(distanceLine[10..], " ", "");

        var race = new Race(Int64.Parse(timeNumber), Int64.Parse(distanceNumber));

        bool startedWinning = false;
        
        long timeHeld = 0;
        while (!startedWinning)
        {
            if (CheckIfWinner(timeHeld, race))
            {
                startedWinning = true;
            }
            else
            {
                timeHeld++;
            }
        }

        waysToWin = race.Length - (timeHeld * 2) + 1;

        Console.WriteLine("Ways to win are : " + waysToWin);
    }

    //holdTime = speed
    private bool CheckIfWinner(long holdTime, Race race)
    {
        long distance = holdTime * (race.Length - holdTime);

        return distance > race.RecordDistance;
    }
}