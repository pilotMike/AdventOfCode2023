using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge01;

public interface IChallenge01Input : IInputSource<int>;
public sealed class Challenge01Input() : InputSource(ChallengeNumber.New(1), ChallengePart.Part1), IChallenge01Input;