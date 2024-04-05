using AdventOfCode2023.Challenges.Challenge04;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges.Challenge04;

public class ParserTests
{
    [Fact]
    public void Parse_CorrectlyParsesInput()
    {
        var parser = new Parser();
        var res = parser.Parse(TestInput.Create<IChallenge4Input, ScratchCard>("04_1.txt"));

        res[0].CardNumber.Should().Be(1);
        res[0].WinningNumbers.AsEnumerable().Should().BeEquivalentTo(new Set<int>(new[] { 41, 48, 83, 86, 17 }));
        res[0].PotentialNumbers.Should().BeEquivalentTo(new[] { 83, 86, 6, 31, 17, 9, 48, 53 });
    }
}