using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ClothesStoreMobileApplication.ViewModels.User;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ClothesStoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ClothesStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModels model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                return BadRequest("Username is already taken.");

   //         var hashedPassword = PasswordHelper.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
    //            Password = hashedPassword,
                Password = model.Password,
                Email = model.Email,
                Phone = model.Phone,
                UserType = model.UserType
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModels model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            //  if (user == null || !PasswordHelper.VerifyPassword(model.Password, user.Password)) 
            if (user == null || user.Password != model.Password)
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var key = new RsaSecurityKey(KeyHelper.GetPrivateKey());
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
