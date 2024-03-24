using AdventOfCode2023.Challenges.Challenge01;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges;

public class Part2ParserTests
{
    [Fact]
    public void Parse_Produces_CorrectSum()
    {
        var expectedNumbers = new Seq<int>(new[] { 29, 83, 13, 24, 42, 14, 76 });

        var input = TestInput.Create<IChallenge01Input, int>("Challenge_01_2.txt");
        
        var nums = new Part2Parser().Parse(input);
        nums.Should().BeEquivalentTo(expectedNumbers);

        nums.Sum().Should().Be(281);
    }
}