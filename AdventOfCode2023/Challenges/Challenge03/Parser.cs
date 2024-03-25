using AdventOfCode2023.Parsers;

using static LanguageExt.Either<int, AdventOfCode2023.Challenges.Challenge03.Symbol>;

namespace AdventOfCode2023.Challenges.Challenge03;



public class Parser : IParser<Seq<SchematicCharacter>, IChallenge03Input>
{
    public Seq<Seq<SchematicCharacter>> Parse(IChallenge03Input input)
    {
        return input.ReadLines()
            .Map(line => line.Map<char, SchematicCharacter>(c =>
                c switch
                {
                    _ when char.IsDigit(c) => Some(Left((int)char.GetNumericValue(c))),
                    '.' => SchematicCharacter.None,
                    _ when Symbol.IsSymbol(c) => Some(Right(new Symbol(c)))
                }).ToSeq());
    }
}