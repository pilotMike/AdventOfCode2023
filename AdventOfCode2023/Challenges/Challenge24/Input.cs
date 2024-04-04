using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge04;

public interface IChallenge24Input : IInputSource<HailStone>;
public class Input() : InputSource(ChallengeNumber.New(24), ChallengePart.Part1), IChallenge24Input
{
    
}