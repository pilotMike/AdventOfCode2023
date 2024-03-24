namespace AdventOfCode2023.Challenges.Challenge02;

public static class Parts
{
    public static GameConfiguration Part1Configuration { get; } = new(new GameSet(new Set<CubeSet>(
        Seq(
            new CubeSet(CubeColor.Red, 12),
            new CubeSet(CubeColor.Green, 13),
            new CubeSet(CubeColor.Blue, 14)
        ))));

    public static int Part1(IChallenge02Input input) => new Part1Parser().Parse(input)
        .Filter(g => g.GameSets.All(sets => Part1Configuration.IsPossible(sets)))
        .Map(g => g.ID)
        .Sum();

    public static int Part2(IChallenge02Input input) => new Part1Parser().Parse(input)
        .Map(game => game.GameSets.SelectMany(gs => gs)
            .GroupBy(cs => cs.Cube)
            .Select(g => g.MaxBy(c => c.Count).Count)
            .Aggregate((power, count) => power * count)
        )
        .Sum();
}