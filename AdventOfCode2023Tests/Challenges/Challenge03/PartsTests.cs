using AdventOfCode2023.Challenges.Challenge03;
using AdventOfCode2023Tests.Extensions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;
using SchematicCharacter =
    LanguageExt.Option<LanguageExt.Either<int, AdventOfCode2023.Challenges.Challenge03.Symbol>>;

namespace AdventOfCode2023Tests.Challenges.Challenge03;

public class PartsTests
{
    [Fact]
    public void Part1IsCorrect()
    {
        var input = TestInput.Create<IChallenge03Input, Seq<SchematicCharacter>>("Challenge_03_1.txt");

        var res = Parts.Part1(input);

        res.Should().Be(4361);
    }

    [Fact]
    public void Parser_CorrectlySplits_NumberThenSymbol()
    {
        var input = TestInput.CreateForString<IChallenge03Input, Seq<SchematicCharacter>>("6.");

        var line = new Parser().Parse(input).First();

        line.Count.Should().Be(2);
        line[0].ShouldBeSome(o => o.ShouldBeLeft(v => v.Should().Be(6)));
        line[1].ShouldBeNone();
    }

    [Fact]
    public void Schematic_CorrectForNumberThenSymbol()
    {
        var input = TestInput.CreateForString<IChallenge03Input, Seq<SchematicCharacter>>("6.");

        var schematic = new Schematic(new Parser().Parse(input));
        var items = schematic.SchematicNumbers;

        items.Count.Should().Be(1);
        items.Head.Value.Should().Be(6);
    }

    [Theory]
    [InlineData("03_2.txt", 413)]
    [InlineData("03_3.txt", 925)]
    [InlineData("03_4.txt", 925)]
    [InlineData("03_5.txt", 587)]
    public void MoreTests(string uri, int expected)
    {
        var input = TestInput.Create<IChallenge03Input, Seq<SchematicCharacter>>(uri);

        var res = Parts.Part1(input);
        
        var schematic = new Schematic(new Parser().Parse(input));
        var items = schematic.SchematicNumbers;

        var adjacents = items.Filter(item =>
            schematic.IsAdjacentToSymbol(item));

        res.Should().Be(expected);
    }

    [Fact]
    public void SchematicAdjacentTest_NearCorner()
    {
        var input = TestInput.CreateForString<IChallenge03Input, Seq<SchematicCharacter>>(
            "12.\n..*");
        
        var schematic = new Schematic(new Parser().Parse(input));
        var items = schematic.SchematicNumbers;

        var adjacents = items.Filter(item =>
            schematic.IsAdjacentToSymbol(item))
            .ToArray();
    }

    [Theory]
    [InlineData(".2.", 0)]
    [InlineData("*.2", 0)]
    [InlineData(".2.\n*.2", 2)]
    public void MoreParserTests(string text, int expected)
    {
        var input = TestInput.CreateForString<IChallenge03Input, Seq<SchematicCharacter>>(text);

        var schematic = new Schematic(new Parser().Parse(input));

        var res = Parts.Part1(input);

        res.Should().Be(expected);
    }

    [Fact]
    public void Part2Test()
    {
        var input = TestInput.Create<IChallenge03Input, Seq<SchematicCharacter>>("Challenge_03_1.txt");

        var res = Parts.Part2(input);

        res.Should().Be(467835);
    }
    
    [Theory]
    [InlineData("03_2.txt", 6756)]
    [InlineData("03_3.txt", 6756)]
    public void MorePart2Tests(string uri, int expected)
    {
        var input = TestInput.Create<IChallenge03Input, Seq<SchematicCharacter>>(uri);

        var res = Parts.Part2(input);
        
        var schematic = new Schematic(new Parser().Parse(input));

        var stars = schematic.SchematicSymbols.Filter(t => t.Symbol.Character == '*').ToArray();

        bool[] expecteds = { true, true, false, false, true };
        var gears = stars.Map(s => schematic.IsGear(s)).Somes().ToArray();
        
        var potentials =
            stars
                .Map(s => schematic.IsGear(s))
                .Somes()
                .ToArray();

        res.Should().Be(expected);
    }
}