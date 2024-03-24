namespace AdventOfCode2023.Parsers;

public interface IInputSource<TIOut>
{
    Seq<string> ReadLines();
}