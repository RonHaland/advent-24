namespace Advent24.Solutions;

internal sealed class Day6 : Solution
{
    public Day6() : base(6)
    {
    }

    internal override string Part1()
    {
        var guardLine = InputLines.Single(l => l.Contains('^'));
        var (guardY, guardX) = (InputLines.ToList().IndexOf(guardLine), guardLine.IndexOf('^'));
        var visitedPositions = new HashSet<(int, int)>();
        var direction = Direction.N;

        while (IsInBounds(guardX, guardY))
        {
            visitedPositions.Add((guardX, guardY));
            (guardX, guardY, direction) = Move(guardX, guardY, direction);
        }

        return visitedPositions.Count.ToString();
    }

    private (int guardX, int guardY, Direction dir) Move(int guardX, int guardY, Direction dir)
    {
        var targetX = guardX + DirectionalMoves[dir].Item1;
        var targetY = guardY + DirectionalMoves[dir].Item2;
        if (!IsInBounds(targetX, targetY)){ return (targetX, targetY, dir); }

        if (InputLines[targetY][targetX] == '#') 
        {
            dir = (Direction)(((int)dir + 1) % 4);
            targetX = guardX + DirectionalMoves[dir].Item1;
            targetY = guardY + DirectionalMoves[dir].Item2;
        }
        guardX = targetX; guardY = targetY;

        return (guardX, guardY, dir);
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
        var guardLine = InputLines.Single(l => l.Contains('^'));
        var (guardY, guardX) = (InputLines.ToList().IndexOf(guardLine), guardLine.IndexOf('^'));
        var direction = Direction.N;
        var resultsInLoop = new HashSet<(int, int)>();
        var mustBeClearPositions = new HashSet<(int, int)>();

        while (IsInBounds(guardX, guardY))
        {
            mustBeClearPositions.Add((guardX, guardY));
            (guardX, guardY, direction) = Move(guardX, guardY, direction);

            if (IsInBounds(guardX, guardY) && !mustBeClearPositions.Contains((guardX, guardY)) && ResultsInLoop(guardX, guardY, direction))
            {
                resultsInLoop.Add((guardX, guardY));
            }
        }

        return resultsInLoop.Count.ToString();
    }

    private bool ResultsInLoop(int x, int y, Direction direction)
    {
        var (scanX, scanY) = (x - DirectionalMoves[direction].Item1, y - DirectionalMoves[direction].Item2);
        var directions = Enum.GetValues<Direction>().ToList();

        List<(int, int, Direction)> hitBlockers = [(x,y,direction)];

        //Put reorder so that current direction is last in queue.
        List<Direction> nextDirections = direction == Direction.W ? 
                directions : 
                [.. directions[directions.IndexOf(direction+1)..], .. directions[..directions.IndexOf(direction+1)]];

        Queue<Direction> directionQueue = new (nextDirections);

        while (directionQueue.Count > 0)
        {
            var searchDirection = directionQueue.Dequeue();
            var searchIsOutOfBounds = false;
            while (true)
            {
                scanX += DirectionalMoves[searchDirection].Item1;
                scanY += DirectionalMoves[searchDirection].Item2;
                if (!IsInBounds(scanX, scanY))
                {
                    searchIsOutOfBounds = true;
                    break;
                }

                if (InputLines[scanY][scanX] is '#' || (x,y) == (scanX, scanY))
                {
                    //hit
                    if (hitBlockers.Contains((scanX, scanY, searchDirection)))
                    {
                        //loop
                        return true;
                    }
                    hitBlockers.Add((scanX, scanY, searchDirection));
                    scanX -= DirectionalMoves[searchDirection].Item1;
                    scanY -= DirectionalMoves[searchDirection].Item2;
                    break;
                }

            }
            if (searchIsOutOfBounds) break; // no loop

            directionQueue.Enqueue(searchDirection);
        }

        return false;
    }

    private enum Direction
    {
        N,
        E,
        S,
        W,
    }

    private readonly Dictionary<Direction, (int, int)> DirectionalMoves = new()
    {
        { Direction.N, (0, -1) },
        { Direction.E, (1, 0) },
        { Direction.S, (0, 1) },
        { Direction.W, (-1, 0) },
    };

}
