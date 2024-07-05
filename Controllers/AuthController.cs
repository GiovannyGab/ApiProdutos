using apiFornecedor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Senha);


            if (result.Succeeded)
            {
                
                var roleResult = await _userManager.AddToRoleAsync(user, "Usuario"); 
                if (roleResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok(GerarJwt(registerUser.Email));
                }
                else
                {
                  
                    return Problem("Falha ao adicionar o usuário à role");
                }
                await _signInManager.SignInAsync(user, false);

                return Ok(GerarJwt(registerUser.Email));
            }
            return Problem("Falha ao registrar usuario");

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginModel loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Senha, false, true);
            if (result.Succeeded)
            {
                return Ok(GerarJwt(loginUser.Email));
            }
            return Problem("Usuario ou senha incorreto");
        }
        private async Task<string> GerarJwt(string email)
        {


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoEmHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }
    }
}
