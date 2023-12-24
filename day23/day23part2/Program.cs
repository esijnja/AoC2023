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

bool HaveBeenHereBefore((int x, int y) pos, Step? prev)
{
    if (prev == null)
        return false;
    if (prev.pos.x == pos.x && prev.pos.y == pos.y)
        return true;
    return HaveBeenHereBefore(pos, prev.prev);
}

int GetDistance(Step? prev)
{
    if (prev == null)
        return 0;
    return 1 + GetDistance(prev.prev);
}

void DoTheNextStep((int x, int y) pos, Step current, char sloop, Queue<Step> queue)
{
    var next = map[pos.x, pos.y];
    if ((next != '#' ) && !HaveBeenHereBefore(pos, current))
    {
        queue.Enqueue(new Step(pos, current));
    }
}

var routes = new List<Step>();

var queue = new Queue<Step>();

queue.Enqueue(new Step(start, null));

while (queue.Count > 0)
{
    var current = queue.Dequeue();
    //Console.WriteLine($"x: {current.pos.x}, y: {current.pos.y}");

    if (current.pos.x == end.x && current.pos.y == end.y)
    {
        routes.Add(current);
        continue;
    }


    var nextPos = (current.pos.x + 1, current.pos.y);
    DoTheNextStep(nextPos, current, '>', queue);

    nextPos = (current.pos.x - 1, current.pos.y);
    DoTheNextStep(nextPos, current, '<', queue);


    nextPos = (current.pos.x, current.pos.y + 1);
    DoTheNextStep(nextPos, current, 'v', queue);

    if (current.pos.y > 0)
    {
        nextPos = (current.pos.x, current.pos.y - 1);
        DoTheNextStep(nextPos, current, '^', queue);
    }
}

foreach (var route in routes)
{
    Console.WriteLine($"Route: {GetDistance(route.prev)}");
}

record Step((int x, int y) pos, Step? prev);