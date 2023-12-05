

// read input
var input = File.ReadAllLines("input.txt");
// for each line
// get first number
// get last number
// line value = first number * 10 + last number
int GetFirstNumber(string line)
{
    foreach (var c in line)
    {
        if (char.IsDigit(c))
        {
            return int.Parse(c.ToString());
        }
    }
    return 0;
}

static string Reverse( string s )
{
    char[] charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

int GetLastNumber(string line)
{
    return GetFirstNumber(Reverse(line));
}

var value = 0;
foreach (var line in input)
{
    var firstNumber = GetFirstNumber(line);
    var lastNumber = GetLastNumber(line);
    var lineValue = firstNumber * 10 + lastNumber;
    value += lineValue;
}

Console.WriteLine(value);