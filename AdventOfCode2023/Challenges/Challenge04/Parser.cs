using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge04;

public interface IChallenge4Input : IInputSource<ScratchCard>;
public class Input() : InputSource(ChallengeNumber.New(4), ChallengePart.Part1), IChallenge4Input;
public class Parser : IParser<ScratchCard, IChallenge4Input>
{
    public Seq<ScratchCard> Parse(IChallenge4Input input) =>
        input.ReadLines()
            .Map(line =>
            {
                var split = line.IndexOf('|');
                var left = line.AsSpan()[..split];
                var right = line.AsSpan()[(split+1)..];

                var colon = left.IndexOf(':');
                var cardNumber = int.Parse(left.Slice(5, colon - 5));
                
                var winningNumbers = left[(colon+1)..].ToString()
                    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .Apply(ns => new Set<int>(ns));

                var potentialNumbers = right.ToString()
                    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToSeq();

                return new ScratchCard(cardNumber, winningNumbers, potentialNumbers);
            });
}