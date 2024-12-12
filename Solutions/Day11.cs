namespace Advent24.Solutions;

internal sealed class Day11 : Solution
{
    public Day11() : base(11)
    {
        
    }

    internal override string Part1()
    {
        long[] initialStones = Input.Split(' ').Select(long.Parse).ToArray();
        var stones = initialStones.ToList();
        foreach (var _ in Enumerable.Range(0,25)) 
        {
            var stoneCount = stones.Count;
            List<long> newStoneOrder = [];
            foreach (var i in Enumerable.Range(0, stoneCount))
            {
                var resultStones = ExecuteRulesOnStone(stones[i]);
                newStoneOrder.AddRange(resultStones);
            }
            stones = newStoneOrder;
        }
        return stones.Count.ToString();
    }

    private long[] ExecuteRulesOnStone(long number)
    {
        if (number == 0)
        {
            return [1];
        }
        var numberString = number.ToString();
        if (numberString.Length % 2 == 0)
        {

            return [
                long.Parse(numberString.Substring(0,numberString.Length/2)),
                long.Parse(numberString.Substring(numberString.Length/2))
                ];
        }
        return [number * 2024];
    }

    internal override string Part2()
    {
        long[] initialStones = Input.Split(' ').Select(long.Parse).ToArray();
        var counts = 0;

        foreach (var n in Enumerable.Range(0, 75))
        {
            //Key: Stone value, Value: count
            var iterationStones = new Dictionary<long, long>();

            foreach (var stone in iterationStones) 
            {
                var nextStones = ExecuteRulesOnStone(stone.Value);

            }

        }

            //Parallel.ForEach(initialStones, (initialStone) =>
            //{
            //    List<long> stones = [initialStone];
            //    foreach (var n in Enumerable.Range(0, 75))
            //    {
            //        var stoneCount = stones.Count;
            //        List<long> newStoneOrder = [];
            //        foreach (var i in Enumerable.Range(0, stoneCount))
            //        {
            //            var resultStones = ExecuteRulesOnStone(stones[i]);
            //            newStoneOrder.AddRange(resultStones);
            //        }
            //        stones = newStoneOrder;

            //        Console.WriteLine("Done iteration {0} for stone {1}", n, initialStone);
            //    }
            //    counts += stones.Count;
            //});

            return counts.ToString();
    }
}
