using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;
using MoreLinq;

namespace AdventOfCode2023.Challenges.Challenge05;

public interface IChallenge05Input : IInputSource<(Seq<MaterialValue> seeds, Seq<MaterialMapCollection> maps)>;
 // interface IChallenge05Parser : IParser<IChallenge05Input, Either<Seq<Seed>, FarmMapCollection>
public class Input() : InputSource(ChallengeNumber.New(5), ChallengePart.Part1), IChallenge05Input;
class Parser
{
    public (Seq<MaterialValue> seeds, Seq<MaterialMapCollection> maps) Parse(IChallenge05Input input)
        => input.ReadLines()
            .Split(string.IsNullOrWhiteSpace)
            .ToSeq()
            .Map(s => s.ToSeq())
            .Apply(sections =>
            {
                // first section has one line
                var seedSection = sections[0][0];

                var seeds = seedSection[(seedSection.IndexOf(':') + 2)..]
                    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .Select(v => new MaterialValue(new FarmMaterial("seed"), v))
                    .ToSeq();

                var allMaps = new List<MaterialMapCollection>();
                foreach (var mappingSection in sections.Skip(1))
                {
                    var names = ParseMaterialNames(mappingSection[0]);
                    var ranges = mappingSection.Skip(1)
                        .Map(line => line.Split(' ').Apply(parts =>
                            (to: int.Parse(parts[0]), from: int.Parse(parts[1]), count: int.Parse(parts[2]))));

                    var materialRanges = ranges.Map(r =>
                            (
                                from: new MaterialRange(names.from, r.from, r.count),
                                to: new MaterialRange(names.to, r.to, r.count)
                            )
                        ).Map(x => new MaterialMap(x.from, x.to))
                        .OrderBy(x => x.Source.Start)
                        .Apply(maps => new MaterialMapCollection(names.from, names.to, maps));
                    allMaps.Add(materialRanges);
                }

                return (seeds, allMaps.ToSeq());
            });

    static (FarmMaterial from, FarmMaterial to) ParseMaterialNames(string line)
    {
        var span = line.AsSpan();
        var first = line.IndexOf('-');
        var second = line.AsSpan().Slice(first + 1).IndexOf('-') + first + 1;

        var from = span[..first];
        var to = span.Slice(second + 1, span.IndexOf(' ') - second);

        return (new FarmMaterial(from.Trim().ToString()), new FarmMaterial(to.Trim().ToString()));
    }
}