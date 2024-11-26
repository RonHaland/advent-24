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

    public abstract void Run();

    public void RunReal()
    {
        ReadInput();
        if (string.IsNullOrWhiteSpace(Input))
        {
            Console.WriteLine("No input for day {0}", Number);
            return;
        }
        Run();
    }

    public void RunTest()
    {
        ReadInput(true);
        if (string.IsNullOrWhiteSpace(Input))
        {
            Console.WriteLine("No test input for day {0}", Number);
            return;
        }
        Run();
    }

    private void ReadInput(bool test = false)
    {
        var inputPath = $"./Inputs{(test ? "Test" : "")}/{Number}.txt";
        Input = (File.Exists(inputPath)) ? File.ReadAllText(inputPath) : "";
        InputLines = Input.Split(Environment.NewLine);
    }
}
