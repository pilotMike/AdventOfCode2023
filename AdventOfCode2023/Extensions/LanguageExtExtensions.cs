using System.Numerics;
using AdventOfCode2023.Domain;
using LanguageExt.TypeClasses;

namespace AdventOfCode2023.Extensions;

public static class LanguageExtExtensions
{
    /// <summary>
    /// The inverse of a filter
    /// </summary>
    /// <example>
    /// <code>
    /// strings.FilterNot(string.IsNullOrWhiteSpace); // filters out null or whitespace
    /// </code></example>
    public static IEnumerable<T> FilterNot<T>(this IEnumerable<T> source, Predicate<T> filter) =>
        source.Filter(x => !filter(x));
    
    /// <summary> If the predicate is matched, returns the first result and the index of the matching item. </summary>
    public static Option<(T item, int index)> FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate) => 
        source.Select((item, index) => (item, index)).Find(t => predicate(t.item));

    public static Seq<(T first, T second)> CartesianPairs<T>(this Seq<T> seq) =>
        from ta in seq.MapS((i, x) => (i, x))
        from b in seq.Skip(ta.i + 1)
        select (ta.x, b);

    public static Seq<TOut> MapS<T, TOut>(this Seq<T> seq, Func<int, T, TOut> projection) =>
        seq.Select((x, i) => projection(i, x)).ToSeq();

}

public static class RangeExtensions
{
    public static Option<int> IndexOf(this LongRange range, long value)
    {
        if (!range.InRange(value)) return None;
        int idx = 0;
        foreach (var item in range)
        {
            if (item == value)
                return idx;
            
            idx++;
        }
        return None;
    }
    
    public static Option<long> IndexOf(this MyLongRange range, long value)
    {
        if (!range.InRange(value)) return None;
        return value - range.From;
        // int idx = 0;
        // foreach (var item in range)
        // {
        //     if (item == value)
        //         return idx;
        //     
        //     idx++;
        // }
        // return None;
    }

    public static Option<long> ElementAtOrDefault(this MyLongRange range, long index)
    {
        if (!range.InRange(index + range.From)) return Option<long>.None;
        return index + range.From;
    }
    
    // public static Option<int> IndexOf<TRange, TMonoid, T>(this TRange range, T item)
    //     where TRange : Range<TRange, TMonoid, T>
    //     where TMonoid : struct, Monoid<T>, Ord<T>, Arithmetic<T>
    //     where T : IEquatable<T>
    // {
    //     if (!range.InRange(item)) return Option<int>.None;
    //     int i = 0;
    //     foreach (var x in range)
    //     {
    //         i++;
    //         if (x.Equals(item))
    //             return i;
    //     }
    //
    //     return Option<int>.None;
    // }
    //
    // public static Option<int> IndexOf<TSelf, TMonoid, T>(this Range<TSelf, TMonoid, T> range, T item)
    //     where TSelf : Range<TSelf, TMonoid, T>
    //     where TMonoid : struct, Monoid<T>, Ord<T>, Arithmetic<T>
    // {
    //     if (!range.InRange(item)) return Option<int>.None;
    //     int i = 0;
    //     foreach (var x in range)
    //     {
    //         i++;
    //         if (x.Equals(item))
    //             return i;
    //     }
    //
    //     return Option<int>.None;
    // }
}