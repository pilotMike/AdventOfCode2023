using AdventOfCode2023.Domain;
using FluentAssertions;

namespace AdventOfCode2023Tests.Domain;

public class NumberRangeTests
{
    [Theory]
    [InlineData(10, true, 20, true, 10, true)]
    [InlineData(10, true, 20, true, 15, true)]
    [InlineData(10, true, 20, true, 20, true)]
    [InlineData(10, false, 20, true, 10, false)]
    [InlineData(10, false, 20, true, 15, true)]
    [InlineData(10, false, 20, true, 20, true)]
    [InlineData(10, false, 20, false, 10, false)]
    [InlineData(10, false, 20, false, 15, true)]
    [InlineData(10, false, 20, false, 20, false)]
    public void Contains_IsCorrect(int start, bool startInclusive, int end, bool endInclusive, int value, bool expected)
    {
        new NumberRange(new(start, startInclusive), new(end, endInclusive))
            .Contains(value)
            .Should()
            .Be(expected);
    }
    
    [Theory]
    [InlineData(10, true, 20, true, 10, true)]
    [InlineData(10, true, 20, true, 15, true)]
    [InlineData(10, true, 20, true, 20, true)]
    [InlineData(10, false, 20, true, 10, false)]
    [InlineData(10, false, 20, true, 15, true)]
    [InlineData(10, false, 20, true, 20, true)]
    [InlineData(10, false, 20, false, 10, false)]
    [InlineData(10, false, 20, false, 15, true)]
    [InlineData(10, false, 20, false, 20, false)]
    public void Contains_IsCorrect_Doubles(int start, bool startInclusive, int end, bool endInclusive, double value, bool expected)
    {
        new NumberRange(new(start, startInclusive), new(end, endInclusive))
            .Contains(value)
            .Should()
            .Be(expected);
    }
}