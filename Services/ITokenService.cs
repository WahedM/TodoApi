using Microsoft.AspNetCore.Identity;
using TodoApi.DTOs;

namespace TodoApi.Services
{
    public interface ITokenService
    {
        AuthResponseDto CreateToken(IdentityUser user);
    }
}
