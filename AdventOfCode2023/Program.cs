using AdventOfCode2023.Challenges.Challenge01;

namespace AdventOfCode2023;

public class Program
{
    public static void Main(string[] args)
    {
        var p1 = Parts.ExecutePart1(new Challenge01Input());
        
        var p2 = Parts.ExecutePart2(new Challenge01Input());
        
        Console.WriteLine(p1);
        Console.WriteLine(p2);

        
        Console.ReadLine();
    }
}