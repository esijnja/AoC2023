var input = File.ReadAllLines("input.txt");

var sequence = input[0].ToCharArray();

var nav = input[2..];

var instructions = new Dictionary<string, (string L, string R)>();

var start = "AAA" ; //nav[0][0..3];
var end =  "ZZZ" ;//nav[^1][0..3];

// read the navigation instructions
foreach (var instruction in nav)
{
    var key = instruction[0..3];
    var left = instruction[7..10];
    var right = instruction[12..15];
    Console.WriteLine($"{key} {left} {right}");
    instructions.Add(key, (left, right));
}

var node = instructions[start];
int steps = 0;
bool atTheEnd = false;
int sequenceLength = sequence.Length;
while (!atTheEnd)
{
    var stepInSequence = sequence[steps % sequenceLength]; 
    Console.WriteLine($"{steps} {stepInSequence} {node.L} {node.R} {end}");
    if (stepInSequence == 'L')
    {
        if (node.L == end)
        {
            atTheEnd = true;
        }
        node = instructions[node.L];
    }
    else
    {
        if (node.R == end)
        {
            atTheEnd = true;
        }
        node = instructions[node.R];
    }
    steps++;
}

Console.WriteLine($"{steps}");


