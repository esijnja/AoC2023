var input = File.ReadAllLines("input.txt");
int maxWidth = 0;
int maxHeight = 0;
int minWidth = 0;
int minHeight = 0;
int dwidth = 0;
int dheight = 0;
var lines = new List<Line>();
foreach (var line in input)
{
    var parts = line.Split(' ');

    var cline = new Line(parts[0][0], int.Parse(parts[1].ToString()), parts[2].Substring(1, parts[2].Length - 2));
    lines.Add(cline);
    switch (cline.direction)
    {
        case 'R':
            dwidth += cline.steps;
            if (dwidth > maxWidth)
                maxWidth = dwidth;
            break;
        case 'L':
            dwidth -= cline.steps;
            if (dwidth < minWidth)
                minWidth = dwidth;
            break;
        case 'D':
            dheight += cline.steps;
            if (dheight > maxHeight)
                maxHeight = dheight;
            break;
        case 'U':
            dheight -= cline.steps;
            if (dheight < minHeight)
                minHeight = dheight;
            break;
    }
}

Console.WriteLine($"Width: {maxWidth}, Height: {maxHeight}");
Console.WriteLine($"MinWidth: {minWidth}, MinHeight: {minHeight}");

var map = new char[-minWidth + maxWidth + 2, -minHeight + maxHeight + 2];
int x = -minWidth;
int y = -minHeight;
foreach (var line in lines)
{
    Console.WriteLine($"x: {x}, y: {y} - {line.direction} {line.steps} {line.color}");
    for (int i = 0; i < line.steps; i++)
    {
        switch (line.direction)
        {
            case 'R':
                map[++x, y] = '#';
                break;
            case 'L':
                map[--x, y] = '#';
                break;
            case 'U':
                map[x, --y] = '#';
                break;
            case 'D':
                map[x, ++y] = '#';
                break;
        }
    }
}



var queue = new Queue<(int x, int y)>();
queue.Enqueue((-minWidth + 1, -minHeight + 1));

while (queue.Count > 0)
{
    var (qx, qy) = queue.Dequeue();
    
    if (map[qx, qy] == '#')
        continue;
    map[qx, qy] = '#';
    queue.Enqueue((qx + 1, qy));
    queue.Enqueue((qx - 1, qy));
    queue.Enqueue((qx, qy + 1));
    queue.Enqueue((qx, qy - 1));
}
int result = 0;
for (int i = 0; i <= -minHeight + maxHeight; i++)
{
    for (int j = 0; j <= -minWidth + maxWidth; j++)
    {
        if (map[j, i] == '#')
        {
            Console.Write(map[j, i]);
            result++;
        }
        else
            Console.Write('.');
    }
    Console.WriteLine();
}

Console.WriteLine($"Result: {result}");
record Line(char direction, int steps, string color);