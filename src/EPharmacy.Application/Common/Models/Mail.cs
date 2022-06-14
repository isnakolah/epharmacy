namespace EPharmacy.Application.Common.Models;

public sealed record class Mail(string To, string Subject, string Body);
