var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width,height];
int[,] route = new int[width,height];

for (int y = 0; y< input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x,y] = row[x];
    }  
}


var start = FindStart(map);
(int x, int y) FindStart(char[,] map)
{
    for(int y = 0; y < height; y++)
    {
        for(int x = 0; x < width; x++)
        {
            if(map[x,y] == 'S')
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
route[cursur.x,cursur.y] = 0;
int count = 1;
while(true)
{
    next = FindNext(map, prev, cursur);
    if(next == start)
    {
        break;
    }
    if(next == (-1, -1))
    {
        break;
    }
    route[next.x,next.y] = count++;
    prev = cursur;
    cursur = next;
    
}
//PrintMap(route);
Console.WriteLine("count: " + count);

void PrintMap(int[,] map)
{
    for(int y = 0; y < height; y++)
    {
        for(int x = 0; x < width; x++)
        {
            Console.Write(" " + (map[x,y]==0 ? '.' : map[x,y]));
        }
        Console.WriteLine();
    }
}

(int, int) FindNext (char[,] map, (int x, int y) prev, (int x, int y) cursur)
{
    Console.WriteLine("prev: " + prev + " cursur: " + cursur + " map: " + map[cursur.x,cursur.y]);
    int x = cursur.x;
    int y = cursur.y;
    var current = map[cursur.x,cursur.y];
    switch(current)
    {
        case 'S':
        // start all direction
            if (map[x,y-1] != '.') return (x, y-1);
            if (map[x+1,y] != '.') return (x+1, y);
            if (map[x,y+1] != '.') return (x, y+1);
            if (map[x-1,y] != '.') return (x-1, y);
            return (-1, -1);
            break;
        case 'F':
            if (prev == (x, y+1)) 
            { 
                next = (x+1, y);
                return (map[x+1,y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y+1);
                return (map[x,y+1] != '.') ? next : (-1, -1);
            }
            break;
        case '-':
            if (prev == (x-1, y)) 
            { 
                next = (x+1, y);
                return (map[x+1,y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x-1, y);
                return (map[x-1,y] != '.') ? next : (-1, -1);
            }
            break;
        case '7':
            if (prev == (x, y+1)) 
            { 
                next = (x-1, y);
                return (map[x-1,y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y+1);
                return (map[x,y+1] != '.') ? next : (-1, -1);
            }
            break;
        case 'J':
            if (prev == (x, y-1)) 
            { 
                next = (x-1, y);
                return (map[x-1,y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y-1);
                return (map[x,y-1] != '.') ? next : (-1, -1);
            }
            break;
        case '|':
            if (prev == (x, y+1)) 
            { 
                next = (x, y-1);
                return (map[x,y-1] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y+1);
                return (map[x,y+1] != '.') ? next : (-1, -1);
            }
            break;
        case 'L':
            if (prev == (x, y-1)) 
            { 
                next = (x+1, y);
                return (map[x+1,y] != '.') ? next : (-1, -1);
            }
            else
            {
                next = (x, y-1);
                return (map[x,y-1] != '.') ? next : (-1, -1);
            }
            break;
        default:
            return (-1, -1);
    }

}