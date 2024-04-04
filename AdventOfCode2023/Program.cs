using AdventOfCode2023.Challenges.Challenge03;
using AdventOfCode2023.Challenges.Challenge04;
using Parts = AdventOfCode2023.Challenges.Challenge04.Parts;

namespace AdventOfCode2023;

public class Program
{
    public static void Main(string[] args)
    {
        var p1 = Parts.Part1(new Input());
        var borrowedp1 = Parts.Part1Borrowed(new Input());
        var p1a = Parts.Part1_A(new Input());
        
        // var p2 = Parts.ExecutePart2(new Challenge01Input());
        
        Console.WriteLine(p1);
        Console.WriteLine(borrowedp1);
        Console.WriteLine(p1a);
        // Console.WriteLine(p2);

        
        Console.ReadLine();
    }
}