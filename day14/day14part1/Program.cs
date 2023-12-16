var input = File.ReadAllLines("input.txt");

int width = input[0].Length;
int height = input.Length;

char[,] map = new char[width, height];

void PrintMap(char[,] map)
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

for (int y = 0; y < input.Length; y++)
{
    var row = input[y].ToCharArray();
    for (int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = row[x];
    }
}

for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        if (map[x, y] == 'O')
        {
            var dy = y - 1;
            while(dy >= 0 && map[x, dy] == '.')
            {
                map[x, dy] = 'O';
                map[x, dy + 1] = '.';
                dy--;
            }
        }
    }
}

PrintMap(map);

var result = 0;

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (map[x, y] == 'O')
        {
            result+= (height - y) ;
        }
    }
}
Console.WriteLine(result);