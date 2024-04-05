namespace AdventOfCode2023.Challenges.Challenge04;


public record ScratchCard(int CardNumber, Set<int> WinningNumbers, Seq<int> PotentialNumbers);

public static class ScratchCardExt
{
    public static int WinningCount(this ScratchCard sc) => sc.PotentialNumbers.Count(
        x => sc.WinningNumbers.Contains(x));
    
    public static int Score(this ScratchCard sc) =>
        sc.PotentialNumbers.Filter(x => sc.WinningNumbers.Contains(x))
            .Aggregate(0, (acc, _) => acc == 0 ? 1 : acc * 2);
}