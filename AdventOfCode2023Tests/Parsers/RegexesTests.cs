using AdventOfCode2023.Challenges.Challenge01;
using AdventOfCode2023.Extensions;
using AdventOfCode2023.Parsers;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Parsers;

public class RegexesTests
{
    [Fact]
    public void IsDigitOrSpelledNumber_IsCorrect()
    {
        var expected = new Seq<string>(
            "two1nine\neightthree\none2three\ntwo3four\n4nineeightseven2\none234\n7six".Split("\n"));

        var input = TestInput.Create<IChallenge01Input, int>("Challenge_01_2.txt");
        var regex = new Regexes().IsDigitOrSpelledNumber();

        var readLines = input.ReadLines().Map(l => regex.Matches(l).Map(m => m.Value).JoinString());
        
        readLines
            .Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public void IsDigitOrSpelledNumber_SplitsNumberIntoCharacters()
    {
        var input = "96";

        var reg = new Regexes().IsDigitOrSpelledNumber();

        var matches = reg.Matches(input);
        matches.Count.Should().Be(2);
        matches[0].Value.Should().Be("9");
        matches[1].Value.Should().Be("6");
    }
}