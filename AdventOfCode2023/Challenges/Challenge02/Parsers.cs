using AdventOfCode2023.Parsers;
using MoreLinq.Extensions;
using Pidgin;
using static Pidgin.Parser;

namespace AdventOfCode2023.Challenges.Challenge02;


public class Part1Parser : IParser<Game, IChallenge02Input>
{
    public Seq<Game> Parse(IChallenge02Input input)
    {
        var game = String("Game ");
        var cubeColors = OneOf(String("blue"), String("red"), String("green"));
        var start = OneOf(String(": "), String("; "), EndOfLine, String(", "));
    
        var parseCube =
            from s in start.Optional()
            from num in Num
            from _ in Whitespace
            from colorString in cubeColors
            let color = colorString switch
            {
                "green" => CubeColor.Green,
                "red" => CubeColor.Red,
                "blue" => CubeColor.Blue
            }
            select (cubeSet:new CubeSet(color, num), newGroup: s.Match(x => x == "; ",() => false));
        
        var parser =
            from gameid in game.Then(Num)
            from _ in String(": ")
            from cubesText in parseCube.Many()
            let gameSets =  cubesText.Segment(x => x.newGroup)
                .Select(g =>
                {
                    var cubeSets = g.Select(x => x.cubeSet);
                    return new GameSet(new Set<CubeSet>(cubeSets));
                })
                .ToArray()
            select new Game(gameid, gameSets.ToSeq());
    
        return input.ReadLines().Select(line =>
        {
            var res = parser.Parse(line);
            return res.Value;
        }).ToSeq();
    }
}