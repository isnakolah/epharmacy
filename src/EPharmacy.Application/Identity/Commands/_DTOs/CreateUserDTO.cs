namespace EPharmacy.Application.Identity.Commands.DTOs;

public record CreateUserDTO(string FullName, string Gender, string PhoneNumber, string Email, string ConciergeID = default!);
