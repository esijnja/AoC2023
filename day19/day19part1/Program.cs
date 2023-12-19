// parse the input file

using Newtonsoft.Json;

var input = File.ReadAllLines("input.txt");

var workflows = new List<Workflow>();
var parts = new List<Part>();

var index = 0;

while (input[index] != "")
{
    var parts_ = input[index].Split('{', '}');
    var name = parts_[0];
    var rulestring = parts_[1].Split(',');
    var rules = new List<string>();
    for (int i = 0; i < rulestring.Length; i++)
    {
        rules.Add(rulestring[i]);
    }
    workflows.Add(new Workflow(name, rules));
    index++;
}
index++;
while (index < input.Length)
{
    var p = ParseLine(input[index][1..^1]);
    //var p = JsonConvert.DeserializeObject<Part>(input[index]);
    parts.Add(p);
    index++;
}

var start = workflows.First(w => w.name == "in");
var result = 0;
foreach (var part in parts)
{
    Console.WriteLine($"Part: {part.x}, {part.m}, {part.a}, {part.s}");
    result += SortTheParts(part, start);
}

int SortTheParts(Part part, Workflow workflow)
{
    foreach (var rule in workflow.rules)
    {
        var aap = rule.Split(':');
        if (aap.Length == 1)
        {
            return Zr(aap[0], part);
        }
        else
        {
            var noot = ProcessRule(aap[0], aap[1], part);
            if (noot == "")
            {
                continue;
            }
            return Zr(noot, part);
        }
    }
    return 0;
}

string ProcessRule(string rule, string nextstr, Part part)
{
    
    var chars = rule.ToCharArray();
    var prop = chars[0];
    var op = chars[1];
    var value = int.Parse(chars[2..]);

    switch (prop)
    {
        case 'x':
            if (((part.x < value) && (op == '<')) ||
                ((part.x > value) && (op == '>')))
            {
                return nextstr;
            }
            break;
        case 'm':
            if (((part.m < value) && (op == '<')) ||
                ((part.m > value) && (op == '>')))
            {
                return nextstr;
            }
            break;
        case 'a':
            if (((part.a < value) && (op == '<')) ||
                ((part.a > value) && (op == '>')))
            {
                return nextstr;
            }
            break;
        case 's':
            if (((part.s < value) && (op == '<')) ||
                ((part.s > value) && (op == '>')))
            {
                return nextstr;
            }
            break;
    }

    return "";
}

int Zr(string str, Part part)
{
    if (str == "R")
    {
        return 0;
    }
    else if (str == "A")
    {
        return part.x + part.m + part.a + part.s;
    }
    else
    {
        Console.WriteLine("Next: " + str);
        var next = workflows.First(w => w.name == str);
        return SortTheParts(part, next);
    }
}

Part ParseLine(string line)
{
    var parts = line.Split(',');
    var x = int.Parse(parts[0].Split('=')[1]);
    var m = int.Parse(parts[1].Split('=')[1]);
    var a = int.Parse(parts[2].Split('=')[1]);
    var s = int.Parse(parts[3].Split('=')[1]);
    return new Part(x, m, a, s);
}

//Console.WriteLine($"Start: {start.name}, Rules: {string.Join(", ", start.rules)} ");
Console.WriteLine($"Result: {result}");


record Workflow(string name, List<string> rules);
record Part(int x, int m, int a, int s);