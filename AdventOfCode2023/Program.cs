using AdventOfCode2023.Challenges.Challenge02;

namespace AdventOfCode2023;

public class Program
{
    public static void Main(string[] args)
    {
        var p1 = Parts.Part2(new Part1Input());
        
        // var p2 = Parts.ExecutePart2(new Challenge01Input());
        
        Console.WriteLine(p1);
        // Console.WriteLine(p2);

        
        Console.ReadLine();
    }
}