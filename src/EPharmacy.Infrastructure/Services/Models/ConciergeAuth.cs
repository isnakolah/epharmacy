using System.Text.Json.Serialization;

namespace EPharmacy.Infrastructure.Services.Models;

internal class ConciergeAuthRequestBody
{
    public ConciergeAuthRequestBody(string username, string password)
    {
        Username = username;
        Password = password;
    }

    [JsonPropertyName("Username")]
    public string Username { get; set; }

    [JsonPropertyName("Password")]
    public string Password { get; set; }
}