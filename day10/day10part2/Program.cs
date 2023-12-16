var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];
int[,] route = new int[width * 3, height * 3];

for (int y = 0; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = row[x];
    }
}


var start = FindStart(map);
(int x, int y) FindStart(char[,] map)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (map[x, y] == 'S')
            {
                return (x, y);
            }
        }
    }
    return (-1, -1);
}

var cursur = start;
var next = start;
var prev = start;
SetRoute(route, next, map[next.x, next.y]);
route[cursur.x, cursur.y] = 0;
int count = 0;
while (true)
{
    next = FindNext(map, prev, cursur);
    if (next == start)
    {
        break;
    }
    if (next == (-1, -1))
    {
        break;
    }
    SetRoute(route, next, map[next.x, next.y]);
    //Console.WriteLine("next: " + next + " map: " + map[next.x,next.y]);
    //PrintMap(route);
    route[next.x, next.y] = 1;
    prev = cursur;
    cursur = next;
}

PrintMap(route);

// for (int y = 0; y< height; y++)
// {
//     Flood(route, 0, y*3);
// }

// for (int x = 0; x < width; x++)
// {
//     Flood(route, x*3, 0);    
// }  
// for (int y = 0; y< height; y++)
// {
//     Flood(route, width-1, y*3);
// }

// for (int x = 0; x < width; x++)
// {
//     Flood(route, x, height-1);
// }  

//Flood(/*route,*/ 0, 0);
//route[0, 0] = -1;
var queue = new Queue<(int x, int y)>();
for (int y = 0; y < height * 3; y++)
{
    queue.Enqueue((0, y));
    queue.Enqueue((width * 3 - 1, y));
}

for (int x = 0; x < width * 3; x++)
{
    queue.Enqueue((x, 0));
    queue.Enqueue((x, height * 3 - 1));
}

queue.Enqueue((0, 0));
queue.Enqueue((0, 90));
while (queue.Any())
{
    var data = queue.Dequeue();
    //Console.WriteLine("x: " + data.x + " y: " + data.y);
    Flood(data.x, data.y);
}


//PrintMap(route);
PrintMapFull(route);

for (int y = 0; y < height * 3; y = y + 3)
{
    for (int x = 0; x < width * 3; x = x + 3)
    {
        if (route[x + 1, y + 1] == 0)
        {
            count++;
        }
    }
}

Console.WriteLine("count: " + count);


void SetRoute(int[,] route, (int x, int y) next, char c)
{
    var x = next.x * 3;
    var y = next.y * 3;
    switch (c)
    {
        case 'S':
            //     0 1 2
            //  0  0 1 0
            //  1  1 1 1 
            //  2  0 1 0
            route[x + 1, y] = 1;
            route[x, y + 1] = 1;
            route[x + 1, y + 1] = 1;
            route[x + 2, y + 1] = 1;
            route[x + 1, y + 2] = 1;
            break;
        case 'F':
            //    0 1 2
            // 0  0 0 0
            // 1  0 1 1 
            // 2  0 1 0
            route[x + 1, y + 1] = 1;
            route[x + 2, y + 1] = 1;
            route[x + 1, y + 2] = 1;
            break;
        case '-':
            //    0 1 2
            // 0  0 0 0
            // 1  1 1 1 
            // 2  0 0 0
            route[x, y + 1] = 1;
            route[x + 1, y + 1] = 1;
            route[x + 2, y + 1] = 1;
            break;
        case '7':
            //     0 1 2
            //  0  0 0 0
            //  1  1 1 0 
            //  2  0 1 0
            route[x, y + 1] = 1;
            route[x + 1, y + 1] = 1;
            route[x + 1, y + 2] = 1;
            break;
        case 'J':
            //    -1 0 1
            // -1  0 1 0
            //  0  1 1 0 
            //  1  0 0 0
            route[x + 1, y] = 1;
            route[x, y + 1] = 1;
            route[x + 1, y + 1] = 1;
            break;
        case '|':
            //    -1 0 1
            // -1  0 1 0
            //  0  0 1 0 
            //  1  0 1 0
            route[x + 1, y] = 1;
            route[x + 1, y + 1] = 1;
            route[x + 1, y + 2] = 1;
            break;
        case 'L':
            //    -1 0 1
            // -1  0 1 0
            //  0  0 1 1 
            //  1  0 0 0
            route[x + 1, y] = 1;
            route[x + 1, y + 1] = 1;
            route[x + 2, y + 1] = 1;
            break;
        default:
            break;
    }
}



