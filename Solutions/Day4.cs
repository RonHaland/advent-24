namespace Advent24.Solutions;

internal sealed partial class Day4 : Solution
{
    public Day4() : base(4)
    {

    }

    internal override string Part1()
    {
        var word = "XMAS";
        var count = 0;

        for (var y = 0; y < InputLines.Length; y++) 
        {
            for (var x = 0; x < InputLines[y].Length; x++)
            {
                if (InputLines[y][x] != word.First())
                {
                    continue;
                }

                foreach(Direction dir in Enum.GetValues<Direction>())
                {
                    var stack = MakeStack(word);
                    stack.Pop();
                    var curX = x;
                    var curY = y;
                    var failed = false;
                    while (stack.Count > 0)
                    {
                        var searchX = curX + DirectionalSearch[dir].Item1;
                        var searchY = curY + DirectionalSearch[dir].Item2;

                        if (searchX < 0 || searchY < 0 || searchY > InputLines.Length-1 || searchX > InputLines[searchY].Length-1)
                        {
                            failed = true;
                            break;
                        }

                        if (InputLines[searchY][searchX] != stack.Pop())
                        {
                            failed = true;
                            break;
                        }

                        curX = searchX;
                        curY = searchY;
                    }
                    if (failed)
                    {
                        continue;
                    }
                    count++;
                }
            }
        }

        return count.ToString();
    }

    internal override string Part2()
    {
        char[] mustContain = ['M', 'S'];
        var count = 0;

        for (int y = 1; y < InputLines.Length-1; y++)
        {
            for (int x = 1; x < InputLines[y].Length-1; x++)
            {
                if (InputLines[y][x] != 'A')
                {
                    continue;
                }

                char[] NESW = [InputLines[y - 1][x + 1], InputLines[y + 1][x - 1]];
                char[] SENW = [InputLines[y + 1][x + 1], InputLines[y - 1][x - 1]];
                var xCross = (ContainsAll(mustContain, NESW) && ContainsAll(mustContain, SENW));

                if (xCross)
                {
                    count ++;
                }
            }
        }

        return count.ToString();
    }

    private static Stack<T> MakeStack<T>(IEnumerable<T> input)
    {
        return input.Reverse().Aggregate(new Stack<T>(), (prev, cur) =>
        {
            prev.Push(cur);
            return prev;
        });
    }

    private enum Direction
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }

    private readonly Dictionary<Direction, (int, int)> DirectionalSearch = new()
    { 
        { Direction.N, (0, -1) },
        { Direction.NE, (1, -1) },
        { Direction.E, (1, 0) },
        { Direction.SE, (1, 1) },
        { Direction.S, (0, 1) },
        { Direction.SW, (-1, 1) },
        { Direction.W, (-1, 0) },
        { Direction.NW, (-1, -1) },
    };

    private static bool ContainsAll<T>(IEnumerable<T> first, IEnumerable<T> second)
    {
        return first.All(f => second.Contains(f)) && second.All(f => first.Contains(f));
    }
}
