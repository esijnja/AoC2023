// parse the input file

var input = File.ReadAllLines("input.txt");

var workflows = new List<Workflow>();
var acceptedParts = new List<Part>();
var rejectedParts = new List<Part>();

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

var part = new Part();
var start = workflows.First(w => w.name == "in");


ProcessWorkflow(start, part);

void ProcessWorkflow(Workflow workflow, Part part)
{
    var partNegative = part;

    foreach (var rule in workflow.rules)
    {
        Console.WriteLine($"{workflow.name} Rule: {rule}");
        var ruleParts = rule.Split(':');

        if (ruleParts.Length == 1)
        {
            var next = ruleParts[0];
            if (next == "R")
            {
                rejectedParts.Add(partNegative);
                ;
            }
            else if (next == "A")
            {
                Console.WriteLine($"{workflow.name} {rule} Accepting part: {partNegative.xMin} - {partNegative.xMax} {partNegative.mMin} - {partNegative.mMax} {partNegative.aMin} - {partNegative.aMax} {partNegative.sMin} - {partNegative.sMax}");
                acceptedParts.Add(partNegative);
                
            }
            else
            {
                ProcessWorkflow(workflows.First(w => w.name == next), partNegative);
            }
        }
        else
        {
            var next = ruleParts[1];
            var rulestring = ruleParts[0];
            var chars = rulestring.ToCharArray();
            var prop = chars[0];
            var op = chars[1];
            var value = int.Parse(chars[2..]);
            var partB = partNegative;
            
            switch (prop)
            {
                case 'x':
                    if (op == '<')
                    {
                        if (partNegative.xMin < value)
                        {
                            partNegative.xMin = value;
                        }
                        if (partB.xMax > value - 1)
                        {
                            partB.xMax = value - 1;
                        }
                    }
                    else
                    {
                        if (partNegative.xMax > value)
                        {
                            partNegative.xMax = value;
                        }
                        if (partB.xMin < value+1)
                        {
                            partB.xMin = value+1;
                        }
                    }

                    break;
                case 'm':
                    if (op == '<')
                    {
                        if (partNegative.mMin < value)
                        {
                            partNegative.mMin = value;
                        }
                        if (partB.mMax > value - 1)
                        {
                            partB.mMax = value - 1;
                        }
                    }
                    else
                    {
                        if (partNegative.mMax > value)
                        {
                            partNegative.mMax = value;
                        }
                        if (partB.mMin < value+1)
                        {
                            partB.mMin = value+1;
                        }
                    }

                    break;
                case 'a':
                    if (op == '<')
                    {
                        if (partNegative.aMin < value)
                        {
                            partNegative.aMin = value;
                        }
                        if (partB.aMax > value - 1)
                        {
                            partB.aMax = value - 1;
                        }
                    }
                    else
                    {
                        if (partNegative.aMax > value)
                        {
                            partNegative.aMax = value;
                        }
                        if (partB.aMin < value + 1)
                        {
                            partB.aMin = value + 1;
                        }
                        
                    }

                    break;
                case 's':
                    if (op == '<')
                    {
                        if (partNegative.sMin < value)
                        {
                            partNegative.sMin = value;
                        }
                        if (partB.sMax > value - 1)
                        {
                            partB.sMax = value - 1;
                        }
                    }
                    else
                    {
                        if (partNegative.sMax > value)
                        {
                            partNegative.sMax = value;
                        }
                        if (partB.sMin < value + 1)
                        {
                            partB.sMin = value + 1;
                        }
                    }

                    break;
            }
            if (next == "R")
            {
                rejectedParts.Add(partB);
                
            }
            else if (next == "A")
            {
                Console.WriteLine($"{workflow.name} {rule} Accepting part: {partB.xMin} - {partB.xMax} {partB.mMin} - {partB.mMax} {partB.aMin} - {partB.aMax} {partB.sMin} - {partB.sMax}");
                acceptedParts.Add(partB);
                
            }
            else
            {
                ProcessWorkflow(workflows.First(w => w.name == next), partB);
//                ProcessWorkflow(workflows.First(w => w.name == next), partB);
            }
        }
    }
}

var result = 0L;

foreach (var partx in acceptedParts)
{
    var diffx = partx.xMax - partx.xMin + 1;
    var diffm = partx.mMax - partx.mMin + 1;
    var diffa = partx.aMax - partx.aMin + 1;
    var diffs = partx.sMax - partx.sMin + 1;

    diffx = diffx < 0 ? 0 : diffx;
    diffm = diffm < 0 ? 0 : diffm;
    diffa = diffa < 0 ? 0 : diffa;
    diffs = diffs < 0 ? 0 : diffs;
    
    
    Console.Write($"x: {partx.xMin} - {partx.xMax} {diffx} ");
    Console.Write($"m: {partx.mMin} - {partx.mMax} {diffm} ");
    Console.Write($"a: {partx.aMin} - {partx.aMax} {diffa} ");
    Console.Write($"s: {partx.sMin} - {partx.sMax} {diffs} ");
    var temp = diffx * diffm * diffa *  diffs;
    Console.WriteLine($"Temp: {temp}");
    result += temp;
}

Console.WriteLine($"Result: {result}");

// result = 0f;
// foreach (var partx in rejectedParts)
// {
//     var diffx = partx.xMax - partx.xMin + 1;
//     var diffm = partx.mMax - partx.mMin + 1;
//     var diffa = partx.aMax - partx.aMin + 1;
//     var diffs = partx.sMax - partx.sMin + 1;
//
//     diffx = diffx < 0 ? 0 : diffx;
//     diffm = diffm < 0 ? 0 : diffm;
//     diffa = diffa < 0 ? 0 : diffa;
//     diffs = diffs < 0 ? 0 : diffs;
//     
//     
//     Console.Write($"x: {partx.xMin} - {partx.xMax} {diffx} ");
//     Console.Write($"m: {partx.mMin} - {partx.mMax} {diffm} ");
//     Console.Write($"a: {partx.aMin} - {partx.aMax} {diffa} ");
//     Console.Write($"s: {partx.sMin} - {partx.sMax} {diffs} ");
//     var temp = diffx/4000f * diffm/4000f * diffa/4000f *  diffs/4000f;
//     Console.WriteLine($"Temp: {temp}");
//     result += temp;
// }
// Console.WriteLine($"Result: {result}");
record Workflow(string name, List<string> rules);

struct Part
{
    public Part()
    {
        xMin = 1;
        xMax = 4000;
        mMin = 1;
        mMax = 4000;
        aMin = 1;
        aMax = 4000;
        sMin = 1;
        sMax = 4000;
    }
    public long xMin = 1;
    public long xMax = 4000;
    public long mMin = 1;
    public long mMax = 4000;
    public long aMin = 1;
    public long aMax = 4000;
    public long sMin = 1;
    public long sMax = 4000;
}