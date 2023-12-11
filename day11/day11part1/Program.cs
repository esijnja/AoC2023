var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

int total = 0;
int yExpantion = 0;
//char[,] map0 = new char[input[0].Length, input.Length];
foreach (var line in input)
{
    var count = line.Count(c => c == '#');

    //Console.WriteLine(count);
    if (count == 0)
    {
        yExpantion++;
    }
    total += count;

}
height += yExpantion;
var lineNumber = 0;
char[,] map1 = new char[width, height];
foreach (var line in input)
{
    Console.WriteLine($"Line number: {lineNumber}");
    var count = line.Count(c => c == '#');

    char[] chars = line.ToCharArray();

    for (int i = 0; i < chars.Length; i++)
    {
        map1[i, lineNumber] = chars[i];
    }
    lineNumber++;
    //Console.WriteLine(count);
    if (count == 0)
    {
        yExpantion++;
        for (int i = 0; i < chars.Length; i++)
        {
            map1[i, lineNumber] = chars[i];
        }
        lineNumber++;
    }
    total += count;

}
Console.WriteLine("next");
var xExpantion = 0;
for (int x = 0; x < width; x++)
{

    var count = 0;
    for (int y = 0; y < height; y++)
    {
        try
        {
            if (map1[x, y] == '#')
            {
                count++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{x}, {y}");
        }
    }
    Console.WriteLine(count);
    if (count == 0)
    {
        xExpantion++;
    }

}
Console.WriteLine($"{width} {xExpantion}");
var colomnumber = 0;
var newWidth = width + xExpantion;
var map2 = new char[newWidth, height];
for (int x = 0; x < width; x++)
{
    var count = 0;
    for (int y = 0; y < height; y++)
    {
        if (map1[x, y] == '#')
        {
            count++;
        }
        map2[colomnumber, y] = map1[x, y];
    }
    colomnumber++;
    // Console.WriteLine(count);
    if (count == 0)
    {
        for (int y = 0; y < height; y++)
        {
            map2[colomnumber, y] = map1[x, y];
        }
        colomnumber++;
    }
}
Console.WriteLine("next");
var galaxy = new List<(int x, int y)>();
for (int y = 0; y < height; y++)
{
    var count = 0;
    for (int x = 0; x < newWidth; x++)
    {
        if (map2[x, y] == '#')
        {
            galaxy.Add((x,y));
        }
    }
}

long totaldistance = 0;
for (int i = 0; i < galaxy.Count; i++)
{
    for (int j = 0; j < galaxy.Count; j++)
    {
        if (i == j)
        {
            continue;
        }
        var distance = CalcDistance(galaxy[i], galaxy[j]);
        totaldistance += distance;
    }
}

Console.WriteLine(totaldistance/2);

long CalcDistance((int x, int y) a, (int x, int y) b)
{
    return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
}