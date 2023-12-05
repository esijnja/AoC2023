var input = File.ReadAllLines("input.txt");
// for each line
// get first number
// get last number
// line value = first number * 10 + last number


List<(int @value, string digit)> digits = new List<(int @value, string digit)> 
{   
    (1,"1"), (2,"2"), (3,"3"), (4,"4"), (5,"5"), (6,"6"), (7,"7"), (8,"8"), (9,"9"),
    (1, "one"), (2, "two"), (3, "three"), (4, "four"), (5, "five"), (6, "six"), (7, "seven"), (8, "eight"), (9, "nine") 
};

int GetFirstNumber(string line)
{
    int index_ = line.Length;
    int retValue = 0;
    foreach (var digit in digits)
    {
        int index = line.IndexOf(digit.digit);
        if (index != -1 && index < index_)
        {
            index_ = index;
            retValue = digit.value;
        }
    }
    return retValue;
}

int GetLastNumber(string line)
{
    int index_ = -1;
    int retValue = 0;
    foreach (var digit in digits)
    {
        int index = line.LastIndexOf(digit.digit);
        if (index != -1 && index > index_)
        {
            index_ = index;
            retValue = digit.value;
        }
    }
    return retValue;
}

var value = 0;
foreach (var line in input)
{
    var firstNumber = GetFirstNumber(line);
    var lastNumber = GetLastNumber(line);
    var lineValue = firstNumber * 10 + lastNumber;
    Console.WriteLine($"{line} = {lineValue}");
    value += lineValue;
}

Console.WriteLine(value);