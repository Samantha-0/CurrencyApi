using RestSharp;

Console.WriteLine("This program will convert the input amount from a given currency, to another currency. " +
                  "Exchange rate used is from the current date, unless a different date is supplied.");

var inputAmount = RequestAmountValue();
var inputFromCurrency = RequestCurrencyValue("base");
var inputToCurrency = RequestCurrencyValue("target");
var inputDate = RequestDateValue();

Console.WriteLine(
    $"{inputFromCurrency} {inputAmount} to {inputToCurrency}, using exchange rate from {inputDate:dd.MM.yyyy} is:");

var calculatedAmount = Calculate(inputFromCurrency, inputToCurrency, inputDate, inputAmount);
Console.WriteLine(calculatedAmount);

Console.ReadKey();

string RequestCurrencyValue(string type)
{
    while (true)
    {
        Console.WriteLine($"Enter the {type} currency as 3 character code");
        var s = Console.ReadLine() ?? string.Empty;
        if (s.Length == 3) return s;

        Console.WriteLine("Invalid format. Currency needs to be exactly 3 characters (for example: EUR, NOK, etc) ");
    }
}

DateTime RequestDateValue()
{
    Console.WriteLine("Enter exchange rate date (dd.MM.yyyy). Press enter to use today's date");
    var input = Console.ReadLine() ?? string.Empty;

    if (input == string.Empty)
        return DateTime.Now;

    try
    {
        var dDate = DateTime.Parse(input);
        return dDate;
    }
    catch (Exception)
    {
        Console.WriteLine("Invalid format. Write date in format dd.MM.yyyy");
        return RequestDateValue();
    }
}

decimal RequestAmountValue()
{
    while (true)
    {
        Console.WriteLine("Enter amount to convert");
        var s = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrEmpty(s))
        {
            Console.WriteLine("Amount is empty. Please enter a valid amount");
            continue;
        }

        if (decimal.TryParse(s, out var input)) return input;

        Console.WriteLine("Invalid format. Please enter a valid decimal value");
    }
}

decimal? Calculate(string from, string to, DateTime date, decimal amount)
{
    var client = new RestClient("https://localhost:44331/api/v1/currency/");

    var resource = $"calculate?from={from}&to={to}&date={date.ToShortDateString()}&amount={amount}";
    var request = new RestRequest(resource);

    var response = client.Execute(request);
    if (!response.IsSuccessful || response.Content == null) Console.WriteLine("Something went wrong");

    return decimal.Parse(response.Content);
}