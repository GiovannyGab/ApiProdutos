using apiFornecedor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace apiFornecedor.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;


        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;

        }



        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UserRegisterModel registerUser)
        {

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Senha);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(await GerarJwt());
            }
            return Problem("Falha ao registrar usuario");

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginModel loginUser)
        {
            return Ok();
        }
    }
}
