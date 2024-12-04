using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Advent24.Solutions;

internal sealed partial class Day4 : Solution
{
    public Day4() : base(4)
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
        var word = "XMAS";
        var count = 0;

        for (var y = 0; y < InputLines.Length; y++) 
        {
            for (var x = 0; x < InputLines[y].Length; x++)
            {
                if (InputLines[y][x] == word.First())
                {
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
        }

        return count.ToString();
    }

    private string Part2()
    {
        return "";
    }

    [GeneratedRegex("X")]
    private static partial Regex XRegex();

    private Stack<T> MakeStack<T>(IEnumerable<T> input)
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

    private Dictionary<Direction, (int, int)> DirectionalSearch = new Dictionary<Direction, (int, int)> { 
        { Direction.N, (0, -1) },
        { Direction.NE, (1, -1) },
        { Direction.E, (1, 0) },
        { Direction.SE, (1, 1) },
        { Direction.S, (0, 1) },
        { Direction.SW, (-1, 1) },
        { Direction.W, (-1, 0) },
        { Direction.NW, (-1, -1) },
    };
}
