var input = File.ReadAllLines("input.txt");


var racetimes = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()[1..^0].Select(x => int.Parse(x)).ToArray();
var raceRecords = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()[1..^0].Select(x => int.Parse(x)).ToArray();


(int, int) Calc(int racetime, int raceRecord)
{
    int disciminator = (racetime * racetime) - (4 * raceRecord);
    
    var srDisciminator = Math.Sqrt(disciminator);

    var topf = ((racetime + srDisciminator + 1) / 2);
    var bottomf = ((racetime - srDisciminator) / 2);

    Console.WriteLine($"{bottomf} {topf}");

    var top = (int)(((racetime + srDisciminator) / 2));
    var bottom = (int)(((racetime - srDisciminator + 2) / 2));
    Console.WriteLine($"{bottom} {top}");
    return (bottom, top);
}



int value = 1;
for (int i = 0; i < racetimes.Length; i++)
{
   // Console.WriteLine($"{racetimes[i]} {raceRecords[i]}");
    (int bottom, int top) = Calc(racetimes[i], raceRecords[i]);
    Console.WriteLine($"{bottom} {top} {top - bottom + 1}");
    value *= (top - bottom + 1);
}
Console.WriteLine(value);
