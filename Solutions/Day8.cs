using System.Linq;

namespace Advent24.Solutions;

internal sealed class Day8 : Solution
{
    public Day8() : base(8)
    {
        
    }

    private static char[] invalidChars = ['\r', '\n', '.'];

    internal override string Part1()
    {
        HashSet<(int, int)> antiNodes = [];

        var frequencies = Input.Where(c => !invalidChars.Contains(c)).GroupBy(c => c).Distinct();
        var antennas = InputLines.SelectMany((l, y) => l.Select((c, x) => (x, y, c))).Where(c => !invalidChars.Contains(c.c)).GroupBy(c => c.c).ToDictionary(k => k.Key, v => v.Select(c => (c.x, c.y)).ToList());

        foreach (var frequency in antennas)
        {
            var newAntiNodes = frequency.Value.SelectMany((a, i) =>
            {
                List<(int, int)> targetAntennas = [.. frequency.Value[..i], .. frequency.Value[(i + 1)..]];
                return targetAntennas.SelectMany((t, ti) =>
                {
                    var diff = (t.Item1 - a.x, t.Item2 - a.y);
                    List<(int, int)> newAntiNodes = [(t.Item1 + diff.Item1, t.Item2 + diff.Item2), (a.x - diff.Item1, a.y - diff.Item2)];
                    return newAntiNodes;
                });
            });

            foreach (var node in newAntiNodes)
            {
                if (IsInBounds(node.Item1, node.Item2))
                    antiNodes.Add(node);
            }
        }


        return antiNodes.Count.ToString();
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0
            && y >= 0
            && x < InputLines.Max(c => c.Length)
            && y < InputLines.Length;
    }

    internal override string Part2()
    {
        return "";
    }
}
