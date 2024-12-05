using System.Text.RegularExpressions;

namespace Advent24.Solutions;

internal sealed partial class Day3 : Solution
{
    public Day3() : base(3)
    {

    }
    internal override string Part1()
    {
        var matches = MulRegex().Matches(Input);

        var sum = matches
            .Select(m => m.Value[4..^1].Split(',').Select(n => int.Parse(n))
            .Aggregate(1, (prev, cur) => prev * cur ))
            .Sum();
        return sum.ToString();
    }

    internal override string Part2() 
    {
        var mulList = MulRegex().Matches(Input).Select(m => (m.Value[4..^1], m.Index));
        var doList = DoRegex().Matches(Input).Select(m => ("TRUE", m.Index));
        var dontList = DontRegex().Matches(Input).Select(m => ("FALSE", m.Index));

        IEnumerable<(string, int)> combined = [.. mulList, .. doList, .. dontList];

        var enabled = true;
        var sum = 0;
        foreach (var match in combined.OrderBy(c => c.Item2))
        {
            switch (match.Item1)
            {
                case "FALSE":
                    enabled = false; continue;
                case "TRUE":
                    enabled = true; continue;
            }

            if (!enabled) continue;

            sum += match.Item1.Split(',').Select(n => int.Parse(n)).Aggregate(1, (prev, cur) => prev * cur);
        }

        return sum.ToString();
    }

    [GeneratedRegex("mul\\([0-9]+,[0-9]+\\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex("do\\(\\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex("don\\'t\\(\\)")]
    private static partial Regex DontRegex();
}
