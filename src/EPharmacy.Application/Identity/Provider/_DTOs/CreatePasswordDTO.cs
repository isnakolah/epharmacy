namespace EPharmacy.Application.Identity.Provider.DTO;

public record class CreatePasswordDTO(string Email, string PasswordToken, string NewPassword);
