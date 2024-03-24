namespace AdventOfCode2023.Challenges.Challenge01;

public static class Parts
{
    public static int ExecutePart1(IChallenge01Input input) => new Part1Parser().Parse(input).Sum();
    public static int ExecutePart2(IChallenge01Input input) => new Part2Parser().Parse(input).Sum();
}