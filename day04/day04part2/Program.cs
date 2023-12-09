var input = File.ReadAllLines("input.txt");

// read all cards
// all cards to process list
// process list
//  - add card(s) to list


var cards = new List<Card>();

foreach (var line in input)
{
    var parts = line.Split(':');
    var Number = int.Parse(parts[0].Split(' ',StringSplitOptions.RemoveEmptyEntries)[1]);
    var winningNumbers = parts[1].Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var myNumbers = parts[1].Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    cards.Add(new Card(Number, winningNumbers, myNumbers));
}

var cardsToProcess = new Queue<Card>(cards);


int cardsToProcessCount = 0;
while (cardsToProcess.Any())
{
    cardsToProcessCount++;
    var card = cardsToProcess.Dequeue();
    int matchingNumbers = 0;
    foreach (var winningNumber in card.winningNumbers)
    {
        if (card.myNumbers.Contains(winningNumber))
        {
            matchingNumbers++;
        }
    }
    if (matchingNumbers > 0)
    {
        for (int i = 0; i < matchingNumbers; i++)
        {
            var wonCard = cards.First(c => c.Number == card.Number + i + 1);
            cardsToProcess.Enqueue(wonCard);
        }  
    }
}

Console.WriteLine(cardsToProcessCount);

record Card(int Number, string[] winningNumbers, string[] myNumbers);