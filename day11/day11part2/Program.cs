var input = File.ReadAllLines("input.txt");

int expand = 999_999;

int count = 0;
var galaxies = new List<Galaxy>();
foreach (var line in input)
{
    var galxs = line.AllIndexesOf("#");

    foreach (var galx in galxs)
    {
        galaxies.Add(new (galx, count));
    }

    count++;
}
Console.WriteLine(galaxies.Count);

int maxX = galaxies.Max(g => g.x);
for (int x = 0; x < maxX; x++)
{
    if (galaxies.Where(g => g.x == x).Count() == 0)
    {
        foreach( var gal in galaxies.Where(g => g.x > x))
        {
            gal.exx += expand; 
            Console.WriteLine($"{gal.x},{gal.y}[{gal.exx},{gal.exy}]");
        }
    }
}

int maxY = galaxies.Max(g => g.y);
for (int y = 0; y < maxY; y++)
{
    if (galaxies.Where(g => g.y == y).Count() == 0)
    {
        //galaxies.Where(g => g.y > y).ToList().ForEach(g => g.exy += expand);
        foreach( var gal in galaxies.Where(g => g.y > y))
        {
            gal.exy += expand;
            Console.WriteLine($"{gal.x},{gal.y}[{gal.exx},{gal.exy}]");
        }
    }
}


long totaldistance = 0;
for (int i = 0; i < galaxies.Count; i++)
{
    for (int j = 0; j < galaxies.Count; j++)
    {
        if (i == j)
        {
            continue;
        }
        var distance = CalcDistance(galaxies[i], galaxies[j]);
        totaldistance += distance;
    }
}

Console.WriteLine(totaldistance/2);

long CalcDistance(Galaxy a, Galaxy b)
{
    Console.WriteLine($"{a.x},{a.y}[{a.exx},{a.exy}] - {b.x},{b.y}[{b.exx},{b.exy}] = {Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y)}");
    return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
}



record Galaxy(int x, int y)
{
    public int exx = 0;
    public int exy = 0;
    public int X => x + exx;
    public int Y => y + exy;
}

static class Extentions
{
    public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
    {
        int minIndex = str.IndexOf(searchstring);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
        }
    }
}