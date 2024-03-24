using System.Text.RegularExpressions;

namespace AdventOfCode2023.Parsers;

public partial class Regexes
{
    /// <summary>
    /// You'll need to pass the MatchCollection into <see cref="GetMatchedGroupTexts"/> to get the desired text
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"(?=([0-9]|one|two|three|four|five|six|seven|eight|nine))")]
    public partial Regex IsDigitOrSpelledNumber();
}