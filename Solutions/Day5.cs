using System.Diagnostics;
using System.Linq;

namespace Advent24.Solutions;

internal sealed class Day5 : Solution
{
    private List<string> pageRulesInput = [];
    private List<List<int>> pagesInput = [];
    public Day5() : base(5)
    {
    }

    internal override void Setup()
    {
        var inputList = InputLines.ToList();
        pageRulesInput = inputList[..inputList.IndexOf("")];
        pagesInput = inputList[(inputList.IndexOf("")+1)..].Select(p => p.Split(",").Select(int.Parse).ToList()).ToList();
    }

    internal override string Part1()
    {
        var ruleDict = MakeRuleDict();
        var sum = 0;

        foreach (var pageset in pagesInput)
        {
            var isCorrectOrder = pageset.Select((s, i) =>
            {
                return !ruleDict[s].Before.Any(r =>
                    pageset[(i + 1)..].Contains(r.Number));
            }).All(c => c);

            sum += isCorrectOrder ? pageset[pageset.Count / 2] : 0;
        }


        return sum.ToString();
    }

    private Dictionary<int, PageRule> MakeRuleDict()
    {
        var ruleDict = new Dictionary<int, PageRule>();

        foreach (var rule in pageRulesInput)
        {
            var numbers = rule.Split("|").Select(int.Parse);
            var before = numbers.First();
            var after = numbers.Last();
            var beforeRule = ruleDict.TryGetValue(before, out var rule1) ? rule1 : new(before);
            var afterRule = ruleDict.TryGetValue(after, out var rule2) ? rule2 : new(after);
            beforeRule.AddAfter(afterRule);
            afterRule.AddBefore(beforeRule);
            ruleDict[before] = beforeRule;
            ruleDict[after] = afterRule;
        }

        return ruleDict;
    }

    internal override string Part2()
    {
        var ruleDict = MakeRuleDict();
        var sum = 0;

        foreach (var pageset in pagesInput)
        {
            var isCorrectOrder = pageset.Select((s, i) =>
            {
                return !ruleDict[s].Before.Any(r =>
                    pageset[(i + 1)..].Contains(r.Number));
            }).All(c => c);

            if (isCorrectOrder) continue;

            List<int> orderedPages = [..pageset];
            while (!isCorrectOrder)
            {
                var firstIncorrect = orderedPages.Select((s, i) =>
                {
                    return (!ruleDict[s].Before.Any(r =>
                        orderedPages[(i + 1)..].Contains(r.Number)), i);
                }).First(s => !s.Item1);

                orderedPages = [.. orderedPages[..firstIncorrect.i], orderedPages[firstIncorrect.i + 1], ..orderedPages[(firstIncorrect.i + 2)..], orderedPages[firstIncorrect.i]];

                isCorrectOrder = orderedPages.Select((s, i) =>
                {
                    return !ruleDict[s].Before.Any(r =>
                        orderedPages[(i + 1)..].Contains(r.Number));
                }).All(c => c);
            }
            
            sum += orderedPages[orderedPages.Count / 2];
        }


        return sum.ToString();
    }

    class PageRule(int number)
    {
        public int Number { get; set; } = number;

        public List<PageRule> Before = [];
        public List<PageRule> After = [];

        public void AddBefore(PageRule rule)
        {
            Before.Add(rule);
        }

        public void AddAfter(PageRule rule)
        {
            After.Add(rule);
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
