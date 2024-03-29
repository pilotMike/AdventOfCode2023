using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge04;

public interface IChallenge04Input : IInputSource<HailStone>;
public class Input() : InputSource(ChallengeNumber.New(4), ChallengePart.Part1), IChallenge04Input
{
    
}