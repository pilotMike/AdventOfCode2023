namespace AdventOfCode2023.Domain;

public readonly record struct Distance : IComparable<Distance>
{
    public Distance(double distance) => Value = Math.Round(distance, 3);

    public double Value { get; }

    public int CompareTo(Distance other) => Value.CompareTo(other.Value);
}