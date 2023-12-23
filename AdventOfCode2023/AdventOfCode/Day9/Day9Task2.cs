namespace AdventOfCode.Day9;

using static Utilities.Utilities;

public class Day9Task2 : ITask
{
    public void RunTask()
    {
        var totalSum = 0;
            
        var streamReader = new StreamReader("../../../input.txt");
        var line = streamReader.ReadLine();
        
        while (line != null)
        {
            var matrix = new List<List<int>>();
            var nextList = new List<int>();
            var currentList = ParseNumbersToList(line.Split(' '));
            
            matrix.Add(currentList);
            
            //Create all necessary lists inside matrix
            while (ListHasAnyValues(currentList))
            {
                nextList.Clear();
                for(var i = 0; i < currentList.Count - 1; i++)
                {
                    nextList.Add(currentList[i+1] - currentList[i]);
                }

                currentList = new List<int>(nextList);
                matrix.Add(currentList);
            }
            
            matrix[^1].Insert(0, 0);
            //Create new start values for each list
            for (int i = matrix.Count - 2; i >= 0; i--)
            {
                matrix[i].Insert(0, matrix[i][0] - matrix[i+1][0]); //Add the sum of previous lists last element to this lists last element
            }

            totalSum += matrix[0][0];
            
            line = streamReader.ReadLine();
        }
        Console.WriteLine("Total sum is: " + totalSum);
    }
}