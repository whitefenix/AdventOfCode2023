using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day1Task1 : Task {         
    public void RunTask() {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        //Read the first line of text
        var line = sr.ReadLine();
            
        while (line != null)
        {
            string numberPattern = "([0-9])";
                
            var result = Regex.Matches(line, numberPattern);
                
            string stringNumber = "";
            if (result.Count == 1)
            {
                stringNumber = result[0].Value + result[0].Value;
            }
            else
            {
                stringNumber = result[0].Value + result[^1].Value;
            }
                
            totalSum += int.Parse(stringNumber);

            line = sr.ReadLine();
        }
            
        Console.WriteLine("The sum is: " + totalSum);
    }
}