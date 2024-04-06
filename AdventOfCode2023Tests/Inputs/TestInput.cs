using AdventOfCode2023.Parsers;
using Moq;

namespace AdventOfCode2023Tests.Inputs;

internal static class TestInput
{
    public static TInput Create<TInput, T>(string uri) 
        where TInput : class, IInputSource<T>
    {
        var mock = new Mock<TInput>();
        mock.Setup(x => x.ReadLines(It.IsAny<bool>()))
            .Returns(() => new TestInput<T>($"../../../Inputs/{uri}").ReadLines());
        return mock.Object;
    }
    
    public static TInput CreateForString<TInput, T>(string text)
        where TInput : class, IInputSource<T>
    {
        var mock = new Mock<TInput>();
        mock.Setup(x => x.ReadLines(It.IsAny<bool>()))
            .Returns(() => text.Split(Environment.NewLine).ToSeq());
        return mock.Object;
    }
}

public class TestInput<T>(string uri) : IInputSource<T>
{
    public Seq<string> ReadLines(bool includeEmptyLines = false) => File.ReadLines(uri).ToSeq();
}