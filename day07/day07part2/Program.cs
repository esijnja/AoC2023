var input = File.ReadAllLines("input.txt");

var kvps = new Dictionary<char, int>()
{
    { 'J', 1 },
    { '2', 2 },
    { '3', 3 },
    { '4', 4 },
    { '5', 5 },
    { '6', 6 },
    { '7', 7 },
    { '8', 8 },
    { '9', 9 },
    { 'T', 10 },
    { 'Q', 12 },
    { 'K', 13 },
    { 'A', 14 },
};

string ReplaceJ(string hand)
{

    var cards = hand.ToArray();
    var cardOrderGrouped = cards.GroupBy(c => c).OrderByDescending(c => c.Count()).ToList();
    
    int count = 0;
    foreach (var cardGroup in cardOrderGrouped)
    {
        if (cardGroup.Key == 'J')
        {
            count = cardGroup.Count();
        }
    }
    
    if (count == 0 || count == 5)
    {
        return hand;
    }
    char key = 'J';
    if (cardOrderGrouped[0].Key == 'J')
    {
        key = cardOrderGrouped[1].Key;
    }
    else
    {
        key = cardOrderGrouped[0].Key;
    }
    return hand.Replace("J", key.ToString());
}

HandOrder DetermineHandOrder(string hand)
{
    var cards = hand.ToArray();
    
    Console.WriteLine($"cards: {string.Join(",", cards)}");

    var cardOrderGrouped = cards.GroupBy(c => c).ToList();

    Console.WriteLine($"cardOrderGrouped: {string.Join(",", cardOrderGrouped.Select(c => $"{c.Key} {c.Count()}"))}");

    HandOrder handOrder = HandOrder.HighCard;

    if (cardOrderGrouped.Count == 5)
    {
        handOrder =  HandOrder.HighCard;
    }
    else if (cardOrderGrouped.Count == 4)
    {
        handOrder =  HandOrder.OnePair;
    }
    else if (cardOrderGrouped.Count == 3)
    {
        if (cardOrderGrouped.Any(c => c.Count() == 3))
        {
            handOrder =  HandOrder.ThreeOfAKind;
        }
        else
        {
            handOrder =  HandOrder.TwoPairs;
        }
    }
    else if (cardOrderGrouped.Count == 2)
    {
        if (cardOrderGrouped.Any(c => c.Count() == 4))
        {
            handOrder =  HandOrder.FourOfAKind;
        }
        else
        {
            handOrder =  HandOrder.FullHouse;
        }
    }
    else if (cardOrderGrouped.Count == 1)
    {
        handOrder =  HandOrder.FiveOfAKind;
    }
    else
    {
        throw new Exception("Unknown hand");
    }

    return handOrder;
}

var Hands = new List<HandBid>();
// Sort on hand
//   1. Sort by characters
foreach (var line in input)
{
    var aap = line.Split(" ");
    var hand = aap[0];
    var bid = aap[1];

    HandOrder handOrder = DetermineHandOrder(ReplaceJ(hand));
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

