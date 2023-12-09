var input = File.ReadAllLines("input.txt");

long TheLast(List<long> numbers)
{
    var next = new List<long>();
    for (int i = 0; i < numbers.Count-1; i++)
    {
        var diff = numbers[i+1] - numbers[i];
        next.Add(diff);
        Console.Write($" {diff}");
    }
    Console.WriteLine();
    if (next.All(n => n == 0))
    {
        return numbers[^1];
    }
    return numbers[^1] + TheLast(next);
}

long result = 0;
foreach (var line in input)
{
    var split = line.Split(" ");
    var numbers = split.Select(long.Parse).ToList();
    var r = TheLast(numbers);
    Console.WriteLine($"Last: {r}");
    result = result + r;

}


Console.WriteLine($"Result: {result}");


