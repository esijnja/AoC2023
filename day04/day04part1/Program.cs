var input = File.ReadAllLines("input.txt");

long winnings = 0;

foreach (var line in input)
{
    var parts = line.Split(':');

    var winningNumbers = parts[1].Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var myNumbers = parts[1].Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    int matchingNumbers = 0;
    foreach (var winningNumber in winningNumbers)
    {
        if (myNumbers.Contains(winningNumber))
        {
            matchingNumbers++;
        }
    }
    if (matchingNumbers > 0)
    {
        int cardWinning = 1;
        // for (int i = 0; i < matchingNumbers; i++)
        // {
        //     cardWinning *= 2;
        // }
        cardWinning = 1 << matchingNumbers-1;
        winnings += cardWinning;
    }
}
Console.WriteLine(winnings);
