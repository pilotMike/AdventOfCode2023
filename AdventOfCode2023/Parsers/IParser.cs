namespace AdventOfCode2023.Parsers;

public interface IParser<T, TInput> where TInput : IInputSource<T>
{
    Seq<T> Parse(TInput input);
}