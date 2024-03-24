using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge02;

public interface IChallenge02Input : IInputSource<Game>;
public class Part1Input() : InputSource(ChallengeNumber.New(2), ChallengePart.Part1), IChallenge02Input
{
    public Seq<string> ReadLines() => base.ReadLines().ToSeq();
}