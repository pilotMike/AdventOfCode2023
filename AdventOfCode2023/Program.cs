using System.Runtime.CompilerServices;
using AdventOfCode2023.Challenges.Challenge05;
[assembly:InternalsVisibleTo("AdventOfCode2023Tests")]

namespace AdventOfCode2023;

public class Program
{
    public static void Main(string[] args)
    {
        var p1 = V2.Part1(new Input());
        
        // var p2 = Parts.ExecutePart2(new Challenge01Input());
        
        Console.WriteLine(p1);
        // Console.WriteLine(p2);

        
        Console.ReadLine();
    }
}