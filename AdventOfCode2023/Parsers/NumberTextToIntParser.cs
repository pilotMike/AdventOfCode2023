namespace AdventOfCode2023.Parsers;

public static class NumberTextToIntParser
{
    public static Option<int> Parse(string s) =>
        s switch
        {
            "one" or "1" => 1,
            "two" or "2" => 2,
            "three" or "3" => 3,
            "four" or "4" => 4,
            "five" or "5" => 5,
            "six" or "6" => 6,
            "seven" or "7" => 7,
            "eight" or "8" => 8,
            "nine" or "9" => 9,
            _ => None
        };
}