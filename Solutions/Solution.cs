using System.Diagnostics;

namespace Advent24.Solutions;

internal abstract class Solution
{
    internal int Number { get; set; }
    internal string Input { get; set; } = "";
    internal string[] InputLines { get; set; } = [];

    protected Solution(int number)
    {
        Number = number;
    }
    internal virtual void Setup() { }

    private void InternalRun()
    {
        Setup();
        var sw = Stopwatch.StartNew();

        Console.WriteLine("Part1!");
        Console.WriteLine(Part1());
        Console.WriteLine("Took {0} ms", sw.ElapsedMilliseconds);
        Console.WriteLine();
        sw.Restart();

        Console.WriteLine("Part2!");
        Console.WriteLine(Part2());
        Console.WriteLine("Took {0} ms", sw.ElapsedMilliseconds);
        Console.WriteLine();
    }

    internal abstract string Part1();
    internal abstract string Part2();

    public void RunReal()
    {
        ReadInput();
        if (string.IsNullOrWhiteSpace(Input))
        {
            Console.WriteLine("No input for day {0}", Number);
            return;
        }
        InternalRun();
    }

    public void RunTest()
    {
        ReadInput(true);
        if (string.IsNullOrWhiteSpace(Input))
        {
            Console.WriteLine("No test input for day {0}", Number);
            return;
        }
        InternalRun();
    }

    private void ReadInput(bool test = false)
    {
        var inputPath = $"./Inputs{(test ? "Test" : "")}/{Number}.txt";
        Input = (File.Exists(inputPath)) ? File.ReadAllText(inputPath) : "";
        InputLines = Input.Split(Environment.NewLine);

        if (string.IsNullOrWhiteSpace(InputLines.Last()))
        {
            InputLines = InputLines[..(InputLines.Length - 1)];
        }
    }
}
