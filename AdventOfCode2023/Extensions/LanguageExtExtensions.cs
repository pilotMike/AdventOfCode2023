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
}