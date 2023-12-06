
const int maxRed = 12;
const int maxGreen = 13;
const int maxBlue = 14;

var input = File.ReadAllLines("input.txt");

int ParseGamenumber (string gameNumber)
{
    var parts = gameNumber.Split(' ');
    return int.Parse(parts[1]);
}

(int , int , int ) ParseColorSet(string colorSet)
{
    int red = 0, green = 0, blue = 0;
    var parts = colorSet.Split(',');
    foreach (var part in parts)
    {
        Console.WriteLine(part);
        var color = part.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine($"'{color[0]}' '{color[1]}'");
        if (color[1].Contains( "red")) red = int.Parse(color[0]);
        if (color[1].Contains("green")) green = int.Parse(color[0]);
        if (color[1].Contains("blue")) blue = int.Parse(color[0]);
    }
    
    return (red, green, blue);
}

(int, int, int, int) ReadMaxColor(string line)
{
    int cRed=0, cGreen=0, cBlue=0;
    var parts = line.Split(':');
    var gameNumber = ParseGamenumber(parts[0]);
    var colorSets = parts[1].Split(';');
    foreach (var colorSet in colorSets)
    {
        (int red, int green, int blue) = ParseColorSet(colorSet);
        if (red > cRed) cRed = red;
        if (green > cGreen) cGreen = green;
        if (blue > cBlue) cBlue = blue;
    }
    return (gameNumber, cRed, cGreen, cBlue);
}

var value = 0;
foreach (var line in input)
{ 
    (int gameNumber ,int red, int green, int blue) = ReadMaxColor(line);
    
    int linevalue = red * green * blue;
    value += linevalue;
    Console.WriteLine($"{line} {red} {green} {blue} {gameNumber} {linevalue}");
    
}

Console.WriteLine(value);