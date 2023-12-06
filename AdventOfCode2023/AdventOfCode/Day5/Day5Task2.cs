// using System.Diagnostics;
// using System.Text.RegularExpressions;
//
// namespace AdventOfCode.Day5;
//
// public class Day5Task2 : ITask
// {
//     public class Mapping
//     {
//         public Mapping(long destination, long source, long range)
//         {
//             DestinationStart = destination;
//             SourceStart = source;
//             this.Range = range;
//         }
//         public long DestinationStart { get; set; }
//         public long SourceStart { get; set; }
//         public long Range { get; set; }
//         public long DestinationEnd => DestinationStart + Range - 1;
//         
//         public long SourceEnd => DestinationEnd + Range - 1;
//     }
//
//     public class SeedRange
//     {
//         public SeedRange(long firstSeed, long range)
//         {
//             FirstSeed = firstSeed;
//             Range = range;
//             
//         }
//         public long FirstSeed { get; set; }
//         public long Range { get; set; }
//         
//         public long LastSeed => FirstSeed + Range - 1;
//     }
//     
//     public void RunTask()
//     {
//         int totalSum = 0;
//
//         List<Mapping> seedToSoil = new();
//         List<Mapping> soilToFertilizer = new();
//         List<Mapping> fertilizerToWater = new();
//         List<Mapping> waterToLight = new();
//         List<Mapping> lightToTemperature = new();
//         List<Mapping> temperatureToHumidity = new();
//         List<Mapping> humidityToLocation = new();
//
//         List<List<Mapping>> mappingsMatrix = new List<List<Mapping>>{seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation};
//             
//         StreamReader sr = new StreamReader("../../../test.txt");
//         var line = sr.ReadLine();
//         var seedsLine = line.Split(":");
//         var seeds = TrimSpaces(seedsLine[1].Split(" ").ToList());
//         var seedRangesAsNumbers = new List<long>();
//         foreach (var seed in seeds)
//         {
//             seedRangesAsNumbers.Add(Int64.Parse(seed));
//         }
//
//         var seedRanges = new List<SeedRange>();
//         
//         for (int seedStart = 0; seedStart < seedRangesAsNumbers.Count; seedStart+=2)
//         {
//             seedRanges.Add(new SeedRange(seedRangesAsNumbers[0], seedRangesAsNumbers[1]));
//         }
//         
//         int matrixIndex = 0;
//
//         sr.ReadLine();
//         sr.ReadLine();
//         line = sr.ReadLine();
//         while (line != null)
//         {
//             if (line.Equals(""))
//             {
//                 line = sr.ReadLine();
//                 continue;
//             }
//             if (!Regex.IsMatch(line[0].ToString(), "[0-9]"))
//             {
//                 matrixIndex++;
//                 line = sr.ReadLine();
//                 continue;
//             }
//
//             var numbersList = line.Split(" ");
//             long destinationStart = ParseNumber(numbersList[0]);
//             long sourceStart = ParseNumber(numbersList[1]);
//             long range = ParseNumber(numbersList[2]);
//
//             var newMapping = new Mapping(destinationStart, sourceStart, range);
//             mappingsMatrix[matrixIndex].Add(newMapping);
//             
//             line = sr.ReadLine();
//         }
//         
//         //numbers are now start + range
//         
//         
//         /*
//          Map a range of seeds (from x to y) to a new range (from x to y) using our map
//          if the old range ends before the map starts
//          (e.g. we are mapping from a source that ends at 10, and the map starts at 20)
//          range is the same so add the source to result
//          same if our map ends before the range starts
//          
//          if the start of the seeds are less than our start AND the end is larger or equals to our start
//          eg. range goes from 5-15 and we start at 10
//          add the range from their start to before we start
//          if their end is larger than ours, add our entire range, as well as the extra vit
//          else if their start is lower than our start, 
//          
//          
//          * 
//          */
//         /*
//           Example:
//           Start range is 1-10
//           Maps to 15-25
//           New range is 15-25
//           
//           Example:
//           Start range is 1-10
//           Maps to 15-20 and 25-30
//           Start range splits to 15, 5 and 25, 5
//           
//          */
//
//         //Parse
//
//         var currentRanges = seedRanges;
//
//         foreach (var nextMappingsList in mappingsMatrix) //For each mapping step
//         {
//             var newRanges = new List<SeedRange>();
//             foreach (var range in currentRanges) //For each of the seed ranges
//             {
//                 
//             }
//         }
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//         var newNumberList = seedRanges;
//         foreach (var nextMappingsList in mappingsMatrix) //For each mapping step
//         {
//             var tempList = new List<long>();
//             foreach (var mappedNumber in newNumberList) //for each current number
//             {
//                 bool foundMapping = false;
//                 foreach (var mapping in nextMappingsList) //For every mapping in the current mapping step
//                 {
//                     if (mappedNumber >= mapping.SourceStart && mappedNumber <= mapping.SourceStart+mapping.Range) //TODO: Check for off by one
//                     {
//                         //Add destination number + difference between source start and actual number
//                         tempList.Add(mapping.DestinationStart + (mappedNumber - mapping.SourceStart) ); 
//                         foundMapping = true;
//                     }
//                 }
//                 if (!foundMapping)
//                 {
//                     tempList.Add(mappedNumber);
//                 }
//             }
//
//             newNumberList = tempList;
//         }
//         
//         //Finally get lowest number
//         Console.WriteLine("Lowest number is : " + newNumberList.Min());
//     }
//
//   
//     //Removes all entries that are just spaces
//     private static List<string> TrimSpaces(IEnumerable<string> input)
//     {
//         return input.Where(entry => !entry.Equals("")).ToList();
//     }
//     
//     private static long ParseNumber(string numberString)
//     {
//         var combinedString = "";
//         var matches = Regex.Matches(numberString, "[0-9]");
//         foreach (var match in matches)
//         {
//             combinedString += match.ToString();
//         }
//         return long.Parse(combinedString);
//     }
// }