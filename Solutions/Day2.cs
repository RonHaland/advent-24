namespace Advent24.Solutions;

internal sealed class Day2 : Solution
{
    public Day2() : base(2)
    {
        
    }

    public override void Run()
    {
        Console.WriteLine("Part1!");
        Console.WriteLine(Part1());


        Console.WriteLine("Part2!");
        Console.WriteLine(Part2());
    }

    private string Part1()
    {
        var count = InputLines.Where(i => IsSafe(i.Split(" ").Select(n => int.Parse(n))));
        return count.Count().ToString();
    }

    private bool IsSafe(IEnumerable<int> levels)
    {
        var isIncreasing = levels.First() < levels.Last();
        var safe = levels.Skip(1).Aggregate((true, levels.First()), (prev, curr) =>
        {
            if (!prev.Item1) return prev;
            if (isIncreasing && (prev.Item2 >= curr || prev.Item2 + 3 < curr)) 
            {
                return (false, curr);
            }
            if (!isIncreasing && (prev.Item2 <= curr || prev.Item2 > curr + 3))
            {
                return (false, curr);
            }

            return (true, curr);
        }).Item1;
        return safe;
    }


    private bool IsSafeWith1Removed(IEnumerable<int> levels)
    {
        var isIncreasing = levels.First() < levels.Last();
        var safe = levels.Skip(1).Aggregate((true, levels.First(), false), (prev, curr) =>
        {
            if (!prev.Item1 && prev.Item3) return prev;
            if (isIncreasing && (prev.Item2 >= curr || prev.Item2 + 3 < curr))
            {
                if (!prev.Item3)
                    return (true, prev.Item2, true);
                return (false, curr, true);
            }
            if (!isIncreasing && (prev.Item2 <= curr || prev.Item2 > curr + 3))
            {
                if (!prev.Item3)
                    return (true, prev.Item2, true);
                return (false, curr, true);
            }

            return (true, curr, prev.Item3);
        }).Item1;
        return safe;
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

    private string Part2()
    {

        var count = InputLines.Where(i => IsSafeWithProblemDampener(i.Split(" ").Select(n => int.Parse(n))));
        return count.Count().ToString();
    }
}
