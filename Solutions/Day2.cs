namespace Advent24.Solutions;

internal sealed class Day2 : Solution
{
    public Day2() : base(2)
    {
        
    }

    internal override string Part1()
    {
        var count = InputLines.Count(i => IsSafe(i.Split(" ").Select(n => int.Parse(n))));
        return count.ToString();
    }

    private bool IsSafe(IEnumerable<int> levels)
    {
        var isIncreasing = levels.First() < levels.Last();

        var levelList = isIncreasing ? levels.ToList() : levels.Reverse().ToList();
        var current = levelList.First();

        for (int i = 1; i < levelList.Count; i++)
        {
            var level = levelList[i];
            if (current + 3 >= level && current < level)
            {
                current = level;
                continue;
            }
            return false;
        }

        return true;
    }

    private bool IsSafeWithProblemDampener(IEnumerable<int> levels, bool dampened = false)
    {
        var isIncreasing = levels.First() < levels.Last();

        var levelList = isIncreasing ? levels.ToList() : levels.Reverse().ToList();
        var current = levelList.First();

        for (int i = 1; i < levelList.Count; i++)
        {
            var level = levelList[i];
            if (current + 3 >= level && current < level)
            {
                current = level;
                continue;
            }

            if (dampened)
            {
                return false;
            }

            return IsSafeWithProblemDampener([.. levelList[..(i)], .. levelList[(i+1)..]], true) 
                || IsSafeWithProblemDampener([.. levelList[..(i-1)], .. levelList[(i)..]], true);
        }

        return true;
    }

    internal override string Part2()
    {
        var count = InputLines.Count(i => IsSafeWithProblemDampener(i.Split(" ").Select(n => int.Parse(n))));
        return count.ToString();
    }
}
