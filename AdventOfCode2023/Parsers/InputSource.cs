using AdventOfCode2023.Domain;

namespace AdventOfCode2023.Parsers;

public abstract class InputSource(ChallengeNumber challenge, ChallengePart part)
{
    private Seq<string>? _data;
    private readonly string _path = $"../../../Inputs/Challenge_{challenge}_{part}.txt";

    public Seq<string> ReadLines(bool includeEmptyLines = false) => 
        _data ??= File.ReadAllLines(_path)
            .Apply(lines => includeEmptyLines ? lines : lines.FilterNot(string.IsNullOrWhiteSpace))
            .ToSeq();
}