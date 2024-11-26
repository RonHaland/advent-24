using Advent24.Solutions;
using System.Reflection;


while (true)
{
    Console.WriteLine("Enter day to run or press enter to run latest");

    var days = Assembly.GetExecutingAssembly().DefinedTypes
                            .Where(t => t.IsAssignableTo(typeof(Solution)) && t.Name != "Solution")
                            .Select(s => (Solution)s!.GetConstructor(Array.Empty<Type>())!
                                .Invoke(new object[0]));

    days = days.OrderBy(n => n.Number);

    Console.WriteLine($"Valid days: {string.Join(", ", days.Select(d => d.Number))}");

    var input = Console.ReadLine();
    Solution solutionToRun;

    if (int.TryParse(input, out var number) && days.Any(d => d.Number == number))
    {
        solutionToRun = days.First(d => d.Number == number);
    }
    else
    {
        solutionToRun = days.Last();
    }


    Console.WriteLine($"--- Running Day {solutionToRun.Number} with testinput ---");
    solutionToRun.RunTest();

    Console.WriteLine($"--- Running Day {solutionToRun.Number} ---");
    solutionToRun.RunReal();

    Console.WriteLine("");
    Console.WriteLine("Run another? (y/n)");
    var again = Console.ReadKey();
    if (again.KeyChar != 'y' && again.KeyChar != 'Y')
    {
        break;
    }
    Console.Clear();
}