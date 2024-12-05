namespace Advent24.Solutions;

internal sealed class Day1 : Solution
{
    public Day1() : base(1)
    {
        
    }

    internal override string Part1()
    {
        var sortedList2 = InputLines.Select(l => l.Split("   ").Last()).Order().ToList();
        var output = InputLines.Select(l => l.Split("   ").First()).Order().Select((n,i) =>
        {
            var number1 = int.Parse(n);
            var number2 = int.Parse(sortedList2.Skip(i).First());

            return Math.Abs(number1 - number2);
        }).Sum();

        return output.ToString();
    }

    internal override string Part2()
    {
        var list1 = InputLines.Select(l => l.Split("   ").First()).ToList();
        var list2 = InputLines.Select(l => l.Split("   ").Last()).ToList();

        var result = InputLines.Select(l => l.Split("   ").First()).Select((n) =>
        {

            return long.Parse(n) * list2.Count(n2 => n2 == n);
        }).Sum();

        return result.ToString();
    }
}
