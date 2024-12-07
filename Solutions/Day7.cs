namespace Advent24.Solutions;

internal sealed class Day7 : Solution
{
    public Day7() : base(7)
    {
    }

    internal override string Part1()
    {
        var sum = InputLines.Select(c =>
        {
            var test = long.Parse(c.Split(':').First());
            var numbers = new Queue<long>(c.Split(':').Last().Trim().Split(' ').Select(long.Parse));
            var node = new NumberNode(numbers.Dequeue());
            node.CreateBranches(numbers);
            return node.HasMatchInChildren(test) ? test : 0;
        }).Sum();

        return sum.ToString();
    }

    internal override string Part2()
    {
        var sum = InputLines.Select(c =>
        {
            var test = long.Parse(c.Split(':').First());
            var numbers = new Queue<long>(c.Split(':').Last().Trim().Split(' ').Select(long.Parse));
            var node = new NumberNode(numbers.Dequeue());
            node.CreateBranches2(numbers, test);
            return node.HasMatchInChildren(test) ? test : 0;
        }).Sum();

        return sum.ToString();
    }

    private class NumberNode(long Number)
    {
        private readonly List<NumberNode> Children = [];
        public void CreateBranches(Queue<long> numbers)
        {
            if (numbers is { Count: 0 })
                return;
            var nextNumber = numbers.Dequeue();
            foreach (var op in Enum.GetValues<Operation>())
            {
                if ( op == Operation.Concat)
                {
                    continue;
                }
                var childNumber = op switch
                {
                    Operation.Add => Number + nextNumber,
                    Operation.Multiply => Number * nextNumber,
                    _ => Number
                };
                var childNode = new NumberNode(childNumber);
                childNode.CreateBranches(new Queue<long>([..numbers]));
                Children.Add(childNode);
            }
        }

        public void CreateBranches2(Queue<long> numbers, long value)
        {
            if (numbers is { Count: 0 })
                return;
            var nextNumber = numbers.Dequeue();
            foreach (var op in Enum.GetValues<Operation>())
            {
                var childNumber = op switch
                {
                    Operation.Add => Number + nextNumber,
                    Operation.Multiply => Number * nextNumber,
                    Operation.Concat => long.Parse(Number.ToString() + nextNumber.ToString()),
                    _ => Number
                };
                var childNode = new NumberNode(childNumber);
                if (childNumber > value) { childNode.IsFailed = true; }
                else
                {
                    childNode.CreateBranches2(new Queue<long>([.. numbers]), value);
                }
                Children.Add(childNode);
            }
        }

        public bool IsFailed = false;

        public bool HasMatchInChildren(long value)
        {
            if (IsFailed)
                return false;
            if (Children is { Count: 0 })
                return Number == value;

            return Children.Any(c => c.HasMatchInChildren(value));
        }
    }

    private enum Operation
    {
        Multiply,
        Add,
        Concat,
    }
}
