using AdventOfCode2023.Extensions;

using CardNumber = System.Int32;
using Count = System.Int32;

namespace AdventOfCode2023.Challenges.Challenge04;

public static class Parts
{
    public static int Part1(IChallenge4Input input) =>
        new Parser().Parse(input)
            .Map(s => s.Score())
            .Sum();

    public static int Part2(IChallenge4Input input)
        => new Parser().Parse(input)
            .Apply(cards =>
            {
                //return GetCountsRecursive(cards, cards);
                
                Dictionary<CardNumber, Count> wonCards = cards.ToDictionary(x => x.CardNumber, _ => 1);
                cards.Iter(card =>
                {
                    if (card.WinningCount() != 0)
                    {
                        GetWonCards(card, cards, wonCards)
                            .Iter(c => AddCard(wonCards, c));
                    }
                });
                return wonCards.Values.Sum();
            });

    private static void AddCard(Dictionary<CardNumber, Count> wonCards, ScratchCard scratchCard)
    {
        if (!wonCards.TryGetValue(scratchCard.CardNumber, out var count))
            count = 0;

        wonCards[scratchCard.CardNumber] = count + 1;
    }

    // failed with exception
    // private static int GetCountsRecursive(Seq<ScratchCard> cards, Seq<ScratchCard> allCards, int total = 0) =>
    //     cards.HeadOrNone()
    //         .Map(card =>
    //         {
    //             Console.WriteLine($"{card.CardNumber} : total: {total}");
    //             return GetCountsRecursive(cards.Tail.Append(GetWonCards(card, allCards)), allCards, total + 1);
    //         })
    //         .IfNone(0);

    // private static Seq<ScratchCard> GetWonCards(ScratchCard card, Seq<ScratchCard> list)
    //     => card.WinningCount()
    //         .Apply(count => list.Skip(card.CardNumber).Take(count));

    private static IEnumerable<ScratchCard> GetWonCards(ScratchCard sc, Seq<ScratchCard> cards, Dictionary<int, int> counts)
    {
        var count = sc.WinningCount();
        var copies = counts.TryGetValue(sc.CardNumber, out var copy) ? copy : 1;
        return cards.Skip(sc.CardNumber).Take(count)
            .SelectMany(c => Enumerable.Repeat(c, copies));
    }
}