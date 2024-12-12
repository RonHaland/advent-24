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
        var initialStones = Input.Split(' ').Select(long.Parse).ToDictionary(k => k, v => 1L);
        var iterationStones = initialStones;

        foreach (var n in Enumerable.Range(0, 75))
        {
            //Key: Stone value, Value: count
            var nextIterationStones = new Dictionary<long, long>();

            foreach (var stone in iterationStones) 
            {
                var nextStones = ExecuteRulesOnStone(stone.Key);
                foreach (var nextStone in nextStones) 
                {
                    nextIterationStones[nextStone] = nextIterationStones.TryGetValue(nextStone, out var val) ? val + stone.Value : stone.Value;
                }
            }

            iterationStones = nextIterationStones;
        }
        return iterationStones.Sum(v => v.Value).ToString();
    }
}
