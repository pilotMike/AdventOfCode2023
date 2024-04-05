global using SchematicCharacter =
    LanguageExt.Option<LanguageExt.Either<int, AdventOfCode2023.Challenges.Challenge03.Symbol>>;
using System.Numerics;
using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;
using MoreLinq;
using static AdventOfCode2023.Challenges.Challenge03.Module;


namespace AdventOfCode2023.Challenges.Challenge03;

public static class Module
{
    public static Symbol symbol => Symbol.Instance;
}

public record struct Symbol(char Character)
{
    public static Symbol Star => new Symbol('*');
    
    public static Symbol Instance { get; } = new();

    public static bool IsSymbol(char c) => c != '.' && !char.IsDigit(c);
}

public record struct Gear(SchematicNumber First, SchematicNumber Second);

public record struct SchematicNumber(PointRange PointRange, long Value)
{
    public SchematicNumber(Point start, long LengthX, long value)
        : this(new PointRange(start, start with { X = start.X + LengthX }), value)
    {
    }

    public bool Contains(Point point) => PointRange.Contains(point);
}

public class Schematic
{
    private readonly SchematicCharacter[,] _data;

    public Seq<SchematicNumber> SchematicNumbers { get; }
    public Seq<(Point Point, Symbol Symbol)> SchematicSymbols { get; }

    public Schematic(Seq<Seq<SchematicCharacter>> items)
    {
        var width = items[0].Length;
        var height = items.Length;

        _data = new SchematicCharacter[height, width];
        for (int y = 0; y < items.Length; y++)
        {
            for (int x = 0; x < items[y].Length; x++)
            {
                _data[y, x] = items[y][x];
            }
        }

        Point lastPoint = default;
        long lastLine = 0;
        SchematicNumbers = _data.SelectAll((Point, SchematicCharacter) => (Point, SchematicCharacter))
            .Segment(item =>
            {
                var newLine = lastLine != item.Point.Y;
                lastLine = item.Point.Y;
                return newLine;
            })
            .Select(line =>
            {
                return line.SegmentWithLast((lastOption, item) =>
                    {
                        // if first character, don't skip
                        var split = lastOption.Match(
                            last =>
                            {
                                var (lastIsBlank, lastIsNum) =
                                    last.SchematicCharacter.Match(e => (false, e.IsLeft), (true, false));
                                var (isBlank, isNum) =
                                    item.SchematicCharacter.Match(e => (false, e.IsLeft), (true, false));

                                return !(lastIsBlank == isBlank && lastIsNum == isNum);
                            },
                            () => false);
                        
                        lastPoint = item.Point;
                        return split;
                    })
                    .Where(grouping =>
                    {
                        var filter = grouping.First().SchematicCharacter.Match(x => x.IsLeft, false);
                        return filter;
                    })
                    .Select(grouping =>
                    {
                        Point? start = default, end = default;
                        Seq<int> characters = default;
                        foreach (var item in grouping)
                        {
                            start ??= item.Point;
                            end = item.Point;

                            characters = characters.Add(item.SchematicCharacter
                                .Match(e => e.Match(
                                        symbol => raise<int>(new Exception("Should have been left")),
                                        l => l),
                                    () => raise<int>(new Exception("Should have been some"))));
                        }

                        int value = characters.Aggregate((acc, i) => acc * 10 + i);

                        return (from s in start.ToOption()
                                from e in end.ToOption()
                                select new SchematicNumber(start.Value, end.Value.X - start.Value.X, value))
                            .IfNone(() =>
                                raise<SchematicNumber>(new Exception("didn't set either the start or the end")));
                    });
            })
            .SelectMany(x => x)
            .ToSeq();

        SchematicSymbols = _data.SelectAll((Point, SchematicCharacter) => (Point, SchematicCharacter))
            .Where(t => t.SchematicCharacter.Match(c => c.IsRight, false))
            .Select(t => (t.Point, t.SchematicCharacter.Match(c => c.Match(s => s, _ => raise<Symbol>(new Exception())), () => raise<Symbol>(new Exception()))))
            .ToSeq();
    }


    public bool IsAdjacentToSymbol(SchematicNumber schematicNumber) =>
        schematicNumber.PointRange.AdjacentPointsWithHorizontalOrientation()
            .Map(p => _data.Find(p))
            .Somes()
            .Somes()
            .Any(item => item.IsRight);

    public Option<Gear> IsGear((Point point, Symbol symbol) symbolPoint)
    {
        if (symbolPoint.symbol.Character != '*') return Option<Gear>.None;

        var pointsForNumbers = symbolPoint.point.GetAdjacentPoints()
            .Map(p => (p, entry:_data.Find(p)))
            .Map(res =>
                from cell in res.entry
                from value in cell
                where value.IsLeft
                select res.p)
            .Somes()
            .ToSeq();

        var adjacentSchematicNumbers =
            (
                from p in pointsForNumbers
                from num in SchematicNumbers
                where num.Contains(p)
                select num
            ).Distinct().ToArray();

        return adjacentSchematicNumbers.Length == 2
            ? new Gear(adjacentSchematicNumbers[0], adjacentSchematicNumbers[1])
            : Option<Gear>.None;
    }
}

public static class Parts
{
    public static long Part1(IChallenge03Input input)
    {
        var schematicDiagram = new Schematic(new Parser().Parse(input));

        return schematicDiagram.SchematicNumbers
            .Filter(num => schematicDiagram.IsAdjacentToSymbol(num))
            .Map(num => num.Value)
            .Sum();
    }

    public static long Part2(IChallenge03Input input)
    {
        var schematicDiagram = new Schematic(new Parser().Parse(input));
        var symbols = schematicDiagram.SchematicSymbols;

        return symbols
            .Map(t => schematicDiagram.IsGear(t))
            .Somes()
            .Map(g => g.First.Value * g.Second.Value)
            .Sum();
    }
}