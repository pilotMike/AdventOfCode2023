using System.Text.RegularExpressions;
using AdventOfCode2023.Challenges.Challenge01;
using AdventOfCode2023.Extensions;
using AdventOfCode2023.Parsers;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests.Parsers;

public class NumberToTextParserTests
{
    private readonly ITestOutputHelper _writer;

    public NumberToTextParserTests(ITestOutputHelper writer)
    {
        _writer = writer;
    }
    
    [Fact]
    public void Parse_ParsesNumberText_ToNumbers()
    {
        // first and last digits
        var expectedNumbers = new Seq<int>(new[] { 29, 83, 13, 24, 42, 14, 76 });

        var input = TestInput.Create<IChallenge01Input, int>("Challenge_01_2.txt");

        var reg = new Regexes().IsDigitOrSpelledNumber();
        var nums = input.ReadLines().Map(
                line => reg.Matches(line)
                    .GetMatchedGroupTexts()
                    .Map(NumberTextToIntParser.Parse)
                    .Somes()
                    .ToSeq()
                    .Apply(nums => int.Parse($"{nums.Head}{nums.Last}")));

        nums.Should().BeEquivalentTo(expectedNumbers);

        nums.Sum().Should().Be(281);
    }

    [Fact]
    public void Parse_UsesCorrectLookAhead()
    {
        // eighthree should be 8three
        var input = "eighthree";
        
        var reg = new Regexes().IsDigitOrSpelledNumber();

        var res = reg.Matches(input);
        
        var allMatches = res.GetMatchedGroupTexts().ToList();
        _writer.WriteLine(allMatches.JoinString(","));
        
        allMatches[0].Should().Be("eight");
        allMatches[1].Should().Be("three");
    }
}