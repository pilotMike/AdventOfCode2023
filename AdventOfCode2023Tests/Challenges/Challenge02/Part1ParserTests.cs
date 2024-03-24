using AdventOfCode2023.Challenges.Challenge02;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;
using static AdventOfCode2023.Challenges.Challenge02.CubeColor;

namespace AdventOfCode2023Tests.Challenges.Challenge02;

public class Part1ParserTests
{
    [Fact]
    public void ParsesCorrectly()
    {
        var input = TestInput.Create<IChallenge02Input, Game>("Challenge_02_1.txt");

        var parser = new Part1Parser();

        var result = parser.Parse(input);

        var game1 = new Game(1, Seq(
            new GameSet(new Set<CubeSet>(Seq(new CubeSet(Blue, 3), new(Red, 4)))),
            new GameSet(new Set<CubeSet>(Seq(new CubeSet(Red, 1), new(Green, 2), new(Blue, 6)))),
            new GameSet(new Set<CubeSet>(Seq1(new CubeSet(Green, 2))))
            )
        );

        result[0].Should().BeEquivalentTo(game1);
    }
}