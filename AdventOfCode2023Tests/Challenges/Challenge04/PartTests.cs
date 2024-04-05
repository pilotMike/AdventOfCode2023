using AdventOfCode2023.Challenges.Challenge04;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges.Challenge04;

public class PartTests
{
    [Fact]
    public void Part1_MatchesExample()
    {
        var parser = new Parser();
        var res = parser.Parse(TestInput.Create<IChallenge4Input, ScratchCard>("04_1.txt"));

        var firstCardScore = res[0].Score();

        firstCardScore.Should().Be(8);
        res[1].Score().Should().Be(2);
        res[2].Score().Should().Be(2);
        res[3].Score().Should().Be(1);
        res[4].Score().Should().Be(0);
        res[5].Score().Should().Be(0);
    }

    [Fact]
    public void Part2_MatchesExample()
    {
        Parts.Part2(TestInput.Create<IChallenge4Input, ScratchCard>("04_1.txt"))
            .Should().Be(30);

    }
}