namespace EPharmacy.Application.Common.Models;

public sealed record class TextMessage
{
    private static string KE_PREFIX => "254";

    public TextMessage(string prefix, string number, string message)
    {
        (Prefix, Number, Message) = (prefix, number, message);
    }

    public string Prefix { get; init; }
    public string Number { get; init; }
    public string Message { get; set; }

    public static TextMessage Kenyan(string number, string message)
    {
        var validNumber = number.Length > 9 ? number[^9..] : number;

        return new(KE_PREFIX, validNumber, message);
    }
}