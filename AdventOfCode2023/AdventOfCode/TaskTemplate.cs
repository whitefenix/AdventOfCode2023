namespace AdventOfCode;

public class TaskTemplate : ITask
{
    public void RunTask()
    {
        int totalSum = 0;
            
        StreamReader sr = new StreamReader("../../../input.txt");
        var line = sr.ReadLine();
        
        while (line != null)
        {
            
            line = sr.ReadLine();
        }
        Console.WriteLine("Total sum is: " + totalSum);
    }
}