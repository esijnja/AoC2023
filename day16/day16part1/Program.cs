var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

var queue = new Queue<(int x, int y, Direction direction)>();

char[,] map = new char[width, height];
char[,] covered = new char[width, height];

for (int y = 0; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = row[x];
    }
}

void DrawMap()
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            Console.Write(map[x, y]);
        }
        Console.WriteLine();
    }
}

void Step(int x, int y, Direction direction)
{
    
    if (x < 0 || x >= width || y < 0 || y >= height)
    {
        Console.WriteLine("Out of bounds");
        return;
    }

    covered[x, y] = '#';
    Console.WriteLine($"Step: {x}, {y}, {direction} {map[x, y]}");
    switch (map[x, y])
    {
        case '>':
                if (direction == Direction.East)
                {
                    return;
                }

                break;
        case '<':
            if (direction == Direction.West)
            {
                return;
            }
            break;
        case '^':
            if (direction == Direction.North)
            {
                return;
            }

            break;
        case 'v':
            if (direction == Direction.South)
            {
                return;
            }

            break;
    }
    
    switch (map[x, y])
    {
        case '/':
            switch (direction)
            {
                case Direction.North:
                    direction = Direction.East;
                    x++;
                    break;
                case Direction.East:
                    y--;
                    direction = Direction.North;
                    break;
                case Direction.South:
                    x--;
                    direction = Direction.West;
                    break;
                case Direction.West:
                    y++;
                    direction = Direction.South;
                    break;
            }
            queue.Enqueue((x, y, direction));
            break;
        case '\\':
            switch (direction)
            {
                case Direction.North:
                    x--;
                    direction = Direction.West;
                    break;
                case Direction.East:
                    y++;
                    direction = Direction.South;
                    break;
                case Direction.South:
                    x++;
                    direction = Direction.East;
                    break;
                case Direction.West:
                    y--;
                    direction = Direction.North;
                    break;
            }
            queue.Enqueue((x, y, direction));
            break;
        case '|':
            switch (direction)
            {
                case Direction.West:
                case Direction.East:
                    queue.Enqueue((x, y - 1, Direction.North));
                    queue.Enqueue((x, y + 1, Direction.South));
                    break;
                case Direction.South:
                    queue.Enqueue((x, y + 1, direction));
                    break;
                case Direction.North:
                    queue.Enqueue((x, y - 1, direction));
                    break;
            }
            break;
        case '-':
            switch (direction)
            {
                case Direction.North:
                case Direction.South:
                    queue.Enqueue((x - 1, y, Direction.West));
                    queue.Enqueue((x + 1, y, Direction.East));
                    break;
                case Direction.East:
                    queue.Enqueue((x + 1, y, direction));
                    break;
                case Direction.West:
                    queue.Enqueue((x - 1, y, direction));
                    break;
            }
            break;
        default:
            switch (direction)
            {
                case Direction.North:
                    map[x, y] = '^';
                    y--;
                    break;
                case Direction.East:
                    map[x, y] = '>';
                    x++;
                    break;
                case Direction.South:
                    map[x, y] = 'v';
                    y++;
                    break;
                case Direction.West:
                    map[x, y] = '<';
                    x--;
                    break;
            }
            queue.Enqueue((x, y, direction));
            break;
    }
}

//map[0, 0] = '>';
//start
queue.Enqueue((0, 0, Direction.East));

while (queue.Any())
{
    var data = queue.Dequeue();
    Step(data.x, data.y, data.direction);
}

DrawMap();
var count = 0;
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (covered[x, y] == '#')
        {
            count++;
        }
    }
}

var result = count;
Console.WriteLine($"Result: {result}");

enum Direction
{
    North,
    East,
    South,
    West
}