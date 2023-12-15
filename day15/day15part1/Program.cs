var input = File.ReadAllLines("input.txt");

var keys = input[0].Split(',').ToList();
var total = 0;
foreach (var key in keys)
{
    var current = 0;
    var chars = key.ToCharArray();
    foreach (var c in chars)
    {
        current += (int)c;
        current *= 17;
        current %= 256; 
    }
    Console.WriteLine(current);
    total += current;
}
Console.WriteLine(total);
