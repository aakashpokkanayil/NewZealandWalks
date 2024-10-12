using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NewZealandWalks.Models.Dto.Auth;
using NewZealandWalks.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewZealandWalks.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;

        public IdentityRepository(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string?> LoginUserAsync(LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                var passwordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (passwordResult)
                {
                    var roles=await userManager.GetRolesAsync(user);
                    return GenarateToken(user, roles.ToList());
                }

            }
            return null;
        }

        public async Task<IdentityResult?> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (identityResult.Succeeded) return identityResult;
            }

            return null;

        }

        public string GenarateToken(IdentityUser identityUser, List<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token= new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(5),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }

   
}
