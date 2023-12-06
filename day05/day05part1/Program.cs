var input = File.ReadAllLines("input.txt");



// seeds:
var seeds = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()[1..^0].Select(x => long.Parse(x)).ToArray();

var Seed2Soil = new List<Mapper>();
var Soil2Fertilizer = new List<Mapper>();
var Fertilizer2Water = new List<Mapper>();
var Water2Light = new List<Mapper>();
var Light2Temperature = new List<Mapper>();
var Temperature2Humidity = new List<Mapper>();
var Humidity2Location = new List<Mapper>();

Mapper ParseMapper(string line)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    return new Mapper(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
}

int ParseBlock(string[] block, int index, List<Mapper> map)
{
    Console.WriteLine($"Parsing block {index}: {block[index]}");
    while (block[++index].Length > 0)
    {
        Console.WriteLine($"Parsing block {index}: {block[index]}");
        map.Add(ParseMapper(block[index]));
    }
    return index;
}

long Map(long value, List<Mapper> map)
{
    foreach (var mapper in map)
    {
        if (value >= mapper.source && value <= mapper.source + mapper.range)
        {
            return mapper.destination + (value - mapper.source);
        }
    }
    return value;
}


int a = ParseBlock(input, 2 , Seed2Soil);
int b = ParseBlock(input, a+1, Soil2Fertilizer);
int c = ParseBlock(input, b+1, Fertilizer2Water);
int d = ParseBlock(input, c+1, Water2Light);
int e = ParseBlock(input, d+1, Light2Temperature);
int f = ParseBlock(input, e+1, Temperature2Humidity);
int g = ParseBlock(input, f+1, Humidity2Location);

long shortist = long.MaxValue;
foreach (var seed in seeds)
{
    var result = Map(Map(Map(Map(Map(Map(Map(seed,Seed2Soil),Soil2Fertilizer),Fertilizer2Water),Water2Light),Light2Temperature),Temperature2Humidity),Humidity2Location);
    if (result < shortist) shortist = result;
}

Console.WriteLine(shortist);
record Mapper(long destination, long source, long range);