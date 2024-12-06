
using System;

namespace Advent24.Solutions;

internal sealed class Day6 : Solution
{
    public Day6() : base(6)
    {
    }

    internal override string Part1()
    {
        var playerLine = InputLines.Single(l => l.Contains('^'));
        var (playerY, playerX) = (InputLines.ToList().IndexOf(playerLine), playerLine.IndexOf('^'));
        var visits = new HashSet<(int, int)>();
        var direction = Direction.N;

        while (IsInBounds(playerX, playerY))
        {
            visits.Add((playerX, playerY));
            (playerX, playerY, direction) = Move(playerX, playerY, direction);
        }

        return visits.Count.ToString();
    }

    private (int playerX, int playerY, Direction dir) Move(int playerX, int playerY, Direction dir)
    {
        var targetX = playerX + DirectionalMoves[dir].Item1;
        var targetY = playerY + DirectionalMoves[dir].Item2;
        if (!IsInBounds(targetX, targetY)){ return (targetX, targetY, dir); }

        if (InputLines[targetY][targetX] == '#') 
        {
            dir = (Direction)(((int)dir + 1) % 4);
            targetX = playerX + DirectionalMoves[dir].Item1;
            targetY = playerY + DirectionalMoves[dir].Item2;
        }
        playerX = targetX; playerY = targetY;

        return (playerX, playerY, dir);
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 
            && y >= 0 
            && x < InputLines.Max(c => c.Length) 
            && y < InputLines.Count();
    }

    internal override string Part2()
    {
        var playerLine = InputLines.Single(l => l.Contains('^'));
        var (playerY, playerX) = (InputLines.ToList().IndexOf(playerLine), playerLine.IndexOf('^'));
        var visits = new HashSet<(int, int)>();
        var direction = Direction.N;
        var resultsInLoop = new HashSet<(int, int)>();

        while (IsInBounds(playerX, playerY))
        {
            visits.Add((playerX, playerY));
            (playerX, playerY, direction) = Move(playerX, playerY, direction);
            if (IsInBounds(playerX, playerY) && ResultsInLoop(playerX, playerY, direction))
            {
                resultsInLoop.Add((playerX, playerY));
            }
        }

        return resultsInLoop.Count.ToString();
    }

    private bool ResultsInLoop(int x, int y, Direction direction)
    {
        var (scanX, scanY) = (x - DirectionalMoves[direction].Item1, y - DirectionalMoves[direction].Item2);
        var directions = Enum.GetValues<Direction>().ToList();

        List<(int, int, Direction)> hitBlockers = [(x,y,direction)];

        List<Direction> subDirections = direction == Direction.W ? directions : [.. directions[directions.IndexOf(direction+1)..], .. directions[..directions.IndexOf(direction+1)]];
        Queue<Direction> dirQueue = new (subDirections);
        var moves = 0;

        while (dirQueue.Count > 0)
        {
            var directionalMoves = 0;
            var subDir = dirQueue.Dequeue();
            var subDirOB = false;
            while (true)
            {
                scanX = scanX + DirectionalMoves[subDir].Item1;
                scanY = scanY + DirectionalMoves[subDir].Item2;
                if (!IsInBounds(scanX, scanY))
                {
                    subDirOB = true;
                    break;
                }

                if (InputLines[scanY][scanX] is '#' || (x,y) == (scanX, scanY))
                {
                    //hit
                    if (hitBlockers.Contains((scanX, scanY, subDir)))
                    {
                        //loop
                        Console.WriteLine("[{0}, {1}] causes loop", x, y);
                        return true;
                    }
                    hitBlockers.Add((scanX, scanY, subDir));
                    scanX = scanX - DirectionalMoves[subDir].Item1;
                    scanY = scanY - DirectionalMoves[subDir].Item2;
                    break;
                }
                directionalMoves++;
                moves++;
                if (moves > 10000) { Console.WriteLine("super long loop"); return false; }
            }
            if (subDirOB) break;
            dirQueue.Enqueue(subDir);
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
