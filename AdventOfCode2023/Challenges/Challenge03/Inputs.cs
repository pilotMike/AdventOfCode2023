using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge03;

public interface IChallenge03Input : IInputSource<Seq<SchematicCharacter>>;

public class Challenge03Input() : InputSource(ChallengeNumber.New(3), ChallengePart.Part1), IChallenge03Input;