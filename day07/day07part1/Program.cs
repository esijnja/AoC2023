var input = File.ReadAllLines("input.txt");

var kvps = new Dictionary<char, int>()
{
    { '2', 2 },
    { '3', 3 },
    { '4', 4 },
    { '5', 5 },
    { '6', 6 },
    { '7', 7 },
    { '8', 8 },
    { '9', 9 },
    { 'T', 10 },
    { 'J', 11 },
    { 'Q', 12 },
    { 'K', 13 },
    { 'A', 14 },
};
// {
//     new KeyValuePair<string, int>("2", 2),
//     new KeyValuePair<string, int>("3", 3),
//     new KeyValuePair<string, int>("4", 4),
//     new KeyValuePair<string, int>("5", 5),
//     new KeyValuePair<string, int>("6", 6),
//     new KeyValuePair<string, int>("7", 7),
//     new KeyValuePair<string, int>("8", 8),
//     new KeyValuePair<string, int>("9", 9),
//     new KeyValuePair<string, int>("T", 10),
//     new KeyValuePair<string, int>("J", 11),
//     new KeyValuePair<string, int>("Q", 12),
//     new KeyValuePair<string, int>("K", 13),
//     new KeyValuePair<string, int>("A", 14),
// };

HandOrder DetermineHandOrder(string hand)
{
    var cards = hand.ToArray();
    
    Console.WriteLine($"cards: {string.Join(",", cards)}");

    var cardOrderGrouped = cards.GroupBy(c => c).ToList();

    Console.WriteLine($"cardOrderGrouped: {string.Join(",", cardOrderGrouped.Select(c => $"{c.Key} {c.Count()}"))}");


    if (cardOrderGrouped.Count == 5)
    {
        return HandOrder.HighCard;
    }
    else if (cardOrderGrouped.Count == 4)
    {
        return HandOrder.OnePair;
    }
    else if (cardOrderGrouped.Count == 3)
    {
        if (cardOrderGrouped.Any(c => c.Count() == 3))
        {
            return HandOrder.ThreeOfAKind;
        }
        else
        {
            return HandOrder.TwoPairs;
        }
    }
    else if (cardOrderGrouped.Count == 2)
    {
        if (cardOrderGrouped.Any(c => c.Count() == 4))
        {
            return HandOrder.FourOfAKind;
        }
        else
        {
            return HandOrder.FullHouse;
        }
    }
    else if (cardOrderGrouped.Count == 1)
    {
        return HandOrder.FiveOfAKind;
    }
    else
    {
        throw new Exception("Unknown hand");
    }
}

var Hands = new List<HandBid>();
// Sort on hand
//   1. Sort by characters
foreach (var line in input)
{
    var aap = line.Split(" ");
    var hand = aap[0];
    var bid = aap[1];

    HandOrder handOrder = DetermineHandOrder(hand);
    Console.WriteLine($"{handOrder} {hand} {bid} ");

    Hands.Add(new HandBid(handOrder, hand, int.Parse(bid)));
}

var RevertOrderedHands = Hands.OrderBy(h => h.HandOrder).ThenBy(h => h.Hand, new HandComparer(kvps)).ToList();

long result = 0;
for (int i = 0; i < RevertOrderedHands.Count; i++)
{
    Console.WriteLine($"{i+1} {RevertOrderedHands[i].HandOrder} {RevertOrderedHands[i].Hand} {RevertOrderedHands[i].Bid}");
    result += ((i+1) * RevertOrderedHands[i].Bid);
}

Console.WriteLine($"Result: {result}");

enum HandOrder { HighCard, OnePair, TwoPairs, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind };
record HandBid(HandOrder HandOrder, string Hand, int Bid);



public class HandComparer : IComparer<string>
{
    private readonly Dictionary<char, int> kvps;
    public HandComparer(Dictionary<char, int> kvps)
    {
        this.kvps = kvps;
    }

    public int Compare(string? x, string? y)
    {
        var xs = x.ToArray();
        var ys = y.ToArray();
        if (CardCompare(xs[0], ys[0]) == 0)
        {
            if (CardCompare(xs[1], ys[1]) == 0)
            {
                if (CardCompare(xs[2], ys[2]) == 0)
                {
                    if (CardCompare(xs[3], ys[3]) == 0)
                    {
                        return CardCompare(xs[4], ys[4]);
                    }
                    else
                    {
                        return CardCompare(xs[3], ys[3]);
                    }
                }
                else
                {
                    return CardCompare(xs[2], ys[2]);
                }
            }
            else
            {
                return CardCompare(xs[1], ys[1]);
            }
        }
        else
        {
            return CardCompare(xs[0], ys[0]);
        }
    }

    int CardCompare(char x, char y)
    {
        kvps.TryGetValue(x, out int xv);
        kvps.TryGetValue(y, out int yv);

        return xv - yv;
    }
}

