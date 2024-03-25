using System.Diagnostics;

namespace AdventOfCode2023.Extensions;

public static class EnumerableSegmentExtensions
{
    /// <summary>
    /// The Segment function in MoreLinq skips over the first element. This causes bugs for me.
    /// This one takes a predicate that has the last item and the current item.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate">function that takes the Last Item and Current Item</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static IEnumerable<IEnumerable<T>> SegmentWithLast<T>(this IEnumerable<T> source, Func<Option<T>, T, bool> predicate)
    {
        Option<T> last = default;
        List<T> list = new();
        foreach (var item in source)
        {
            var ltemp = last;
            last = item;
            
            if (predicate(ltemp, item))
            {
                yield return list;
                list = new();
            }
            
            list.Add(item);
        }

        if (list.Count > 0)
        {
            yield return list;
        }
    }
}