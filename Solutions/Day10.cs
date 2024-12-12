namespace Advent24.Solutions;

internal sealed class Day10 : Solution
{
    public Day10() : base(10)
    {
        
    }

    internal override string Part1()
    {
        List<Step> trailHeads = MakeTrails();
        return trailHeads.Sum(t => t.GetScore().Count).ToString();
    }

    internal override string Part2()
    {
        List<Step> trailHeads = MakeTrails();
        return trailHeads.Sum(t => t.GetRating()).ToString();
    }

    private List<Step> MakeTrails()
    {
        return InputLines.SelectMany((l, y) => l.Select((c, x) =>
        {
            if (c is not '0') return null;
            var trailHead = new Step(0, x, y);
            FindNextSteps(trailHead, x, y);
            return trailHead;
        })).Where(s => s is not null).Select(s => s!).ToList();
    }

    private class Step(int height, int x, int y)
    {
        public List<Step> Children { get; private set; } = [];
        public int Height { get; private set; } = height;

        public int X { get; private set; } = x;
        public int Y { get; private set; } = y;

        public HashSet<(int, int)> GetScore()
        {
            HashSet<(int, int)> reachablePeaks = [];
            if (Height == 9)
                return [(X, Y)];
            if (Children.Count == 0)
                return [];
            return Children.SelectMany(c => c.GetScore()).ToHashSet();
        }

        public int GetRating()
        {
            if (Height == 9)
                return 1;
            if (Children.Count == 0)
                return 0;
            return Children.Sum(c => c.GetRating());
        }
    }

    private void FindNextSteps(Step step, int x, int y)
    {
        foreach (var move in moves)
        {
            (int x, int y) target = (x + move.x, y + move.y);
            if (!IsInBounds(target.x, target.y))
            {
                continue;
            }
            var nextStepHeight = int.Parse(InputLines[target.y][target.x].ToString());
            if (nextStepHeight != step.Height + 1)
            {
                continue;
            }
            var nextStep = new Step(nextStepHeight, target.x, target.y);
            if (nextStepHeight < 9)
            {
                FindNextSteps(nextStep, target.x, target.y);
            }
            step.Children.Add(nextStep);
        }
    }

    internal bool IsInBounds(int x, int y)
    {
        return x >= 0
            && y >= 0
            && x < InputLines.Max(c => c.Length)
            && y < InputLines.Length;
    }

    internal static (int x, int y)[] moves = [(0, -1), (1, 0), (0, 1), (-1, 0)];
}
