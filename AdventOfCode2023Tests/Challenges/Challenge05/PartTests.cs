using AdventOfCode2023.Challenges.Challenge05;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges.Challenge05;

public class PartTests
{
    [Fact]
    public void Part1_MatchesExample()
    {
        var input =
            TestInput.Create<IChallenge05Input, (Seq<MaterialValue> seeds, Seq<MaterialMapCollection> maps)>("05_1.txt");

        var res = V2.Part1(input);
        res.Should().Be(35);
    }
}