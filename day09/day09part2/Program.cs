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
    var r = TheLast(numbers.Reverse<long>().ToList());
    Console.WriteLine($"Last: {r}");
    result = result + r;

}


Console.WriteLine($"Result: {result}");


// var sequences = new List<List<Sequence>>();


// long DetermineLast(List<Sequence> nodes)
// {
    
    

//     var next = new List<Sequence>();
//     for (int i = 0; i < nodes.Count-1; i++)
//     {
//         var seq = new Sequence(nodes[i].right, nodes[i+1].left);
//         next.Add(seq);
//         Console.Write($" {seq.diff}");
//     }
//     Console.WriteLine();

//     if (next.All(n => n.diff == 0))
//     {
//         return next[^1].right;
//     }
//     return DetermineLast(next, next[^1].right + next[^1].diff);
// }



// long result = 0;

// foreach (var line in input)
// {
//     var split = line.Split(" ");
//     var nodes = new List<Sequence>();
//     for (int i = 0; i < split.Length-1; i++)
//     {
//         var seq = new Sequence(long.Parse(split[i]), long.Parse(split[i+1]));
//         nodes.Add(seq);
//         Console.Write($" {seq.diff}");
//     }
//     Console.WriteLine();
//     sequences.Add(nodes);

//     var last = DetermineLast(nodes, 0);
//     Console.WriteLine($"Last: {last}");
//     result =+ last;
// }

// Console.WriteLine($"Result: {result}");

// record Sequence(long left, long right)
// {
//     public long diff => right - left;
// }


