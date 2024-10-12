using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Models.Dto.Auth;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityRepository identityRepository;

        public AuthController(IIdentityRepository identityRepository)
        {
            this.identityRepository = identityRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register( RegisterRequestDto registerRequestDto)
        {
            var identityResult= await identityRepository.RegisterUserAsync(registerRequestDto);
            if (identityResult != null) return Ok();
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var token=await identityRepository.LoginUserAsync(loginRequestDto);
            if (token != null) return Ok(token); 
            return BadRequest("Username or Password Incorrect");
        }
    }
}
