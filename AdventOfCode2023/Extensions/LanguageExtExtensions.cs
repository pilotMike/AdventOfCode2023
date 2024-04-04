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
        from ta in seq.Map((i, x) => (i, x))
        from b in seq.Skip(ta.i + 1)
        select (ta.x, b);

    public static Seq<TOut> Map<T, TOut>(this Seq<T> seq, Func<int, T, TOut> projection) =>
        seq.Select((x, i) => projection(i, x)).ToSeq();

}