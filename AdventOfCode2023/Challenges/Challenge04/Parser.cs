using AdventOfCode2023.Domain;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge04;

public class Parser : IParser<HailStone, IChallenge04Input>
{
    public Seq<HailStone> Parse(IChallenge04Input input) =>
        input.ReadLines()
            .Map(line =>
            {
                var parts = line.Split('@');
                var coordinates = parts[0].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var velocity = parts[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var point = (Point3)(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));
                var vel = (Velocity3)(int.Parse(velocity[0]), int.Parse(velocity[1]), int.Parse(velocity[2]));

                return new HailStone(point, vel);
            });
}