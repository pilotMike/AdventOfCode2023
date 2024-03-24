using System.Text;

namespace AdventOfCode2023.Extensions;

public static class StringExtensions
{
    public static string JoinString(this IEnumerable<char> chars) => 
        chars.Aggregate(new StringBuilder(), (sb, next) => sb.Append(next)).ToString();
    
    public static string JoinString(this IEnumerable<string> strings) => 
        strings.Aggregate(new StringBuilder(), (sb, next) => sb.Append(next)).ToString();
    
    public static string JoinString(this IEnumerable<string> strings, string splitter) => 
        strings.Aggregate(new StringBuilder(), (sb, next) => sb.AppendJoin(splitter, next)).ToString();
}