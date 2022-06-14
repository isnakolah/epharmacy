namespace EPharmacy.Application.Identity.Commands.DTOs;

public sealed record class UpdatePasswordDTO(string NewPassword, string CurrentPassword);
