using AdventOfCode2023.Challenges.Challenge02;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Challenges.Challenge02;

public class Challenge02Tests
{
    [Fact]
    public void Possibilities_Are_Correct()
    {
        var config = Parts.Part1Configuration;

        var input = TestInput.Create<IChallenge02Input, Game>("Challenge_02_1.txt");
        var games = new Part1Parser().Parse(input);

        var idsums = games.Where(g => g.GameSets.All(s => config.IsPossible(s)))
            .Select(g => g.ID)
            .Sum();

        idsums.Should().Be(8);
    }
}