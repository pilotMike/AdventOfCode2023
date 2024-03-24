using System.Text.RegularExpressions;

namespace AdventOfCode2023.Extensions;

public static class RegexExtensions
{
    public static IEnumerable<string> GetMatchedGroupTexts(this MatchCollection matchCollection)
    {
        foreach (Match match in matchCollection)
        {
            foreach (Group group in match.Groups)
            {
                if (group.Length > 0 && !string.IsNullOrWhiteSpace(group.Value))
                {
                    yield return group.Value;
                }
            }
        }
    }
}