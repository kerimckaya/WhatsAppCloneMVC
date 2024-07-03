using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WhatsAppCloneMVC.Models;
using WhatsAppCloneMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WhatsAppCloneMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ChatContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ChatContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                if (user == null)
                    return Unauthorized();

                var claims = new[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);


                ClaimsIdentity identity = new(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim("UserId", user.Id.ToString()));
                ClaimsPrincipal principal = new(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(3),
                    });

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

