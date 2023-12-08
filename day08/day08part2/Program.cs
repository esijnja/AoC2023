var input = File.ReadAllLines("input.txt");

var sequence = input[0].ToCharArray();

var nav = input[2..];

var instructions = new Dictionary<string, Node>();

var starterKeys = new List<string>();

// read the navigation instructions
foreach (var instruction in nav)
{
    var key = instruction[0..3];
    var left = instruction[7..10];
    var right = instruction[12..15];
    Console.WriteLine($"{key} {left} {right}");
    instructions.Add(key, new (left, right));
    if (key.EndsWith("A"))
    {
        starterKeys.Add(key);
    }
}


var nodes = new List<Node>();
var maxSteps = new List<long>();
foreach (var key in starterKeys)
{
    nodes.Add(instructions[key]);
    maxSteps.Add(0);
}
Console.WriteLine($"{nodes.Count}");
long steps = 0;
bool atTheEnd = false;
int sequenceLength = sequence.Length;
while (!atTheEnd)
{
    
    var stepInSequence = sequence[steps % sequenceLength]; 
    steps++;
    if (steps%1000000==0) Console.WriteLine($"{steps}");
    for (var i=0; i<nodes.Count; i++)
    {
        if (stepInSequence == 'L')
        {
            if (nodes[i].L.EndsWith("Z")) maxSteps[i] = steps;
            nodes[i] = instructions[nodes[i].L];
        }
        else
        {
            if (nodes[i].R.EndsWith("Z")) maxSteps[i] = steps;
            nodes[i] = instructions[nodes[i].R];
        }
    }
    
    atTheEnd = maxSteps.All(x=> x != 0);
}

Console.WriteLine($"{ string.Join(", ", maxSteps)}");

static long GreatestCommonDivisor(long a, long b)
{
    return b == 0L ? a : GreatestCommonDivisor(b, a % b);
}

static long LeastCommonMultipol(long a, long b)
{
    Console.WriteLine($"{a} {b}");
    return ((a / GreatestCommonDivisor(a, b))*b) ;
}

var result = maxSteps.Aggregate(LeastCommonMultipol);

Console.WriteLine($"{result}");
record Node(string L, string R);

