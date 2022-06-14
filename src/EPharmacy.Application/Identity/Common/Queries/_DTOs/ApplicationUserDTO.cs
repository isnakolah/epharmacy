namespace EPharmacy.Application.Identity.Common.Queries.DTOs;

public record ApplicationUserDTO(string ID, string FullName, string Email, string PhoneNumber, string Gender, string Role, bool EmailVerified = false, bool PhoneNumberVerified = false);
