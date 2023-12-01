using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

class Day1Task2 : Task {         
    public void RunTask() {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            Console.WriteLine("Input line is: " + line);
            var convertedLine = ConvertLetterNumbers(line);
            Console.WriteLine("Converted line is: " + convertedLine);

            var stringNumber = "";
            if (convertedLine.Length == 1)
            {
                stringNumber += convertedLine[0];
                stringNumber += convertedLine[0];
            }
            else
            {
                stringNumber += convertedLine[0];
                stringNumber += convertedLine[^1];
            }
                
            totalSum += int.Parse(stringNumber);
            
            Console.WriteLine("Stringnumber is: " + stringNumber);

            line = sr.ReadLine();
        }
            
        Console.WriteLine("The sum is: " + totalSum);
    }

    private string ConvertLetterNumbers(string line)
    {
        StringBuilder builtLine = new StringBuilder();

        while (line.Length > 0)
        {
            if (Regex.IsMatch(line, "^one"))
            {
                builtLine.Append('1');
            }
            else if (Regex.IsMatch(line, "^two"))
            {
                builtLine.Append('2');
            }
            else if (Regex.IsMatch(line, "^three"))
            {
                builtLine.Append('3');
            }
            else if (Regex.IsMatch(line, "^four"))
            {
                builtLine.Append('4');
            }
            else if (Regex.IsMatch(line, "^five"))
            {
                builtLine.Append('5');
            }
            else if (Regex.IsMatch(line, "^six"))
            {
                builtLine.Append('6');
            }
            else if (Regex.IsMatch(line, "^seven"))
            {
                builtLine.Append('7');
            }
            else if (Regex.IsMatch(line, "^eight"))
            {
                builtLine.Append('8');
            }
            else if (Regex.IsMatch(line, "^nine"))
            {
                builtLine.Append('9');
            }
            else if (Regex.IsMatch(line, "^[0-9]"))
            {
                builtLine.Append(line[0]);
            }
            line = line.Remove(0, 1);
        }

        return builtLine.ToString();
    }
}