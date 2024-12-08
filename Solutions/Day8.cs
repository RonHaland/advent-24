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
                List<(int x, int y)> targetAntennas = [.. frequency.Value[..i], .. frequency.Value[(i + 1)..]];
                return targetAntennas.SelectMany((t, ti) =>
                {
                    (int x, int y) diff = (t.x - a.x, t.y - a.y);
                    List<(int x, int y)> newAntiNodes = [(t.x + diff.x, t.y + diff.y), (a.x - diff.x, a.y - diff.y)];
                    return newAntiNodes;
                });
            });

            foreach (var node in newAntiNodes)
            {
                if (IsInBounds(node.x, node.y))
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
        HashSet<(int, int)> antiNodes = [];

        var frequencies = Input.Where(c => !invalidChars.Contains(c)).GroupBy(c => c).Distinct();
        var antennas = InputLines.SelectMany((l, y) => l.Select((c, x) => (x, y, c))).Where(c => !invalidChars.Contains(c.c)).GroupBy(c => c.c).ToDictionary(k => k.Key, v => v.Select(c => (c.x, c.y)).ToList());

        foreach (var frequency in antennas)
        {
            var newAntiNodes = frequency.Value.SelectMany((a, i) =>
            {
                List<(int x, int y)> targetAntennas = [.. frequency.Value[..i], .. frequency.Value[(i + 1)..]];
                return targetAntennas.SelectMany((t, ti) =>
                {
                    (int x, int y) diff = (t.x - a.x, t.y - a.y);
                    List<(int x, int y)> newAntiNodes = [..AddNodes(t, diff, false), ..AddNodes(a, diff, true)];
                    return newAntiNodes;
                });
            });

            foreach (var node in newAntiNodes)
            {
                if (IsInBounds(node.x, node.y))
                    antiNodes.Add(node);
            }
        }

        return antiNodes.Count.ToString();
    }

    internal List<(int x, int y)> AddNodes((int x, int y) start, (int x, int y) diff, bool increase)
    {
        var result = new List<(int x, int y)>();
        (int x, int y) next = increase ? 
            (start.x + diff.x, start.y + diff.y) : 
            (start.x - diff.x, start.y - diff.y);
        while (IsInBounds(next.x, next.y))
        {
            result.Add((next.x, next.y));
            next = increase ?
            (next.x + diff.x, next.y + diff.y) :
            (next.x - diff.x, next.y - diff.y);
        }
        return result;
    }
}
