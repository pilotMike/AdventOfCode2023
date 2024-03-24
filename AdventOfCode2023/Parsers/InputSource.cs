using AdventOfCode2023.Domain;

namespace AdventOfCode2023.Parsers;

public abstract class InputSource(ChallengeNumber challenge, ChallengePart part)
{
    private Seq<string>? _data;
    private readonly string _path = $"../../../Inputs/Challenge_{challenge}_{part}.txt";

    public Seq<string> ReadLines() => _data ??= File.ReadAllLines(_path).FilterNot(string.IsNullOrWhiteSpace).ToSeq();
}