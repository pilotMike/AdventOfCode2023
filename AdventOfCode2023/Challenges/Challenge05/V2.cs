using MoreLinq;
using AdventOfCode2023.Domain;

namespace AdventOfCode2023.Challenges.Challenge05;

// I'm not sure where I went wrong with the original implementation, so I started over.
public static class V2
{
    delegate MaterialValue MapMaterial(MaterialValue mv);
    record MaterialMapper(FarmMaterial Source, FarmMaterial Destination, MapMaterial MapFunc)
    {
        public MaterialMapper LinkTo(MaterialLookup next) =>
            new(Source, next.Destination,
                x => MapFunc(x).Apply(next.Map));
    }

    class FarmMaterial(string value) : NewType<FarmMaterial, string>(value);
    readonly record struct MaterialValue(FarmMaterial Material, long Value);
    
    static MyLongRange CreateRange(long start, long count) => MyLongRange.FromMinMax(start, start + count - 1, 1);
    record MaterialMap(FarmMaterial Source, FarmMaterial Destination, 
        MyLongRange SourceMap, MyLongRange DestinationMap)
    {
        public Option<MaterialValue> Map(MaterialValue mv) =>
            mv.Material != Source
                ? throw new ArgumentException("material doesn't match source", nameof(mv))
                : SourceMap.IndexOf(mv.Value)
                    .Bind(x => DestinationMap.ElementAtOrDefault(x))
                    .Map(v => new MaterialValue(Destination, v));

    }
    record MaterialLookup(FarmMaterial Source, FarmMaterial Destination)
    {
        private readonly List<MaterialMap> _maps = new();
        
        public MaterialLookup Add(MaterialMap map)
        {
            if (map.Source != Source || map.Destination != Destination)
                throw new ArgumentOutOfRangeException(nameof(map), "source and destination don't match");
            _maps.Add(map);
            return this;
        }

        public MaterialValue Map(MaterialValue mv) =>
            mv.Material != Source
                ? throw new ArgumentException("material doesn't match source", nameof(mv))
                : _maps.Choose(m => m.Map(mv)).DefaultIfEmpty(mv with { Material = Destination }).First();
    }

    public static long Part1(IChallenge05Input input) => new Parser().Parse(input)
        .Apply(t =>
        {
            var (seeds, lookups) = t;
            var linked = LinkLookups(lookups);
            var ret = seeds.Map(seed => linked.MapFunc(seed))
                .MinBy(x => x.Value)
                .Value;
            return ret;
        });

    private static MaterialMapper LinkLookups(Seq<MaterialLookup> materialLookups)
    {
        MaterialMapper mapper = materialLookups.Head.Apply(x => new MaterialMapper(x.Source, x.Destination, x.Map));
        materialLookups = materialLookups.Tail;
        while (materialLookups.Count > 0)
        {
            foreach (var next in materialLookups)
            {
                if (mapper.Destination == next.Source)
                {
                    mapper = mapper.LinkTo(next);
                    break;
                }
            }
            materialLookups = materialLookups.Tail;

        }

        return mapper;
    }
    
    class Parser
    {
        public (Seq<MaterialValue> seeds, Seq<MaterialLookup> materialLookups) Parse(IChallenge05Input input) =>
            input.ReadLines(true)
                .Split(string.IsNullOrWhiteSpace)
                .ToSeq()
                .Map(s => s.ToSeq())
                .Apply(sections =>
                {
                    // first section has one line
                    var seedSection = sections[0][0];

                    var seeds = seedSection[(seedSection.IndexOf(':') + 2)..]
                        .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse)
                        .Select(v => new MaterialValue(new FarmMaterial("seed"), v))
                        .ToSeq();

                    var sectionMaps = sections.Skip(1)
                        .Select(mappingSection =>
                        {
                            var names = ParseMaterialNames(mappingSection[0]);
                            return mappingSection.Skip(1)
                                .Map(line => line.Split(' ').Apply(parts =>
                                    (to: long.Parse(parts[0]), from: long.Parse(parts[1]), count: long.Parse(parts[2]))))
                                .Map(x => new MaterialMap(names.from, names.to,
                                    CreateRange(x.from, x.count), CreateRange(x.to, x.count)))
                                .Fold(new MaterialLookup(names.from, names.to), (l, n) => l.Add(n));
                        });

                    return (seeds, sectionMaps);
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
}