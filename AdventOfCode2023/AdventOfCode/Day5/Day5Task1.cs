using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day5;

public class Day5Task1 : ITask
{
    public struct Mapping
    {
        public Mapping(long destination, long source, long range)
        {
            destinationStart = destination;
            sourceStart = source;
            this.range = range;
        }
        public long destinationStart { get; set; }
        public long sourceStart { get; set; }
        public long range { get; set; }
    }
    
    public void RunTask()
    {
        int totalSum = 0;

        List<Mapping> SeedToSoil = new();
        List<Mapping> SoilToFertilizer = new();
        List<Mapping> FertilizerToWater = new();
        List<Mapping> WaterToLight = new();
        List<Mapping> LightToTemperature = new();
        List<Mapping> TemperatureToHumidity = new();
        List<Mapping> HumidityToLocation = new();

        List<List<Mapping>> mappingsMatrix = new List<List<Mapping>>{SeedToSoil, SoilToFertilizer, FertilizerToWater, WaterToLight, LightToTemperature, TemperatureToHumidity, HumidityToLocation};
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        var seedsLine = line.Split(":");
        var seeds = TrimSpaces(seedsLine[1].Split(" ").ToList());
        var seedsAsNumbers = new List<long>();
        foreach (var seed in seeds)
        {
            seedsAsNumbers.Add(Int64.Parse(seed));
        }
        int matrixIndex = 0;

        sr.ReadLine();
        sr.ReadLine();
        line = sr.ReadLine();
        while (line != null)
        {
            if (line.Equals(""))
            {
                line = sr.ReadLine();
                continue;
            }
            if (!Regex.IsMatch(line[0].ToString(), "[0-9]"))
            {
                matrixIndex++;
                line = sr.ReadLine();
                continue;
            }

            var numbersList = line.Split(" ");
            long destinationStart = ParseNumber(numbersList[0]);
            long sourceStart = ParseNumber(numbersList[1]);
            long range = ParseNumber(numbersList[2]);

            var newMapping = new Mapping(destinationStart, sourceStart, range);
            mappingsMatrix[matrixIndex].Add(newMapping);
            
            line = sr.ReadLine();
        }

        var newNumberList = seedsAsNumbers;
        foreach (var nextMappingsList in mappingsMatrix) //For each mapping step
        {
            var tempList = new List<long>();
            foreach (var mappedNumber in newNumberList) //for each current number
            {
                bool foundMapping = false;
                foreach (var mapping in nextMappingsList) //For every mapping in the current mapping step
                {
                    if (mappedNumber > mapping.sourceStart && mappedNumber < mapping.sourceStart+mapping.range) //TODO: Check for off by one
                    {
                        //Add destination number + difference between source start and actual number
                        tempList.Add(mapping.destinationStart + (mappedNumber - mapping.sourceStart) ); 
                        foundMapping = true;
                    }
                }
                if (!foundMapping)
                {
                    tempList.Add(mappedNumber);
                }
            }

            newNumberList = tempList;
        }
        
        //Finally get lowest number
        Console.WriteLine("Lowest number is : " + newNumberList.Min());
    }

  
    //Removes all entries that are just spaces
    private static List<string> TrimSpaces(IEnumerable<string> input)
    {
        return input.Where(entry => !entry.Equals("")).ToList();
    }
    
    private static long ParseNumber(string numberString)
    {
        var combinedString = "";
        var matches = Regex.Matches(numberString, "[0-9]");
        foreach (var match in matches)
        {
            combinedString += match.ToString();
        }
        return long.Parse(combinedString);
    }
}