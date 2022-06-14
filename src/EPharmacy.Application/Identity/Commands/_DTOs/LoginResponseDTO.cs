using EPharmacy.Application.Identity.Common.Queries.DTOs;

namespace EPharmacy.Application.Identity.Commands.DTOs;

public record class LoginResponseDTO
{
    public LoginResponseDTO(ApplicationUserDTO user, string token)
    {
        User = user;
        Token = token;
    }

    public ApplicationUserDTO User { get; set; }

    public string Token { get; set; }
}