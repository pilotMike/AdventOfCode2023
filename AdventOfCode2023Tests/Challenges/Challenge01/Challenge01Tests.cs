using AdventOfCode2023Tests.Inputs;
using AdventOfCode2023.Challenges.Challenge01;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges;

public class Challenge01Tests
{
    [Fact]
    public void Sample01Input()
    {
        var input = TestInput.Create<IChallenge01Input, int>("Challenge_01_1.txt");
        var res = Parts.ExecutePart1(input);
        res.Should().Be(142);
    }

    [Fact]
    public void Part2_MatchesTextToNumbers()
    {
        var expectedNumbers = new Seq<int>(new[] { 29, 83, 13, 24, 42, 14, 76 });
        
        var input = TestInput.Create<IChallenge01Input, int>("Challenge_01_2.txt");

        new Part2Parser().Parse(input);
    }
}