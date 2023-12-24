var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];

for (int y = 0; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = row[x];
    }
}

var start = (x: 1, y: 0);
var end = (x: width - 2, y: height - 1);


// find all junctions

var junctions = new List<(int x, int y)>();

junctions.Add(start);

for (int y = 1; y < height-1; y++)
{
    for (int x = 1; x < width-1; x++)
    {
        if (map[x, y] == '#')
            continue;
        var count = 0;
        if (map[x  , y-1] == '.') count++;
        if (map[x-1, y  ] == '.') count++;
        if (map[x+1, y  ] == '.') count++;
        if (map[x  , y+1] == '.') count++;
        if (count > 2)
        {
            junctions.Add((x, y));
        }
    }
}

junctions.Add(end);
var aap = 0;
foreach (var junction in junctions)
{
    aap++;
    Console.WriteLine($"Junction {aap}: {junction.x}, {junction.y}");
}

var vertex = new List<Vertex>();

foreach (var junction in junctions)
{
    //Console.WriteLine($"Junction: {junction.x}, {junction.y}");
    if (map[junction.x + 1, junction.y] == '.')
    {
        // find next junction
        ((int x, int y) pos, int steps) next = FindNextJunction(junction, (junction.x + 1, junction.y));
        vertex.Add(new Vertex(junction, next.pos, next.steps));
    }

    if (map[junction.x - 1, junction.y] == '.')
    {
        // find next junction
        ((int x, int y) pos, int steps)  next = FindNextJunction(junction, (junction.x - 1, junction.y));
        vertex.Add(new Vertex(junction, next.pos, next.steps));
    }

    if (junction.y > 0 && map[junction.x, junction.y-1] == '.')
    {
        // find next junction
        ((int x, int y) pos, int steps)  next = FindNextJunction(junction, (junction.x, junction.y-1));
        vertex.Add(new Vertex(junction, next.pos, next.steps));
    }

    if (junction.y+1 < height && map[junction.x, junction.y+1] == '.')
    {
        // find next junction
        ((int x, int y) pos, int steps)  next = FindNextJunction(junction, (junction.x, junction.y+1));
        vertex.Add(new Vertex(junction, next.pos, next.steps));
    }
}

((int x, int y) pos, int steps) FindNextJunction((int x, int y) src, (int x, int y) pos)
{
    var steps = 1;
    var visted = new List<(int x, int y)>();
    visted.Add(src);
    while (!junctions.Contains(pos))
    {
        visted.Add(pos);
        steps++;
        //Console.WriteLine($"Steps: {steps} - {pos.x}, {pos.y}");

        if (map[pos.x + 1, pos.y] == '.' && !visted.Contains((pos.x + 1, pos.y)))
        {
            pos = (pos.x + 1, pos.y);
            continue;
        }
        
        if (map[pos.x - 1, pos.y] == '.' && !visted.Contains((pos.x - 1, pos.y)))
        {
            pos = (pos.x - 1, pos.y);
            continue;
        }

        if (map[pos.x, pos.y - 1] == '.' && !visted.Contains((pos.x, pos.y-1)))
        {
            pos = (pos.x, pos.y - 1);
            continue;
        }

        if (map[pos.x, pos.y + 1] == '.' && !visted.Contains((pos.x, pos.y+1)))
        {
            pos = (pos.x, pos.y + 1);
            continue;
        }
        break;
    }

    return (pos, steps);
}

var vcount=0;
foreach (var v in vertex)
{   vcount++;
    Console.WriteLine($"Vertex {vcount}: {v.src.x}, {v.src.y} -> {v.des.x}, {v.des.y} ({v.steps})");
}

var routes = new List<QQ>();

var startVertex = vertex.Where(v => v.src.x == start.x && v.src.y == start.y).First();

var qeueu = new Queue<QQ>();
var noot = new List<(int x, int y)>();
noot.Add(start);
qeueu.Enqueue(new QQ(startVertex, new List<Vertex>(), noot));
while (qeueu.Count > 0)
{
    var current = qeueu.Dequeue();
    //Console.WriteLine($"Current: {current.v.src.x}, {current.v.src.y} -> {current.v.des.x}, {current.v.des.y} ({current.v.steps})");

    if (current.v.src.x == end.x && current.v.src.y == end.y)
    {
        routes.Add(current);
        Console.WriteLine($"Found: {current.v.src.x}, {current.v.src.y} -> {current.v.des.x}, {current.v.des.y} ({current.v.steps})");
        continue;
    }

    

    var nextVertex = vertex.Where(v => v.src == current.v.des && !current.nodes.Contains(v.src) ).ToList();
    foreach (var next in nextVertex)
    {
        var list = new List<Vertex>();
        list.AddRange(current.vi);
        list.Add(current.v);
        var list2 = new List<(int x, int y)>();
        list2.AddRange(current.nodes);
        list2.Add(current.v.src);
        qeueu.Enqueue(new QQ(next, list, list2));
    }

}

foreach (var route in routes)
{
    Console.WriteLine($"Route: {route.vi.Sum(v => v.steps)}");
}


record Vertex((int x, int y) src, (int x, int y) des, int steps);
record QQ(Vertex v, List<Vertex> vi, List<(int x, int y)> nodes);

// bool HaveBeenHereBefore((int x, int y) pos, Step? prev)
// {
//     return prev?.steps.Any(s => s.x == pos.x && s.y == pos.y) ?? false;
//     // if (prev == null)
//     //     return false;
//     // if (prev.pos.x == pos.x && prev.pos.y == pos.y)
//     //     return true;
//     // return HaveBeenHereBefore(pos, prev.prev);
// }

// int GetDistance(Step? prev)
// {
//     return prev.steps.Count;
//     // if (prev == null)
//     //     return 0;
//     // return 1 + GetDistance(prev.prev);
// }

// void DoTheNextStep((int x, int y) pos, Step current, char sloop, Queue<Step> queue)
// {
//     var next = map[pos.x, pos.y];
//     if ((next != '#' ) && !HaveBeenHereBefore(pos, current))
//     {   var list = new List<(int x, int y)>();
//         list.AddRange(current.steps);
//         list.Add((pos.x, pos.y));
//         queue.Enqueue(new Step(pos, list));
//     }
// }

// var routes = new List<Step>();

// var queue = new Queue<Step>();

// queue.Enqueue(new Step(start, new List<(int x, int y)>()));

// while (queue.Count > 0)
// {
//     var current = queue.Dequeue();
//     //Console.WriteLine($"x: {current.pos.x}, y: {current.pos.y}");

//     if (current.pos.x == end.x && current.pos.y == end.y)
//     {
//         routes.Add(current);
//         continue;
//     }


//     var nextPos = (current.pos.x + 1, current.pos.y);
//     DoTheNextStep(nextPos, current, '>', queue);

//     nextPos = (current.pos.x - 1, current.pos.y);
//     DoTheNextStep(nextPos, current, '<', queue);


//     nextPos = (current.pos.x, current.pos.y + 1);
//     DoTheNextStep(nextPos, current, 'v', queue);

//     if (current.pos.y > 0)
//     {
//         nextPos = (current.pos.x, current.pos.y - 1);
//         DoTheNextStep(nextPos, current, '^', queue);
//     }
// }

// foreach (var route in routes)
// {
//     Console.WriteLine($"Route: {GetDistance(route)}");
// }

// record Step((int x, int y) pos, List<(int x, int y)> steps);