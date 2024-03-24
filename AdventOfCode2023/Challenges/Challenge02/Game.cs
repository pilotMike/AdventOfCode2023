using System.Collections;
using System.Text.RegularExpressions;
using AdventOfCode2023.Parsers;

namespace AdventOfCode2023.Challenges.Challenge02;

public enum CubeColor { Unknown, Red, Green, Blue }
public record struct CubeSet(CubeColor Cube, int Count) : IComparable<CubeSet>
{
    public int CompareTo(CubeSet other)
    {
        var cubeComparison = Cube.CompareTo(other.Cube);
        if (cubeComparison != 0) return cubeComparison;
        return Count.CompareTo(other.Count);
    }
}

public class GameSet(Set<CubeSet> cubeSets) : IEnumerable<CubeSet>
{
    private readonly Dictionary<CubeColor, int> _data = cubeSets.ToDictionary(x => x.Cube, x => x.Count);
    
    public Option<int> this [CubeColor cubeColor] => _data.TryGetValue(cubeColor, out var count) ? count : Option<int>.None;

    public IEnumerator<CubeSet> GetEnumerator() => cubeSets.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record Game(int ID, Seq<GameSet> GameSets)
{
    
}

public record GameConfiguration(GameSet Possibilities)
{
    public bool IsPossible(GameSet gameSet) => 
        (
            from potential in gameSet
            join cubeSet in Possibilities on potential.Cube equals cubeSet.Cube into g
            from possible in g.DefaultIfEmpty()
            select possible != default && potential.Count <= possible.Count
        ).All(b => b);
}





