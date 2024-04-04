namespace AdventOfCode2023.Domain;

public readonly record struct NumberIndex(long Value, bool IsInclusive);
public readonly record struct NumberRange(NumberIndex Start, NumberIndex End)
{
    public bool Contains(long n) => 
        (Start.IsInclusive ? Start.Value <= n : Start.Value < n) &&
        (End.IsInclusive ? End.Value >= n : End.Value > n);

    public bool Contains(double n) => 
        (Start.IsInclusive ? Start.Value <= n : Start.Value < n) &&
        (End.IsInclusive ? End.Value >= n : End.Value > n);
}