void Flood2()
{
    for (int y = 0; y < height * 3; y++)
    {
        for (int x = 0; x < width * 3; x++)
        {
            if (route[x, y] == 0)
            {
                if ((x > 0 && route[x - 1, y] == -1) ||
                    (x < (width * 3) - 1 && route[x + 1, y] == -1) ||
                    (y > 0 && route[x, y - 1] == -1) ||
                    (y < (height * 3) - 1 && route[x, y + 1] == -1))
                {
                    route[x, y] = -1;
                }
            }
            if (x % 300 == 1 && y % 300 == 1)
            {
                Console.WriteLine("x: " + x + " y: " + y);
                PrintMap(route);
            }
        }
    }
}

void Flood( /*int[,] map,*/ int x, int y)
{

    if (x < 0 || x >= width * 3 || y < 0 || y >= height * 3)
    {
        return;
    }

    if (route[x, y] != 0)
    {
        return;
    }


    route[x, y] = -1;
    // if (x % 3 == 1 && y % 3 == 1)
    // {
    //     Console.WriteLine("x: " + x + " y: " + y);
    //     PrintMap(route);
    // }

    // Flood(/*map,*/ x + 1, y);
    // Flood(/*map,*/ x - 1, y);
    // Flood(/*map,*/ x, y + 1);
    // Flood(/*map,*/ x, y - 1);
    queue.Enqueue((x + 1, y));
    queue.Enqueue((x - 1, y));
    queue.Enqueue((x, y + 1));
    queue.Enqueue((x, y - 1));
}

void PrintMap(int[,] map)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            Console.Write(map[x * 3 + 1, y * 3 + 1] == 0 ? '.' : map[x * 3 + 1, y * 3 + 1] == 1 ? '+' : ' ');
        }
        Console.WriteLine();
    }
}
void PrintMapFull(int[,] map)
{
    for (int y = 0; y < height * 3; y++)
    {
        for (int x = 0; x < width*3 ; x++)
        {
            Console.Write(map[x, y] == 0 ? '.' : map[x, y] == 1 ? '+' : ' ');
        }
        Console.WriteLine();
    }
}

(int, int) FindNext(char[,] map, (int x, int y) prev, (int x, int y) cursur)
{
    //Console.WriteLine("prev: " + prev + " cursur: " + cursur + " map: " + map[cursur.x,cursur.y]);
    int x = cursur.x;
    int y = cursur.y;
    var current = map[cursur.x, cursur.y];
    switch (current)
    {
        case 'S':
            // start all direction
            if (map[x, y - 1] != '.') return (x, y - 1);
            if (map[x + 1, y] != '.') return (x + 1, y);
            if (map[x, y + 1] != '.') return (x, y + 1);
            if (map[x - 1, y] != '.') return (x - 1, y);
            return (-1, -1);
            break;
        case 'F':
            if (prev == (x, y + 1))
            {
                next = (x + 1, y);
                return (map[x + 1, y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y + 1);
                return (map[x, y + 1] != '.') ? next : (-1, -1);
            }
            break;
        case '-':
            if (prev == (x - 1, y))
            {
                next = (x + 1, y);
                return (map[x + 1, y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x - 1, y);
                return (map[x - 1, y] != '.') ? next : (-1, -1);
            }
            break;
        case '7':
            if (prev == (x, y + 1))
            {
                next = (x - 1, y);
                return (map[x - 1, y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y + 1);
                return (map[x, y + 1] != '.') ? next : (-1, -1);
            }
            break;
        case 'J':
            if (prev == (x, y - 1))
            {
                next = (x - 1, y);
                return (map[x - 1, y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y - 1);
                return (map[x, y - 1] != '.') ? next : (-1, -1);
            }
            break;
        case '|':
            if (prev == (x, y + 1))
            {
                next = (x, y - 1);
                return (map[x, y - 1] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y + 1);
                return (map[x, y + 1] != '.') ? next : (-1, -1);
            }
            break;
        case 'L':
            if (prev == (x, y - 1))
            {
                next = (x + 1, y);
                return (map[x + 1, y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y - 1);
                return (map[x, y - 1] != '.') ? next : (-1, -1);
            }
            break;
        default:
            return (-1, -1);
    }

}