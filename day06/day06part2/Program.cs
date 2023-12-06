var input = File.ReadAllLines("input.txt");

var aap = input[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", "");
var racetime = long.Parse(aap);
var noot = input[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", "");
var raceRecord = long.Parse(noot);
//var racetimes = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()[1..^0].Select(x => int.Parse(x)).ToArray();
//var raceRecords = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()[1..^0].Select(x => int.Parse(x)).ToArray();
Console.WriteLine(aap);

(long, long) Calc(long racetime, long raceRecord)
{
    long disciminator = (racetime * racetime) - (4 * raceRecord);
    
    var srDisciminator = Math.Sqrt(disciminator);

    var topf = ((racetime + srDisciminator + 1) / 2);
    var bottomf = ((racetime - srDisciminator) / 2);

    Console.WriteLine($"{bottomf} {topf}");

    var top = (long)(((racetime + srDisciminator) / 2));
    var bottom = (long)(((racetime - srDisciminator + 2) / 2));
    Console.WriteLine($"{bottom} {top}");
    return (bottom, top);
}



long value = 1;
// for (int i = 0; i < racetimes.Length; i++)
// {
//    // Console.WriteLine($"{racetimes[i]} {raceRecords[i]}");
//     (int bottom, int top) = Calc(racetimes[i], raceRecords[i]);
//     Console.WriteLine($"{bottom} {top} {top - bottom + 1}");
//     value *= (top - bottom + 1);
// }
(long bottom, long top) = Calc(racetime, raceRecord);
value = value *= (top - bottom + 1);
Console.WriteLine(value);
