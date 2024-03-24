using AdventOfCode2023.Domain;
using AdventOfCode2023.Extensions;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge01;

public interface IChallenge01Parser : IParser<int, IChallenge01Input>;

public class Part1Parser : IChallenge01Parser
{
    public Seq<int> Parse(IChallenge01Input input) =>
        input.ReadLines()
            .Map(line => 
                line.Filter(char.IsNumber)
                .ToSeq()
                .Apply(nums => int.Parse($"{nums.Head}{nums.Last}")));
}

public class Part2Parser : IChallenge01Parser
{
    private readonly IChallengeObserver obs = new NopObserver();
    
    public Seq<int> Parse(IChallenge01Input input)
    {
        var reg = new Regexes().IsDigitOrSpelledNumber();
        return input.ReadLines()
            .Map(line => reg.Matches(line)
                .GetMatchedGroupTexts()
                .Map(NumberTextToIntParser.Parse)
                .Somes()
                .ToSeq()
                .Apply(nums =>
                {
                    var parsed = int.Parse($"{nums.Head}{nums.Last}");
                    obs.Observe(line, parsed);
                    return parsed;
                }));
    }
}