using Microsoft.AspNetCore.Identity;
using NewZealandWalks.Models.Dto.Auth;

namespace NewZealandWalks.Repository.Interfaces
{
    public interface IIdentityRepository
    {
        Task<IdentityResult?> RegisterUserAsync(RegisterRequestDto registerRequestDto);
        Task<string?> LoginUserAsync(LoginRequestDto loginRequestDto);

    }
}
